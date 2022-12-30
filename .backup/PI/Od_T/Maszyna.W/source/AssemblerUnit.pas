unit AssemblerUnit;

interface

uses
  System.SysUtils, System.Classes;

type
  EAssemblerError = class ( Exception )
  private
    FLine, FCol : integer;
  public
    constructor Create( const Msg : string; const LineNbr, ColNbr : integer );
    property ColNumber : integer read FCol;
    property LineNumber : integer read FLine;
  end;

type
  TAssembler = class
  private
    FCompiled: Boolean;
    FLine: integer;
    FColumn: integer;
    FAsmLines : array [ 0 .. 2047 ] of integer; // kod programu w wersji skompilowanej
    FTxtLines : array [ 0 .. 2047 ] of integer; // numery linii kodu źródłowego odpowiadające danej lini programu - dla debuggera
    FAsmLinesCount: integer;
    function GetAsmLine(Index: integer): integer;
    function GetTxtLine(Index: integer): integer;
  protected
  public
    procedure Compile( Text : TStrings );
    property Compiled : Boolean read FCompiled;
    property Column : integer read FColumn;
    property Line : integer read FLine;
    property AsmLines[ Index: integer ] : integer read GetAsmLine;
    property TxtLines[ Index : integer ] : integer read GetTxtLine;
    property AsmLinesCount : integer read FAsmLinesCount;
  end;

implementation

uses
  System.Character, Languages, Microcontroller, ControlUnit;

{ TAssembler }

// usuń komentarze i zamień tekst na duże litery
function Uncomment( Text : string ) : string;
var
  i : integer;
begin
  i := Pos( '//', Text );
  if i > 0 then
    Result := Copy( Text, 1, i - 1 ).ToUpper
  else
    Result := Text.ToUpper;
end;

// funkcja pobiera jeden element leksykalny, począwszy od wskazanego wiersz
// i kolumny, przy okazji przesuwając wskaźniki
function GetItem( Line : string; var Col : integer ) : string;
type
  TMode = ( moEmpty, moAlpha, moNumber, mo1stChar, mo2ndChar );
var
  mode : TMode;
  i, j : integer;
function ProcessEmpty( Ch : char ) : TMode;
begin
  if Ch.IsDigit or ( Ch = '+' ) or ( Ch = '-' ) then Result := moNumber
  else if Ch.IsLetter then Result := moAlpha
  else if Ch = '''' then Result := mo1stChar
  else
  begin
    raise EAssemblerError.CreateFmt( lsUnexpectedChar, [ Ch ] );
  end;
end;
begin // GetItem
  Result := '';
  Line := Copy( Line, Col, 2048 );
  if Trim( Line ) = '' then
    exit;
  mode := moEmpty;
  j := 1;
  for i := 1 to Length( Line ) do
  begin
    j := i;
    case mode of
      moEmpty: if not Line[ i ].IsWhiteSpace then mode := ProcessEmpty( Line[ i ] );
      moAlpha: if not Line[ i ].IsLetterOrDigit then break;
      moNumber: if not Line[ i ].IsDigit then break;
      mo1stChar: ;
      mo2ndChar: if Line[ i ] = '''' then break else raise EAssemblerError.CreateFmt( lsUnexpectedChar, [ Line[ i ] ] );
    end;
  end;
  Result := Trim( Copy( Line, 1, j ) );
  Inc( Col, j );
end;

procedure TAssembler.Compile(Text: TStrings);
var
  i, j, k, Adres : integer;
  S, Labels : TStringList;
  item : string;
  mode : ( moLabel, moInstruction );
procedure NoArgument;
begin
  // tu już nic nie może być
  item := GetItem( S.Strings[ i ], FColumn );
  if item <> '' then
    raise EAssemblerError.Create( Format( lsUnExpectedText, [ item ] ), FLine, FColumn );
end;
function GetArgument( Last : Boolean ) : integer;
var
  ii : integer;
begin
  item := GetItem( S.Strings[ i ], FColumn );
  if item = '' then
    raise EAssemblerError.Create( lsArgumentExpected, FLine, FColumn );
  if not Last then
  begin
    if item[ Length( item ) ] <> ',' then
      raise EAssemblerError.Create( lsCommaExpected, FLine, FColumn );
    item := Copy( item, 1, Length( item ) - 1 ); // pomijamy przecinek
  end;
  if ( Length( item ) = 3 ) and ( item[ 1 ] = '''' ) and ( item[ 3 ] = '''' ) then // znak podany w apostrofach
    Result := Ord( item[ 2 ] )
  else if ( item[ 1 ].IsDigit ) or ( item[ 1 ] = '+' ) or ( item[ 1 ] = '-' ) then
    Result := StrToInt( item )
  else // to może być tylko etykieta
  begin
    ii := Labels.IndexOf( item );
    if ii < 0 then
      raise EAssemblerError.Create( Format( lsUnknownLabel, [ item ] ),FLine, FColumn );
    Result := Integer( Labels.Objects[ ii ] );
  end;
end;
begin  // TAssembler.Compile
  Adres := 0;
  Labels := TStringList.Create;
  try
    S := TStringList.Create;
    try
      // pierwszy przebieg asemblacji
      S.Assign( Text );
      // dla każdej linii
      for i := 0 to S.Count - 1 do  // usuń komentarze
      begin
        S.Strings[ i ] := Uncomment( S.Strings[ i ] );
        FColumn := 1;
        FLine := i + 1;
        mode := moLabel;
        item := GetItem( S.Strings[ i ], FColumn );
        while item <> '' do
        begin
          // DEBUG: Text.Add( '[' + item + ']'#9 + IntToStr( i + 1 ) + ' ' + IntToStr( FColumn ) );
          if ( mode = moLabel ) and ( item[ Length( item ) ] = ':' ) then
            Labels.AddObject( Copy( item, 1, Length( item ) - 1 ), TObject( Adres ) )
          else if mode = moLabel then
          begin // to rozkaz lub pseudorozkaz - przesuń odpowiednio adres
            // czy to pseudorozkaz ?
            if ( item = lsDirectiveRST ) or ( item = lsDirectiveRPA ) then
              Inc( Adres )
            else // może to jakiś rozkaz z listy rozkazów ?
            begin
              j := Controller.Mnemonic2Code( item );
              if j < 0 then
                raise EAssemblerError.Create( Format( lsUnknownInstruction, [ item ] ), FLine, FColumn);
              if Controller.Instructions[ j ].Arguments > 1 then
                Inc( Adres, Controller.Instructions[ j ].Arguments )
              else
                Inc( Adres );
            end;
            mode := moInstruction;
          end;
          item := GetItem( S.Strings[ i ], FColumn );
        end;
      end;
      {_______________________________________________________________________}
      // drugi przebieg asemblacji
      FAsmLinesCount := 0;
      Adres := 0;
      for i := 0 to 2047 do
      begin
        FAsmLines[ i ] := 0;
        FTxtLines[ i ] := 0;
      end;
      // dla każdej linii
      for i := 0 to S.Count - 1 do
      begin
        FColumn := 1;
        FLine := i + 1;
        mode := moLabel;
        item := GetItem( S.Strings[ i ], FColumn );
        while item <> '' do
        begin
          if ( mode = moLabel ) and ( item[ Length( item ) ] = ':' ) then
            // nie rób nic
          else if mode = moLabel then
          begin // to rozkaz lub pseudorozkaz - przesuń odpowiednio adres
            // czy to pseudorozkaz ?
            if item = lsDirectiveRST then
            begin
              // pobierz stałą
              FAsmLines[ Adres ] := GetArgument( true );
              FTxtLines[ Adres ] := i;
              NoArgument;
              Inc( Adres );
            end
            else if item = lsDirectiveRPA then
            begin
              NoArgument;
              FTxtLines[ Adres ] := i;
              Inc( Adres )
            end
            else // może to jakiś rozkaz z listy rozkazów ?
            begin
              j := Controller.Mnemonic2Code( item );
              if j < 0 then
                raise EAssemblerError.Create( Format( lsUnknownInstruction, [ item ] ), FLine, FColumn);
              FAsmLines[ Adres ] := j shl Computer.AddressLength;
              FTxtLines[ Adres ] := i;
              for k := 1 to Controller.Instructions[ j ].Arguments do
              begin
                FAsmLines[ Adres ] := FAsmLines[ Adres ] or GetArgument( k = Controller.Instructions[ j ].Arguments );
                FTxtLines[ Adres ] := i;
                Inc( Adres );
              end;
              if Controller.Instructions[ j ].Arguments = 0 then
                Inc( Adres );
              NoArgument;
            end;
            mode := moInstruction;
          end;
          item := GetItem( S.Strings[ i ], FColumn );
        end;
        FAsmLinesCount := Adres;
      end;
       {_____________________________________________________________________}
    finally
      S.Free;
    end;
    { DEBUG
    // pokaż tablicę etykiet
    Text.Add( '__________________' );
    for i := 0 to FAsmLinesCount - 1 do
      Text.Add( IntToStr( FAsmLines[ i ] ) + '  [' + IntToStr( FTxtLines[ i ] + 1 ) +']' );
    }
  finally
    Labels.Free;
  end;
end;

function TAssembler.GetAsmLine(Index: integer): integer;
begin
  if Index < FAsmLinesCount then
    Result := FAsmLines[ Index ]
  else
    raise Exception.Create('Error in AssemblerUnit.TAssembler.GetAsmLine');
end;

function TAssembler.GetTxtLine(Index: integer): integer;
begin
  if Index < FAsmLinesCount then
    Result := FTxtLines[ Index ]
  else
    raise Exception.Create('Error in AssemblerUnit.TAssembler.GetTxtLine');
end;

{ EAssemblerError }

constructor EAssemblerError.Create(const Msg: string; const LineNbr,
  ColNbr: integer);
begin
  inherited Create( Msg );
  FCol := ColNbr;
  FLine := LineNbr;
end;

end.

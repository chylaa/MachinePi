unit Compiler;
{ Kompiluje opis rozkazu zamieniając tekst w tablicę linii
  elementów Microcontroller.TLine, wydziela także nazwę rozkazu
  i liczbę jego parametrów
}


interface

uses
  System.Classes, System.SysUtils,
  ControlUnit, Microcontroller;

type
  TCompiler = class
  private
    FArgs : integer;
    FArchitecture : TSignalSet;
    FColumn, FRow : integer;   // kolumna i wiersz miejsca z błędem
    FErrorMsg : string;
    FMnemonic : string;
    FOutput : array[ 0 .. 63 ] of TLine;
    FOutputCount : integer;
    FReady : Boolean;   // info, czy podjęto już próbę kompilacji
    FSuccess : Boolean;
    function GetMnemonic: string;
    function GetArgs: integer;
    function GetOutput(Index: integer): TLine;
    function GetArchitecture: TSignalSet;
    function GetSuccess: Boolean;
    function GetErrorMsg: string;
  protected
  public
    constructor Create;
    destructor Destroy; override;
    procedure Compile( Text : TStrings );
    // po kompilacji stają się dostępne poniższe własności
    property Args : integer read GetArgs; // liczba argumentów rozkazu
    property Architecture : TSignalSet read GetArchitecture; // jakie sygnały sterujące są używane
    property ErrorMessage : string read GetErrorMsg; // komunikat o błędzie
    property ErrorColumn : integer read FColumn;  // okresla położenie błędu w tekście
    property ErrorRow : integer read FRow;        //   - j.w. -
    property Mnemonic : string read GetMnemonic; // nazwa rozkazu
    property Output[ Index : integer ] : TLine read GetOutput; // "skompilowany" rozkaz
    property OutputCount : integer read FOutputCount; // ile linii TLine tworzy Output
    property Success : Boolean read GetSuccess; // czy kompilacja zakończona sukcesem
  end;

type
  ECompilerError = class( Exception );

function SignalName( Signal : TSignals ) : string;

implementation

uses
  System.Character,
  Languages;

var
  SignalNames : array[ TSignals ] of string;

function SignalName( Signal : TSignals ) : string;
begin
  Result := SignalNames[ Signal ];
end;

{ TCompiler }

procedure TCompiler.Compile( Text: TStrings );
type
  TCycle = record
    Signals : TSignalSet;
    Condition : TCondition;
    NextTrue : integer;
    NextFalse : integer;
    CurrentLabel : string;
    TrueLabel : string;
    FalseLabel : string;
  end;
var
  Lines : TStringList;
  Cycles : array [ 0 .. 63 ] of TCycle;
  CycleCount : integer;
// usuwa komentarze i zamienia wszystko na małe litery
procedure UnComment;
var
  i, j : integer;
  Temp : string;
begin
  for i := 0 to Lines.Count - 1 do
  begin
    Temp := Lines[ i ];
    j := Pos( '//', Temp );
    if j > 0 then
      Temp := Copy( Temp, 1, j - 1 );
    Lines[ i ] := AnsiLowerCase( Temp );
  end;
end; // UnComment
// pobiera z Lines po jednej jednostce składniowej i przesuwa FRow i FColumn
// jak już nic nie ma, zwraca tekst pusty
function GetItem : string;
var
  CurrentLine : string;
  ch : char;
  i, j : integer;
  State : ( stEmpty, stAlpha, stSemicolon, stAtSign );
begin
  Result := '';
  State := stEmpty;
  repeat
    CurrentLine := Copy( Lines[ FRow ], FColumn, 2048 );
    j := 0;
    for i := 1 to Length( CurrentLine ) do
    begin
      ch := CurrentLine[ i ];
      if ch.IsLetterOrDigit then
        // dopóki są kolejne litery i cyfry to jest słowo
        case State of
          stEmpty: begin State := stAlpha; continue end;
          stAtSign,
          stAlpha: continue;
          stSemicolon: raise Exception.Create( 'Exception in TCompiler.Compile.GetItem' ); // niemożliwe
        end
      else if ch = ';' then
        case State of
          stEmpty: begin State := stSemicolon; j := i; break end;
          stAtSign,
          stAlpha: begin j := i - 1; break end;
          stSemicolon: raise Exception.Create( 'Exception in TCompiler.Compile.GetItem' ); // niemożliwe
        end
      else if ch = '@' then
        case State of
          stEmpty: begin State := stAtSign; continue end;
          stAlpha,
          stAtSign: begin j := i - 1; FColumn := FColumn + j; raise ECompilerError.Create( lsAtInsideSignalName ) end;
          stSemicolon: raise Exception.Create( 'Exception in TCompiler.Compile.GetItem' ); // niemożliwe
        end
      else if ch.IsWhiteSpace then
        case State of
          stEmpty: continue;
          stAtSign,
          stAlpha: begin j := i; break end;
          stSemicolon: raise Exception.Create( 'Exception in TCompiler.Compile.GetItem' ); // niemożliwe
        end
      else
      begin
        j := i - 1;
        FColumn := FColumn + j;
        raise ECompilerError.CreateFmt( lsUnexpectedCharacter, [ ch ] );
      end;
    end;
    // opuścił for - albo coś znalazł albo pusty wiersz
    case State of
      stEmpty: begin if FRow = ( Lines.Count - 1 ) then exit; Inc( FRow ); FColumn := 1 end;
      stAtSign,
      stAlpha: begin FColumn := FColumn + j; Result := Trim( Copy( CurrentLine, 1, j ) ); end;
      stSemicolon: begin FColumn := FColumn + j; Result := ';' end;
    end;
  until State <> stEmpty;
end; // GetItem;
var
  Item : string;
  ch : char;
  i, j : integer;
  Found : Boolean;
procedure CycleProcessing;
var
  // jaki element był ostatnio przeczytany
  State : ( stStart, stSignals, stIf, stCondition, stThen, stIfLabel, stElse1, stElse2, stElseLabel, stGoto, stGotoLabel, stEnd );
  Found : Boolean;
  signal : TSignals;
begin // CycleProcessing
  Cycles[ CycleCount ].Signals := [];
  Cycles[ CycleCount ].Condition := cnNone;
  Cycles[ CycleCount ].CurrentLabel := '';
  Cycles[ CycleCount ].TrueLabel := '';
  Cycles[ CycleCount ].FalseLabel := '';
  State := stStart;
  // czytanie opisu linii i przetwarzanie pojedynczego taktu
  repeat
     Found := false;
     for signal := Low( TSignals ) to High( TSignals ) do
       if Item = SignalNames[ signal ] then
       begin
         Cycles[ CycleCount ].Signals := Cycles[ CycleCount ].Signals + [ signal ];
         Found := true;
       end;
     if not Found and ( Item = lsSignalNameSa ) then
     begin
       Cycles[ CycleCount ].Signals := Cycles[ CycleCount ].Signals + [ sgAs ];
       Found := true;
     end;
     if Found and ( State > stSignals ) then
     begin
       raise ECompilerError.CreateFmt( lsWrongSignalPosition, [ Item ] );
     end;
     if Found then
        State := stSignals
     else if Item = lsIfKeyword then
     begin
       if State > stSignals then  // tu if już nie może się pojawić
         raise ECompilerError.CreateFmt( lsUnexpectedWord, [ Item ]);
       State := stIf;
     end
     else if Item = lsThenKeyword then
     begin
       if State <> stCondition then
         raise ECompilerError.CreateFmt( lsUnexpectedWord, [ Item ]);
       State := stThen;
     end
     else if Item = lsElse1Keyword then
     begin
       if State <> stIfLabel then
         raise ECompilerError.CreateFmt( lsUnexpectedWord, [ Item ]);
       if lsElse2Keyword = '' then
         State := stElse2
       else
         State := stElse1;
     end
     else if Item = lsElse2Keyword then
     begin
       if State <> stElse1 then
         raise ECompilerError.CreateFmt( lsUnexpectedWord, [ Item ]);
       State := stElse2
     end
     else if Item = lsGotoKeyword then
     begin
       if State > stSignals then  // tu if już nie może się pojawić
         raise ECompilerError.CreateFmt( lsUnexpectedWord, [ Item ]);
       State := stGoto;
     end
     else if Item = lsEndKeyword then
     begin
       if State > stSignals then  // tu if już nie może się pojawić
         raise ECompilerError.CreateFmt( lsUnexpectedWord, [ Item ]);
       State := stEnd;
       Cycles[ CycleCount ].Condition := cnLastCycle;
     end
     // może to jakiś warunek
     else if Item = lsConditionNegative then
     begin
       if State <> stIf then
         raise ECompilerError.CreateFmt( lsUnexpectedWord, [ Item ]);
       State := stCondition;
       Cycles[ CycleCount ].Condition := cnNegative;
     end
     else if Item = lsConditionZero then
     begin
       if State <> stIf then
         raise ECompilerError.CreateFmt( lsUnexpectedWord, [ Item ]);
       State := stCondition;
       Cycles[ CycleCount ].Condition := cnZero;
     end
     else if Item = lsConditionInterrupt then
     begin
       if State <> stIf then
         raise ECompilerError.CreateFmt( lsUnexpectedWord, [ Item ]);
       State := stCondition;
       Cycles[ CycleCount ].Condition := cnInterrupt;
     end
     else if Item[ 1 ] = '@' then // etykieta
     begin
       case State of
         stStart: begin
                    Cycles[ CycleCount ].CurrentLabel := Item;
                    State := stSignals
                  end;
         stThen: begin
                    Cycles[ CycleCount ].TrueLabel := Item;
                    State := stIfLabel
                 end;
         stElse2: begin
                    Cycles[ CycleCount ].FalseLabel := Item;
                    State := stElseLabel
                  end;
         stGoto: begin
                    Cycles[ CycleCount ].TrueLabel := Item;
                    Cycles[ CycleCount ].FalseLabel := Item;
                    State := stGotoLabel
                 end;
       else
         raise ECompilerError.CreateFmt( lsUnexpectedWord, [ Item ]);
       end;
     end
     else
       raise ECompilerError.CreateFmt( lsUnexpectedWord, [ Item ] );
     Item := GetItem;
  until ( Item = '' ) or ( Item = ';' );
end;  // CycleProcessing
begin  // TCompiler.Compile
  FReady := true;
  Lines := TStringList.Create;
  try
    // kopiuję tekst
    Lines.Assign( Text );
    // usuwam z niego komentarze i konwertuję na małe litery
    UnComment;
    FRow := 0;
    FColumn := 1;
    // zaczynamy od początku i szukamy słowa ROZKAZ
    Item := GetItem;
    if Item <> lsInstructionKeyword then // to nie słowo rozkaz
      raise ECompilerError.Create( lsInstructionKeywordExpected );
    Item := GetItem;  // teraz nazwa rozkazu
    ch := Item[ 1 ];
    if ch.IsLetter then // to może być nazwa rozkazu
      FMnemonic := Item;
    Item := GetItem;
    if Item <> ';' then
      raise ECompilerError.Create( lsSemicolonExpected );
    Item := GetItem;
    // ocjonalnie może się pojawić linia z liczbą argumentów
    if Item = lsArgumentsKeyword then // jest info o argumentach
    begin
      Item := GetItem;
      FArgs := StrToInt( Item );
      Item := GetItem;
      if Item <> ';' then
        raise ECompilerError.Create( lsSemicolonExpected );
      Item := GetItem;
    end
    else  // ustawianie wartości domyślnej nie jest konieczne ale jest
      FArgs := 1; // wartość domyślna
    // teraz musi być opis taktów
    CycleCount := 0;
    while Item <> '' do
    begin
      CycleProcessing;
      Item := GetItem;
      Inc( CycleCount );
    end;
    // pełnych taktów jest CycleCount numerowanych od 0 do CycleCount - 1
    with Cycles[ CycleCount - 1 ] do
      if Condition = cnNone then
        Condition := cnLastCycle;
    // pierwszy takt czyt wys wei il i bez warunku
    if ( Cycles[ 0 ].Signals <> [ sgCzyt, sgWys, sgWei, sgIl ] ) or ( Cycles[ 0 ].Condition <> cnNone ) then
      raise ECompilerError.Create( lsUnexpected1stCycle );
    Cycles[ 0 ].Condition := cnFirstCycle;
    // wszystkie takty domyślnie wskazują na następny (za wyjątkiem pierwszego i ostatniego)
    for i := 1 to CycleCount - 2 do
      with Cycles[ i ] do
        if Condition = cnNone then
        begin
          NextTrue := i + 1;
          NextFalse := i + 1;
        end;
    Cycles[ 0 ].NextTrue := 0;
    Cycles[ 0 ].NextFalse := 0;
    if Cycles[ CycleCount - 1 ].Condition = cnNone then
    begin
      Cycles[ CycleCount - 1 ].Condition := cnLastCycle;
      Cycles[ CycleCount - 1 ].NextTrue := 0;
      Cycles[ CycleCount - 1 ].NextFalse := 0;
    end;
    // ustal adresy na podstawie etykiet
    for i := 0 to CycleCount - 1 do
    begin
      if Cycles[ i ].TrueLabel <> '' then
      begin
        Found := false;
        for j := 0 to CycleCount - 1 do
          if Cycles[ j ].CurrentLabel = Cycles[ i ].TrueLabel then
          begin
            Cycles[ i ].NextTrue := j;
            Found := true;
            break;
          end;
        if not Found then
          raise ECompilerError.CreateFmt( lsUnknownLabel, [ Cycles[ i ].TrueLabel ]);
      end;
      if Cycles[ i ].FalseLabel <> '' then
      begin
        Found := false;
        for j := 0 to CycleCount - 1 do
          if Cycles[ j ].CurrentLabel = Cycles[ i ].FalseLabel then
          begin
            Cycles[ i ].NextFalse := j;
            Found := true;
            break;
          end;
        if not Found then
          raise ECompilerError.CreateFmt( lsUnknownLabel, [ Cycles[ i ].TrueLabel ]);
      end;
    end;
    // teraz trzeba przekopiować opis rozkazu do FOutput
    for i := 0 to CycleCount - 1 do
    begin
      FOutput[ i ].Signals := Cycles[ i ].Signals;
      FArchitecture := FArchitecture + Cycles[ i ].Signals;
      FOutput[ i ].Condition := Cycles[ i ].Condition;
      FOutput[ i ].NextTrue := Cycles[ i ].NextTrue;
      FOutput[ i ].NextFalse := Cycles[ i ].NextFalse;
    end;
    FOutputCount := CycleCount;
    // TODO: sprawdzenie, czy wszystkie sygnały są dostępne dla bieżącej architektury
    FSuccess := true;
  finally
    Lines.Free;
  end;
end;  // TCompiler.Compile

constructor TCompiler.Create;
var
  i : integer;
begin
  FReady := false;
  FArgs := 1;  // domyślnie 1 argument i gdy brak w opisie informacji tak zakładamy
  FArchitecture := [];  // wstępnie żaden sygnał nie jest potrzebny
  FErrorMsg := '';      // najpierw brak komunikatów o błędach
  FMnemonic := '???';   // nieznana nazwa rozkazu
  FOutputCount := 0;    // zerowa liczba taktów
  FSuccess := false;    // jeszcze nie skompilowano
  for i := Low( FOutput ) to High( FOutput ) do
    with FOutput[ i ] do
    begin
      Signals := [];
      Condition := cnUnused;
      NextTrue := 0;
      NextFalse := 0;
    end;
  FColumn := 0;
  FRow := 0;
end;

destructor TCompiler.Destroy;
begin

  inherited;
end;

function TCompiler.GetArchitecture: TSignalSet;
begin
  if not FReady then
    raise Exception.Create( 'Exception in TCompiler.GetArchitecture' );
  Result := FArchitecture;
end;

function TCompiler.GetArgs: integer;
begin
  if not FReady then
    raise Exception.Create( 'Exception in TCompiler.GetArgs' );
  Result := FArgs;
end;

function TCompiler.GetErrorMsg: string;
begin
  if not FReady then
    raise Exception.Create( 'Exception in TCompiler.GetErrorMsg' );
  Result := FErrorMsg;
end;

function TCompiler.GetMnemonic: string;
begin
  if not FReady then
    raise Exception.Create( 'Exception in TCompiler.GetMnemonic' );
  Result := FMnemonic;
end;

function TCompiler.GetOutput(Index: integer): TLine;
begin
  if not FReady then
    raise Exception.Create( 'Exception in TCompiler.GetOutput' );
  if ( Index < 0 ) or ( Index >= FOutputCount ) then
    raise Exception.Create( 'Range error in TCompiler.GetOutput' );
  Result := FOutput[ Index ];
end;

function TCompiler.GetSuccess: Boolean;
begin
  if not FReady then
    raise Exception.Create( 'Exception in TCompiler.GetSuccess' );
  Result := FSuccess;
end;

initialization
  SignalNames[ sgstop ] := lsSignalNameStop ;
  SignalNames[ sgczyt ] := lsSignalNameCzyt ;
  SignalNames[ sgwyad ] := lsSignalNameWyAD ;
  SignalNames[ sgwyl ] := lsSignalNameWyl ;
  SignalNames[ sgwyls ] := lsSignalNameWyls ;
  SignalNames[ sgwyak ] := lsSignalNameWyak ;
  SignalNames[ sgwys ] := lsSignalNameWys ;
  SignalNames[ sgwyx ] := lsSignalNameWyx ;
  SignalNames[ sgwyy ] := lsSignalNameWyy ;
  SignalNames[ sgwyws ] := lsSignalNameWyWS ;
  SignalNames[ sgwyrb ] := lsSignalNameWyrb ;
  SignalNames[ sgwyg ] := lsSignalNameWyG ;
  SignalNames[ sgwyap ] := lsSignalNameWyap ;
  SignalNames[ sgwyrm ] := lsSignalNameWyrm ;
  SignalNames[ sgpisz ] := lsSignalNamePisz ;
  SignalNames[ sgas ] := lsSignalNameAS ;
  SignalNames[ sgweja ] := lsSignalNameWeja ;
  SignalNames[ sgneg ] := lsSignalNameNeg ;
  SignalNames[ sglub ] := lsSignalNameLub ;
  SignalNames[ sgi ] := lsSignalNameI ;
  SignalNames[ sgdod ] := lsSignalNameDod ;
  SignalNames[ sgode ] := lsSignalNameOde ;
  SignalNames[ sgprzep ] := lsSignalNamePrzep ;
  SignalNames[ sgmnoz ] := lsSignalNameMnoz ;
  SignalNames[ sgdziel ] := lsSignalNameDziel ;
  SignalNames[ sgshr ] := lsSignalNameShr ;
  SignalNames[ sgshl ] := lsSignalNameShl ;
  SignalNames[ sgwei ] := lsSignalNameWei ;
  SignalNames[ sgil ] := lsSignalNameIl ;
  SignalNames[ sgwel ] := lsSignalNameWel ;
  SignalNames[ sgwea ] := lsSignalNameWea ;
  SignalNames[ sgwes ] := lsSignalNameWes ;
  SignalNames[ sgiak ] := lsSignalNameIAk ;
  SignalNames[ sgdak ] := lsSignalNameDAK ;
  SignalNames[ sgweak ] := lsSignalNameWeak ;
  SignalNames[ sgwex ] := lsSignalNameWex ;
  SignalNames[ sgwey ] := lsSignalNameWey ;
  SignalNames[ sgiws ] := lsSignalNameIws ;
  SignalNames[ sgiws ] := lsSignalNameDws ;
  SignalNames[ sgwews ] := lsSignalNameWeWs ;
  SignalNames[ sgwerb ] := lsSignalNameWerb ;
  SignalNames[ sgstart ] := lsSignalNameStart ;
  SignalNames[ sgrint ] := lsSignalNameRint ;
  SignalNames[ sgeni ] := lsSignalNameEni ;
  SignalNames[ sgwerm ] := lsSignalNameWerm ;

end.

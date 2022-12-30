unit Microcontroller;

{
  TODO: realizuje funkcje standardowego układu sterującego
}

interface

uses
  ControlUnit, Classes, System.SysUtils;

type
  TBeforeExecuteCycleEvent = procedure ( Sender : TObject; SignalSet : TSignalSet ) of object;
  TInstructionEvent = procedure ( Sender : TObject; InstructionAddress : integer ) of object;

type
  TTraceLevel = ( tlCycle, tlInstruction, tlProgram ); // może dałoby się tlSubroutine ???

type
  TCondition = ( cnUnused, cnFirstCycle, cnNone, cnNegative, cnZero, cnInterrupt,
                 cnLastCycle );

type
  TLine = record  // opis pojedynczego taktu
    Signals : TSignalSet;
    Condition : TCondition;
    NextTrue : integer;
    NextFalse : integer;
  end;

type
  TInstruction = record
    Mnemonic : string;
    Code : integer;
    Arguments : integer;
    UsedSignals : TSignalSet;
  end;

const
  MaxLines = 256;
  SecondCycleOffset = MaxLines - 32;

type
  TMicroProgram = class
  private
    function GetLines(Address: integer): TLine;
  protected
  public
    property Lines[ Address : integer ] : TLine read GetLines;
  end;

type
  TMicroControl = class
  private
    CurrentLine : integer;
    FLines : array [ 0 .. MaxLines - 1 ] of TLine;
    FAfterInstruction: TInstructionEvent;
    FBeforeExecuteCycle: TBeforeExecuteCycleEvent;
    FBeforeInstruction: TInstructionEvent;
    FInstructions : array [ 0 .. 31 ] of TInstruction;
    procedure SetBeforeExecuteCycle(const Value: TBeforeExecuteCycleEvent);
    procedure SetAfterInstruction(const Value: TInstructionEvent);
    procedure SetBeforeInstruction(const Value: TInstructionEvent);
    function GetInstructions(ACode: integer): TInstruction;
    procedure DeleteInstruction( ACode : integer ); // usuwa rozkaz z listy rozkazów
  protected
    procedure ClearLines;
  public
    // tu dodać Breakpoints i Watches
    // tu przechowywać projekty rozkazów a może i kod asemblerowy
    // określić poziom śledzenia TraceLevel
    procedure ExecuteNextCycle;
    procedure ExecuteInstruction;
    procedure ExecuteProgram;
    procedure CompileInstruction( Text : TStrings );
    procedure Reset;
    // może dałoby się ExecuteSubroutine
    constructor Create;
    destructor Destroy; override;
    // zdarzenia
    // po kazdym wykonanym rozkazie można np. odświerzyć okno z podglądem zmiennych Watches
    property AfterInstruction : TInstructionEvent read FAfterInstruction write SetAfterInstruction;
    // przed kazdym wykonywanym taktem można np. uaktywnić sygnały
    property BeforeExecuteCycle : TBeforeExecuteCycleEvent read FBeforeExecuteCycle write SetBeforeExecuteCycle;
    // przed kazdym wykonywanym rozkazem w trybie ciągłym można np. sprawdzić breakpointy
    property BeforeInstruction : TInstructionEvent read FBeforeInstruction write SetBeforeInstruction;
    // tu trzymana jest lista rozkazów
    property Instructions [ ACode : integer ] : TInstruction read GetInstructions;
    function Mnemonic2Code( Text : string ) : integer; // zwraca -1, gdy brak rozkazu o podanej nazwie
  end;

var
  Controller : TMicroControl;

implementation

uses
  Languages, Compiler;

{ TMicroControl }

procedure TMicroControl.ClearLines;  // ustawia wartości domyślne
var
  i : integer;
begin
  for i := 0 to 31 do
  begin
    FInstructions[ i ].Mnemonic := '';
    FInstructions[ i ].Code := 1;
    FInstructions[ i ].Arguments := 1;
    FInstructions[ i ].UsedSignals := [];
  end;
  for i := 0 to MaxLines - 1 do
    with FLines[ i ] do
    begin
      Signals := [];
      Condition := cnUnused;
      NextTrue := 0;
      NextFalse := 0;
    end;
  // i ustawia domyślne rozkazy
  FLines[ 0 ].Signals := [ sgCzyt, sgWys, sgWei, sgIl ];
  FLines[ 0 ].Condition := cnFirstCycle;
  // STP kod 0
  FLines[ SecondCycleOffset + 0 ].Signals := [ sgStop ];
  FLines[ SecondCycleOffset + 0 ].Condition := cnLastCycle;
  FLines[ SecondCycleOffset + 0 ].NextTrue := 0;
  FLines[ SecondCycleOffset + 0 ].NextFalse := 0;
  // DOD kod 1
  FLines[ SecondCycleOffset + 1 ].Signals := [ sgWyAD, sgWea ];
  FLines[ SecondCycleOffset + 1 ].Condition := cnNone;
  FLines[ SecondCycleOffset + 1 ].NextTrue := 1;
  FLines[ SecondCycleOffset + 1 ].NextFalse := 1;
  FLines[ 1 ].Signals := [ sgCzyt, sgWys, sgWeja, sgDod, sgWeak, sgWyl, sgWea ];
  FLines[ 1 ].Condition := cnLastCycle;
  FLines[ 1 ].NextTrue := 0;
  FLines[ 1 ].NextFalse := 0;
  // ODE kod 2
  FLines[ SecondCycleOffset + 2 ].Signals := [ sgWyAD, sgWea ];
  FLines[ SecondCycleOffset + 2 ].Condition := cnNone;
  FLines[ SecondCycleOffset + 2 ].NextTrue := 2;
  FLines[ SecondCycleOffset + 2 ].NextFalse := 2;
  FLines[ 2 ].Signals := [ sgCzyt, sgWys, sgWeja, sgOde, sgWeak, sgWyl, sgWea ];
  FLines[ 2 ].Condition := cnLastCycle;
  FLines[ 2 ].NextTrue := 0;
  FLines[ 2 ].NextFalse := 0;
  // ŁAD kod 3
  FLines[ SecondCycleOffset + 3 ].Signals := [ sgWyAD, sgWea, sgWyak, sgWes ];
  FLines[ SecondCycleOffset + 3 ].Condition := cnNone;
  FLines[ SecondCycleOffset + 3 ].NextTrue := 3;
  FLines[ SecondCycleOffset + 3 ].NextFalse := 3;
  FLines[ 3 ].Signals := [ sgPisz, sgWyl, sgWea ];
  FLines[ 3 ].Condition := cnLastCycle;
  FLines[ 3 ].NextTrue := 0;
  FLines[ 3 ].NextFalse := 0;
  // POB kod 4
  FLines[ SecondCycleOffset + 4 ].Signals := [ sgWyAD, sgWea ];
  FLines[ SecondCycleOffset + 4 ].Condition := cnNone;
  FLines[ SecondCycleOffset + 4 ].NextTrue := 4;
  FLines[ SecondCycleOffset + 4 ].NextFalse := 4;
  FLines[ 4 ].Signals := [ sgCzyt, sgWys, sgWeja, sgPrzep, sgWeak, sgWyl, sgWea ];
  FLines[ 4 ].Condition := cnLastCycle;
  FLines[ 4 ].NextTrue := 0;
  FLines[ 4 ].NextFalse := 0;
  // SOB kod 5
  FLines[ SecondCycleOffset + 5 ].Signals := [ sgWyAD, sgWel, sgWea ];
  FLines[ SecondCycleOffset + 5 ].Condition := cnLastCycle;
  FLines[ SecondCycleOffset + 5 ].NextTrue := 0;
  FLines[ SecondCycleOffset + 5 ].NextFalse := 0;
  // SOM kod 6
  FLines[ SecondCycleOffset + 6 ].Signals := [ ];
  FLines[ SecondCycleOffset + 6 ].Condition := cnNegative;
  FLines[ SecondCycleOffset + 6 ].NextTrue := 5;
  FLines[ SecondCycleOffset + 6 ].NextFalse := 6;
  FLines[ 5 ].Signals := [ sgWyAD, sgWel, sgWea ];
  FLines[ 5 ].Condition := cnLastCycle;
  FLines[ 5 ].NextTrue := 0;
  FLines[ 5 ].NextFalse := 0;
  FLines[ 6 ].Signals := [ sgWyl, sgWea ];
  FLines[ 6 ].Condition := cnLastCycle;
  FLines[ 6 ].NextTrue := 0;
  FLines[ 6 ].NextFalse := 0;
  // SOZ kod 7
  FLines[ SecondCycleOffset + 7 ].Signals := [ ];
  FLines[ SecondCycleOffset + 7 ].Condition := cnZero;
  FLines[ SecondCycleOffset + 7 ].NextTrue := 5;
  FLines[ SecondCycleOffset + 7 ].NextFalse := 6;
  FLines[ 7 ].Signals := [ sgWyAD, sgWel, sgWea ];
  FLines[ 7 ].Condition := cnLastCycle;
  FLines[ 7 ].NextTrue := 0;
  FLines[ 7 ].NextFalse := 0;
  FLines[ 8 ].Signals := [ sgWyl, sgWea ];
  FLines[ 8 ].Condition := cnLastCycle;
  FLines[ 8 ].NextTrue := 0;
  FLines[ 8 ].NextFalse := 0;
  FInstructions[ 0 ].Mnemonic := lsInstructionSTP;
  FInstructions[ 0 ].Code := 0;
  FInstructions[ 0 ].Arguments := 0;
  FInstructions[ 0 ].UsedSignals := [ sgCzyt, sgWys, sgWei, sgIl, sgStop ];
  FInstructions[ 1 ].Mnemonic := lsInstructionDOD;
  FInstructions[ 1 ].Code := 1;
  FInstructions[ 1 ].Arguments := 1;
  FInstructions[ 1 ].UsedSignals := [ sgCzyt, sgWys, sgWei, sgIl, sgWyAD, sgWea, sgWeja, sgWeak, sgWyl, sgDod ];
  FInstructions[ 2 ].Mnemonic := lsInstructionODE;
  FInstructions[ 2 ].Code := 2;
  FInstructions[ 2 ].Arguments := 1;
  FInstructions[ 2 ].UsedSignals := [ sgCzyt, sgWys, sgWei, sgIl, sgWyAD, sgWea, sgWeja, sgWeak, sgWyl, sgOde ];
  FInstructions[ 3 ].Mnemonic := lsInstructionLAD;
  FInstructions[ 3 ].Code := 3;
  FInstructions[ 3 ].Arguments := 1;
  FInstructions[ 3 ].UsedSignals := [ sgCzyt, sgWys, sgWei, sgIl, sgWyAD, sgWea, sgWyak, sgWes, sgPisz, sgWyl ];
  FInstructions[ 4 ].Mnemonic := lsInstructionPOB;
  FInstructions[ 4 ].Code := 4;
  FInstructions[ 4 ].Arguments := 1;
  FInstructions[ 4 ].UsedSignals := [ sgCzyt, sgWys, sgWei, sgIl, sgWyAD, sgWea, sgWeja, sgWeak, sgWyl, sgPrzep ];
  FInstructions[ 5 ].Mnemonic := lsInstructionSOB;
  FInstructions[ 5 ].Code := 5;
  FInstructions[ 5 ].Arguments := 1;
  FInstructions[ 5 ].UsedSignals := [ sgCzyt, sgWys, sgWei, sgIl, sgWyAD, sgWea, sgWel ];
  FInstructions[ 6 ].Mnemonic := lsInstructionSOM;
  FInstructions[ 6 ].Code := 6;
  FInstructions[ 6 ].Arguments := 1;
  FInstructions[ 6 ].UsedSignals := [ sgCzyt, sgWys, sgWei, sgIl, sgWyAD, sgWea, sgWel, sgWyl ];
  FInstructions[ 7 ].Mnemonic := lsInstructionSOZ;
  FInstructions[ 7 ].Code := 7;
  FInstructions[ 7 ].Arguments := 1;
  FInstructions[ 7 ].UsedSignals := [ sgCzyt, sgWys, sgWei, sgIl, sgWyAD, sgWea, sgWel, sgWyl ];
end;

// skompiluj rozkaz opisany podanym tekstem i dopisz go lub zamień na liście rozkazów
procedure TMicroControl.CompileInstruction(Text: TStrings);
var
  Addresses : array[ 0 .. 63 ] of integer;
  i, j : integer;
  Found : Boolean;
  FCompiler : TCompiler;
  NewCode : integer;
begin
  // całość kompilacji
  FCompiler := TCompiler.Create;
  try
    FCompiler.Compile( Text );
    if not FCompiler.Success then
      raise Exception.Create('Error in Microcontroller.TMicroControl.CompileInstruction');
    Found := false;
    NewCode := -1;
    j := ( 1 shl Computer.CodeLength ) - 1;  // ustalamy, jakie adresy są dostępne
    for i := 0 to j do
      if AnsiSameText( FCompiler.Mnemonic, FInstructions[ i ].Mnemonic ) and ( FInstructions[ i ].Code <> -1 ) then
      begin
        NewCode := FInstructions[ i ].Code;
        Found := true;
        break;
      end;
    if Found then
      DeleteInstruction( SecondCycleOffset + NewCode )
    else
    begin
      // ustalenie mnemoniki, kodu itd.
      Found := false;
      j := ( 1 shl Computer.CodeLength ) - 1;  // ustalamy, jakie adresy są dostępne
      for i := 0 to j do
        if FInstructions[ i ].Mnemonic = '' then
        begin
          Found := true;
          NewCode := i;
          break;
        end;
    end;
    if not Found then
      raise Exception.Create( lsFullInstructionList );
    FInstructions[ NewCode ].Mnemonic := AnsiUpperCase( FCompiler.Mnemonic );
    FInstructions[ NewCode ].Code := NewCode;
    FInstructions[ NewCode ].Arguments := FCompiler.Args;
    FInstructions[ NewCode ].UsedSignals := FCompiler.Architecture;
    // teraz zbudowanie tablicy asocjacyjnej dla adresów mikrorozkazów
    for i := 0 to High( Addresses ) do
      Addresses[ i ] := -1;
    Addresses[ 0 ] := 0;
    Addresses[ 1 ] := SecondCycleOffset + NewCode;
    j := 1;
    for i := 2 to FCompiler.OutputCount - 1 do
    begin
      while FLines[ j ].Condition <> cnUnused do
      begin
        Inc( j );
        if j >= SecondCycleOffset then
          raise Exception.Create( lsFullMicromemory );
      end;
      Addresses[ i ] := j;
      Inc( j );
    end;
    // i przekopiowanie adresów wg tej tablicy
    FLines[ SecondCycleOffset + NewCode ].Signals := FCompiler.Output[ 1 ].Signals;
    FLines[ SecondCycleOffset + NewCode ].Condition := FCompiler.Output[ 1 ].Condition;
    FLines[ SecondCycleOffset + NewCode ].NextTrue := Addresses[ FCompiler.Output[ 1 ].NextTrue ];
    FLines[ SecondCycleOffset + NewCode ].NextFalse := Addresses[ FCompiler.Output[ 1 ].NextFalse ];
    for i := 2 to FCompiler.OutputCount - 1 do
    begin
      FLines[ Addresses[ i ] ].Signals := FCompiler.Output[ i ].Signals;
      FLines[ Addresses[ i ] ].Condition := FCompiler.Output[ i ].Condition;
      FLines[ Addresses[ i ] ].NextTrue := Addresses[ FCompiler.Output[ i ].NextTrue ];
      FLines[ Addresses[ i ] ].NextFalse := Addresses[ FCompiler.Output[ i ].NextFalse ];
    end;
  finally
    FCompiler.Free;
  end;


  // potem odpowiednio skopiuj do TMicrocontrol.Flines
  // w zależności od mnemoniki albo dodaj albo podmień
end;

constructor TMicroControl.Create;
begin
  FBeforeExecuteCycle := nil;
  FBeforeInstruction := nil;
  FAfterInstruction := nil;
  ClearLines;
  Reset;
end;

// usuwa rozkaz z listy rozkazów
procedure TMicroControl.DeleteInstruction(ACode: integer);
procedure RecursiveDelete( LineNo : integer );
begin
  with FLines[ LineNo ] do
  begin
    Signals := [];
    Condition := cnUnused;
    if ( NextTrue > 0 ) and ( FLines[ NextTrue ].Condition <> cnUnused ) then
      RecursiveDelete( NextTrue );
    if ( NextFalse > 0 ) and ( FLines[ NextFalse ].Condition <> cnUnused ) then
      RecursiveDelete( NextFalse );
    NextTrue := 0;
    NextFalse := 0;
  end;
end;
begin
  // usuwa z FInstructions i FLines
  FInstructions[ ACode ].Mnemonic := '';
  FInstructions[ ACode ].Code := -1;
  FInstructions[ ACode ].Arguments := 0;
  RecursiveDelete( ACode );
end;

destructor TMicroControl.Destroy;
begin

  inherited;
end;

procedure TMicroControl.ExecuteInstruction;
begin
   repeat
     ExecuteNextCycle;
   until CurrentLine = 0;
end;

procedure TMicroControl.ExecuteNextCycle;
var
  SignalSet : TSignalSet;
  i : integer;
begin
  // ustal SignalSet do wykonania
  SignalSet := FLines[ CurrentLine ].Signals;
  if Assigned( FBeforeExecuteCycle ) then
    FBeforeExecuteCycle( Self, SignalSet );
  Computer.ExecuteCycle( SignalSet );
  // teraz ustal adres następnego mikrorozkazu do wykonania
  i := CurrentLine;
  case FLines[ i ].Condition of
    cnNone : CurrentLine := FLines[ i ].NextTrue;
    cnFirstCycle : CurrentLine := SecondCycleOffset + Computer.InstructionCode;
    cnNegative : if Computer.NegativeAccumulator then
                   CurrentLine := FLines[ i ].NextTrue
                 else
                   CurrentLine := FLines[ i ].NextFalse;
    cnZero : if Computer.RegisterAK = 0 then
               CurrentLine := FLines[ i ].NextTrue
             else
               CurrentLine := FLines[ i ].NextFalse;
    cnLastCycle : CurrentLine := 0;
    cnInterrupt : if Computer.RegisterRP > 0 then
                    CurrentLine := FLines[ i ].NextTrue
                  else
                    CurrentLine := FLines[ i ].NextFalse;
  else  // cnUnused
    raise Exception.Create('Exception in TMicroControl.ExecuteNextCycle');
  end;

end;

procedure TMicroControl.ExecuteProgram;
var
  InstrAddr : integer;
begin
  repeat
    InstrAddr := Computer.RegisterA;
    if Assigned( FBeforeInstruction ) then
      FBeforeInstruction( Self, InstrAddr );
    ExecuteInstruction;
    if Assigned( FAfterInstruction ) then
      FAfterInstruction( Self, InstrAddr );
  until false; // skończy się po zgłoszeniu wyjątku z zakończeniem programu rozkazem stop
end;

function TMicroControl.GetInstructions(ACode: integer): TInstruction;
begin
  Result := FInstructions[ ACode ];
end;

function TMicroControl.Mnemonic2Code(Text: string): integer;
var
  i : integer;
begin
  Result := -1; // jak nie ma rozkazu, to zwróci -1
  for i := 0 to 31 do
    if FInstructions[ i ].Mnemonic = AnsiUpperCase( Text ) then
    begin
      Result := i;
      exit;
    end;
end;

procedure TMicroControl.Reset;
begin
  Computer.Reset;
  // oraz ustaw swoje zmienne
  CurrentLine := 0;  // zaczynamy od początku rozkazu
end;

procedure TMicroControl.SetAfterInstruction(const Value: TInstructionEvent);
begin
  FAfterInstruction := Value;
end;

procedure TMicroControl.SetBeforeExecuteCycle(
  const Value: TBeforeExecuteCycleEvent);
begin
  FBeforeExecuteCycle := Value;
end;

procedure TMicroControl.SetBeforeInstruction(const Value: TInstructionEvent);
begin
  FBeforeInstruction := Value;
end;

{ TMicroProgram }

function TMicroProgram.GetLines(Address: integer): TLine;
begin

end;

initialization
  Controller := TMicroControl.Create

finalization
  Controller.Free;

end.

unit ControlUnit;

{ Jednostka centralna komputera }

interface

uses
  System.SysUtils, Classes;

type
  TSignals = ( sgStop, sgCzyt, sgWyAD, sgWyl, sgWyls, sgWyak, sgWys, sgWyx, sgWyy,
               sgWyWS, sgWyrb, sgWyG, sgWyap, sgWyrm, sgPisz, sgAS,
               sgWeja, sgNeg, sgLub, sgI, sgDod, sgOde, sgPrzep, sgMnoz,
               sgDziel, sgShr, sgShl, sgWei, sgIl, sgWel, sgWea, sgWes,
               sgIAk, sgDAK, sgWeak, sgWex, sgWey, sgIws, sgDws,
               sgWeWs, sgWerb, sgStart, sgRint, sgEni, sgWerm );

  TSignalSet = set of TSignals;

type
  TRegisterCode = ( rcL, rcA, rcI, rcAK, rcS, rcWS, rcRZ, rcRM, rcRP, rcAP,
                    rcX, rcY, rcRB, rcG );
  TRegisterType = ( rtWord, rtAddress, rtBit, rtInterrupt );

type
  TRegister = class
  private
    Code : TRegisterCode;
    RegType : TRegisterType;
    FValue: integer;
    procedure SetValue(const Value: integer);
  public
     property Value : integer read FValue write SetValue;
  end;

type
  TRegisterArray = array [ TRegisterCode ] of TRegister;

type
  TComputer = class  // tu jest cała maszyna W
  private
    InternalMagA : integer;
    InternalMagS : integer;
    InternalInputJAL : integer;
    InternalOutputJAL : integer;
    // -----------------------------
    FAddressLength: integer;
    FInterruptVector : array[ 1 .. 4 ] of integer;
    FInterruptLabel : array[ 1 .. 4 ] of string;
    FInstructionPointer : integer;
    FCodeLength: integer;
    FRAM: array [ 0 .. 2047 ] of integer;
    FRegisters : TRegisterArray;
    FOnStartSignal: TNotifyEvent;
    FOnRefresh : TNotifyEvent;
    FOutputPort: integer;
    FInputPort: integer;
    FOnInstructionPointerChange: TNotifyEvent;
    procedure ClearRAM;
    function GetWordLength: integer;
    function GetRAM(Address: integer): integer;
    procedure SetAddressLength(const Value: integer);
    procedure SetCodeLength(const Value: integer);
    procedure SetRAM(Address: integer; const Value: integer);
    function GetSelectedRegister(Index: TRegisterCode): TRegister;
    procedure SetOnStartSignal(const Value: TNotifyEvent);
    procedure SetOnRefresh(const Value: TNotifyEvent);
    function GetInstructionCode: integer;
    function GetNegativeAccumulator: Boolean;
    function GetInterruptVector(Index: integer): integer;
    procedure SetInterruptVector(Index: integer; const Value: integer);
    function GetInterruptLabel(Index: integer): string;
    procedure SetInterruptLabel(Index: integer; const Value: string);
    procedure SetInputPort(const Value: integer);
    procedure SetOutputPort(const Value: integer);
    function GetRegisterAD: integer;
    function GetInstructionPointer: integer;
    procedure SetOnInstructionPointerChange(const Value: TNotifyEvent);
  protected
    procedure ExecuteSignal( Signal : TSignals ); virtual;
  public
    // konfiguracja maszyny
    function GetRegister(const Index: Integer): integer;
    procedure SetRegister(const Index, Value: integer);
    property AddressLength : integer read FAddressLength write SetAddressLength;
    property AllRegisters[ Index : TRegisterCode ] : TRegister read GetSelectedRegister;
    property CodeLength : integer read FCodeLength write SetCodeLength;
    property InstructionCode : integer read GetInstructionCode;
    property InstructionPointer : integer read GetInstructionPointer;
    property InterruptLabel[ Index : integer ] : string read GetInterruptLabel write SetInterruptLabel;
    property InterruptVector[ Index : integer ] : integer read GetInterruptVector write SetInterruptVector;
    property InputPort : integer read FInputPort write SetInputPort;
    property OutputPort : integer read FOutputPort write SetOutputPort;
    property NegativeAccumulator : Boolean read GetNegativeAccumulator;
    property RAM[ Address : integer ] : integer read GetRAM write SetRAM;
    property RegisterA : integer index rcA read GetRegister write SetRegister;
    property RegisterL : integer index rcL read GetRegister write SetRegister;
    property RegisterI : integer index rcI read GetRegister write SetRegister;
    property RegisterAK : integer index rcAK read GetRegister write SetRegister;
    property RegisterS : integer index rcS read GetRegister write SetRegister;
    property RegisterWS : integer index rcWS read GetRegister write SetRegister;
    property RegisterRZ : integer index rcRZ read GetRegister write SetRegister;
    property RegisterRM : integer index rcRM read GetRegister write SetRegister;
    property RegisterRP : integer index rcRP read GetRegister write SetRegister;
    property RegisterAP : integer index rcAP read GetRegister write SetRegister;
    property RegisterX : integer index rcX read GetRegister write SetRegister;
    property RegisterY : integer index rcY read GetRegister write SetRegister;
    property RegisterRB : integer index rcRB read GetRegister write SetRegister;
    property RegisterG : integer index rcG read GetRegister write SetRegister;
    property RegisterAD : integer read GetRegisterAD;
    property WordLength : integer read GetWordLength;
    // metody
    constructor Create;
    destructor Destroy; override;
    procedure ExecuteCycle( SignalSet : TSignalSet );
    procedure Reset;  // ustawia tylko rejestry L i A na 0
    procedure Refresh;
    // zdarzenia
    property OnStartSignal : TNotifyEvent read FOnStartSignal write SetOnStartSignal;
    property OnRefresh : TNotifyEvent read FOnRefresh write SetOnRefresh;
    property OnInstructionPointerChange : TNotifyEvent read FOnInstructionPointerChange write SetOnInstructionPointerChange;
  end;

type
  EInvalidAddressLength = class( Exception );
  EInvalidCodeLength = class( Exception );
  EProgramFinished = class( Exception );  // wykonano sygnał stop
  EDuplicateWrite = class( Exception );   // próba zapisu 2 wartości na magistralę
  EBusIsEmpty = class( Exception );       // próba odczytu z pustej magistrali
  EReadWrite = class( Exception );        // próba jednoczesnego odczytu i zapisu w pamięci

var
  Computer : TComputer;

function Signal2Name( s : TSignals ) : string;
function RegisterCode2Name( r : TRegisterCode ) : string;

implementation

uses
  Languages;

const
  emptyBus = -1;

const
  MaxWordValue = 65535;  // największa wartość słowa w maszynie W


function Signal2Name( s : TSignals ) : string;
begin
  case s of
    sgStop: Result := lsSignalNameStop;
    sgCzyt: Result := lsSignalNameCzyt;
    sgWyAD: Result := lsSignalNameWyAD;
    sgWyl: Result := lsSignalNameWyl;
    sgWyls: Result := lsSignalNameWyls;
    sgWyak: Result := lsSignalNameWyak;
    sgWys: Result := lsSignalNameWys;
    sgWyx: Result := lsSignalNameWyx;
    sgWyy: Result := lsSignalNameWyy;
    sgWyWS: Result := lsSignalNameWyWS;
    sgWyrb: Result := lsSignalNameWyrb;
    sgWyG: Result := lsSignalNameWyG;
    sgWyap: Result := lsSignalNameWyap;
    sgWyrm: Result := lsSignalNameWyrm;
    sgPisz: Result := lsSignalNamePisz;
    sgAS: Result := lsSignalNameAS;
    sgWeja: Result := lsSignalNameWeja;
    sgNeg: Result := lsSignalNameNeg;
    sgLub: Result := lsSignalNameLub;
    sgI: Result := lsSignalNameI;
    sgDod: Result := lsSignalNameDod;
    sgOde: Result := lsSignalNameOde;
    sgPrzep: Result := lsSignalNamePrzep;
    sgMnoz: Result := lsSignalNameMnoz;
    sgDziel: Result := lsSignalNameDziel;
    sgShr: Result := lsSignalNameShr;
    sgShl: Result := lsSignalNameShl;
    sgWei: Result := lsSignalNameWei;
    sgIl: Result := lsSignalNameIl;
    sgWel: Result := lsSignalNameWel;
    sgWea: Result := lsSignalNameWea;
    sgWes: Result := lsSignalNameWes;
    sgIAk: Result := lsSignalNameIAk;
    sgDAK: Result := lsSignalNameDAK;
    sgWeak: Result := lsSignalNameWeak;
    sgWex: Result := lsSignalNameWex;
    sgWey: Result := lsSignalNameWey;
    sgIws: Result := lsSignalNameIws;
    sgDws: Result := lsSignalNameDws;
    sgWeWs: Result := lsSignalNameWeWs;
    sgWerb: Result := lsSignalNameWerb;
    sgStart: Result := lsSignalNameStart;
    sgRint: Result := lsSignalNameRint;
    sgEni: Result := lsSignalNameEni;
    sgWerm: Result := lsSignalNameWerm;
  end;
end;

function RegisterCode2Name( r : TRegisterCode ) : string;
begin
  case r of
    rcL: Result := lsRegisterNameL;
    rcA: Result := lsRegisterNameA;
    rcI: Result := lsRegisterNameI;
    rcAK: Result := lsRegisterNameAK;
    rcS: Result := lsRegisterNameS;
    rcWS: Result := lsRegisterNameWS;
    rcRZ: Result := lsRegisterNameRZ;
    rcRM: Result := lsRegisterNameRM;
    rcRP: Result := lsRegisterNameRP;
    rcAP: Result := lsRegisterNameAP;
    rcX: Result := lsRegisterNameX;
    rcY: Result := lsRegisterNameY;
    rcRB: Result := lsRegisterNameRB;
    rcG: Result := lsRegisterNameG;
  end;
end;

{ TComputer }

procedure TComputer.ClearRAM;
var
  i : integer;
begin
  for i := 0 to 2047 do
    FRAM[ i ] := 0;
end;

constructor TComputer.Create;
var
  rc : TRegisterCode;
begin
  inherited;
  FOnStartSignal := nil;
  FOnInstructionPointerChange := nil;
  // ustawienie wartości domyślnych
  FAddressLength := 5;
  FCodeLength := 3;
  // utwórz rejestry
  for rc := Low( TRegisterCode) to High( TRegisterCode ) do
  begin
    FRegisters[ rc ] := TRegister.Create;
    FRegisters[ rc ].Code := rc;
    FRegisters[ rc ].FValue := 0;
    FRegisters[ rc ].RegType := rtWord;
  end;
  FRegisters[ rcA ].RegType := rtAddress;
  FRegisters[ rcL ].RegType := rtAddress;
  FRegisters[ rcWS ].RegType := rtAddress;
  FRegisters[ rcAP ].RegType := rtAddress;
  FRegisters[ rcRZ ].RegType := rtInterrupt;
  FRegisters[ rcRM ].RegType := rtInterrupt;
  FRegisters[ rcRP ].RegType := rtInterrupt;
  FRegisters[ rcG ].RegType := rtBit;
  FInterruptVector[ 1 ] := 1;
  FInterruptVector[ 2 ] := 2;
  FInterruptVector[ 3 ] := 3;
  FInterruptVector[ 4 ] := 4;
  FInterruptLabel[ 1 ] := 'AP1';
  FInterruptLabel[ 2 ] := 'AP2';
  FInterruptLabel[ 3 ] := 'AP3';
  FInterruptLabel[ 4 ] := 'AP4';
  FInputPort := 1;
  FOutputPort := 2;
  // wyczyść pamięć RAM
  ClearRAM;
end;

destructor TComputer.Destroy;
begin
  inherited;
end;

procedure TComputer.ExecuteCycle(SignalSet: TSignalSet);
var
  sg : TSignals;
begin
  if sgCzyt in SignalSet then
    if sgPisz in SignalSet then
      raise EReadWrite.Create( lsReadWriteError );
  if sgCzyt in SignalSet then
    if sgWes in SignalSet then
      raise EDuplicateWrite.Create( lsDuplicateWriteRegError );
  if sgWel in SignalSet then
    if sgIl in SignalSet then
      raise EDuplicateWrite.Create( lsDuplicateWriteRegError );
  if sgWews in SignalSet then
    if sgIws in SignalSet then
      raise EDuplicateWrite.Create( lsDuplicateWriteRegError );
  if sgWews in SignalSet then
    if sgDws in SignalSet then
      raise EDuplicateWrite.Create( lsDuplicateWriteRegError );
  if sgDws in SignalSet then
    if sgIws in SignalSet then
      raise EDuplicateWrite.Create( lsDuplicateWriteRegError );
  if sgWeak in SignalSet then
    if sgIak in SignalSet then
      raise EDuplicateWrite.Create( lsDuplicateWriteRegError );
  if sgWeak in SignalSet then
    if sgDak in SignalSet then
      raise EDuplicateWrite.Create( lsDuplicateWriteRegError );
  if sgDak in SignalSet then
    if sgIak in SignalSet then
      raise EDuplicateWrite.Create( lsDuplicateWriteRegError );
  // zainicjuj magistrale
  InternalMagA := emptyBus;
  InternalMagS := emptyBus;
  InternalInputJAL := emptyBus;
  InternalOutputJAL := emptyBus;
  if SignalSet = [ sgCzyt, sgWys, sgWei, sgIl ] then // pierwszy takt
  begin
    FInstructionPointer := RegisterL;
    if Assigned( FOnInstructionPointerChange ) then
      FOnInstructionPointerChange( Self );
  end;
  // wykonaj akcję przypisaną do poszczególnych sygnałów
  for sg := Low( TSignals ) to High( TSignals ) do
    if sg in SignalSet then  // wykonaj ten sygnał
      ExecuteSignal( sg );
end;

procedure TComputer.ExecuteSignal(Signal: TSignals);
procedure AssignBus( var Bus : integer; Value : integer; Size : integer );
var
  Mask : integer;
begin
  if Bus <> emptyBus then
    raise EDuplicateWrite.Create( lsDuplicateWriteBusError );
  Mask := ( 1 shl Size ) - 1;
  Bus := Value and Mask;
end; // AssignBus
function BusValue( Bus : integer ) : integer;
begin
  if Bus = emptyBus then
    raise EBusIsEmpty.Create( lsBusEmptyError );
  Result := Bus;
end; // BusValue
function HighestInterrupt( Value : integer ) : integer;
begin
  if Value > 7 then
    Result := 8
  else if Value > 3 then
    Result := 4
  else if Value > 1 then
    Result := 2
  else if Value > 0 then
    Result := 1
  else
    Result := 0;
end; // HighestInterrupt
begin  // TComputer.ExecuteSignal
  case Signal of
    sgStop: raise EProgramFinished.Create( lsProgramFinished );
    sgCzyt: RegisterS := RAM[ RegisterA ];
    sgWyAD: AssignBus( InternalmagA, RegisterI, AddressLength );
    sgWyl:  AssignBus( InternalmagA, RegisterL, AddressLength );
    sgWyls: AssignBus( InternalmagS, RegisterL, WordLength );
    sgWyak: AssignBus( InternalmagS, RegisterAK, WordLength );
    sgWys:  AssignBus( InternalmagS, RegisterS, WordLength );
    sgWyx:  AssignBus( InternalmagS, RegisterX, WordLength );
    sgWyy:  AssignBus( InternalmagS, RegisterY, WordLength );
    sgWyWS: AssignBus( InternalmagA, RegisterWS, AddressLength );
    sgWyrb: AssignBus( InternalmagS, RegisterRB, WordLength );
    sgWyG:  AssignBus( InternalmagS, RegisterG, 1 );
    sgWyap: AssignBus( InternalmagA, RegisterAP, AddressLength );
    sgWyrm: AssignBus( InternalmagA, RegisterRM, 4 );
    sgPisz: RAM[ RegisterA ] := RegisterS;
    sgAS:   if InternalmagA = emptyBus then AssignBus( InternalmagA, InternalmagS, AddressLength )
            else AssignBus( InternalmagS, InternalmagA, AddressLength );
    sgWeja: AssignBus( InternalinputJAL, InternalmagS, WordLength );
    sgNeg:  AssignBus( InternaloutputJAL, - InternalinputJAL, WordLength );
    sgLub:  AssignBus( InternaloutputJAL, RegisterAK or InternalinputJAL, WordLength );
    sgI:    AssignBus( InternaloutputJAL, RegisterAK and InternalinputJAL, WordLength );
    sgDod:  AssignBus( InternaloutputJAL, RegisterAK + InternalinputJAL, WordLength );
    sgOde:  AssignBus( InternaloutputJAL, RegisterAK - InternalinputJAL, WordLength );
    sgPrzep: AssignBus( InternaloutputJAL, InternalinputJAL, WordLength );
    sgMnoz: AssignBus( InternaloutputJAL, RegisterAK * InternalinputJAL, WordLength );
    sgDziel: AssignBus( InternaloutputJAL, RegisterAK div InternalinputJAL, WordLength );
    sgShr:  AssignBus( InternaloutputJAL, RegisterAK shr InternalinputJAL, WordLength );
    sgShl:  AssignBus( InternaloutputJAL, RegisterAK shl InternalinputJAL, WordLength );
    sgWei:  RegisterI := BusValue( InternalmagS );
    sgIl:   RegisterL := RegisterL + 1;
    sgWel:  RegisterL := BusValue( InternalmagA );
    sgWea:  RegisterA := BusValue( InternalmagA );
    sgWes:  RegisterS := BusValue( InternalmagS );
    sgIAk:  RegisterAK := RegisterAk + 1;
    sgDAK:  RegisterAK := RegisterAk - 1;
    sgWeak: RegisterAK := BusValue( InternaloutputJAL );
    sgWex:  RegisterX := BusValue( InternalmagS );
    sgWey:  RegisterY := BusValue( InternalmagS );
    sgIws:  RegisterWS := RegisterWS + 1;
    sgDws:  RegisterWS := RegisterWS - 1;
    sgWeWs: RegisterWS := BusValue( InternalmagA );
    sgWerb: RegisterRB := BusValue( InternalmagS );
    sgStart: begin
              RegisterG := 0;
              if Assigned( FOnStartSignal ) then
                FOnStartSignal( Self )
              else
                raise Exception.Create( 'Exception in ControlUnit.TComputer.ExecuteSignal - handle OnStartSignal event' )
             end;
    sgRint: begin
              RegisterRZ := RegisterRZ and not RegisterRP;
              RegisterRP := 0;
              RegisterAP := 0
            end;
    sgEni:  begin
              RegisterRP := HighestInterrupt( RegisterRZ and not RegisterRM );
              case RegisterRP of
              1 : RegisterAP := InterruptVector[ 4 ];
              2 : RegisterAP := InterruptVector[ 3 ];
              4 : RegisterAP := InterruptVector[ 2 ];
              8 : RegisterAP := InterruptVector[ 1 ];
              else
                RegisterAP := 0;
              end;
            end;
    sgWerm: RegisterRM := BusValue( InternalmagA );
  end;
end;

function TComputer.GetInstructionCode: integer;
begin
  Result := RegisterI shr AddressLength;
end;

// funkcja zwraca adres aktualnego wykonywanego rozkazu
function TComputer.GetInstructionPointer: integer;
begin
  Result := FInstructionPointer;
end;

function TComputer.GetInterruptLabel(Index: integer): string;
begin
  if Index in [ 1, 2, 3, 4 ] then
    Result := FInterruptLabel[ Index ]
  else
    raise Exception.Create('Error in ControlUnit.TComputer.GetInterruptLabel');
end;

function TComputer.GetInterruptVector(Index: integer): integer;
begin
  if Index in [ 1, 2, 3, 4 ] then
    Result := FInterruptVector[ Index ]
  else
    raise Exception.Create('Error in ControlUnit.TComputer.GetInterruptVector');
end;

function TComputer.GetNegativeAccumulator: Boolean;
begin
  Result := ( RegisterAK shr ( WordLength - 1 ) ) > 0;
end;

function TComputer.GetRAM(Address: integer): integer;
var
  Mask : integer;
begin
  Mask := ( 1 shl AddressLength ) - 1;
  Result := FRAM[ Address and Mask ];
end;

function TComputer.GetRegister(const Index: Integer): integer;
begin
  Result := FRegisters[ TRegisterCode( Index ) ].Value;
end;

function TComputer.GetRegisterAD: integer;
var
  Mask : integer;
begin
  Mask := ( 1 shl AddressLength ) - 1;
  Result := RegisterI and Mask;
end;

function TComputer.GetSelectedRegister(Index: TRegisterCode): TRegister;
begin
  Result := FRegisters[ Index ];
end;

function TComputer.GetWordLength: integer;
begin
  Result := AddressLength + CodeLength;
end;

procedure TComputer.Refresh;
begin
  if Assigned( FOnRefresh ) then
    FOnRefresh( Self );
end;

procedure TComputer.Reset;
begin
  RegisterA := 0;
  RegisterL := 0;
  FInstructionPointer := 0;
end;

procedure TComputer.SetAddressLength(const Value: integer);
begin
  if ( Value < 5 ) or ( Value > 11 ) then
    raise EInvalidAddressLength.Create( 'EInvalidAddressLength exception in Control.SetAddressLength');
  if FAddressLength = Value then
    exit;
  FAddressLength := Value;
end;

procedure TComputer.SetCodeLength(const Value: integer);
begin
  if ( Value < 3 ) or ( Value > 5 ) then
    raise EInvalidCodeLength.Create('EInvalidCodeLength exception in Control.SetCodeLength');
  if FCodeLength = Value then
    exit;
  FCodeLength := Value;
end;

procedure TComputer.SetInputPort(const Value: integer);
begin
  if FInputPort = Value then
    exit;
  FInputPort := Value;
end;

procedure TComputer.SetInterruptLabel(Index: integer; const Value: string);
begin
  if Index in [ 1, 2, 3, 4 ] then
    FInterruptLabel[ Index ] := AnsiUpperCase( Value )
  else
    raise Exception.Create('Error in ControlUnit.TComputer.SetInterruptLabel');
end;

procedure TComputer.SetInterruptVector(Index: integer; const Value: integer);
begin
  if Index in [ 1, 2, 3, 4 ] then
    FInterruptVector[ Index ] := Value and (( 1 shl AddressLength ) - 1 )
  else
    raise Exception.Create('Error in ControlUnit.TComputer.SetInterruptVector');
end;

procedure TComputer.SetOnInstructionPointerChange(const Value: TNotifyEvent);
begin
  FOnInstructionPointerChange := Value;
end;

procedure TComputer.SetOnRefresh(const Value: TNotifyEvent);
begin
  FOnRefresh := Value;
end;

procedure TComputer.SetOnStartSignal(const Value: TNotifyEvent);
begin
  FOnStartSignal := Value;
end;

procedure TComputer.SetOutputPort(const Value: integer);
begin
  if FOutputPort = Value then
    exit;
  FOutputPort := Value;
end;

procedure TComputer.SetRAM(Address: integer; const Value: integer);
var
  Mask : integer;
  WordMask : integer;
begin
  WordMask := ( 1 shl WordLength ) - 1;
  Mask := ( 1 shl AddressLength ) - 1;
  FRAM[ Address and Mask ] := Value and WordMask;
end;

procedure TComputer.SetRegister(const Index, Value: integer);
begin
  FRegisters[ TRegisterCode( Index ) ].Value := Value;
end;

{ TRegister }

procedure TRegister.SetValue(const Value: integer);
var
  Mask : integer;
  Size : integer;
begin
  if FValue = Value then
    exit;
  Size := 1;  // aby uniknąć ostrzeżenia
  case RegType of
    rtWord : Size := Computer.WordLength;
    rtAddress : Size := Computer.AddressLength;
    rtBit : Size := 1;
    rtInterrupt : Size := 4;
  end;
  Mask := ( 1 shl Size ) - 1;
  FValue := Value and Mask;
end;

initialization
  Computer := TComputer.Create;

finalization
  Computer.Free;

end.

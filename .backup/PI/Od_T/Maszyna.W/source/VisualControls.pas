unit VisualControls;

interface

uses
  System.Messaging, System.Classes, Vcl.Controls, ControlUnit;

type
  TNumeralSystem = ( nsUnsigned, nsSiged, nsBinary, nsHexadecimal );

type
  TRegister = class( TGraphicControl )
  private
    FNumeralSystem: TNumeralSystem;
    FRegisterCode: TRegisterCode;
    FLength: integer;
    procedure SetRegisterValue(const Value: integer);
    procedure SetNumeralSystem(const Value: TNumeralSystem);
    procedure SetRegisterCode(const Value: TRegisterCode);
    function GetRegisterName: string;
    function GetRegisterValue: integer;
    function GetRegisterAsSigned: integer;
    procedure SetLength(const Value: integer);
  protected
    function IntToBin( Value : integer ) : string;
    procedure Paint; override;
  public
    constructor Create( AOwner : TComponent ); override;
    property RegisterName : string read GetRegisterName;
    property RegisterValue : integer read GetRegisterValue write SetRegisterValue;
    property RegisterValueAsSigned : integer read GetRegisterAsSigned;
  published
    property Length : integer read FLength write SetLength;
    property NumeralSystem : TNumeralSystem read FNumeralSystem write SetNumeralSystem;
    property OnClick;
    property OnDblClick;
    property PopupMenu;
    property RegisterCode : TRegisterCode read FRegisterCode write SetRegisterCode;
    property Visible;
  end;

type
  TBus = class( TGraphicControl )
  private
    FActive: Boolean;
    procedure SetActive(const Value: Boolean);
  protected
    procedure Paint; override;
  public
    constructor Create( AOwner : TComponent ); override;
  published
    property Active : Boolean read FActive write SetActive;
    property PopupMenu;
    property Visible;
  end;

type
  TSignalKind = ( skLevel, skImpulse );
  TSignalOrientation = ( soLeft, soRight, soUp, soDown );

type
  TControlSignal = class( TGraphicControl )
  private
    FMouseInside : Boolean;
    FKind: TSignalKind;
    FSignalName: string;
    FActive: Boolean;
    FOrientation: TSignalOrientation;
    FSignal: TSignals;
    procedure SetActive(const Value: Boolean);
    procedure SetKind(const Value: TSignalKind);
    procedure SetOrientation(const Value: TSignalOrientation);
    procedure SetSignal(const Value: TSignals);
    procedure CMMouseEnter(var Msg: TMessage); message CM_MOUSEENTER;
    procedure CMMouseLeave(var Msg: TMessage); message CM_MOUSELEAVE;
  protected
    procedure Paint; override;
  public
    constructor Create( AOwner : TComponent ); override;
    property SignalName : string read FSignalName;
  published
    property Active : Boolean read FActive write SetActive;
    property Kind : TSignalKind read FKind write SetKind;
    property OnClick;
    property Orientation : TSignalOrientation read FOrientation write SetOrientation;
    property PopupMenu;
    property Signal : TSignals read FSignal write SetSignal;
    property Visible;
  end;

procedure Register;

implementation

uses
  System.Types, System.SysUtils, System.UITypes, Vcl.Graphics, Languages;

procedure Register;
begin
  RegisterComponents( 'Extended', [ TRegister, TBus, TControlSignal ] );
end;

{ TRegister }

constructor TRegister.Create(AOwner: TComponent);
begin
  inherited;
  FRegisterCode := rcL;
  FNumeralSystem := nsUnsigned;
end;

function TRegister.GetRegisterAsSigned: integer;
var
  V : integer;
begin
  Result := RegisterValue;
  V := 1 shl ( Length - 1 );
  if ( Result and V ) > 0 then  // ujemna
  begin
    V := -1 shl Length;
    Result := Result or V;
  end;
end;

function TRegister.GetRegisterName: string;
begin
  Result := RegisterCode2Name( FRegisterCode );
end;

function TRegister.GetRegisterValue: integer;
begin
  Result := Computer.GetRegister( Ord( RegisterCode ) ) and ( ( 1 shl Length ) - 1 );
end;

function TRegister.IntToBin(Value: integer): string;
const
  Nibbles : array[ 0 .. 15 ] of string = ( '0000', '0001', '0010', '0011',
                                           '0100', '0101', '0110', '0111',
                                           '1000', '1001', '1010', '1011',
                                           '1100', '1101', '1110', '1111' );
var
  i, Nibble : integer;
begin
  Result := '';
  for i := 1 to 4 do
  begin
    Nibble := Value and 15;
    Value := Value shr 4;
    Result := Nibbles[ Nibble ] + Result;
  end;
  Result := Copy( Result, 16 - Length + 1, Length );
end;

procedure TRegister.Paint;
var
  R : TRect;
  T : string;
begin
  inherited;
  case NumeralSystem of
    nsUnsigned: T := IntToStr( RegisterValue );
    nsSiged: T := IntToStr( RegisterValueAsSigned );
    nsBinary: T := '%' + IntToBin( RegisterValue );
    nsHexadecimal: T := '$' + IntToHex( RegisterValue, ( Length + 3 ) div 4 );
  end;
  T := RegisterName + ': ' + T;
  R := Rect( 1, 1, Width - 1, Height - 1 );
  Canvas.Rectangle( 0, 0, Width, Height );
  Canvas.TextRect( R, T, [ tfCenter, tfVerticalCenter, tfSingleLine, tfEndEllipsis ] );
end;

procedure TRegister.SetLength(const Value: integer);
begin
  if FLength = Value then
    exit;
  FLength := Value;
  Invalidate;
end;

procedure TRegister.SetNumeralSystem(const Value: TNumeralSystem);
begin
  if FNumeralSystem = Value then
    exit;
  FNumeralSystem := Value;
  Invalidate;
end;

procedure TRegister.SetRegisterCode(const Value: TRegisterCode);
begin
  if FRegisterCode = Value then
    exit;
  FRegisterCode := Value;
  Invalidate;
end;

procedure TRegister.SetRegisterValue(const Value: integer);
begin
  if RegisterValue = Value then
    exit;
  //FRegisterValue := Value;
  Computer.SetRegister( Ord( RegisterCode ), Value );
  Invalidate;
end;

{ TBus }

constructor TBus.Create(AOwner: TComponent);
begin
  inherited;
  FActive := false;
end;

procedure TBus.Paint;
begin
  inherited;
  if FActive then
    Canvas.Brush.Color := clRed
  else
    Canvas.Brush.Color := clBtnFace;
  Canvas.Rectangle( 0, 0, Width, Height );
end;

procedure TBus.SetActive(const Value: Boolean);
begin
  if FActive = Value then
    exit;
  FActive := Value;
  Invalidate;
end;

{ TControlSignal }

procedure TControlSignal.CMMouseEnter(var Msg: TMessage);
begin
  {
  if csDesigning in ComponentState then
    exit;
  FMouseInside := true;
  Invalidate;
  }
end;

procedure TControlSignal.CMMouseLeave(var Msg: TMessage);
begin
  {
  if csDesigning in ComponentState then
    exit;
  FMouseInside := false;
  Invalidate;
  }
end;

constructor TControlSignal.Create(AOwner: TComponent);
begin
  inherited;
  FMouseInside := false;
  FKind := skLevel;
  FSignal := sgStop;
  FSignalName := Signal2Name( FSignal );
  FActive := false;
  FOrientation := soLeft;
end;

procedure TControlSignal.Paint;
var
  x0, y0 : integer;
  R : TRect;
  T : string;
begin
  inherited;
  T := SignalName;
  if FOrientation = soRight then
    T := T + ' ';
  Canvas.Brush.Color := clBtnFace;
  Canvas.Pen.Width := 1;
  Canvas.Font.Style := [ ];
  if FActive then
  begin
    Canvas.Pen.Color := clRed;
    Canvas.Pen.Width := 2;
    Canvas.Font.Style := [ fsBold ];
  end
  else
  begin
    Canvas.Pen.Color := clBlack;
    Canvas.Pen.Width := 1;
    Canvas.Font.Style := [];
  end;
  Canvas.Font.Color := Canvas.Pen.Color;
  if FMouseInside then
    Canvas.Font.Style := Canvas.Font.Style + [ fsItalic ];
  if FKind = skImpulse then
    Canvas.Font.Style := Canvas.Font.Style + [ fsUnderline ];
  // sygnał poziomowy skierowany w lewo
  // x0 := ( Width - 1 ) shr 1;
  if Orientation = soLeft then
  begin
    T := ' ' + T;
    x0 := Width - Canvas.TextWidth( T );
    y0 := ( Height - 1 ) shr 1;
    R := Rect( x0, 0, Width, Height );
    Canvas.TextRect( R, T, [ tfLeft, tfVerticalCenter, tfSingleLine, tfEndEllipsis ] );
    Canvas.MoveTo( x0, y0 );
    Canvas.LineTo( 0, y0 );
    // Canvas.Pen.Width := 1;
    Canvas.Brush.Color := Canvas.Pen.Color;
    if Kind = skImpulse then
      Canvas.Polygon( [ Point( 0, y0 ), Point( 10, y0 - 4 ),Point( 10, y0 + 4 ) ] )
    else
    begin
      Canvas.MoveTo( 10, y0 - 7 );
      Canvas.LineTo( 10, y0 + 7 );
    end;
  end
  else if Orientation = soRight then
  begin
    x0 := Canvas.TextWidth( T );
    y0 := ( Height - 1 ) shr 1;
    R := Rect( 0, 0, x0, Height );
    Canvas.TextRect( R, T, [ tfLeft, tfVerticalCenter, tfSingleLine, tfEndEllipsis ] );
    Canvas.MoveTo( x0, y0 );
    Canvas.LineTo( Width, y0 );
    // Canvas.Pen.Width := 1;
    Canvas.Brush.Color := Canvas.Pen.Color;
    if Kind = skImpulse then
      Canvas.Polygon( [ Point( Width, y0 ), Point( Width - 10, y0 - 4 ), Point( Width - 10, y0 + 4 ) ] )
    else
    begin
      Canvas.MoveTo( Width - 10, y0 - 7 );
      Canvas.LineTo( Width - 10, y0 + 7 );
    end;
  end
  else if Orientation = soUp then
  begin
    x0 := ( Width - 1 ) shr 1;
    y0 := Height - Canvas.TextHeight( T );
    R := Rect( 0, y0, Width, Height );
    Canvas.TextRect( R, T, [ tfCenter, tfBottom, tfSingleLine, tfEndEllipsis ] );
    Canvas.MoveTo( x0, y0 );
    Canvas.LineTo( x0, 0 );
    // Canvas.Pen.Width := 1;
    Canvas.Brush.Color := Canvas.Pen.Color;
    if Kind = skImpulse then
      Canvas.Polygon( [ Point( x0, 0 ), Point( x0 - 4, 10 ), Point( x0 + 4, 10 ) ] )
    else
    begin
      Canvas.MoveTo( x0 - 7, 10 );
      Canvas.LineTo( x0 + 7, 10 );
    end;
  end
  else // Orientation = soDown
  begin
    x0 := ( Width - 1 ) shr 1;
    y0 := Canvas.TextHeight( T );
    R := Rect( 0, 0, Width, y0 );
    Canvas.TextRect( R, T, [ tfCenter, tfBottom, tfSingleLine, tfEndEllipsis ] );
    Canvas.MoveTo( x0, y0 );
    Canvas.LineTo( x0, Height );
    // Canvas.Pen.Width := 1;
    Canvas.Brush.Color := Canvas.Pen.Color;
    if Kind = skImpulse then
      Canvas.Polygon( [ Point( x0, Height ), Point( x0 - 4, Height - 10 ), Point( x0 + 4, Height - 10 ) ] )
    else
    begin
      Canvas.MoveTo( x0 - 7, Height - 10 );
      Canvas.LineTo( x0 + 7, Height - 10 );
    end;
  end;


end;

procedure TControlSignal.SetActive(const Value: Boolean);
begin
  if FActive = Value then
    exit;
  FActive := Value;
  Invalidate;
end;

procedure TControlSignal.SetKind(const Value: TSignalKind);
begin
  if FKind = Value then
    exit;
  FKind := Value;
  Invalidate;
end;

procedure TControlSignal.SetOrientation(const Value: TSignalOrientation);
begin
  if FOrientation = Value then
    exit;
  FOrientation := Value;
  Invalidate;
end;

procedure TControlSignal.SetSignal(const Value: TSignals);
begin
  if FSignal = Value then
    exit;
  FSignal := Value;
  FSignalName := Signal2Name( Value );
  Invalidate;
end;

end.

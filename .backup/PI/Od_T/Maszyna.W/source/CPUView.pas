unit CPUView;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, BaseUnit, Vcl.StdCtrls, Vcl.Menus,
  Vcl.ExtCtrls, Vcl.CheckLst, ControlUnit, VisualControls, Vcl.Buttons;

type
  TCPUForm = class(TBaseForm)
    RegisterA: TRegister;
    PopupMenuRegister: TPopupMenu;
    MenuItemSetRegisterValue: TMenuItem;
    MenuItemShowRegisterAs: TMenuItem;
    MenuItemClearRegister: TMenuItem;
    MenuItemShowAsSigned: TMenuItem;
    MenuItemShowAsUnsigned: TMenuItem;
    MenuItemShowAsBinary: TMenuItem;
    MenuItemShowAsHexadecimal: TMenuItem;
    ListBoxPaO: TListBox;
    RegisterS: TRegister;
    RegisterL: TRegister;
    RegisterAK: TRegister;
    RegisterI: TRegister;
    BusS: TBus;
    BusA: TBus;
    BusAS: TBus;
    RegisterRZ: TRegister;
    RegisterRM: TRegister;
    RegisterRP: TRegister;
    RegisterAP: TRegister;
    RegisterX: TRegister;
    RegisterY: TRegister;
    RegisterRB: TRegister;
    RegisterG: TRegister;
    RegisterWS: TRegister;
    CheckBoxManualControl: TCheckBox;
    SpeedButton1: TSpeedButton;
    SpeedButton2: TSpeedButton;
    SpeedButton3: TSpeedButton;
    SpeedButton4: TSpeedButton;
    ControlSignalStop: TControlSignal;
    ControlSignalWyl: TControlSignal;
    ControlSignalWel: TControlSignal;
    ControlSignalIl: TControlSignal;
    ControlSignalWyls: TControlSignal;
    ControlSignalWyad: TControlSignal;
    ControlSignalWei: TControlSignal;
    ControlSignalWes: TControlSignal;
    ControlSignalWeja: TControlSignal;
    ControlSignalWys: TControlSignal;
    ControlSignalWea: TControlSignal;
    ControlSignalWews: TControlSignal;
    ControlSignalWyws: TControlSignal;
    ControlSignalCzyt: TControlSignal;
    ControlSignalPisz: TControlSignal;
    ControlSignalWyap: TControlSignal;
    ControlSignalWyrm: TControlSignal;
    ControlSignalWerm: TControlSignal;
    ControlSignalEni: TControlSignal;
    ControlSignalRint: TControlSignal;
    ControlSignalWyx: TControlSignal;
    ControlSignalWex: TControlSignal;
    ControlSignalWey: TControlSignal;
    ControlSignalWyy: TControlSignal;
    ControlSignalWeRB: TControlSignal;
    ControlSignalWyRB: TControlSignal;
    ControlSignalStart: TControlSignal;
    ControlSignalWyG: TControlSignal;
    ControlSignalDod: TControlSignal;
    ControlSignalOde: TControlSignal;
    ControlSignalPrzep: TControlSignal;
    ControlSignalWeak: TControlSignal;
    ControlSignalWyak: TControlSignal;
    ControlSignalIws: TControlSignal;
    ControlSignalDws: TControlSignal;
    ControlSignalMno: TControlSignal;
    ControlSignalIAk: TControlSignal;
    ControlSignalDziel: TControlSignal;
    ControlSignalDak: TControlSignal;
    ControlSignalNeg: TControlSignal;
    ControlSignalLub: TControlSignal;
    ControlSignalI: TControlSignal;
    ControlSignalShl: TControlSignal;
    ControlSignalShr: TControlSignal;
    ControlSignalAS: TControlSignal;
    procedure FormCreate(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    //procedure ButtonMemoryWriteClick(Sender: TObject);
    //procedure ButtonMemoryReadClick(Sender: TObject);
    //procedure ButtonRegisterWriteClick(Sender: TObject);
    //procedure ButtonRegisterReadClick(Sender: TObject);
    procedure FormPaint(Sender: TObject);
    procedure ListBoxPaODrawItem(Control: TWinControl; Index: Integer;
      Rect: TRect; State: TOwnerDrawState);
    procedure ListBoxPaODblClick(Sender: TObject);
    procedure ListBoxPaOKeyPress(Sender: TObject; var Key: Char);
    procedure RegisterDblClick(Sender: TObject);
    procedure MenuItemSetRegisterValueClick(Sender: TObject);
    procedure PopupMenuRegisterPopup(Sender: TObject);
    procedure MenuItemShowAsSignedClick(Sender: TObject);
    procedure MenuItemShowAsUnsignedClick(Sender: TObject);
    procedure MenuItemShowAsBinaryClick(Sender: TObject);
    procedure MenuItemShowAsHexadecimalClick(Sender: TObject);
    procedure MenuItemClearRegisterClick(Sender: TObject);
    procedure SpeedButtonClick(Sender: TObject);
    procedure ControlSignalClick(Sender: TObject);
    procedure CheckBoxManualControlClick(Sender: TObject);
  private
    CurrentRegister : VisualControls.TRegister;
    procedure ClearSignals;
    procedure RefreshComputerView( Sender : TObject );
    procedure SignalsHighlighter( Sender : TObject; SignalSet : TSignalSet );
  public
    procedure ChangeRegisterValue( ARegister : VisualControls.TRegister );
    procedure ShowCPUDialog;
    procedure ShowActiveSignals;
    procedure ExecuteCycle;
    procedure ExecuteCycleManual;
  end;

var
  CPUForm: TCPUForm;

implementation

{$R *.dfm}

uses
  RTTI, Microcontroller, Languages, CPUConfig;

{******************************************
procedure TCPUForm.ButtonMemoryReadClick(Sender: TObject);
begin
  inherited;
  EditMemoryValue.Text := IntToStr( Computer.RAM[ StrToInt( EditMemoryAddress.Text ) ] );
end;

procedure TCPUForm.ButtonMemoryWriteClick(Sender: TObject);
begin
  inherited;
  Computer.RAM[ StrToInt( EditMemoryAddress.Text ) ] := StrToInt( EditMemoryValue.Text );
end;

procedure TCPUForm.ButtonRegisterReadClick(Sender: TObject);
begin
  inherited;
  if RadioGroup1.ItemIndex < 0 then
    exit;
  EditRegister.Text := IntToStr( Computer.AllRegisters[ TRegisterCode( RadioGroup1.ItemIndex ) ].Value );
end;

procedure TCPUForm.ButtonRegisterWriteClick(Sender: TObject);
begin
  inherited;
  if RadioGroup1.ItemIndex < 0 then
    exit;
  Computer.AllRegisters[ TRegisterCode( RadioGroup1.ItemIndex ) ].Value := StrToInt( EditRegister.Text );
end;

}

procedure TCPUForm.ChangeRegisterValue( ARegister: VisualControls.TRegister );
var
  Value : string;
begin
  Value := IntToStr( ARegister.RegisterValue );
  if InputQuery( Format( lsModifyRegisterCaption, [ ARegister.RegisterName ] ),
        Format( lsModifyRegisterPrompt, [ ARegister.RegisterName ] ), Value ) then
    ARegister.RegisterValue := StrToInt( Value );
end;

procedure TCPUForm.CheckBoxManualControlClick(Sender: TObject);
begin
  inherited;
  if not CheckBoxManualControl.Checked then // wcześniej było to zaznaczone
    ClearSignals;
end;

procedure TCPUForm.ClearSignals;
var
  i : integer;
begin
  for i := 0 to ComponentCount - 1 do
    if Components[ i ] is TControlSignal then
       with TControlSignal( Components[ i ] ) do
         Active := false;
end;

procedure TCPUForm.ControlSignalClick(Sender: TObject);
begin
  inherited;
  if not CheckBoxManualControl.Checked then
    exit;
  with Sender as TControlSignal do
    Active := not Active;
end;

procedure TCPUForm.ExecuteCycle;
var
  Signals : TSignalSet;
  i : integer;
begin
  if not CheckBoxManualControl.Checked then
  begin
    ShowActiveSignals;
    Controller.ExecuteNextCycle;
  end
  else
  begin
    Signals := [];
    for i := 0 to ComponentCount - 1 do
      if Components[ i ] is TControlSignal then
         with TControlSignal( Components[ i ] ) do
           if Visible and Active then
             Signals := Signals + [ Signal ];
    Computer.ExecuteCycle( Signals );
  end;
  ListBoxPaO.Refresh;
  Invalidate;
end;

procedure TCPUForm.ExecuteCycleManual;
var
  Signals : TSignalSet;
  i : integer;
begin
  Signals := [];
  for i := 0 to ComponentCount - 1 do
    if Components[ i ] is TControlSignal then
       with TControlSignal( Components[ i ] ) do
         if Visible and Active then
           Signals := Signals + [ Signal ];
  Computer.ExecuteCycle( Signals );
  ListBoxPaO.Refresh;
  Invalidate;
end;

procedure TCPUForm.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  inherited;
  Action := caNone;
end;

procedure TCPUForm.FormCreate(Sender: TObject);
var
  //s : TSignals;
  i, RAMSize : integer;
begin
  inherited;
  Caption := 'CPU';
  MenuItemSetRegisterValue.Caption := lsMenuSetRegisterValue;
  MenuItemShowRegisterAs.Caption := lsMenuShowRegisterAs;
  MenuItemClearRegister.Caption := lsMenuClearRegister;
  MenuItemShowAsSigned.Caption := lsMenuShowAsSigned;
  MenuItemShowAsUnsigned.Caption := lsMenuShowAsUnsigned;
  MenuItemShowAsBinary.Caption := lsMenuShowAsBinary;
  MenuItemShowAsHexadecimal.Caption := lsMenuShowAsHexadecimal;
  CheckBoxManualControl.Caption := lsManualControlCheckBox;
  //CheckListBox1.Items.Clear;
  //for s := Low( TSignals ) to High( TSignals ) do
    //CheckListBox1.Items.Add( TRttiEnumerationType.GetName( s ) );
  ListBoxPaO.Items.Clear;
  RAMSize := ( 1 shl Computer.AddressLength );
  for i := 0 to RAMSize - 1 do
    ListBoxPaO.Items.Add( IntToStr( i ) );
  ListBoxPaO.ItemIndex := 0;
  Computer.OnRefresh := RefreshComputerView;
end;


procedure TCPUForm.FormPaint(Sender: TObject);
var
  R : TRect;
  T : string;
begin
  inherited;
  with Canvas do
  begin
    // JAL
    T := lsALUName;
    Brush.Color := clwindow;
    R := Rect( 341, 305, 341 + 171, 305 + 135 );
    Rectangle( R );
    TextRect( R, T, [ tfSingleLine, tfCenter, tfVerticalCenter ] );
    // wyad
    MoveTo( 213, 135 );
    LineTo( 208, 125 );
    MoveTo( 203, 135 );
    LineTo( 208, 125 );
    LineTo( 208, 418 );
    // wei
    MoveTo( 143, 450 );
    LineTo( 138, 440 );
    MoveTo( 133, 450 );
    LineTo( 138, 440 );
    LineTo( 138, 483 );
    // weja
    MoveTo( 425, 450 );
    LineTo( 420, 440 );
    MoveTo( 415, 450 );
    LineTo( 420, 440 );
    LineTo( 420, 483 );
    // wes
    MoveTo( 747, 450 );
    LineTo( 742, 440 );
    MoveTo( 737, 450 );
    LineTo( 742, 440 );
    LineTo( 742, 483 );
    // wys
    MoveTo( 705, 473 );
    LineTo( 700, 483 );
    MoveTo( 695, 473 );
    LineTo( 700, 483 );
    LineTo( 700, 440 );
    // wyak
    MoveTo( 575, 473 );
    LineTo( 570, 483 );
    MoveTo( 565, 473 );
    LineTo( 570, 483 );
    LineTo( 570, 240 );
    LineTo( 420, 240 );
    LineTo( 420, 280 );
    // wea
    MoveTo( 721, 165 );
    LineTo( 716, 175 );
    MoveTo( 711, 165 );
    LineTo( 716, 175 );
    LineTo( 716, 125 );
    // wel
    MoveTo( 124, 165 );
    LineTo( 119, 175 );
    MoveTo( 114, 165 );
    LineTo( 119, 175 );
    LineTo( 119, 125 );
    // wyl
    MoveTo( 80, 135 );
    LineTo( 75, 125 );
    MoveTo( 70, 135 );
    LineTo( 75, 125 );
    LineTo( 75, 175 );
    // wyls
    if ControlSignalWyls.Visible then
    begin
      MoveTo( 35, 473 );
      LineTo( 30, 483 );
      MoveTo( 25, 473 );
      LineTo( 30, 483 );
      LineTo( 30, 200 );
    end;
    // wews
    if ControlSignalWews.Visible then
    begin
      MoveTo( 450, 165 );
      LineTo( 445, 175 );
      MoveTo( 440, 165 );
      LineTo( 445, 175 );
      LineTo( 445, 125 );
    end;
    // wyws
    if ControlSignalWyws.Visible then
    begin
      MoveTo( 410, 135 );
      LineTo( 405, 125 );
      MoveTo( 400, 135 );
      LineTo( 405, 125 );
      LineTo( 405, 175 );
    end;
    // wyx
    if ControlSignalWyx.Visible then
    begin
      MoveTo( 117, 500 );
      LineTo( 112, 490 );
      MoveTo( 107, 500 );
      LineTo( 112, 490 );
      LineTo( 112, 528 );
    end;
    // wex
    if ControlSignalWex.Visible then
    begin
      MoveTo( 83, 518 );
      LineTo( 78, 528 );
      MoveTo( 73, 518 );
      LineTo( 78, 528 );
      LineTo( 78, 490 );
    end;
    // wyy
    if ControlSignalWyy.Visible then
    begin
      MoveTo( 348, 500 );
      LineTo( 343, 490 );
      MoveTo( 338, 500 );
      LineTo( 343, 490 );
      LineTo( 343, 528 );
    end;
    // wey
    if ControlSignalWey.Visible then
    begin
      MoveTo( 314, 518 );
      LineTo( 309, 528 );
      MoveTo( 304, 518 );
      LineTo( 309, 528 );
      LineTo( 309, 490 );
    end;
    // wyrb
    if ControlSignalWyrb.Visible then
    begin
      MoveTo( 579, 500 );
      LineTo( 574, 490 );
      MoveTo( 569, 500 );
      LineTo( 574, 490 );
      LineTo( 574, 528 );
    end;
    // werb
    if ControlSignalWerb.Visible then
    begin
      MoveTo( 545, 518 );
      LineTo( 540, 528 );
      MoveTo( 535, 518 );
      LineTo( 540, 528 );
      LineTo( 540, 490 );
    end;
    // wyg
    if ControlSignalWyG.Visible then
    begin
      MoveTo( 759, 500 );
      LineTo( 754, 490 );
      MoveTo( 749, 500 );
      LineTo( 754, 490 );
      LineTo( 754, 528 );
    end;
    // start
    if ControlSignalStart.Visible then
    begin
      MoveTo( 725, 518 );
      LineTo( 720, 528 );
      MoveTo( 715, 518 );
      LineTo( 720, 528 );
      LineTo( 720, 490 );
    end;
    // wyap
    if ControlSignalWyAP.Visible then
    begin
      MoveTo( 721, 110 );
      LineTo( 716, 120 );
      MoveTo( 711, 110 );
      LineTo( 716, 120 );
      LineTo( 716, 75 );
    end;
    // wyrm
    if ControlSignalWyRM.Visible then
    begin
      MoveTo( 290, 110 );
      LineTo( 285, 120 );
      MoveTo( 280, 110 );
      LineTo( 285, 120 );
      LineTo( 285, 75 );
    end;
    // wyrm
    if ControlSignalWeRM.Visible then
    begin
      MoveTo( 324, 89);
      LineTo( 319, 79);
      MoveTo( 314, 89);
      LineTo( 319, 79);
      LineTo( 319, 120 );
    end;
  end;
end;

procedure TCPUForm.ListBoxPaODblClick(Sender: TObject);
var
  Value : string;
begin
  inherited;
  if ListBoxPaO.ItemIndex < 0 then
    exit;
  Value := IntToStr( Computer.RAM[ ListBoxPaO.ItemIndex ] );
  if InputQuery( lsModifyRAMQueryCaption, Format( lsModifyRAMQueryPrompt,
        [ ListBoxPaO.ItemIndex ] ), Value ) then
    Computer.RAM[ ListBoxPaO.ItemIndex ] := StrToInt( Value );
  ListBoxPao.Invalidate;
end;

procedure TCPUForm.ListBoxPaODrawItem(Control: TWinControl; Index: Integer;
  Rect: TRect; State: TOwnerDrawState);
var
  T : string;
  Mask : integer;
begin
  Mask := ( 1 shl Computer.AddressLength ) - 1;
  T := ' (???)';
  try
    T := #9'(' + Controller.Instructions[ Computer.RAM[ Index ] shr Computer.AddressLength ].Mnemonic
        + ' ' + IntToStr( Computer.RAM[ Index ] and Mask ) + ')';
  except on Exception do
    T := #9' (???)';
  end;
  T := ' ' + ( Control as TListBox ).Items[ Index ] + ':'#9
      + IntToStr( Computer.RAM[ Index ] ) + T;
  with ( Control as TListBox ).Canvas do
  begin
    FillRect( Rect );
    TextRect( Rect, T, [ tfExpandTabs ] );
  end;
end;

procedure TCPUForm.ListBoxPaOKeyPress(Sender: TObject; var Key: Char);
begin
  inherited;
  if Key = #13 then
    ListBoxPaODblClick( Sender );
end;

procedure TCPUForm.MenuItemClearRegisterClick(Sender: TObject);
begin
  inherited;
  CurrentRegister.RegisterValue := 0;
end;

procedure TCPUForm.MenuItemSetRegisterValueClick(Sender: TObject);
begin
  inherited;
  ChangeRegisterValue( CurrentRegister );
end;

procedure TCPUForm.MenuItemShowAsBinaryClick(Sender: TObject);
begin
  inherited;
  CurrentRegister.NumeralSystem := nsBinary;
end;

procedure TCPUForm.MenuItemShowAsHexadecimalClick(Sender: TObject);
begin
  inherited;
  CurrentRegister.NumeralSystem := nsHexadecimal;
end;

procedure TCPUForm.MenuItemShowAsSignedClick(Sender: TObject);
begin
  inherited;
  CurrentRegister.NumeralSystem := nsSiged;
end;

procedure TCPUForm.MenuItemShowAsUnsignedClick(Sender: TObject);
begin
  inherited;
  CurrentRegister.NumeralSystem := nsUnsigned;
end;

procedure TCPUForm.PopupMenuRegisterPopup(Sender: TObject);
begin
  inherited;
  CurrentRegister := ( Sender as TPopupMenu ).PopupComponent as VisualControls.TRegister;
  MenuItemShowAsSigned.Checked := false;
  MenuItemShowAsUnsigned.Checked := false;
  MenuItemShowAsBinary.Checked := false;
  MenuItemShowAsHexadecimal.Checked := false;
  case CurrentRegister.NumeralSystem of
    nsUnsigned: MenuItemShowAsUnsigned.Checked := true;
    nsSiged: MenuItemShowAsSigned.Checked := true;
    nsBinary: MenuItemShowAsBinary.Checked := true;
    nsHexadecimal: MenuItemShowAsHexadecimal.Checked := true;
  end;
end;

// procedura wywoływana gdy trzeba odświezyć widok komputera
procedure TCPUForm.RefreshComputerView(Sender: TObject);
begin
  RegisterS.Invalidate;
  RegisterA.Invalidate;
  RegisterL.Invalidate;
  RegisterAK.Invalidate;
  RegisterI.Invalidate;
  RegisterWS.Invalidate;
  RegisterX.Invalidate;
  RegisterY.Invalidate;
  RegisterRB.Invalidate;
  RegisterG.Invalidate;
  RegisterAP.Invalidate;
  RegisterRP.Invalidate;
  RegisterRM.Invalidate;
  RegisterRZ.Invalidate;
  ListBoxPao.Invalidate;
end;

procedure TCPUForm.RegisterDblClick(Sender: TObject);
begin
  inherited;
  ChangeRegisterValue( Sender as VisualControls.TRegister );
end;


procedure TCPUForm.ShowActiveSignals;
begin
  Controller.BeforeExecuteCycle := SignalsHighlighter;
end;

procedure TCPUForm.ShowCPUDialog;
var
  F : TCPUConfigForm;
begin
  F := TCPUConfigForm.Create( Application );
  with F do
  try
    Available[ ccInterbusConnection ] := BusAS.Visible;
    Available[ ccIncAndDecAccumulator ] := ControlSignalIAk.Visible;
    Available[ ccLogicOpInALU ] := ControlSignalLub.Visible;
    Available[ ccExtendedArithmetic ] := ControlSignalMno.Visible;
    Available[ ccStackAvailable ] := RegisterWS.Visible;
    Available[ ccXRegister ] := RegisterX.Visible;
    Available[ ccYRegister ] := RegisterY.Visible;
    Available[ ccInterrupts ] := RegisterRZ.Visible;
    Available[ ccIOOperations ] := RegisterRB.Visible;
    if ShowModal = mrOK then
    begin
      RegisterAP.Length := Computer.AddressLength;
      RegisterL.Length := Computer.AddressLength;
      RegisterWS.Length := Computer.AddressLength;
      RegisterA.Length := Computer.AddressLength;
      RegisterI.Length := Computer.WordLength;
      RegisterAK.Length := Computer.WordLength;
      RegisterS.Length := Computer.WordLength;
      RegisterX.Length := Computer.WordLength;
      RegisterY.Length := Computer.WordLength;
      RegisterRB.Length := Computer.WordLength;
      // widoczność
      BusAS.Visible := Available[ ccInterbusConnection ];
      ControlSignalIAk.Visible := Available[ ccIncAndDecAccumulator ];
      ControlSignalLub.Visible := Available[ ccLogicOpInALU ];
      ControlSignalMno.Visible := Available[ ccExtendedArithmetic ];
      RegisterWS.Visible := Available[ ccStackAvailable ];
      RegisterX.Visible := Available[ ccXRegister ];
      RegisterY.Visible := Available[ ccYRegister ];
      RegisterRZ.Visible := Available[ ccInterrupts ];
      RegisterRB.Visible := Available[ ccIOOperations ];
      // cd. widoczności
      ControlSignalAS.Visible := BusAS.Visible;
      ControlSignalDAk.Visible := ControlSignalIAk.Visible;
      ControlSignalNeg.Visible := ControlSignalLub.Visible;
      ControlSignalI.Visible := ControlSignalLub.Visible;
      ControlSignalShr.Visible := ControlSignalLub.Visible;
      ControlSignalShl.Visible := ControlSignalLub.Visible;
      ControlSignalDziel.Visible := ControlSignalMno.Visible;
      ControlSignalWews.Visible := RegisterWS.Visible;
      ControlSignalWyws.Visible := RegisterWS.Visible;
      ControlSignalIws.Visible := RegisterWS.Visible;
      ControlSignalDws.Visible := RegisterWS.Visible;
      ControlSignalWeX.Visible := RegisterX.Visible;
      ControlSignalWyX.Visible := RegisterX.Visible;
      ControlSignalWeY.Visible := RegisterY.Visible;
      ControlSignalWyY.Visible := RegisterY.Visible;
      ControlSignalWyRM.Visible := RegisterRZ.Visible;
      ControlSignalWeRM.Visible := RegisterRZ.Visible;
      ControlSignalEni.Visible := RegisterRZ.Visible;
      ControlSignalRint.Visible := RegisterRZ.Visible;
      ControlSignalWyAP.Visible := RegisterRZ.Visible;
      RegisterAP.Visible := RegisterRZ.Visible;
      RegisterRP.Visible := RegisterRZ.Visible;
      RegisterRM.Visible := RegisterRZ.Visible;
      SpeedButton1.Visible := RegisterRZ.Visible;
      SpeedButton2.Visible := RegisterRZ.Visible;
      SpeedButton3.Visible := RegisterRZ.Visible;
      SpeedButton4.Visible := RegisterRZ.Visible;
      ControlSignalWyLS.Visible := RegisterRZ.Visible or RegisterWS.Visible;
      RegisterG.Visible := RegisterRB.Visible;
      ControlSignalWyRB.Visible := RegisterRB.Visible;
      ControlSignalWeRB.Visible := RegisterRB.Visible;
      ControlSignalStart.Visible := RegisterRB.Visible;
      ControlSignalWyG.Visible := RegisterRB.Visible;
    end;
  finally
    Free;
  end;
  Invalidate;
end;

procedure TCPUForm.SignalsHighlighter(Sender: TObject; SignalSet: TSignalSet);
var
  i : integer;
begin
  for i := 0 to ComponentCount - 1 do
    if Components[ i ] is TControlSignal then
       with TControlSignal( Components[ i ] ) do
         Active := Signal in SignalSet;
end;

procedure TCPUForm.SpeedButtonClick(Sender: TObject);
begin
  inherited;
  with Sender as TSpeedButton do
  begin
    RegisterRZ.RegisterValue := RegisterRZ.RegisterValue or TSpeedButton( Sender ).Tag;
  end;
end;

initialization
  CPUForm := nil;

end.

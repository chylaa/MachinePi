unit CPUConfig;

{
  TODO: obsługa ostatniej zakładki do zrobienia
}

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.ComCtrls, Vcl.TabNotBk, Vcl.StdCtrls,
  Vcl.Samples.Spin, Vcl.ExtCtrls, Vcl.CheckLst;

type
  TComputerComponent = ( ccInterbusConnection, ccIncAndDecAccumulator, ccLogicOpInALU,
        ccExtendedArithmetic, ccStackAvailable, ccXRegister, ccYRegister,
        ccInterrupts, ccIOOperations );

type
  TComputerComponents = set of TComputerComponent;

type
  TCPUConfigForm = class(TForm)
    PageControl1: TPageControl;
    TabSheetArchitecture: TTabSheet;
    TabSheetComponents: TTabSheet;
    TabSheetAddresses: TTabSheet;
    ButtonOK: TButton;
    ButtonCancel: TButton;
    GroupBoxMachineWord: TGroupBox;
    LabelAddressLength: TLabel;
    LabelCodeLength: TLabel;
    SpinEditAddressLength: TSpinEdit;
    SpinEditCodeLength: TSpinEdit;
    RadioGroupType: TRadioGroup;
    CheckListBox: TCheckListBox;
    GroupBoxInterrupts: TGroupBox;
    GroupBoxIODevices: TGroupBox;
    EditLabel1: TEdit;
    EditLabel2: TEdit;
    EditLabel3: TEdit;
    EditLabel4: TEdit;
    Label1: TLabel;
    Label2: TLabel;
    Label4: TLabel;
    Label3: TLabel;
    LabelLabels: TLabel;
    EditAddress1: TEdit;
    EditAddress2: TEdit;
    LabelAddresses: TLabel;
    EditAddress3: TEdit;
    EditAddress4: TEdit;
    LabeledEditInput: TLabeledEdit;
    LabeledEditOutput: TLabeledEdit;
    procedure FormCreate(Sender: TObject);
    procedure ButtonOKClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure CheckListBoxClickCheck(Sender: TObject);
    procedure RadioGroupTypeClick(Sender: TObject);
    procedure EditAddressExit(Sender: TObject);
    procedure LabeledEditInputExit(Sender: TObject);
  private
    function GetAvailable(Index: TComputerComponent): Boolean;
    procedure SetAvailable(Index: TComputerComponent; const Value: Boolean);
    procedure SetMachineType;
    procedure SetCheckListBox( config : TComputerComponents );
  public
    property Available[ Index : TComputerComponent ] : Boolean read GetAvailable write SetAvailable;
  end;

implementation

{$R *.dfm}

uses
  Languages, ControlUnit;

const
  mtMachineW = [];
  mtMachineWPlus = [ ccInterbusConnection ];
  mtMachineL = [ ccInterbusConnection, ccStackAvailable, ccInterrupts ];
  mtMachineEW = [ ccInterbusConnection, ccStackAvailable, ccInterrupts,
        ccIOOperations, ccExtendedArithmetic ];

procedure TCPUConfigForm.ButtonOKClick(Sender: TObject);
begin
  Computer.CodeLength := SpinEditCodeLength.Value;
  Computer.AddressLength := SpinEditAddressLength.Value;
  Computer.InterruptVector[ 1 ] := StrToInt( EditAddress1.Text );
  Computer.InterruptVector[ 2 ] := StrToInt( EditAddress2.Text );
  Computer.InterruptVector[ 3 ] := StrToInt( EditAddress3.Text );
  Computer.InterruptVector[ 4 ] := StrToInt( EditAddress4.Text );
  Computer.InterruptLabel[ 1 ] := EditLabel1.Text;
  Computer.InterruptLabel[ 2 ] := EditLabel2.Text;
  Computer.InterruptLabel[ 3 ] := EditLabel3.Text;
  Computer.InterruptLabel[ 4 ] := EditLabel4.Text;
  Computer.InputPort := StrToInt( LabeledEditInput.Text );
  Computer.OutputPort := StrToInt( LabeledEditOutput.Text );
end;

procedure TCPUConfigForm.CheckListBoxClickCheck(Sender: TObject);
begin
  SetMachineType;
end;

procedure TCPUConfigForm.EditAddressExit(Sender: TObject);
begin
  with Sender as TEdit do
    try
      StrToInt( Text );
    except on Exception do
      begin
        SetFocus;
        raise;
      end;
    end;
end;

procedure TCPUConfigForm.SetCheckListBox(config: TComputerComponents);
var
  c : TComputerComponent;
begin
  for c := Low( TComputerComponent ) to High( TComputerComponent ) do
    CheckListBox.Checked[ Ord( c ) ] := c in config;
end;

procedure TCPUConfigForm.FormActivate(Sender: TObject);
begin
  SetMachineType;
  EditAddress1.Text := IntToStr( Computer.InterruptVector[ 1 ] );
  EditAddress2.Text := IntToStr( Computer.InterruptVector[ 2 ] );
  EditAddress3.Text := IntToStr( Computer.InterruptVector[ 3 ] );
  EditAddress4.Text := IntToStr( Computer.InterruptVector[ 4 ] );
  EditLabel1.Text := Computer.InterruptLabel[ 1 ];
  EditLabel2.Text := Computer.InterruptLabel[ 2 ];
  EditLabel3.Text := Computer.InterruptLabel[ 3 ];
  EditLabel4.Text := Computer.InterruptLabel[ 4 ];
  LabeledEditInput.Text := IntToStr( Computer.InputPort );
  LabeledEditOutput.Text := IntToStr( Computer.OutputPort );
end;

procedure TCPUConfigForm.FormCreate(Sender: TObject);
begin
  Caption := lsCPUConfigCaption;
  TabSheetArchitecture.Caption := lsCPUConfigArchitecture;
  TabSheetComponents.Caption := lsCPUConfigComponents;
  TabSheetAddresses.Caption := lsCPUConfigAdresses;
  ButtonOK.Caption := lsOKButton;
  ButtonCancel.Caption := lsCancelButton;
  GroupBoxMachineWord.Caption := lsGroupBoxMachineWord;
  LabelAddressLength.Caption := lsLabelAddressLength;
  LabelCodeLength.Caption := lsLabelCodeLength;
  RadioGroupType.Caption := lsGroupBoxMachineType;
  with CheckListBox do
  begin
    Clear;
    Items.Add( lsInterbusConnection );
    Items.Add( lsIncAndDecAccumulator );
    Items.Add( lsLogicOpInALU );
    Items.Add( lsExtendedArithmetic );
    Items.Add( lsStackAvailable );
    Items.Add( lsXRegister );
    Items.Add( lsYRegister );
    Items.Add( lsInterrupts );
    Items.Add( lsIOOperations );
  end;
  GroupBoxInterrupts.Caption := lsInterruptHandlers;
  GroupBoxIODevices.Caption := lsIODevices;
  LabelLabels.Caption := lsLabelLabels;
  LabelAddresses.Caption := lsLabelAddresses;
  LabeledEditInput.EditLabel.Caption := lsLabelInput;
  LabeledEditOutput.EditLabel.Caption := lsLabelOutput;
  // teraz ustalam wartości
  SpinEditAddressLength.Value := Computer.AddressLength;
  SpinEditCodeLength.Value := Computer.CodeLength;
end;

function TCPUConfigForm.GetAvailable(Index: TComputerComponent): Boolean;
begin
  Result := CheckListBox.Checked[ Ord( Index ) ];
end;


procedure TCPUConfigForm.LabeledEditInputExit(Sender: TObject);
begin
  with Sender as TLabeledEdit do
    try
      StrToInt( Text );
    except on Exception do
      begin
        SetFocus;
        raise;
      end;
    end;

end;

procedure TCPUConfigForm.RadioGroupTypeClick(Sender: TObject);
begin
  case RadioGroupType.ItemIndex of
  0 : SetCheckListBox( mtMachineW );
  1 : SetCheckListBox( mtMachineWPlus );
  2 : SetCheckListBox( mtMachineL );
  3 : SetCheckListBox( mtMachineEW );
  end;
end;

procedure TCPUConfigForm.SetAvailable(Index: TComputerComponent;
  const Value: Boolean);
begin
  CheckListBox.Checked[ Ord( Index ) ] := Value;
end;

procedure TCPUConfigForm.SetMachineType;
var
  c : TComputerComponent;
  s : TComputerComponents;
begin
  s := [];
  for c := Low( TComputerComponent ) to High( TComputerComponent ) do
    if Available[ c ] then
      s := s + [ c ];
  RadioGroupType.ItemIndex := -1;
  if s = mtMachineW then
    RadioGroupType.ItemIndex := 0
  else if s = mtMachineWPlus then
    RadioGroupType.ItemIndex := 1
  else if s = mtMachineL then
    RadioGroupType.ItemIndex := 2
  else if s = mtMachineEW then
    RadioGroupType.ItemIndex := 3
end;

end.

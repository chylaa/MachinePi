unit IODevices;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, BaseUnit, Vcl.Menus, Vcl.StdCtrls;

type
  EInvalidPortNumber = class( Exception );

type
  TIOConsoleForm = class(TBaseForm)
    GroupBoxInput: TGroupBox;
    EditInput: TEdit;
    GroupBoxOutput: TGroupBox;
    MemoOutput: TMemo;
    FontDialog: TFontDialog;
    MenuItemTools: TMenuItem;
    MenuItemIOFont: TMenuItem;
    MenuItemClearOutput: TMenuItem;
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure MenuItemIOFontClick(Sender: TObject);
    procedure MenuItemClearOutputClick(Sender: TObject);
    procedure EditInputChange(Sender: TObject);
  private
    InIOCycle : Boolean;  // czy wystartowano urządzenie ale to czeka na znak
    procedure GetOneChar;
    procedure StartHandler( Obj : TObject );
  public
    { Public declarations }
  end;

var
  IOConsoleForm: TIOConsoleForm;

implementation

{$R *.dfm}

uses
  Languages, ControlUnit, CPUView;

procedure TIOConsoleForm.EditInputChange(Sender: TObject);
begin
  inherited;
  if InIOCycle and ( EditInput.Text <> '' ) then
    GetOneChar;
end;

procedure TIOConsoleForm.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  inherited;
  Action := caHide;
end;

procedure TIOConsoleForm.FormCreate(Sender: TObject);
begin
  inherited;
  InIOCycle := false;
  Computer.OnStartSignal := StartHandler;
  Caption := lsIOConsoleCaption;
  GroupBoxInput.Caption := lsInputCaption;
  GroupBoxOutput.Caption := lsOutputCaption;
  MenuItemTools.Caption := lsToolsMenuCaption;
  MenuItemIOFont.Caption := lsToolsFontCaption;
  MenuItemClearOutput.Caption := lsClearOutputCaption;
end;

procedure TIOConsoleForm.FormDestroy(Sender: TObject);
begin
  Computer.OnStartSignal := nil;
  inherited;
end;

procedure TIOConsoleForm.GetOneChar;
var
  NewChar : char;
begin
  NewChar := EditInput.Text [ 1 ];
  EditInput.Text := Copy( EditInput.Text, 2, 1024 );
  Computer.RegisterRB := Ord( NewChar );
  Computer.RegisterG := 1;
  CPUForm.Invalidate;
end;

procedure TIOConsoleForm.MenuItemClearOutputClick(Sender: TObject);
begin
  inherited;
  MemoOutput.Clear
end;

procedure TIOConsoleForm.MenuItemIOFontClick(Sender: TObject);
begin
  inherited;
  FontDialog.Font := MemoOutput.Font;
  if FontDialog.Execute then
  begin
    MemoOutput.Font := FontDialog.Font;
    EditInput.Font := FontDialog.Font;
  end;
end;

procedure TIOConsoleForm.StartHandler(Obj: TObject);
var
  Port : integer;
  NewChar : string;
begin
  NewChar := Chr( Computer.RegisterRB );
  if Computer.RegisterRB in [ 10, 13 ] then
    NewChar := #13#10;
  Port := Computer.RegisterAD;
  if Port = Computer.OutputPort then
  begin
    MemoOutput.Text := MemoOutput.Text + NewChar;
    Computer.RegisterG := 1;
  end
  else if Port = Computer.InputPort then
    begin
      if EditInput.Text = '' then
      begin
        InIOCycle := true;
        Computer.RegisterG := 0;
      end
      else
        GetOneChar;
    end
  else
    raise EInvalidPortNumber.Create( lsInvalidPortNumber );
end;

end.

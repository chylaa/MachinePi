unit MainUnit;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.ComCtrls, Vcl.Menus, Vcl.ExtCtrls;

type
  TMainForm = class(TForm)
    StatusBar: TStatusBar;
    MainMenu: TMainMenu;
    FileMenuItem: TMenuItem;
    ExitMenuItem: TMenuItem;
    ViewMenuItem: TMenuItem;
    ViewCPUMenuItem: TMenuItem;
    PanelLeft: TPanel;
    PanelRight: TPanel;
    SplitterHorizontal: TSplitter;
    SplitterVertical: TSplitter;
    PageControl: TPageControl;
    TestMenuItem: TMenuItem;
    RunTestMenuItem: TMenuItem;
    PageControlCPU: TPageControl;
    MenuItemLine1: TMenuItem;
    CodeTestMenuItem: TMenuItem;
    MenuItemProcessor: TMenuItem;
    MenuItemCPUConfiguration: TMenuItem;
    MenuItemRun: TMenuItem;
    MenuItemRunCycle: TMenuItem;
    MenuItemViewIO: TMenuItem;
    MenuItemFileNew: TMenuItem;
    MenuItemNewProgram: TMenuItem;
    procedure FormCreate(Sender: TObject);
    procedure FormResize(Sender: TObject);
    procedure RunTestMenuItemClick(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure PageControlCPUGetSiteInfo(Sender: TObject; DockClient: TControl;
      var InfluenceRect: TRect; MousePos: TPoint; var CanDock: Boolean);
    procedure PageControlCPUUnDock(Sender: TObject; Client: TControl;
      NewTarget: TWinControl; var Allow: Boolean);
    procedure ExitMenuItemClick(Sender: TObject);
    procedure ViewCPUMenuItemClick(Sender: TObject);
    procedure ViewWindowMenuItemClick(Sender: TObject);
    procedure CodeTestMenuItemClick(Sender: TObject);
    procedure MenuItemCPUConfigurationClick(Sender: TObject);
    procedure MenuItemRunCycleClick(Sender: TObject);
    procedure MenuItemViewIOClick(Sender: TObject);
    procedure MenuItemNewProgramClick(Sender: TObject);
  private
    procedure DefineShortCuts;
    procedure DeleteWindowFromMenu( Sender : TObject );
    procedure Localize;
    procedure ShowCPUForm;
    procedure ShowHint( Sender : TObject );
    procedure ShowWindow( Form : TCustomForm );
  public
    { Public declarations }
  end;

type
  ENoBaseForm = class( Exception );
  ESenderIsNil = class( Exception );

var
  MainForm: TMainForm;

implementation

{$R *.dfm}

uses
  System.UITypes, Languages, VersionUtils, BaseUnit, DebugUnit,
  CPUView, IODevices, ProgramEditor, Microcontroller;

procedure TMainForm.CodeTestMenuItemClick(Sender: TObject);
var
  MenuItem : TMenuItem;
  AWindow : TBaseForm;
begin
  AWindow := TDebugForm.Create( Application );
  AWindow.Show;
  // dołącz okno do menu
  MenuItem := TMenuItem.Create( Self );
  MenuItem.Caption := AWindow.Caption;
  MenuItem.Tag := Integer( AWindow );
  ViewMenuItem.Add( MenuItem );
  MenuItem.OnClick := ViewWindowMenuItemClick;
  // pamiętaj o odnotowaniu zamknięcia okna
  AWindow.OnCloseEvent := DeleteWindowFromMenu;
end;

procedure TMainForm.DefineShortCuts;
begin
  MenuItemRunCycle.ShortCut := vkF7;
end;

procedure TMainForm.DeleteWindowFromMenu( Sender : TObject );
var
  i : integer;
  f : TBaseForm;
begin
  if not Assigned( Sender ) then
    raise ESenderIsNil.Create('ESenderIsNil exception in TMainForm.DeleteWindowFromMenu');
  if not ( Sender is TBaseForm ) then
    raise ENoBaseForm.Create( 'ENoBaseForm exception in TMainForm.DeleteWindowFromMenu' );
  // usunąć okno typu TBaseForm z menu View
  f := TBaseForm( Sender );
  for i := 0 to ViewMenuItem.Count - 1 do
    if TMenuItem( ViewMenuItem.Items[ i ] ).Tag = Integer( f ) then
    begin
      ViewMenuItem.Delete( i );
      break;
    end;
end;

procedure TMainForm.ExitMenuItemClick(Sender: TObject);
begin
  Close;
end;

procedure TMainForm.FormCreate(Sender: TObject);
begin
  PanelLeft.Width := 840;
  PageControlCPU.Height := 570;
  // ustaw komunikaty we właściwym języku
  Localize;
  DefineShortCuts;
  // ustaw podpwiedzi
  Application.OnHint := ShowHint;
  StatusBar.Panels[ 0 ].Text := '';
  StatusBar.Panels[ 1 ].Text := VersionUtils.FileVersion + ' ' + LanguageCode;
end;

procedure TMainForm.FormResize(Sender: TObject);
begin
  StatusBar.Panels[ 0 ].Width := Width - StatusBar.Panels[ 1 ].Width;
end;

procedure TMainForm.FormShow(Sender: TObject);
begin
  ShowCPUForm;
end;

procedure TMainForm.Localize;
begin
  Caption := lsMainWindowCaption;
  FileMenuItem.Caption := lsFileMenuItemCaption;
  MenuItemFileNew.Caption := lsMenuItemFileNewCaption;
  ExitMenuItem.Caption := lsExitMenuItemCaption;
  ViewMenuItem.Caption := lsViewMenuItemCaption;
  ViewCPUMenuItem.Caption := lsViewCPUMenuItemCaption;
  MenuItemProcessor.Caption := lsMenuItemProcessor;
  MenuItemCPUConfiguration.Caption := lsMenuItemConfiguation;
  MenuItemRun.Caption := lsMenuItemRunCaption;
  MenuItemRunCycle.Caption := lsMenuItemRunCycleCaption;
  MenuItemViewIO.Caption := lsMenuItemViewIOCaption;
end;

procedure TMainForm.MenuItemCPUConfigurationClick(Sender: TObject);
begin
  CPUForm.ShowCPUDialog;
end;

procedure TMainForm.MenuItemNewProgramClick(Sender: TObject);
var
  MenuItem : TMenuItem;
  AWindow : TProgramEditorForm;
begin
  AWindow := TProgramEditorForm.Create( Application );
  AWindow.Show;
  // dołącz okno do menu
  MenuItem := TMenuItem.Create( Self );
  MenuItem.Caption := AWindow.Caption;
  MenuItem.Tag := Integer( AWindow );
  ViewMenuItem.Add( MenuItem );
  MenuItem.OnClick := ViewWindowMenuItemClick;
  // pamiętaj o odnotowaniu zamknięcia okna
  AWindow.OnCloseEvent := DeleteWindowFromMenu;
end;

procedure TMainForm.MenuItemRunCycleClick(Sender: TObject);
begin
  CPUForm.ExecuteCycle;
end;

procedure TMainForm.MenuItemViewIOClick(Sender: TObject);
begin
  if IOConsoleForm.Visible then
    IOConsoleForm.Hide
  else
  begin
    IOConsoleForm.Show;
    IOConsoleForm.SetFocus;
  end;
  MenuItemViewIO.Checked := IOConsoleForm.Visible;
end;

procedure TMainForm.PageControlCPUGetSiteInfo(Sender: TObject; DockClient: TControl;
  var InfluenceRect: TRect; MousePos: TPoint; var CanDock: Boolean);
begin
  CanDock := DockClient is TCPUForm;
end;

procedure TMainForm.PageControlCPUUnDock(Sender: TObject; Client: TControl;
  NewTarget: TWinControl; var Allow: Boolean);
begin
  Allow := not ( Client is TCPUForm );
end;

procedure TMainForm.RunTestMenuItemClick(Sender: TObject);
var
  MenuItem : TMenuItem;
  AWindow : TBaseForm;
begin
  AWindow := TBaseForm.Create( Application );
  AWindow.Show;
  // dołącz okno do menu
  MenuItem := TMenuItem.Create( Self );
  MenuItem.Caption := AWindow.Caption;
  MenuItem.Tag := Integer( AWindow );
  ViewMenuItem.Add( MenuItem );
  MenuItem.OnClick := ViewWindowMenuItemClick;
  // pamiętaj o odnotowaniu zamknięcia okna
  AWindow.OnCloseEvent := DeleteWindowFromMenu;
end;

procedure TMainForm.ShowCPUForm;
begin
  if not Assigned( CPUForm ) then
    CPUForm := TCPUForm.Create( Application );
  CPUForm.ManualDock( PageControlCPU );
  CPUForm.Show;
  PageControlCPU.ActivePage.TabVisible := false;
  PageControlCPU.ActivePageIndex := 0;
  CPUForm.SetFocus;
  IOConsoleForm.ManualDock( PageControl );
end;

procedure TMainForm.ShowHint(Sender: TObject);
begin
  StatusBar.Panels[ 0 ].Text := Application.Hint;
end;

procedure TMainForm.ShowWindow(Form: TCustomForm);
var
  i : integer;
begin
  if not Assigned( Form ) then
    exit;
  if Assigned( Form.HostDockSite ) then
    if Form.HostDockSite = PageControl then
      for i := 0 to PageControl.PageCount - 1 do
        if PageControl.Pages[ i ].Controls[ 0 ] = Form then
        begin
          PageControl.ActivePageIndex := i;
          break;
        end;
  Form.SetFocus;
end;

procedure TMainForm.ViewCPUMenuItemClick(Sender: TObject);
begin
  if not Assigned( CPUForm ) then
    ShowCPUForm;
  CPUForm.SetFocus;
end;

procedure TMainForm.ViewWindowMenuItemClick(Sender: TObject);
var
  BaseForm : TBaseForm;
begin
  BaseForm := TBaseForm( ( Sender as TMenuItem ).Tag );
  ShowWindow( BaseForm );
end;

end.

unit BaseUnit;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, Vcl.Menus;

type
  TBaseCloseEvent = procedure( Sender : TObject ) of object;

type
  TBaseForm = class(TForm)
    MainMenu: TMainMenu;
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormCreate(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure FormDeactivate(Sender: TObject);
  private
    FBaseCloseEvent : TBaseCloseEvent;
  public
    property OnCloseEvent : TBaseCloseEvent read FBaseCloseEvent write FBaseCloseEvent;
  end;

var
  BaseForm: TBaseForm;

implementation

{$R *.dfm}

uses
  MainUnit;

var
  FormID : integer;

procedure TBaseForm.FormActivate(Sender: TObject);
begin
  ( Application.MainForm as TMainForm ).MainMenu.Merge( MainMenu );
end;

procedure TBaseForm.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Action := caFree;
  if Assigned( FBaseCloseEvent ) then
    FBaseCloseEvent( Self );
  FBaseCloseEvent := nil;
end;

procedure TBaseForm.FormCreate(Sender: TObject);
begin
  FBaseCloseEvent := nil;
  Inc( FormID );
  Caption := IntToStr( FormID );
end;

procedure TBaseForm.FormDeactivate(Sender: TObject);
begin
  ( Application.MainForm as TMainForm ).MainMenu.Unmerge( MainMenu );
end;

initialization
  FormID := 0;

end.

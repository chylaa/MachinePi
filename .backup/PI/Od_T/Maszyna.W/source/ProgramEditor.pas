// TODO: porządne połączenie menu - opisane w https://android.developreference.com/article/26855126/How+to+merge+two+menus+in+a+MDI+application

unit ProgramEditor;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, BaseUnit, Vcl.Menus, SynEdit,
  Vcl.ExtDlgs, Vcl.ComCtrls, System.ImageList, Vcl.ImgList,
  DebuggerUnit;

type
  TProgramEditorForm = class(TBaseForm)
    SynEdit: TSynEdit;
    MenuItemProgram: TMenuItem;
    MenuItemCompile: TMenuItem;
    MenuItemRun: TMenuItem;
    MenuItemEdit: TMenuItem;
    MenuItemUndo: TMenuItem;
    MenuItemRedo: TMenuItem;
    N1: TMenuItem;
    MenuItemCut: TMenuItem;
    MenuItemCopy: TMenuItem;
    MenuItemPaste: TMenuItem;
    MenuItemDelete: TMenuItem;
    MenuItemSelectAll: TMenuItem;
    N2: TMenuItem;
    MenuItemFind: TMenuItem;
    MenuItemFindNext: TMenuItem;
    MenuItemFindPrevious: TMenuItem;
    MenuItemReplace: TMenuItem;
    SaveTextFileDialog: TSaveTextFileDialog;
    MenuItemFile: TMenuItem;
    MenuItemSave: TMenuItem;
    StatusBar: TStatusBar;
    ImageListGutterGlyphs: TImageList;
    procedure FormCreate(Sender: TObject);
    procedure FormCloseQuery(Sender: TObject; var CanClose: Boolean);
    procedure MenuItemSaveClick(Sender: TObject);
    procedure SynEditChange(Sender: TObject);
    procedure MenuItemCompileClick(Sender: TObject);
    procedure SynEditStatusChange(Sender: TObject; Changes: TSynStatusChanges);
    procedure MenuItemRunClick(Sender: TObject);
    procedure SynEditSpecialLineColors(Sender: TObject; Line: Integer;
      var Special: Boolean; var FG, BG: TColor);
    procedure FormDestroy(Sender: TObject);
  private
    Debugger : TDebugger;
    FileSaved : Boolean;
    ProgramCompiled : Boolean;
    procedure CompileProgram( const SilentMode : Boolean );
    procedure FreeDebugger;
    procedure RefreshStatusBar;
    procedure SaveToFile;
    procedure UpdateGutterMarks;
  public
  end;

var
  ProgramEditorForm: TProgramEditorForm;

implementation

{$R *.dfm}

uses
  System.UITypes, Languages, AssemblerUnit, ControlUnit, SynHighlighterMW;

procedure TProgramEditorForm.CompileProgram( const SilentMode : Boolean );
var
  i : integer;
begin
  // TODO - wyłącz debugger
  FreeDebugger;
  if not FileSaved then
    SaveToFile;
  with TAssembler.Create do
  try
    try
      Compile( SynEdit.Lines );
      // zapisać wynik do pamięci programu
      for i := 1 to AsmLinesCount do
        Computer.RAM[ i - 1 ] := AsmLines[ i - 1 ];
      Computer.Reset;
      Computer.Refresh;
      if not SilentMode then
        ShowMessage( lsProgramCompiled );
      ProgramCompiled := true;
      // utwórz debugger
      Debugger := TDebugger.Create;
      for i := 1 to AsmLinesCount do
        Debugger.AddLine( AsmLines[ i - 1 ], TxtLines[ i - 1 ] + 1 );
      // TODO - przekaż debuggerowi wymagane informacje
      { DEBUG }
      {
      for i := 1 to AsmLinesCount do
      begin
        SynEdit.Lines.Add( IntToStr( AsmLines[ i - 1 ] ) + ' ' + IntToStr( TxtLines[ i - 1 ] + 1 ) );
      end;
      SynEdit.Refresh;
      }
    except on E : EAssemblerError do
      // ustaw się na błędzie w Memo1 - czasem nie ma numeru wiersza z błędem
      begin
        if E.LineNumber > 0 then
          with SynEdit do
          begin
            // przesuń się do odpowiedniej linii i kolumny
            CaretX := E.ColNumber;
            CaretY := E.LineNumber;
            SetFocus;
          end;
        // Nie wypisuj komunikatu o błędzie bo ten pojawi się w komunikacie o wyjątku
        //ShowMessage( Format( '%d %d : %s', [ E.LineNumber, E.ColNumber, E.Message ] ) );
        raise
      end;
    end;
  finally
    Free;
  end;
end;

procedure TProgramEditorForm.FormCloseQuery(Sender: TObject;
  var CanClose: Boolean);
begin
  inherited;
  CanClose := true;
  // if SynEdit.Modified then
  if not FileSaved then
    case MessageDlg( lsProgramTextIsModified, mtWarning, mbYesNoCancel, 0 ) of
    mrYes : SaveToFile;
    mrNo : ;
    else // mrCancel
      CanClose := false;
    end;
end;

procedure TProgramEditorForm.FormCreate(Sender: TObject);
var
  HL: TSynSampleSyn;
begin
  inherited;
  Caption := 'Program ' + Caption;
  Debugger := nil;
  FileSaved := true;
  ProgramCompiled := false;
  MenuItemFile.Caption := lsFileMenuItemCaption;
  MenuItemSave.Caption := lsSaveMenuItemCaption;
  MenuItemProgram.Caption := lsProgramMenuItemCaption;
  MenuItemCompile.Caption := lsCompileMenuItemCaption;
  MenuItemRun.Caption := lsRunMenuItemCaption;
  MenuItemEdit.Caption := lsEditMenuItemCaption;
  MenuItemUndo.Caption := lsUndoMenuItemCaption;
  MenuItemRedo.Caption := lsRedoMenuItemCaption;
  MenuItemCut.Caption := lsCutMenuItemCaption;
  MenuItemCopy.Caption := lsCopyMenuItemCaption;
  MenuItemPaste.Caption := lsPasteMenuItemCaption;
  MenuItemDelete.Caption := lsDeleteMenuItemCaption;
  MenuItemSelectAll.Caption := lsSelectAllMenuItemCaption;
  MenuItemFind.Caption := lsFindMenuItemCaption;
  MenuItemFindNext.Caption := lsFindNextMenuItemCaption;
  MenuItemFindPrevious.Caption := lsFindPreviousMenuItemCaption;
  MenuItemReplace.Caption := lsReplaceMenuItemCaption;
  StatusBar.SimplePanel := true;
  RefreshStatusBar;

  HL := TSynSampleSyn.Create(Self);
  SynEdit.Highlighter := HL;
end;

procedure TProgramEditorForm.FormDestroy(Sender: TObject);
begin
  FreeDebugger;
  inherited;
end;

procedure TProgramEditorForm.FreeDebugger;
begin
  if Assigned( Debugger ) then
  begin
    Debugger.Free;
    Debugger := nil;
  end;
end;

procedure TProgramEditorForm.MenuItemCompileClick(Sender: TObject);
begin
  inherited;
  CompileProgram( false );
  UpdateGutterMarks;
  SynEdit.Invalidate;
end;

procedure TProgramEditorForm.MenuItemRunClick(Sender: TObject);
begin
  inherited;
  if not ProgramCompiled then
  begin
    CompileProgram( true );
    UpdateGutterMarks;
  end;
  ShowMessage( 'Do zrobienia wykonywanie programu' );
  // TODO -- wykonaj program wraz z wizualizacją
  // na razie wykonuje pojedynczy rozkaz

  SynEdit.Invalidate;
end;

procedure TProgramEditorForm.MenuItemSaveClick(Sender: TObject);
begin
  inherited;
  SaveToFile;
end;

procedure TProgramEditorForm.RefreshStatusBar;
begin
  if FileSaved then
    StatusBar.SimpleText := Format( '%5d %5d', [ SynEdit.CaretY, SynEdit.CaretX ] )
  else
    StatusBar.SimpleText := Format( '%5d %5d Text modified', [ SynEdit.CaretY, SynEdit.CaretX ] );
end;

procedure TProgramEditorForm.SaveToFile;
begin
  if ( SaveTextFileDialog.FileName <> '' ) or SaveTextFileDialog.Execute then
  begin
    SynEdit.Lines.SaveToFile( SaveTextFileDialog.FileName );
    Caption := SaveTextFileDialog.FileName;
    FileSaved := true;
    RefreshStatusBar;
  end
end;

procedure TProgramEditorForm.SynEditChange(Sender: TObject);
begin
  inherited;
  FileSaved := false;
  ProgramCompiled := false;
end;

procedure TProgramEditorForm.SynEditSpecialLineColors(Sender: TObject;
  Line: Integer; var Special: Boolean; var FG, BG: TColor);
begin
  inherited;
  if Assigned( Debugger ) then
  begin
    if Debugger.CanGotoCursor( Line ) then
      // sprawdź, czy to bieżąca linia, jeśli tak podświetl ją
      if Debugger.CurrentLine = Line then
      begin
        Special := true;
        FG := clWhite;
        BG := clBlue;
      end
      else
      begin
        Special := true;
        FG := SynEdit.Font.Color;
        BG := SynEdit.Color;
      end;
  end;
end;
{
procedure TSimpleIDEMainForm.SynEditorSpecialLineColors(Sender: TObject;
  Line: Integer; var Special: Boolean; var FG, BG: TColor);
var
  LI: TDebuggerLineInfos;
begin
  if FDebugger <> nil then begin
    LI := FDebugger.GetLineInfos(Line);
    if dlCurrentLine in LI then begin
      Special := TRUE;
      FG := clWhite;
      BG := clBlue;
    end else if dlBreakpointLine in LI then begin
      Special := TRUE;
      FG := clWhite;
      if dlExecutableLine in LI then
        BG := clRed
      else
        BG := clGray;
    end;
  end;
end;
}

procedure TProgramEditorForm.SynEditStatusChange(Sender: TObject;
  Changes: TSynStatusChanges);
begin
  inherited;
  RefreshStatusBar;
end;

procedure TProgramEditorForm.UpdateGutterMarks;
var
  i : integer;
  Mark : TSynEditMark;
begin
  // TODO
  if not Assigned( Debugger ) then
    exit;
  SynEdit.Marks.Clear;
  for i := 0 to SynEdit.Lines.Count - 1 do
    if Debugger.CanGotoCursor( i + 1 ) then
    begin
      Mark := TSynEditMark.Create( SynEdit );
      with Mark do
      begin
        Line := i + 1;
        Char := 1;
        ImageIndex := 0;
        Visible := true;
        InternalImage := false;
      end;
      SynEdit.Marks.Place( Mark );
    end;
end;

end.

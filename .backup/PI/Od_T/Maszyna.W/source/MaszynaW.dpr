program MaszynaW;

uses
  Vcl.Forms,
  MainUnit in 'MainUnit.pas' {MainForm},
  Languages in 'Languages.pas',
  VersionUtils in 'VersionUtils.pas',
  BaseUnit in 'BaseUnit.pas' {BaseForm},
  ControlUnit in 'ControlUnit.pas',
  Microcontroller in 'Microcontroller.pas',
  Compiler in 'Compiler.pas',
  DebugUnit in 'DebugUnit.pas' {DebugForm},
  AssemblerUnit in 'AssemblerUnit.pas',
  VisualControls in 'VisualControls.pas',
  CPUView in 'CPUView.pas' {CPUForm},
  CPUConfig in 'CPUConfig.pas' {CPUConfigForm},
  IODevices in 'IODevices.pas' {IOConsoleForm},
  ProgramEditor in 'ProgramEditor.pas' {ProgramEditorForm},
  DebuggerUnit in 'DebuggerUnit.pas',
  SynHighlighterMW in 'SynHighlighterMW.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.Title := 'Maszyna W';
  Application.CreateForm(TMainForm, MainForm);
  Application.CreateForm(TCPUForm, CPUForm);
  Application.CreateForm(TIOConsoleForm, IOConsoleForm);
  Application.Run;
end.

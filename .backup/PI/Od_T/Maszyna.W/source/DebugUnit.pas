unit DebugUnit;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, BaseUnit, Vcl.Menus, Vcl.StdCtrls;

type
  TDebugForm = class(TBaseForm)
    Memo1: TMemo;
    GetItemButton: TButton;
    Label1: TLabel;
    procedure GetItemButtonClick(Sender: TObject);
  private
    //FCompiler : TCompiler;
  public
  end;

var
  DebugForm: TDebugForm;

implementation

{$R *.dfm}

uses
  System.Character, Languages, ControlUnit, Microcontroller, AssemblerUnit;


procedure TDebugForm.GetItemButtonClick(Sender: TObject);
var
  i : integer;
begin
  inherited;
  // Controller.CompileInstruction( Memo1.Lines );
  with TAssembler.Create do
  try
    try
      Compile( Memo1.Lines );
      Label1.Caption := lsProgramCompiled;
      // zapisać wynik do pamięci programu
      for i := 1 to AsmLinesCount do
      begin
        Computer.RAM[ i - 1 ] := AsmLines[ i - 1 ];
      end;
      Computer.Reset;
      Computer.Refresh;
    except on E : EAssemblerError do
      // ustaw się na błędzie w Memo1 - czasem nie ma numeru wiersza z błędem
      begin
        if E.LineNumber > 0 then
          with Memo1 do
          begin
            // przesuń się do odpowiedniej linii i kolumny
            SelStart := Perform(EM_LINEINDEX, E.LineNumber - 1, 0) + E.ColNumber - 2;
            Memo1.SelLength := 0;
            Perform(EM_SCROLLCARET, 0, 0);
            Memo1.SetFocus;
          end;
        Label1.Caption := Format( '%d %d : %s', [ E.LineNumber, E.ColNumber, E.Message ] );
        raise
      end;
    end;
  finally
    Free;
  end;
end;



{ UWAGA: Przy pokazywaniu pozycji z asemblera należy jeszcze wrócić o 1 znak !!!!
 ustawianie pozycji kursora w TMemo
  with Memo1 do
  begin
    // Move to FRow line, Character FColumn
    SelStart := Perform(EM_LINEINDEX, FRow, 0) + FColumn - 1;
    Memo1.SelLength := 0;
    Perform(EM_SCROLLCARET, 0, 0);
    Memo1.SetFocus;
  end;
}

end.

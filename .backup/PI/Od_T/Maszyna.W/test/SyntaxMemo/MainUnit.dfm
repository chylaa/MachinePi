object Form1: TForm1
  Left = 0
  Top = 0
  Caption = 'Form1'
  ClientHeight = 398
  ClientWidth = 715
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 0
    Top = 0
    Width = 715
    Height = 13
    Align = alTop
    Alignment = taCenter
    Caption = 'Label1'
    ExplicitLeft = 352
    ExplicitTop = 216
    ExplicitWidth = 31
  end
  object KeywordList: TListBox
    Left = 0
    Top = 13
    Width = 121
    Height = 385
    Align = alLeft
    ItemHeight = 13
    Items.Strings = (
      'begin'
      'end'
      'procedure'
      'function'
      'program'
      'unit'
      'interface'
      'implementation'
      '//'
      'uses'
      'type'
      'private'
      'protected'
      'override'
      'class')
    TabOrder = 0
    ExplicitTop = 0
    ExplicitHeight = 97
  end
  object Memo1: TMemo
    Left = 121
    Top = 13
    Width = 594
    Height = 385
    Align = alClient
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Tahoma'
    Font.Style = []
    Lines.Strings = (
      'unit MainUnit;'
      ''
      'interface'
      ''
      'uses'
      
        '  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Varia' +
        'nts, '
      'System.Classes, Vcl.Graphics,'
      
        '  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, Vcl.ComCtr' +
        'ls;'
      ''
      'type'
      '  // Interjected Class'
      '  TMemo = class(Vcl.StdCtrls.TMemo)'
      '  private'
      '    procedure WMPaint(var Message: TWMPaint); message WM_PAINT;'
      '    procedure WMSize(var Message: TWMSize); message WM_SIZE;'
      '    procedure WMMove(var Message: TWMMove); message WM_MOVE;'
      
        '    procedure WMVScroll(var Message: TWMMove); message WM_VSCROL' +
        'L;'
      '    procedure WMMousewheel(var Message: TWMMove); message '
      'WM_MOUSEWHEEL;'
      '  protected'
      '    procedure Change; override;'
      
        '    procedure KeyDown(var Key: Word; Shift: TShiftState); overri' +
        'de;'
      
        '    procedure KeyUp(var Key: Word; Shift: TShiftState); override' +
        ';'
      
        '    procedure MouseDown(Button: TMouseButton; Shift: TShiftState' +
        '; X, Y: Integer);'
      '      override;'
      
        '    procedure MouseUp(Button: TMouseButton; Shift: TShiftState; ' +
        'X, Y: Integer);'
      '      override;'
      '  public'
      '    PosLabel: TLabel;'
      '    procedure Update_label;'
      '    procedure GotoXY(mCol, mLine: Integer);'
      '    function Line: Integer;'
      '    function Col: Integer;'
      '    function TopLine: Integer;'
      '    function VisibleLines: Integer;'
      '  end;'
      ''
      ''
      ''
      ''
      'type'
      '  TForm1 = class(TForm)'
      '    KeywordList: TListBox;'
      '    Label1: TLabel;'
      '    Memo1: TMemo;'
      '    procedure FormCreate(Sender: TObject);'
      
        '    procedure Memo1KeyUp(Sender: TObject; var Key: Word; Shift: ' +
        'TShiftState);'
      
        '    procedure FormClose(Sender: TObject; var Action: TCloseActio' +
        'n);'
      '  private'
      '    { Private declarations }'
      '  public'
      '    { Public declarations }'
      '  end;'
      ''
      'var'
      '  Form1: TForm1;'
      ''
      'implementation'
      ''
      '{$R *.dfm}'
      ''
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      
        '// functions for managing keywords and numbers of each line of T' +
        'Memo ///////////'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      'function IsSeparator(Car: Char): Boolean;'
      'begin'
      '  case Car of'
      
        '    '#39'.'#39', '#39';'#39', '#39','#39', '#39':'#39', '#39#161#39', '#39'!'#39', '#39#183#39', '#39'"'#39', '#39#39#39#39', '#39'^'#39', '#39'+'#39', '#39'-'#39',' +
        ' '#39'*'#39', '#39'/'#39', '#39'\'#39', '#39#168#39', '#39' '#39','
      
        '    '#39'`'#39', '#39'['#39', '#39']'#39', '#39'('#39', '#39')'#39', '#39#186#39', '#39#170#39', '#39'{'#39', '#39'}'#39', '#39'?'#39', '#39#191#39', '#39'%'#39', ' +
        #39'='#39': Result := True;'
      '    else'
      '      Result := False;'
      '  end;'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'function NextWord(var s: string; var PrevWord: string): string;'
      'begin'
      '  Result   := '#39#39';'
      '  PrevWord := '#39#39';'
      '  if s = '#39#39' then Exit;'
      '  while (s <> '#39#39') and IsSeparator(s[1]) do'
      '  begin'
      '    PrevWord := PrevWord + s[1];'
      '    Delete(s, 1,1);'
      '  end;'
      '  while (s <> '#39#39') and not IsSeparator(s[1]) do'
      '  begin'
      '    Result := Result + s[1];'
      '    Delete(s, 1,1);'
      '  end;'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'function IsKeyWord(s: string): Boolean;'
      'begin'
      '  Result := False;'
      '  if s = '#39#39' then Exit;'
      '  Result := Form1.KeywordList.Items.IndexOf(lowercase(s)) <> -1;'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'function IsNumber(s: string): Boolean;'
      'var'
      '  i: Integer;'
      'begin'
      '  Result := False;'
      '  for i := 1 to Length(s) do'
      '    case s[i] of'
      '      '#39'0'#39'..'#39'9'#39':;'
      '      else'
      '        Exit;'
      '    end;'
      '  Result := True;'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      
        '// New or overrided methods and properties for TMemo using Inter' +
        'jected Class ///'
      
        '// Technique ///////////////////////////////////////////////////' +
        '////////////////'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'function TMemo.VisibleLines: Integer;'
      'begin'
      '  Result := Height div (Abs(Self.Font.Height) + 2);'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'procedure TMemo.GotoXY(mCol, mLine: Integer);'
      'begin'
      '  Dec(mLine);'
      '  SelStart  := 0;'
      '  SelLength := 0;'
      '  SelStart  := mCol + Self.Perform(EM_LINEINDEX, mLine, 0);'
      '  SelLength := 0;'
      '  SetFocus;'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'procedure TMemo.Update_label;'
      'begin'
      '  if PosLabel = nil then Exit;'
      
        '  PosLabel.Caption := '#39'('#39' + IntToStr(Line + 1) + '#39','#39' + IntToStr(' +
        'Col) + '#39')'#39';'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'function TMemo.TopLine: Integer;'
      'begin'
      
        '  Result := SendMessage(Self.Handle, EM_GETFIRSTVISIBLELINE, 0, ' +
        '0);'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'function TMemo.Line: Integer;'
      'begin'
      
        '  Result := SendMessage(Self.Handle, EM_LINEFROMCHAR, Self.SelSt' +
        'art, 0);'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'function TMemo.Col: Integer;'
      'begin'
      
        '  Result := Self.SelStart - SendMessage(Self.Handle, EM_LINEINDE' +
        'X,'
      '    SendMessage(Self.Handle,'
      '    EM_LINEFROMCHAR, Self.SelStart, 0), 0);'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'procedure TMemo.WMVScroll(var Message: TWMMove);'
      'begin'
      '  Update_label;'
      '  Invalidate;'
      '  inherited;'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'procedure TMemo.WMSize(var Message: TWMSize);'
      'begin'
      '  Invalidate;'
      '  inherited;'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'procedure TMemo.WMMove(var Message: TWMMove);'
      'begin'
      '  Invalidate;'
      '  inherited;'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'procedure TMemo.WMMousewheel(var Message: TWMMove);'
      'begin'
      '  Invalidate;'
      '  inherited;'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'procedure TMemo.Change;'
      'begin'
      '  Update_label;'
      '  Invalidate;'
      '  inherited Change;'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'procedure TMemo.KeyDown(var Key: Word; Shift: TShiftState);'
      'begin'
      '  Update_label;'
      '  inherited KeyDown(Key, Shift);'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'procedure TMemo.KeyUp(var Key: Word; Shift: TShiftState);'
      'begin'
      '  Update_label;'
      '  inherited KeyUp(Key, Shift);'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      
        'procedure TMemo.MouseDown(Button: TMouseButton; Shift: TShiftSta' +
        'te; X, Y: '
      'Integer);'
      'begin'
      '  Update_label;'
      '  inherited MouseDown(Button, Shift, X, Y);'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      
        'procedure TMemo.MouseUp(Button: TMouseButton; Shift: TShiftState' +
        '; X, Y: Integer);'
      'begin'
      '  Update_label;'
      '  inherited MouseUp(Button, Shift, X, Y);'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      'procedure TMemo.WMPaint(var Message: TWMPaint);'
      'var'
      '  PS: TPaintStruct;'
      '  DC: HDC;'
      '  Canvas: TCanvas;'
      '  i: Integer;'
      '  X, Y: Integer;'
      '  OldColor: TColor;'
      '  Size: TSize;'
      '  Max: Integer;'
      '  s, Palabra, PrevWord: string;'
      'begin'
      '  DC := Message.DC;'
      '  if DC = 0 then DC := BeginPaint(Handle, PS);'
      '  Canvas := TCanvas.Create;'
      '  try'
      '    OldColor         := Font.Color;'
      '    Canvas.Handle    := DC;'
      '    Canvas.Font.Name := Font.Name;'
      '    Canvas.Font.Size := Font.Size;'
      '    with Canvas do'
      '    begin'
      '      Max := TopLine + VisibleLines;'
      '      if Max > Pred(Lines.Count) then Max := Pred(Lines.Count);'
      ''
      '      //Limpio la secci'#243'n visible'
      '      Brush.Color := Self.Color;'
      '      FillRect(Self.ClientRect);'
      '      Y := 1;'
      '      for i := TopLine to Max do'
      '      begin'
      '        X := 2;'
      '        s := Lines[i];'
      ''
      '        //Detecto todas las palabras de esta l'#237'nea'
      '        Palabra := NextWord(s, PrevWord);'
      '        while Palabra <> '#39#39' do'
      '        begin'
      '          Font.Color := OldColor;'
      '          TextOut(X, Y, PrevWord);'
      
        '          GetTextExtentPoint32(DC, PChar(PrevWord), Length(PrevW' +
        'ord), Size);'
      '          Inc(X, Size.cx);'
      ''
      '          Font.Color := clBlack;'
      '          if IsKeyWord(Palabra) then'
      '          begin'
      '            Font.Color := clHighlight;'
      '            TextOut(X, Y, Palabra);'
      '             {'
      '             //Draw dot underline'
      '             Pen.Color := clHighlight;'
      '             Pen.Style := psDot;'
      
        '             PolyLine([ Point(X,Y+13), Point(X+TextWidth(Palabra' +
        '),Y+13)]);'
      '             }'
      '          end'
      '          else if IsNumber(Palabra) then'
      '          begin'
      '            Font.Color := $000000DD;'
      '            TextOut(X, Y, Palabra);'
      '          end'
      '          else'
      '            TextOut(X, Y, Palabra);'
      ''
      
        '          GetTextExtentPoint32(DC, PChar(Palabra), Length(Palabr' +
        'a), Size);'
      '          Inc(X, Size.cx);'
      ''
      '          Palabra := NextWord(s, PrevWord);'
      '          if (s = '#39#39') and (PrevWord <> '#39#39') then'
      '          begin'
      '            Font.Color := OldColor;'
      '            TextOut(X, Y, PrevWord);'
      '          end;'
      '        end;'
      '        if (s = '#39#39') and (PrevWord <> '#39#39') then'
      '        begin'
      '          Font.Color := OldColor;'
      '          TextOut(X, Y, PrevWord);'
      '        end;'
      ''
      '        s := '#39'W'#39';'
      '        GetTextExtentPoint32(DC, PChar(s), Length(s), Size);'
      '        Inc(Y, Size.cy);'
      '      end;'
      '    end;'
      '  finally'
      '    if Message.DC = 0 then EndPaint(Handle, PS);'
      '  end;'
      '  Canvas.Free;'
      '  inherited;'
      'end;'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      
        '////////////////////////////////////////////////////////////////' +
        '////////////////'
      ''
      ''
      ''
      
        'procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAc' +
        'tion);'
      'begin'
      '  Action := caFree;'
      'end;'
      ''
      'procedure TForm1.FormCreate(Sender: TObject);'
      'begin'
      '  Memo1.PosLabel := Label1;'
      '  Memo1.Update_label;'
      'end;'
      ''
      
        'procedure TForm1.Memo1KeyUp(Sender: TObject; var Key: Word; Shif' +
        't: TShiftState);'
      'begin'
      '  if Key = VK_F1 then Memo1.Invalidate;'
      'end;'
      ''
      'end.')
    ParentFont = False
    TabOrder = 1
    OnKeyUp = Memo1KeyUp
    ExplicitLeft = 272
    ExplicitTop = 176
    ExplicitWidth = 185
    ExplicitHeight = 89
  end
end
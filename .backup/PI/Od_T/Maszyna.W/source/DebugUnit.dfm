inherited DebugForm: TDebugForm
  Caption = 'Debug window'
  ClientHeight = 480
  ClientWidth = 514
  ExplicitWidth = 530
  ExplicitHeight = 519
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel [0]
    Left = 104
    Top = 437
    Width = 31
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Label1'
    ExplicitTop = 432
  end
  object Memo1: TMemo [1]
    Left = 8
    Top = 8
    Width = 498
    Height = 418
    Anchors = [akLeft, akTop, akRight, akBottom]
    Lines.Strings = (
      '// komentarz'
      #9'pob a // komentarz'
      #9'dod b'#9'// komentarz'
      #9#322'ad c'
      '// komentarz'
      #9'stp'
      'a:'#9'rst 5'
      'c:'#9'rpa'
      'spacja:'#9'rst '#39' '#39
      'etyk:'
      'b:'#9'rst 3')
    ScrollBars = ssBoth
    TabOrder = 0
  end
  object GetItemButton: TButton [2]
    Left = 8
    Top = 432
    Width = 75
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Compile'
    TabOrder = 1
    OnClick = GetItemButtonClick
  end
end

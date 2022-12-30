inherited IOConsoleForm: TIOConsoleForm
  Caption = 'IOConsoleForm'
  OnDestroy = FormDestroy
  ExplicitHeight = 290
  PixelsPerInch = 96
  TextHeight = 13
  object GroupBoxInput: TGroupBox [0]
    Left = 0
    Top = 0
    Width = 505
    Height = 65
    Align = alTop
    Caption = 'GroupBoxInput'
    TabOrder = 0
    DesignSize = (
      505
      65)
    object EditInput: TEdit
      Left = 16
      Top = 24
      Width = 473
      Height = 26
      Anchors = [akLeft, akTop, akRight]
      Font.Charset = EASTEUROPE_CHARSET
      Font.Color = clWindowText
      Font.Height = -15
      Font.Name = 'Consolas'
      Font.Style = []
      ParentFont = False
      TabOrder = 0
      OnChange = EditInputChange
    end
  end
  object GroupBoxOutput: TGroupBox [1]
    Left = 0
    Top = 65
    Width = 505
    Height = 166
    Align = alClient
    Caption = 'GroupBoxOutput'
    TabOrder = 1
    DesignSize = (
      505
      166)
    object MemoOutput: TMemo
      Left = 8
      Top = 24
      Width = 481
      Height = 129
      Anchors = [akLeft, akTop, akRight, akBottom]
      Font.Charset = EASTEUROPE_CHARSET
      Font.Color = clWindowText
      Font.Height = -15
      Font.Name = 'Consolas'
      Font.Style = []
      ParentFont = False
      ReadOnly = True
      ScrollBars = ssBoth
      TabOrder = 0
    end
  end
  inherited MainMenu: TMainMenu
    Left = 448
    object MenuItemTools: TMenuItem
      Caption = 'Tools'
      GroupIndex = 8
      object MenuItemIOFont: TMenuItem
        Caption = 'Font'
        OnClick = MenuItemIOFontClick
      end
      object MenuItemClearOutput: TMenuItem
        Caption = 'Clear output'
        OnClick = MenuItemClearOutputClick
      end
    end
  end
  object FontDialog: TFontDialog
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    Left = 392
    Top = 16
  end
end

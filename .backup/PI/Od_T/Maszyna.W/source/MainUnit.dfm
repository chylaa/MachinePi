object MainForm: TMainForm
  Left = 0
  Top = 0
  Caption = 'MainForm'
  ClientHeight = 440
  ClientWidth = 986
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  Menu = MainMenu
  OldCreateOrder = False
  WindowState = wsMaximized
  OnCreate = FormCreate
  OnResize = FormResize
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object SplitterHorizontal: TSplitter
    Left = 840
    Top = 0
    Height = 421
    ExplicitLeft = 392
    ExplicitTop = 208
    ExplicitHeight = 100
  end
  object StatusBar: TStatusBar
    Left = 0
    Top = 421
    Width = 986
    Height = 19
    Panels = <
      item
        Text = 'HintPanel'
        Width = 500
      end
      item
        Text = 'Version'
        Width = 100
      end>
    ParentShowHint = False
    ShowHint = False
    ExplicitTop = 426
  end
  object PanelLeft: TPanel
    Left = 0
    Top = 0
    Width = 840
    Height = 421
    Align = alLeft
    BevelOuter = bvLowered
    Caption = ' '
    TabOrder = 1
    object SplitterVertical: TSplitter
      Left = 1
      Top = 307
      Width = 838
      Height = 3
      Cursor = crVSplit
      Align = alTop
      ExplicitLeft = -1
      ExplicitTop = 310
      ExplicitWidth = 607
    end
    object PageControl: TPageControl
      Left = 1
      Top = 310
      Width = 838
      Height = 110
      Align = alClient
      DockSite = True
      TabOrder = 0
      TabPosition = tpBottom
      ExplicitLeft = -1
      ExplicitTop = 313
    end
    object PageControlCPU: TPageControl
      Left = 1
      Top = 1
      Width = 838
      Height = 306
      Align = alTop
      TabOrder = 1
      OnGetSiteInfo = PageControlCPUGetSiteInfo
      OnUnDock = PageControlCPUUnDock
      ExplicitLeft = -1
      ExplicitTop = -2
    end
  end
  object PanelRight: TPanel
    Left = 843
    Top = 0
    Width = 143
    Height = 421
    Align = alClient
    BevelOuter = bvLowered
    Caption = ' '
    DockSite = True
    TabOrder = 2
    ExplicitLeft = 845
    ExplicitTop = -6
  end
  object MainMenu: TMainMenu
    Left = 32
    Top = 24
    object FileMenuItem: TMenuItem
      Caption = '&File'
      GroupIndex = 1
      object MenuItemFileNew: TMenuItem
        Caption = 'New'
        GroupIndex = 1
        object MenuItemNewProgram: TMenuItem
          Caption = 'Program'
          OnClick = MenuItemNewProgramClick
        end
      end
      object ExitMenuItem: TMenuItem
        Caption = 'Exit'
        GroupIndex = 10
        OnClick = ExitMenuItemClick
      end
    end
    object ViewMenuItem: TMenuItem
      Caption = 'View'
      GroupIndex = 3
      object ViewCPUMenuItem: TMenuItem
        Caption = 'CPU'
        OnClick = ViewCPUMenuItemClick
      end
      object MenuItemViewIO: TMenuItem
        Caption = 'IO Console'
        OnClick = MenuItemViewIOClick
      end
      object MenuItemLine1: TMenuItem
        Caption = '-'
      end
    end
    object MenuItemProcessor: TMenuItem
      Caption = 'Processor'
      GroupIndex = 5
      object MenuItemCPUConfiguration: TMenuItem
        Caption = 'Configuration'
        OnClick = MenuItemCPUConfigurationClick
      end
    end
    object MenuItemRun: TMenuItem
      Caption = 'Run'
      GroupIndex = 7
      object MenuItemRunCycle: TMenuItem
        Caption = 'Cycle'
        OnClick = MenuItemRunCycleClick
      end
    end
    object TestMenuItem: TMenuItem
      Caption = 'Test'
      GroupIndex = 9
      object RunTestMenuItem: TMenuItem
        Caption = 'New window'
        OnClick = RunTestMenuItemClick
      end
      object CodeTestMenuItem: TMenuItem
        Caption = 'Code'
        OnClick = CodeTestMenuItemClick
      end
    end
  end
end

object CPUConfigForm: TCPUConfigForm
  Left = 0
  Top = 0
  BorderStyle = bsDialog
  Caption = 'CPUConfigForm'
  ClientHeight = 393
  ClientWidth = 573
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poMainFormCenter
  OnActivate = FormActivate
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object PageControl1: TPageControl
    Left = 8
    Top = 8
    Width = 553
    Height = 337
    ActivePage = TabSheetArchitecture
    TabOrder = 0
    object TabSheetArchitecture: TTabSheet
      Caption = 'Architecture'
      object GroupBoxMachineWord: TGroupBox
        Left = 48
        Top = 40
        Width = 441
        Height = 137
        Caption = 'Word'
        TabOrder = 0
        object LabelAddressLength: TLabel
          Left = 32
          Top = 40
          Width = 97
          Height = 13
          Caption = 'LabelAddressLength'
        end
        object LabelCodeLength: TLabel
          Left = 32
          Top = 80
          Width = 83
          Height = 13
          Caption = 'LabelCodeLength'
        end
        object SpinEditAddressLength: TSpinEdit
          Left = 160
          Top = 37
          Width = 41
          Height = 22
          MaxValue = 11
          MinValue = 5
          TabOrder = 0
          Value = 5
        end
        object SpinEditCodeLength: TSpinEdit
          Left = 160
          Top = 77
          Width = 41
          Height = 22
          MaxValue = 5
          MinValue = 3
          TabOrder = 1
          Value = 3
        end
      end
    end
    object TabSheetComponents: TTabSheet
      Caption = 'Components'
      ImageIndex = 1
      object RadioGroupType: TRadioGroup
        Left = 48
        Top = 40
        Width = 441
        Height = 57
        Caption = 'RadioGroupType'
        Columns = 4
        Items.Strings = (
          'W'
          'W+'
          'L'
          'EW')
        TabOrder = 0
        OnClick = RadioGroupTypeClick
      end
      object CheckListBox: TCheckListBox
        Left = 48
        Top = 112
        Width = 441
        Height = 169
        OnClickCheck = CheckListBoxClickCheck
        ItemHeight = 13
        TabOrder = 1
      end
    end
    object TabSheetAddresses: TTabSheet
      Caption = 'Addresses'
      ImageIndex = 2
      object GroupBoxInterrupts: TGroupBox
        Left = 56
        Top = 33
        Width = 441
        Height = 145
        Caption = 'Interrupt handlers'
        TabOrder = 0
        object Label1: TLabel
          Left = 48
          Top = 35
          Width = 6
          Height = 13
          Caption = '1'
        end
        object Label2: TLabel
          Left = 48
          Top = 62
          Width = 6
          Height = 13
          Caption = '2'
        end
        object Label4: TLabel
          Left = 48
          Top = 116
          Width = 6
          Height = 13
          Caption = '4'
        end
        object Label3: TLabel
          Left = 48
          Top = 89
          Width = 6
          Height = 13
          Caption = '3'
        end
        object LabelLabels: TLabel
          Left = 104
          Top = 14
          Width = 30
          Height = 13
          Caption = 'Labels'
        end
        object LabelAddresses: TLabel
          Left = 272
          Top = 14
          Width = 44
          Height = 13
          Caption = 'Adresses'
        end
        object EditLabel1: TEdit
          Left = 104
          Top = 32
          Width = 121
          Height = 21
          TabOrder = 0
          Text = 'AP1'
        end
        object EditLabel2: TEdit
          Left = 104
          Top = 59
          Width = 121
          Height = 21
          TabOrder = 1
          Text = 'AP2'
        end
        object EditLabel3: TEdit
          Left = 104
          Top = 86
          Width = 121
          Height = 21
          TabOrder = 2
          Text = 'AP3'
        end
        object EditLabel4: TEdit
          Left = 104
          Top = 113
          Width = 121
          Height = 21
          TabOrder = 3
          Text = 'AP4'
        end
        object EditAddress1: TEdit
          Left = 272
          Top = 32
          Width = 121
          Height = 21
          TabOrder = 4
          Text = '0'
          OnExit = EditAddressExit
        end
        object EditAddress2: TEdit
          Left = 272
          Top = 59
          Width = 121
          Height = 21
          TabOrder = 5
          Text = '0'
          OnExit = EditAddressExit
        end
        object EditAddress3: TEdit
          Left = 272
          Top = 86
          Width = 121
          Height = 21
          TabOrder = 6
          Text = '0'
          OnExit = EditAddressExit
        end
        object EditAddress4: TEdit
          Left = 272
          Top = 113
          Width = 121
          Height = 21
          TabOrder = 7
          Text = '0'
          OnExit = EditAddressExit
        end
      end
      object GroupBoxIODevices: TGroupBox
        Left = 56
        Top = 208
        Width = 441
        Height = 65
        Caption = 'GroupBoxIODevices'
        TabOrder = 1
        object LabeledEditInput: TLabeledEdit
          Left = 88
          Top = 24
          Width = 121
          Height = 21
          EditLabel.Width = 26
          EditLabel.Height = 13
          EditLabel.Caption = 'Input'
          LabelPosition = lpLeft
          LabelSpacing = 5
          TabOrder = 0
          Text = '1'
          OnExit = LabeledEditInputExit
        end
        object LabeledEditOutput: TLabeledEdit
          Left = 272
          Top = 24
          Width = 121
          Height = 21
          EditLabel.Width = 34
          EditLabel.Height = 13
          EditLabel.Caption = 'Output'
          LabelPosition = lpLeft
          LabelSpacing = 5
          TabOrder = 1
          Text = '2'
          OnExit = LabeledEditInputExit
        end
      end
    end
  end
  object ButtonOK: TButton
    Left = 189
    Top = 359
    Width = 75
    Height = 25
    Caption = 'OK'
    ModalResult = 1
    TabOrder = 1
    OnClick = ButtonOKClick
  end
  object ButtonCancel: TButton
    Left = 309
    Top = 359
    Width = 75
    Height = 25
    Cancel = True
    Caption = 'Cancel'
    ModalResult = 2
    TabOrder = 2
  end
end

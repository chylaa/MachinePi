inherited CPUForm: TCPUForm
  Hint = 'CPU'
  Caption = 'CPUForm'
  ClientHeight = 452
  ClientWidth = 829
  DragMode = dmManual
  OnPaint = FormPaint
  ExplicitWidth = 845
  ExplicitHeight = 491
  PixelsPerInch = 96
  TextHeight = 13
  object RegisterA: TRegister [0]
    Left = 508
    Top = 140
    Width = 137
    Height = 21
    Hint = 'Address register'
    Length = 5
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcA
  end
  object RegisterS: TRegister [1]
    Left = 508
    Top = 331
    Width = 137
    Height = 21
    Hint = 'Data register'
    Length = 8
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcS
  end
  object RegisterL: TRegister [2]
    Left = 8
    Top = 140
    Width = 137
    Height = 21
    Hint = 'Program counter'
    Length = 5
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcL
  end
  object RegisterAK: TRegister [3]
    Left = 273
    Top = 222
    Width = 137
    Height = 21
    Hint = 'Accumulator'
    Length = 8
    NumeralSystem = nsSiged
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcAK
  end
  object RegisterI: TRegister [4]
    Left = 44
    Top = 331
    Width = 137
    Height = 21
    Hint = 'Instruction register'
    Length = 8
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcI
  end
  object BusS: TBus [5]
    Left = 8
    Top = 386
    Width = 637
    Height = 5
    Hint = 'Data bus'
    Active = False
  end
  object BusA: TBus [6]
    Left = 8
    Top = 96
    Width = 637
    Height = 5
    Hint = 'Address bus'
    Active = False
  end
  object BusAS: TBus [7]
    Left = 208
    Top = 100
    Width = 5
    Height = 287
    Hint = 'Interbus connection'
    Active = False
    Visible = False
  end
  object RegisterRZ: TRegister [8]
    Left = 8
    Top = 44
    Width = 137
    Height = 21
    Hint = 'Interrupt request register'
    Length = 4
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcRZ
    Visible = False
  end
  object RegisterRM: TRegister [9]
    Left = 174
    Top = 44
    Width = 137
    Height = 21
    Hint = 'Interrupt mask register'
    Length = 4
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcRM
    Visible = False
  end
  object RegisterRP: TRegister [10]
    Left = 341
    Top = 44
    Width = 137
    Height = 21
    Hint = 'In-service register'
    Length = 4
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcRP
    Visible = False
  end
  object RegisterAP: TRegister [11]
    Left = 508
    Top = 44
    Width = 137
    Height = 21
    Hint = 'Interrupt handler register'
    Length = 5
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcAP
    Visible = False
  end
  object RegisterX: TRegister [12]
    Left = 8
    Top = 423
    Width = 137
    Height = 21
    Hint = 'Register X'
    Length = 8
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcX
    Visible = False
  end
  object RegisterY: TRegister [13]
    Left = 193
    Top = 423
    Width = 137
    Height = 21
    Hint = 'Register Y'
    Length = 8
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcY
    Visible = False
  end
  object RegisterRB: TRegister [14]
    Left = 378
    Top = 423
    Width = 137
    Height = 21
    Hint = 'IO buffer'
    Length = 8
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcRB
    Visible = False
  end
  object RegisterG: TRegister [15]
    Left = 563
    Top = 423
    Width = 50
    Height = 21
    Hint = 'IO ready'
    Length = 1
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcG
    Visible = False
  end
  object RegisterWS: TRegister [16]
    Left = 273
    Top = 140
    Width = 137
    Height = 21
    Hint = 'Stack pointer'
    Length = 5
    NumeralSystem = nsUnsigned
    OnDblClick = RegisterDblClick
    PopupMenu = PopupMenuRegister
    RegisterCode = rcWS
    Visible = False
  end
  object SpeedButton1: TSpeedButton [17]
    Tag = 8
    Left = 6
    Top = 8
    Width = 31
    Height = 30
    Hint = 'Interrupt request (level 1)'
    Caption = '1'
    Visible = False
    OnClick = SpeedButtonClick
  end
  object SpeedButton2: TSpeedButton [18]
    Tag = 4
    Left = 43
    Top = 8
    Width = 31
    Height = 30
    Hint = 'Interrupt request (level 2)'
    Caption = '2'
    Visible = False
    OnClick = SpeedButtonClick
  end
  object SpeedButton3: TSpeedButton [19]
    Tag = 2
    Left = 78
    Top = 8
    Width = 31
    Height = 30
    Hint = 'Interrupt request(level  3)'
    Caption = '3'
    Visible = False
    OnClick = SpeedButtonClick
  end
  object SpeedButton4: TSpeedButton [20]
    Tag = 1
    Left = 114
    Top = 8
    Width = 31
    Height = 30
    Hint = 'Interrupt request (level 4)'
    Caption = '4'
    Visible = False
    OnClick = SpeedButtonClick
  end
  object ControlSignalStop: TControlSignal [21]
    Left = 78
    Top = 280
    Width = 50
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgStop
  end
  object ControlSignalWyl: TControlSignal [22]
    Left = 9
    Top = 114
    Width = 50
    Height = 20
    Hint = 'Read program counter'
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgWyl
  end
  object ControlSignalWel: TControlSignal [23]
    Left = 95
    Top = 114
    Width = 50
    Height = 20
    Hint = 'Write to program counter'
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWel
  end
  object ControlSignalIl: TControlSignal [24]
    Left = 51
    Top = 161
    Width = 50
    Height = 33
    Hint = 'Increment program counter'
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soUp
    Signal = sgIl
  end
  object ControlSignalWyls: TControlSignal [25]
    Left = 24
    Top = 207
    Width = 50
    Height = 20
    Hint = 'Read program counter into data bus'
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWyls
    Visible = False
  end
  object ControlSignalWyad: TControlSignal [26]
    Left = 116
    Top = 240
    Width = 50
    Height = 20
    Hint = 'Read address from intruction register'
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgWyAD
  end
  object ControlSignalWei: TControlSignal [27]
    Left = 112
    Top = 358
    Width = 50
    Height = 20
    Hint = 'Write to instrction register'
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWei
  end
  object ControlSignalWes: TControlSignal [28]
    Left = 595
    Top = 360
    Width = 50
    Height = 20
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWes
  end
  object ControlSignalWeja: TControlSignal [29]
    Left = 337
    Top = 360
    Width = 50
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWeja
  end
  object ControlSignalWys: TControlSignal [30]
    Left = 509
    Top = 360
    Width = 50
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgWys
  end
  object ControlSignalWea: TControlSignal [31]
    Left = 573
    Top = 114
    Width = 50
    Height = 20
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWea
  end
  object ControlSignalWews: TControlSignal [32]
    Left = 360
    Top = 114
    Width = 50
    Height = 20
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWeWs
    Visible = False
  end
  object ControlSignalWyws: TControlSignal [33]
    Left = 273
    Top = 114
    Width = 50
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgWyWS
    Visible = False
  end
  object ControlSignalCzyt: TControlSignal [34]
    Left = 460
    Top = 174
    Width = 50
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgCzyt
  end
  object ControlSignalPisz: TControlSignal [35]
    Left = 460
    Top = 200
    Width = 50
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgPisz
  end
  object ControlSignalWyap: TControlSignal [36]
    Left = 573
    Top = 71
    Width = 50
    Height = 20
    Hint = 'Read interrupt handler address'
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWyap
    Visible = False
  end
  object ControlSignalWyrm: TControlSignal [37]
    Left = 177
    Top = 70
    Width = 50
    Height = 20
    Hint = 'Read mask register'
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgWyrm
    Visible = False
  end
  object ControlSignalWerm: TControlSignal [38]
    Left = 257
    Top = 70
    Width = 50
    Height = 20
    Hint = 'Write to mask register'
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWerm
    Visible = False
  end
  object ControlSignalEni: TControlSignal [39]
    Left = 357
    Top = 10
    Width = 50
    Height = 33
    Hint = 'Enable interrupt'
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soDown
    Signal = sgEni
    Visible = False
  end
  object ControlSignalRint: TControlSignal [40]
    Left = 413
    Top = 10
    Width = 50
    Height = 33
    Hint = 'Reset interrupt'
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soDown
    Signal = sgRint
    Visible = False
  end
  object ControlSignalWyx: TControlSignal [41]
    Left = 91
    Top = 397
    Width = 50
    Height = 20
    Hint = 'Read X register'
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWyx
    Visible = False
  end
  object ControlSignalWex: TControlSignal [42]
    Left = 11
    Top = 397
    Width = 50
    Height = 20
    Hint = 'Write to X register'
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgWex
    Visible = False
  end
  object ControlSignalWey: TControlSignal [43]
    Left = 196
    Top = 397
    Width = 50
    Height = 20
    Hint = 'Write to Y register'
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgWey
    Visible = False
  end
  object ControlSignalWyy: TControlSignal [44]
    Left = 274
    Top = 397
    Width = 50
    Height = 20
    Hint = 'Read Y register'
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWyy
    Visible = False
  end
  object ControlSignalWeRB: TControlSignal [45]
    Left = 380
    Top = 397
    Width = 50
    Height = 20
    Hint = 'Write to IO buffer'
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgWerb
    Visible = False
  end
  object ControlSignalWyRB: TControlSignal [46]
    Left = 460
    Top = 397
    Width = 50
    Height = 20
    Hint = 'Read IO buffer'
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWyrb
    Visible = False
  end
  object ControlSignalStart: TControlSignal [47]
    Left = 527
    Top = 397
    Width = 50
    Height = 20
    Hint = 'Start IO cycle'
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgStart
    Visible = False
  end
  object ControlSignalWyG: TControlSignal [48]
    Left = 602
    Top = 397
    Width = 50
    Height = 20
    Hint = 'Read IO status (ready or not)'
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgWyG
    Visible = False
  end
  object ControlSignalDod: TControlSignal [49]
    Left = 227
    Top = 265
    Width = 46
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgDod
  end
  object ControlSignalOde: TControlSignal [50]
    Left = 227
    Top = 282
    Width = 46
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgOde
  end
  object ControlSignalPrzep: TControlSignal [51]
    Left = 227
    Top = 298
    Width = 46
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgPrzep
  end
  object ControlSignalWeak: TControlSignal [52]
    Left = 223
    Top = 222
    Width = 50
    Height = 20
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgWeak
  end
  object ControlSignalWyak: TControlSignal [53]
    Left = 285
    Top = 196
    Width = 50
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgWyak
  end
  object ControlSignalIws: TControlSignal [54]
    Left = 223
    Top = 140
    Width = 50
    Height = 20
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgIws
    Visible = False
  end
  object ControlSignalDws: TControlSignal [55]
    Left = 411
    Top = 140
    Width = 50
    Height = 20
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgDws
    Visible = False
  end
  object ControlSignalMno: TControlSignal [56]
    Left = 227
    Top = 315
    Width = 46
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgMnoz
    Visible = False
  end
  object ControlSignalIAk: TControlSignal [57]
    Left = 227
    Top = 249
    Width = 46
    Height = 20
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgIAk
    Visible = False
  end
  object ControlSignalDziel: TControlSignal [58]
    Left = 227
    Top = 332
    Width = 46
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soRight
    Signal = sgDziel
    Visible = False
  end
  object ControlSignalDak: TControlSignal [59]
    Left = 409
    Top = 249
    Width = 42
    Height = 20
    Active = False
    Kind = skImpulse
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgDAK
    Visible = False
  end
  object ControlSignalNeg: TControlSignal [60]
    Left = 409
    Top = 265
    Width = 42
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgNeg
    Visible = False
  end
  object ControlSignalLub: TControlSignal [61]
    Left = 409
    Top = 282
    Width = 42
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgLub
    Visible = False
  end
  object ControlSignalI: TControlSignal [62]
    Left = 409
    Top = 298
    Width = 42
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgI
    Visible = False
  end
  object ControlSignalShl: TControlSignal [63]
    Left = 409
    Top = 315
    Width = 42
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgShl
    Visible = False
  end
  object ControlSignalShr: TControlSignal [64]
    Left = 409
    Top = 332
    Width = 42
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgShr
    Visible = False
  end
  object ControlSignalAS: TControlSignal [65]
    Left = 212
    Top = 360
    Width = 34
    Height = 20
    Active = False
    Kind = skLevel
    OnClick = ControlSignalClick
    Orientation = soLeft
    Signal = sgAS
    Visible = False
  end
  object ListBoxPaO: TListBox [66]
    Left = 508
    Top = 167
    Width = 137
    Height = 168
    Hint = 'Memory'
    Style = lbOwnerDrawFixed
    TabOrder = 0
    OnDblClick = ListBoxPaODblClick
    OnDrawItem = ListBoxPaODrawItem
    OnKeyPress = ListBoxPaOKeyPress
  end
  object CheckBoxManualControl: TCheckBox [67]
    Left = 508
    Top = 8
    Width = 137
    Height = 17
    Hint = 'Switch control mode (manual/programmable)'
    Caption = 'CheckBoxManualControl'
    TabOrder = 1
    OnClick = CheckBoxManualControlClick
  end
  inherited MainMenu: TMainMenu
    Left = 784
    Top = 24
  end
  object PopupMenuRegister: TPopupMenu
    OnPopup = PopupMenuRegisterPopup
    Left = 704
    Top = 24
    object MenuItemSetRegisterValue: TMenuItem
      Caption = 'Set value'
      OnClick = MenuItemSetRegisterValueClick
    end
    object MenuItemShowRegisterAs: TMenuItem
      Caption = 'Show as'
      object MenuItemShowAsSigned: TMenuItem
        Caption = 'signed decimal'
        OnClick = MenuItemShowAsSignedClick
      end
      object MenuItemShowAsUnsigned: TMenuItem
        Caption = 'unsigned decimal'
        OnClick = MenuItemShowAsUnsignedClick
      end
      object MenuItemShowAsBinary: TMenuItem
        Caption = 'binary'
        OnClick = MenuItemShowAsBinaryClick
      end
      object MenuItemShowAsHexadecimal: TMenuItem
        Caption = 'hexadecimal'
        OnClick = MenuItemShowAsHexadecimalClick
      end
    end
    object MenuItemClearRegister: TMenuItem
      Caption = 'Clear'
      OnClick = MenuItemClearRegisterClick
    end
  end
end

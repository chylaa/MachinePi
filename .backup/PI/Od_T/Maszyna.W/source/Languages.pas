{$DEFINE LANGUAGE_POLISH}

unit Languages;

interface

{$IFDEF LANGUAGE_POLISH}

resourcestring
  // MainUnit
  LanguageCode = 'PL';
  lsMainWindowCaption = 'Symulator maszyny W';
  lsFileMenuItemCaption = 'Plik';
  lsMenuItemFileNewCaption = 'Nowy';
  lsExitMenuItemCaption = 'Zakończ';
  lsViewMenuItemCaption = 'Pokaż';
  lsViewCPUMenuItemCaption = 'Jednostka centralna';
  lsMenuItemViewIOCaption = 'Konsola we/wy';
  lsMenuItemRunCaption = 'Wykonaj';
  lsMenuItemRunCycleCaption = 'Takt';
  // CPUView
  lsMenuItemProcessor = 'Procesor';
  lsMenuItemConfiguation = 'Konfiguracja';
  lsALUName = 'JAL';
  lsManualControlCheckBox = 'sterowanie ręczne';
  lsModifyRAMQueryCaption = 'Modyfikacja komórki pamięci';
  lsModifyRAMQueryPrompt = 'Pao[ %d ] = ';
  lsModifyRegisterCaption = 'Modyfikacja rejestru %s';
  lsModifyRegisterPrompt = 'Rejestr %s = ';
  lsMenuSetRegisterValue = 'Wpisz wartość';
  lsMenuShowRegisterAs = 'Pokaż jako';
  lsMenuClearRegister = 'Zeruj';
  lsMenuShowAsSigned = 'liczba ze znakiem';
  lsMenuShowAsUnsigned = 'liczba bez znaku';
  lsMenuShowAsBinary = 'liczba binarna';
  lsMenuShowAsHexadecimal = 'liczba szesnastkowa';
  // ControlUnit
  lsProgramFinished = 'Zakończono wykonywanie programu';
  lsDuplicateWriteBusError = 'Próba zapisu na magistralę dwu wartości jednocześnie';
  lsDuplicateWriteRegError = 'Próba zapisu do rejestru dwu wartości jednocześnie';
  lsBusEmptyError = 'Próba odczytu z pustej magistrali';
  lsReadWriteError = 'Próba jednoczesnego odczytu i zapisu pamięci';
  lsRegisterNameL = 'L';
  lsRegisterNameA = 'A';
  lsRegisterNameI = 'I';
  lsRegisterNameAK = 'Ak';
  lsRegisterNameS = 'S';
  lsRegisterNameWS = 'WS';
  lsRegisterNameRZ = 'RZ';
  lsRegisterNameRM = 'RM';
  lsRegisterNameRP = 'RP';
  lsRegisterNameAP = 'AP';
  lsRegisterNameX = 'X';
  lsRegisterNameY = 'Y';
  lsRegisterNameRB = 'RB';
  lsRegisterNameG = 'G';
  // Microcontroller
  lsFullInstructionList = 'Brak miejsca na liście rozkazów';
  lsFullMicromemory = 'Brak miejsca w pamięci mikroprogramu';
  lsUnexpected1stCycle = 'Błędna postać pierwszego taktu rozkazu';
  lsInstructionSTP = 'STP';
  lsInstructionDOD = 'DOD';
  lsInstructionODE = 'ODE';
  lsInstructionLAD = 'ŁAD';
  lsInstructionPOB = 'POB';
  lsInstructionSOB = 'SOB';
  lsInstructionSOM = 'SOM';
  lsInstructionSOZ = 'SOZ';
  // Compiler
  lsInstructionKeyword = 'rozkaz';  // pisać małymi literami
  lsArgumentsKeyword = 'argumenty';  // pisać małymi literami
  lsIfKeyword = 'jeżeli';  // pisać małymi literami
  lsThenKeyword = 'to';
  lsElse1Keyword = 'gdy';
  lsElse2Keyword = 'nie';  // może być puste
  lsGotoKeyword = 'dalej';
  lsEndKeyword = 'koniec';
  lsInstructionKeywordExpected = 'Oczekiwane słowo kluczowe ROZKAZ';
  lsSemicolonExpected = 'Oczekiwany średnik';
  lsUnexpectedCharacter = 'Nieoczekiwany znak %s';
  lsAtInsideSignalName = 'Nieoczekiwany znak @';
  lsWrongSignalPosition = 'Sygnał %s pojawił się w nieoczekiwanym miejscu';
  lsUnexpectedWord = 'Nieoczekiwane słowo %s';
  lsUnknownLabel = 'Nieznana etykieta %s'; // używane też w Assembler
  // Compiler - c.d. - nazwy sygnałów sterujących
  lsSignalNameStop = 'stop';
  lsSignalNameCzyt = 'czyt';
  lsSignalNameWyAD = 'wyad';
  lsSignalNameWyl = 'wyl';
  lsSignalNameWyls = 'wyls';
  lsSignalNameWyak = 'wyak';
  lsSignalNameWys = 'wys';
  lsSignalNameWyx = 'wyx';
  lsSignalNameWyy = 'wyy';
  lsSignalNameWyWS = 'wyws';
  lsSignalNameWyrb = 'wyrb';
  lsSignalNameWyG = 'wyg';
  lsSignalNameWyap = 'wyap';
  lsSignalNameWyrm = 'wyrm';
  lsSignalNamePisz = 'pisz';
  lsSignalNameAS = 'as';
  lsSignalNameWeja = 'weja';
  lsSignalNameNeg = 'neg';
  lsSignalNameLub = 'lub';
  lsSignalNameI = 'i';
  lsSignalNameDod = 'dod';
  lsSignalNameOde = 'ode';
  lsSignalNamePrzep = 'przep';
  lsSignalNameMnoz = 'mnoz';
  lsSignalNameDziel = 'dziel';
  lsSignalNameShr = 'shr';
  lsSignalNameShl = 'shl';
  lsSignalNameWei = 'wei';
  lsSignalNameIl = 'il';
  lsSignalNameWel = 'wel';
  lsSignalNameWea = 'wea';
  lsSignalNameWes = 'wes';
  lsSignalNameIAk = 'iak';
  lsSignalNameDAK = 'dak';
  lsSignalNameWeak = 'weak';
  lsSignalNameWex = 'wex';
  lsSignalNameWey = 'wey';
  lsSignalNameIws = 'iws';
  lsSignalNameDws = 'dws';
  lsSignalNameWeWs = 'wews';
  lsSignalNameWerb = 'werb';
  lsSignalNameStart = 'start';
  lsSignalNameRint = 'rint';
  lsSignalNameEni = 'eni';
  lsSignalNameWerm = 'werm';
  lsSignalNameSa = 'sa';
  // nazwy warunków testowanych
  lsConditionNegative = 'z';
  lsConditionZero = 'zak';
  lsConditionInterrupt = 'int';
  // Assembler - nazwy dyrektyw
  lsDirectiveRST = 'RST';
  lsDirectiveRPA = 'RPA';
  lsDirectiveEND = 'KON';
  // Assembler - błędy
  lsProgramCompiled = 'Program został skompilowany';
  lsUnexpectedChar = 'Nieoczekiwany znak "%s".';
  lsUnknownInstruction = 'Nieznany rozkaz %s';
  lsArgumentExpected = 'Oczekiwany argument';
  lsUnExpectedText = 'Nieoczekiwany tekst "%s".';
  lsCommaExpected = 'Oczekiwany przecinek';
  // CPUConfig
  lsCPUConfigCaption = 'Konfiguracja maszyny';
  lsCPUConfigArchitecture = 'Architektura';
  lsCPUConfigComponents = 'Składniki';
  lsCPUConfigAdresses = 'Adresy';
  lsLabelAddressLength = 'Liczba bitów adresowych';
  lsLabelCodeLength = 'Liczba bitów kodu';
  lsGroupBoxMachineWord = 'Słowo maszynowe';
  lsGroupBoxMachineType = 'Typ';
  lsInterbusConnection = 'Połączenie międzymagistralowe';
  lsIncAndDecAccumulator = 'Inkrementacja i dekrementacja akumulatora';
  lsLogicOpInALU = 'Operacje logiczne w JAL';
  lsExtendedArithmetic = 'Rozszerzone operacje arytmetyczne';
  lsStackAvailable = 'Obsługa stosu';
  lsXRegister = 'Rejestr X';
  lsYRegister = 'Rejestr Y';
  lsInterrupts = 'Przerwania';
  lsIOOperations = 'Układ wejścia/wyjścia';
  lsInterruptHandlers = 'Procedury obsługi przerwań';
  lsIODevices = 'Urządzenia wejścia/wyjścia';
  lsLabelLabels = 'Etykiety';
  lsLabelAddresses = 'Adresy w pamięci';
  lsLabelInput = 'Wejście';
  lsLabelOutput = 'Wyjście';
  // IODevices
  lsIOConsoleCaption = 'Konsola wejścia/wyjścia';
  lsInputCaption = 'Wejście';
  lsOutputCaption = 'Wyjście';
  lsInvalidPortNumber = 'Błędny numer portu wejścia/wyjścia';
  lsClearOutputCaption = 'Wyczyść konsolę wyjścia';
  // ProgramEditor
  lsProgramTextIsModified = 'Tekst programu został zmodyfikowany. Zapisać zmiany ?';
  lsSaveMenuItemCaption = 'Zapisz';
  lsProgramMenuItemCaption = 'Program';
  lsCompileMenuItemCaption = 'Kompiluj';
  lsRunMenuItemCaption = 'Wykonaj';
  lsEditMenuItemCaption = 'Edycja';
  lsUndoMenuItemCaption = 'Cofnij';
  lsRedoMenuItemCaption = 'Powtórz';
  lsCutMenuItemCaption = 'Wytnij';
  lsCopyMenuItemCaption = 'Kopiuj';
  lsPasteMenuItemCaption = 'Wklej';
  lsDeleteMenuItemCaption = 'Usuń';
  lsSelectAllMenuItemCaption = 'Zaznacz całość';
  lsFindMenuItemCaption = 'Znajdź';
  lsFindNextMenuItemCaption = 'Znajdź następne';
  lsFindPreviousMenuItemCaption = 'Znajdź poprzednie';
  lsReplaceMenuItemCaption = 'Zamień';
  // ogólne
  lsOKButton = 'OK';
  lsCancelButton = 'Anuluj';
  lsToolsMenuCaption = 'Narzędzia';
  lsToolsFontCaption = 'Czcionka';
{$ENDIF}


{$IFDEF LANGUAGE_ENGLISH}

resourcestring
  LanguageCode = 'EN';
  lsMainWindowCaption = 'W machine simulator';
  lsFileMenuItemCaption = 'File';
  lsMenuItemFileNewCaption = 'New';
  lsExitMenuItemCaption = 'Exit';
  lsViewMenuItemCaption = 'View';
  lsViewCPUMenuItemCaption = 'CPU';

{$ENDIF}

implementation

end.

start: POB ST1
       DNS            //straznik
Petla: WPR 1
        £AD A
        POB A
        ODE ST2
        SOM stop
        ODE ST3
        SOM Maksimumm 
        SOZ Maksimumm
        SOB koniec  

Maksimumm: 
           POB A
           DNS
           SOB Petla
Koniec:
       SDP Max
       POB L1
       SOM Liczba1 
       Liczba1: POB WYNIK
                £AD L1
                SOB start
       POB L2
       SOM Liczba2
       Liczba2: POB WYNIK
                £AD L2
                SOB Wieksza            
       Wieksza:
                POB L1
                ODE L2
                SOM Maksimum
                DOD L2
                £AD L1
                SOB Wypisz
       
       Maksimum: POB L2
                 SOB Wypisz
       stop: STP
        


Max: PZS
      £AD LICZNIK   //Œlad
      Mnozenie: PZS
                £AD A
                POB A
                SOM Wyjscie
                POB N
                DOD ST4
                £AD N
                POB N
                £AD TMP_N
                   Petla3:
                          SOZ suma 
                          POB TMP_N
                          ODE ST4
                          £AD TMP_N
                          POB I
                          £AD TMP_I
                          POB A
                          £AD TMP_A
                        Petla2:
                               POB A
                               DOD TMP_A
                               POB TMP_I
                               ODE ST4
                               £AD TMP_I
                               SOZ Petla3
                               SOB Petla2
    
                               suma: POB WYNIK
                                     DOD A
                                     £AD WYNIK
                                     SOB Mnozenie
                                     
                               Wyjscie: POB LICZNIK
                                        DNS
                                        POB WYNIK
                                        PWR
                                        
Wypisz:
stp                                         


I: RST 9           //mnoznik
TMP_I: RPA         //tymczasowa wartosc mnoznika
N: RST -1          //liczba cyfr liczby
TMP_N: RPA         //tymczasowa liczba cyfr 
A: RPA             //cyfra
TMP_A: RPA          
LICZNIK: RPA
WYNIK: RST 0
L1: RST -1
L2: RST -1
ST1: RST -1
ST2: RST 48
ST3: RST 9
ST4: RST 1


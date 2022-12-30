POB A1
SOZ END 
DNS
POB B2
SOZ END
DNS
SDP Mnozenie
End: 
POB WYZ
DOD TMP
£AD WYZ
POB TMP
ODE TMP
£AD TMP
POB A2
SOZ END2
DNS
POB B1
SOZ END2
DNS
SDP Mnozenie
END2:
POB WYZ
ODE TMP
£AD WYZ
POB TMP
ODE TMP
£AD TMP
POB WYZ
STP

//PODPROGRAM MNO¯ENIE  
Mnozenie: PZS
          £AD Œlad
          PZS
          £AD A
          PZS
          £AD B
          Petla:
                POB TMP
                DOD B
                £AD TMP
                POB A
                ODE LICZ
                £AD A
                SOZ Koniec
                SOB Petla
          Koniec:
                 POB Œlad
                 DNS
                 PWR                   
                               
//ZMIENNE
WYZ: RST 0
TMP: RST 0
Œlad: RPA
A: RPA
B: RPA
LICZ: RST 1
A1: RST 2
A2: RST 3
B1: RST 2
B2: RST 6
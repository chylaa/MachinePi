//program gl�wny
SDP Czyt
�AD Liczba1
SDP Czyt
�AD Liczba2
POB Liczba1
ODE Liczba2
SOM PierwszaMniejsza 
POB Liczba1
SOB Wypisz 
PierwszaMniejsza:
POB Liczba2 
Wypisz: SDP PiszLiczbe   
STP 
Liczba1: RPA 
Liczba2: RPA 

//podprogram czytanie
CZYT:
POB LICZBA 
ODE LICZBA
�AD LICZBA
CZYT_PETLA:
WPR 1
�AD CYFRA
POB ST57
ODE CYFRA
SOM CZYT_KONIEC
POB CYFRA
ODE ST48
SOM CZYT_KONIEC
DOD LICZBA
MNO ST10
�AD LICZBA
SOB CZYT_PETLA 
CZYT_KONIEC: POB LICZBA
DZI ST10
�AD LICZBA
 PWR 
STP 
//ZMIENNE
CYFRA: RPA
LICZBA: RPA
ST10: RST 10
ST48: RST 48
ST57: RST 57

//podprogram wypisz
PiszLiczbe:  
//pobranie z akumulatora argumentu, by sie nie zgubil
�AD Pisz_pierwotnaliczba 
//-1 wartownik
POB Pisz_Sminus1
DNS      
//p�tla zapisujaca liczbe na stos
Pisz_Petla: 
POB Pisz_pierwotnaliczba
//dzielenie i mnozenie
DZI Pisz_S10
�AD Pisz_tmpwynik
MNO Pisz_S10
�AD Pisz_wynikmnozenia  
//reszty z dzielenia
POB Pisz_pierwotnaliczba
ODE Pisz_wynikmnozenia 
//cyfra na stos
DNS  
//dzielenie przez 10
POB Pisz_tmpwynik
�AD Pisz_pierwotnaliczba  
SOZ Pisz_KoniecPetli    
SOB Pisz_Petla      
Pisz_KoniecPetli:  
Pisz_PetlaWypisujaca: 
PZS
SOM Pisz_KoniecPodprogramu 
DOD Pisz_S48
WYP 2
SOB Pisz_PetlaWypisujaca 
Pisz_KoniecPodprogramu: 
POB Pisz_znaknowejlini
WYP 2 
PWR

//zmienne wypisz
Pisz_pierwotnaliczba: RST 0
Pisz_tmpwynik: RST 0
Pisz_S48: RST 48
Pisz_Sminus1: RST -1
Pisz_S1: RST 1
Pisz_S10: RST 10
Pisz_wynikmnozenia: RST 10
Pisz_znaknowejlini: RST 13
St1: RST 1
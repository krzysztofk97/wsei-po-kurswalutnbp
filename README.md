# Kurs Walut NBP (wsei-po-kurswalutnbp)

*Projekt realizowany był w trakcie ćwiczeń z przedmiotu "Programowanie obiektowe"*

## Opis projektu

Prosta aplikacja konsolowa, której zadaniem jest wykorzystanie udostępnionych przez Narodowy Bank Polski informacji o kursach walut w formacie XML, i przetworzenie ich w taki sposób, aby użytkownik otrzymał informację o kursie danej waluty w danym przez niego okresie.

*Nardowy Bank Polski udostępnia na swoich stronach API służące do pobierania danych, jednak ćwiczenie polega głównie na przetwarzaniu danych w oparciu o te zapisane w plikach XML.*

## Działanie aplikacji

*Aby uruchomić aplikację, należy ją uprzednio skompilować z wykorzytsaniem środowiska Visual Studio lub konsolowych narzędzi dotnet.*

Po uruchomieniu aplikacja zapyta o trzyliterowy kod waluty. Następnie o podanie daty poczatkowej i końcowej, dla której mają zostać wyświetlone informacje. Jeżeli waluta o danym kodzie nie zostanie znaleziona, aplikacja zwróci informację, że nie udało się jej znaleźć.

Aplikacja akceptuje również parametry przekazywane podczas uruchamiania aplikacji:

`> dotnet KursWalutNBP.dll <kod_waluty> <data_poczatkowa> <data_koncowa>`

Gdzie:
* `<kod_waluty>` - trzyliterowy kod waluty (np. EUR lub USD)
* `<data_poczatkowa>` - data początkowa, od której aplikacja ma zacząć pobieranie danych
* `<data_koncowa>` - data końcowa, na której aplikacja ma zakończyć pobieranie danych

### Przykłady

`> dotnet KursWalutNBP.dll`
```
Podaj trzyliterowy kod waluty (np. EUR): EUR
Wybrana waluta to: EUR
Podaj datę początkową: 01.01.2021
Wybrana data początkowa: 01.01.2021
Podaj datę końcową: 01.02.2021
Wybrana data końcowa: 01.02.2021

Dane dla EUR (euro) w okresie od 01.01.2021 do 01.02.2021
Kurs średni: 4,5346
Najniższy dzienny średni kurs: 4,4973 (w dniu 07.01.2021)
Najwyższy dzienny średni kurs: 4,5497 (w dniu 26.01.2021)

Dane na podstawie 20 raportów ze strony Narodowego Banku Polskiego
```

`> dotnet KursWalut.dll USD`
```
Wybrana waluta to: USD
Podaj datę początkową: 14.01.2021
Wybrana data początkowa: 14.01.2021
Podaj datę końcową: 18.03.2021
Wybrana data końcowa: 18.03.2021

Dane dla USD (dolar amerykański) w okresie od 14.01.2021 do 18.03.2021
Kurs średni: 3,7577
Najniższy dzienny średni kurs: 3,6940 (w dniu 16.02.2021)
Najwyższy dzienny średni kurs: 3,8705 (w dniu 18.03.2021)

Dane na podstawie 46 raportów ze strony Narodowego Banku Polskiego
```

## Użyte technologie

* .NET 5 / C#
* Linq
# Instrukcja publikacji na GitHub

## Kroki do opublikowania projektu:

### 1. Inicjalizacja repozytorium Git (w folderze projektu)
```bash
git init
git add .
git commit -m "Initial commit - School Draw System"
```

### 2. Utwórz repozytorium na GitHub
- WejdŸ na https://github.com
- Kliknij "New repository"
- Nazwa: `SystemLosowania`
- Opis: `System losowania osoby do odpowiedzi - .NET MAUI`
- Ustaw jako Public
- **NIE** zaznaczaj "Add README" (ju¿ mamy)
- Kliknij "Create repository"

### 3. Po³¹cz lokalne repozytorium z GitHub
```bash
git remote add origin https://github.com/TWOJA-NAZWA-UZYTKOWNIKA/SystemLosowania.git
git branch -M main
git push -u origin main
```

### 4. Wyœlij link przez formularz
Link bêdzie wygl¹da³ tak:
`https://github.com/TWOJA-NAZWA-UZYTKOWNIKA/SystemLosowania`

Wyœlij go na: https://forms.cloud.microsoft/e/iRG4btTUhX

---

## Alternatywnie - przez Visual Studio:
1. Kliknij prawym na Solution w Solution Explorer
2. Wybierz "Add Solution to Source Control"
3. Wybierz GitHub
4. Zaloguj siê i opublikuj

---

## Co zawiera projekt:

? Wszystkie wymagania na ocenê 5:
- System szczêœliwego numerka
- Sprawdzanie obecnoœci
- Blokada po 3 odpowiedziach
- Zapis wszystkich danych do TXT
- Selektory CSS: ^, >, klasy, ID
- Stylowanie paska tytu³owego
- Oryginalna animacja "Falling Cards"

? Struktura Models/Views
? Nazewnictwo angielskie
? Zapis/odczyt bez bibliotek zewnêtrznych

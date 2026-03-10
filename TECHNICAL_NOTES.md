# Kluczowe elementy projektu - wyjaœnienia

## 1. SELEKTORY CSS

### Selektor ^ (caret) - dopasowanie wszystkich elementów danego typu
W pliku `Styles.css` linia 2-4:
```css
^contentpage {
    background-color: #f5f5f5;
}
```
To oznacza: "Wszystkie elementy ContentPage niezale¿nie od tego gdzie s¹"

Linia 7-9:
```css
^contentpage > scrollview {
    background-color: transparent;
}
```
To oznacza: "Wszystkie ScrollView, które s¹ bezpoœrednimi dzieæmi ContentPage"

### Selektor > (bezpoœrednie dzieci)
W pliku `Styles.css` linia 102-106:
```css
.formContainer > entry {
    border-color: #667eea;
    border-width: 1;
    corner-radius: 8;
}
```
To oznacza: "Wszystkie Entry, które s¹ bezpoœrednimi dzieæmi elementu z klas¹ formContainer"

Inne przyk³ady (linia 43-46):
```css
.classList > ^frame {
    background-color: white;
    corner-radius: 8;
}
```

## 2. SYSTEM ZAPISU BEZ BIBLIOTEK

Plik: `Models/FileManager.cs`

### Zapis (metoda SaveClass):
```csharp
string line = student.FirstName + "|" + student.LastName + "|" + 
     student.IsPresent.ToString() + "|" + 
      student.TimesAnswered.ToString() + "|" + 
              student.LuckyNumber.ToString();
File.WriteAllLines(filePath, lines);
```

U¿yty separator: `|` (pipe)
Format: `FirstName|LastName|IsPresent|TimesAnswered|LuckyNumber`

### Odczyt (metoda LoadClass):
```csharp
string[] lines = File.ReadAllLines(filePath);
foreach (string line in lines)
{
    string[] parts = line.Split('|');
    Student student = new Student
    {
    FirstName = parts[0],
        LastName = parts[1],
      IsPresent = bool.Parse(parts[2]),
        TimesAnswered = int.Parse(parts[3]),
      LuckyNumber = int.Parse(parts[4])
    };
}
```

## 3. LOGIKA LOSOWANIA

Plik: `Models/DrawManager.cs`, metoda `DrawStudent`

### Krok 1: Filtrowanie dostêpnych uczniów
```csharp
foreach (Student student in schoolClass.Students)
{
    if (student.IsPresent && student.TimesAnswered < 3)
    {
        availableStudents.Add(student);
    }
}
```

### Krok 2: Reset gdy wszyscy odpowiedzieli 3 razy
```csharp
if (availableStudents.Count == 0)
{
 foreach (Student student in schoolClass.Students)
    {
    student.TimesAnswered = 0;
    }
    availableStudents = schoolClass.Students.Where(s => s.IsPresent).ToList();
}
```

### Krok 3: Szczêœliwy numerek
```csharp
List<Student> luckyStudents = availableStudents
    .Where(s => s.LuckyNumber == todayLuckyNumber).ToList();

if (luckyStudents.Count > 0)
{
    // Losuj spoœród uczniów ze szczêœliwym numerkiem
}
```

### Krok 4: Zwiêkszenie licznika
```csharp
selectedStudent.TimesAnswered++;
return selectedStudent;
```

## 4. ANIMACJA "FALLING CARDS"

Plik: `Views/DrawPage.xaml.cs`

### Tworzenie karty:
```csharp
Label card = new Label
{
    Text = randomStudents[i].GetFullName(),
    StyleClass = new List<string> { "fallingCard" },
    Opacity = 0
};
```

### Animacja spadania:
```csharp
var animation = new Animation(v => 
{
    var currentBounds = AbsoluteLayout.GetLayoutBounds(card);
    AbsoluteLayout.SetLayoutBounds(card, 
 new Rect(currentBounds.X, v, currentBounds.Width, currentBounds.Height));
}, -100, endY);

animation.Commit(card, "FallAnimation", 16, (uint)duration, Easing.CubicIn);
```

Karty:
- Startuj¹ powy¿ej ekranu (Y = -100)
- Spadaj¹ w losowych pozycjach X
- Przyspieszaj¹ (Easing.CubicIn)
- Znikaj¹ na dole
- Co 100ms spada nowa karta

## 5. MODYFIKACJA PASKA TYTU£OWEGO

Plik: `App.xaml.cs`
```csharp
MainPage = new NavigationPage(new MainMenuPage())
{
    BarBackgroundColor = Color.FromArgb("#667eea"),
    BarTextColor = Colors.White
};
```

Równie¿ w CSS (Styles.css):
- T³o paska: fioletowy gradient (#667eea)
- Tekst: bia³y

## 6. STRUKTURA FOLDERÓW

```
SystemLosowania/
??? Models/          <- Modele danych i logika biznesowa
?   ??? Student.cs
?   ??? SchoolClass.cs
?   ??? FileManager.cs
?   ??? DrawManager.cs
??? Views/      <- Strony aplikacji (XAML + Code-behind)
?   ??? MainMenuPage.xaml/.cs
?   ??? ManageClassesPage.xaml/.cs
?   ??? ManageStudentsPage.xaml/.cs
?   ??? SelectClassPage.xaml/.cs
?   ??? SelectClassForEditPage.xaml/.cs
?   ??? AttendancePage.xaml/.cs
?   ??? DrawPage.xaml/.cs
??? Resources/
    ??? Styles/
        ??? Styles.css    <- Style CSS
```

## 7. WYKORZYSTANE TECHNOLOGIE

- **XAML** - interfejs u¿ytkownika
- **CSS** - stylowanie (klasy, ID, ^, >)
- **C#** - logika aplikacji
- **File I/O** - zapis/odczyt bez bibliotek
- **Animations** - animacje MAUI
- **Navigation** - nawigacja miêdzy stronami

## 8. FLOW APLIKACJI

1. **MainMenuPage** - Menu g³ówne
   ?
2. **ManageClassesPage** - Tworzenie/usuwanie klas
   ?
3. **SelectClassForEditPage** - Wybór klasy
   ?
4. **ManageStudentsPage** - Dodawanie/edycja uczniów
   ?
5. **SelectClassPage** - Wybór klasy do losowania
   ?
6. **AttendancePage** - Sprawdzanie obecnoœci
   ?
7. **DrawPage** - LOSOWANIE z animacj¹!

---

Wszystkie te elementy spe³niaj¹ wymagania na ocenê 5! ??

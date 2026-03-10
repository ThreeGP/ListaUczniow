using System;
using System.Collections.Generic;
using System.IO;

namespace SystemLosowania.Models
{
    public class FileManager
    {
   private string folderPath;

        public FileManager()
        {
    // tworze folder gdzie beda zapisywane klasy
 folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SchoolClasses");

            // sprawdzam czy folder istnieje jak nie to tworze
            if (Directory.Exists(folderPath) == false)
     {
                Directory.CreateDirectory(folderPath);
  }
        }

        public void SaveClass(SchoolClass schoolClass)
      {
     // zapisuje klase do pliku txt
   string filePath = Path.Combine(folderPath, schoolClass.ClassName + ".txt");
            
   List<string> lines = new List<string>();

   // przechodze przez kazdego ucznia i tworze linie
            for (int i = 0; i < schoolClass.Students.Count; i++)
   {
          Student student = schoolClass.Students[i];
    
              // skladam dane ucznia w jedna linie rozdzielona |
          string line = student.FirstName + "|" + student.LastName + "|" +
       student.IsPresent.ToString() + "|" +
       student.TimesAnswered.ToString() + "|" +
         student.LuckyNumber.ToString();
        
     lines.Add(line);
       }

        // zapisuje wszystko do pliku
      File.WriteAllLines(filePath, lines);
        }

        public SchoolClass LoadClass(string className)
        {
  // wczytuje klase z pliku
     string filePath = Path.Combine(folderPath, className + ".txt");

// sprawdzam czy plik istnieje
            if (File.Exists(filePath) == false)
      {
     return null;
       }

      SchoolClass schoolClass = new SchoolClass(className);
            string[] lines = File.ReadAllLines(filePath);

   // przechodze przez kazda linie i tworze ucznia
      for (int i = 0; i < lines.Length; i++)
            {
     string line = lines[i];
    
           // pomijam puste linie
 if (line == "" || line == null)
                {
          continue;
       }

        // dziele linie na czesci
        string[] parts = line.Split('|');

            // sprawdzam czy jest wystarczajaco duzo danych
         if (parts.Length >= 5)
      {
        Student student = new Student();
          student.FirstName = parts[0];
       student.LastName = parts[1];
           student.IsPresent = bool.Parse(parts[2]);
               student.TimesAnswered = int.Parse(parts[3]);
          student.LuckyNumber = int.Parse(parts[4]);
     
             schoolClass.Students.Add(student);
     }
            }

            return schoolClass;
    }

        public List<string> GetAllClassNames()
        {
    // pobieram nazwy wszystkich klas
            List<string> classNames = new List<string>();

      // sprawdzam czy folder istnieje
if (Directory.Exists(folderPath) == false)
            {
           return classNames;
        }

            // pobieram wszystkie pliki txt
     string[] files = Directory.GetFiles(folderPath, "*.txt");

            // wyciagam nazwy plikow
  for (int i = 0; i < files.Length; i++)
         {
    string file = files[i];
        string fileName = Path.GetFileNameWithoutExtension(file);
    classNames.Add(fileName);
       }

            return classNames;
        }

        public void DeleteClass(string className)
        {
     // usuwam plik klasy
       string filePath = Path.Combine(folderPath, className + ".txt");

     if (File.Exists(filePath) == true)
            {
         File.Delete(filePath);
 }
        }
    }
}

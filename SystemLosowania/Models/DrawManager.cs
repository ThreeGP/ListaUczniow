using System;
using System.Collections.Generic;

namespace SystemLosowania.Models
{
 public class DrawManager
    {
        private Random random;

   public DrawManager()
     {
  // tworze generator losowy
            random = new Random();
      }

        public Student DrawStudent(SchoolClass schoolClass, int todayLuckyNumber)
        {
    // najpierw zbieram uczniow ktorzy sa obecni i odpowiadali mniej niz 3 razy
    List<Student> availableStudents = new List<Student>();

   for (int i = 0; i < schoolClass.Students.Count; i++)
            {
       Student student = schoolClass.Students[i];
   
        // sprawdzam czy uczen jest obecny i czy odpowiadal mniej niz 3 razy
     if (student.IsPresent == true && student.TimesAnswered < 3)
          {
      availableStudents.Add(student);
   }
    }

   // jak nie ma nikogo to resetuje licznik odpowiedzi
  if (availableStudents.Count == 0)
            {
       // zeruje licznik dla wszystkich
      for (int i = 0; i < schoolClass.Students.Count; i++)
        {
          schoolClass.Students[i].TimesAnswered = 0;
                }

 // zbieram od nowa tylko obecnych
    availableStudents = new List<Student>();
       for (int i = 0; i < schoolClass.Students.Count; i++)
  {
      if (schoolClass.Students[i].IsPresent == true)
              {
  availableStudents.Add(schoolClass.Students[i]);
    }
         }
     }

   // jak nadal nie ma nikogo to zwracam null
      if (availableStudents.Count == 0)
            {
      return null;
            }

  // sprawdzam czy ktos ma szczesliwy numerek
     List<Student> luckyStudents = new List<Student>();
    for (int i = 0; i < availableStudents.Count; i++)
            {
    if (availableStudents[i].LuckyNumber == todayLuckyNumber)
     {
       luckyStudents.Add(availableStudents[i]);
 }
     }

      // jak sa uczniowie ze szczesliwym numerkiem to losuje z nich
           if (luckyStudents.Count > 0)
     {
   int index = random.Next(0, luckyStudents.Count);
  Student drawnStudent = luckyStudents[index];
           drawnStudent.TimesAnswered = drawnStudent.TimesAnswered + 1;
return drawnStudent;
   }

      // jak nie ma szczesliwych to losuje z dostepnych
        int randomIndex = random.Next(0, availableStudents.Count);
     Student selectedStudent = availableStudents[randomIndex];
            selectedStudent.TimesAnswered = selectedStudent.TimesAnswered + 1;
     return selectedStudent;
        }

        public List<Student> GetRandomStudentsForAnimation(SchoolClass schoolClass, int count)
        {
     // generuje losowa liste uczniow do animacji
    List<Student> animationStudents = new List<Student>();
    
      for (int i = 0; i < count; i++)
   {
    // sprawdzam czy nie przekraczam ilosci uczniow
       if (i < schoolClass.Students.Count)
   {
    int index = random.Next(0, schoolClass.Students.Count);
 animationStudents.Add(schoolClass.Students[index]);
    }
            }

   return animationStudents;
        }
    }
}

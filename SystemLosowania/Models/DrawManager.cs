using System;
using System.Collections.Generic;

namespace SystemLosowania.Models;

public class DrawManager
{
    private readonly Random random;

    public DrawManager()
    {
        random = new Random();
    }

    public Student DrawStudent(SchoolClass schoolClass, int todayLuckyNumber)
    {
        List<Student> availableStudents = new List<Student>();

        for (int i = 0; i < schoolClass.Students.Count; i++)
        {
            Student student = schoolClass.Students[i];
            if (student.IsPresent && student.TimesAnswered < 3)
            {
                availableStudents.Add(student);
            }
        }

        if (availableStudents.Count == 0)
        {
            for (int i = 0; i < schoolClass.Students.Count; i++)
            {
                schoolClass.Students[i].TimesAnswered = 0;
            }

            availableStudents = new List<Student>();
            for (int i = 0; i < schoolClass.Students.Count; i++)
            {
                if (schoolClass.Students[i].IsPresent)
                {
                    availableStudents.Add(schoolClass.Students[i]);
                }
            }
        }

        if (availableStudents.Count == 0)
        {
            return null;
        }

        List<Student> luckyStudents = new List<Student>();
        for (int i = 0; i < availableStudents.Count; i++)
        {
            if (availableStudents[i].LuckyNumber == todayLuckyNumber)
            {
                luckyStudents.Add(availableStudents[i]);
            }
        }

        if (luckyStudents.Count > 0)
        {
            int index = random.Next(0, luckyStudents.Count);
            Student drawnStudent = luckyStudents[index];
            drawnStudent.TimesAnswered++;
            return drawnStudent;
        }

        int randomIndex = random.Next(0, availableStudents.Count);
        Student selectedStudent = availableStudents[randomIndex];
        selectedStudent.TimesAnswered++;
        return selectedStudent;
    }

    public List<Student> GetRandomStudentsForAnimation(SchoolClass schoolClass, int count)
    {
        List<Student> animationStudents = new List<Student>();

        for (int i = 0; i < count; i++)
        {
            if (i < schoolClass.Students.Count)
            {
                int index = random.Next(0, schoolClass.Students.Count);
                animationStudents.Add(schoolClass.Students[index]);
            }
        }

        return animationStudents;
    }
}

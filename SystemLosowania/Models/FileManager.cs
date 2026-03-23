using System;
using System.Collections.Generic;
using System.IO;

namespace SystemLosowania.Models;

public class FileManager
{
    private readonly string folderPath;

    public FileManager()
    {
        folderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SchoolClasses");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    public void SaveClass(SchoolClass schoolClass)
    {
        string filePath = Path.Combine(folderPath, schoolClass.ClassName + ".txt");
        List<string> lines = new List<string>();

        for (int i = 0; i < schoolClass.Students.Count; i++)
        {
            Student student = schoolClass.Students[i];
            string line = student.FirstName + "|" + student.LastName + "|" +
                          student.IsPresent + "|" +
                          student.TimesAnswered + "|" +
                          student.LuckyNumber;

            lines.Add(line);
        }

        File.WriteAllLines(filePath, lines);
    }

    public SchoolClass LoadClass(string className)
    {
        string filePath = Path.Combine(folderPath, className + ".txt");

        if (!File.Exists(filePath))
        {
            return null;
        }

        SchoolClass schoolClass = new SchoolClass(className);
        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split('|');
            if (parts.Length < 5)
            {
                continue;
            }

            Student student = new Student
            {
                FirstName = parts[0],
                LastName = parts[1],
                IsPresent = bool.Parse(parts[2]),
                TimesAnswered = int.Parse(parts[3]),
                LuckyNumber = int.Parse(parts[4])
            };

            schoolClass.Students.Add(student);
        }

        return schoolClass;
    }

    public List<string> GetAllClassNames()
    {
        List<string> classNames = new List<string>();

        if (!Directory.Exists(folderPath))
        {
            return classNames;
        }

        string[] files = Directory.GetFiles(folderPath, "*.txt");
        for (int i = 0; i < files.Length; i++)
        {
            string fileName = Path.GetFileNameWithoutExtension(files[i]);
            classNames.Add(fileName);
        }

        return classNames;
    }

    public void DeleteClass(string className)
    {
        string filePath = Path.Combine(folderPath, className + ".txt");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}

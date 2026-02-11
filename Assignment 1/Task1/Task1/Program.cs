using System;
using System.Collections.Generic;
using System.Linq;

public record Person(string Name, int Age, string City);

class Program
{
    static void Main()
    {
        string[] csvLines =
        {
            "Amit,22,Pune",
            "Vishal,17,Mumbai",
            "Jayesh,30,Bangalore",
            "Sia,15,Delhi",
            "Vidhi,18,Chennai"
        };

        List<Person> people = csvLines
            .Select(line => line.Split(','))
            .Select(parts => parts switch
            {
                [var name, var ageText, var city]
                    when int.TryParse(ageText, out int age)
                    => new Person(name, age, city),

                _ => null
            })
            .Where(p => p is not null)
            .ToList()!;

        var adults = people
            .Where(p => p is { Age: >= 18 })
            .ToList();

        Console.WriteLine("Adults:");
        foreach (var person in adults)
        {
            Console.WriteLine($"{person.Name} ({person.Age}) - {person.City}");
        }
    }
}




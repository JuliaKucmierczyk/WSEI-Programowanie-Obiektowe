using System;
using System.Collections.Generic;

namespace Lab_6
{
    class Student
    {
        public string name { get; set; }
        public int ECTS { get; set; }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Student Equals");
            return obj is Student student &&
                   name == student.name &&
                   ECTS == student.ECTS;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("Student HashCode");
            return HashCode.Combine(name, ECTS);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ICollection<string> names = new List<string>();
            names.Add("Ewa");
            names.Add("Karol");
            names.Add("Adam");
            names.Add("Adam");
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine(names.Contains("Adam"));
            Console.WriteLine(names.Remove("Adam"));

            Console.WriteLine();
            // Wykonaj podobne operacje na kolekcji studentów ( LISTA )s
            ICollection<Student> students = new List<Student>(); // rzutowanie niejawsne ze szczegółu na ogół
            students.Add(new Student() { name = "Ewa", ECTS = 169 });
            students.Add(new Student() { name = "Karol", ECTS = 190 });
            students.Add(new Student() { name = "Adam", ECTS = 120 });
            students.Add(new Student() { name = "Adam", ECTS = 83 });
            foreach (Student s in students)
            {
                Console.WriteLine(s.name + " " + s.ECTS);
            }
            Console.WriteLine(students.Contains(new Student() { name = "Karol", ECTS = 190 }));
            Console.WriteLine(students.Remove(new Student() { name = "Adam", ECTS = 83 }));
            List<Student> list = (List<Student>)students; // rzutowanie jawne z ogółu na szczegół
            list[0] = new Student() { };
            list.Insert(1, new Student() { name = "Ania", ECTS = 44 });
            list.RemoveAt(2);
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].name);
            }
            Console.WriteLine();

            // Teraz SET
            ISet<string> set = new HashSet<string>();
            set.Add("Ewa");
            set.Add("Karol");
            set.Add("Ania");
            Console.WriteLine(set.Contains("Ewa"));
            Console.WriteLine(set.Remove("Ewa"));
            foreach (string name in set)
            {
                Console.WriteLine(name);
            }
            ISet<Student> group = new HashSet<Student>();
            group.Add(new Student() { name = "Ewa", ECTS = 99 });
            foreach (var s in group)
            {
                Console.WriteLine(s.name + " " + s.ECTS);
            }
            Console.WriteLine(group.Contains(new Student() { name = "Karol", ECTS = 190 }));
            ISet<int> s1 = new HashSet<int>(new int[] { 1, 2, 3, 4, 5 });
            ISet<int> s2 = new HashSet<int>(new int[] { 7, 9, 2, 6, 8 });
            s1.IntersectWith(s2);
            Console.WriteLine(string.Join(", ", s1));

            Dictionary<string, string> phoneBook = new Dictionary<string, string>();
            phoneBook.Add("Adam", "123456789");
            phoneBook["Ewa"] = "098765432";
            phoneBook["Karol"] = "678345182";
            Console.WriteLine(phoneBook["Ewa"]);
            foreach (var item in phoneBook)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
            Dictionary<string, object> semiObj = new Dictionary<string, object>();
            semiObj["name"] = "adam";
            semiObj["points"] = 45;
            semiObj["student"] = list[0];


            // ZADANIE
            string[] arr = { "ewa", "karol", "mariola", "maciek", "zbyszek", "ewa", "adam" };
            //podaj ile razy występuje każde imię w tabelii arr
            Dictionary<string, int> ile = new Dictionary<string, int>();

            int count = 0;
            foreach (string name in arr)
            {
                Console.WriteLine(ile[name] = count++); 
            }
        }
    }
}

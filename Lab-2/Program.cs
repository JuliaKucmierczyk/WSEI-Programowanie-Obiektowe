using System;

namespace lab_2
{
    class Program
    {
        //ĆWICZENIE 2 wykonywane na lekcji
        interface IFly
        {
            void Fly();
        }

        interface ISwim
        {
            void Swim();
        }

        class Duck : Animal, ISwim, IFly
        {
            public void Fly()
            {

            }
            public void Swim()
            {

            }
        }

        class Hydroplane : ISwim, IFly
        {
            public void Fly()
            {

            }
            public void Swim()
            {

            }
        }

        abstract class Animal
        {

        }


        //Implementacja klas na podstawie diagramów (ZADANIE 6)
        abstract class Person
        {
            public virtual string FirstName
            {
                get; init;
            }
            public virtual string Lastname
            {
                get; init;
            }
            public virtual DateTime BirthDate
            {
                get; init;
            }
        }
        class Student : Person
        {
            public virtual int StudentID
            {
                get; init;
            }
        }
        class Lecturer : Person
        {
            public virtual string AcademicDegree
            {
                get; init;
            }
        }

        abstract class StudentLectureGroup 
        {
            public virtual string Name
            {
                get; init;
            }
            public abstract Student AddStudent();
            public abstract Lecturer SetLecturer();
        }

        //Ćwiczenia na lekcji
        abstract class Product
        {
            public virtual decimal Price
            {
                get; init;
            }
            public abstract decimal GetVatPrice();
        }
        class Computer : Product
        {
            public decimal Vat { get; init; }
            public override decimal GetVatPrice()
            {
                return Price * Vat / 100.0m;
            }
        }
        class Paint : Product
        {
            public decimal Vat { get; set; }
            public decimal Capacity { get; init; }
            public decimal PriceUnit { get; init; }
            public override decimal GetVatPrice()
            {
                return PriceUnit * Capacity * Vat / 100m;
            }
            public override decimal Price
            {
                get
                {
                    return PriceUnit * Capacity;
                }
            }
        }


        class Butter : Product
        {
            public override decimal GetVatPrice()
            {
                return 2m;
            }
        }



        static void Main(string[] args)
        {
            Product[] shop = new Product[4];
            shop[0] = new Computer() { Price = 2000m, Vat = 23m };
            shop[1] = new Paint() { PriceUnit = 12, Capacity = 5, Vat = 8 };
            shop[2] = new Computer() { Price = 2400m, Vat = 23m };
            shop[3] = new Butter();
            decimal sumVat = 0;
            decimal sumPrice = 0;
            foreach (var product in shop)
            {
                sumVat += product.GetVatPrice();
                sumPrice += product.Price;

                //STARSZA WERSJA RZUTOWANIA
                if (product is Computer
                    )
                {
                    Computer computer = (Computer)product;
                }

                //NOWA WERSJA RZUTOWANIA
                Computer computer1 = product as Computer;
                Console.WriteLine(computer1?.Vat);

            }

            Console.WriteLine(sumPrice);
            Console.WriteLine(sumVat);

            IFly[] flyingObject = new IFly[2];
            Duck duck = new Duck();
            flyingObject[0] = duck;
            flyingObject[0] = new Hydroplane();

            ISwim[] swimmingObjects = new ISwim[2];
            swimmingObjects[0] = duck;

            //IAggregate aggregate;
            //IIterator iterator = aggregate.CreateIterator() as IIterator;

            //while (iterator.HasNext())
            //{
            //   Console.WriteLine(iterator.GetNext());
            //}


            //UŻYCIE ZAIMPELENOWANEGO DIAGRAMU (ZADANIE 6)

            Student student = new Student() { StudentID = 1, BirthDate = new DateTime(2008, 5, 1, 8, 30, 52), FirstName = "Adam", Lastname = "Kowalski" };
            Lecturer lecturer = new Lecturer() { AcademicDegree = "Profesor", FirstName = "Jarosław", Lastname = "Kozak"};
            Console.WriteLine("\nDANE STUDENTA:");
            Console.WriteLine("Imię : "+student.FirstName+"\nNazwisko: "+student.Lastname+"\nNumer ID: "+student.StudentID); 
            Console.WriteLine("\nDANE WYKŁADOWCY:");
            Console.WriteLine("Imię : " + lecturer.FirstName + "\nNazwisko: " + lecturer.Lastname + "\nStopień naukowy: " + lecturer.AcademicDegree);

        }
    }

    interface IAggregate
    {
        IIterator CreateIterator();
    }
    interface IIterator
    {
        bool HasNext();
        int GetNext();
    }
}
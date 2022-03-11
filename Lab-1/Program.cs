#nullable enable
using System;
using System.Linq;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {


            var money1 = Money.OfWithException(5, Currency.EUR);
            Console.WriteLine(money1.Value + " " + money1.Currency);
            var money2 = Money.ParseValue("13.45", Currency.EUR);
            Console.WriteLine(money2.Value + " " + money2.Currency);
            Console.WriteLine("money2.Currency = " + money2.Currency);
            var result1 = money1 * 0.2m;
            Console.WriteLine(result1.Value + " " + result1.Currency);
            var result2 = money1 + money2;
            Console.WriteLine($"{money1.Value} {money1.Currency} + {money2.Value} {money2.Currency} = {result2.Value} {result2.Currency}");
            Console.WriteLine($"{money1.Value} {money1.Currency} < {money2.Value} {money2.Currency} = {money1 < money2}");
            Console.WriteLine($"{money1.Value} {money1.Currency} > {money2.Value} {money2.Currency} = {money1 > money2}");

            float fmoney = (float)money1;
            Console.WriteLine(money1.Value + " " + money1.Currency);


            Student[] register =
            {
                new Student{ Nazwisko="Kowalski", Imie="Jan", Średnia=3.0m },
                new Student{ Nazwisko="Nowak", Imie="Krzysztof", Średnia=2.0m },
                new Student{ Nazwisko="Kościszko", Imie="Tadeusz", Średnia=5.0m },
                new Student{ Nazwisko="Zagłoba", Imie="Onfury", Średnia=4.0m },
                new Student{ Nazwisko="Antolski", Imie="Bartosz", Średnia=3.5m }
            };

            Array.Sort(register);
            register.ToList().ForEach(a => Console.WriteLine(a + " "));

            var money3 = Money.OfWithException(100, Currency.PLN);
            var result3 = money3.ToCurrency(Currency.USD, 4.1m);
            var result4 = money3.ToCurrency(Currency.PLN, 4.1m);
            Console.WriteLine($"money3: {money3.Value} {money3.Currency}");
            Console.WriteLine($"result3: {result3.Value} {result3.Currency}");
            Console.WriteLine($"result4: {result4.Value} {result4.Currency}");
        }
    }

    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }

    public class Money
    {
        private readonly decimal _value;
        private readonly Currency _currency;
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }

        public static Money OfWithException(decimal value, Currency currency)
        {
            if (value < 0 || Enum.IsDefined(currency) == false)
                throw new ArgumentException();
            else
                return new Money(value, currency);
        }

        public static Money ParseValue(string valueStr, Currency currency)
        {
            decimal value;
            if (Decimal.TryParse(valueStr, out value))
                return OfWithException(value, currency);

            else throw new ArgumentException("Niepoprawna wartość");
        }

        public decimal Value
        {
            get { return _value; }
        }
        public Currency Currency
        {
            get { return _currency; }
        }

        public static Money operator *(Money money, decimal factor)
        {
            return OfWithException(money.Value * factor, money.Currency);
        }

        public static Money operator +(Money money1, Money money2)
        {
            if (money1.Currency != money2.Currency)
                throw new ArgumentException("Nieprawidłowe waluty");

            return OfWithException(money1.Value + money2.Value, money1.Currency);
        }

        public static bool operator >(Money a, Money b)
        {
            return a.Value > b.Value;
        }
        public static bool operator <(Money a, Money b)
        {
            return a.Value < b.Value;
        }

        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }
        public static explicit operator double(Money money)
        {
            return (double)money.Value;
        }
        public static explicit operator float(Money money)
        {
            return (float)money.Value;
        }
    }

    public class Tank
    {
        public readonly int Capacity;
        private int _level;
        public Tank(int capacity)
        {
            Capacity = capacity;
        }
        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value < 0 || value > Capacity)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _level = value;
            }
        }
        public bool refuel(int amount)
        {
            if (amount < 0)
            {
                return false;
            }
            if (_level + amount > Capacity)
            {
                return false;
            }
            _level += amount;
            return true;
        }

        public bool consume(int amount)
        {
            if (amount <= 0)
                return false;
            if (amount > Level)
                return false;
            Level -= amount;
            return true;
        }

        public bool refuel(Tank sourceTank, int amount)
        {
            if (sourceTank.Level <= 0)
                return false;
            if (sourceTank.Level - amount < 0)
                return false;
            if (amount + Level > Capacity)
                return false;

            sourceTank.Level -= amount;
            Level += amount;
            return true;
        }
    }

    class Student : IComparable
    {
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public decimal Średnia { get; set; }

        public int CompareTo(Student? otherStudent)
        {
            if (ReferenceEquals(this, otherStudent))
                return 0;
            if (ReferenceEquals(null, otherStudent))
                return 1;

            var surnameComparision = Nazwisko.CompareTo(otherStudent.Nazwisko);
            var nameComparision = Imie.CompareTo(otherStudent.Imie);

            if (surnameComparision != 0)
                return surnameComparision;

            else if (nameComparision != 0)
                return nameComparision;

            else
                return Średnia.CompareTo(otherStudent.Średnia);
        }
        public int CompareTo(object other)
        {
            if (ReferenceEquals(this, other))
                return 0;
            if (ReferenceEquals(null, other))
                return 1;
            return CompareTo((Student)other);
        }

        public override string ToString()
        {
            return $"{Nazwisko}, {Imie}, {Średnia}";
        }
    }

    public static class MoneyExtension
    {
        public static Money ToCurrency(this Money money, Currency currency, decimal scaler)
        {
            if(money.Currency == currency)
                return Money.OfWithException(money.Value * scaler, currency);

            return Money.OfWithException(Math.Round(money.Value / scaler, 2), currency);
        }
    }
}

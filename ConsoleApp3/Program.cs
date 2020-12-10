using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Транспортное средство, Управление авто, Машина, Двигатель, Разумное существо, Человек, Трансформер;
namespace Lab5
{
    abstract class ThinkingCreature
    {
        public abstract void Think();
    }
    interface CarControl
    {
        bool CheckStatus();
        void MoveForward();
        void MoveBack();
        void TurnLeft();
        void TurnRight();
        void Stop();
    }
    abstract class Vehicle
    {
        public int WheelsNumber { get; set; }
        public abstract bool CheckStatus();
    }

    class Car : Vehicle
    {
        public bool IsActive { get; set; }
        public string Model { get; set; }
        public int Speed { get; set; }
        public string ManufacturerCountry { get; set; }
        public int Cost { get; set; }
        public Car()
        {
            Model = "Toyota Camry";
            Speed = 100;
            ManufacturerCountry = "Japan";
            Cost = 10000;
            IsActive = false;
        }
        public Car(string model, int speed, string manufacturerCountry, int cost, bool isActive)
        {
            Model = model;
            Speed = speed;
            ManufacturerCountry = manufacturerCountry;
            Cost = cost;
            IsActive = isActive;
        }
        override public bool CheckStatus()
        {
            return IsActive;
        }
        public override string ToString()
        {
            return
                $"This is an object of {this.GetType()} " +
                $"type, with {this.GetHashCode()} hashcode.\n" +
                $"Model: {Model}\n" +
                $"Speed: {Speed}\n" +
                $"Cost: {Cost}\n" +
                $"TurnedOn:({IsActive})\n";

        }
    }
    class CarWithControl : Car, CarControl
    {

        public void MoveForward()
        {
            Console.WriteLine("The car is moving forward");
        }
        public void MoveBack()
        {
            Console.WriteLine("The car is moving back");
        }
        public void TurnLeft()
        {
            Console.WriteLine("The car is turning left");
        }
        public void TurnRight()
        {
            Console.WriteLine("The car is turning right");
        }
        public void Stop()
        {
            Console.WriteLine("The car has stopped!");
        }
    }
    sealed class Engine
    {
        public int Capacity { get; set; }
    }
    class Human : Unit
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Human()
        {
            Name = "undefined Name";
            Surname = "undefined SecondName";
        }
        public Human(string name, string secondName, int dateofcreation)
        {
            Name = name;
            Surname = secondName;
            if (DateOfCreation < 1920)
            {
                throw new AgeException("Человек старше 100 лет? Не может такого быть", dateofcreation);
            }
            else
            {
                DateOfCreation = dateofcreation;
            }
        }
        public override string ToString()
        {
            return
                $"This is an object of {this.GetType()} type, " +
                $"with {this.GetHashCode()} hashcode.\n" +
                $"Name of the human is {Name}\n" +
                $"Surname: {Surname}";
        }
    }
    class Transformer : Unit
    {
        public bool IsPreparedForBattle { get; set; }
        public int NumberOfGuns { get; set; }
        public void Shoot()
        {
            Console.WriteLine("Poof!");
        }
        public Transformer()
        {
            IsPreparedForBattle = false;
            NumberOfGuns = 0;
            Name = "undefined";
            DateOfCreation = 0;
            Capacity = 0;
        }
        public Transformer(bool isPreparedForBattle, int numberOfGuns, string name,int dateofcreation, int capacity)
        {
            IsPreparedForBattle = isPreparedForBattle;
            NumberOfGuns = numberOfGuns;
            if (name.Length==0)
            {
                throw new UnnamedUnitException("Объект без name");    
            }
            else
            {
                Name = name;
            }
            
            DateOfCreation = dateofcreation;
            Capacity = capacity;
        }
        public override string ToString()
        {
            return
                $"This is an object of {this.GetType()} " +
                $"type, with {this.GetHashCode()} hashcode.\n" +
                $"Transformer prepared for battle ({IsPreparedForBattle})\n" +
                $"Number of guns: {NumberOfGuns}\n";
        }
    }
    class Printer
    {
        public virtual string IAmPrinting(Object obj)
        {
            if (obj != null)
            {
                return obj.ToString();
            }
            return null;
        }
    }
    enum MilitaryRanks //перечисление 6 лабораторная
    {
        Soldier,
        Officer,
        General
    }
    struct Owner //структура 6 лабораторная
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public Owner(string name, string surname, int age)
        {
            Name = name;
            Surname = surname;
            Age = age;
        }
    }
    public class Unit
    {
        public int DateOfCreation { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
    public static class Army //класс-контейнер
    {
        public static List<Unit> combatUnit = new List<Unit>();

        public static void AddElem(Unit elem) //добавление элемента
        {
            if (elem.DateOfCreation > 2002)
            {
                throw new AgeException("Лицам до 18 в армию нельзя", elem.DateOfCreation);
            }
            else
            {
                combatUnit.Add(elem);
            }
            
        }
        public static void DeleteElem(Unit elem) //удаление элемента
        {
            if (combatUnit.IndexOf(elem) == -1)
            {
                throw new NotAtListException("Нет в списке армии", elem);
            }
            else
            {
                combatUnit.Remove(elem);
            }
           
        }
        public static void ShowElem() //вывод списка на консольное окно   
        {
            Console.WriteLine("***************************************************************");
            foreach (object obj in combatUnit)
            {
                Console.WriteLine($"{obj} ");
            }
            Console.WriteLine("***************************************************************");
        }
    }
    public partial class Controller //класс-контроллер
    {
        static int AmountOfUnits = 0;
        public static void OutbutByGivenYear()
        {
            Console.Write("Введите год: ");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.Write("Боевые единицы заданного года рождения (создания): ");
            foreach (Unit i in Army.combatUnit)
            {
                if (i.DateOfCreation == year)
                    Console.WriteLine(i.Name);
                Console.WriteLine();
            }
        }
        public static void OutputOfTransformerByCapacity()
        {
            Console.Write("Введите мощность: ");
            int power = Convert.ToInt32(Console.ReadLine());
            Console.Write("Имена трансформеров заданной мощности: "); 
                foreach (Unit i in Army.combatUnit)
            {
                if (i.Capacity == power)
                {
                    Console.WriteLine(i.Name);
                }
            }
            Console.WriteLine();
        }
        
        public static void AmountOfCombatUnits()
        {
            foreach (Unit x in Army.combatUnit)
            {
                AmountOfUnits++;
            }
            Console.WriteLine("Количество боевых единиц: " + AmountOfUnits);
        }
    }
    class AgeException : Exception
    {
        public int Value { get; }
        public AgeException(string message, int val)
        : base(message)
        {
            Value = val;
        }
    }
    class NotAtListException : Exception
    {
        public object Value { get; }
        public NotAtListException(string message, object val)
        : base(message)
        {
            Value = val;
        }
    }
    class UnnamedUnitException : Exception
    {
        public object Value { get; }
        public UnnamedUnitException(string message)
        : base(message){ }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Human oHuman1 = new Human();
            Human oHuman2 = new Human("Angelina", "Draguts");
            Human refHuman2 = oHuman2 as Human;

            Car oCar1 = new Car();
            Car oCar2 = new Car("Volvo XC90", 220, "Germany", 80000, true);
            Car refCar2 = oCar2 as Car;

            try { 
            Transformer oTransfromer1 = new Transformer();
            Transformer oTransfromer2 = new Transformer(true,4,"Serega", 2000, 5000);
            Transformer refTransfromer2 = oTransfromer2 as Transformer;
            Transformer unit1 = new Transformer(true, 5, "Bee", 1976, 10000);
            Transformer unit2 = new Transformer(true, 2, "Autobot", 1980, 15000);
            Transformer unit3 = new Transformer(true, 5, "Vasya", 2003, 10000);
            

            //Printer oPrinter = new Printer();
            //Object[] arr = { refHuman2, refCar2, refTransfromer2 };

            //foreach (object element in arr)
            //Console.WriteLine(oPrinter.IAmPrinting(element)+ "\n_____________________________\n");
            try
            {
                Army.AddElem(oTransfromer2);
                Army.AddElem(unit1);
                Army.AddElem(unit2);
                Army.AddElem(oHuman2);
                Army.AddElem(unit3);
            }
            catch (AgeException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine($"Некорректное значение: {ex.Value}");
            }

            Army.ShowElem();
            Console.WriteLine();

            //Controller.AmountOfCombatUnits();
            //Console.WriteLine();

            //Controller.OutbutByGivenYear();
            //Console.WriteLine();

            //Controller.OutputOfTransformerByCapacity();
            //Console.WriteLine();
            try
            {
                Army.DeleteElem(unit1);
                Army.DeleteElem(unit3); 
            }
            catch(NotAtListException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine($"Некорректное значение: {ex.Value}");
            }
           
            Army.ShowElem();
            
            }
            catch (UnnamedUnitException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
}

using System;

namespace TouristManagement
{
    // Базовий клас Tour
    public class Tour
    {
        public string Route { get; set; } = "Стандартний маршрут";
        public int Days { get; set; } = 7;
        public string PlanDetails { get; set; } = "Стандартне планування";

        // Віртуальний метод для планування поїздки
        public virtual void Plan()
        {
            Console.WriteLine($"Планування поїздки: {PlanDetails}");
        }

        // Метод для перевірки кількості днів
        public void ValidateDays()
        {
            if (Days <= 0)
            {
                throw new ArgumentException("Кількість днів повинна бути більше 0.");
            }
        }

        // Перевантажені методи для розрахунку вартості
        public virtual double CalculateCost(int days)
        {
            return days * 50; // Базова вартість за день
        }

        public virtual double CalculateCost(int days, string tourType)
        {
            double baseCost = CalculateCost(days);
            switch (tourType)
            {
                case "ExcursionTour":
                    return baseCost + 100;
                case "SkiTour":
                    return baseCost + 150;
                case "Cruise":
                    return baseCost + 200;
                default:
                    return baseCost;
            }
        }
    }

    // Клас для екскурсійного туру
    public class ExcursionTour : Tour
    {
        public override void Plan()
        {
            if (PlanDetails == "Стандартне планування")
            {
                PlanDetails = "Відвідування культурних об'єктів";
            }
            base.Plan();
        }
    }

    // Клас для гірськолижного туру
    public class SkiTour : Tour
    {
        public override void Plan()
        {
            if (PlanDetails == "Стандартне планування")
            {
                PlanDetails = "Спортивні активності";
            }
            base.Plan();
        }
    }

    // Клас для круїзу
    public class Cruise : Tour
    {
        public override void Plan()
        {
            if (PlanDetails == "Стандартне планування")
            {
                PlanDetails = "Маршрути та екскурсії на суші";
            }
            base.Plan();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (running)
            {
                // Меню вибору типу поїздки
                Console.WriteLine("Оберіть тип поїздки:");
                Console.WriteLine("1. Екскурсійний тур");
                Console.WriteLine("2. Гірськолижний тур");
                Console.WriteLine("3. Круїз");
                Console.WriteLine("4. Вихід");

                string choice = Console.ReadLine();
                Tour selectedTour = null;

                switch (choice)
                {
                    case "1":
                        selectedTour = new ExcursionTour();
                        break;
                    case "2":
                        selectedTour = new SkiTour();
                        break;
                    case "3":
                        selectedTour = new Cruise();
                        break;
                    case "4":
                        running = false;
                        continue;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        continue;
                }

                // Пропозиція використовувати стандартні дані або ввести свої
                Console.WriteLine("Оберіть опцію:");
                Console.WriteLine("1. Використати стандартні дати та маршрут");
                Console.WriteLine("2. Ввести власні дати та маршрут");

                string dataChoice = Console.ReadLine();

                if (dataChoice == "1")
                {
                    // Використання стандартних даних
                    Console.WriteLine($"Використовуються стандартні дати (7 днів) та маршрут ({selectedTour.Route}).");
                }
                else if (dataChoice == "2")
                {
                    // Введення власних даних
                    Console.WriteLine("Введіть кількість днів:");
                    int days;
                    while (!int.TryParse(Console.ReadLine(), out days) || days <= 0)
                    {
                        Console.WriteLine("Кількість днів повинна бути більше 0. Спробуйте ще раз.");
                    }
                    selectedTour.Days = days;

                    Console.WriteLine("Введіть маршрут:");
                    selectedTour.Route = Console.ReadLine();

                    // Введення деталізованого планування
                    Console.WriteLine("Введіть деталі планування поїздки:");
                    selectedTour.PlanDetails = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    continue;
                }

                // Планування поїздки
                selectedTour.Plan();

                // Розрахунок вартості
                Console.WriteLine("Оберіть метод розрахунку вартості:");
                Console.WriteLine("1. Розрахунок на основі кількості днів");
                Console.WriteLine("2. Розрахунок на основі кількості днів і типу поїздки");

                string costChoice = Console.ReadLine();
                double cost = 0;

                switch (costChoice)
                {
                    case "1":
                        cost = selectedTour.CalculateCost(selectedTour.Days);
                        break;
                    case "2":
                        cost = selectedTour.CalculateCost(selectedTour.Days, selectedTour.GetType().Name);
                        break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        continue;
                }

                Console.WriteLine($"Загальна вартість поїздки: {cost} грн");

                Console.WriteLine("Бажаєте продовжити? (так/ні)");
                string continueChoice = Console.ReadLine().ToLower();
                if (continueChoice != "так")
                {
                    running = false;
                }
            }

            Console.WriteLine("Програма завершена.");
        }
    }
}

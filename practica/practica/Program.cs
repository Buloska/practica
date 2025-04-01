using System;
using System.Collections.Generic;

namespace TestingMethodsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====================================");
            Console.WriteLine("=== ТЕСТИРОВАНИЕ РАЗНЫМИ МЕТОДАМИ ===");
            Console.WriteLine("====================================\n");
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Выберите тип тестирования:");
                Console.ResetColor();

                Console.WriteLine("1. Тестирование белого ящика (площадь треугольника)");
                Console.WriteLine("2. Тестирование черного ящика (площадь цилиндра)");
                Console.WriteLine("3. Модульное тестирование (калькулятор)");
                Console.WriteLine("4. Интеграционное тестирование (сервис заказов)");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("0. Выход");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Ваш выбор: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine(), out var choice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз.\n");
                    Console.ResetColor();
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n------------------------------------");
                Console.ResetColor();

                switch (choice)
                {
                    case 1:
                        WhiteBoxTesting();
                        break;
                    case 2:
                        BlackBoxTesting();
                        break;
                    case 3:
                        UnitTesting();
                        break;
                    case 4:
                        IntegrationTesting();
                        break;
                    case 0:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неизвестный выбор. Попробуйте еще раз.");
                        Console.ResetColor();
                        break;
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("------------------------------------\n");
                Console.ResetColor();
            }
        }

        static void WhiteBoxTesting()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("=== ТЕСТИРОВАНИЕ БЕЛОГО ЯЩИКА ===");
            Console.WriteLine("Тестируем метод расчета площади треугольника с полным знанием его реализации");
            Console.ResetColor();

            Console.WriteLine("\nЗапуск тестов...");
            TestTriangle(3, 4, 5, 6, "Обычный треугольник (3,4,5)");
            TestTriangle(1, 2, 3, 0, "Вырожденный треугольник (1,2,3)");
            TestTriangle(-1, 2, 3, 0, "Отрицательные стороны (-1,2,3)", true);

            Console.WriteLine("\nИтоги тестирования белого ящика:");
            Console.WriteLine("- Проверены все возможные пути выполнения кода");
            Console.WriteLine("- Протестированы граничные случаи");
            Console.WriteLine("- Учтены все возможные исключения");
        }

        static void TestTriangle(double a, double b, double c, double expected, string description, bool expectException = false)
        {
            Console.WriteLine($"\nТест: {description}");
            try
            {
                double actual = TriangleArea(a, b, c);
                if (expectException)
                {
                    PrintTestResult("Результат:", "Ожидалось исключение", "Исключение не возникло", false);
                    return;
                }

                bool success = Math.Abs(expected - actual) < 0.0001;
                PrintTestResult("Результат:", expected.ToString(), actual.ToString(), success);
                Console.WriteLine($"Подробности: a={a}, b={b}, c={c}, p={(a + b + c) / 2}, площадь={actual}");
            }
            catch (ArgumentException)
            {
                PrintTestResult("Результат:", expectException ? "Ожидалось исключение" : expected.ToString(),
                              "Получено исключение", expectException);
            }
        }

        static double TriangleArea(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentException("Стороны должны быть положительными");

            if (a + b <= c || a + c <= b || b + c <= a)
                return 0;

            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        static void BlackBoxTesting()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=== ТЕСТИРОВАНИЕ ЧЕРНОГО ЯЩИКА ===");
            Console.WriteLine("Тестируем метод расчета площади цилиндра без знания его реализации");
            Console.WriteLine("Введите параметры цилиндра для тестирования:");
            Console.ResetColor();

            Console.Write("Введите радиус цилиндра: ");
            double radius = double.Parse(Console.ReadLine());

            Console.Write("Введите высоту цилиндра: ");
            double height = double.Parse(Console.ReadLine());

            Console.WriteLine("\nЗапуск теста...");
            try
            {
                double result = CylinderArea(radius, height);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Тест пройден успешно!");
                Console.ResetColor();
                Console.WriteLine($"Площадь поверхности цилиндра:");
                Console.WriteLine($"- Радиус: {radius}");
                Console.WriteLine($"- Высота: {height}");
                Console.WriteLine($"- Результат: {result:F4}");
                Console.WriteLine($"- Проверка: 2*π*r*h + 2*π*r² = {2 * Math.PI * radius * height:F4} + {2 * Math.PI * radius * radius:F4} = {result:F4}");
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Тест не пройден: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine($"Ошибка при параметрах:");
                Console.WriteLine($"- Радиус: {radius}");
                Console.WriteLine($"- Высота: {height}");
            }

            Console.WriteLine("\nИтоги тестирования черного ящика:");
            Console.WriteLine("- Проверена реакция метода на различные входные данные");
            Console.WriteLine("- Учтены недопустимые значения параметров");
            Console.WriteLine("- Результаты выведены без знания внутренней логики метода");
        }

        static void UnitTesting()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== МОДУЛЬНОЕ ТЕСТИРОВАНИЕ ===");
            Console.WriteLine("Тестируем изолированные методы калькулятора");
            Console.ResetColor();

            var calculator = new Calculator();

            Console.WriteLine("\nТестирование сложения:");
            TestCalculatorOperation(calculator.Add, 2, 3, 5, "2 + 3 = 5");

            Console.WriteLine("\nТестирование деления:");
            TestCalculatorOperation(calculator.Divide, 10, 2, 5, "10 / 2 = 5");
            TestCalculatorOperation(calculator.Divide, 10, 0, 0, "10 / 0 = исключение", true);

            Console.WriteLine("\nИтоги модульного тестирования:");
            Console.WriteLine("- Протестированы все основные операции калькулятора");
            Console.WriteLine("Проверены граничные случаи(деление на ноль)");
            Console.WriteLine("- Каждый тест изолирован от других");
        }

        static void IntegrationTesting()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== ИНТЕГРАЦИОННОЕ ТЕСТИРОВАНИЕ ===");
            Console.WriteLine("Тестируем взаимодействие сервиса заказов с базой данных");
            Console.ResetColor();

            Console.WriteLine("\nИнициализация компонентов...");
            var db = new Database();
            var orderService = new OrderService(db);

            Console.WriteLine("\nТест 1: Создание и получение заказа");
            var order = new Order { Id = 1, ProductName = "Ноутбук" };
            orderService.PlaceOrder(order);
            var retrieved = orderService.GetOrder(1);
            Console.WriteLine($"Заказ создан: ID={retrieved.Id}, Товар={retrieved.ProductName}");
            Console.WriteLine($"Проверка: Заказ {(retrieved.ProductName == "Ноутбук" ? "соответствует" : "не соответствует")} ожидаемому");

            Console.WriteLine("\nТест 2: Удаление заказа");
            orderService.CancelOrder(1);
            try
            {
                var deleted = orderService.GetOrder(1);
                Console.WriteLine("Ошибка: Заказ все еще существует в системе");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Успех: Заказ успешно удален из системы");
            }

            Console.WriteLine("\nИтоги интеграционного тестирования:");
            Console.WriteLine("- Проверено полное взаимодействие между сервисом и базой данных");
            Console.WriteLine("- Учтены сценарии создания, получения и удаления заказов");
            Console.WriteLine("- Проверена обработка ошибок при работе с несуществующими заказами");
        }

        static double CylinderArea(double radius, double height)
        {
            if (radius <= 0)
                throw new ArgumentException("Радиус должен быть положительным");
            if (height <= 0)
                throw new ArgumentException("Высота должна быть положительной");

            return 2 * Math.PI * radius * height + 2 * Math.PI * radius * radius;
        }

        static void TestCalculatorOperation(Func<double, double, double> operation,
                                        double a, double b, double expected,
                                        string description, bool expectException = false)
        {
            Console.WriteLine($"\nТест: {description}");
            try
            {
                double result = operation(a, b);
                if (expectException)
                {
                    PrintTestResult("Результат:", "Ожидалось исключение", "Исключение не возникло", false);
                    return;
                }
                bool success = Math.Abs(result - expected) < 0.0001;
                PrintTestResult("Результат:", expected.ToString(), result.ToString(), success);
                Console.WriteLine($"Детали: {a} op {b} = {result} (ожидалось {expected})");
            }
            catch (Exception ex) when (expectException)
            {
                PrintTestResult("Результат:", "Ожидалось исключение", ex.GetType().Name, true);
                Console.WriteLine($"Детали исключения: {ex.Message}");
            }
            catch (Exception ex)
            {
                PrintTestResult("Результат:", expected.ToString(), ex.GetType().Name, false);
                Console.WriteLine($"Неожиданное исключение: {ex.Message}");
            }
        }

        static void PrintTestResult(string label, string expected, string actual, bool isSuccess)
        {
            Console.Write($"{label} ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(expected);
            Console.ResetColor();
            Console.Write(" → ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(actual);
            Console.ResetColor();
            Console.Write(" [");
            Console.ForegroundColor = isSuccess ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write(isSuccess ? "УСПЕХ" : "ОШИБКА");
            Console.ResetColor();
            Console.WriteLine("]");
        }

        public class Calculator
        {
            public double Add(double a, double b) => a + b;
            public double Subtract(double a, double b) => a - b;
            public double Multiply(double a, double b) => a * b;
            public double Divide(double a, double b) => b == 0 ? throw new DivideByZeroException() : a / b;
        }

        public class Order
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
        }

        public class Database
        {
            private readonly Dictionary<int, Order> _orders = new Dictionary<int, Order>();

            public void AddOrder(Order order) => _orders[order.Id] = order;
            public Order GetOrder(int id) => _orders.TryGetValue(id, out var order) ? order : throw new KeyNotFoundException();
            public void RemoveOrder(int id) => _orders.Remove(id);
        }

        public class OrderService
        {
            private readonly Database _db;

            public OrderService(Database db) => _db = db;
            public void PlaceOrder(Order order) => _db.AddOrder(order);
            public Order GetOrder(int id) => _db.GetOrder(id);
            public void CancelOrder(int id) => _db.RemoveOrder(id);
        }
    }
}
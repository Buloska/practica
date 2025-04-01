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
            Console.WriteLine("=== ������������ ������� �������� ===");
            Console.WriteLine("====================================\n");
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("�������� ��� ������������:");
                Console.ResetColor();

                Console.WriteLine("1. ������������ ������ ����� (������� ������������)");
                Console.WriteLine("2. ������������ ������� ����� (������� ��������)");
                Console.WriteLine("3. ��������� ������������ (�����������)");
                Console.WriteLine("4. �������������� ������������ (������ �������)");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("0. �����");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("��� �����: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine(), out var choice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("������������ ����. ���������� ��� ���.\n");
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
                        UnitTestingDemo();
                        break;
                    case 4:
                        IntegrationTestingDemo();
                        break;
                    case 0:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("����������� �����. ���������� ��� ���.");
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
            Console.WriteLine("=== ������������ ������ ����� ===");
            Console.WriteLine("����� ���������� ������� ������������ �� ������� ������\n");
            Console.ResetColor();

            TestTriangle(3, 4, 5, 6, "���� 1 (3,4,5)");

            TestTriangle(2, 2, 2, Math.Sqrt(3), "���� 2 (2,2,2)");

            TestTriangle(1, 2, 3, 0, "���� 3 (1,2,3)");

            try
            {
                double actual = TriangleArea(-1, 2, 3);
                PrintTestResult("���� 4 (-1,2,3)", "��������� ����������", actual.ToString(), false);
            }
            catch (ArgumentException)
            {
                PrintTestResult("���� 4 (-1,2,3)", "����������", "����������", true);
            }
        }

        static void TestTriangle(double a, double b, double c, double expected, string testName)
        {
            try
            {
                double actual = TriangleArea(a, b, c);
                bool success = Math.Abs(expected - actual) < 0.0001;
                PrintTestResult(testName, expected.ToString(), actual.ToString(), success);
            }
            catch (Exception ex)
            {
                PrintTestResult(testName, "���������� ���������", ex.Message, false);
            }
        }

        static double TriangleArea(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentException("������� ������ ���� ��������������");

            if (a + b <= c || a + c <= b || b + c <= a)
                return 0;

            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        static void BlackBoxTesting()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=== ������������ ������� ����� ===");
            Console.WriteLine("����� ���������� ������� ����������� ��������\n");
            Console.ResetColor();

            TestCylinder(2, 3, 2 * Math.PI * 2 * 3 + 2 * Math.PI * 2 * 2, "���� 1 (r=2,h=3)");

            try
            {
                double area = CylinderArea(2, 0);
                PrintTestResult("���� 2 (r=2,h=0)", "����������", area.ToString(), false);
            }
            catch (ArgumentException)
            {
                PrintTestResult("���� 2 (r=2,h=0)", "����������", "����������", true);
            }

            try
            {
                double area = CylinderArea(-1, 5);
                PrintTestResult("���� 3 (r=-1,h=5)", "����������", area.ToString(), false);
            }
            catch (ArgumentException)
            {
                PrintTestResult("���� 3 (r=-1,h=5)", "����������", "����������", true);
            }

            TestCylinder(100, 200, 2 * Math.PI * 100 * 200 + 2 * Math.PI * 100 * 100, "���� 4 (r=100,h=200)");
        }

        static void TestCylinder(double r, double h, double expected, string testName)
        {
            try
            {
                double actual = CylinderArea(r, h);
                bool success = Math.Abs(expected - actual) < 0.0001;
                PrintTestResult(testName, expected.ToString(), actual.ToString(), success);
            }
            catch (Exception ex)
            {
                PrintTestResult(testName, "���������� ���������", ex.Message, false);
            }
        }

        static double CylinderArea(double radius, double height)
        {
            if (radius <= 0)
                throw new ArgumentException("������ ������ ���� �������������");
            if (height <= 0)
                throw new ArgumentException("������ ������ ���� �������������");

            return 2 * Math.PI * radius * height + 2 * Math.PI * radius * radius;
        }

        static void UnitTestingDemo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== ��������� ������������ ===");
            Console.WriteLine("������������ ������ Calculator\n");
            Console.ResetColor();

            Calculator calculator = new Calculator();

            TestCalculatorOperation(calculator.Add, 5, 3, 8, "�������� (5+3)");
            TestCalculatorOperation(calculator.Add, -5, -3, -8, "�������� (-5 + -3)");

            TestCalculatorOperation(calculator.Subtract, 10, 4, 6, "��������� (10-4)");
            TestCalculatorOperation(calculator.Subtract, 4, 10, -6, "��������� (4-10)");

            
            TestCalculatorOperation(calculator.Multiply, 6, 7, 42, "��������� (6*7)");
            TestCalculatorOperation(calculator.Multiply, -6, 7, -42, "��������� (-6*7)");

            
            TestCalculatorOperation(calculator.Divide, 15, 3, 5, "������� (15/3)");
            TestCalculatorOperation(calculator.Divide, 10, 4, 2.5, "������� (10/4)");

            
            try
            {
                double result = calculator.Divide(10, 0);
                PrintTestResult("������� �� ���� (10/0)", "����������", result.ToString(), false);
            }
            catch (DivideByZeroException)
            {
                PrintTestResult("������� �� ���� (10/0)", "����������", "����������", true);
            }
        }

        static void TestCalculatorOperation(Func<double, double, double> operation, double a, double b, double expected, string testName)
        {
            try
            {
                double actual = operation(a, b);
                bool success = Math.Abs(expected - actual) < 0.0001;
                PrintTestResult(testName, expected.ToString(), actual.ToString(), success);
            }
            catch (Exception ex)
            {
                PrintTestResult(testName, "���������� ���������", ex.Message, false);
            }
        }

        static void IntegrationTestingDemo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== �������������� ������������ ===");
            Console.WriteLine("������������ �������������� OrderService � Database\n");
            Console.ResetColor();

            var database = new Database();
            var orderService = new OrderService(database);

            
            var order1 = new Order { Id = 1, ProductName = "�������", Quantity = 1, Price = 50000 };
            orderService.PlaceOrder(order1);
            PrintTestResult("���� 1: �������� ������", "�����", "�����", true);

            
            var retrievedOrder = orderService.GetOrder(1);
            bool success = retrievedOrder != null && retrievedOrder.ProductName == "�������";
            PrintTestResult("���� 2: ��������� ������", "�������", retrievedOrder?.ProductName ?? "null", success);

            
            orderService.CancelOrder(1);
            try
            {
                var deletedOrder = orderService.GetOrder(1);
                PrintTestResult("���� 3: �������� ������", "����������", deletedOrder.ToString(), false);
            }
            catch (KeyNotFoundException)
            {
                PrintTestResult("���� 3: �������� ������", "�����", "�����", true);
            }

            
            try
            {
                var nonExistentOrder = orderService.GetOrder(999);
                PrintTestResult("���� 4: �������������� �����", "����������", nonExistentOrder.ToString(), false);
            }
            catch (KeyNotFoundException)
            {
                PrintTestResult("���� 4: �������������� �����", "����������", "����������", true);
            }
        }

        static void PrintTestResult(string testName, string expected, string actual, bool isSuccess)
        {
            Console.Write($"{testName}: ��������� ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(expected);
            Console.ResetColor();
            Console.Write(", �������� ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(actual);
            Console.ResetColor();
            Console.Write(" - ");

            if (isSuccess)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("�����");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("������");
            }
            Console.ResetColor();
        }
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
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class Database
    {
        private readonly Dictionary<int, Order> _orders = new Dictionary<int, Order>();

        public void AddOrder(Order order) => _orders[order.Id] = order;
        public Order GetOrder(int id) => _orders.TryGetValue(id, out var order) ? order : throw new KeyNotFoundException();
        public void RemoveOrder(int id) { if (!_orders.Remove(id)) throw new KeyNotFoundException(); }
    }

    public class OrderService
    {
        private readonly Database _database;
        public OrderService(Database database) => _database = database;
        public void PlaceOrder(Order order) => _database.AddOrder(order);
        public Order GetOrder(int id) => _database.GetOrder(id);
        public void CancelOrder(int id) => _database.RemoveOrder(id);
    }
}
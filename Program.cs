using System;

namespace EllipseTask
{
    // 🔹 Базовий клас: крива другого порядку
    // Загальне рівняння: a11*x^2 + 2*a12*x*y + a22*y^2 + b1*x + b2*y + c = 0
    class QuadraticCurve
    {
        protected double a11, a12, a22, b1, b2, c;

        // Встановити коефіцієнти
        public virtual void SetCoefficients(double a11, double a12, double a22, double b1, double b2, double c)
        {
            this.a11 = a11;
            this.a12 = a12;
            this.a22 = a22;
            this.b1 = b1;
            this.b2 = b2;
            this.c = c;
        }

        // Вивести коефіцієнти
        public virtual void PrintCoefficients()
        {
            Console.WriteLine("Коефіцієнти кривої другого порядку:");
            Console.WriteLine($"a11 = {a11}, a12 = {a12}, a22 = {a22}, b1 = {b1}, b2 = {b2}, c = {c}");
        }

        // Перевірка належності точки кривій
        public virtual bool IsPointOnCurve(double x, double y, double eps = 1e-6)
        {
            double val = a11 * x * x + 2 * a12 * x * y + a22 * y * y + b1 * x + b2 * y + c;
            return Math.Abs(val) < eps;
        }
    }

    // 🔹 Похідний клас: Еліпс
    // Рівняння: (x^2 / a^2) + (y^2 / b^2) = 1
    class Ellipse : QuadraticCurve
    {
        private double a, b; // півосі

        public Ellipse(double a, double b)
        {
            SetAxes(a, b);
        }

        public void SetAxes(double a, double b)
        {
            if (a <= 0 || b <= 0)
                throw new ArgumentException("Півосі a і b повинні бути додатними.");

            this.a = a;
            this.b = b;

            // Перетворення в загальне рівняння
            // (1/a^2) * x^2 + (1/b^2) * y^2 - 1 = 0
            SetCoefficients(1.0 / (a * a), 0, 1.0 / (b * b), 0, 0, -1);
        }

        public override void PrintCoefficients()
        {
            Console.WriteLine("Еліпс:");
            Console.WriteLine($"Рівняння: x^2/{a * a} + y^2/{b * b} = 1");
            base.PrintCoefficients();
        }

        public bool IsPointOnEllipse(double x, double y, double eps = 1e-6)
        {
            double val = (x * x) / (a * a) + (y * y) / (b * b);
            return Math.Abs(val - 1) < eps;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть півосі еліпса:");
            Console.Write("a = ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("b = ");
            double b = double.Parse(Console.ReadLine());

            Ellipse ellipse = new Ellipse(a, b);
            ellipse.PrintCoefficients();

            Console.WriteLine("\nВведіть координати точки:");
            Console.Write("x = ");
            double x = double.Parse(Console.ReadLine());
            Console.Write("y = ");
            double y = double.Parse(Console.ReadLine());

            if (ellipse.IsPointOnEllipse(x, y))
                Console.WriteLine($"Точка ({x}, {y}) належить еліпсу.");
            else
                Console.WriteLine($"Точка ({x}, {y}) не належить еліпсу.");
        }
    }
}


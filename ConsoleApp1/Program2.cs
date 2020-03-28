using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    /// <summary>
    ///Представляет функциию  
    /// </summary>
    [XmlIncludeAttribute(typeof(Line)), XmlIncludeAttribute(typeof(Hyperbola)), XmlIncludeAttribute(typeof(Kub))]
    [Serializable]
    
    public abstract  class  Function
    {
        /// <summary>
        /// Расчитывает значение функции
        /// </summary>
        /// <param name="x">Значение для которого расчитывается функция</param>
        /// <returns>Значение функции</returns>
        public abstract double Count(double x);
    }
    /// <summary>
    /// Представлет функцию Линия
    /// </summary>  
    [Serializable]
    public class Line : Function
    {
        /// <summary>
        /// Коэффициент при x
        /// </summary>
        public readonly double A;
        /// <summary>
        /// Коэффициент прибавляемый к х
        /// </summary>
        public readonly double B;

        public Line(double a, double b)
        {
            A = a;
            B = b;
        }
        public Line()
        {
            A = 0;
            B = 0;
        }
        /// <summary>
        /// Расчитывает значение функции
        /// </summary>
        /// <param name="x">Значение для которого расчитывается функция</param>
        /// <returns>Значение функции</returns>
        public override double Count(double x)
        {

            return A * x + B;
        }
    }
    /// <summary>
    /// Представляет функцию Куб
    /// </summary>
    [Serializable]
    public class Kub : Function
    { 
        /// <summary>
        /// Коэффициент при х^2
        /// </summary>
        public readonly double A;
        /// <summary>
        /// Коэффициент при х
        /// </summary>
        public readonly double B;
        /// <summary>
        /// Коэффициент прибавляемый к х
        /// </summary>
        public readonly double C;

        public Kub(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }
        public Kub()
        {
            A = 0;
            B = 0;
            C = 0;
        }
        /// <summary>
        /// Расчитывает значение функции
        /// </summary>
        /// <param name="x">Значение для которого расчитывается функция</param>
        /// <returns>Значение функции</returns>
        public override double Count(double x)
        {
            return A * x * x + B * x + C;
        }
    }
    /// <summary>
    /// Представляет функцию Гипербола
    /// </summary>
    [Serializable]
    public class Hyperbola : Function
    {
        /// <summary>
        /// Коэффициент
        /// </summary>
        public readonly double A;
        public Hyperbola(double a)
        {
            A = a;
        }
        public Hyperbola()
        {
            A = 0;
        }
        /// <summary>
        /// Расчитывает значение функции
        /// </summary>
        /// <param name="x">Значение для которого расчитывается функция</param>
        /// <returns>Значение функции</returns>
        public override double Count(double x)
        {
            return A / x;
        }
    }
    class Program
    {

        /// <param name="pathinput">путь к файлу input.txt</param>
        static string pathinput = @"C:\Users\Алина\Documents\input.txt";

       
        static void Main(string[] args)
        {

            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
            Trace.Indent();
            Trace.WriteLine("Начало работы программы");
            string inputFile = @"C:\Users\Алина\Documents\input.txt";
            string serialFile = @"C:\Users\Алина\Documents\output.txt";
            string[] input = File.ReadAllLines(inputFile);
            TextWriter serialWriter = new StreamWriter(serialFile);
            XmlSerializer ser = new XmlSerializer(typeof(Function));
            Trace.WriteLine("Файл прочитан");
            double x = Convert.ToDouble(input[0]);
            string[] tmp;
            List<Function> f = new List<Function>();
            Trace.WriteLine("Анализ данных");
            Trace.Indent();
            for (int i=1; i< input.Length; i++)
            {
                tmp = input[i].Split();
                switch(tmp[0])
                {
                    case "l":
                        f.Add(new Line(Convert.ToDouble(tmp[1]), Convert.ToDouble(tmp[2])));
                        Trace.WriteLine("Добавлена линия");
                        break;
                    case "h":
                        f.Add(new Hyperbola(Convert.ToDouble(tmp[1])));
                        Trace.WriteLine("Добавлена гипербола");
                        break;
                    case "k":
                        f.Add(new Kub(Convert.ToDouble(tmp[1]), Convert.ToDouble(tmp[2]), Convert.ToDouble(tmp[3])));
                        Trace.WriteLine("Добавлен куб");
                        break;
                }
            }
            Trace.Unindent();
            Trace.WriteLine("Анализ данных завершен");
            Trace.WriteLine("Вывод данных на экран");
                      
            foreach (Function fun in f)
            {
                Console.WriteLine("значение функции {0} для x = {1} равно {2}", fun.GetType(), x, fun.Count(x));
                ser.Serialize(serialWriter, fun);
            }
            
            Trace.WriteLine("Завершение работы программы");          
            Console.ReadKey();
        }
    }
}

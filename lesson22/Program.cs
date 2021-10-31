using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lesson22
{
    class Program
    {
        //Сформировать массив случайных целых чисел(размер  задается пользователем).
        //Вычислить сумму чисел массива и максимальное число в массиве.
        //Реализовать решение  задачи с  использованием механизма  задач продолжения.

        static int[] GetArray()
        {
            Console.Write("Укажите размерность массива: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Random random = new Random();
            int[] array = new int[n];
            Console.Write("Массив чисел: [");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(-99, 99);
                Console.Write("  {0}", array[i]);
                Thread.Sleep(50);
            }
            Console.WriteLine("  ]");
            return array;
        }
        static void GetSum(object a)
        {
            int[] array = (int[])a;
            int sum = 0;
            Console.Write("Поэтапный подсчет суммы: ");
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
                Console.Write("  {0}",sum);
                Thread.Sleep(50);
            }
            Console.WriteLine();
            Console.WriteLine($"Сумма чисел: {sum}");
        }
        static void GetMax(Task task, object a)
        {
            int[] array = (int[])a;
            Console.Write("Поэтапное определение максимального числа: {0}", array[0]);
            int max =array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    Console.Write("  {0}", array[i]);
                    max = array[i];
                }
                Thread.Sleep(50);
            }
            Console.WriteLine();
            Console.WriteLine($"Максимальное число: {max}");
        }
        static void Main(string[] args)
        {
            int[] array = GetArray();

            Action<object> action = new Action<object>(GetSum);
            Task task1 = new Task(action, array);

            Action<Task, object> action1 = new Action<Task, object>(GetMax);
            Task task2 = task1.ContinueWith(action1, array);

            task1.Start();

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using List;

namespace _1_lb
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new CircularList<int>();

            list.Notify += GoMessage;

            list.Add(3);
            list.Add(21);
            list.Add(12);
            list.Add(14);
            list.Add(17);
            list.Add(89);
            list.Add(2);
            list.Add(3);
            list.Add(3);
            list.Add(11);
            list.Add(12);
            list.Add(13);

            Console.WriteLine(list.ElementAt(0));

            Console.WriteLine(list.GetFirst());

            Console.WriteLine(list.GetLast());

            int[] addingList = new int[5] { 1, 2, 3, 4, 5 };
            list.AddRange(addingList);
            OutPutList(list);

            list.RemoveAt(0);
            OutPutList(list);

            list.Remove(5);
            OutPutList(list);

            list.RemoveFirst();
            OutPutList(list);

            list.RemoveLast();
            OutPutList(list);

            list.Clear();
            OutPutList(list);

            Console.ReadKey();
        }
        static void OutPutList(CircularList<int> list)
        {
            int counter = 0;
            foreach (var x in list)
            {
                if (counter < list.Count)
                {
                    Console.Write(x.ToString() + " ");
                    counter++;
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine();
        }
        static void GoMessage(object e,string message)
        {
            Console.WriteLine(message);
        }
    }
}

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
            var list = new CircularList<int> { 1, 2, 3 };
            list.emptyListEvent += OutputMessageAddInt32Item;

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

            OutPutList(list);

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
            //Double
            var listD = new CircularList<double> { 1.2112, 2.3232, 3.43434 };
            listD.emptyListEvent += OutputMessageAddDoubleItem;

            listD.Add(3.23);
            listD.Add(21.1212);
            listD.Add(12.21);
            listD.Add(14.2332);
            listD.Add(17.424);
            listD.Add(89.2323);
            listD.Add(2.213);
            listD.Add(3.2323);
            listD.Add(3.233);
            listD.Add(11.2323);
            listD.Add(12.2323);
            listD.Add(13.1212545);

            OutPutListD(listD);

            Console.WriteLine(listD.ElementAt(0));

            Console.WriteLine(listD.GetFirst());

            Console.WriteLine(listD.GetLast());

            double[] addingListDouble = {2.323221, 2.1212, 312.23, 4.42, 5.121 };
            listD.AddRange(addingListDouble);
            OutPutListD(listD);

            listD.RemoveAt(0);
            OutPutListD(listD);

            listD.Remove(17.424);
            OutPutListD(listD);

            listD.RemoveFirst();
            OutPutListD(listD);

            listD.RemoveLast();
            OutPutListD(listD);

            listD.Clear();
            OutPutListD(listD);

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
        static void OutPutListD(CircularList<double> list)
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
        static void OutputMessageAddInt32Item(object e, CircleEventArgs args)
        {
            Console.WriteLine($"All element were deleted from {e.GetType()}. Length of list: {args.Count}");
        }
        static void OutputMessageAddDoubleItem(object e, CircleEventArgs args)
        {
            Console.WriteLine($"All element were deleted from {e.GetType()}. Length of list: {args.Count}");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircularList;

namespace _1_lb
{
    class Program
    {
        static void Main(string[] args)
        {
            var t_list = new CircularList<int>();
            t_list.AddRange(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 });
            OutPutList(t_list);
            t_list.Reverse();
            OutPutList(t_list);


            CircularList<Int32> newL = new CircularList<Int32>(new Int32[] { 1,2,3,4,5,6,7,8,9,10});
            newL.emptyListEvent += OutputMessageAddInt32Item;
            newL.Add(11);
            newL.AddRange(new Int32[] { 12, 13, 14 });
            newL.AddAt(1221, 12);
            newL.RemoveAt(0);
            newL.RemoveAt(13);
            OutPutList(newL);

            newL.Remove(1221);
            newL.RemoveFirst();
            newL.RemoveLast();
            OutPutList(newL);

            newL.Reverse();
            OutPutList(newL);
            Console.WriteLine(newL.Contains(8));
            Console.WriteLine(newL.Contains(13));

            Int32[] testArray = new Int32[] { 23, 23, 12, 12 };
            CircularList<Int32> newL2 = new CircularList<Int32>(new Int32[] {1,2});
            newL2.emptyListEvent += OutputMessageAddInt32Item;
            //OutPutList(newL2);
            //newL2.Remove(1);
            //OutPutList(newL2);
            //OutPutList(newL2);
            newL2.Reverse();
            OutPutList(newL2);
            //newL2.CopyTo(testArray, 2);
            //OutPutArrgs(testArray);

            //Int32 circular list

            int[] addingList = new int[] { 5, 4, 3, 2, 1, 22, 33, 44, 55, 66, 77, 88, 99, 101 };
            OutPutArrgs(addingList);

            /*CircularList<int> listCopy = new CircularList<int> { 1, 2, 3, 4, 5 };
            listCopy.CopyTo(addingList, 4);
             OutPutArrgs(addingList);
            */

            var list = new CircularList<int>();
            list.emptyListEvent += OutputMessageAddInt32Item;

            //Add values to the end of CircularList
            list.AddRange(addingList);
            OutPutList(list);

            //Add value to the list on position
            list.AddAt(12, 0);
            list.AddAt(21, list.Count - 1);
            list.AddAt(23, list.Count - 2);
            list.AddAt(8888, 7);
            OutPutList(list);
            //Contains
            //Console.WriteLine(list.Contains(1223));
            //Console.WriteLine(list.Contains(4));

            //Add-method
            /*list.Add(3);
            list.Add(21);
            list.Add(12);
            list.Add(14);

            OutPutList(list);*/

            //Get element for index
            //Console.WriteLine(list.ElementAt(0));

            //Get first element
            //Console.WriteLine(list.GetFirst());

            //Get last element
            //Console.WriteLine(list.GetLast());

            //Remove element from position
            /*list.RemoveAt(0);
            OutPutList(list);*/

            //Remove element with data
            /*list.Remove(5);
            OutPutList(list);*/

            //Remove first element
            /*list.RemoveFirst();
            OutPutList(list);*/

            //Remove last element
            /*list.RemoveLast();
            OutPutList(list);*/

            //Clear circular linker list
            /*list.Clear();
            OutPutList(list);*/

            //Double
            /*var listD = new CircularList<double>();
            listD.emptyListEvent += OutputMessageAddDoubleItem;

            double[] addingListDouble = { 2.323221, 2.1212, 312.23, 4.42, 5.121 };
            listD.AddRangeLast(addingListDouble);
            OutPutListD(listD);

            Console.WriteLine(listD.Contains(4.42));
            Console.WriteLine(listD.Contains(1223.21));

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

            listD.RemoveAt(0);
            OutPutListD(listD);

            listD.Remove(17.424);
            OutPutListD(listD);

            listD.RemoveFirst();
            OutPutListD(listD);

            listD.RemoveLast();
            OutPutListD(listD);

            listD.Clear();
            OutPutListD(listD);*/

            Console.ReadKey();
        }
        static void OutPutArrgs(int[] arrgs)
        {
            foreach (var elem in arrgs)
            {
                Console.Write(elem.ToString() + " ");
            }
            Console.WriteLine();
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
            Console.WriteLine($"List with type of {e.GetType()} is empty. Length of list: {args.Count}");
        }
        static void OutputMessageAddDoubleItem(object e, CircleEventArgs args)
        {
            Console.WriteLine($"List with type of {e.GetType()} is empty. Length of list: {args.Count}");
        }
    }
}
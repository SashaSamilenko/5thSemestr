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
            CircularList<Int32> list = new CircularList<Int32>(new int[] { 11, 22, 33, 44, 144 });
            OutPutList<Int32>(list);
            list.AddAt(3, 4);
            OutPutList<Int32>(list);

            //Int32Example();

            //DoubleExample();

            Console.ReadKey();
        }
        static void Int32Example()
        {
            var list = new CircularList<int>(new int[] { 1, 2, 3, 4, 5 });
            list.emptyListEvent += OutputMessageAddItem;
            OutPutList(list);

            //Add values to the end of CircularList
            int[] addingList = new int[] { 5, 4, 3, 2, 1, 22, 33, 44, 55, 66, 77, 88, 99, 101 };
            OutPutArrgs(addingList);
            list.AddRange(addingList);
            OutPutList(list);

            //Add-method
            /*list.Add(3);
            list.Add(21);
            list.Add(12);
            list.Add(14);
            OutPutList(list);*/

            //Add value to the list on position
            /*list.AddAt(12, 0);
            list.AddAt(21, list.Count - 1);
            list.AddAt(23, list.Count - 2);
            list.AddAt(8888, 7);
            OutPutList(list);*/

            //Remove element from position
            //list.RemoveAt(0);
            //OutPutList(list);

            //Remove element with data
            //list.Remove(5);
            //OutPutList(list);

            //Remove first element
            //list.RemoveFirst();
            //OutPutList(list);

            //Remove last element
            //list.RemoveLast();
            //OutPutList(list);

            //Clear circular linker list
            /*list.Clear();
            OutPutList(list);*/

            //Contains
            //Console.WriteLine(list.Contains(1223));
            //Console.WriteLine(list.Contains(4));

            //Get element for index
            //Console.WriteLine(list.ElementAt(0));

            //Get first element
            //Console.WriteLine(list.GetFirst());

            //Get last element
            //Console.WriteLine(list.GetLast());

            //Method Reverse()
            list.Reverse();
            OutPutList(list);

            //Copy element of the list into array starting given position
            /*Int32[] testArray = new Int32[] { 23, 23, 12, 12 };
            OutPutArrgs(testArray);

            CircularList<Int32> listForCopy = new CircularList<Int32>(new int[] { 1, 2, 3 });
            OutPutList(listForCopy);

            listForCopy.CopyTo(testArray, 1);
            //listForCopy.CopyTo(testArray, 2);
            OutPutArrgs(testArray);*/
        }
        static void DoubleExample()
        {
            var listD = new CircularList<double>() { 1.12, 2.22, 3.32, 4.42, 5.52 };
            OutPutList<Double>(listD);
            listD.emptyListEvent += OutputMessageAddItem;


            //Method Add that add one item to the list
            /*listD.Add(3.23);
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
            listD.Add(13.1212545);*/

            //Method AddAt that add one item to position to the list
            //listD.AddAt(21.2121);

            //Method AddRange that add range of items to the list
            /*double[] addingListDouble = { 2.323221, 2.1212, 312.23, 4.42, 5.121 };
            listD.AddRangeLast(addingListDouble);
            OutPutList(listD);*/

            //Remove element on position from the list
            /*listD.RemoveAt(0);
            OutPutList(listD);*/

            //Remove first occurrence of element from the list
            /*listD.Remove(17.424);
            OutPutList(listD);*/

            //Remove last element from the list
            /*listD.RemoveFirst();
            OutPutList(listD);*/

            //Remove first element from the list
            /*listD.RemoveLast();
            OutPutList(listD);*/

            //Clear list
            /*listD.Clear();
            OutPutList(listD);*/

            //Methods contains. Return true if list has an item and returns false if list hasn`t item
            /*Console.WriteLine(listD.Contains(4.42));
            Console.WriteLine(listD.Contains(1223.21));*/

            //Output element on given position from the list
            //Console.WriteLine(listD.ElementAt(0));

            //Output first element from the list
            //Console.WriteLine(listD.GetFirst());

            //Output last element from the list
            //Console.WriteLine(listD.GetLast());

            //Reverse list
            /*listD.Reverse();
            OutPutList(listD);*/

            //Copy element of the list into array starting given position
            /*Double[] testArrayD = new Double[] { 23.32, 23.23, 12.21, 12.33 };
            OutPutArrgs(testArrayD);

            CircularList<Double> listForCopy = new CircularList<Double>(new Double[] { 1.1, 2.22, 3.333 });
            OutPutList(listForCopy);

            listForCopy.CopyTo(testArrayD, 1);
            //listForCopy.CopyTo(testArrayD, 2);
            OutPutArrgs(testArrayD);*/
        }
        static void OutPutArrgs<T>(T[] arrgs)
        {
            foreach (var elem in arrgs)
            {
                Console.Write(elem.ToString() + " ");
            }
            Console.WriteLine();
        }
        static void OutPutList<T>(CircularList<T> list)
        {
            foreach (var x in list)
            {
                Console.Write(x.ToString() + " ");
            }
            Console.WriteLine();
        }
        static void OutputMessageAddItem(object e, EventArgs args)
        {
            Console.WriteLine($"List with type of {e.GetType()} is empty. Length of list: {((CircleEventArgs)args).Count}");
        }
    }
}
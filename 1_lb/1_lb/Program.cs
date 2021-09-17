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

            int noun = 0;
            foreach(var x in list)
            {
                if (noun < list.Count)
                {
                    Console.Write(x.ToString() + " ");
                    noun++;
                }
                else 
                {
                    break;
                }
            }
            Console.WriteLine();
            list.RemoveAt(3);
            list.Remove(3);
            noun = 0;
            foreach (var x in list)
            {
                if (noun < list.Count)
                {
                    Console.Write(x.ToString() + " ");
                    noun++;
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine();
            Console.WriteLine(list[0].ToString());
            Console.ReadKey();
        }
    }
}

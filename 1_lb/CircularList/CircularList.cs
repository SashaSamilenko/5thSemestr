using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    public class CircularList<T> : System.Collections.Generic.IEnumerable<T>
    {
        private Item<T> first = null;
        private Item<T> tail = null;
        private int countOfItems = 0;
        public int Count
        { 
            get => countOfItems;
        }
        public void Add(T data)
        {
            if(data==null)
            {
                throw new ArgumentNullException(nameof(data));//nameof return name of varible, method or class
            }
            var Item = new Item<T>(data);
            if(first==null)
            {
                first = Item;
            }
            else
            {
                tail.Next = Item;
            }
            tail = Item;
            tail.Next = first;
            countOfItems += 1;
        }
        public void RemoveAt(int index)
        {
            var current = first;
            Item<T> previous = null;
            for(int i=0; i<countOfItems; i++)
            {
                if(i==index)
                {
                    if (previous != null)
                    {
                        if (current.Next == first)
                        {
                            tail = previous;
                            tail.Next = first;
                        }
                        else
                        {
                            previous.Next = current.Next;
                        }
                    }
                    else
                    {
                        first = current.Next;
                        if(first==null)
                        {
                            tail = null;
                        }
                        else 
                        {
                            tail.Next = first;
                        }
                    }
                    countOfItems -= 1;
                    break;
                }
                previous = current;
                current = current.Next;
            }
        }
        public void Remove(T data)
        {
            if(data==null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            var current = first;
            Item<T> previous = null;
            while(current!=null)
            {
                if (current.CurrentData.Equals(data))
                {
                    if (previous != null)
                    {
                        if (current.Next == first)
                        {
                            tail = previous;
                            tail.Next = first;
                        }
                        else
                        {
                            previous.Next = current.Next;
                        }
                    }
                    else
                    {
                        first = current.Next;
                        if (first == null)
                        {
                            tail = null;
                        }
                        else
                        {
                            tail.Next = first;
                        }
                    }
                    countOfItems -= 1;
                    break;
                }
                previous = current;
                current = current.Next;
            }
        }
        public void Clear()
        {
            first = null;
            tail = null;
            countOfItems = 0;
        }
        public IEnumerator<T> GetEnumerator()
        {
            var current = first;
            while (current != null)
            {
                yield return current.CurrentData;
                current = current.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            // Просто возвращаем перечислитель, определенный выше.
            // Это необходимо для реализации интерфейса IEnumerable
            // чтобы была возможность перебирать элементы связного списка операцией foreach.
            return ((IEnumerable)this).GetEnumerator();
        }
        /*public void Delete(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            var current = _head;
            Item<T> previous = null;
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        if (current.Next == null)
                        {
                            _tail = previous;
                        }
                    }
                    else
                    {
                        _head = _head.Next;
                        if (_head == null)
                        {
                            _tail = null;
                        }
                    }
                    _count--;
                    break;
                }
                previous = current;
                current = current.Next;
            }
        }*/
    }
}

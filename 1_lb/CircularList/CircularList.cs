using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    /// <summary>
    ///  Class CircularList
    ///  This class is CircularLinkedList!
    ///  Present CircularLinkedList!
    /// </summary>
    public class CircularList<T> : System.Collections.Generic.IEnumerable<T>
    {
        /// <summary>
        /// This is empty list delegate
        /// </summary>
        public delegate void EmptyListHandler(object sender, CircleEventArgs args);

        /// <summary>
        /// This is add new element event
        /// </summary>
        public event EmptyListHandler emptyListEvent;

        /// <summary>
        /// This is reference to the first element of the list
        /// </summary>
        private Item<T> first = null;

        /// <summary>
        /// This is reference to the last element of the list
        /// </summary>
        private Item<T> tail = null;

        /// <summary>
        /// This is count of elements of the list
        /// </summary>
        private int countOfItems = 0;

        /// <summary>
        /// This is property of count
        /// </summary>
        public int Count
        {
            get => countOfItems;
        }

        /// <summary>
        /// This is default costructure of class "CircularList"
        /// </summary>
        public CircularList() { }

        /// <summary>
        /// This is costructure of class "CircularList" with one parameter
        /// </summary>
        public CircularList(T data) 
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            var Item = new Item<T>(data);
            if (first == null)
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

        /// <summary>
        /// This is constucture of class "CircularList"
        /// </summary>
        /// <param name="elements"></param>
        public CircularList(IEnumerable<T> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements));//nameof return name of varible, method or class
            }
            for (int i = 0; i < elements.Count(); i++)
            {
                var Item = new Item<T>(elements.ElementAt(i));
                if (first == null)
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
        }

        /// <summary>
        /// This method returns element of the list in position of index
        /// </summary>
        /// <param name="index">Index of element</param>
        /// <returns>Element on the index posotion</returns>
        public T ElementAt(int index)
        {
            if (index >= countOfItems)
            {
                throw new IndexOutOfRangeException("Index out of range!");
            }
            var current = first;
            Item<T> previous = null;
            for (int i = 0; i < countOfItems; i++)
            {
                if (i == index)
                {
                    return current.CurrentData;
                }
                previous = current;
                current = current.Next;
            }
            throw new IndexOutOfRangeException("Index out of range!");
        }

        /// <summary>
        /// This method returns first element of the list
        /// </summary>
        /// <returns>First element</returns>
        public T GetFirst()
        {
            return first.CurrentData;
        }

        /// <summary>
        /// This method returns last element of the list
        /// </summary>
        /// <returns>Last element</returns>
        public T GetLast()
        {
            return tail.CurrentData;
        }

        /// <summary>
        /// This method add element to the end
        /// </summary>
        /// <param name="data">This is data, which will be adding to the list</param>
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

        /// <summary>
        /// This method add elements to the end
        /// </summary>
        /// <param name="collections">This is collections of data, which will be adding to the list</param>
        public void AddRange(IEnumerable<T> collections)
        {
            if (collections == null)
            {
                throw new ArgumentNullException(nameof(collections));//nameof return name of varible, method or class
            }
            for (int i = 0; i < collections.Count(); i++)
            {
                var Item = new Item<T>(collections.ElementAt(i));
                if (first == null)
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
        }

        /// <summary>
        /// This method remove element on index position
        /// </summary>
        /// <param name="index">This is position of deleted element</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= countOfItems)
            {
                throw new IndexOutOfRangeException("Index out of range!");
            }
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
            emptyListEventMethod();
        }

        /// <summary>
        /// This method remove element with certain data
        /// </summary>
        /// <param name="data">This is data of deleted element</param>
        public void Remove(T data)
        {
            if(data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            var current = first;
            Item<T> previous = null;
            int counter = 0;
            while(counter<countOfItems)
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
                counter++;
                previous = current;
                current = current.Next;
            }
            emptyListEventMethod();
        }

        /// <summary>
        /// This method remove first element of the list
        /// </summary>
        public void RemoveFirst()
        {
            first = first.Next;
            countOfItems -= 1;
            emptyListEventMethod();
        }

        /// <summary>
        /// This method remove last element of the list
        /// </summary>
        public void RemoveLast()
        {
            var current = first;
            Item<T> previous = null;
            for (int i = 0; i < countOfItems; i++)
            {
                if (i == countOfItems-1)
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
            emptyListEventMethod();
        }

        /// <summary>
        /// This method delete all the elements from the list
        /// </summary>
        public void Clear()
        {
            first = null;
            tail = null;
            countOfItems = 0;
            emptyListEventMethod();
        }

        /// <summary>
        /// This method check length of list and throw event if length equals zero
        /// </summary>
        private void emptyListEventMethod()
        {
            if (countOfItems == 0)
            {
                CircleEventArgs args = new CircleEventArgs(countOfItems);
                emptyListEvent?.Invoke(this, args);
            }
        }

        /// <summary>
        /// This method return elements one by one
        /// </summary>
        /// /// <returns>Elements one by one!</returns>
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
            return ((IEnumerable)this).GetEnumerator();
        }
    }
}

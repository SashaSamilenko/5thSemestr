using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularList
{
    /// <summary>
    ///  Class CircularList
    ///  This class is CircularLinkedList!
    ///  Present CircularLinkedList!
    /// </summary>
    public class CircularList<T> : IEnumerable, ICollection, IEnumerable<T>, ICollection<T>
    {
        /// <summary>
        /// EventHandler - generation delegate.
        /// It is mean that receivers of information have to give a call back method with prototype
        /// that matches type-delegate EventHandler CircleEventArgs
        /// </summary>
        public event EventHandler<CircleEventArgs> emptyListEvent;

        /// <summary>
        /// This is reference to the first element of the list
        /// </summary>
        private Item<T> head = null;

        /// <summary>
        /// This is reference to the previous element of the list
        /// </summary>
        private Item<T> tail = null;

        /// <summary>
        /// This is count of elements of the list
        /// </summary>
        private int count = 0;

        /// <summary>
        /// This is property of count
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index >= count || index < 0)
                {
                    throw new IndexOutOfRangeException("Index out of range.");
                }
                var current = head;
                Item<T> previous = null;
                for (int i = 0; i < count; i++)
                {
                    if (i == index)
                    {
                        break;
                    }
                    previous = current;
                    current = current.Next;
                }
                return current.CurrentData;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the collection is read-only
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the ICollection.
        /// </summary>
        public object SyncRoot => throw new NotImplementedException();

        /// <summary>
        /// Gets a value indicating whether access to the Array is synchronized (thread safe).
        /// </summary>
        public bool IsSynchronized => throw new NotImplementedException();

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
            var item = new Item<T>(data);
            head = item;
            head.Next = head;
            head.Previous = head;
            tail = head;
            tail.Next = head;
            tail.Previous = head;
            count += 1;
        }

        /// <summary>
        /// This is constucture of class "CircularList"
        /// </summary>
        /// <param name="elements"></param>
        public CircularList(IEnumerable<T> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements));
            }
            Item<T> tempItem = null;
            for (int i = 0; i < elements.Count(); i++)
            {
                var item = new Item<T>(elements.ElementAt(i));
                if (head == null)
                {
                    head = item;
                    head.Next = head;
                    head.Previous = head;
                    tail = head;
                    tail.Next = head;
                    tail.Previous = head;
                }
                else
                {
                    if (count == 1)
                    {
                        tail = item;
                        tail.Previous = head;
                        tail.Next = head;
                        head.Next = tail;
                        head.Previous = tail; 
                    }
                    else
                    {
                        tail.Next = item;
                        tempItem = tail;
                        tail = tail.Next;
                        tail.Previous = tempItem;
                        tail.Next = head;
                        head.Previous = tail;
                    }
                }
                count += 1;
            }
        }

        /// <summary>
        /// This method returns element of the list in position of index
        /// </summary>
        /// <param name="index">Index of element</param>
        /// <returns>Element on the index posotion</returns>
        public T ElementAt(int index)
        {
            if (index >= count || index < 0)
            {
                throw new IndexOutOfRangeException("Index out of range!");
            }
            var current = head;
            Item<T> previous = null;
            for (int i = 0; i < count; i++)
            {
                if (i == index)
                {
                    return current.CurrentData;
                }
                previous = current;
                current = current.Next;
            }
            return default(T);
        }

        /// <summary>
        /// This method returns first element of the list
        /// </summary>
        /// <returns>First element</returns>
        public T GetFirst()
        {
            return head.CurrentData;
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
        /// Add new item to the end of list.
        /// </summary>
        /// <param name="data">This is data, which will be adding to the list</param>
        public void Add(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            var item = new Item<T>(data);
            if (head == null)
            {
                head = item;
                head.Next = head;
                head.Previous = head;
                tail = head;
                tail.Next = head;
                tail.Previous = head;
            }
            else
            {
                if (tail == head)
                {
                    tail = item;
                    head.Next = tail;
                    head.Previous = tail;
                    tail.Next = head;
                    tail.Previous = head;
                }
                else
                {
                    Item<T> tItem = null;
                    tail.Next = item;
                    tItem = tail;
                    tail = tail.Next;
                    tail.Next = head;
                    tail.Previous = tItem;
                    head.Previous = tail;
                }
                //tail.Next = item;
            }
            count += 1;
        }

        /// <summary>
        /// Add new items to the end of list.
        /// </summary>
        /// <param name="elements">This is collections of data, which will be adding to the list</param>
        public void AddRange(IEnumerable<T> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements));
            }
            Item<T> tempItem = null;
            for (int i = 0; i < elements.Count(); i++)
            {
                Item<T> item = new Item<T>(elements.ElementAt(i));
                if (head == null)
                {
                    head = item;
                    head.Next = head;
                    head.Previous = head;
                    tail = head;
                    tail.Next = head;
                    tail.Previous = head;
                }
                else
                {
                    if (count == 1)
                    {
                        tail = item;
                        tail.Previous = head;
                        tail.Next = head;
                        head.Next = tail;
                        head.Previous = tail;
                    }
                    else
                    {
                        tail.Next = item;
                        tempItem = tail;
                        tail = tail.Next;
                        tail.Previous = tempItem;
                        tail.Next = head;
                        head.Previous = tail;
                    }
                }
                count += 1;
            }
        }

        /// <summary>
        /// Adds a new node or value at position as index
        /// </summary>
        /// <param name="data"></param>
        /// <param name="position"></param>
        public void AddAt(T data, int position)
        {
            if (position >= count || position < 0)
            {
                throw new IndexOutOfRangeException("Position out of range!");
            }
            if(data == null)
            {
                throw new NullReferenceException("First parametr cannot be null reference.");
            }
            Item<T> item = new Item<T>(data);
            Item<T> current = head;
            Item<T> previous = null;
            Int32 i = 0;
            while (i != position)
            {
                previous = current;
                current = current.Next;
                i++;
            }
            if(i == 0)
            {
                if (head == null)
                {
                    head = item;
                    tail = head;
                }
                else 
                {
                    
                    tail.Next = item;
                    head = item;
                    head.Previous = tail;
                    head.Next = current;
                    current.Previous = head;
                }
            }
            else
            {
                if(i==count-1)
                {
                    current.Next = item;
                    tail = item;
                    tail.Previous = current;
                    tail.Next = head;
                }
                else
                {
                    previous.Next = item;
                    item.Previous = previous;
                    item.Next = current;
                    current.Previous = item;
                }
            }
            count++;
        }

        /// <summary>
        /// This method remove element with certain data
        /// </summary>
        /// <param name="data">This is data of deleted element</param>
        public bool Remove(T data)
        {
            bool removing = false;
            if(data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            Item<T> current = head;
            Item<T> previous = null;
            int counter = 0;
            while(counter < count && !current.CurrentData.Equals(data))
            {
                previous = current;
                current = current.Next;
                counter++;
            }
            if(counter == count)
            {
                return false;
            }
            if (counter == 0)
            {
                if (count == 1)
                {
                    head = null;
                    tail = null;
                }
                else
                {
                    head = head.Next;
                    tail.Next = head;
                    head.Previous = tail;
                }
            }
            else
            {
                if (counter == count - 1)
                {
                    tail = previous;
                    tail.Next = head;
                    head.Previous = tail;
                }
                else
                {
                    current = current.Next;
                    previous.Next = current;
                    current.Previous = previous;
                }
            }
            removing = true;
            count -= 1;
            CircleEventArgs args = new CircleEventArgs(count);
            EmptyListEventMethod(args);
            return removing;
        }

        /// <summary>
        /// This method remove first element of the list
        /// </summary>
        public void RemoveFirst()
        {
            if(count == 1)
            {
                head = null;
                tail = null;
            }
            else
            {
                head = head.Next;
                tail.Next = head;
                head.Previous = tail;
            }
            count -= 1;
            CircleEventArgs args = new CircleEventArgs(count);
            EmptyListEventMethod(args);
        }

        /// <summary>
        /// This method remove last element of the list
        /// </summary>
        public void RemoveLast()
        {
            if(count == 0)
            {
                CircleEventArgs arr = new CircleEventArgs(count);
                EmptyListEventMethod(arr);
            }
            else
            {
                if (count == 1)
                {
                    head = null;
                    tail = null;
                }
                else
                {
                    tail = tail.Previous;
                    tail.Next = head;
                    head.Previous = tail;
                }
            }
            count -= 1;
            CircleEventArgs args = new CircleEventArgs(count);
            EmptyListEventMethod(args);
        }

        /// <summary>
        /// This method remove element on index position
        /// </summary>
        /// <param name="index">This is position of deleted element</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Index out of range!");
            }
            Item<T> previous = null;
            Item<T> current = head;
            Int32 i = 0;
            while (i != index)
            {
                previous = current;
                current = current.Next;
                i++;
            }
            if (i == 0)
            {
                head = head.Next;
                tail.Next = head;
                head.Previous = tail;
            }
            else
            {
                if (i == count - 1)
                {
                    tail = previous;
                    tail.Next = head;
                    head.Previous = tail;
                }
                else
                {
                    current = current.Next;
                    previous.Next = current;
                    current.Previous = previous;
                }
            }
            count -= 1;
            CircleEventArgs args = new CircleEventArgs(count);
            EmptyListEventMethod(args);
        }

        /// <summary>
        /// This method delete all the elements from the list
        /// </summary>
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
            CircleEventArgs args = new CircleEventArgs(count);
            EmptyListEventMethod(args);
        }

        /// <summary>
        /// Determines if an item is in the collection
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            if(count == 0)
            {
                return false;
            }
            bool found = false;
            var jumper = head;
            do
            {
                if (Equals(jumper.CurrentData, item))
                {
                    found = true;
                    break;
                }
                else
                {
                    jumper = jumper.Next;
                }
            }
            while (!Equals(jumper, head));
            return found;
        }

        /// <summary>
        /// Copies the elements of the ICollection to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            this.CopyTo((T[])array, index);
        }

        /// <summary>
        /// Copies the elements of the ICollection to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new NullReferenceException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("The starting array index cannot be less then zero.");
            if (Count >= array.Length - arrayIndex + 1)
                throw new ArgumentException("The destination array has fewer elements than the collection or index more then allowable value.");
            var jumper = head;
            for (int i = 0; i < count; i++)
            {
                array[i + arrayIndex] = jumper.CurrentData;
                jumper = jumper.Next;
            }
        }

        /// <summary>
        /// Reverses the order of the elements in the entire System.Collections.Generic.List`1.
        /// </summary>
        public void Reverse()
        {
            if(count == 0)
            {
                return;
            }
            Item<T> current = head;
            Item<T> previous = null;
            Item<T> tempo = null;
            for (Int32 i = 0; i < count; i++)
            {
                previous = current;

                tempo = current.Next;
                current.Next = current.Previous;
                current.Previous = tempo;

                current = previous.Next;
            }
            head = head.Next;
            tail = tail.Next;

        }

        /// <summary>
        /// This method check length of list and throw event if length equals zero
        /// </summary>
        private void EmptyListEventMethod(CircleEventArgs args)
        {
            if (args.Count == 0)
            {
                emptyListEvent?.Invoke(this, args);
            }
        }

        /// <summary>
        /// This method return elements one by one
        /// </summary>
        /// /// <returns>Elements one by one!</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var current = head;
            do
            {
                if(current == null)
                {
                    yield break;
                }
                yield return current.CurrentData;
                current = current.Next;
            }
            while (current != head);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }
}
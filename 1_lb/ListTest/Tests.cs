using NUnit.Framework;
using List;
using System;

namespace ListTest
{
    public class Tests
    {
        [Test]
        public void CircularList_ConstructorWithoutParametrs()
        {
            //average
            CircularList<int> list;
            //act
            list = new CircularList<int>();
            //assert
            Assert.AreEqual(0, list.Count, message: "Constructor for class 'CircularList' without parameters works incorrectly");
        }

        [Test]
        [TestCase(-4)]
        [TestCase(0)]
        [TestCase(231)]
        [TestCase(-32000)]
        [TestCase(32000)]
        public void CircularList_SetNormalItem(int element)
        {
            //arange
            CircularList<int> list;
            //act
            list = new CircularList<int>(element);
            //assert
            Assert.AreEqual(1, list.Count, message: "Constructor of CircularList with one parametr works incorrectly");
        }

        [Test]
        [TestCase(null)]
        public void CircularList_SetNullItem_GetArgumentNullException(int element)
        {
            //arange
            CircularList<int> list;
            //act
            try
            {
                list = new CircularList<int>(element);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.Throws<ArgumentNullException>(
                delegate
                {
                    throw new ArgumentNullException(e.Message);
                });
            }
        }

        [Test]
        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 6, 754, 345, 3, 34, 12 })]
        [TestCase(new int[] { -12, 12, -24, -23, -12 })]
        public void CircularList_SetALotOfItems(int[] elements)
        {
            //arange
            CircularList<int> list;
            //act
            list = new CircularList<int>(elements);
            int expectedCount = elements.Length;
            //assert
            Assert.AreEqual(expectedCount, list.Count, message: "Constructor of CircularList with one parametr works incorrectly");
        }

        [Test]
        [TestCase(null)]
        public void CircularList_SetALotOfItems_GetArgumentNullException(int[] elements)
        {
            //arange
            CircularList<int> list;
            try
            {
                //act
                list = new CircularList<int>(elements);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.Throws<ArgumentNullException>(
                delegate
                {
                    throw new ArgumentNullException(e.Message);
                });
            }
        }

        [Test]
        [TestCase(0, new int[] { 1 }, 1)]
        [TestCase(8, new int[] { 0, 1, 1, 22, 434, 1, 23, 4, 12, 13 }, 12)]
        public void ElementAt_GetElementFromListForIndex_DataElementReturns(int position, int[] elements, int expectedResult)
        {
            //arange
            var list = new CircularList<int>(elements);
            //act
            int actResult = list.ElementAt(position);
            //assert
            Assert.AreEqual(expectedResult, actResult, message: "ElemetAt works incorrectly");
        }

        [Test]
        [TestCase(3, new int[] { 1 }, -5)]
        [TestCase(-1, new int[] { 0, 1, 1, 22, 434, 1, 23, 4, 12, 13 }, -5)]
        [TestCase(31, new int[] { 0, 1, 1, 22, 434, 1, 23, 4, 12, 13 }, -5)]
        public void ElementAt_GetElementFromListForIndex_GetIndexOutOfRangeException(int position, int[] elements, int expectedResult)
        {
            //arange
            var list = new CircularList<int>(elements);
            //act
            try
            {
                int actResult = list.ElementAt(position);
            }
            catch (IndexOutOfRangeException e)
            {
                //assert
                Assert.Throws<IndexOutOfRangeException>(
                delegate
                {
                    throw new IndexOutOfRangeException(e.Message);
                });
            }
        }

        [Test]
        [TestCase(new int[] { 1 }, 1)]
        [TestCase(new int[] { -2, 3, 12, 424, 1 }, -2)]
        public void GetFirst_GetFirstElementOfTheList_FirstDataElementOfListReturns(int[] elements, int expectedValue)
        {

            //arange
            var list = new CircularList<int>(elements);
            //act
            var actValue = list.GetFirst();
            //assert
            Assert.AreEqual(expectedValue, actValue, 0.001, message: "GetFirst works incorrectly");
        }

        [Test]
        [TestCase(new int[] { 3 }, 3)]
        [TestCase(new int[] { -2, 3, 12, 424, 1 }, 1)]
        public void GetLast_GetLastElementOfTheList_LastDataElementOfListReturns(int[] elements, int expectedValue)
        {
            //arange
            var list = new CircularList<int>(elements);
            //act
            var actValue = list.GetLast();
            //assert
            Assert.AreEqual(expectedValue, actValue, 0.001, message: "GetFirst works incorrectly");
        }

        [Test]
        [TestCase(new int[] {}, 3)]
        [TestCase(new int[] { 11, 22, 33, 44,144 }, 3)]
        [TestCase(new int[] { 11, 22, 33, 44,132 }, -23)]
        public void Add_AddElementToTheEndOfList_ListWithNewItemReturn(int[] elements, int addItem)
        {
            //arange
            var list = new CircularList<int>(elements);
            int expectedCount = list.Count + 1;
            //act
            list.Add(addItem);
            int actCount = list.Count;
            //assets
            Assert.AreEqual(expectedCount, actCount, message: "Add works incorrectly");
        }

        [Test]
        [TestCase(new int[] { 1, 2, 3, 4, 121 }, null)]
        [TestCase(new int[] { }, null)]
        public void Add_AddNullElementToTheEndOfList_GetArgumentNullException(int[] elements, int addItem)
        {
            //arange
            var list = new CircularList<int>(elements);
            //act
            try
            {
                list.Add(addItem);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.Throws<ArgumentNullException>(
                delegate
                {
                    throw new ArgumentNullException(e.Message);
                });
            }
        }

        [Test]
        [TestCase(new int[] { 11, 22, 33, 44 }, new int[] { 88 })]
        [TestCase(new int[] { 11, 22, 33, 44 }, new int[] { 23, 223, 4211, 45, 34 })]
        [TestCase(new int[] { }, new int[] { 23, 223, 4211, 45, 34 })]
        public void AddRange_AddElementsToTheEndOfList_ListWithNewItemsReturn(int[] stateElements, int[] addElements)
        {
            //arange
            var list = new CircularList<int>(stateElements);
            //act
            int expectedCount = list.Count + addElements.Length;
            list.AddRange(addElements);
            int actCount = list.Count;
            //assert
            Assert.AreEqual(expectedCount, actCount, message: "AddRange works incorrectly");
        }

        [Test]
        [TestCase(new int[] { 1, 2, 3, 4 }, null)]
        public void AddRange_AddElementsToTheEndOfList_GetArgumentNullException(int[] stateElements, int[] addElements)
        {
            //arange
            var list = new CircularList<int>(stateElements);
            //act
            try
            {
                list.AddRange(addElements);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.Throws<ArgumentNullException>(
                delegate
                {
                    throw new ArgumentNullException(e.Message);
                });
            }
        }

        [Test]
        [TestCase(5, new int[] { 31, 12, 32, 41, 23, 1232 })]
        [TestCase(2, new int[] { 11, 22, 33, 44, 111 })]
        public void RemoveAt_RemoveElementOfTheListAtIndex_ListWithoutElementReturn(int index, int[] elements)
        {
            //averrage
            var list = new CircularList<int>(elements);
            int expectedLength = elements.Length - 1;
            //act
            list.RemoveAt(index);
            int actLength = list.Count;
            //assert
            Assert.AreEqual(expectedLength, actLength, message: "RemoveAt works incorrectly");
        }

        [Test]
        [TestCase(null, new int[] { 31, 12, 32, 41, 23, 1232 })]
        [TestCase(33, new int[] { 1, 2, 3, 4, 51 })]
        [TestCase(-5, new int[] { 11, 22, 33, 44, 232 })]
        public void RemoveAt_RemoveElementOfTheListAtIndex_GetIndexOutOfRangeException(int index, int[] elements)
        {
            //averrage
            var list = new CircularList<int>(elements);
            try
            {
                //act
                list.RemoveAt(index);
            }
            catch (IndexOutOfRangeException e)
            {
                //assert
                Assert.Throws<IndexOutOfRangeException>(
                delegate
                {
                    throw new IndexOutOfRangeException(e.Message);
                });
            }
        }

        [Test]
        [TestCase(11, new int[] { 11, 22, 33, 44,231,332 })]
        [TestCase(44, new int[] { 11, 22, 33, 44, 231, 332 })]
        [TestCase(-144, new int[] { -144, -13, -1, 0, 11, 22, 33, 44, 231, 332 })]
        public void Remove_RemoveElementWithData_ListWithoutFirstApperanceOfElementReturn(int data, int[] elements)
        {
            //averrage
            var list = new CircularList<int>(elements);
            int expectedLength = elements.Length >0?elements.Length - 1:0;
            //act
            list.Remove(data);
            int actLength = list.Count;
            //assert
            Assert.AreEqual(expectedLength, actLength, message: "Remove works incorrectly");
        }

        [Test]
        [TestCase(null, new int[] { 0, 11, 23, 37, 44, 242, 333 })]
        [TestCase(null, new int[] { })]
        public void Remove_RemoveElementWithData_GetArgumentNullException(int data, int[] elements)
        {
            //averrage
            var list = new CircularList<int>(elements);
            try
            {
                //act
                list.Remove(data);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.Throws<ArgumentNullException>(
                delegate
                {
                    throw new ArgumentNullException(e.Message);
                });
            }
        }

        [Test]
        [TestCase(5, new int[] { 1, 2, 3, 4 })]
        [TestCase(5, new int[] { -22 })]
        [TestCase(5, new int[] { })]
        public void Remove_RemoveFirstElementOfTheListWithUnrealData_PreviouslyListReturn(int data, int[] elements)
        {
            //averrage
            var list = new CircularList<int>(elements);
            int expectedLength = elements.Length;
            //act
            list.Remove(data);
            int actLength = list.Count;
            //assert
            Assert.AreEqual(expectedLength, actLength, message: "Remove works incorrectly");
        }

        [Test]
        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { -2, 3, 12, 424, 1 })]
        public void RemoveFirst_RemoveFirstElementOfTheList_ListWithoutFirstElementReturns(int[] elements)
        {
            //arange
            var list = new CircularList<int>(elements);
            int expectedLength = list.Count - 1;
            //act
            list.RemoveFirst();
            int actLength = list.Count;
            //assert
            Assert.AreEqual(expectedLength, actLength, message: "RemoveFirst works incorrectly");
        }

        [Test]
        [TestCase(new int[] { 3 })]
        [TestCase(new int[] { -2, 3, 12, 424, 1 })]
        public void RemoveLast_RemoveLastElementOfTheList_ListWithoutLastElementReturns(int[] elements)
        {
            //arange
            var list = new CircularList<int>(elements);
            int expectedLength = list.Count - 1;
            //act
            list.RemoveLast();
            int actLength = list.Count;
            //assert
            Assert.AreEqual(expectedLength, actLength, message: "RemoveLast works incorrectly");
        }

        [Test]
        [TestCase(new int[] {1})]
        [TestCase(new int[] {1,2,3,4,5,6,7,8,89,0,0,1,2,21,1,1})]
        public void Clear_ClearTheList_EmptyListReturn(int[] elements)
        {
            //arange
            var list = new CircularList<int>(elements);
            int expectedCount = 0;
            //act
            list.Clear();
            int actCount = list.Count;
            //assert
            Assert.AreEqual(expectedCount, actCount, message: "Clear works not correctly");
        }
    }
}
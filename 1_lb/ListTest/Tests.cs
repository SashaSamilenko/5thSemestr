using NUnit.Framework;
using List;
using System;

namespace ListTest
{
    public class Tests
    {
        [Test]
        [TestCase(null)]
        [TestCase(-4)]
        [TestCase(0)]
        [TestCase(231)]
        public void CircularList_ConstructorWithOneParametr(int element)
        {
            //arange
            CircularList<int> list;
            //act
            try
            {
                list = new CircularList<int>(element);
                //assert
                Assert.AreEqual(1, list.Count, message: "Constructor of CircularList with one parametr works incorrectly");
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
        [TestCase(null)]
        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 6, 754, 345, 3, 34, 12 })]
        [TestCase(new int[] { -12, 12, -24, -23, -12 })]
        public void CircularList_ConstructorWithALotOfParametrs(int[] elements)
        {
            //arange
            CircularList<int> list;
            int expectedCount = elements.Length;
            //act
            try
            {
                list = new CircularList<int>(elements);
                //assert
                Assert.AreEqual(expectedCount, list.Count, message: "Constructor of CircularList with one parametr works incorrectly");
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
        [TestCase(3, new int[] { 1 }, -5)]
        [TestCase(8, new int[] { 0, 1, 1, 22, 434, 1, 23, 4, 12, 13 }, 12)]
        [TestCase(-1, new int[] { 0, 1, 1, 22, 434, 1, 23, 4, 12, 13 }, -5)]
        [TestCase(31, new int[] { 0, 1, 1, 22, 434, 1, 23, 4, 12, 13 }, -5)]
        public void ElementAt_GetElementFromListForIndex_DataElementReturns(int position, int[] elements, int expectedResult)
        {
            //arange
            var list = new CircularList<int>(elements);
            //act
            try
            {
                int actResult = list.ElementAt(position);
                //assert
                Assert.AreEqual(expectedResult, actResult, message: "ElemetAt works incorrectly");
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

        /*[Test]
        [TestCase(2, null, 0)]
        public void ElementAt_GetElementFromEmptyListForIndex_DataElementReturns(int position, int[] elements, int expectedResult)
        {
            //arange
            try
            {
                var list = new CircularList<int>(elements);
            }
            catch (ArgumentNullException y)
            {
                //assert
                Assert.Throws<ArgumentNullException>(
                delegate
                {
                    throw new ArgumentNullException(y.Message);
                });
            }
        }*/

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
        [TestCase(new int[] { 1, 2, 3, 4, }, null)]
        [TestCase(new int[] { 11, 22, 33, 44, }, 3)]
        public void Add_AddElementToTheEndOfList(int[] elements, int addItem)
        {
            //arange
            var list = new CircularList<int>(elements);
            int expectedCount = list.Count + 1;
            //act
            try
            {
                list.Add(addItem);
                int actCount = list.Count;
                //assets
                Assert.AreEqual(expectedCount, actCount, message: "Add works incorrectly");
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
        [TestCase(new int[] { 1, 2, 3, 4, }, null)]
        [TestCase(new int[] { 11, 22, 33, 44, }, new int[] { 88 })]
        [TestCase(new int[] { 11, 22, 33, 44, }, new int[] { 23, 223, 4211, 45, 34 })]
        public void AddRange_AddElementsToTheEndOfList(int[] stateElements, int[] addElements)
        {
            //arange
            var list = new CircularList<int>(stateElements);
            //act
            try
            {
                list.AddRange(addElements);
                int expectedCount = list.Count + addElements.Length;
                int actCount = list.Count;
                //assert
                Assert.AreEqual(expectedCount, actCount, message: "AddRange works incorrectly");
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
            catch (NullReferenceException f)
            {
                //assert
                Assert.Throws<NullReferenceException>(
                delegate
                {
                    throw new NullReferenceException(f.Message);
                });
            }
        }

        [Test]
        [TestCase(null, new int[] { 31, 12, 32, 41, })]
        [TestCase(5, new int[] { 1, 2, 3, 4, })]
        [TestCase(-1, new int[] { 11, 22, 33, 44, })]
        [TestCase(0, new int[] { 11, 22, 33, 44, })]
        public void RemoveAt_RemoveElementOfTheListAtIndex_ListWithoutElementReturn(int index, int[] elements)
        {
            //averrage
            var list = new CircularList<int>(elements);
            int expectedLength = elements.Length - 1;
            try
            {
                //act
                list.RemoveAt(index);
                int actLength = list.Count;
                //assert
                Assert.AreEqual(expectedLength, actLength, message: "RemoveAt works incorrectly");
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
            catch (NullReferenceException e)
            {
                //assert
                Assert.Throws<NullReferenceException>(
                delegate
                {
                    throw new NullReferenceException(e.Message);
                });
            }
        }

        [Test]
        [TestCase(11, new int[] { 11, 22, 33, 44, })]
        public void Remove_RemoveFirstElementOfTheListWithData_ListWithoutElementReturn(int data, int[] elements)
        {
            //averrage
            var list = new CircularList<int>(elements);
            int expectedLength = elements.Length - 1;
            try
            {
                //act
                list.Remove(data);
                int actLength = list.Count;
                //assert
                Assert.AreEqual(expectedLength, actLength, message: "Remove works incorrectly");
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
        [TestCase(5, new int[] { 1, 2, 3, 4, })]
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
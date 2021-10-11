using NUnit.Framework;
using CircularList;
using System;
using System.Collections.Generic;
using System.Collections;

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
        public void CircularList_SetNullItem_GetArgumentNullException()
        {
            //assert
            Assert.Throws < ArgumentNullException>(()=> new CircularList<int>(null));
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
            Assert.Throws<ArgumentNullException>(() => new CircularList<int>(elements));
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
            //assert
            Assert.Throws<IndexOutOfRangeException>(()=>list.ElementAt(position));
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
        [TestCase(new int[] { }, 3)]
        [TestCase(new int[] { }, -123)]
        [TestCase(new int[] { 11, 22, 33, 44, 144 }, 3)]
        [TestCase(new int[] { 11, 22, 33, 44, 132 }, -23)]
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
        [TestCase(new int[] { 11, 22, 33, 44, 144 }, 3, 4)]
        [TestCase(new int[] { 11, 22, 33, 44, 132, 121, 1212 }, -23, 5)]
        public void AddAt_AddElementToThePosition_ListWithNewItemReturn(int[] elements, int addItem, int index)
        {
            //arange
            var list = new CircularList<int>(elements);
            int expectedCount = list.Count + 1;
            //act
            list.AddAt(addItem, index);
            int actCount = list.Count;
            //assets
            Assert.AreEqual(expectedCount, actCount, message: "Add works incorrectly");
        }

        [Test]
        [TestCase(new int[] { }, 3, 0)]
        [TestCase(new int[] { }, -123, 0)]
        [TestCase(new int[] { }, null, 0)]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, -123, -4)]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, -123, 12)]
        public void AddAt_AddElementToTheWrongPosition_GetIndexOutOfRangeException(int[] elements, int addItem, int index)
        {
            //arange
            var list = new CircularList<int>(elements);
            int expectedCount = list.Count + 1;
            //assert
            Assert.Throws<IndexOutOfRangeException>(() => list.AddAt(addItem, index)); 
        }

        [Test]
        [TestCase(new string[] { "1", "2" }, null, 1)]
        [TestCase(new string[] { "1", "2", "3", "4", "5" }, null, 4)]
        public void AddAt_AddWrongElementToThePosition_GetNullReferenceException(string[] elements, string addItem, int index)
        {
            //arange
            var list = new CircularList<string>(elements);
            int expectedCount = list.Count + 1;
            //assert
            Assert.Throws<NullReferenceException>(() => list.AddAt(addItem, index));
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
            //assert
            Assert.Throws<ArgumentNullException>(() => list.AddRange(addElements));
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
        [TestCase(33, new int[] { 1, 2, 3, 4, 51 })]
        [TestCase(-5, new int[] { 11, 22, 33, 44, 232 })]
        [TestCase(5, new int[] { })]
        [TestCase(0, new int[] { })]
        [TestCase(-3, new int[] { })]
        public void RemoveAt_RemoveElementOfTheListAtIndex_GetIndexOutOfRangeException(int index, int[] elements)
        {
            //averrage
            var list = new CircularList<int>(elements);
            //assert
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(index));
        }

        [Test]
        [TestCase(22, new int[] { 11, 22, 33, 44, 231, 332 }, true)]
        [TestCase(44, new int[] { 11, 22, 33, 44, 231, 332 }, true)]
        [TestCase(-144, new int[] { -144, -13, -1, 0, 11, 22, 33, 44, 231, 332 }, true)]
        [TestCase(5, new int[] { 1, 2, 3, 4 }, false)]
        [TestCase(5, new int[] { -22 }, false)]
        public void Remove_RemoveElementWithData_ListWithoutFirstApperanceOfElementReturn(int data, int[] elements, bool exceptedV)
        {
            //averrage
            var list = new CircularList<int>(elements);
            //act
            bool actV = list.Remove(data);
            //assert
            Assert.AreEqual(exceptedV, actV, message: "Remove works incorrectly");
        }

        [Test]
        [TestCase(5, new int[] { 1, 2, 3, 4 })]
        [TestCase(5, new int[] { -22 })]
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
        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 89, 0, 0, 1, 2, 21, 1, 1 })]
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

        [Test]
        [TestCase(new int[] {1}, 1, true)]
        [TestCase(new int[] {324,24,-123,23}, 23, true)]
        [TestCase(new int[] { 1 }, 31, false)]
        [TestCase(new int[] { 324, 24, -123, 23 }, 25, false)]
        [TestCase(new int[] { 324, 24, -123, 23 }, null, false)]
        public void Contains_CheckIfListContainsItem_TrueOrFalseReturn(int[] arrgs, int item, bool expectedValue)
        {
            //arange
            CircularList<int> list = new CircularList<int>(arrgs);
            //act
            bool checker = list.Contains(item);
            //assert
            Assert.AreEqual(expectedValue, checker, message: "Contains does not work correctly");
        }

        [Test]
        [TestCase(new int[] { 123,23,2,12,2,3,2,1,2,3},new int[] {-2,-3,-1,-4,-5},5)]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new int[] { -2, -3, -1, -4, -5 }, 3)]
        public void CopyTo_CopyListToArrgsStartingIndexPosition_ArrgsWithNewItemsFromListReturn(int[] arrgs, int[]elements, int index)
        {
            //arrange
            CircularList<int> list = new CircularList<int>(elements);
            //acts
            list.CopyTo(arrgs, index);
            //assert
            for (int i = index; i < arrgs.Length-index-1; i++)
            {
                Assert.AreEqual(arrgs[i], list.ElementAt(i - index), message: "CopyTo does not work correctly!");
            }
        }

        [Test]
        [TestCase(null, new int[] { -2, -3, -1, -4, -5 }, 5)]
        [TestCase(null, new int[] { -2, -3, -1, -4, -5 }, 3)]
        public void CopyTo_CopyListToArrgsStartingIndexPosition_GetNullReferenceException(int[] arrgs, int[] elements, int index)
        {
            //arrange
            CircularList<int> list = new CircularList<int>(elements);
            //assert
            Assert.Throws<NullReferenceException>(() => list.CopyTo(arrgs, index));
        }

        [Test]

        [TestCase(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new int[] { -2, -3, -1, -4, -5 }, -2)]
        [TestCase(new int[] { 14,2133,3233,1212 }, new int[] { 21 }, -12)]
        public void CopyTo_CopyListToArrgsStartingIndexPosition_GetArgumentOutOfRangeException(int[] arrgs, int[] elements, int index)
        {
            //arrange
            CircularList<int> list = new CircularList<int>(elements);
            //assert
            Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(arrgs, index));
        }

        [Test]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new int[] { -2, -3, -1, -4, -5 }, 7)]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3}, new int[] { 27, -3, -1, -4, -5 }, 12)]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3 }, new int[] { 11,23,13, 27 }, 3)]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3 }, new int[] { 11, 23, 13, 27 }, 4)]
        public void CopyTo_CopyListToArrgsStartingIndexPosition_GetArgumentException(int[] arrgs, int[] elements, int index)
        {
            //arrange
            CircularList<int> list = new CircularList<int>(elements);
            //assert
            Assert.Throws<ArgumentException>(() => list.CopyTo(arrgs, index));
        }

        [Test]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new int[] { -2, -3, -1, -4, -5 }, 5)]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new int[] { -2, -3, -1, -4, -5 }, 3)]
        public void CopyTo_CopyListToArrayStartingIndexPosition_ArrayWithNewItemsFromListReturn(int[] arrgs, int[] elements, int index)
        {
            //arrange
            CircularList<int> list = new CircularList<int>(elements);
            //acts
            list.CopyTo((Array)arrgs, index);
            //assert
            for (int i = index; i < arrgs.Length - index - 1; i++)
            {
                Assert.AreEqual(arrgs[i], list.ElementAt(i - index), message: "CopyTo does not work correctly!");
            }
        }

        [Test]
        [TestCase(null, new int[] { -2, -3, -1, -4, -5 }, 5)]
        [TestCase(null, new int[] { -2, -3, -1, -4, -5 }, 3)]
        public void CopyTo_CopyListToArrayStartingIndexPosition_GetNullReferenceException(int[] arrgs, int[] elements, int index)
        {
            //arrange
            CircularList<int> list = new CircularList<int>(elements);
            //assert
            Assert.Throws<NullReferenceException>(() => list.CopyTo((Array)arrgs, index));
        }

        [Test]

        [TestCase(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new int[] { -2, -3, -1, -4, -5 }, -2)]
        [TestCase(new int[] { 14, 2133, 3233, 1212 }, new int[] { 21 }, -12)]
        public void CopyTo_CopyListToArrayStartingIndexPosition_GetArgumentOutOfRangeException(int[] arrgs, int[] elements, int index)
        {
            //arrange
            CircularList<int> list = new CircularList<int>(elements);
            //assert
            Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo((Array)arrgs, index));
        }

        [Test]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new int[] { -2, -3, -1, -4, -5 }, 7)]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3 }, new int[] { 27, -3, -1, -4, -5 }, 12)]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3 }, new int[] { 11, 23, 13, 27 }, 3)]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3 }, new int[] { 11, 23, 13, 27 }, 4)]
        public void CopyTo_CopyListToArrayStartingIndexPosition_GetArgumentException(int[] arrgs, int[] elements, int index)
        {
            //arrange
            CircularList<int> list = new CircularList<int>(elements);
            //assert
            Assert.Throws<ArgumentException>(() => list.CopyTo((Array)arrgs, index));
        }

        [Test]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 })]
        [TestCase(new int[] { 123, 23})]
        [TestCase(new int[] { 123})]
        [TestCase(new int[] {})]
        public void Reverse_ReverseList_ReturnReverseList(int[] args)
        {
            //arrange
            int[] copyArgs = args;
            CircularList<int> list = new CircularList<int>(args);
            //acts
            list.Reverse();
            //assert
            for (int i = 0; i < args.Length; i++)
            {
                Assert.AreEqual(args[i], list.ElementAt(list.Count-1-i), message: "Reverse does not work correctly!");
            }
        }

        [Test]
        [TestCase(new int[] { 1,2,3,4}, 10)]
        public void GetEnumerator_EnumerateItemsInList_ReturnItemsFromList(Int32[] array, Int32 expectedV)
        {
            //arrange
            CircularList<Int32> list = new CircularList<int>(array);
            Int32 actV = 0;
            //acts
            foreach(var item in list)
            {
                actV += item;
            }
            //assert
            Assert.AreEqual(expectedV, actV, message: "GetEnumarator works incorectly");
        }
    }
}
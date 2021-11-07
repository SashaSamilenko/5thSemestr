using NUnit.Framework;
using CircularList;
using System;
using System.Collections.Generic;
using System.Collections;
using FluentAssertions;
using System.Data;
using System.Linq;
using NUnit.Framework.Constraints;

namespace TestProject
{
    public class CircularListTests
    {
        private MockForEvent<Int32> mock;
        private EventArgs mess;
        internal void VerifyMethod(object sender, CircleEventArgs e)
        {
            mess = e;
        }
        [Test]
        public void CircularList_ConstructorWithoutParametrs()
        {
            //average
            CircularList<int> list;
            //act
            list = new CircularList<int>();
            //assert
            list.Count.Should().Be(0);
        }

        [Test]
        [TestCase(-4)]
        [TestCase(0)]
        [TestCase(231)]
        [TestCase(-32000)]
        [TestCase(32000)]
        public void CircularList_SetItem_ReturnListWithThisItem(int element)
        {
            //arange
            CircularList<int> list;
            //act
            list = new CircularList<int>(element);
            //assert
            list[0].Should().Be(element);
        }

        [Test]
        public void CircularList_SetNullItem_GetArgumentNullException()
        {
            //arange
            String addingElement = null;
            CircularList<String> list;
            //act
            Action resultFunc = () => { list = new CircularList<String>(addingElement);  };
            //assert
            resultFunc.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void CircularList_SetALotOfItems_ReturnListWithItems()
        {
            //arange
            Int32[] elements = new int[] { 1, 2, 3, 4, 5, 6, 6, 754, 345, 3, 34, 12 };
            CircularList<int> list;
            //act
            list = new CircularList<int>(elements);
            //assert
            Int32 position = 0;
            foreach(var item in list)
            {
                item.Should().Be(elements[position]);
                position += 1;
            }
        }

        [Test]
        public void CircularList_SetALotOfItems_GetArgumentNullException()
        {
            //arrange
            Int32[] elements = null;
            CircularList<Int32> list;
            //act
            //Func<CircularList<String>>
            Action result = () => { list = new CircularList<Int32>(elements); };
            //assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        [TestCase(0, new Int32[] { 1 })]
        [TestCase(8, new Int32[] { 0, 1, 1, 22, 434, 1, 23, 4, 12, 13 })]
        public void ElementAt_GetElementFromListForIndex_ReturnDataElement(Int32 position, Int32[] elements)
        {
            //arange
            CircularList<Int32> list = new CircularList<Int32>(elements);
            Int32 expectedValue = elements[position];
            //act
            Int32 actResult = list.ElementAt(position);
            //assert
            actResult.Should().Be(expectedValue);
        }

        [Test]
        [TestCase(3, new int[] { 1 })]
        [TestCase(-1, new int[] { 0, 1, 1, 22, 434, 1, 23, 4, 12, 13 })]
        [TestCase(31, new int[] { 0, 1, 1, 22, 434, 1, 23, 4, 12, 13 })]
        public void ElementAt_GetElementFromListForWrongPosition_GetIndexOutOfRangeException(int position, int[] elements)
        {
            //arange
            CircularList<int> list = new CircularList<int>(elements);
            //act
            Func<Int32> result = () => list.ElementAt(position);
            //assert
            result.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void GetFirst_GetFirstElementOfTheList_ReturnFirstDataElementOfList()
        {
            //arange
            Int32[] elements = new int[] { -2, 3, 12, 424, 1 };
            CircularList<Int32> list = new CircularList<Int32>(elements);
            Int32 expectedValue = elements[0];
            //act
            Int32 actValue = list.GetFirst();
            //assert
            actValue.Should().Be(expectedValue);
        }

        [Test]
        public void GetLast_GetLastElementOfTheList_ReturnLastDataElementOfList()
        {
            //arange
            Int32[] elements = new Int32[] { -2, 3, 12, 424, 1 };
            Int32 expectedValue = 1;
            CircularList<Int32> list = new CircularList<Int32>(elements);
            //act
            Int32 result = list.GetLast();
            //assert
            result.Should().Be(expectedValue);
        }

        [Test]
        [TestCase(new int[] { }, 3)]
        [TestCase(new int[] { 12 }, -123)]
        [TestCase(new int[] { 11, 22, 33, 44, 144 }, 3)]
        [TestCase(new int[] { 11, 22, 33, 44, 132 }, -23)]
        public void Add_AddElementToTheEndOfList_ReturnListWithNewItem(int[] elements, int addItem)
        {
            //arange
            var list = new CircularList<int>(elements);
            //act
            list.Add(addItem);
            int actElement = list[list.Count - 1];
            //assets
            actElement.Should().Be(addItem);
        }

        [Test]
        [TestCase(new int[] { 11, 22, 33, 44, 132 }, -23)]
        public void Add_AddElementToTheEndOfList_GetReadOnlyException(int[] elements, int addItem)
        {
            //arange
            CircularList<int> list = new CircularList<int>(elements) { IsReadOnly = true};
            //act
            Action result = () =>list.Add(addItem);
            //assets
            result.Should().Throw<ReadOnlyException>();
        }

        [Test]
        public void Add_AddNullToTheList_GetArgumentNullException()
        {
            //arrange
            CircularList<object> list = new CircularList<object>();
            //act
            Action result = () => list.Add(null);
            //assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        [TestCase(new int[] { 11}, 3, 0)]
        [TestCase(new int[] { 11, 22, 33, 44, 144 }, 3, 4)]
        [TestCase(new int[] { 11, 22, 33, 44, 132, 121, 1212 }, -23, 5)]
        [TestCase(new int[] { 11, 22, 33, 44, 132, 121, 1212 }, -23, 0)]
        public void AddAt_AddElementToThePosition_ListWithNewItemReturn(int[] elements, int addItem, int position)
        {
            //arange
            CircularList<Int32> list = new CircularList<Int32>(elements);
            //act
            list.AddAt(addItem, position);
            Int32 actElement = list[position];
            //assets
            actElement.Should().Be(addItem);
        }

        [Test]
        [TestCase(new int[] { }, 3, 0)]
        [TestCase(new int[] { }, -123, 0)]
        [TestCase(new int[] { }, null, 0)]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, -123, -4)]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, -123, 12)]
        public void AddAt_AddElementToTheWrongPosition_GetIndexOutOfRangeException(int[] elements, int addItem, int position)
        {
            //arange
            var list = new CircularList<int>(elements);
            //act
            Action result = () => list.AddAt(addItem, position);
            //assert
            result.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        [TestCase(new string[] { "1", "2" }, null, 1)]
        [TestCase(new string[] { "1", "2", "3", "4", "5" }, null, 4)]
        public void AddAt_AddWrongElementToThePosition_GetNullReferenceException(string[] elements, string addItem, int index)
        {
            //arange
            var list = new CircularList<string>(elements);
            //act
            Action result = () => list.AddAt(addItem, index);
            //assert
            result.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void AddAt_AddElementToThePositionInReadOnlyList_GetReadOnlyException()
        {
            //arange
            CircularList<Int32> list = new CircularList<Int32>(1){IsReadOnly = true};
            //act
            Action result = () => list.AddAt(3, 0);
            //assert
            result.Should().Throw<ReadOnlyException>();
        }

        [Test]
        [TestCase(new int[] { 11, 22, 33, 44 })]
        [TestCase(new int[] { 23, 223, 4211, 45, 34 })]
        public void AddRange_AddElementsToTheEndOfList_ListWithNewItemsReturn(int[] addElements)
        {
            //arange
            CircularList<Int32> list = new CircularList<Int32>();
            //act
            list.AddRange(addElements);
            //assert
            Int32 arrayPosition = 0;
            foreach(var item in list)
            {
                item.Should().Be(addElements[arrayPosition]);
                arrayPosition += 1;
            }
        }

        [Test]
        public void AddRange_AddEmptyArrayOfElementToTheEndOfList_GetArgumentNullException()
        {
            //arange
            CircularList<String> list = new CircularList<String>();
            //act
            Action result = () => list.AddRange(null);
            //assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void AddRange_AddElementsWithNullToTheEndOfList_GetArgumentNullException()
        {
            //arange
            Object[] elements = new object[] { 2, 3, 4, null };
            CircularList<Object> list = new CircularList<Object>();
            //act
            Action result = () => list.AddRange(elements);
            //assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void AddRange_AddElementsToTheReadOnlyList_GetReadOnlyException()
        {
            //arange
            CircularList<String> list = new CircularList<String>() { IsReadOnly = true };
            //act
            Action result = () => list.AddRange(null);
            //assert
            result.Should().Throw<ReadOnlyException>();
        }

        [Test]
        [TestCase(22, new Int32[] { 22 })]
        [TestCase(22, new Int32[] { 21, 22 })]
        [TestCase(22, new Int32[] { 11, 22, 33, 44, 231, 332 })]
        [TestCase(44, new Int32[] { 11, 22, 33, 44, 231, 332 })]
        [TestCase(-144, new Int32[] { -144, -13, -1, 0, 11, 22, 33, 44, 231, 332 })]
        public void Remove_RemoveElementWithData_ReturnTrue(Int32 data, Int32[] elements)
        {
            //averrage
            CircularList<Int32> list = new CircularList<Int32>(elements);
            //act
            bool result = list.Remove(data);
            //assert
            result.Should().Be(true);
        }

        [Test]
        [TestCase(5, new Int32[] { 1, 2, 3, 4 })]
        [TestCase(2, new Int32[] { -22 })]
        [TestCase(-22, new Int32[] { 1, 2, 3, 4 })]
        [TestCase(121, new Int32[] { -22 })]
        public void Remove_TryToRemoveUnrealElementOfTheList_ReturnFalse(Int32 data, Int32[] elements)
        {
            //averrage
            CircularList<Int32> list = new CircularList<Int32>(elements);
            //act
            bool result = list.Remove(data);
            //assert
            result.Should().Be(false);
        }

        [Test]
        public void Remove_TryToRemoveElementFromEmptyList_ReturnFalse()
        {
            //averrage
            CircularList<Int32> list = new CircularList<Int32>();
            //act
            bool result = list.Remove(2);
            //assert
            result.Should().Be(false);
        }

        [Test]
        public void Remove_RemoveNullElement_GetArgumentNullException()
        {
            //averrage
            CircularList<String> list = new CircularList<String>(new String[] { "1", "2", "3" });
            //act
            Func<bool> result = () => list.Remove(null);
            //assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Remove_RemoveElementFromReadOnlyList_GetReadOnlyException()
        {
            //averrage
            CircularList<String> list = new CircularList<String>(new String[] { "1", "2", "3" }) { IsReadOnly = true};
            //act
            Func<bool> result = () => list.Remove("1");
            //assert
            result.Should().Throw<ReadOnlyException>();
        }

        [Test]
        [TestCase(new Int32[] { 1 })]
        [TestCase(new Int32[] { -2, 3, 12, 424, 1 })]
        public void RemoveFirst_RemoveFirstElementOfTheList_ReturnListWithoutFirstElement(Int32[] elements)
        {
            //arange
            CircularList<Int32> list = new CircularList<Int32>(elements);
            int expectedLength = elements.Length-1;
            //act
            list.RemoveFirst();
            Int32 actLength = list.Count;
            //assert
            Int32 arrayPosition = 1;
            foreach(var item in (IEnumerable)list)
            {
                item.Should().Be(elements[arrayPosition]);
                arrayPosition += 1;
            }
        }

        [Test]
        public void RemoveFirst_RemoveFirstElementFromEmptyList_ReturnPreviouslyList()
        {
            //arange
            CircularList<Int32> list = new CircularList<Int32>();
            //act
            list.RemoveFirst();
            //assert
            list.Should().HaveCount(0);
        }

        [Test]
        public void RemoveFirst_RemoveFirstElementFromReadOnlyList_GetReadOnlyException()
        {
            //arange
            CircularList<Int32> list = new CircularList<Int32>(new Int32[] { 1, 2, 3, 4, 5 }) { IsReadOnly = true};
            //act
            Action result = () => list.RemoveFirst();
            //assert
            result.Should().Throw<ReadOnlyException>();
        }

        [Test]
        [TestCase(new Int32[] { 3 })]
        [TestCase(new Int32[] { -2, 3, 12, 424, 1 })]
        public void RemoveLast_RemoveLastElementOfTheList_ReturnListWithoutLastElement(Int32[] elements)
        {
            //arange
            var list = new CircularList<int>(elements);
            Int32 expectedLength = elements.Length - 1;
            //act
            list.RemoveLast();
            Int32 actLength = list.Count;
            //assert
            Int32 arrayPosition = 0;
            foreach (var item in list)
            {
                item.Should().Be(elements[arrayPosition]);
                arrayPosition += 1;
            }
        }

        [Test]
        public void RemoveLast_RemoveLastElementFromEmptyList_ReturnPreviousList()
        {
            //arange
            CircularList<Int32> list = new CircularList<Int32>();
            //act
            list.RemoveLast();
            //assert
            list.Count.Should().Be(0);
        }

        [Test]
        public void RemoveLast_RemoveLastElementFromReadOnlyList_GetReadOnlyException()
        {
            //arange
            CircularList<Int32> list = new CircularList<Int32>(new Int32[] { 1, 2, 3, 4, 5 }) { IsReadOnly = true };
            //act
            Action result = () => list.RemoveLast();
            //assert
            result.Should().Throw<ReadOnlyException>();
        }

        [Test]
        [TestCase(0, new Int32[] { 31, 1232 })]
        [TestCase(2, new Int32[] { 31, 1232, 5 })]
        [TestCase(5, new Int32[] { 31, 12, 32, 41, 23, 1232 })]
        [TestCase(2, new Int32[] { 11, 22, 33, 44, 111 })]
        public void RemoveAt_RemoveElementOfTheListAtIndex_ReturnListWithoutElement(Int32 position, Int32[] elements)
        {
            //averrage
            CircularList<Int32> list = new CircularList<Int32>(elements);
            //act
            list.RemoveAt(position);
            //assert
            for (Int32 listPosition = 0; listPosition < elements.Length - 1; listPosition++)
            {
                if (listPosition < position)
                {
                    list[listPosition].Should().Be(elements[listPosition]);
                }
                else
                {
                    list[listPosition].Should().Be(elements[listPosition+1]);
                }
            }
        }

        [Test]
        [TestCase(33, new int[] { 1, 2, 3, 4, 51 })]
        [TestCase(-5, new int[] { 11, 22, 33, 44, 232 })]
        [TestCase(5, new int[] { })]
        public void RemoveAt_RemoveElementOfTheListAtIndex_GetIndexOutOfRangeException(Int32 index, Int32[] elements)
        {
            //averrage
            CircularList<Int32> list = new CircularList<Int32>(elements);
            //act
            Action result = () => list.RemoveAt(index);
            //assert
            result.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void RemoveAt_RemoveElementFromReadOnlyListAtIndex_GetReadOnlyException()
        {
            //averrage
            CircularList<Int32> list = new CircularList<Int32>(new Int32[] { 1, 2, 3, 4, 5 }) { IsReadOnly = true};
            Int32 deletingPosition = 1;
            //act
            Action result = () => list.RemoveAt(deletingPosition);
            //assert
            result.Should().Throw<ReadOnlyException>();
        }

        [Test]
        [TestCase(new Int32[] { 1 })]
        [TestCase(new Int32[] { 1, 2, 3, 4, 5, 6, 7, 8, 89, 0, 0, 1, 2, 21, 1, 1 })]
        public void Clear_ClearTheList_EmptyListReturn(Int32[] elements)
        {
            //arange
            CircularList<Int32> list = new CircularList<Int32>(elements);
            //act
            list.Clear();
            //assert
            list.Should().HaveCount(0);
        }

        [Test]
        public void Clear_ClearReadOnlyList_GetReadOnlyException()
        {
            //arrange
            CircularList<Int32> list = new CircularList<Int32>(new Int32[] { 1, 2, 3, 4 }) { IsReadOnly = true };
            //act
            Action result = () => list.Clear();
            //assert
            result.Should().Throw<ReadOnlyException>();
        }

        [Test]
        [TestCase(new Int32[] { 1 }, 1)]
        [TestCase(new Int32[] { 324, 24, -123, 23 }, 23)]
        public void Contains_CheckIfListContainsItem_ReturnTrue(Int32[] arrgs, Int32 item)
        {
            //arange
            CircularList<Int32> list = new CircularList<Int32>(arrgs);
            //act
            bool actCheking = list.Contains(item);
            //assert
            actCheking.Should().BeTrue();
        }

        [Test]
        [TestCase(new Int32[] { 1 }, 31)]
        [TestCase(new Int32[] { 324, 24, -123, 23 }, 25)]
        [TestCase(new Int32[] { 324, 24, -123, 23 }, null)]
        public void Contains_CheckIfListContainsItem_ReturnFalse(Int32[] arrgs, Int32 item)
        {
            //arange
            CircularList<Int32> list = new CircularList<Int32>(arrgs);
            //act
            bool actChecking = list.Contains(item);
            //assert
            actChecking.Should().BeFalse();
        }

        [Test]
        public void Contains_CheckIfEmptyListContainsItem_ReturnFalse()
        {
            //arrange
            CircularList<Int32> list = new CircularList<Int32>();
            //act
            bool actChecking = list.Contains(1);
            //assert
            actChecking.Should().BeFalse();
        }

        [Test]
        [TestCase(new Int32[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new Int32[] { -2, -3, -1, -4, -5 }, 5)]
        [TestCase(new Int32[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new Int32[] { -2, -3, -1, -4, -5 }, 3)]
        public void CopyTo_CopyListToArrgsStartingPosition_ReturnArrgsWithNewItemsFromList(Int32[] arrgs, Int32[] elements, Int32 index)
        {
            //arrange
            CircularList<Int32> list = new CircularList<Int32>(elements);
            //acts
            list.CopyTo(arrgs, index);
            //assert
            for (Int32 i = index; i < arrgs.Length - index - 1; i++)
            {
                arrgs[i].Should().Be(list[i - index]);
            }
        }

        [Test]
        [TestCase(null, new Int32[] { -2, -3, -1, -4, -5 }, 5)]
        [TestCase(null, new Int32[] { -2, -3, -1, -4, -5 }, 3)]
        public void CopyTo_CopyListToArrgsStartingPosition_GetNullReferenceException(Int32[] arrgs, Int32[] elements, Int32 index)
        {
            //arrange
            CircularList<Int32> list = new CircularList<Int32>(elements);
            //act
            Action result = () => list.CopyTo(arrgs, index);
            //assert
            result.Should().Throw<NullReferenceException>();
        }

        [Test]
        [TestCase(new Int32[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new Int32[] { -2, -3, -1, -4, -5 }, -2)]
        [TestCase(new Int32[] { 14, 2133, 3233, 1212 }, new Int32[] { 21 }, -12)]
        public void CopyTo_CopyListToArrgsStartingPositionLessThenZero_GetIndexOutOfRangeException(Int32[] arrgs, Int32[] elements, Int32 index)
        {
            //arrange
            CircularList<Int32> list = new CircularList<Int32>(elements);
            //act
            Action result = () => list.CopyTo(arrgs, index);
            //assert
            result.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        [TestCase(new Int32[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new Int32[] { -2, -3, -1, -4, -5 }, 7)]
        [TestCase(new Int32[] { 123, 23, 2, 12, 2, 3 }, new Int32[] { 11, 23, 13, 27 }, 4)]
        public void CopyTo_CopyListToSmallArrgsStartingPosition_GetArgumentException(Int32[] arrgs, Int32[] elements, Int32 index)
        {
            //arrange
            CircularList<Int32> list = new CircularList<Int32>(elements);
            //act
            Action result = () => list.CopyTo(arrgs, index);
            //assert
            result.Should().Throw<ArgumentException>();
        }

        [Test]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new int[] { -2, -3, -1, -4, -5 }, 5)]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new int[] { -2, -3, -1, -4, -5 }, 3)]
        public void CopyTo_CopyListToArrayStartingPosition_ReturnArrayWithNewItemsFromList(int[] arrgs, int[] elements, int index)
        {
            //arrange
            CircularList<int> list = new CircularList<int>(elements);
            //act
            list.CopyTo((Array)arrgs, index);
            //assert
            for (int i = index; i < arrgs.Length - index - 1; i++)
            {
                arrgs[i].Should().Be(list[i - index]);
            }
        }

        [Test]
        [TestCase(null, new int[] { -2, -3, -1, -4, -5 }, 5)]
        [TestCase(null, new int[] { -2, -3, -1, -4, -5 }, 3)]
        public void CopyTo_CopyListToArrayStartingPosition_GetNullReferenceException(int[] arrgs, int[] elements, int index)
        {
            //arrange
            CircularList<int> list = new CircularList<int>(elements);
            //act
            Action result = () => list.CopyTo((Array)arrgs, index);
            //assert
            result.Should().Throw<NullReferenceException>();
        }

        [Test]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new int[] { -2, -3, -1, -4, -5 }, -2)]
        [TestCase(new int[] { 14, 2133, 3233, 1212 }, new int[] { 21 }, -12)]
        public void CopyTo_CopyListToArrayStartingPositionLessThenZero_GetIndexOutOfRangeException(int[] arrgs, int[] elements, int index)
        {
            //arrange
            CircularList<int> list = new CircularList<int>(elements);
            //act
            Action result = () => list.CopyTo((Array)arrgs, index);
            //assert
            result.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 }, new int[] { -2, -3, -1, -4, -5 }, 7)]
        [TestCase(new int[] { 123, 23, 2, 12, 2, 3 }, new int[] { 11, 23, 13, 27 }, 4)]
        public void CopyTo_CopyListToSmallArrayStartingPosition_GetArgumentException(int[] arrgs, int[] elements, int index)
        {
            //arrange
            CircularList<int> list = new CircularList<int>(elements);
            //act
            Action result = () => list.CopyTo((Array)arrgs, index);
            //assert
            result.Should().Throw<ArgumentException>();
        }

        [Test]
        [TestCase(new Int32[] { 123, 23, 2, 12, 2, 3, 2, 1, 2, 3 })]
        [TestCase(new Int32[] { 123 })]
        [TestCase(new Int32[] { })]
        public void Reverse_ReverseList_ReturnReverseList(Int32[] args)
        {
            //arrange
            CircularList<Int32> list = new CircularList<Int32>(args);
            //act
            list.Reverse();
            //assert
            for (Int32 i = 0; i < args.Length; i++)
            {
                list[list.Count - 1 - i].Should().Be(args[i]);
            }
        }

        [Test]
        public void Reverse_ReverseReadOnlyList_GetReadOnlyException()
        {
            //arrange
            CircularList<Int32> list = new CircularList<Int32>(new Int32[] { 1, 2, 3, 4, 8, 7, 6, 5, 8 }) { IsReadOnly = true};
            //act
            Action result = () => list.Reverse();
            //assert
            result.Should().Throw<ReadOnlyException>();
        }
        [Test]
        [TestCase(new Int32[] { })]
        [TestCase(new Int32[] { 1, 2, 3, 4 })]
        public void GetEnumerator_EnumerateItemsInList_ReturnItemsFromList(Int32[] array)
        {
            //arrange
            CircularList<Int32> list = new CircularList<Int32>(array);
            Int32 position = 0;
            //act
            foreach (var item in list)
            {
                //assert
                item.Should().Be(list[position]);
                position += 1;
            }
        }

        [Test]
        public void EmptyListEventMethod_ClearEmptyList_CatchEventCall()
        {
            //arrange
            mock = new MockForEvent<int>();
            mock.Object.emptyListEvent += VerifyMethod;
            //act
            mock.Object.Clear();
            //assert
            mess.Should().NotBeNull();
            mess.GetType().Should().Be(typeof(CircleEventArgs));
        }

        [Test]
        public void EmptyListEventMethod_RemoveElementFromTheList_CatchEventCall()
        {
            //arrange
            mock = new MockForEvent<int>(5);
            mock.Object.emptyListEvent += VerifyMethod;
            //act
            mock.Object.Remove(5);
            //assert
            mess.Should().NotBeNull();
            mess.GetType().Should().Be(typeof(CircleEventArgs));
        }

        [Test]
        public void EmptyListEventMethod_RemoveFirstElementFromTheList_CatchEventCall()
        {
            //arrange
            mock = new MockForEvent<int>(5);
            mock.Object.emptyListEvent += VerifyMethod;
            //act
            mock.Object.RemoveFirst();
            //assert
            mess.Should().NotBeNull();
            mess.GetType().Should().Be(typeof(CircleEventArgs));
        }

        [Test]
        public void EmptyListEventMethod_RemoveLastElementFromTheList_CatchEventCall()
        {
            //arrange
            mock = new MockForEvent<int>(5);
            mock.Object.emptyListEvent += VerifyMethod;
            //act
            mock.Object.RemoveLast();
            //assert
            mess.Should().NotBeNull();
            mess.GetType().Should().Be(typeof(CircleEventArgs));
        }

        [Test]
        public void EmptyListEventMethod_RemoveAtPositionElementFromTheList_CatchEventCall()
        {
            //arrange
            mock = new MockForEvent<int>(5);
            mock.Object.emptyListEvent += VerifyMethod;
            //act
            mock.Object.RemoveAt(0);
            //assert
            mess.Should().NotBeNull();
            mess.GetType().Should().Be(typeof(CircleEventArgs));
        }
        /*[Test]
        public void EmptyListEventMethod_RemoveAtPositionElementFromTheList_CatchEventCall()
        {
            //arange
            CircularList<Int32> list = new CircularList<int>(12);
            FakeObjectForFollowingEvent moke = new FakeObjectForFollowingEvent();
            list.emptyListEvent += moke.FakeFollowingMethod;
            //act
            list.RemoveAt(0);
            //assert
            moke.CircularList.Should().NotBeNull();
            moke.EventArgs.Should().NotBeNull();
        }*/

        [Test]
        [TestCase(new Int32[] { }, 0)]
        [TestCase(new Int32[] { 1,2,3,4,5}, -1)]
        [TestCase(new Int32[] { 1, 2, 3, 4, 5 }, 8)]
        public void Indexator_TryGetElementWithWrongPosition_GetIndexOutOfRangeException(Int32[] elements, Int32 position)
        {
            //arrange
            CircularList<Int32> list = new CircularList<Int32>(elements);
            //act
            Func<Int32> result = () => list[position];
            //assert
            result.Should().Throw<IndexOutOfRangeException>();
        }
    }
}
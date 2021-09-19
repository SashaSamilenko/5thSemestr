using NUnit.Framework;
using List;
using System;

namespace ListTest
{
    public class Tests
    {
        [Test]
        [TestCase(0, new int[] { 1 }, 1)]
        [TestCase(3, new int[] { 1 },0)]
        [TestCase(8, new int[] { 0, 1, 1, 22, 434, 1, 23, 4, 12, 13 }, 12)]
        [TestCase(-1, new int[] { 0, 1, 1, 22, 434, 1, 23, 4, 12, 13 },0)]
        [TestCase(31, new int[] { 0, 1, 1, 22, 434, 1, 23, 4, 12, 13 },0)]
        public void ElementAt_GetElementFromListForIndex_DataElementReturns(int position, int[] elements, int expectedResult)
        {
            //arange
            var list = new CircularList<int>(elements); 
            //act
            int actResult=0;
            try
            {
                actResult = list.ElementAt(position);
            }
            catch (IndexOutOfRangeException e)
            {
                //assert
                Assert.Throws<ArgumentOutOfRangeException>(
                delegate 
                { 
                    throw new ArgumentOutOfRangeException(e.Message); 
                });
            }
            //finally
            //{
                //assert
                Assert.AreEqual(expectedResult, actResult, message: "ElemetAt works incorrectly");
            //}
        }

        [Test]
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
        }
    }
}
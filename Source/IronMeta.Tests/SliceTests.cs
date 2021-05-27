using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronMeta.Utils;
using IronMeta.Utils.Slices;

#if __MonoCS__
using NUnit.Framework;
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace IronMeta.UnitTests
{
    [TestClass]
    public class SliceTests
    {
        private static IEnumerable<T> EnumerateValues<T>(params T[] items)
        {
            for (int i = 0; i < items.Length; i++)
                yield return items[i];
        }


        [TestMethod]
        public void TestIndexOfHandlesNullsForIList()
        {
            var list = new object[] { 0, 1, null, 3 };
            var slice = new Slice<object>(list, 1, 2);
            var actual = slice.IndexOf(null);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void TestIndexOfHandlesNullsForNonIList()
        {
            var list = "asdfj".Cast<object>().Concat(new object[] { null });
            var slice = new Slice<object>(list, 4, 2);
            var actual = slice.IndexOf(null);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void TestIndexOfTakesIntoAccountStartForIList()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice = new Slice<int>(list, 1, 2);
            var actual = slice.IndexOf(0);
            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void TestIndexOfTakesIntoAccountCountForIList()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice = new Slice<int>(list, 1, 2);
            var actual = slice.IndexOf(4);
            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void TestIndexOfTakesIntoAccountStartForNonIList()
        {
            var input = "asdfj";
            var slice = new Slice<char>(input, 1, 2);
            var actual = slice.IndexOf('a');
            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void TestIndexOfTakesIntoAccountCountForNonIList()
        {
            var input = "asdfj";
            var slice = new Slice<char>(input, 1, 2);
            var actual = slice.IndexOf('j');
            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void TestIndexerTakesIntoAccountStartForIList()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice = new Slice<int>(list, 2, 2);
            var actual = slice[0];
            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void TestIndexerTakesIntoAccountCountForIList()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice = new Slice<int>(list, 2, 2);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => slice[2]);
        }

        [TestMethod]
        public void TestIndexerTakesIntoAccountStartForNonIList()
        {
            var list = "asdfj";
            var slice = new Slice<char>(list, 2, 2);
            var actual = slice[0];
            Assert.AreEqual('d', actual);
        }

        [TestMethod]
        public void TestIndexerTakesIntoAccountCountForNonIList()
        {
            var list = "asdfj";
            var slice = new Slice<char>(list, 2, 2);
            Assert.ThrowsException<IndexOutOfRangeException>(() => slice[2]);
        }

        [TestMethod]
        public void TestInsertPlacesNewElementInCorrectPosition()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice = new Slice<int>(list, 2, 2);
            Assert.ThrowsException<InvalidOperationException>(() => slice.Insert(0, 5));
        }

        [TestMethod]
        public void TestAddPlacesNewElementAtEndOfCollection()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice = new Slice<int>(list, 2, 2);
            Assert.ThrowsException<InvalidOperationException>(() => slice.Add(5));
        }

        [TestMethod]
        public void TestClearEmptiesTheSlice()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice = new Slice<int>(list, 2, 2);
            Assert.ThrowsException<InvalidOperationException>(() => slice.Clear());
        }

        [TestMethod]
        public void TestContainsReturnsTrueWhenEqualItemInSlice()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice = new Slice<int>(list, 2, 2);
            var actual = slice.Contains(3);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TestContainsReturnsFalseWhenEqualItemNotInSlice()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice = new Slice<int>(list, 2, 2);
            var actual = slice.Contains(0);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TestCopyToCopiesAllSliceElementsToCorrectLocationInDestination()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice = new Slice<int>(list, 2, 2);
            var expected = new[] { 0, 2, 3 };
            var actual = new int[3];
            slice.CopyTo(actual, 1);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestRemoveRemovesFoundItemAndReturnsTrueWhenItemFound()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice = new Slice<int>(list, 2, 2);
            Assert.ThrowsException<InvalidOperationException>(() => slice.Remove(2));
        }

        [TestMethod]
        public void TestRemoveDoesNotRemovesAnythingAndReturnsFalseWhenItemNotFound()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice = new Slice<int>(list, 2, 2);
            Assert.ThrowsException<InvalidOperationException>(() => slice.Remove(4));
        }

        [TestMethod]
        public void TestShouldContainCorrectElementsWhenMadeFromExistingSliceForArray()
        {
            var list = new[] { 0, 1, 2, 3, 4 };
            var slice1 = new Slice<int>(list, 2, 3);
            var slice2 = new Slice<int>(slice1, 1, 2);
            CollectionAssert.AreEqual(list[3..], slice2.ToArray());
        }

        [TestMethod]
        public void TestShouldContainCorrectElementsWhenMadeFromExistingSliceForList()
        {
            var list = new List<int> { 0, 1, 2, 3, 4 };
            var slice1 = new Slice<int>(list, 2, 3);
            var slice2 = new Slice<int>(slice1, 1, 2);
            CollectionAssert.AreEqual(new int[] { 3, 4 }, slice2.ToArray());
        }

        [TestMethod]
        public void TestShouldContainCorrectElementsWhenMadeFromExistingSliceForEnumerable()
        {
            var list = EnumerateValues(0, 1, 2, 3, 4);
            var slice1 = new Slice<int>(list, 2, 3);
            var slice2 = new Slice<int>(slice1, 1, 2);
            CollectionAssert.AreEqual(new int[] { 3, 4 }, slice2.ToArray());
        }

        [TestMethod]
        public void TestShouldContainCorrectElementsWhenMadeFromExistingSliceForString()
        {
            var list = "asdfj";
            var slice1 = new Slice<char>(list, 2, 3);
            var slice2 = new Slice<char>(slice1, 1, 2);
            CollectionAssert.AreEqual(new char[] { 'f', 'j' }, slice2.ToArray());
        }
    }
}

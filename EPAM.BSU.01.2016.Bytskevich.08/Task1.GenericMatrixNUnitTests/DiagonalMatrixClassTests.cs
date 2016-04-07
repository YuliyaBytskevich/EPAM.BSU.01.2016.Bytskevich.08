using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using  Task1.GenericMatirx;

namespace Task1.GenericMatrixNUnitTests
{
    [TestFixture]
    class DiagonalMatrixClassTests
    {
        #region Test method: ctor public DiagonalMatrix(int order)
        [TestCase(3, Result = "0 0 0 \n0 0 0 \n0 0 0 \n")]
        [TestCase(-10, ExpectedException = typeof(ArgumentException))]
        public string CreateNewIntMatrix_WithOrder(int order)
        {
            return new DiagonalMatrix<int, EventArgs>(order).ToString();
        }

        [TestCase(3, Result = "   \n   \n   \n")]
        [TestCase(-10, ExpectedException = typeof(ArgumentException))]
        public string CreateNewStringMatrix_WithOrder(int order)
        {
            return new DiagonalMatrix<string, EventArgs>(order).ToString();
        }
        #endregion

        #region Test method: SetCellValue(int rowIndex, int columnIndex, T value)
        [TestCase(1, 2, 100, Result = "0 0 0 \n0 0 0 \n0 0 0 \n")]
        [TestCase(5, 5, 100, ExpectedException = typeof(ArgumentOutOfRangeException))]
        public string SetCellValueInIntMatrix3x3_WithIndexes(int i, int j, int value)
        {
            DiagonalMatrix<int, EventArgs> m = new DiagonalMatrix<int, EventArgs>(3);
            m.SetCellValue(i, j, value);
            return m.ToString();
        }

        [TestCase(1, 2, "hello", Result = "   \n   \n   \n")]
        [TestCase(-10, 0, "i'm gonna crush it", ExpectedException = typeof(ArgumentOutOfRangeException))]
        public string SetCellValueInStringMatrix3x3_WithIndexes(int i, int j, string value)
        {
            DiagonalMatrix<string, EventArgs> m = new DiagonalMatrix<string, EventArgs>(3);
            m.SetCellValue(i, j, value);
            return m.ToString();
        }
        #endregion

        #region Test method: GetCellValue(int rowIndex, int columnIndex)
        [TestCase(1, 1, Result = ":)")]
        [TestCase(0, 1, Result = null)]
        [TestCase(1, 2, ExpectedException = typeof(ArgumentOutOfRangeException))]
        public string GetCellValueFromStringMatrix2x2_WithIndexes(int i, int j)
        {
            DiagonalMatrix<string, EventArgs> m = new DiagonalMatrix<string, EventArgs>(2);
            m.SetCellValue(0, 0, "hello");
            m.SetCellValue(0, 1, "world");
            m.SetCellValue(1, 0, "!");
            m.SetCellValue(1, 1, ":)");
            return m.GetCellValue(i, j);
        }
        #endregion

        #region Test method: operator +(SquareMatrix<T, U> first, SquareMatrix<T, U> second)
        [TestCase(Result = "30 0 0 \n0 30 0 \n0 0 20 \n")]
        public string SumTwoIntMatrix_2x2_and_3x3()
        {
            DiagonalMatrix<int, EventArgs> first = new DiagonalMatrix<int, EventArgs>(2);
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    first.SetCellValue(i, j, 10);
            DiagonalMatrix<int, EventArgs> second = new DiagonalMatrix<int, EventArgs>(3);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    second.SetCellValue(i, j, 20);
            SquareMatrix<int, EventArgs> sum = first + second;
            return sum.ToString();
        }

        [TestCase(ExpectedException = typeof(ArgumentNullException))]
        public string SumTwoIntMatrix_2x2_and_null()
        {
            DiagonalMatrix<int, EventArgs> first = new DiagonalMatrix<int, EventArgs>(2);
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    first.SetCellValue(i, j, 10);
            SquareMatrix<int, EventArgs> second = null;
            SquareMatrix<int, EventArgs> sum = first + second;
            return sum.ToString();
        }

        [TestCase(ExpectedException = typeof(InvalidOperationException))]
        public string SumTwoStringMatrix_1x1_and_2x2()
        {
            DiagonalMatrix<string, EventArgs> first = new DiagonalMatrix<string, EventArgs>(1);
            first.SetCellValue(0, 0, "BANG!");
            DiagonalMatrix<string, EventArgs> second = new DiagonalMatrix<string, EventArgs>(2);
            SquareMatrix<string, EventArgs> sum = first + second;
            return sum.ToString();
        }
        #endregion

    }
}

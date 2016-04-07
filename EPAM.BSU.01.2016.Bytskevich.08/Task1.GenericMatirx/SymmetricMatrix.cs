using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.GenericMatirx
{
    public class SymmetricMatrix<T, U> : SquareMatrix<T, U> where U : new()
    {
        public SymmetricMatrix(int order)
        {
            if (order <= 0)
                throw new ArgumentException("Matrix order must be a positive integer value");
            elements = new T[order, order];
            Order = order;
        }

        protected override bool ValidateInputElement(int rowIndex, int columnIndex, T value)
        {
            if (rowIndex < 0 || rowIndex >= Order || columnIndex < 0 || columnIndex >= Order)
                throw new ArgumentOutOfRangeException();
            if (rowIndex != columnIndex)
            {
                elements[rowIndex, columnIndex] = value;
                elements[columnIndex, rowIndex] = value;
                return true;
            }
            elements[rowIndex, columnIndex] = value;
            return false;
        }

    }
}

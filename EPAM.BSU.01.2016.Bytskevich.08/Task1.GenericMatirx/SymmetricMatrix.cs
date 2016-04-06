using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.GenericMatirx
{
    public class SymmetricMatrix<T> : SquareMatrix<T> 
    {
        public SymmetricMatrix(int order)
        {
            elements = new T[order, order];
            Order = order;
            changeHandler = new ElementChangedHandler();
            changeHandler.ElementChanged += ActWhenElementIsChanged;
        }

        public override void SetCellValue(int rowIndex, int columnIndex, T value)
        {
            changeHandler.ActAsElementIsChanged(ValidateInputElement, rowIndex, columnIndex, value, "Symmetric matrix");
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

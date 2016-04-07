using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.GenericMatirx
{
    public class DiagonalMatrix<T> : SquareMatrix<T>
{
        public DiagonalMatrix(int order, ElementChangedHandler handler)
        {
            elements = new T[order, order];
            Order = order;
            changesHandler = handler;
            changesHandler.ElementChanged += DefaultEvent;
        }

        protected override bool ValidateInputElement(int rowIndex, int columnIndex, T value)
        {
            if (rowIndex < 0 || rowIndex >= Order || columnIndex < 0 || columnIndex >= Order)
                throw new ArgumentOutOfRangeException();
            if (rowIndex == columnIndex)
            {
                elements[rowIndex, columnIndex] = value;
                return true;
            }
            elements[rowIndex, columnIndex] = default(T);
            return false;
        }

        protected override void DefaultEvent(Object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("DIAGONAL MATRIX: element change event");
        }

    }
}

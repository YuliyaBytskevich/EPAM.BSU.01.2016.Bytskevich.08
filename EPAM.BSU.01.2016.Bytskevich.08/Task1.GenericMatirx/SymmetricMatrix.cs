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
        public SymmetricMatrix(int order, ElementChangedHandler handler)
        {
            elements = new T[order, order];
            Order = order;
            changesHandler = handler; //TODO: make it not reqired parameter
            changesHandler.ElementChanged += DefaultEvent;
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

        protected override void DefaultEvent(Object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("SYMMETRIC MATRIX: element change event");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task1.GenericMatirx
{
    public class SquareMatrix<T>
    {
        public int Order { get; protected set; }
        protected T[,] elements;
        protected static ElementChangedHandler changesHandler;

        protected SquareMatrix() { }

        public SquareMatrix(int order, ElementChangedHandler handler)
        {
            elements = new T[order, order];
            Order = order;
            changesHandler = handler;
            changesHandler.ElementChanged += DefaultEvent;
        }

        public virtual void SetCellValue(int rowIndex, int columnIndex, T value)
        {
            ValidateInputElement(rowIndex, columnIndex, value);
            changesHandler.ActAsElementIsChanged();
        }

        public T GetCellValue(int rowIndex, int columnIndex)
        {
            return elements[rowIndex, columnIndex];
        }

        public static SquareMatrix<T> operator +(SquareMatrix<T> first, SquareMatrix<T> second)
        {
            SquareMatrix<T> result = new SquareMatrix<T>(Math.Max(first.Order, second.Order), changesHandler);
            try
            {
                for (int i = 0; i < first.Order; i++)
                    for (int j = 0; j < first.Order; j++)
                        result.SetCellValue(i, j, first.GetCellValue(i, j));
                for (int i = 0; i < second.Order; i++)
                    for (int j = 0; j < second.Order; j++)
                        result.SetCellValue(i, j, Add(result.GetCellValue(i, j), second.GetCellValue(i, j)));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException();
            }
            return result;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Order; i++)
            {
                for (int j = 0; j < Order; j++)
                    result += elements[i, j] + " ";
                result += "\n";
            }
            return result;
        }

        protected virtual bool ValidateInputElement(int rowIndex, int columnIndex, T value)
        {
            if (rowIndex < 0 || rowIndex >= Order || columnIndex < 0 || columnIndex >= Order)
                throw new ArgumentOutOfRangeException();
            elements[rowIndex, columnIndex] = value;
            return true;
        }

        protected virtual void DefaultEvent(Object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("SQUARE MATRIX: element change event");
        } 

        private static T Add<T>(T a, T b)
        {
            ParameterExpression paramA = Expression.Parameter(typeof(T), "a"), paramB = Expression.Parameter(typeof(T), "b");
            BinaryExpression body = Expression.Add(paramA, paramB);
            Func<T, T, T> add = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();
            return add(a, b);
        }

    }

}




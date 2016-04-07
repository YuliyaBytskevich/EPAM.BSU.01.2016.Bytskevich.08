using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task1.GenericMatirx
{
    public class SquareMatrix<T, U>: IChangesHandleable<U> where U: new()
    {
        public int Order { get; protected set; }
        protected T[,] elements;
        protected ElementChangedHandler<U> handler;

        protected SquareMatrix() { }

        public SquareMatrix(int order)
        {
            if (order <= 0 )
                throw new ArgumentException("Matrix order must be a positive integer value");
            elements = new T[order, order];
            Order = order;
        }

        public virtual void SetCellValue(int rowIndex, int columnIndex, T value)
        {
            ValidateInputElement(rowIndex, columnIndex, value);
            handler?.ActAsElementIsChanged();
        }

        public T GetCellValue(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= Order || columnIndex < 0 || columnIndex >= Order)
                throw new ArgumentOutOfRangeException();
            return elements[rowIndex, columnIndex];
        }

        public static SquareMatrix<T, U> operator +(SquareMatrix<T, U> first, SquareMatrix<T, U> second)
        {
            if (first == null || second == null)
                throw new ArgumentNullException();
            SquareMatrix<T, U> result = new SquareMatrix<T, U>(Math.Max(first.Order, second.Order));
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

        private static T Add<T>(T a, T b)
        {
            ParameterExpression paramA = Expression.Parameter(typeof(T), "a"), paramB = Expression.Parameter(typeof(T), "b");
            BinaryExpression body = Expression.Add(paramA, paramB);
            Func<T, T, T> add = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();
            return add(a, b);
        }

        #region IChangesHandleable methods
        public void EnableHandlingOnChanging(ElementChangedHandler<U> handler)
        {
            this.handler = handler;
        }

        public void DisableHandlingOnChanging()
        {
            handler = null;
        }

        public void AddCustomEventOnChanging(EventHandler<U> customEvent)
        {
            handler.ElementChanged += customEvent;
        }
        #endregion
    }

}




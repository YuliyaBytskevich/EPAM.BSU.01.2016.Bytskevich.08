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
        #region ElementChangedEventArgs
        protected internal class ElementChangedEventArgs : EventArgs
        {
            public Func<int, int, T, bool> CheckingFunction { get; }
            public int ElementRowIndex { get; }
            public int ElementColumnIndex { get; }
            public T ElementValue{ get; }
            public string Message { get;  }

            public ElementChangedEventArgs(Func<int, int, T, bool> checkingFunction, int rowIndex, int columnIndex, T value, string message)
            {
                CheckingFunction = checkingFunction;
                ElementRowIndex = rowIndex;
                ElementColumnIndex = columnIndex;
                ElementValue = value;
                Message = message;
            }
        }
        #endregion

        #region ElementChangedHandler
        protected internal class ElementChangedHandler
        {
            public event EventHandler<ElementChangedEventArgs> ElementChanged = delegate { };

            protected virtual void OnElementChanged(object sender, ElementChangedEventArgs e)
            {
                ElementChanged(sender, e);
            }

            public void ActAsElementIsChanged(Func<int, int, T, bool> checkingFunction, int rowIndex, int columnIndex, T value, string message)
            {
                OnElementChanged(this, new ElementChangedEventArgs(checkingFunction, rowIndex, columnIndex, value, message));
            }
        }
        #endregion

        public int Order { get; protected set; }
        protected T[,] elements;
        protected ElementChangedHandler changeHandler;

        protected SquareMatrix() { }
        public SquareMatrix(int order)
        {
            elements = new T[order, order];
            Order = order;
            changeHandler = new ElementChangedHandler();
            changeHandler.ElementChanged += ActWhenElementIsChanged;
        }

        public virtual void SetCellValue(int rowIndex, int columnIndex, T value)
        {
            changeHandler.ActAsElementIsChanged(ValidateInputElement, rowIndex, columnIndex, value, "Square matrix");
        }

        public T GetCellValue(int rowIndex, int columnIndex)
        {
            return elements[rowIndex, columnIndex];
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

        protected virtual void ActWhenElementIsChanged(Object sender, ElementChangedEventArgs eventArgs)
        {
            bool validationResult = eventArgs.CheckingFunction(eventArgs.ElementRowIndex, eventArgs.ElementColumnIndex,
                eventArgs.ElementValue);
            Debug.WriteLine(eventArgs.Message + ": validation result = " + validationResult);
            // do something with eventArgs.Message
        }
        
        public static SquareMatrix<T> operator +(SquareMatrix<T> first, SquareMatrix<T> second)
        {
            SquareMatrix<T> result = new SquareMatrix<T>(Math.Max(first.Order, second.Order));
            /*var addOpration = typeof (T).GetMethod("op_Addition");
            if (addOpration != null)
            {
                for (int i = 0; i < first.Order; i++)
                    for (int j = 0; j < first.Order; j++)
                           result.SetCellValue(i, j, Add(first.GetCellValue(i, j), second.GetCellValue(i, j)));
            }
            else
                throw new InvalidOperationException();*/
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

        private static T Add<T>(T a, T b)
        {
            ParameterExpression paramA = Expression.Parameter(typeof(T), "a"),
                paramB = Expression.Parameter(typeof(T), "b");
            BinaryExpression body = Expression.Add(paramA, paramB);
            Func<T, T, T> add = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();
            return add(a, b);
        }

    }

}




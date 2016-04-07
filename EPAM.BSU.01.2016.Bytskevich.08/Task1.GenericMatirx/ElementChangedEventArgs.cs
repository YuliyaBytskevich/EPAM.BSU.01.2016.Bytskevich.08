using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.GenericMatirx
{
    public class ElementChangedEventArgs<T> : EventArgs
    {
        public Func<int, int, T, bool> DoSomethingWithElement { get; }
        public int ElementRowIndex { get; }
        public int ElementColumnIndex { get; }
        public T ElementValue { get; }
        public string Message { get; }

        public ElementChangedEventArgs() { } 

        public ElementChangedEventArgs(Func<int, int, T, bool> doSomethingWithElement, int rowIndex, int columnIndex, T value, string message)
        {
            DoSomethingWithElement = doSomethingWithElement;
            ElementRowIndex = rowIndex;
            ElementColumnIndex = columnIndex;
            ElementValue = value;
            Message = message;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.GenericMatirx
{
    public class ElementChangedHandler<T> where T : new()
    {
        public event EventHandler<T> ElementChanged = delegate { };

        protected virtual void OnElementChanged(object sender, T e)
        {
            ElementChanged(sender, e);
        }

        public void ActAsElementIsChanged()
        {
            OnElementChanged(this, new T());
        }

        public void ClearEventsList()
        {
            foreach (Delegate d in ElementChanged.GetInvocationList())
            {
                ElementChanged-= (EventHandler<T>)d;
            }
        }

    }
}

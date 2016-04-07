using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.GenericMatirx
{
    public class ElementChangedHandler
    {
        public event EventHandler<EventArgs> ElementChanged = delegate { };

        protected virtual void OnElementChanged(object sender, EventArgs e)
        {
            ElementChanged(sender, e);
        }

        public void ActAsElementIsChanged()
        {
            OnElementChanged(this, new EventArgs());
        }
    }
}

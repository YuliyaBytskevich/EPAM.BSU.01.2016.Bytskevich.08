using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.GenericMatirx
{
    public interface IChangesHandleable<T> where T : new()
    {
        void EnableHandlingOnChanging(ElementChangedHandler<T> handler);

        void DisableHandlingOnChanging();

        void AddCustomEventOnChanging(EventHandler<T> customEvent);

    }
}

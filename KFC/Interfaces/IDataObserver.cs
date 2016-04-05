using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.Interfaces
{
    interface IDataObserver
    {
        void OnData(SenderData data);
    }
}

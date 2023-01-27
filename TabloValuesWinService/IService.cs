using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace TabloValuesWinService
{
    [ServiceContract]
    [ServiceKnownType(typeof(TabloCell))]
    public interface IService
    {

        [OperationContract]
        IList<TabloCell> GetTabloValues();

    }
}

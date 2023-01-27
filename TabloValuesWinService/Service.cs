using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace TabloValuesWinService
{
    public class Service : IService
    {
        public IList<TabloCell> GetTabloValues()
        {
            var sqlBase = new OperationDataBase();
            return sqlBase.GetLastTrainEvents(ConfigurationManager.ConnectionStrings["tablo"].ConnectionString);

        }
    }
}

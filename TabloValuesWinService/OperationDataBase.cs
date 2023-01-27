using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace TabloValuesWinService
{
    public class OperationDataBase
    {

        public IList<TabloCell> GetLastTrainEvents(string connectionString)
        {
            var result = new List<TabloCell>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Station1, Station2, Name, Value, R, G, B  FROM TabloValue", connection);
                //
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                    result.Add(new TabloCell()
                                    {
                                        Station1 = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                        Station2 = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                        Name = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                        Value = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                        R = reader.GetByte(4),
                                        G = reader.GetByte(5),
                                        B = reader.GetByte(6),
                                    });
                            }
                            catch { }
                        }
                    }
                }
            }
            //
            return result;
        }

    }
}

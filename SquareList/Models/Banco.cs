using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SquareList.Models
{
   
        public class Banco : IDisposable
        {
            private readonly SqlConnection connection;
            public Banco()
            {
                connection = new SqlConnection(@"Data Source=LAPTOP-D6R29BOD;Initial Catalog=dbTCC;User ID=sa;Password=123456;MultipleActiveResultSets = true;" );

                connection.Open();
            }
            public void Executar(string StrQery)
            {
                var CommandE = new SqlCommand
                {
                    CommandText = StrQery,
                    CommandType = CommandType.Text,
                    Connection = connection
                };
                CommandE.ExecuteNonQuery();
            }
            public SqlDataReader Retorno(string StQuery)

            {
                var CommandS = new SqlCommand(StQuery, connection);
                return CommandS.ExecuteReader();
            }
            public void Dispose()
            {
                connection.Close();
            }
        }
    
}
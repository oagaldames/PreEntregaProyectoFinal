using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PreEntregaProyectoFinal.Clases
{
    public class DataSql
    {

        public  string StrServidor { get; set; }
        public string StrDatabase { get; set; }

        public SqlConnection Connection { get; set; }


        public DataSql()
        {
           StrServidor = string.Empty;
           StrDatabase = string.Empty;
            
        }

        
        public DataSql(string strServidor, string strDatabase, SqlConnection connection)
        {
            StrServidor = strServidor;
            StrDatabase = strDatabase;
            Connection = connection;
        }

        public bool ConectarSQL()
        {
           try
           { 
                SqlConnectionStringBuilder connectionBuilder = new();
                connectionBuilder.DataSource = StrServidor;
                connectionBuilder.InitialCatalog = StrDatabase;
                connectionBuilder.IntegratedSecurity = true;
                var cs = connectionBuilder.ConnectionString;
                Connection = new SqlConnection(cs);
                Connection.Open();

                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                string error = err.Message;
                Console.WriteLine("\nERROR ConectarSQL  " + error);
                return false;
            }


        }


        public void DesconectarSQL()
        {

            if (Connection != null)
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }
    }
}

 

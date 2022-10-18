using PreEntregaProyectoFinal.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PreEntregaProyectoFinal.Metodos
{
    public class MetodosVentas
    {
        public static List<Venta> TraerVentasPorUsuario(int IdUsuario)
        {
            var listaVenta = new List<Venta>();
            try
            { 
                DataSql db = new DataSql();
               
                if (db.ConectarSQL())
                {
                    SqlCommand cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Venta " +
                                      "WHERE IdUsuario = @IdUsu";

                    var paramIdUsu = new SqlParameter("IdUsu", SqlDbType.BigInt);
                    paramIdUsu.Value = IdUsuario;
                    cmd.Parameters.Add(paramIdUsu);
                    
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var venta = new Venta();
                        venta.Id = Convert.ToInt32(reader.GetValue(0).ToString());
                        venta.Comentarios = reader.GetValue(1).ToString();
                        venta.IdUsuario = Convert.ToInt32(reader.GetValue(2));

                        listaVenta.Add(venta);

                    }
                    reader.Close();
                    db.DesconectarSQL();
                }
                return listaVenta;
            }
            catch (Exception err)
            {
                string error = err.Message;
                Console.WriteLine("\nERROR TraerVentasPorUsuario  " + error);
                return listaVenta;
            }
        }
    }
}

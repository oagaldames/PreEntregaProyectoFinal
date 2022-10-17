using PreEntregaProyectoFinal.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreEntregaProyectoFinal.Metodos
{
    public class MetodosProducto
    {
        public static List<Producto> TraerProducto(int IdUsuario)
        {
            var listaProductos = new List<Producto>();
            try
            {
                DataSql db = new DataSql();
                            
                if (db.ConectarSQL())
                {
                    SqlCommand cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Producto WHERE IdUsuario=@IdUsu";

                    var paramIdUsu = new SqlParameter("IdUsu", SqlDbType.BigInt);
                    paramIdUsu.Value = IdUsuario;

                    cmd.Parameters.Add(paramIdUsu);
                    
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var producto = new Producto();
                        producto.Id = Convert.ToInt32(reader.GetValue(0));
                        producto.Descripciones = reader.GetValue(1).ToString();
                        producto.Costo = Convert.ToDouble(reader.GetValue(2));
                        producto.PrecioVenta = Convert.ToDouble(reader.GetValue(3));
                        producto.Stock = Convert.ToInt32(reader.GetValue(4));
                        listaProductos.Add(producto);

                    }
                    reader.Close();
                    db.DesconectarSQL();
                }
                return listaProductos;
            }
            catch (Exception err)
            {
                string error = err.Message;
                Console.WriteLine("\nERROR ConectarSQL  " + error);
                return listaProductos;
            }

        }
    }
}

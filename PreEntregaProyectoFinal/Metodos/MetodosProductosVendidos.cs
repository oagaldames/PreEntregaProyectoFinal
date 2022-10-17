using PreEntregaProyectoFinal.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PreEntregaProyectoFinal.Metodos
{
    public class MetodosProductosVendidos
    {
        public static List<ProductoVendido> TraerProductosVendidos(int IdUsuario)
        {
            var listaProductosVendidos = new List<ProductoVendido>();
            try
            { 
                List<Producto> listaProductosUsaurio = new List<Producto>();
                MetodosProducto metodosProducto = new MetodosProducto();
                listaProductosUsaurio = MetodosProducto.TraerProducto(IdUsuario);

                DataSql db = new DataSql();
                        
                if (db.ConectarSQL())
                {
                    SqlCommand cmd = db.Connection.CreateCommand();
                    var paramProdId = new SqlParameter("ProdId", SqlDbType.BigInt);
                    foreach (Producto productosUsuario in listaProductosUsaurio)
                    {
                        cmd.CommandText = "SELECT ProductoVendido.Id,ProductoVendido.Stock FROM ProductoVendido " +
                            "WHERE IdProducto=@ProdId"; 
                           
                        paramProdId.Value = productosUsuario.Id;
                        cmd.Parameters.Add(paramProdId);
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            var productoVendido = new ProductoVendido();
                            productoVendido.Id = Convert.ToInt32(reader.GetValue(0));
                            productoVendido.Stock = Convert.ToInt32(reader.GetValue(1));
                            listaProductosVendidos.Add(productoVendido);
                        }
                        reader.Close();
                        cmd.Parameters.Clear();
                    }
                
                    db.DesconectarSQL();
                }
                return listaProductosVendidos;
            }
            catch (Exception err)
            {
                string error = err.Message;
                Console.WriteLine("\nERROR TraerProductosVendidos  " + error);
                return listaProductosVendidos;
            }
        }

    }
}

using PreEntregaProyectoFinal.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PreEntregaProyectoFinal.Metodos
{
    public class MetodosProductosVendidos
    {
        public static List<Producto> TraerProductosVendidos(int IdUsuario)
        {
            var listaProductos = new List<Producto>();
            try
            { 
                int idProducto;
                List<Producto> listaProductosUsaurio = new List<Producto>();
                MetodosProducto metodosProducto = new MetodosProducto();
                listaProductosUsaurio = MetodosProducto.TraerProducto(IdUsuario);

                DataSql db = new DataSql();
                db.StrDatabase = Parametros.BaseDeDatos;
                db.StrServidor = Parametros.Servidor;
                
        
                if (db.ConectarSQL())
                {
                    SqlCommand cmd = db.Connection.CreateCommand();

                    foreach (Producto productosUsuario in listaProductosUsaurio)
                    {
                        cmd.CommandText = "SELECT ProductoVendido.Stock FROM ProductoVendido "+
                            "INNER JOIN Producto ON ProductoVendido.IdProducto = producto.Id "+
                            "WHERE IdProducto=" + productosUsuario.Id;
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            var producto = new Producto();
                            producto.Id = productosUsuario.Id;
                            producto.Descripciones = productosUsuario.Descripciones;
                            producto.Stock = Convert.ToInt32(reader.GetValue(0));
                            listaProductos.Add(producto);
                        }
                        reader.Close();
                    }
                
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

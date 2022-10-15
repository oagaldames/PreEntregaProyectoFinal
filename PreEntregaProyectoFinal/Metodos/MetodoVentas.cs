using PreEntregaProyectoFinal.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PreEntregaProyectoFinal.Metodos
{
    public class MetodosVentas
    {
        public static List<Producto> TraerVentasPorUsuario(int IdUsuario)
        {
            var listaProductos = new List<Producto>();
            try
            { 
                DataSql db = new DataSql();
                db.StrDatabase = Parametros.BaseDeDatos;
                db.StrServidor = Parametros.Servidor;
                

                if (db.ConectarSQL())
                {
                    SqlCommand cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "SELECT Producto.Descripciones," +
                        "ProductoVendido.Stock,(Producto.PrecioVenta * ProductoVendido.Stock) AS monto " +
                        "FROM ProductoVendido " +
                        "inner join Producto On ProductoVendido.IdProducto = Producto.id " +
                        "inner join Venta on ProductoVendido.IdVenta = Venta.Id " +
                        "WHERE Venta.IdUsuario = "+ IdUsuario;
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var producto = new Producto();
                        producto.Descripciones = reader.GetValue(0).ToString();
                        producto.Stock = Convert.ToInt32(reader.GetValue(1));
                        producto.Costo = Convert.ToDouble(reader.GetValue(2));
                    
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
                Console.WriteLine("\nERROR TraerVentasPorUsuario  " + error);
                return listaProductos;
            }
        }
    }
}

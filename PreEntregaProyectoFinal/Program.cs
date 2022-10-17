using PreEntregaProyectoFinal.Clases;
using PreEntregaProyectoFinal.Funciones;
using PreEntregaProyectoFinal.Metodos;
using System.Data.SqlClient;

namespace PreEntregaProyectoFinal
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("***PRE ENTREGA PROYECTO FINAL CODERHOUSE***");

            // Verifico conexion al SQL
            DataSql db = new DataSql();
            
            if (!db.ConectarSQL())
            {
                Console.WriteLine("\nERROR DE CONECCION CON EL SQL");
            }
            else
            {
                 // Prueba punto a Traer Usuario:  Recibe como parámetro un nombre del usuario,
                // buscarlo en la base de datos y devolver el objeto con todos sus datos 
                Console.WriteLine("\n***TEST PUNTO A ***\nINGRESE EL NOMBRE DEL USUARIO A BUSCAR");

                String nombreIngresado = Console.ReadLine();
                List<Usuario> listaUsuariosDevueltos = new List<Usuario>();
                MetodosUsuario metodoUsuario = new MetodosUsuario();

                listaUsuariosDevueltos = MetodosUsuario.TraerUsuarioPorNombre(nombreIngresado);


                if (listaUsuariosDevueltos.Count > 0)
                {
                    Console.WriteLine("DATOS  USUARIO  CON NOMBRE {0}", nombreIngresado);
                    foreach (Usuario usuariosDevueltos in listaUsuariosDevueltos)
                    {
                        Console.WriteLine("\nID: {0}\nNOMBRE: {1}\nAPELLIDO: {2}\nNOMBREUSUARIO:{3} " +
                            "\nCONTRASEÑA: {4}\nEMAIL: {5}"
                            , usuariosDevueltos.Id.ToString()
                            , usuariosDevueltos.Nombre.ToString()
                            ,usuariosDevueltos.Apellido.ToString()
                            , usuariosDevueltos.NombreUsuario.ToString()
                            ,usuariosDevueltos.Contraseña.ToString()
                            , usuariosDevueltos.Mail.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("EL USUARIO  CON NOMBRE {0} NO EXISTE", nombreIngresado);
                }
                // punto b.	Traer Producto: Recibe un número de IdUsuario como parámetro,
                // debe traer todos los productos cargados en la base de este usuario en particular

                Console.WriteLine("\n**** TEST PUNTO B **** \nINGRESE ID DEL USUARIO ");

                bool ingOK= int.TryParse(Console.ReadLine(), out int idIngresado);
                if (ingOK)
                {
                    List<Producto> listaProductosDevueltos = new List<Producto>();
                    MetodosProducto metodosProducto = new MetodosProducto();
                    listaProductosDevueltos = MetodosProducto.TraerProducto(idIngresado);
                    Console.WriteLine("\nPRODUCTOS CARGADOS POR USUARIO ID {0}", idIngresado);
                    foreach (Producto ProductosDevueltos in listaProductosDevueltos)
                    {

                        Console.WriteLine("\nID: {0} - DESCRIPCIONES: {1} - COSTO:{2} - PRECIOVENTA {3} - STOCK {4} "
                            , ProductosDevueltos.Id.ToString()
                            , ProductosDevueltos.Descripciones.ToUpper()
                            , ProductosDevueltos.Costo.ToString()
                            , ProductosDevueltos.PrecioVenta.ToString()
                            , ProductosDevueltos.Stock);
                    }
                }
                else
                {
                    Console.WriteLine("\nERROR INGRESO ID");
                }
                // punto c.	Traer Productos Vendidos: Traer Todos los productos vendidos de un Usuario,
                // cuya información está en su producto (Utilizar dentro de esta función el "Traer Productos"
                // anteriormente hecho para saber que productosVendidos ir a buscar).

                Console.WriteLine("\n**** TEST PUNTO C **** \nINGRESE ID DEL USUARIO ");
                ingOK = int.TryParse(Console.ReadLine(), out idIngresado);
                if (ingOK)
                {

                    List<ProductoVendido> listaProductosVendidos = new List<ProductoVendido>();
                    MetodosProducto metodosProductoVendidos = new MetodosProducto();
                    listaProductosVendidos = MetodosProductosVendidos.TraerProductosVendidos(idIngresado);
                    Console.WriteLine("\nPRODUCTOS VENDIDOS POR USUARIO ID {0}", idIngresado);

                    foreach (ProductoVendido productosvendidos in listaProductosVendidos)
                    {
                        Console.WriteLine("\nIDPRODUCTO: {0} - CANTIDAD VENDIDA: {1} "
                            , productosvendidos.Id.ToString()
                            , productosvendidos.Stock.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("\nERROR INGRESO ID");
                }
                // punto d. Traer Ventas: Recibe como parámetro un IdUsuario, debe traer todas las ventas de la base
                // asignados al usuario particular.

                Console.WriteLine("\n**** TEST PUNTO D **** \nINGRESE ID DEL USUARIO ");
                ingOK = int.TryParse(Console.ReadLine(), out idIngresado);
                if (ingOK)
                {

                    List<Producto> listaProductosVendidosPorUsuario = new List<Producto>();
                    MetodosVentas metodosProductoVendidosPorUsuario = new MetodosVentas();
                    listaProductosVendidosPorUsuario = MetodosVentas.TraerVentasPorUsuario(idIngresado);
                    Console.WriteLine("\nVENTAS POR USUARIO POR USUARIO ID {0}", idIngresado);
                    foreach (Producto ventasUsuario in listaProductosVendidosPorUsuario)
                    {
                        Console.WriteLine("\n DESCRIPCION: {0} - CANTIDAD VENDIDA: {1} - MONTO VENDIDO {2}"
                            , ventasUsuario.Descripciones.ToUpper()
                            , ventasUsuario.Stock.ToString()
                            , ventasUsuario.Costo.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("\nERROR INGRESO ID");
                }
                // punto e.	Inicio de sesión: Se le pase como parámetro el nombre del usuario y la contraseña,
                // buscar en la base de datos si el usuario existe y si coincide con la contraseña lo devuelve
                // (el objeto Usuario), caso contrario devuelve uno vacío (Con sus datos vacíos y el id en 0). 

                Console.WriteLine("\n**** TEST PUNTO E **** \nINGRESE EL USUARIO ");
                string userIngresado = Console.ReadLine().ToString();

                Console.WriteLine("\nINGRESE LA CONTRASEÑA ");
                string PassIngresado = Console.ReadLine().ToString();

                List<Usuario> listaUsuariologin = new List<Usuario>();
                MetodosUsuario metodoUsuarioLogin = new MetodosUsuario();
                listaUsuariologin = MetodosUsuario.LoginUsuario(userIngresado, PassIngresado);

                int idUsusario = Convert.ToInt32(listaUsuariologin.ElementAt(0).Id);
                if (idUsusario == 0)
                {
                    Console.WriteLine("\n USUARIO o CONTRASEÑA INCORRECTA !!");
                }
                else
                {
                    Console.WriteLine("\nUSUARIO CORRECTO !");
                }

                Console.WriteLine("\nID: {0}\nNOMBRE: {1}\nAPELLIDO: {2}\nNOMBREUSUARIO: {3} " +
                            "\nCONTRASEÑA: {4}\nEMAIL: {5}"
                            , listaUsuariologin.ElementAt(0).Id.ToString()
                            , listaUsuariologin.ElementAt(0).Nombre.ToString()
                            ,listaUsuariologin.ElementAt(0).Apellido.ToString()
                            , listaUsuariologin.ElementAt(0).NombreUsuario.ToString()
                            , listaUsuariologin.ElementAt(0).Contraseña.ToString()
                            , listaUsuariologin.ElementAt(0).Mail.ToString());
            }
        }
           
    }
}

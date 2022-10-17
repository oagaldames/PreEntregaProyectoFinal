using PreEntregaProyectoFinal.Clases;
using System.Data;
using System.Data.SqlClient;

namespace PreEntregaProyectoFinal.Funciones
{
    public class MetodosUsuario
    {                
        public static List<Usuario> TraerUsuarioPorNombre(string Nombre)
        {
            var listaUsuarios = new List<Usuario>();
            try
            { 
                DataSql db = new DataSql();
                //db.StrDatabase= Parametros.BaseDeDatos;
                //db.StrServidor= Parametros.Servidor;
                
                if (db.ConectarSQL())
                {
                    SqlCommand cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Usuario WHERE Nombre=@NomUsu";
                    var paramNomUsu = new SqlParameter("NomUsu", SqlDbType.VarChar);
                    paramNomUsu.Value = Nombre;
                    cmd.Parameters.Add(paramNomUsu);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader.GetValue(0));
                        usuario.Nombre = reader.GetValue(1).ToString();
                        usuario.Apellido = reader.GetValue(2).ToString();
                        usuario.NombreUsuario = reader.GetValue(3).ToString();
                        usuario.Contraseña = reader.GetValue(4).ToString();
                        usuario.Mail = reader.GetValue(5).ToString();
                        listaUsuarios.Add(usuario);
                     }

                    db.DesconectarSQL();
                }
                return listaUsuarios;
            }
            catch (Exception err)
            {
                string error = err.Message;
                Console.WriteLine("\nERROR TraerUsuarioPorNombre  " + error);
                return listaUsuarios;
            }
        }

        public static List<Usuario> LoginUsuario(string NombreUsuario, string Pass)
        {
            var listaUsuarios = new List<Usuario>();
            try
            { 
                DataSql db = new DataSql();
                               
                if (db.ConectarSQL())
                {
                    SqlCommand cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Usuario WHERE NombreUsuario=@NomUsu AND Contraseña=@passUsu";

                    var paramNomUsu = new SqlParameter("NomUsu", SqlDbType.VarChar);
                    paramNomUsu.Value = NombreUsuario;

                    var paramPass = new SqlParameter("passUsu", SqlDbType.VarChar);
                    paramPass.Value = Pass;

                    cmd.Parameters.Add(paramNomUsu);
                    cmd.Parameters.Add(paramPass);

                    var reader = cmd.ExecuteReader();
                    var usuario = new Usuario();
                    if (reader.HasRows)
                    {                  
                        while (reader.Read())
                        {
                            usuario.Id = Convert.ToInt32(reader.GetValue(0));
                            usuario.Nombre = reader.GetValue(1).ToString();
                            usuario.Apellido = reader.GetValue(2).ToString();
                            usuario.Contraseña = reader.GetValue(3).ToString();
                            usuario.Mail = reader.GetValue(4).ToString();                        
                        }
                    }
                    else
                    {
                        usuario.Id = 0;
                        usuario.Nombre = String.Empty;
                        usuario.Apellido = String.Empty;
                        usuario.Contraseña = String.Empty;
                        usuario.Mail = String.Empty;

                    }
                    listaUsuarios.Add(usuario);
                    db.DesconectarSQL();
                }
                return listaUsuarios;
            }
            catch (Exception err)
            {
                string error = err.Message;
                Console.WriteLine("\nERROR LoginUsuario  " + error);
                return listaUsuarios;
            }
        }

    }  
}
        
    

    
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLogic
{
    public class DBCommun
    {
        private static string cadenaConexionLocal = "Data Source=.;Initial Catalog=PoetaMuerto; Integrated Security=true";
        private static string cadenaConexionHosting = "Data Source=SQL5102.site4now.net;Initial Catalog=DB_A6EEFE_PoetaMuerto;User Id=DB_A6EEFE_PoetaMuerto_admin;Password=Cardona07Aguilar";
        public static SqlConnection ConexionSQL()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexionHosting); //creamos una conexion
            conexion.Open();//abrimos la conexion
            return conexion;
        }
        public static SqlCommand crearCommand(string query, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(query, connection);//creamos el sqlcommand
            command.CommandType = CommandType.Text;//y le decimos que sera una consulta la que enviaremos a la base de datos
            return command;
        }
    }
}

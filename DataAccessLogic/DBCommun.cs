using System.Data.SqlClient;

namespace DataAccessLogic
{
    public class DBCommun
    {
        private static string cadenaConexion = "Data Source=.;Initial Catalog=PoetaMuerto; Integrated Security=true";
        public static SqlConnection ConexionSQL()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}

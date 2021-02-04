using System.Data.SqlClient;

namespace DataAccessLogic
{
    public class DBCommun
    {
        private static string cadenaConexionLocal = "Data Source=.;Initial Catalog=PoetaMuerto; Integrated Security=true";
        private static string cadenaConexionHosting = "Data Source=SQL5102.site4now.net;Initial Catalog=DB_A6EEFE_PoetaMuerto;User Id=DB_A6EEFE_PoetaMuerto_admin;Password=Cardona07Aguilar";
        public static SqlConnection ConexionSQL()
        {
            return new SqlConnection(cadenaConexionHosting);
        }
    }
}

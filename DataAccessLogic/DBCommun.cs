using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLogic
{
    public class DBCommun
    {
        #region crea y abre nuestra conexion a la base de datos
        private static string cadenaConexionLocal = "Data Source=.;Initial Catalog=PoetaMuerto; user=sa; password=triz7+10";
        private static string cadenaConexionHosting = "Data Source=SQL5102.site4now.net;Initial Catalog=DB_A6EEFE_PoetaMuerto;User Id=DB_A6EEFE_PoetaMuerto_admin;Password=Cardona07Aguilar";
        public static SqlConnection ConectarSQL()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexionHosting); //creamos la conexion
            conexion.Open();//abrimos la conexion
            return conexion;
        }
        #endregion

        #region crea nuestro SqlCommand y define si es un Text o un StoredProcedure
        public static SqlCommand crearCommand(string query, SqlConnection connection, bool itsStoredProcedure=false)
        {
            SqlCommand command = new SqlCommand(query, connection);//creamos el sqlcommand
            if (itsStoredProcedure)// si viene true
                command.CommandType = CommandType.StoredProcedure;//y le decimos que sera un procedimiento almacenado
            else//si es false
                command.CommandType = CommandType.Text;//le decimos que sera una consulta la que enviaremos a la base de datos
            return command;
        }
        #endregion

        #region crea los parametros a enviar en los stored procedure
        public static SqlCommand crearParameters(SqlCommand command, object[,] arreglo)
        {
            int tamanoArray = arreglo.Length / 2;//obtenemos el tamaño del arreglo
            for (var i = 0; i < tamanoArray; i++)//recorremos los parametros que vienen en el arreglo
            {
                object valorPropiedad = null, parametroSQL = null;
                for (int j = 0; j < 2; j++)
                {
                    if (j == 1) { //si es 1 es el valor de la entidad
                        valorPropiedad = arreglo[i, j]; 
                    }else { //si es 0 es el parametro que se enviara a SQL
                        parametroSQL = arreglo[i, j]; 
                    }   
                }
                command.Parameters.AddWithValue(parametroSQL.ToString(), valorPropiedad);
            }
            return command;
        }
        #endregion
    }
}

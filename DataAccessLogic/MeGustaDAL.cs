using BussinesEntity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLogic
{
    public class MeGustaDAL
    {

        public string contarLike(Int64 Idpoema)
        {
            try
            {
                using(var con = DBCommun.ConexionSQL())
                {
                    string consulta = "select * from MeGusta where Idpoema={0}";
                    string query = string.Format(consulta, Idpoema);
                    SqlCommand command = DBCommun.crearCommand(query, con);
                    IDataReader reader = command.ExecuteReader();
                    Int64 contador = 0;
                    while (reader.Read())
                    {
                        contador++;
                    }
                    return contador.ToString();
                }
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public int verificarFuente(Int64 Idpoema, string Ipcliente)
        {
            using (var con = DBCommun.ConexionSQL())
            {
                string consulta = "select * from MeGusta where Idpoema={0} and Ipcliente='{1}'";
                string query = string.Format(consulta, Idpoema, Ipcliente);
                SqlCommand command = DBCommun.crearCommand(query, con);
                IDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    return 1;
                else
                    return 0;
            }
        }
        public string darLike(MeGusta meGusta)
        {
            try
            {
                string query = "";
                using (var con = DBCommun.ConexionSQL())
                {
                    if (verificarFuente(meGusta.Idpoema, meGusta.Ipcliente) == 0) 
                    {
                        string consulta = "insert into MeGusta(Idpoema, Ipcliente)values({0},'{1}')";
                        query = string.Format(consulta, meGusta.Idpoema, meGusta.Ipcliente);
                    }
                    else
                    {
                        string consulta = "delete from MeGusta where Idpoema={0} and Ipcliente='{1}'";
                        query = string.Format(consulta, meGusta.Idpoema, meGusta.Ipcliente);
                    }
                    var command = DBCommun.crearCommand(query, con);
                    return command.ExecuteNonQuery().ToString();
                }
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}

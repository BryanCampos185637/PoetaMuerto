using BussinesEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLogic
{
    public class MeGustaDAL
    {
        //ver los like
        public List<MeGusta> listar()
        {
            List<MeGusta> lst = new List<MeGusta>();
            using(var con = DBCommun.ConexionSQL())
            {
                var query = "select * from MeGusta";
                var command = DBCommun.crearCommand(query, con);
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lst.Add(new MeGusta(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2)));
                }
            }
            return lst;
        }
        //metodo para poder contar los like
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
                    while (reader.Read())//recorremos la lista
                    {
                        contador++;//cada vez que pase se aumenta el contador
                    }
                    return contador.ToString();
                }
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        //metodo que verifica que el mismo cliente no de mas de un like
        public int verificarFuente(Int64 Idpoema, string Ipcliente)
        {
            using (var con = DBCommun.ConexionSQL())
            {
                string consulta = "select * from MeGusta where Idpoema={0} and Ipcliente='{1}'";
                string query = string.Format(consulta, Idpoema, Ipcliente);
                SqlCommand command = DBCommun.crearCommand(query, con);
                IDataReader reader = command.ExecuteReader();
                if (reader.Read())//si encuentra un resultado retornamos 1
                    return 1;
                else//de lo contrario retornamos 0
                    return 0;
            }
        }
        //metodo que nos sirve para dar like
        public string darLike(MeGusta meGusta)
        {
            try
            {
                string query = "";
                using (var con = DBCommun.ConexionSQL())
                {
                    //validamos si el cliente ya a dado like
                    if (verificarFuente(meGusta.Idpoema, meGusta.Ipcliente) == 0) 
                    {
                        //si es 0 entonces se agrega el like
                        string consulta = "insert into MeGusta(Idpoema, Ipcliente)values({0},'{1}')";
                        query = string.Format(consulta, meGusta.Idpoema, meGusta.Ipcliente);
                    }
                    else
                    {
                        //si no se elimina el like
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

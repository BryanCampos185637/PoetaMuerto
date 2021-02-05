using BussinesEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLogic
{
    public class PoemaDAL
    {
        string consulta = "", formatoConsulta = "";
        SqlCommand command;
        List<Poema> lst= new List<Poema>();
        public int guardar(Poema poema)
        {
            try
            {
                using (var con = DBCommun.ConexionSQL())
                {
                    if (poema.Idpoema == 0)
                    {
                        consulta = "insert into Poema(Titulo,Verso,Imagen,Bhabilitado)values('{0}','{1}','{2}','{3}')";
                        formatoConsulta = string.Format(consulta, poema.Titulo, poema.Verso, poema.Imagen, poema.Bhabilitado);
                    }
                    else
                    {
                        consulta = "update Poema set Titulo='{0}',Verso='{1}',Imagen='{2}' where Idpoema={3}";
                        formatoConsulta = string.Format(consulta, poema.Titulo, poema.Verso, poema.Imagen, poema.Idpoema);
                    }
                    command = DBCommun.crearCommand(formatoConsulta, con);
                    return command.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        public List<Poema> lstPoema()
        {
            try
            {
                using (var con = DBCommun.ConexionSQL())
                {
                    consulta = "select * from Poema where Bhabilitado='A'";
                    command = DBCommun.crearCommand(consulta, con);
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        lst.Add(new Poema(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                    }
                    return lst;
                }
            }
            catch(Exception e)
            {
                string d= e.Message;
                return null;
            }
        }
        public Poema ObtenerPorId(Int64 id)
        {
            try
            {
                Poema poema = new Poema();
                using (var con = DBCommun.ConexionSQL())
                {
                    consulta = "select * from Poema where Idpoema={0}";
                    formatoConsulta = string.Format(consulta, id);
                    command = DBCommun.crearCommand(formatoConsulta, con);
                    IDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        poema.Idpoema = reader.GetInt64(0);
                        poema.Titulo = reader.GetString(1);
                        poema.Verso = reader.GetString(2);
                        poema.Imagen = reader.GetString(3);
                        poema.Bhabilitado = reader.GetString(4);
                    }
                    return poema;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public int eliminar(Int64 id)
        {
            try
            {
                using (var con = DBCommun.ConexionSQL())
                {
                    consulta = "update Poema set Bhabilitado='D' where Idpoema={0}";
                    formatoConsulta = string.Format(consulta, id);
                    command = DBCommun.crearCommand(formatoConsulta, con);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<Poema> PoemasPopulares()
        {
            try
            {
                MeGustaDAL meGustaDAL = new MeGustaDAL();
                using (var con = DBCommun.ConexionSQL())
                {
                    consulta = "select * from Poema where Bhabilitado='A'";
                    command = DBCommun.crearCommand(consulta, con);
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = reader.GetInt64(0);
                        if (Convert.ToInt32(meGustaDAL.contarLike(id)) >= 3)
                        {
                            lst.Add(new Poema(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                        }
                    }
                    return lst;
                }
            }
            catch (Exception e)
            {
                string d = e.Message;
                return null;
            }
        }
    }
}

using BussinesEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

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
                using (var con = DBCommun.ConectarSQL())
                {
                    //si el id viene en 0 es un agregar
                    if (poema.Idpoema == 0)
                    {
                        consulta = "insert into Poema(Titulo,Verso,Imagen,Bhabilitado)values('{0}','{1}','{2}','{3}')";
                        formatoConsulta = string.Format(consulta, poema.Titulo, poema.Verso, poema.Imagen, poema.Bhabilitado);
                        command = DBCommun.crearCommand(formatoConsulta, con, false);
                    }
                    else//si no es un actualizar
                    {
                        consulta = "ActualizarPoema";
                        command = DBCommun.crearCommand(consulta, con, true);
                        object[,] array = {
                            { "Idpoema", poema.Idpoema},
                            { "Titulo",poema.Titulo },
                            { "Verso", poema.Verso},
                            { "Imagen", poema.Imagen}
                        };
                        command = DBCommun.crearParameters(command, array);
                    }
                    return command.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        //lista todos los poemas que esten habilitados
        public List<Poema> lstPoema()
        {
            Poema poema;
            try
            {
                using (var con = DBCommun.ConectarSQL())
                {
                    consulta = "ListarPoemas";
                    command = DBCommun.crearCommand(consulta, con, true);
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        poema = new Poema();
                        poema.Idpoema = reader.GetInt64(0);
                        poema.Titulo = reader.GetString(1);
                        poema.Verso = reader.GetString(2);
                        poema.Imagen = reader.GetString(3)==null || reader.GetString(3).Length <= 100 ? "/Content/img/J.png" : reader.GetString(3);
                        lst.Add(poema);
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
        //obtiene un poema en especifico
        public Poema ObtenerPorId(Int64 id)
        {
            try
            {
                Poema poema = new Poema();
                using (var con = DBCommun.ConectarSQL())
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
                        poema.Imagen = reader.GetString(3) == null || reader.GetString(3).Length <= 100 ? "/Content/img/J.png" : reader.GetString(3);
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
        //metodo para eliminar poemas segun el id
        public int eliminar(Int64 id)
        {
            try
            {
                using (var con = DBCommun.ConectarSQL())
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
        //lista todos aquellos poemas que tengan mas de 9 like
        public List<Poema> PoemasPopulares()
        {
            try
            {
                MeGustaDAL meGustaDAL = new MeGustaDAL();
                using (var con = DBCommun.ConectarSQL())
                {
                    consulta = "select * from Poema where Bhabilitado='A'";
                    command = DBCommun.crearCommand(consulta, con);
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())//recorremos la lista
                    {
                        //verificamos cuantos like tiene el poema actual
                        if (Convert.ToInt32(meGustaDAL.contarLike(reader.GetInt64(0))) >= 10)//si es mayor o igual a 10 entonces lo agregamos
                        {
                            lst.Add(new Poema(reader.GetInt64(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3).Length <= 100 ? "Content/img/J.png" : reader.GetString(3)
                                ));
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
        //filtra los poemas 
        public List<Poema> FiltrarPoemas(Poema poema)
        {
            try
            {
                using (var con = DBCommun.ConectarSQL())
                {
                    consulta = "FiltrarPoemas";
                    command = DBCommun.crearCommand(consulta, con, true);
                    object[,] parametros = { { "Titulo", poema.Titulo } };
                    command = DBCommun.crearParameters(command,parametros);
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        lst.Add(new Poema(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
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

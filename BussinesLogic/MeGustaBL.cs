using System;
using System.Collections.Generic;
using BussinesEntity;
using DataAccessLogic;

namespace BussinesLogic
{
    public class MeGustaBL
    {
        MeGustaDAL dal = new MeGustaDAL();
        public string ContarMeGusta(Int64 Idpoema)
        {
            return dal.contarLike(Idpoema);
        }
        public string darMegusta(MeGusta meGusta)
        {
            var resp = dal.darLike(meGusta);
            if (Convert.ToInt32(resp) > 0)
                return "ok";
            else
                return resp;
        }
        public List<MeGusta> listar()
        {
            return dal.listar();
        }
    }
}

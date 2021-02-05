using DataAccessLogic;
using BussinesEntity;
using System.Collections.Generic;
using System;

namespace BussinesLogic
{
    public class PoemaBL
    {
        PoemaDAL dal = new PoemaDAL();
        public string guardar(Poema poema)
        {
            int result = dal.guardar(poema);
            if (result > 0)
                return "ok";
            else
                return "error";
        }
        public List<Poema> lstPoema()
        {
            return dal.lstPoema();
        }
        public List<Poema> poemasPopulares()
        {
            return dal.PoemasPopulares();
        }
        public string eliminar(Int64 id)
        {
            int result = dal.eliminar(id);
            if (result > 0)
                return "ok";
            else
                return "error";
        }
        public Poema obtenerPorId(Int64 id)
        {
            return dal.ObtenerPorId(id);
        }
    }
}

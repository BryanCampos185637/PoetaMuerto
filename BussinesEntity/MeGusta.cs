using System;

namespace BussinesEntity
{
    public class MeGusta
    {
        public MeGusta()
        {

        }
        public MeGusta(Int64 id, Int64 poema, string ip)
        {
            Idmegusta = id;
            Idpoema = poema;
            Ipcliente = ip;
        }

        public Int64 Idmegusta { get; set; }
        public Int64 Idpoema { get; set; }
        public string Ipcliente { get; set; }
    }
}

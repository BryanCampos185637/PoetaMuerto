using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;
namespace UI.Cliente.Models
{
    //aqui llamamos la configuracion que dejamos en el web.config
    public class ConfiguracionPayPal
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;
        static ConfiguracionPayPal()
        {
            var configuracion = ObtenerConfiguracion();
            ClientId = configuracion["clientId"];
            ClientSecret = configuracion["clientSecret"];
        }
        public static Dictionary<string, string> ObtenerConfiguracion()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }
        //crear token de acceso
        private static string obtenerTokenDeAcceso()
        {
            var tokenAcceso = new OAuthTokenCredential(ClientId, ClientSecret, ObtenerConfiguracion()).GetAccessToken();
            return tokenAcceso;
        }
        public static APIContext ObtenerAPIContext()
        {
            APIContext aPIContext = new APIContext(obtenerTokenDeAcceso());
            aPIContext.Config = ObtenerConfiguracion();
            return aPIContext;
        }
    }
}
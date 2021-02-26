using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BussinesEntity;
using BussinesLogic;
using PayPal.Api;
using UI.Cliente.Models;

namespace UI.Cliente.Controllers
{
    public class PoemaController : Controller
    {
        PoemaBL bl = new PoemaBL();
        // GET: Poema
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Failure()
        {
            return View();
        }
        [HttpGet]
        public JsonResult poemas()
        {
            try
            {
                var json = Json(bl.lstPoema().OrderByDescending(p => p.Idpoema).ToList(), JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;
            }
            catch(Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public string addlike(MeGusta meGusta)
        {
            MeGustaBL bl = new MeGustaBL();
            meGusta.Ipcliente = Request.UserHostAddress;//capturamos la ip del cliente
            return bl.darMegusta(meGusta);
        }

        [HttpGet]
        public JsonResult contarLike(Int64 Idpoema)
        {
            MeGustaBL bl = new MeGustaBL();
            return Json(bl.ContarMeGusta(Idpoema),JsonRequestBehavior.AllowGet);
        }

        private Payment pagar;

        //crear pago utilizando la ApiCOntext
        private Payment CrearPayment(APIContext aPIContext, string redirectUrl, string cantidad)
        {
            //creamos la lista del pedido
            var listItem = new ItemList() { items = new List<Item>() };
            listItem.items.Add(new Item()
            {
                name = "Ayuda a Josue Cardona",
                currency = "USD",
                price = cantidad,
                quantity = "1",
                sku = "sku"
            });
            var payer = new Payer() { payment_method="paypal" };
            //hacer la redireccion
            var redirect = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };
            //crear el detalle del objeto
            var detalle = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = cantidad
            };
            //crear amount
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(detalle.tax) + Convert.ToDouble(detalle.shipping) + Convert.ToDouble(detalle.subtotal)).ToString(),
                details = detalle
            };
            //creamos la transaccion
            var transaccionList = new List<Transaction>();
            transaccionList.Add(new Transaction()
            {
                description = "Chien testing transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItem
            });
            pagar = new Payment()
            {
                intent="sale",
                payer=payer,
                transactions= transaccionList,
                redirect_urls=redirect
            };
            return pagar.Create(aPIContext);
        }
        //ejecutar pago
        private Payment ejecutarPago(APIContext aPIContext,string payerId, string paymentId)
        {
            var ejecutaPago = new PaymentExecution()
            {
                payer_id=payerId,
            };
            pagar = new Payment()
            {
                id = paymentId
            };
            return pagar.Execute(aPIContext, ejecutaPago);
        }
        //crear metodo de pago con paypal
        public ActionResult pagarConPaypal(decimal? dinero)
        {
            //obtenemos el contexto del clienteId y clienteSecret
            APIContext context = ConfiguracionPayPal.ObtenerAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //creamos un comprador
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "Poema/pagarConPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createPayment = CrearPayment(context, baseURI + "guid=" + guid, "0.25");
                    //obtener los link de retorno de paypal al crear la funcion de llamda
                    var links = createPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;
                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }
                    Session.Add(guid, createPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    //
                    var guid = Request.Params["guid"];
                    var ejecutar = ejecutarPago(context, payerId, Session[guid] as string);
                    if (ejecutar.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                }
            }
            catch(Exception e)
            {
                PayPalLogger.log("Error: " + e.Message);
                return View("Failure");
            }
            return View("Index");
        }
    }
}
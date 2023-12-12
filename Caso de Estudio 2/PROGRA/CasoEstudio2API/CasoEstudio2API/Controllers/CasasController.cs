using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;

namespace CasoEstudio2API.Controllers
{
    public class CasasController : ApiController
    {
        [HttpGet]
        [Route("ConsultarCasas")]
        public List<CasasSistema> ConsultarCasas()
        {
            try
            {
                using (var con = new CasoEstudioMNEntities())
                {

                    con.Configuration.LazyLoadingEnabled = false;


                    var data = (from c in con.CasasSistema
                                where c.PrecioCasa >= 115000 && c.PrecioCasa <= 180000
                                orderby c.UsuarioAlquiler ascending
                                select c).ToList();

                    return data;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("LitemPrincipal")]
        public List<System.Web.Mvc.SelectListItem> LitemPrincipal()
        {
            try
            {
                using (var con = new CasoEstudioMNEntities())
                {
                    var casas = (from c in con.CasasSistema
                                       where c.UsuarioAlquiler == null
                                       select c).ToList();

                    List<System.Web.Mvc.SelectListItem> listaPrincipales = new List<System.Web.Mvc.SelectListItem>();
                    var result = new List<System.Web.Mvc.SelectListItem>();

                    foreach (var casa in casas)
                    {
                        result.Add(new System.Web.Mvc.SelectListItem
                        {
                            Value = casa.IdCasa.ToString(),
                            Text = casa.DescripcionCasa.ToString(),
                        });
                    }
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("SaldoAnterior")]
        public CasasSistema SaldoAnterior(long q)
        {
            try
            {
                using (var con = new CasoEstudioMNEntities())
                {

                    con.Configuration.LazyLoadingEnabled = false;

                    var data = (from c in con.CasasSistema
                                where c.IdCasa == q
                                select c).FirstOrDefault();

                    return data;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPut]
        [Route("ActualizarCasas")]
        public int ActualizarCasas(CasasSistema casa)
        {
            using (var context = new CasoEstudioMNEntities())
            {
                var datos = context.CasasSistema.Where(x => x.IdCasa == casa.IdCasa).FirstOrDefault();

                if (datos != null)
                {
                    datos.UsuarioAlquiler = casa.UsuarioAlquiler;
                    datos.FechaAlquiler = DateTime.Now;

                    
                        context.SaveChanges();
                        return 1;
                  
                }
                else
                {
                    Console.WriteLine("No se encontró la casa con el ID proporcionado.");
                    return 0; 
                }
            }
        }


    }
}

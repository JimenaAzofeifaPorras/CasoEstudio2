using CasoEstudio2.Entities;
using Newtonsoft.Json;
using Raven.Abstractions.Connection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

public class CasasModel
    {
        public string urlApi = ConfigurationManager.AppSettings["urlApi"];

        public List<CasaEnt> ConsultarCasas()
        {
            using (var client = new HttpClient())
            {
                var url = urlApi + "ConsultarCasas";

                var response = client.GetAsync(url).Result;
                var content = response.Content.ReadAsStringAsync().Result;

                // Deserializar la cadena JSON a tu tipo deseado
                var result = JsonConvert.DeserializeObject<List<CasaEnt>>(content);

                return result;
            }
        }


        public List<SelectListItem> LitemPrincipal()
        {
            using (var client = new HttpClient())
            {
                var url = urlApi + "/LitemPrincipal";

                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<SelectListItem>>(content);
                }
                else
                {
                    // Manejar el caso en que la respuesta no fue exitosa
                    Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                    return new List<SelectListItem>(); // o lanzar una excepción, según tus necesidades
                }
            }
        }


        public CasaEnt ConsultaPrecio(long q)
        {
            using (var client = new HttpClient())
            {
                var url = urlApi + "SaldoAnterior?q=" + q;

                var response = client.GetAsync(url).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<CasaEnt>(content);
            
            }
        }


        public int ActualizarCasas(CasaEnt casa)
        {
            using (var client = new HttpClient())
            {
                string url = urlApi + "/ActualizarCasas";
                var jsonData = System.Net.Http.Json.JsonContent.Create(casa);
                var response = client.PutAsync(url, jsonData).Result;

                return response.Content.ReadFromJsonAsync<int>().Result;
            }
        }










}
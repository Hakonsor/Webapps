using BLL;
using Nips.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Nips.Controllers
{
    public class FAQController : ApiController
    {
       
        // GET: FAQ
        private SpørsmålBLL db = new SpørsmålBLL();
        // GET: FAQ
        public HttpResponseMessage Get()
        {
            List<Spørsmål> alleSpørsmål = db.getList();
            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(alleSpørsmål);
            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        public HttpResponseMessage Get(bool meh)
        {
            List<Spørsmål> alleSpørsmål = db.getList();
            System.Diagnostics.Debug.WriteLine("Yup");
            foreach (Spørsmål s in alleSpørsmål)
            {
                if(s.Svar == null)
                {
                    alleSpørsmål.Remove(s);
                }
            }
            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(alleSpørsmål);
            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        public HttpResponseMessage Get(int id)
        {
            Spørsmål alleSpørsmål = db.getSpørsmål(id);
            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(alleSpørsmål);
            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        public HttpResponseMessage Post(Spørsmål spørsmål)
        {
            System.Diagnostics.Debug.WriteLine("invalid?dd");
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("invalid?");
                bool OK = db.lagreSpørsmål(spørsmål);
                if (OK)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                }
            }
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(" klarte ikke å lagre dette spørsmålet i databasen")
            };
        }

        public HttpResponseMessage Put(int id, Spørsmål spørsmål)
        {
            if (ModelState.IsValid)
            {
                bool OK = db.putSpørsmål(id, spørsmål);
                if (OK)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                }
            }
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Fant ikke dette spørsmålet i databasen")
            };
        }

        public HttpResponseMessage Delete(int id)
        {
            bool OK = db.deleteSpørsmål(id);
            if (OK)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                };
            }
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Fant ikke dette spørsmålet i databasen")
            };
        }


    }
}

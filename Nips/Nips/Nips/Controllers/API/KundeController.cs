using System;
using Nips.BLL;
using Nips.Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nips.Controllers.API
{
    public class KundeController : ApiController
    {
        /// <summary> kapish?
        /// Vi bruker denne Mvc WebApi 2.0 for å benytte oss av ajax.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/kundet/search/{query}")]
        public List<Kunde> Search(string query)
        {
            var repo = new KundeBLL();

            var result =
                repo.getKunde().Where((Kunde => Kunde.fornavn.ToLower().Contains(query.ToLower()))).ToList();
            if (result.Count == 0)
                result = repo.getKunde().Where(Kunde => Kunde.etternavn.ToLower().Contains(query.ToLower())).ToList();
            return result;

        }
    }
}
    


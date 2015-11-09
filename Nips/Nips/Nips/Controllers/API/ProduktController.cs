using Nips.BLL;
using Nips.Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Nips.Controllers.API
{
    public class ProduktController : ApiController
    {

        /// <summary>
        /// Vi bruker denne Mvc WebApi 2.0 for å benytte oss av ajax.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/produkt/search/{query}")]
        public List<Produkt> Search(string query)
        {
            var repo = new ProduktBLL();

            var result =
                repo.alleVare().Where((produkt => produkt.navn.ToLower().Contains(query.ToLower()))).ToList();
            if (result.Count == 0)
                result = repo.alleVare().Where(produkt => produkt.beskrivelse.ToLower().Contains(query.ToLower())).ToList();
            return result;

        }
    }
}
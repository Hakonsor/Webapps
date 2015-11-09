using BLL;
using Nips.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Nips.Controllers.API
{
    public class FAQController : ApiController
    {

        SpørsmålBLL db = new SpørsmålBLL();
        // GET: FAQ
        public List<Spørsmål> Get()
        {
            return db.getList();
        }

        public Spørsmål Get(int id)
        {
            return db.getSpørsmål(id);
        }

        public bool Post(Spørsmål spørsmål)
        {
            return db.lagreSpørsmål(spørsmål);
        }

        public bool Put(int id, Spørsmål spørsmål)
        {
            return db.putSpørsmål(id, spørsmål);
        }

        public bool Delete(int id)
        {
            return db.deteleSpørsmål(id);
        }

    }
}
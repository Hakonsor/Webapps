using Nips.DAL.Repositories;
using Nips.Model.Entities;
using System.Collections.Generic;
using System;
using BLL.Test;

namespace Nips.BLL
{
    public class ProduktBLL : IProduktLogikk
    {
        private IProduktRepository _repository;

        public ProduktBLL()
        {
            
            _repository = new ProduktRepository();
        }
        public ProduktBLL(IProduktRepository stub)
        {
            
            _repository = stub;
        }
        public List<Produkt> alleVare()
        {
           return _repository.alleVare();
        }
        public int lagreVare(Produkt innVare)
        {
            return _repository.lagreVare(innVare);
        }
        public bool endreVare(int id, Produkt innVare)
        {
            
            return _repository.endreVare(id, innVare);
        }
        public bool slettVare(int slettId)
        {
            
            return _repository.slettVare(slettId);
        }
        public Produkt hentEnVare(int id)
        {
            
            return _repository.hentEnVare(id);
        }

        public void writeToFile(Exception e)
        {
            throw new NotImplementedException();
        }
    }
}

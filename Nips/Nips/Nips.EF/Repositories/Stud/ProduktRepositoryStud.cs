using Nips.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nips.Model.Entities;

namespace Nips.EF.Repositories.Stud
{
    public class ProduktRepositoryStud : IProduktRepository
    {
        public List<Produkt> alleVare()
        {
            var liste = new List<Produkt>();
            
            var produkt = new Produkt()
            {
                id = 1,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            liste.Add(produkt);
            produkt.id = 2;
            liste.Add(produkt);
            produkt.id = 3;
            liste.Add(produkt);
            produkt.id = 4;
            liste.Add(produkt);
            return liste;
        }

        public bool endreVare(int id, Produkt innVare)
        {
            if (id == innVare.id) return true;
            return false;
        }

        public Produkt hentEnVare(int id)
        {
            var produkt = new Produkt()
            {
                id = 1,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            if (id == 1) return produkt;
            return null;
        }

        public int lagreVare(Produkt innVare)
        {
            if (innVare.id == 0) return -1;
            return 1;
        }

        public bool slettVare(int slettId)
        {
            if (slettId == 1) return true;
            return false;
        }

        public void writeToFile(Exception e)
        {
            throw new NotImplementedException();
        }
    }
}

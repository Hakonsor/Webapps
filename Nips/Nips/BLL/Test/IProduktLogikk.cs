using Nips.Model.Entities;
using System;
using System.Collections.Generic;


namespace BLL.Test
{
   public interface IProduktLogikk
    {
        List<Produkt> alleVare();
        int lagreVare(Produkt innVare);
        bool endreVare(int id, Produkt innVare);
        bool slettVare(int slettId);
        Produkt hentEnVare(int id);
        //Skrive til fil
        void writeToFile(Exception e);
    }
}

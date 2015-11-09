using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nips.Model.Entities
{
    public class Handlevogn
    {
        public int userID;
        public List<HandlevognVare> handlevognVare
        {
            get; set;
        }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public int sum { get; set; }
        public Handlevogn(int id)
        {
            userID = id;
            handlevognVare = new List<HandlevognVare>();
            sum = 0;
        }
    }

    public class HandlevognVare
    {
        public Produkt produkt;
        public int antall { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int pris;

        public HandlevognVare(Produkt p, int ant)
        {
            produkt = p;
            antall = ant;
            pris = ant * produkt.pris;
        }
    }

}
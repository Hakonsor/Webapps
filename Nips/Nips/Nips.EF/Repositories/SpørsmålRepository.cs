using Nips.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nips.Domain.Entities;
using Nips.DAL.Entities;

namespace Nips.DAL.Repositories
{
    public class SpørsmålRepository : ISpørsmålRepository
    {
        public bool deteleSpørsmål(int id)
        {
            try
            {
                var Db = new DataService();
                var spørsmål = Db.Spørsmålene.FirstOrDefault(e => e.id == id);
                Db.Spørsmålene.Remove(spørsmål);
                Db.SaveChanges();
                return true;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Spørsmål ble ikke lagret");
                return false;
            }
        }

        public List<Spørsmål> getList()
        {
            try { 
                var Db = new DataService();
                var liste = (from v in Db.Spørsmålene
                             select new Spørsmål()
                             {
                                 Beskrivelse = v.Beskrivelse,
                                 email = v.email,
                                 kundeid = v.id
                             })
                             .ToList();
                return liste;

            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Spørsmål kunne ikke bli lest opp");
                return null;
            }
                
        }

        public Spørsmål getSpørsmål(string epost)
         {
            using (var Db = new DataService())
            {
                var radspørsmål = Db.Spørsmålene.FirstOrDefault(e => e.email.Equals(epost));
                var spørsmål = new Spørsmål
                {
                    kundeid = radspørsmål.id,
                    email = radspørsmål.email,
                    Beskrivelse = radspørsmål.Beskrivelse
                };
                return spørsmål;
            }
         }

        public Spørsmål getSpørsmål(int id)
        {
            var Db = new DataService();
            var radspørsmål = Db.Spørsmålene.FirstOrDefault(e => e.id.Equals(id));
            var spørsmål = new Spørsmål
            {
                kundeid = radspørsmål.id,
                email = radspørsmål.email,
                Beskrivelse = radspørsmål.Beskrivelse
            };
            return spørsmål;
        }

        public bool lagreSpørsmål(Spørsmål spørsmål)
        {
            try{
                var Db = new DataService();
                var radspørsmål = new Spørsmålene()
                {
                    Beskrivelse = spørsmål.Beskrivelse,
                    email = spørsmål.email,
                  
                };
                Db.Spørsmålene.Add(radspørsmål);
                Db.SaveChanges();
                return true;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Spørmål ble ikke lagret");
                return false;
            }
            


        }

        public bool putSpørsmål(int id, Spørsmål spørsmål)
        {
            try
            {
                var Db = new DataService();
                var radspørsmål = new Spørsmålene()
                {
                    Beskrivelse = spørsmål.Beskrivelse,
                    email = spørsmål.email,
                    id = spørsmål.kundeid
                };
                var rad = Db.Spørsmålene.Find(id);
                rad = radspørsmål;
                Db.SaveChanges();
                return true;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Spørmål ble ikke endret");
                return false;
            }
        }


    }
}

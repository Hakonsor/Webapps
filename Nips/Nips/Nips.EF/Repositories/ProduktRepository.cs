using Nips.DAL.Entities;
using Nips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace Nips.DAL.Repositories
{
    public class ProduktRepository : IProduktRepository
    {
        public List<Produkt> alleVare()
        {
            var Db = new DataService();
            var liste = (from v in Db.Produkter 
                         select new Produkt()
                         {
                             id = v.id,
                             navn = v.Navn,
                             pris = v.Pris,
                             antall = v.Antall,
                             kategori = v.Kategori,
                             beskrivelse = v.Beskrivelse,
                             ImageUrl = v.ImageUrl
                         })
                         .ToList();
            return liste;
        }
  
        public int lagreVare(Produkt innVare)
        {
            var Db = new DataService();
            var nyVare = new Produkter()
            {
                Navn = innVare.navn,
                Beskrivelse = innVare.beskrivelse,
                Pris = innVare.pris,
                Antall = innVare.antall,
                Kategori = innVare.kategori,
                ImageUrl = innVare.ImageUrl
            };
            
            try
            {
                Db.Produkter.Add(nyVare);
                Db.SaveChanges();

                return nyVare.id;
            }
            catch (Exception e)
            {
                writeToFile(e);
                return -1;
            }
        }
        public bool endreVare(int id, Produkt innVare)
        {
            try
            {
                var Db = new DataService();
                var endreVare = Db.Produkter.Find(id);
                endreVare.Navn = innVare.navn;
                endreVare.Beskrivelse = innVare.beskrivelse;
                endreVare.Pris = innVare.pris;
                endreVare.Kategori = innVare.kategori;
                endreVare.ImageUrl = innVare.ImageUrl;
                Db.SaveChanges();
                
                writeToFile(new Exception(" Produkt med id: "+id+"\nhar blitt endret"));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool slettVare(int slettId)
        {

            try
            {
                var Db = new DataService();
                var slettVarer = Db.Produkter.Find(slettId);
                Db.Produkter.Remove(slettVarer);
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                writeToFile(e);
                return false;
            }
        }
        public Produkt hentEnVare(int id)
        {
            var Db = new DataService();
            var enDbVare = Db.Produkter.Find(id);

            if (enDbVare == null)
            {
                return null;
            }
            else
            {
                var utVare = new Produkt()
                {
                    id = enDbVare.id,
                    navn = enDbVare.Navn,
                    beskrivelse = enDbVare.Beskrivelse,
                    pris = enDbVare.Pris,
                    antall = enDbVare.Antall,
                    kategori = enDbVare.Kategori,
                    ImageUrl = enDbVare.ImageUrl
                };
                return utVare;
            }
        }

            //Skrive til fil
            private void writeToFile(Exception e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"NipsLogg.txt";

            try
            {
                var Db = new DataService();
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine("******   " + DateTime.Now.ToString() + "   ******");
                    writer.WriteLine("");
                    writer.WriteLine("Message: " + e.Message + Environment.NewLine
                       + "Stacktrace: " + e.StackTrace + Environment.NewLine);
                }
            }
            catch (IOException ioe)
            {
                Debug.WriteLine(ioe.Message);
            }
            catch (UnauthorizedAccessException uae)
            {
                Debug.WriteLine(uae.Message);
            }
        }

        void IProduktRepository.writeToFile(Exception e)
        {
            throw new NotImplementedException();
        }
    }
   }
    


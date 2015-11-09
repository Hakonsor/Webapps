using Nips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nips.DAL.Repositories;
using Nips.DAL.Entities;

namespace Nips.DAL.Repositories.Stud
{
    public class KundeRepositoryStud : DAL.Repositories.Stud.IKundeRepository
    {
        public List<Kunde> getAll()
        {
            try
            {
                var db = new DataService();
                List<Kunde> kunde = db.Kunder.Select(item => new Kunde()
                {
                    fornavn = item.Fornavn,
                    etternavn = item.Etternavn,
                    adresse = item.Adresse,
                    email = item.Email,
                    postnr = item.PoststedId.ToString(),
                    admin = item.Admin
                }).ToList();

                return kunde;
            }
            catch (Exception e)
            {
                writeToFile(e);
                return null;
            }
        }


        public bool add(Kunde iKunde, byte[] hashedPassord)
        {
            if (iKunde.kundeid == 1) return false;
            return true;
        }

        public bool checkEmail(string email, int? id)
        {

            if (email == "per@hotmail.com") return true;
            return false;
        }

        public Kunde finnKunde(string email)
        {
            if (email == "per@hotmail.com") return new Kunde()
            {
                kundeid = 1,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Oslo",
                email = "per@hotmail.com",
                poststed = "Oslo",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678",
                
                
            }; else if  (email == "lala@hotmail.com")
                    return new Kunde()
                    {
                        kundeid = 2,
                        fornavn = "Lala",
                        etternavn = "lala",
                        adresse = "lala",
                        email = "lala@hotmail.com",
                        poststed = "lala",
                        postnr = "1234",
                        tlf = "12345678",
                        passord = "12345678"


                    };
            return null;
        }

        public List<Kunde> getKunde()
        {
            var kund = new Kunde()
            {
                kundeid = 1,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Oslo",
                email = "per@hotmail.com",
                poststed = "Oslo",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"

            };
            var kund2 = new Kunde()
            {
                kundeid = 2,
                fornavn = "Lala",
                etternavn = "lala",
                adresse = "lala",
                email = "lala@hotmail.com",
                poststed = "lala",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"

            };
            List<Kunde> kundList = new List<Kunde>();
            kundList.Add(kund);
            kundList.Add(kund2);
            return kundList;
        }
        public Kunde getKunde(int id)
        {
            if (id == 0)
            {
                var kunde = new Kunde();
                kunde.kundeid = 0;
                return kunde;
            }
            else
            {
                var kunde = new Kunde()
                {
                    kundeid = 1,
                    fornavn = "Per",
                    etternavn = "Hansen",
                    adresse = "Per per",
                    email = "per@hotmail.com",
                    poststed = "1234",
                    postnr = "1234",
                    tlf = "12345678",
                    passord = "12345678"
                };
                return kunde;
            }
        }
        public bool endreKunde(int id, Kunde innKunde)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool slett(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool update(int id, Kunde updateUser)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool updatePass(int id, byte[] newPassord)
        {
            throw new NotImplementedException();
        }

        public bool validate(string email, byte[] newPassord)
        {
            System.Diagnostics.Debug.WriteLine(email == "lala@hotmail.com");
            if (email == "lala@hotmail.com" && makeHash("12345678") == newPassord)
                return true;
            return false;
        }

        public void writeToFile(Exception e)
        {
            throw new NotImplementedException();
        }

        public byte[] makeHash(String passord)
        {
            byte[] inData, outData;
            var algorithm = System.Security.Cryptography.SHA256.Create();
            inData = System.Text.Encoding.ASCII.GetBytes(passord);
            outData = algorithm.ComputeHash(inData);
            return outData;
        }
        public Kunde logIn(String email, String passord)
        {

            byte[] hashedPassord = makeHash(passord);
            bool ok = validate(email, hashedPassord);
            Debug.WriteLine("epost: " + email);
            Debug.WriteLine("hash: " + hashedPassord);
            Debug.WriteLine("OK" + ok);

            if (ok)
            {
                Kunde k = finnKunde(email);
                return k;
            }
            return null;

        }

        Kunde IKundeRepository.makeHash(string passord)
        {
            throw new NotImplementedException();
        }

        public List<Kunde> getResult(string sc)
        {
            throw new NotImplementedException();
        }
    }
}

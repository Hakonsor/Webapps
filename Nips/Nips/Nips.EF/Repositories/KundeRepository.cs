using System;
using System.Linq;
using System.Collections.Generic;
using Nips.Model.Entities;
using Nips.DAL.Entities;
using System.Diagnostics;
using System.IO;
using Nips.DAL.Repositories.Stud;

namespace Nips.DAL.Repositories
{
    public class KundeRepository : BaseRepository , IKundeRepository
    {
        public bool add(Kunde iKunde, byte[] hashedPassord)
        {
            var newKunde = new Kunder()
            {
                Fornavn = iKunde.fornavn,
                Etternavn = iKunde.etternavn,
                Adresse = iKunde.adresse,
                PoststedId = Convert.ToInt16(iKunde.postnr),
                Passord = hashedPassord,
                Tlf = iKunde.tlf,
                Email = iKunde.email,
                Admin = false
            };
            try
            {
                var existPostnr = Db.Poststeder.Find(Convert.ToInt16(iKunde.postnr));

                if (existPostnr == null)
                {
                    var newPoststed = new Poststeder()
                    {
                        PoststederID = Convert.ToInt16(iKunde.postnr),
                        Poststed = iKunde.poststed
                    };
                    newKunde.Poststeder = newPoststed;
                }
                Db.Kunder.Add(newKunde);
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                writeToFile(e);
                System.Diagnostics.Debug.WriteLine("Kunde ble ikke lagret");
                return false;
            }
        }
        public List<Kunde> getKunde()
        {
            try
            {

                var result = (from kunde in Db.Kunder
                             select
                                 new Kunde()
                                 {
                                     fornavn = kunde.Fornavn,
                                     etternavn = kunde.Etternavn,
                                     kundeid = kunde.Id,
                                     adresse = kunde.Adresse,
                                     email = kunde.Email,
                                     admin = kunde.Admin,
                                     tlf = kunde.Tlf,
                                     hashpassord = kunde.Passord,
                                     postnr = kunde.PoststedId.ToString(),
                                     poststed = kunde.Poststeder.Poststed,
                                 }).ToList();
            return result;

            }catch (Exception e)
            {
                writeToFile(e);
                System.Diagnostics.Debug.WriteLine("Listen kunne ikke bli hentet");
            }
                return null;
        }

        public Kunde finnKunde(String email)
        {
            Kunder userFound = Db.Kunder.FirstOrDefault(u => u.Email == email);
            Kunde k = new Kunde();
            k.kundeid = userFound.Id;
            k.fornavn = userFound.Fornavn;
            k.etternavn = userFound.Etternavn;
            k.email = userFound.Email;
            k.tlf = userFound.Tlf;
            k.adresse = userFound.Adresse;
            k.postnr = userFound.PoststedId.ToString();
            k.poststed = Db.Poststeder.Find(userFound.PoststedId).Poststed;
            k.hashpassord = userFound.Passord;
            k.admin = userFound.Admin;
            return k;
        }

        public bool slett(int id)
        {
            try
            {
                Kunder userFound = Db.Kunder.FirstOrDefault(u => u.Id == id);
                Db.Kunder.Remove(userFound);
                Db.SaveChanges();
                writeToFile(new Exception(" Kunden med id: " + id + "\nhar blitt endret"));
                return true;
            }
            catch (Exception e)
            {
                writeToFile(e);
                System.Diagnostics.Debug.WriteLine("Kunden ble ikke slettet");
                return false;
            }

        }

        public Kunde getKunde(int id) 
        {
            Kunder userFound = Db.Kunder.FirstOrDefault(u => u.Id == id);
            Kunde k = new Kunde();
            k.kundeid = userFound.Id;
            k.fornavn = userFound.Fornavn;
            k.etternavn = userFound.Etternavn;
            k.email = userFound.Email;
            k.tlf = userFound.Tlf;
            k.adresse = userFound.Adresse;
            k.postnr = userFound.PoststedId.ToString();
            k.poststed = Db.Poststeder.Find(userFound.PoststedId).Poststed;
            k.hashpassord = userFound.Passord;
            k.admin = userFound.Admin;
            return k;

        }

        public bool validate(String email, byte[] hashedPassord)
        {
            try
            {
                Kunder validated = Db.Kunder.FirstOrDefault(u => u.Passord == hashedPassord && u.Email == email);
                if (validated == null)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                writeToFile(e);
                System.Diagnostics.Debug.WriteLine("Validering mislykket");
                return false;
            }
        }

        public bool update(int id, Kunde updateUser)
        {

            try
            {
                Kunder kun = Db.Kunder.FirstOrDefault(u => u.Id == id);

                kun.Fornavn = updateUser.fornavn;
                kun.Etternavn = updateUser.etternavn;
                kun.Adresse = updateUser.adresse;
                kun.PoststedId = Convert.ToInt16(updateUser.postnr);
                kun.Tlf = updateUser.tlf;
                kun.Email = updateUser.email;
                kun.Admin = updateUser.admin;

                var existPostnr = Db.Poststeder.Find(Convert.ToInt16(updateUser.postnr));

                if (existPostnr == null)
                {
                    var newPoststed = new Poststeder()
                    {
                        PoststederID = Convert.ToInt16(updateUser.postnr),
                        Poststed = updateUser.poststed
                    };
                    kun.Poststeder = newPoststed;
                }
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                writeToFile(e);
                System.Diagnostics.Debug.WriteLine("Oppdatering mislykket");
                return false;

            }
        }

        public bool updatePass(int id, byte[] newPassord)
        {
            try
            {
                Kunder kun = Db.Kunder.FirstOrDefault(u => u.Id == id);
                kun.Passord = newPassord;
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                writeToFile(e);
                System.Diagnostics.Debug.WriteLine("Oppdatering av passord mislykket");
                return false;
            }

        }
        
        public bool checkEmail(string email, int? id)
        {

            Kunder kun = Db.Kunder.FirstOrDefault(u => u.Email.Equals(email));

            if ((kun == null) || (kun != null && kun.Id == id))
                return true;
            else
                return false;
        }

        //Skrive til fil
        private void writeToFile(Exception e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"NipsLogg.txt";

            try
            {
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

        void IKundeRepository.writeToFile(Exception e)
        {
            throw new NotImplementedException();
        }

        public Kunde logIn(string email, string passord)
        {
            throw new NotImplementedException();
        }

        public Kunde makeHash(string passord)
        {
            throw new NotImplementedException();
        }

        public List<Kunde> getAll()
        {
            throw new NotImplementedException();
        }

        public List<Kunde> getResult(string sc)
        {
            throw new NotImplementedException();
        }
    }
}




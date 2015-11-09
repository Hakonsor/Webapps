using Nips.DAL.Repositories;
using Nips.Model.Entities;
using System;
using System.Collections.Generic;
using BLL;
using Nips.DAL.Repositories.Stud;
using System.Diagnostics;

namespace Nips.BLL
{
    public class KundeBLL : IKundeLogikk
    {
        private IKundeRepository _repository;

        public KundeBLL()
        {
            _repository = new KundeRepository();
        }
        public KundeBLL(IKundeRepository stub)
        {
            _repository = stub;
        }
        public bool add(Kunde iKunde, byte[] hashedPassord)
        {
            return _repository.add(iKunde, hashedPassord);
        }
        public List<Kunde> getKunde()
        {
            return _repository.getKunde();
        }

        public List<Kunde> getResult(string s)
        {
            List<Kunde> alleKunder = _repository.getResult(s);
            return alleKunder;
        }

        public Kunde finnKunde(String email)
        {
            return _repository.finnKunde(email);
        }

        public bool slett(int id)
        {
            return _repository.slett(id);
        }

        public Kunde getKunde(int id)
        {
            return _repository.getKunde(id);

        }

        public bool validate(String email, byte[] hashedPassord)
        {
            return _repository.validate(email, hashedPassord);
        }

        public bool update(int id, Kunde updateUser)
        {
            return _repository.update(id, updateUser);

        }

        public bool updatePass(int id, byte[] newPassord)
        {
            return _repository.updatePass(id, newPassord);
        }

        public bool checkEmail(string email, int? id)
        {
            return _repository.checkEmail(email, id);

        }

        public Kunde logIn(String email, String passord)
        {
            return _repository.logIn(email, passord);
        }

        public bool endreKunde(Kunde iKunde, byte[] hashedPassord)
        {
            throw new NotImplementedException();
        }

        public void writeToFile(Exception e)
        {
            throw new NotImplementedException();
        }

        public List<Kunde> getAll()
        {
            throw new NotImplementedException();
        }
    }
}

   
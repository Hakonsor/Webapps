using Nips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Nips.DAL.Repositories.Stud
{
    public interface IKundeRepository
    {
        bool add(Kunde iKunde, byte[] hashedPassord);
        List<Kunde> getKunde();
        Kunde finnKunde(String email);
        bool slett(int id);
        Kunde makeHash(String passord);
        Kunde getKunde(int id);
        Kunde logIn(String email, String passord);
        bool validate(String email, byte[] newPassord);
        bool update(int id, Kunde updateUser);
        bool updatePass(int id, byte[] newPassord);
        bool checkEmail(string email, int? id);
        List<Kunde> getAll();
        List<Kunde> getResult(string sc);
        void writeToFile(Exception e);
    }
}

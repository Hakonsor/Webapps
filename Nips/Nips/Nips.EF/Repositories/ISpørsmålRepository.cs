using Nips.DAL.Entities;
using Nips.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nips.DAL.Repositories
{
    public interface ISpørsmålRepository
    {
        bool lagreSpørsmål(Spørsmål spørsmål);
        Spørsmål getSpørsmål(int id);
        List<Spørsmål> getList();
        Spørsmål getSpørsmål(String epost);
        bool putSpørsmål(int id, Spørsmål spørsmål);
        bool deteleSpørsmål(int id);
    }
}

using Nips.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Test
{
    public interface ISpørsmålLogikk
    {
        bool lagreSpørsmål(Spørsmål spørsmål);
        Spørsmål getSpørsmål(int id);
        List<Spørsmål> getList();
        Spørsmål getSpørsmål(String epost);
        bool putSpørsmål(int id, Spørsmål spørsmål);
        bool deleteSpørsmål(int id);
    }
}

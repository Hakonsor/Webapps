using BLL.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nips.Domain.Entities;
using Nips.DAL.Repositories;

namespace BLL
{
    public class SpørsmålBLL : ISpørsmålLogikk
    {
        private SpørsmålRepository _repository;

        public SpørsmålBLL()
        {
            _repository = new SpørsmålRepository();
        }

        public Spørsmål getSpørsmål(string epost)
        {
            return _repository.getSpørsmål(epost);
        }

        public Spørsmål getSpørsmål(int id)
        {
            return _repository.getSpørsmål(id);
        }

        public List<Spørsmål> getList()
        {
            return _repository.getList();
        }

        public bool lagreSpørsmål(Spørsmål spørsmål)
        {
            return _repository.lagreSpørsmål(spørsmål);
        }

        public bool putSpørsmål(int id, Spørsmål spørsmål)
        {
            return _repository.putSpørsmål(id, spørsmål);
        }

        public bool deteleSpørsmål(int id)
        {
            return _repository.deteleSpørsmål(id);
        }
    }

}
 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Healthis.Entities;
using Healthis.Model;

namespace Healthis.Service
{
    public class VacinacaoService
    {
        private VacinacaoModel vacinacaoModel;
        public VacinacaoService(string connectionString)
        {
            vacinacaoModel = new VacinacaoModel(connectionString);
        }

        public Vacinacao Create(Vacinacao vacinacao) => vacinacaoModel.Create(vacinacao);
        public Vacinacao Update(Vacinacao vacinacao) => vacinacaoModel.Update(vacinacao);
        public bool Delete(int vacinacaoID) => vacinacaoModel.Delete(vacinacaoID);
        public List<Vacinacao> GetAll() => vacinacaoModel.GetAll();
        public Vacinacao Get(int vacinacaoID) => vacinacaoModel.Get(vacinacaoID);
        public bool AssociarVacinaVacinacao(int vacinaID, int vacinacaoID) => vacinacaoModel.AssociarVacinaVacinacao(vacinaID, vacinacaoID);
    }
}

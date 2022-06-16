using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Healthis.Entities;
using Healthis.Model;

namespace Healthis.Service
{
    public class VacinaService
    {
        private VacinaModel vacinaModel;
        public VacinaService(string connectionString)
        {
            vacinaModel = new VacinaModel(connectionString);
        }

        public Vacina Create(Vacina vacina) => vacinaModel.Create(vacina);
        public Vacina Update(Vacina vacina) => vacinaModel.Update(vacina);
        public bool Delete(int vacinaID) => vacinaModel.Delete(vacinaID);
        public List<Vacina> GetAll => vacinaModel.GetAll();
        public Vacina Get(int vacinaID) => vacinaModel.Get(vacinaID);
    }
}

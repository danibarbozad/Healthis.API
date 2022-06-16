using Healthis.Entities;
using Healthis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthis.Service
{
    public class UnidadeSaudeService
    {
        private UnidadeSaudeModel unidadeSaudeModel;
        public UnidadeSaudeService(string connectionString)
        {
            unidadeSaudeModel = new UnidadeSaudeModel(connectionString);
        }

        public UnidadeSaude Create(UnidadeSaude unidadeSaude) => unidadeSaudeModel.Create(unidadeSaude);
        public UnidadeSaude Update(UnidadeSaude unidadeSaude) => unidadeSaudeModel.Update(unidadeSaude);
        public bool Delete(int unidadeSaudeID) => unidadeSaudeModel.Delete(unidadeSaudeID);
        public List<UnidadeSaude> GetAll() => unidadeSaudeModel.GetAll();
        public UnidadeSaude Get(int unidadeSaudeID) => unidadeSaudeModel.Get(unidadeSaudeID);
    }
}

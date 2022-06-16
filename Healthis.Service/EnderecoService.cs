using Healthis.Entities;
using Healthis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthis.Service
{
    public class EnderecoService
    {
        private EnderecoModel enderecoModel;
        public EnderecoService(string connectionString)
        {
            enderecoModel = new EnderecoModel(connectionString);
        }

        public Endereco Create(Endereco endereco) => enderecoModel.Create(endereco);
        public Endereco Update(Endereco endereco) => enderecoModel.Update(endereco);
        public bool Delete(int enderecoID) => enderecoModel.Delete(enderecoID);
        public List<Endereco> GetAll => enderecoModel.GetAll();
        public Endereco Get(int enderecoID) => enderecoModel.Get(enderecoID);
    }
}

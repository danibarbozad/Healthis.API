using Healthis.Entities.ApiEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthis.Entities
{
    public class Endereco
    {
        // DB_Column = id_endereco
        public int ID { get; set; }
        // DB_Column = rua
        public string Rua { get; set; }
        // DB_Column = bairro
        public string Bairro { get; set; }
        // DB_Column = numero
        public string Numero { get; set; }
        // DB_Column = cep
        public string CEP { get; set; }
        // DB_Column = cidade
        public string Cidade { get; set; }
        // DB_Column = uf
        public string UF { get; set; }

        public Endereco ConvertFromRequest(EnderecoRequest request)
        {
            return new Endereco
            {
                Rua = request.Rua,
                Bairro = request.Bairro,
                Numero = request.Numero,
                CEP = request.CEP,
                Cidade = request.Cidade,
                UF = request.UF
            };
        }
    }
}

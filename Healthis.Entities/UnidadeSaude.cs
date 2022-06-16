using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthis.Entities
{
    public class UnidadeSaude
    {
        // DB_Column = id_unidade_saude
        public int ID { get; set; }

        // DB_Column = nome_unidade
        public string NomeUnidade { get; set; }

        // DB_Column = endereco_id_endereco
        public int EnderecoID { get; set; }
        public Endereco Endereco { get; set; }
    }
}

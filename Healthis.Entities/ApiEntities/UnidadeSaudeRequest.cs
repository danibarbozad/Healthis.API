using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healthis.Entities.ApiEntities
{
    public class UnidadeSaudeRequest
    {
        // DB_Column = id_unidade_saude
        public int ID { get; set; }

        // DB_Column = nome_unidade
        public string NomeUnidade { get; set; }

        // DB_Column = endereco_id_endereco
        public int EnderecoID { get; set; }
    }
}
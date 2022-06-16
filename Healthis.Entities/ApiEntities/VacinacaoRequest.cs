using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healthis.Entities.ApiEntities
{
    public class VacinacaoRequest
    {
        // DB_Column = dt_vacinacao
        public DateTime DataVacinacao { get; set; }

        // DB_Column = dt_proxima_dose
        public DateTime DataProximaDose { get; set; }

        // DB_Column = reacao
        public string Reacao { get; set; }

        // DB_Column = descricao_reacao
        public string DescricaoReacao { get; set; }

        // DB_Column = unidade_saude_id_unidade_saude
        public int UnidadeSaudeID { get; set; }

        // DB_Column = unidade_saude_endereco_id_endereco
        public int EnderecoID { get; set; }
    }
}
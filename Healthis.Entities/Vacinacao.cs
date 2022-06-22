using Healthis.Entities.ApiEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthis.Entities
{
    public class Vacinacao
    {
        // DB_Column = id_vacinacao
        public int ID { get; set; }

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
        public UnidadeSaude UnidadeSaude { get; set; }

        // DB_Column = unidade_saude_endereco_id_endereco
        public int EnderecoID { get; set; }
        public Endereco Endereco { get; set; }

        public List<Vacina> Vacinas { get; set; }
        public bool VacinacaoHasVacinas { get { return Vacinas?.Count > 0; } }

        public Vacinacao ConvertFromRequest(VacinacaoRequest request)
        {
            return new Vacinacao
            {
                DataVacinacao = request.DataVacinacao,
                DataProximaDose = request.DataProximaDose,
                Reacao = request.Reacao,
                DescricaoReacao = request.DescricaoReacao,
                UnidadeSaudeID = request.UnidadeSaudeID,
                EnderecoID = request.EnderecoID
            };
        }
    }
}

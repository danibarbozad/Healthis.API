using Healthis.Entities.ApiEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthis.Entities
{
    public class Vacina
    {
        // DB_Column = id_vacina
        public int ID { get; set; }

        // DB_Column = nome_vacina
        public string NomeVacina { get; set; }

        // DB_Column = quantidade_dose
        public int QuantidadeDose { get; set; }

        // DB_Column = validade
        public DateTime Validade { get; set; }

        // DB_Column = lote
        public string Lote { get; set; }

        public Vacina ConvertFromRequest(VacinaRequest request)
        {
            return new Vacina
            {
                NomeVacina = request.NomeVacina,
                QuantidadeDose = request.QuantidadeDose,
                Validade = request.Validade,
                Lote = request.Lote
            };
        }
    }
}

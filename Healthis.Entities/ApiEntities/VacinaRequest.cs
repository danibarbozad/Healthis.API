using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healthis.Entities.ApiEntities
{
    public class VacinaRequest
    {
        // DB_Column = nome_vacina
        public string NomeVacina { get; set; }

        // DB_Column = quantidade_dose
        public int QuantidadeDose { get; set; }

        // DB_Column = validade
        public DateTime Validade { get; set; }

        // DB_Column = lote
        public string Lote { get; set; }
    }
}
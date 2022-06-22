using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthis.Entities.ApiEntities
{
    public class VacinacaoUsuarioRequest
    {
        public int UsuarioID { get; set; }
        public int VacinacaoID { get; set; }
    }
}

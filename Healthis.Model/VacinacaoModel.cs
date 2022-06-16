using Dapper;
using Healthis.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthis.Model
{
    public class VacinacaoModel
    {
        private string _connectionString;

        public VacinacaoModel(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Vacinacao Create(Vacinacao vacinacao)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Query<Vacinacao>("");
            }

            return vacinacao;
        }
    }
}

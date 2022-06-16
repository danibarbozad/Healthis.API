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
    public class VacinaModel
    {
        private string _connectionString;

        public VacinaModel(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Vacina Create(Vacina vacina)
        {
            try
            {
                string query = $@"
                    INSERT INTO vacina
                        (nome_vacina,
                        quantidade_dose,
                        validade,
                        lote)
                    VALUES
                        (@NomeVacina,
                        @QuantidadeDose,
                        @Validade,
                        @Lote);
                    SELECT LAST_INSERT_ID() FROM vacina;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    int id = conn.Query<int>(query, vacina).FirstOrDefault();
                    vacina.ID = id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vacina;
        }

        public Vacina Update(Vacina vacina)
        {
            try
            {
                string query = $@"
                    UPDATE vacina
                    SET
                        nome_vacina = @NomeVacina,
                        quantidade_dose = @QuantidadeDose,
                        validade = @Validade,
                        lote = @Lote
                    WHERE id_vacina = @ID;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Execute(query, vacina);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vacina;
        }

        public Boolean Delete(int vacinaID)
        {
            int success;
            try
            {
                string query = $@"
                    DELETE FROM vacina WHERE id_vacina = @ID";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    success = conn.Execute(query, new { ID = vacinaID });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success > 0;
        }

        public List<Vacina> GetAll()
        {
            List<Vacina> listaVacinas = new List<Vacina>();

            try
            {
                string query = $@"
                    SELECT 
                        id_vacina       AS @ID,
                        nome_vacina     AS @NomeVacina,
                        quantidade_dose AS @QuantidadeDose,
                        validade        AS @Validade,
                        lote            AS @Lote
                    FROM vacina;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    listaVacinas = conn.Query<Vacina>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaVacinas;
        }

        public Vacina Get(int ID)
        {
            Vacina vacina;
            try
            {
                string query = $@"
                    SELECT 
                        id_vacina       AS @ID,
                        nome_vacina     AS @NomeVacina,
                        quantidade_dose AS @QuantidadeDose,
                        validade        AS @Validade,
                        lote            AS @Lote
                    FROM vacina
                    WHERE id_vacina = @ID;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    vacina = conn.Query<Vacina>(query, new { ID }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vacina;
        }
    }
}

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
            try
            {
                string query = $@"
                    INSERT INTO vacinacao
                        (dt_vacinacao,
                        dt_proxima_dose,
                        reacao,
                        descricao_reacao,
                        unidade_saude_id_unidade_saude,
                        unidade_saude_endereco_id_endereco)
                    VALUES
                        (@DataVacinacao,
                        @DataProximaDose,
                        @Reacao,
                        @DescricaoReacao,
                        @UnidadeSaudeID,
                        @EnderecoID);
                    SELECT LAST_INSERT_ID() FROM vacinacao;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    int id = conn.Query<int>(query, vacinacao).FirstOrDefault();
                    vacinacao.ID = id;
                }

                vacinacao.Endereco = new EnderecoModel(_connectionString).Get(vacinacao.EnderecoID);
                vacinacao.UnidadeSaude = new UnidadeSaudeModel(_connectionString).Get(vacinacao.UnidadeSaudeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vacinacao;
        }

        public Vacinacao Update(Vacinacao vacinacao)
        {
            try
            {
                string query = $@"
                    UPDATE vacinacao
                    SET
                        dt_vacinacao = @DataVacinacao,
                        dt_proxima_dose = @DataProximaDose,
                        reacao = @Reacao,
                        descricao_reacao = @DescricaoReacao,
                        unidade_saude_id_unidade_saude = @UnidadeSaudeID,
                        unidade_saude_endereco_id_endereco = @EnderecoID
                    WHERE id_vacinacao = @ID AND unidade_saude_id_unidade_saude = @UnidadeSaudeID AND unidade_saude_endereco_id_endereco = @EnderecoID;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Execute(query, vacinacao);
                }

                vacinacao.Endereco = new EnderecoModel(_connectionString).Get(vacinacao.EnderecoID);
                vacinacao.UnidadeSaude = new UnidadeSaudeModel(_connectionString).Get(vacinacao.UnidadeSaudeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vacinacao;
        }

        public Boolean Delete(int vacinacaoID)
        {
            int success;
            try
            {
                string query = $@"
                    DELETE FROM vacinacao WHERE id_vacinacao = @ID;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    success = conn.Execute(query, new { ID = vacinacaoID });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success > 0;
        }

        public List<Vacinacao> GetAll()
        {
            List<Vacinacao> listaVacinacoes = new List<Vacinacao>();

            try
            {
                string query = $@"
                    SELECT 
                        id_vacinacao AS ID,
                        dt_vacinacao AS DataVacinacao,
                        dt_proxima_dose AS DataProximaDose,
                        reacao AS Reacao,
                        descricao_reacao AS DescricaoReacao,
                        unidade_saude_id_unidade_saude AS UnidadeSaudeID,
                        unidade_saude_endereco_id_endereco AS EnderecoID
                    FROM vacinacao;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    listaVacinacoes = conn.Query<Vacinacao>(query).ToList();
                }

                foreach (Vacinacao vacinacao in listaVacinacoes)
                {
                    vacinacao.Endereco = new EnderecoModel(_connectionString).Get(vacinacao.EnderecoID);
                    vacinacao.UnidadeSaude = new UnidadeSaudeModel(_connectionString).Get(vacinacao.UnidadeSaudeID);
                    vacinacao.Vacinas = GetVacinacaoVacinas(vacinacao.ID);
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaVacinacoes;
        }

        public Vacinacao Get(int ID)
        {
            Vacinacao vacinacao;
            try
            {
                string query = $@"
                     SELECT 
                        id_vacinacao AS ID,
                        dt_vacinacao AS DataVacinacao,
                        dt_proxima_dose AS DataProximaDose,
                        reacao AS Reacao,
                        descricao_reacao AS DescricaoReacao,
                        unidade_saude_id_unidade_saude AS UnidadeSaudeID,
                        unidade_saude_endereco_id_endereco AS EnderecoID
                    FROM vacinacao;
                    WHERE id_vacinacao = @ID";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    vacinacao = conn.Query<Vacinacao>(query, new { ID }).FirstOrDefault();
                }

                vacinacao.Endereco = new EnderecoModel(_connectionString).Get(vacinacao.EnderecoID);
                vacinacao.UnidadeSaude = new UnidadeSaudeModel(_connectionString).Get(vacinacao.UnidadeSaudeID);
                vacinacao.Vacinas = GetVacinacaoVacinas(vacinacao.ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vacinacao;
        }

        public Boolean AssociarVacinaVacinacao(int vacinaID, int vacinacaoID)
        {
            try
            {
                string query = $@"
                    INSERT INTO vacina_has_vacinacao
	                    (vacina_id_vacina,
	                    vacinacao_id_vacinacao)
                    VALUES
	                    (@VacinaID,
	                    @VacinacaoID);";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Execute(query, new { VacinaID = vacinaID, VacinacaoID = vacinacaoID });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public List<Vacina> GetVacinacaoVacinas(int vacinacaoID)
        {
            List<Vacina> vacinas = new List<Vacina>();
            try
            {
                string query = $@"
                     SELECT 
	                    id_vacina       AS ID,
                        nome_vacina     AS NomeVacina,
                        quantidade_dose AS QuantidadeDose,
                        validade        AS Validade,
                        lote            AS Lote
                    FROM 
	                    vacina_has_vacinacao
                    INNER JOIN vacinacao ON vacinacao.id_vacinacao = vacina_has_vacinacao.vacinacao_id_vacinacao
                    INNER JOIN vacina ON vacina.id_vacina = vacina_has_vacinacao.vacina_id_vacina
                    WHERE vacinacao.id_vacinacao = @ID;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    vacinas = conn.Query<Vacina>(query, new { ID = vacinacaoID }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vacinas;
        }
    }
}

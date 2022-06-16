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
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vacinacao;
        }
    }
}

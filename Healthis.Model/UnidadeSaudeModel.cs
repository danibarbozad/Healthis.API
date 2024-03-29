﻿using Dapper;
using Healthis.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthis.Model
{
    public class UnidadeSaudeModel
    {
        private string _connectionString;

        public UnidadeSaudeModel(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UnidadeSaude Create(UnidadeSaude unidadeSaude)
        {
            try
            {
                string query = $@"
                    INSERT INTO unidade_saude
                        (nome_unidade,
                        endereco_id_endereco)
                    OUTPUT Inserted.id_unidade_saude
                    VALUES
                        (@NomeUnidade,
                        @EnderecoID);";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    int id = conn.Query<int>(query, unidadeSaude).FirstOrDefault();
                    unidadeSaude.ID = id;
                }

                unidadeSaude.Endereco = new EnderecoModel(_connectionString).Get(unidadeSaude.EnderecoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return unidadeSaude;
        }

        public UnidadeSaude Update(UnidadeSaude unidadeSaude)
        {
            try
            {
                string query = $@"
                    UPDATE unidade_saude
                    SET
                        nome_unidade = @NomeUnidade,
                        endereco_id_endereco = @EnderecoID
                    WHERE id_unidade_saude = @ID AND endereco_id_endereco = @EnderecoID;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Execute(query, unidadeSaude);
                }

                unidadeSaude.Endereco = new EnderecoModel(_connectionString).Get(unidadeSaude.EnderecoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return unidadeSaude;
        }

        public Boolean Delete(int unidadeSaudeID)
        {
            int success;
            try
            {
                string query = $@"
                    DELETE FROM unidade_saude WHERE id_unidade_saude = @ID;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    success = conn.Execute(query, new { ID = unidadeSaudeID });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success > 0;
        }

        public List<UnidadeSaude> GetAll()
        {
            List<UnidadeSaude> listaUnidadesSaude = new List<UnidadeSaude>();

            try
            {
                string query = $@"
                    SELECT 
                        unidade_saude.id_unidade_saude      AS ID,
                        unidade_saude.nome_unidade          AS NomeUnidade,
                        unidade_saude.endereco_id_endereco  AS EnderecoID
                    FROM unidade_saude;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    listaUnidadesSaude = conn.Query<UnidadeSaude>(query).ToList();
                }

                foreach (UnidadeSaude unidadeSaude in listaUnidadesSaude)
                    unidadeSaude.Endereco = new EnderecoModel(_connectionString).Get(unidadeSaude.EnderecoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaUnidadesSaude;
        }

        public UnidadeSaude Get(int ID)
        {
            UnidadeSaude unidadeSaude;
            try
            {
                string query = $@"
                    SELECT 
                        id_unidade_saude      AS ID,
                        nome_unidade          AS NomeUnidade,
                        endereco_id_endereco  AS EnderecoID
                    FROM unidade_saude
                    WHERE id_unidade_saude = @ID;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    unidadeSaude = conn.Query<UnidadeSaude>(query, new { ID }).FirstOrDefault();
                }

                if (unidadeSaude == null)
                    return unidadeSaude;

                unidadeSaude.Endereco = new EnderecoModel(_connectionString).Get(unidadeSaude.EnderecoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return unidadeSaude;
        }
    }
}

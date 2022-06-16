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
    public class EnderecoModel
    {
        private string _connectionString;

        public EnderecoModel(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Endereco Create(Endereco endereco)
        {
            try
            {
                string query = $@"
                    INSERT INTO endereco    (rua, bairro, numero, cep, cidade, uf) 
                        VALUES              (@Rua, @Bairro, @Numero, @CEP, @Cidade, @UF);
                    SELECT LAST_INSERT_ID() FROM endereco;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    int id = conn.Query<int>(query, endereco).FirstOrDefault();
                    endereco.ID = id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return endereco;
        }

        public Endereco Update(Endereco endereco)
        {
            try
            {
                string query = $@"
                    UPDATE endereco
                    SET rua     = @Rua, 
	                    bairro  = @Bairro, 
                        numero  = @Numero, 
                        cep     = @CEP, 
                        cidade  = @Cidade, 
                        uf      = @UF
                    WHERE
	                    id_endereco = @ID;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Execute(query, endereco);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return endereco;
        }

        public Boolean Delete(int enderecoID)
        {
            int success;
            try
            {
                string query = $@"
                    DELETE FROM endereco WHERE id_endereco = @ID";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    success = conn.Execute(query, new { ID = enderecoID });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success > 0;
        }

        public List<Endereco> GetAll()
        {
            List<Endereco> listaEnderecos = new List<Endereco>();

            try
            {
                string query = $@"
                    SELECT 
	                    id_endereco AS ID,
	                    rua AS Rua, 
	                    bairro AS Bairro, 
                        numero AS Numero, 
                        cep AS CEP, 
                        cidade AS Cidade, 
                        uf AS UF 
                    FROM endereco;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    listaEnderecos = conn.Query<Endereco>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaEnderecos;
        }

        public Endereco Get(int ID)
        {
            Endereco endereco;
            try
            {
                string query = $@"
                    SELECT 
	                    id_endereco AS ID,
	                    rua AS Rua, 
	                    bairro AS Bairro, 
                        numero AS Numero, 
                        cep AS CEP, 
                        cidade AS Cidade, 
                        uf AS UF 
                    FROM endereco
                    WHERE id_endereco = @ID;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    endereco = conn.Query<Endereco>(query, new { ID }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return endereco;
        }
    }
}

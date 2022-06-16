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
    public class UsuarioModel
    {
        private string _connectionString;

        public UsuarioModel(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Usuario Create(Usuario usuario)
        {
            try
            {
                string query = $@"
                    INSERT INTO usuario
                        (nome_usuario,
                        cpf,
                        sexo,
                        dt_nascimento,
                        email,
                        senha,
                        telefone,
                        endereco_id_endereco)
                    VALUES
                        (@Nome,
                        @CPF,
                        @Sexo,
                        @DataNascimento,
                        @Email,
                        @Senha,
                        @Telefone,
                        @EnderecoID);
                    SELECT LAST_INSERT_ID() FROM usuario;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    int id = conn.Query<int>(query, usuario).FirstOrDefault();
                    usuario.ID = id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }

        public Usuario Update(Usuario usuario)
        {
            try
            {
                string query = $@"
                    UPDATE usuario
                    SET
                        nome_usuario = @Nome,
                        cpf = @CPF,
                        sexo = @Sexo,
                        dt_nascimento = @DataNascimento,
                        email = @Email,
                        senha = @Senha,
                        telefone = @Telefone,
                        endereco_id_endereco = @EnderecoID
                    WHERE id_usuario = @ID AND endereco_id_endereco = @EnderecoID;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Execute(query, usuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }

        public Boolean Delete(int usuarioID)
        {
            int success;
            try
            {
                string query = $@"
                    DELETE FROM usuario WHERE id_usuario = @ID;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    success = conn.Execute(query, new { ID = usuarioID });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success > 0;
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            try
            {
                string query = $@"
                    SELECT 
	                    id_usuario AS ID,
                        nome_usuario AS Nome,
                        cpf AS CPF,
                        sexo AS Sexo,
                        dt_nascimento AS DataNascimento,
                        email AS Email,
                        senha AS Senha,
                        telefone AS Telefone,
                        endereco_id_endereco AS EnderecoID
                    FROM usuario;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    listaUsuarios = conn.Query<Usuario>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaUsuarios;
        }

        public Usuario Get(int ID)
        {
            Usuario usuario;
            try
            {
                string query = $@"
                    SELECT 
	                    id_usuario AS ID,
                        nome_usuario AS Nome,
                        cpf AS CPF,
                        sexo AS Sexo,
                        dt_nascimento AS DataNascimento,
                        email AS Email,
                        senha AS Senha,
                        telefone AS Telefone,
                        endereco_id_endereco AS EnderecoID
                    FROM usuario
                    WHERE id_usuario = @ID;";

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    usuario = conn.Query<Usuario>(query, new { ID }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }
    }
}

using Dapper;
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
                        telefone,
                        endereco_id_endereco,
                        username)
                    OUTPUT Inserted.id_usuario
                    VALUES
                        (@Nome,
                        @CPF,
                        @Sexo,
                        @DataNascimento,
                        @Email,
                        @Telefone,
                        @EnderecoID,
                        @UserName);";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    int id = conn.Query<int>(query, usuario).FirstOrDefault();
                    usuario.ID = id;
                }

                if (usuario.EnderecoID.HasValue)
                    usuario.Endereco = new EnderecoModel(_connectionString).Get(usuario.EnderecoID.Value);
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
                        telefone = @Telefone,
                        endereco_id_endereco = @EnderecoID
                    WHERE id_usuario = @ID;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
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

                using (SqlConnection conn = new SqlConnection(_connectionString))
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
                        telefone AS Telefone,
                        endereco_id_endereco AS EnderecoID,
                        username AS UserName
                    FROM usuario;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    listaUsuarios = conn.Query<Usuario>(query).ToList();
                }

                foreach (Usuario usuario in listaUsuarios)
                {
                    if (usuario.EnderecoID.HasValue)
                        usuario.Endereco = new EnderecoModel(_connectionString).Get(usuario.EnderecoID.Value);
                    usuario.Vacinacoes = GetUsuarioVacinacoes(usuario.ID);
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
                        telefone AS Telefone,
                        endereco_id_endereco AS EnderecoID,
                        username AS UserName
                    FROM usuario
                    WHERE id_usuario = @ID;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    usuario = conn.Query<Usuario>(query, new { ID }).FirstOrDefault();
                }

                if (usuario == null)
                    return usuario;

                if (usuario.EnderecoID.HasValue)
                    usuario.Endereco = new EnderecoModel(_connectionString).Get(usuario.EnderecoID.Value);
                usuario.Vacinacoes = GetUsuarioVacinacoes(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }

        public List<Vacinacao> GetUsuarioVacinacoes(int usuarioID)
        {
            List<Vacinacao> vacinacoes = new List<Vacinacao>();
            try
            {
                string query = $@"
                    SELECT 
	                    vacinacao.id_vacinacao AS ID,
	                    vacinacao.dt_vacinacao AS DataVacinacao,
	                    vacinacao.dt_proxima_dose AS DataProximaDose,
	                    vacinacao.reacao AS Reacao,
	                    vacinacao.descricao_reacao AS DescricaoReacao,
	                    vacinacao.unidade_saude_id_unidade_saude AS UnidadeSaudeID,
	                    vacinacao.unidade_saude_endereco_id_endereco AS EnderecoID
                    FROM 
	                    usuario_has_vacinacao
                    INNER JOIN vacinacao ON vacinacao.id_vacinacao = usuario_has_vacinacao.vacinacao_id_vacinacao
                    INNER JOIN usuario ON usuario.id_usuario = usuario_has_vacinacao.usuario_id_usuario
                    WHERE
	                    usuario_id_usuario = @ID;";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    vacinacoes = conn.Query<Vacinacao>(query, new { ID = usuarioID }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vacinacoes;
        }

        public bool VincularVacinacaoUsuario(int usuarioID, int vacinacaoID)
        {
            try
            {
                string query = $@"
                    INSERT INTO usuario_has_vacinacao
                        (usuario_id_usuario,
                        vacinacao_id_vacinacao)
                    VALUES
                        (@UsuarioID,
                        @VacinacaoID);";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Execute(query, new { UsuarioID = usuarioID, VacinacaoID = vacinacaoID });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}

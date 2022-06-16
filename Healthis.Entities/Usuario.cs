﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthis.Entities
{
    public class Usuario
    {
        // DB_Column = id_usuario
        public int ID { get; set; }
        // DB_Column = nome_usuario
        public string Nome { get; set; }
        // DB_Column = cpf
        public int CPF { get; set; }
        // DB_Column = sexo
        public string Sexo { get; set; }
        // DB_Column = dt_nascimento
        public DateTime DataNascimento { get; set; }
        // DB_Column = email
        public string Email { get; set; }
        // DB_Column = senha
        public string Senha { get; set; }
        // DB_Column = telefone
        public string Telefone { get; set; }
        // DB_Column = endereco_id_endereco
        public int EnderecoID { get; set; }        
        public Endereco Endereco { get; set; }
    }
}
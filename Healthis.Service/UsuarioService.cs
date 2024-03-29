﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Healthis.Entities;
using Healthis.Model;

namespace Healthis.Service
{
    public class UsuarioService
    {
        private UsuarioModel usuarioModel;
        public UsuarioService(string connectionString)
        {
            usuarioModel = new UsuarioModel(connectionString);
        }

        public Usuario Create(Usuario usuario) => usuarioModel.Create(usuario);
        public Usuario Update(Usuario usuario)
        {
            return Get(usuarioModel.Update(usuario).ID);
        }

        public bool Delete(int usuarioID) => usuarioModel.Delete(usuarioID);
        public List<Usuario> GetAll() => usuarioModel.GetAll();
        public Usuario Get(int usuarioID) => usuarioModel.Get(usuarioID);
        public Usuario Get(string userName) => usuarioModel.GetUserByUserName(userName);
        public bool VincularVacinacaoUsuario(int usuarioID, int vacinacaoID) => usuarioModel.VincularVacinacaoUsuario(usuarioID, vacinacaoID);
    }
}

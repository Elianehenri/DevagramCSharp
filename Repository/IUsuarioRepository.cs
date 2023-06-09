﻿using DevagramCSharp.Models;

namespace DevagramCSharp.Repository
{
    //assinatura de contratos
    public interface IUsuarioRepository
    {
        Usuario GetUsuarioPorLoginSenha(string email, string senha);
        Usuario GetUsuarioPorId(int id);

        public void AtualizarUsuario(Usuario usuario);
        public void Salvar(Usuario usuario);

        public bool VerificarEmail(string email);

        List<Usuario> GetUsuarioNome(string nome);

    }
}

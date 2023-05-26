using DevagramCSharp.Models;

namespace DevagramCSharp.Repository
{
    //assinatura de contratos
    public interface IUsuarioRepository
    {
         
        public void Salvar(Usuario usuario);

        public bool VerificarEmail(string email);



    }
}

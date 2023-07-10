using DevagramCSharp.Dtos;
using DevagramCSharp.Models;

namespace DevagramCSharp.Repository
{
    public interface IPublicacaoRepository
    {
        public void Publicar(Publicacao publicacao);
        List<FeedRespostaDto> GetPublicacoesFeed(int idUsuario);
        List<FeedRespostaDto> GetPublicacoesFeedUsuario(int idUsuario);
        int GetQtdePublicacoes(int idUsuario);
    }
}

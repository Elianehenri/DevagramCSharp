﻿using DevagramCSharp.Dtos;
using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Impl
{
    public class PublicacaoRepositoryImpl : IPublicacaoRepository
    {
        private readonly DevagramContext _context;

        public PublicacaoRepositoryImpl(DevagramContext context)
        {
            _context = context;
        }
        public List<FeedRespostaDto> GetPublicacoesFeed(int idUsuario)
        {
            var feed =
                from publicacoes in _context.Publicacoes
                join seguidores in _context.Seguidores on publicacoes.IdUsuario equals seguidores.IdUsuarioSeguido
                where seguidores.IdUsuarioSeguidor == idUsuario
                select new FeedRespostaDto
                {
                    IdPublicacao = publicacoes.Id,
                    Descricao = publicacoes.Descricao,
                    Foto = publicacoes.Foto,
                    IdUsuario = publicacoes.IdUsuario
                };

            return feed.ToList();
        }

        public List<FeedRespostaDto> GetPublicacoesFeedUsuario(int idUsuario)
        {
            var feedusuario =
                from publicacoes in _context.Publicacoes
                where publicacoes.IdUsuario == idUsuario
                select new FeedRespostaDto
                {
                    IdPublicacao = publicacoes.Id,
                    Descricao = publicacoes.Descricao,
                    Foto = publicacoes.Foto,
                    IdUsuario = publicacoes.IdUsuario
                };

            return feedusuario.ToList();
        }

        public int GetQtdePublicacoes(int idUsuario)
        {
            return _context.Publicacoes.Count(p => p.IdUsuario == idUsuario);
        }



        public void Publicar(Publicacao publicacao)
        {
            _context.Add(publicacao);
            _context.SaveChanges();
        }
    }
}

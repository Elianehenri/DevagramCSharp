using DevagramCSharp.Dtos;
using System.Net.Http.Headers;

namespace DevagramCSharp.Services
{
    public class CosmicService
    {
        public string EnviarImagem(ImagemDto imagemdto)
        {
            Stream imagem;

            imagem = imagemdto.Imagem.OpenReadStream();

            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "IiOMB9xXqbGLD4kK1KiUL3pB15P18kthHG3Nv0cg94FZ5o6jHb");

            var request = new HttpRequestMessage(HttpMethod.Post, "file");
            var conteudo = new MultipartFormDataContent
            {
                { new StreamContent(imagem), "media", imagemdto.Nome }
            };

            request.Content = conteudo;
            var retornoreq = client.PostAsync("https://workers.cosmicjs.com/v3/buckets/devaria2023-devagramcsharp/media", request.Content).Result;

            var urlretorno = retornoreq.Content.ReadFromJsonAsync<CosmicRepostaDto>();

            return urlretorno.Result.media.url;
        }
    }
}

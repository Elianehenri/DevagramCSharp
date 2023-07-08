using DevagramCSharp.Dtos;
using System.Net.Http.Headers;

namespace DevagramCSharp.Services
{
    public class CosmicService
    {
        public string EnviarImagem(ImagemDto imagemdto)
        {
            //transformar o arquivo em um stream
            Stream imagem;
         
            imagem = imagemdto.Imagem.OpenReadStream();
            //consumir um httpAPI
            var client = new HttpClient();
            //configurar o bearer token => o paramento é a chave do cosmic
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "IiOMB9xXqbGLD4kK1KiUL3pB15P18kthHG3Nv0cg94FZ5o6jHb");
            //criar o body da requisição
            var request = new HttpRequestMessage(HttpMethod.Post, "file");
            var conteudo = new MultipartFormDataContent
            {
                { new StreamContent(imagem), "media", imagemdto.Nome }
            };
            
            request.Content = conteudo;
            //retorno => o link da imagem,no cosmic
            var retornoreq = client.PostAsync("https://workers.cosmicjs.com/v3/buckets/devaria2023-devagramcsharp/media", request.Content).Result;

            var urlretorno = retornoreq.Content.ReadFromJsonAsync<CosmicRepostaDto>();

            return urlretorno.Result.media.url;
        }
    }
}

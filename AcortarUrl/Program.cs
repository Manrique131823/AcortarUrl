using System;
using System.IO.Pipes;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;


class Program
{

    private readonly HttpClient _httpClient;


    public Program(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    static async Task Main(string[] args)
    {

        using (HttpClient httpClient=new HttpClient())
        {
            Program program = new Program(httpClient);

            string url = "https://www.twitch.tv/baitybait";
            await program.AcortarUrl(url);
        }


    }


    public async Task AcortarUrl(string url)
    {

        var urlRequest = new UrlRequest
        {
            Url = url,
            Type = "direct",
            MetaTitle = "Prueba link google",
            MetaDescription = "prueba de acortador"

        };


        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await _httpClient.PostAsJsonAsync("https://sondevs.com/api/url/add", urlRequest);
        var responseRead= await  response.Content.ReadAsStringAsync();

        using (JsonDocument doc = JsonDocument.Parse(responseRead))
        {

            string urlacortada = doc.RootElement.GetProperty("shorturl").GetString();
            urlacortada = urlacortada.Replace("\\/", "/");

            Console.WriteLine(urlacortada);
            Console.Read();

        }



    }





    public class UrlRequest
    {

        public string Url { get; set; }
        public string Type { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }

    }



}
using System.Diagnostics;
using System.Xml;
using Newtonsoft.Json;

namespace SakerLabb.Web.Services;

public class ImportService
{
    private readonly ILogger<ImportService> _logger;
    private readonly HttpClient _http;

    public ImportService(ILogger<ImportService> logger, HttpClient http)
    {
        _logger = logger;
        _http = http;
    }

    public string ImportXml(string xml)
    {
        var settings = new XmlReaderSettings
        {
            // DTD är extra regler i XML som bland annat kan peka på filer eller andra adresser.
            // Förbjud DTD så att en användare inte kan få servern att läsa sådant den inte borde.
            DtdProcessing = DtdProcessing.Prohibit,
            // Tillåt inte XML-läsaren att hämta filer eller adresser utanför dokumentet.
            XmlResolver = null
        };

        // Dokumentet får inte heller slå upp externa resurser när XML-innehållet läses in.
        var document = new XmlDocument { XmlResolver = null };
        using var reader = XmlReader.Create(new StringReader(xml), settings);
        document.Load(reader);

        return document.DocumentElement?.InnerText ?? "";
    }

    public object? ImportJson(string json)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        return JsonConvert.DeserializeObject(json, settings);
    }

    public async Task<string> FetchRemote(string url)
    {
        _logger.LogInformation("Hämtar fjärresurs {Url}", url);
        var response = await _http.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }

    public string Ping(string host)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                // Kör ping direkt så att användarens text inte kan tolkas som Windows-kommandon.
                FileName = "ping.exe",
                RedirectStandardOutput = true,
                // Fånga även fel från ping så att de kan hanteras av appen.
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                // Separata argument hindrar värdnamnet från att bli en del av en kommandorad.
                ArgumentList = { "-n", "2", host }
            }
        };

        process.Start();
        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit(5000);
        return output;
    }
}

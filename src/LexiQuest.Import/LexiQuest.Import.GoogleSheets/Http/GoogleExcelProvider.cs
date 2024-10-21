using LexiQuest.Import.GoogleSheets.Config;
using LexiQuest.Import.GoogleSheets.Services;

namespace LexiQuest.Import.GoogleSheets.Http;


internal class GoogleExcelProvider(IHttpClientFactory httpClientFactory) : IGoogleExcelProvider
{
    public async Task<Stream> GetGoogleExcel(string url, CancellationToken cancellationToken)
    {
        using (var httpClient = httpClientFactory.CreateClient(Assemblies.GoogleImportAssembly.FullName))
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = await httpClient.SendAsync(requestMessage, cancellationToken);
                return await response.Content.ReadAsStreamAsync(cancellationToken);
            }
        }
    }
}
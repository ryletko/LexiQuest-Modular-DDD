using System.Net.Http;
using LexiQuest.WebApp.Shared.Services;

namespace LexiQuest.WebApp.Prerendering;

internal class DummyApiService: IApiService
{
    public HttpClient HttpClient { get; }
}
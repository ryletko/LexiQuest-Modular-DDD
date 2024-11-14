using System.Net.Http;
using LexiQuest.WebApp.Shared.Services;

namespace LexiQuest.WebApp.Services;

internal class DummyApiService: IApiService
{
    public HttpClient HttpClient { get; }
}
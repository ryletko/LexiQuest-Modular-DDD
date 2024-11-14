using System;
using System.Net.Http;
using LexiQuest.WebApp.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace LexiQuest.WebApp.Services;

internal class ApiService : IApiService
{
    public ApiService(HttpClient httpClient, NavigationManager navigationManager)
    {
        HttpClient             =   httpClient;
        HttpClient.BaseAddress ??= new Uri(navigationManager.BaseUri);
    }

    public HttpClient HttpClient { get; }
}
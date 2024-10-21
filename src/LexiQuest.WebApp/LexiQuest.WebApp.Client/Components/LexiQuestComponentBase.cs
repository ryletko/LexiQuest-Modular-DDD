using Microsoft.AspNetCore.Components;

namespace LexiQuest.WebApp.Client.Components;

public class LexiQuestComponentBase(): ComponentBase
{
    
    [Inject]
    protected NavigationManager Navigation { get; set; }
    
    protected bool IsPrerendering => !Navigation.Uri.Contains("_blazor");
    
}
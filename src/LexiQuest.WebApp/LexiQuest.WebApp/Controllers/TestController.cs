using System.Threading.Tasks;
using LexiQuest.WebApp.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace LexiQuest.WebApp.Controllers;

[ApiController]
public class TestController: BaseController
{

    [HttpPost("api/test")]
    public async Task Test()
    {
        
    } 
    
}
/*using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RoastHiveMvc.Models;

namespace RoastHiveMvc.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
*/

/*I commented this out since it looks like it's no longer needed 
   - see files under Pages/Account folder and _LoginPartial.cshtml
*/
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RoastHiveMvc.Controllers;

public class UsersController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = "Administrator")]
    public IActionResult Edit()
    {
        return View();
    }

}

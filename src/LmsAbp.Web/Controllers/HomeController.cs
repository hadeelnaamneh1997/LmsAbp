using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace LmsAbp.Web.Controllers;

public class HomeController : AbpController
{
    [HttpGet("/")]
    public IActionResult Root()
    {
        return RedirectToAction("Index", "Course");
    }

    public IActionResult Index()
    {
        return RedirectToAction("Index", "Course");
    }
}

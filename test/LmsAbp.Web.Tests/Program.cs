using Microsoft.AspNetCore.Builder;
using LmsAbp;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("LmsAbp.Web.csproj");
await builder.RunAbpModuleAsync<LmsAbpWebTestModule>(applicationName: "LmsAbp.Web" );

public partial class Program
{
}

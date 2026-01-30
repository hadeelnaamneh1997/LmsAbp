using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace LmsAbp.Pages;

public class Index_Tests : LmsAbpWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}

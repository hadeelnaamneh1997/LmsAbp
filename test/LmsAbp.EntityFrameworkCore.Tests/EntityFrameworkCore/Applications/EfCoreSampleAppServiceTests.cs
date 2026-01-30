using LmsAbp.Samples;
using Xunit;

namespace LmsAbp.EntityFrameworkCore.Applications;

[Collection(LmsAbpTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<LmsAbpEntityFrameworkCoreTestModule>
{

}

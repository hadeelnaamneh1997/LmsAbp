using LmsAbp.Samples;
using Xunit;

namespace LmsAbp.EntityFrameworkCore.Domains;

[Collection(LmsAbpTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<LmsAbpEntityFrameworkCoreTestModule>
{

}

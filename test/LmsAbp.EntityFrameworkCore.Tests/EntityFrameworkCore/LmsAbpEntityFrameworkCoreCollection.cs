using Xunit;

namespace LmsAbp.EntityFrameworkCore;

[CollectionDefinition(LmsAbpTestConsts.CollectionDefinitionName)]
public class LmsAbpEntityFrameworkCoreCollection : ICollectionFixture<LmsAbpEntityFrameworkCoreFixture>
{

}

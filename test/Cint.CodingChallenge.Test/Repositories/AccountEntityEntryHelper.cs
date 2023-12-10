using System.Linq;
using System.Threading.Tasks;
using Cint.CodingChallenge.Model.DBSet;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;

namespace Cint.CodingChallenge.Web.Test.Repositories;

public static class AccountEntityEntryHelper
{
    public static ValueTask<EntityEntry<Survey>> CreateEntityEntry(Survey e)
    {
        var stateManagerMock = new Mock<IStateManager>();
        var entityTypeMock = new Mock<IRuntimeEntityType>();
        entityTypeMock
            .SetupGet(_ => _.EmptyShadowValuesFactory)
            .Returns(() => new Mock<ISnapshot>().Object);
        entityTypeMock
            .SetupGet(_ => _.Counts)
            .Returns(new PropertyCounts(1, 1, 1, 1, 1, 1));
        entityTypeMock
            .Setup(_ => _.GetProperties())
            .Returns(Enumerable.Empty<IProperty>());
        var internalEntity = new InternalEntityEntry(stateManagerMock.Object,
            entityTypeMock.Object, e);
        var entry = new EntityEntry<Survey>(internalEntity);
        return ValueTask.FromResult(entry);
    }
}
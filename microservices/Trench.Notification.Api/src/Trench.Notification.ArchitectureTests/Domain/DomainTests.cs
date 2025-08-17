using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using Trench.Notification.ArchitectureTests.Infrastructure;
using Trench.Notification.Domain.SeedWorks;

namespace Trench.Notification.ArchitectureTests.Domain;

public class DomainTests : BaseTest
{
    [Fact]
    public void Entities_ShouldHave_PrivateParameterlessConstructor()
    {
        IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Entity))
            .GetTypes();

        var failingTypes = new List<Type>();
        foreach (var entityType in entityTypes)
        {
            var constructors = entityType.GetConstructors(BindingFlags.NonPublic |
                                                          BindingFlags.Instance);

            if (!constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0)) failingTypes.Add(entityType);
        }

        failingTypes.Should().BeEmpty();
    }
}
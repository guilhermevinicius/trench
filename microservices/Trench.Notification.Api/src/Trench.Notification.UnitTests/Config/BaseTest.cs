using Bogus;

namespace Trench.Notification.UnitTests.Config;

public abstract class BaseTest
{
    protected Faker Faker = new("pt_BR");
}
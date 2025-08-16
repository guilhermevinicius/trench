using Bogus;

namespace Trench.Notification.FunctionalTests.Config;

public abstract class BaseTest
{
    protected Faker Faker = new("pt_BR");
}
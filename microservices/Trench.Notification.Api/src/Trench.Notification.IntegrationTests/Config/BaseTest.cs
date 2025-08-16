using Bogus;

namespace Trench.User.IntegrationTests.Config;

public abstract class BaseTest
{
    protected Faker Faker = new("pt_BR");
}
using Bogus;

namespace Trench.User.FunctionalTests.Config;

public abstract class BaseTest
{
    protected Faker Faker = new("pt_BR");
}
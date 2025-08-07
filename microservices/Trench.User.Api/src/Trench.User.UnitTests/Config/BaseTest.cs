using Bogus;

namespace Trench.User.UnitTests.Config;

public abstract class BaseTest
{
    protected Faker Faker = new("pt_BR");
}
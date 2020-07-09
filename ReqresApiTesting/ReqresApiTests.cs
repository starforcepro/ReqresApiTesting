using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using System.Net;

namespace ReqresApiTesting
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ReqresApiTests
    {
        private ReqresApi reqresApi => new ReqresApi();

        [Test]
        public void AuthorizeWhenUserExists()
        {
            var actual = new ReqresApi().Authorize("peter@klaven", "cityslicka");

            using (new AssertionScope())
            {
                actual.Result.Should().Be("{\"token\":\"QpwL5tke4Pnpja7X\"}");
                actual.Code.Should().Be(HttpStatusCode.OK);
            }
        }

        [Test]
        [TestCase("someUser", "somePassword")]
        [TestCase("peter@klaven", "somePassword")]
        public void AuthorizeWhenUserDoesntExistOrIncorrectPassword(string email, string password)
        {
            var actual = reqresApi.Authorize(email, password);

            using (new AssertionScope())
            {
                actual.Result.Should().Be("{\"error\":\"Incorrect email/username or password\"}");
                actual.Code.Should().Be(HttpStatusCode.Forbidden);
            }
        }

        [Test]
        public void AuthorizeWhenPasswordIsMissing()
        {
            var actual = reqresApi.Authorize("peter@klaven", null);

            using (new AssertionScope())
            {
                actual.Result.Should().Be("{\"error\":\"Missing password\"}");
                actual.Code.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        [Test]
        public void AuthorizeWhenEmailIsMissing()
        {
            var actual = reqresApi.Authorize(null, "somePassword");

            using (new AssertionScope())
            {
                actual.Result.Should().Be("{\"error\":\"Missing email or username\"}");
                actual.Code.Should().Be(HttpStatusCode.BadRequest);
            }
        }
    }
}

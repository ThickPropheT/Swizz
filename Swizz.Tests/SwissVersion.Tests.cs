using Swizz;

namespace Swizz.Tests
{

    [TestFixture]
    public class SwissVersionTests
    {
        [Test]
        public void Should_indicate_is_empty_when_constructed_with_default_constructor()
        {
            Assert.That(new SwissVersion().IsEmpty, Is.True);
        }

        [TestCase(typeof(FormatException), "vr")]
        [TestCase(typeof(FormatException), "vNNNrNNN")]
        [TestCase(typeof(FormatException), "v###r###")]
        [TestCase(typeof(FormatException), "v1r1")]
        [TestCase(typeof(FormatException), "vN.NrN")]
        [TestCase(typeof(FormatException), "v1.1")]
        public void Should_fail_to_parse_invalid_version_string(Type exceptionType, string versionString)
        {
            var ex = Assert.Throws(exceptionType, () => SwissVersion.Parse(versionString));
        }

        [TestCase("vr")]
        [TestCase("vNNNrNNN")]
        [TestCase("v###r###")]
        [TestCase("v1r1")]
        [TestCase("vN.NrN")]
        [TestCase("v1.1")]
        public void Should_fail_to_parse_invalid_version_string(string versionString)
        {
            var didSucceed = SwissVersion.TryParse(versionString, out var result);

            Assert.Multiple(() =>
            {
                Assert.That(didSucceed, Is.False);
                Assert.That(result, Is.EqualTo(default(SwissVersion)));
            });
        }

        [Test]
        public void Should_successfully_parse_valid_version_string()
        {
            var version = SwissVersion.Parse("v1.1r1");

            Assert.Multiple(() =>
            {
                Assert.That(version.Major, Is.EqualTo(1));
                Assert.That(version.Minor, Is.EqualTo(1));
                Assert.That(version.Revision, Is.EqualTo(1));
            });
        }

        [Test]
        public void Should_print_to_string()
        {
            var version = new SwissVersion(1, 1, 1).ToString();

            Assert.That(version, Is.EqualTo("v1.1r1"));
        }

        // TODO add tests for comparison operators
    }
}

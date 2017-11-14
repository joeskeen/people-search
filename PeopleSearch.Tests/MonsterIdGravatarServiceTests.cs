using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearch.Services;

namespace PeopleSearch.Tests
{
    [TestClass]
    public class MonsterIdGravatarServiceTests
    {
        private const string DefaultAvatarUrl = "http://www.gravatar.com/avatar/d41d8cd98f00b204e9800998ecf8427e?s=250&r=g&d=monsterid";

        [TestMethod]
        public void NullPerson_ReturnsDefaultAvatarUrl()
        {
            var service = new MonsterIdGravatarService();
            var url = service.GetAvatarUrl(null);
            Assert.AreEqual(DefaultAvatarUrl, url);
        }

        [TestMethod]
        public void NullEmail_ReturnsDefaultAvatarUrl()
        {
            var service = new MonsterIdGravatarService();
            var url = service.GetAvatarUrl(new Data.Person { EmailAddress = null });
            Assert.AreEqual(DefaultAvatarUrl, url);
        }

        [TestMethod]
        public void EmptyEmail_ReturnsDefaultAvatarUrl()
        {
            var service = new MonsterIdGravatarService();
            var url = service.GetAvatarUrl(new Data.Person { EmailAddress = string.Empty });
            Assert.AreEqual(DefaultAvatarUrl, url);
        }

        [TestMethod]
        public void WhitespaceEmail_ReturnsDefaultAvatarUrl()
        {
            var service = new MonsterIdGravatarService();
            var url = service.GetAvatarUrl(new Data.Person { EmailAddress = "   \t   \r\n    " });
            Assert.AreEqual(DefaultAvatarUrl, url);
        }

        private const string TestAtTestDotCom = "test@test.com";
        private const string TestAtTestDotComUrl = "http://www.gravatar.com/avatar/b642b4217b34b1e8d3bd915fc65c4452?s=250&r=g&d=monsterid";

        [TestMethod]
        public void NormalEmail_ReturnsCorrectAvatarUrl()
        {
            var service = new MonsterIdGravatarService();
            var url = service.GetAvatarUrl(new Data.Person { EmailAddress = TestAtTestDotCom });
            Assert.AreEqual(TestAtTestDotComUrl, url);
        }

        [TestMethod]
        public void UppercaseEmail_ReturnsCorrectAvatarUrl()
        {
            var service = new MonsterIdGravatarService();
            var url = service.GetAvatarUrl(new Data.Person { EmailAddress = TestAtTestDotCom.ToUpper() });
            Assert.AreEqual(TestAtTestDotComUrl, url);
        }

        [TestMethod]
        public void NormalEmailWithWhitespace_ReturnsCorrectAvatarUrl()
        {
            var service = new MonsterIdGravatarService();
            var url = service.GetAvatarUrl(new Data.Person { EmailAddress = $" \t {TestAtTestDotCom} \r\n  \t " });
            Assert.AreEqual(TestAtTestDotComUrl, url);
        }
    }
}

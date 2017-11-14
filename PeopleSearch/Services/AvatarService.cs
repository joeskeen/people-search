using PeopleSearch.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PeopleSearch.Services
{
    public interface IAvatarService
    {
        string GetAvatarUrl(Person p);
    }

    public class MonsterIdGravatarService: IAvatarService
    {
        public const int AvatarSize = 250;

        public string GetAvatarUrl(Person p)
        {
            string email = p?.EmailAddress ?? string.Empty;
            string emailHash;
            using (var md5 = MD5.Create())
            {
                emailHash = md5.ComputeHash(Encoding.ASCII.GetBytes(email.ToLower().Trim()))
                    .Aggregate(new StringBuilder(), (sb, b) => { sb.Append(b.ToString("X2")); return sb; })
                    .ToString().ToLower();
            }
            var imageUrl = string.Format("http://www.gravatar.com/avatar/{0}?s={1}&r=g&d=monsterid", emailHash, AvatarSize);
            return imageUrl;
        }
    }
}

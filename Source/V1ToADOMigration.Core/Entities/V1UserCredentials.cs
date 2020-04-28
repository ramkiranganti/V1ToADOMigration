using V1ToADOMigration.Core.Helpers;

namespace V1ToADOMigration.Core.Entities
{
    public class V1UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public V1UserCredentials()
        {
            Username = ConfigHelper.V1Username;
            Password = ConfigHelper.V1Password;
        }
        public V1UserCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}

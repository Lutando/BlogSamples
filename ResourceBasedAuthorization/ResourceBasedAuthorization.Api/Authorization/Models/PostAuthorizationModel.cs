using ResourceBasedAuthorization.Models.Entities;

namespace ResourceBasedAuthorization.Api.Authorization.Models
{
    public class PostAuthorizationModel
    {
        public string Username { get; private set; }

        private PostAuthorizationModel(string username)
        {
            Username = username;
        }

        public static PostAuthorizationModel From(Post resource)
        {
            return new PostAuthorizationModel(resource.Username);
        }
    }
}

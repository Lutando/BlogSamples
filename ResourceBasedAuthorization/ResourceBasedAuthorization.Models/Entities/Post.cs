using System;

namespace ResourceBasedAuthorization.Models.Entities
{
    public class Post
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Text { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        private Post(Guid id, string username, string text, DateTime created, DateTime modified)
        {
            //for brevity we skip domain validation
            Id = id;
            Username = username;
            Text = text;
            Created = created;
            Modified = created;
        }

        public Post(string username, string text)
            :this(Guid.NewGuid(),username,text,DateTime.UtcNow, DateTime.UtcNow)
        {
            
        }
        
        public void EditPost(string text)
        {
            Text = text;
            Modified = DateTime.Now;
        }

        
    }
}

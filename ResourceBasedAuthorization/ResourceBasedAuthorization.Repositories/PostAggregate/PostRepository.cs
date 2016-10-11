using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResourceBasedAuthorization.Models.Entities;
using ResourceBasedAuthorization.Models.Interfaces;

namespace ResourceBasedAuthorization.Repositories.PostAggregate
{
    public class PostRepository : IPostRepository
    {
        //We wont explicitly define the methods for the worked examples. Just assume that they work as described :)
        public Task AddAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Post post)
        {
            throw new NotImplementedException();
        }
    }
}

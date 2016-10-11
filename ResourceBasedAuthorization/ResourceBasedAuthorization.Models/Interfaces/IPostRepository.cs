using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResourceBasedAuthorization.Models.Entities;

namespace ResourceBasedAuthorization.Models.Interfaces
{
    public interface IPostRepository
    {
        Task AddAsync(Post post);
        Task<Post> GetAsync(Guid id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task UpdateAsync(Post post);
        Task DeleteAsync(Post post);
    }
}

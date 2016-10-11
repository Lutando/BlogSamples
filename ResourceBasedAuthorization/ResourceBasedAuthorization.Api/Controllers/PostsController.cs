using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourceBasedAuthorization.Models.Interfaces;

namespace ResourceBasedAuthorization.Api.Controllers
{
    public class PostsController
    {
        private IPostRepository PostRepository { get; set; }

        public PostsController(IPostRepository postRepository)
        {
            PostRepository = postRepository;
        }
        [HttpGet(Name = "GetAllPosts")]
        [Authorize]
        public async Task<IActionResult> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{postId:Guid}", Name = "GetPost")]
        [Authorize]
        public async Task<IActionResult> GetPost(Guid post)
        {
            throw new NotImplementedException();
        }


    }
}

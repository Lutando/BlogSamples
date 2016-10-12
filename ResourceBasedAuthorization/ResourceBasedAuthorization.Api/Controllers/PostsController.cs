using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using ResourceBasedAuthorization.Api.Authorization.Models;
using ResourceBasedAuthorization.Api.Authorization.Operations;
using ResourceBasedAuthorization.Api.Models;
using ResourceBasedAuthorization.Models.Entities;
using ResourceBasedAuthorization.Models.Interfaces;

namespace ResourceBasedAuthorization.Api.Controllers
{
    [Route("api/posts")]
    public class PostsController : Controller
    {
        private IPostRepository PostRepository { get; set; }
        private IAuthorizationService AuthorizationService { get; set; }
        public PostsController(IPostRepository postRepository, IAuthorizationService authorizationService)
        {
            PostRepository = postRepository;
            AuthorizationService = authorizationService;
        }
        [HttpGet(Name = "GetAllPosts")]
        [Authorize]
        public async Task<IActionResult> GetAllPosts()
        {
            //no need for Resource Based AuthZ here
            var posts = await PostRepository.GetAllAsync();

            return Json(posts);
        }

        [HttpGet("{postId:Guid}", Name = "GetPost")]
        [Authorize]
        public async Task<IActionResult> GetPost(Guid postId)
        {
            //no need for Resource Based AuthZ here
            var post = await PostRepository.GetAsync(postId);

            return Json(post);
        }

        [HttpPost(Name = "PostPost")]
        [Authorize]
        public async Task<IActionResult> PostPost([FromBody] PostInputModel post)
        {
            var username = GetUsername();

            var entity = new Post(username, post.Text);

            var authZModel = PostAuthorizationModel.From(entity);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, PostOperations.Write))
            {
                //user is not authorized to apply PostOperations.Write on the resource
                //in this case this just means that the user is not authorized period.
                return new ForbidResult();
            }

            //store the new resource
            await PostRepository.AddAsync(entity);

            //return 201
            return CreatedAtAction("GetPost", new {postId = entity.Id}, entity);
        }

        [HttpDelete(Name = "DeletePost")]
        [Authorize]
        public async Task<IActionResult> DeletePost(Guid postId)
        {

            var post = await PostRepository.GetAsync(postId);

            //404 if not found
            if (post == null)
            {
                return NotFound();
            }

            var authZModel = PostAuthorizationModel.From(post);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, PostOperations.Delete))
            {
                //user is not authorized to apply PostOperations.Delete on the resource
                return new ForbidResult();
            }

            //store the new resource
            await PostRepository.DeleteAsync(post);

            //return 200
            return Ok();
        }


        //PATCH for editing part of the post resource (only the text)
        [HttpPatch("{",Name = "EditPost")]
        [Authorize]
        public async Task<IActionResult> EditPost([FromBody]PostInputModel data, Guid postId)
        {

            var post = await PostRepository.GetAsync(postId);

            //404 if not found
            if (post == null)
            {
                return NotFound();
            }

            var authZModel = PostAuthorizationModel.From(post);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, PostOperations.Edit))
            {
                //user is not authorized to apply PostOperations.Edit on the resource
                return new ForbidResult();
            }
            //modify the resource
            post.EditPost(data.Text);
            //update the new resource
            await PostRepository.UpdateAsync(post);

            //return 204
            return NoContent();
        }

        public string GetUsername()
        {
            return HttpContext.User.Claims.Single(x => x.Type == "username").Value;
        }


    }
}

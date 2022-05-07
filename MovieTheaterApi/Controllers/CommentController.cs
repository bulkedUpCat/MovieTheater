using BLL.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieTheaterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _commentService.GetAllCommentsAsync();
        }


        [HttpGet("{id}")]
        public async Task<IEnumerable<Comment>> GetComments(int id)
        {
            return await _commentService.GetCommentsByMovieId(id);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> AddComment(CommentDTO comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            var result = await _commentService.AddCommentAsync(comment);

            if (result == null)
            {
                return BadRequest();
            }

            return result;
        }
    }
}

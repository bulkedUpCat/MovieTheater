using BLL.Services;
using BLL.Validators;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieTheaterApi.Controllers
{
    [Route("api/comments")]
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


        [HttpGet("movies/{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _commentService.DeleteCommentAsync(id);
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}

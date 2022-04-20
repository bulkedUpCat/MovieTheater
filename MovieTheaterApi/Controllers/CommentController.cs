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
        public IEnumerable<Comment> GetAllComments()
        {
            return _commentService.GetAllComments();
        }


        [HttpGet("{id}")]
        public IEnumerable<Comment> GetComments(int id)
        {
            return _commentService.GetCommentsByMovieId(id);
        }

        [HttpPost]
        public ActionResult<Comment> AddComment(CommentDTO comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            var result =_commentService.AddComment(comment);

            if (result == null)
            {
                return BadRequest();
            }

            return result;
        }
    }
}

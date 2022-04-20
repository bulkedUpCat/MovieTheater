using Core.DTOs;
using Core.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommentService
    {
        private readonly UnitOfWork _unitOfWork;

        public CommentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _unitOfWork.CommentRepository.Get();
        }

        public Comment GetCommentById(int id)
        {
            return _unitOfWork.CommentRepository.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Comment> GetCommentsByMovieId(int movieId)
        {
            var comments = _unitOfWork.CommentRepository.Where(c => c.MovieId == movieId).ToList();
            return comments;
        }

        public Comment AddComment(CommentDTO commentDTO)
        {
            if (commentDTO == null)
            {
                return null;
            }

            var user = _unitOfWork.UserRepository.GetByID(commentDTO.UserId);

            var comment = new Comment()
            {
                UserName = user.Name,
                UserId = commentDTO.UserId,
                MovieId = commentDTO.MovieId,
                Text = commentDTO.Text
            };

            try
            {
                _unitOfWork.CommentRepository.Insert(comment);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }

            return comment;
        }
    }
}

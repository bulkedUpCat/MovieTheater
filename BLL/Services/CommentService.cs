using AutoMapper;
using Core.DTOs;
using Core.Models;
using DAL.Abstractions.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _unitOfWork.CommentRepository.GetAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _unitOfWork.CommentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByMovieId(int movieId)
        {
            var comments = await _unitOfWork.CommentRepository.GetByMovieIdAsync(movieId);
            return comments;
        }

        public async Task<Comment> AddCommentAsync(CommentDTO commentDTO)
        {
            if (commentDTO == null)
            {
                return null;
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(commentDTO.UserId);

            if (user == null)
            {
                return null;
            }

            var comment = _mapper.Map<Comment>(commentDTO);
            comment.UserName = user.UserName;

            /*var comment = new Comment()
            {
                UserName = user.UserName,
                UserId = commentDTO.UserId,
                MovieId = commentDTO.MovieId,
                Text = commentDTO.Text
            };*/

            try
            {
                await _unitOfWork.CommentRepository.InsertAsync(comment);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }

            return comment;
        }
    }
}

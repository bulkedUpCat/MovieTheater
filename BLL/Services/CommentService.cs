using AutoMapper;
using BLL.Abstractions.Interfaces;
using BLL.Validators;
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
    public class CommentService : ICommentService
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
            IEnumerable<Comment> comments = await _unitOfWork.CommentRepository.GetByMovieIdAsync(movieId);
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

        public async Task DeleteCommentAsync(int id)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(id);

            if (comment == null)
            {
                throw new MovieException("Comment not found");
            }

            try
            {
                _unitOfWork.CommentRepository.Delete(comment);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                throw new MovieException(e.Message);
            }
        }
    }
}

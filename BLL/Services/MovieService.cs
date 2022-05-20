using AutoMapper;
using BLL.Abstractions.Interfaces;
using BLL.Email;
using BLL.Validators;
using Core.DTOs;
using Core.Helpers;
using Core.Models;
using DAL.Abstractions.Interfaces;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;

namespace BLL.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly EmailSender _emailSender;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            EmailSender emailSender,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            var movies = await _unitOfWork.MovieRepository.GetAsync();
            return movies;
        }

        public async Task<PagedList<MovieDTO>> GetPagedMoviesAsync(MovieParameters movieParameters)
       {
            var movies = await _unitOfWork.MovieRepository.GetPagedMovies();

            var user = await _userManager.FindByEmailAsync(movieParameters.UserEmail);

            if (user == null && string.IsNullOrEmpty(movieParameters.UserEmail))
            {
                throw new MovieException("The user with this email doesn't exist");
            }

            var moviesDTO = _mapper.Map<IEnumerable<MovieDTO>>(movies);
            var userDTO = _mapper.Map<UserDTO>(user);

            List<MovieDTO> newMovies = new List<MovieDTO>();
            int count;

            foreach (var movie in moviesDTO)
            {
                count = 0;

                foreach (var parameter in movieParameters.Genres)
                {
                    foreach (var genre in movie.Genres)
                    {
                        if (genre.Name == parameter) count++;

                    }
                }

                if (count == movieParameters.Genres.Count)
                {
                    newMovies.Add(movie);
                }
            }

            newMovies = newMovies
                .Where(m => movieParameters.Years.Contains(m.ReleaseDate.ToString()) || movieParameters.Years.Count == 0)
                .Where(m => movieParameters.Runtime.Any(r => m.RuntimeHours < r) || movieParameters.Runtime.Count == 0)
                .OrderByDescending(m => m.WhenAdded)
                .ToList();

            if (movieParameters.WatchLater && !string.IsNullOrEmpty(movieParameters.UserEmail))
            {
                newMovies = newMovies.Where(m => m.WatchLaterUsers.Any(u => u.Id == userDTO.Id)).ToList();
            }
            else if (movieParameters.FavoriteList && !string.IsNullOrEmpty(movieParameters.UserEmail))
            {
                newMovies = newMovies.Where(m => m.FavoriteListUsers.Any(u => u.Id == userDTO.Id)).ToList();
            }

            if (!string.IsNullOrEmpty(movieParameters.SearchString))
            {
                newMovies = newMovies.Where(m => m.Title.Contains(movieParameters.SearchString.ToUpper())).ToList();
            }

            if (movieParameters.PageSize == 0)
            {
                movieParameters.PageSize = newMovies.Count;
            }

            var pagedList = PagedList<MovieDTO>.ToPagedList(newMovies, movieParameters.PageNumber, movieParameters.PageSize);

            return pagedList;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _unitOfWork.MovieRepository.GetByIdAsync(id);

            if (movie == null)
            {
                throw new MovieException("Movie doesn't exist");
            }

            return movie;
        }

        public async Task<bool> AddMovieAsync(AddMovieDTO movieDTO)
        {
            if (movieDTO == null)
            {
                return false;
            }

            var movie = new Movie()
            {
                Title = movieDTO.Title,
                Description = movieDTO.Description,
                ReleaseDate = movieDTO.ReleaseDate,
                Director = movieDTO.Director,
                RuntimeHours = movieDTO.RuntimeHours,
                Image = $"{string.Join("",movieDTO.Title.Split(" "))}.jpg",
                TrailerUrl = movieDTO.TrailerUrl,
            };

            var genres = await _unitOfWork.MovieGenreRepository.GetAsync(mg => movieDTO.Genres.Contains(mg.Name));

            movie.Genres = (ICollection<MovieGenre>) genres;

            try
            {
                await _unitOfWork.MovieRepository.InsertAsync(movie);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            } 

            return true;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _unitOfWork.MovieRepository.GetByIdAsync(id);

            if (movie == null)
            {
                return false;
            }

            try
            {
                _unitOfWork.MovieRepository.Delete(movie);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new MovieException(ex.Message);
            }
            
            return true;
        }

        public async Task<bool> UpdateMovieAsync (Movie updatedMovie)
        {
            if (updatedMovie == null)
            {
                return false;
            }

            try
            {
                _unitOfWork.MovieRepository.Update(updatedMovie);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CreateReport(MovieParameters movieParameters)
        {
            var movies = await GetPagedMoviesAsync(movieParameters);

            if (movies == null)
            {
                throw new MovieException("Movies not found");
            }

            var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            float[] pointColumnWidths = { 300F, 100F, 100F };

            Table table = new Table(pointColumnWidths);
            table.AddCell(new Cell().Add(new Paragraph("Title").SetFontColor(ColorConstants.RED)));
            table.AddCell(new Cell().Add(new Paragraph("Year").SetFontColor(ColorConstants.RED)));
            table.AddCell(new Cell().Add(new Paragraph("Runtime (hours)").SetFontColor(ColorConstants.RED)));

            foreach(var movie in movies)
            {
                table.AddCell(new Cell().Add(new Paragraph(movie.Title)));
                table.AddCell(new Cell().Add(new Paragraph(movie.ReleaseDate.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(movie.RuntimeHours.ToString())));
            }

            document.Add(table);

            document.Close();
            MemoryStream pdfStream = new MemoryStream(stream.ToArray());

            try
            {
                _emailSender.SendSmtpMail(new EmailTemplate()
                {
                    To = movieParameters.UserEmail,
                    Subject = "Movie Report",
                    Body = "Here is you pdf report",
                    Attachment = new Attachment(pdfStream, "report.pdf")
                });
            }
            catch (Exception e)
            {
                throw new MovieException(e.Message);
            }
            finally
            {
                stream.Close();
                pdfStream.Close();
                writer.Close();
            }

            return true;
        }
    }
}

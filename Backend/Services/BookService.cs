using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Dtos;
using Backend.Dtos.BookDtos;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly UserManager<AppUser> _userManager;

        public BookService(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository, IGenreRepository genreRepository,UserManager<AppUser> userManager)
        {
            this._bookRepository = bookRepository;
            this._mapper = mapper;
            this._authorRepository = authorRepository;
            this._genreRepository = genreRepository;
            this._userManager = userManager;
        }
        public async Task<BookDto?> AddBookAsync(CreateBookDto bookDto, string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var userRole = await _userManager.GetRolesAsync(user);
                if (userRole.Count == 0 || userRole[0] != "Admin")
                {
                    throw new UnauthorizedAccessException("Only administrators can add authors");
                }
                var mappedBook = _mapper.Map<Book>(bookDto);
                var bookForReturn = await _bookRepository.AddBookAsync(mappedBook);
                if (bookForReturn is not null)
                {
                    bookForReturn.BookGenres ??= new List<BookGenre>();
                    foreach (int genreId in bookDto.GenreIds)
                    {
                        if (await _genreRepository.GenreExist(genreId))
                        {
                            var bookgenre = new BookGenre
                            {
                                GenreId = genreId,
                                BookId = mappedBook.Id

                            };
                            bookForReturn.BookGenres.Add(bookgenre);
                        }
                    }
                    await _bookRepository.UpdateBookAsync(bookForReturn);
                }
                return _mapper.Map<BookDto>(bookForReturn);
            }
            catch (Exception)
            {
                throw ;
            }
        }
        public async Task<BookDto?> DeleteBookAsync(int id, string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var userRole = await _userManager.GetRolesAsync(user);
                if (userRole.Count == 0 || userRole[0] != "Admin")
                {
                    throw new UnauthorizedAccessException("Only administrators can add authors");
                }                
                var bookToDelete = await _bookRepository.GetBookByIdAsync(id) ?? throw new KeyNotFoundException();
                var deletedBook = _mapper.Map<BookDto>(await _bookRepository.DeleteBookAsync(bookToDelete));
                return deletedBook;
            }
            catch (Exception)
            {
                throw;
            }
        }
        

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            try
            {
                return _mapper.Map<List<BookDto>>(await _bookRepository.GetAllBooksAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(id) ?? throw new KeyNotFoundException();
                return _mapper.Map<BookDto>(book);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookDto?>> GetBooksByAuthorAsync(int authorId)
        {
            try
            {
                var author = await _authorRepository.GetAuthorById(authorId) ?? throw new  KeyNotFoundException();
                var books = await _bookRepository.GetBooksByAuthorAsync(author);
                return _mapper.Map<List<BookDto?>>(books);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<List<BookDto?>> GetBooksByGenreAsync(int genreId)
        {
            try{
            var genre = await _genreRepository.GetGenreById(genreId) ?? throw new KeyNotFoundException();
                var books = await _bookRepository.GetBooksByGenreAsync(genre);
            return _mapper.Map<List<BookDto?>>(books);
            }
            catch (Exception)
            {
                throw;
            }
        }

       

        public async Task<BookDto?> UpdateBookAsync(UpdateBookDto bookDto, int Id, string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var userRole = await _userManager.GetRolesAsync(user);
                if (userRole.Count == 0 || userRole[0] != "Admin")
                {
                    throw new UnauthorizedAccessException("Only administrators can add authors");
                }                
                var existingBook = await _bookRepository.GetBookByIdAsync(Id) ?? throw new KeyNotFoundException();
                var bookToUpdate = _mapper.Map<Book>(bookDto);
                bookToUpdate.Id=Id;
                var updatedBook = await _bookRepository.UpdateBookAsync(bookToUpdate);
                return _mapper.Map<BookDto>(updatedBook); 
            }
            catch (Exception)
            {
                throw; 
            }
        }

       
    }
}
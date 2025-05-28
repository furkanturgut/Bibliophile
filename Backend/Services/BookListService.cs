using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Dtos.BookListDtos;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{
    public class BookListService : IBookListService
    {
        private readonly IBookListRepository _bookListRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public BookListService(IBookListRepository bookListRepository,IBookRepository bookRepository,IMapper mapper, UserManager<AppUser> userManager)
        {
            _bookListRepository = bookListRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<List<BookListDto>> GetAllBookListsAsync()
        {
            try
            {
                var bookLists = await _bookListRepository.GetAllBookListsAsync();
                return _mapper.Map<List<BookListDto>>(bookLists);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookListDto?> GetBookListByIdAsync(int bookId)
        {
            try
            {
                var bookList = await _bookListRepository.GetBookListByIdAsync(bookId);
                if (bookList == null)
                    throw new KeyNotFoundException();
                    
                return _mapper.Map<BookListDto>(bookList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookListDto>> GetBookListsByUserIdAsync(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var bookLists = await _bookListRepository.GetBookListsByUserIdAsync(user.Id);
                return _mapper.Map<List<BookListDto>>(bookLists);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookListDto> CreateBookListAsync(CreateBookListDto createBookListDto, string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var bookList = _mapper.Map<BookList>(createBookListDto);
                bookList.UserId = user.Id;
                bookList.CreatedAt = DateTime.Now;
                bookList.UpdatedAt = DateTime.Now;
                
                var createdBookList = await _bookListRepository.AddBookListAsync(bookList);
                return _mapper.Map<BookListDto>(createdBookList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookListDto?> UpdateBookListAsync(int id, UpdateBookListDto updateBookListDto)
        {
            try
            {
                var bookList = await _bookListRepository.GetBookListByIdAsync(id);
                if (bookList == null)
                    return null;
                    
                _mapper.Map(updateBookListDto, bookList);
                bookList.UpdatedAt = DateTime.Now;
                
                var updatedBookList = await _bookListRepository.UpdateBookListAsync(bookList);
                return _mapper.Map<BookListDto>(updatedBookList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookListDto?> DeleteBookListAsync(int bookId)
        {
            try
            {
                var bookList = await _bookListRepository.GetBookListByIdAsync(bookId);
                if (bookList == null)
                    throw new KeyNotFoundException();
                    
                var deletedBookList = await _bookListRepository.DeleteBookListAsync(bookList);
                return _mapper.Map<BookListDto>(deletedBookList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookListDto>> GetPopularBookListsAsync(int count)
        {
            try
            {
                var bookLists = await _bookListRepository.GetPopularBookListsAsync(count);
                return _mapper.Map<List<BookListDto>>(bookLists);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> BookListExistsAsync(int id)
        {
            try
            {
                return await _bookListRepository.BookListExistsAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsUserListOwnerAsync(int listId, string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var bookList = await _bookListRepository.GetBookListByIdAsync(listId);
                return bookList != null && bookList.UserId == user.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookListDto> AddBookToListAsync(int listId, AddBookToListDto addBookToListDto)
        {
            try
            {
                var bookList = await _bookListRepository.GetBookListByIdAsync(listId);
                if (bookList == null)
                    throw new KeyNotFoundException($"ID {listId} ile kitap listesi bulunamadı");
                    
                var book = await _bookRepository.GetBookByIdAsync(addBookToListDto.BookId);
                if (book == null)
                    throw new KeyNotFoundException($"ID {addBookToListDto.BookId} ile kitap bulunamadı");
                
                var bookOfList = new BooksOfList
                {
                    ListId = listId,
                    BookId = addBookToListDto.BookId
                };
                
                await _bookListRepository.AddBookToListAsync(bookOfList);
                
                // Güncellenmiş listeyi getir
                var updatedBookList = await _bookListRepository.GetBookListByIdAsync(listId);
                return _mapper.Map<BookListDto>(updatedBookList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookListDto?> RemoveBookFromListAsync(int listId, int bookId)
        {
            try
            {
                await _bookListRepository.RemoveBookFromListAsync(listId, bookId);
                
                // Güncellenmiş listeyi getir
                var updatedBookList = await _bookListRepository.GetBookListByIdAsync(listId);
                if (updatedBookList == null)
                    throw new KeyNotFoundException();
                    
                return _mapper.Map<BookListDto>(updatedBookList);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.BookListDtos;

namespace Backend.Interface
{
    public interface IBookListService
    {
        
        Task<List<BookListDto>> GetAllBookListsAsync();
        
       
        Task<BookListDto?> GetBookListByIdAsync(int bookId);
        
        Task<List<BookListDto>> GetBookListsByUserIdAsync(string userName);
        
        Task<BookListDto> CreateBookListAsync(CreateBookListDto createBookListDto, string userName);
        
        Task<BookListDto?> UpdateBookListAsync(int id, UpdateBookListDto updateBookListDto);
        
      
        Task<BookListDto?> DeleteBookListAsync(int bookId);
        
       
        Task<List<BookListDto>> GetPopularBookListsAsync(int count);
        
      
        Task<bool> BookListExistsAsync(int id);
        
      
        Task<bool> IsUserListOwnerAsync(int listId, string userName);
        
       
        Task<BookListDto> AddBookToListAsync(int listId, AddBookToListDto addBookToListDto);
        
        
        Task<BookListDto?> RemoveBookFromListAsync(int listId, int bookId);
    }
}
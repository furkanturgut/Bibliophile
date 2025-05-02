using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.BookListDtos;

namespace Backend.Interface
{
    public interface IBookListService
    {
        /// <summary>
        /// Tüm kitap listelerini getirir
        /// </summary>
        Task<List<BookListDto>> GetAllBookListsAsync();
        
        /// <summary>
        /// ID'ye göre kitap listesi getirir
        /// </summary>
        Task<BookListDto?> GetBookListByIdAsync(int id);
        
        /// <summary>
        /// Kullanıcıya göre kitap listelerini getirir
        /// </summary>
        Task<List<BookListDto>> GetBookListsByUserIdAsync(string userId);
        
        /// <summary>
        /// Yeni bir kitap listesi oluşturur
        /// </summary>
        Task<BookListDto> CreateBookListAsync(CreateBookListDto createBookListDto, string userId);
        
        /// <summary>
        /// Var olan bir kitap listesini günceller
        /// </summary>
        Task<BookListDto?> UpdateBookListAsync(int id, UpdateBookListDto updateBookListDto);
        
        /// <summary>
        /// Bir kitap listesini siler
        /// </summary>
        Task<BookListDto?> DeleteBookListAsync(int id);
        
        /// <summary>
        /// Popüler kitap listelerini getirir
        /// </summary>
        Task<List<BookListDto>> GetPopularBookListsAsync(int count);
        
        /// <summary>
        /// Liste varlığını kontrol eder
        /// </summary>
        Task<bool> BookListExistsAsync(int id);
        
        /// <summary>
        /// Kullanıcının liste sahibi olup olmadığını kontrol eder
        /// </summary>
        Task<bool> IsUserListOwnerAsync(int listId, string userId);
        
        /// <summary>
        /// Listeye kitap ekler
        /// </summary>
        Task<BookListDto> AddBookToListAsync(int listId, AddBookToListDto addBookToListDto);
        
        /// <summary>
        /// Listeden kitap çıkarır
        /// </summary>
        Task<BookListDto?> RemoveBookFromListAsync(int listId, int bookId);
    }
}
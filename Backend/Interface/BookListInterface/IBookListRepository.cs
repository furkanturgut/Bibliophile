using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interface
{
    public interface IBookListRepository
    {
        /// <summary>
        /// Tüm kitap listelerini getirir
        /// </summary>
        Task<List<BookList>> GetAllBookListsAsync();
        
        /// <summary>
        /// ID'ye göre kitap listesi getirir
        /// </summary>
        Task<BookList?> GetBookListByIdAsync(int id);
        
        /// <summary>
        /// Kullanıcıya göre kitap listelerini getirir
        /// </summary>
        Task<List<BookList>> GetBookListsByUserIdAsync(string userId);
        
        /// <summary>
        /// Yeni bir kitap listesi ekler
        /// </summary>
        Task<BookList> AddBookListAsync(BookList bookList);
        
        /// <summary>
        /// Var olan bir kitap listesini günceller
        /// </summary>
        Task<BookList?> UpdateBookListAsync(BookList bookList);
        
        /// <summary>
        /// Bir kitap listesini siler
        /// </summary>
        Task<BookList?> DeleteBookListAsync(BookList bookList);
        
        /// <summary>
        /// Kitap listesi varlığını kontrol eder
        /// </summary>
        Task<bool> BookListExistsAsync(int id);
        
        /// <summary>
        /// Popüler kitap listelerini getirir (beğeni sayısına göre)
        /// </summary>
        Task<List<BookList>> GetPopularBookListsAsync(int count);
        
        /// <summary>
        /// Listeye kitap ekler
        /// </summary>
        Task<BooksOfList> AddBookToListAsync(BooksOfList bookOfList);
        
        /// <summary>
        /// Listeden kitap çıkarır
        /// </summary>
        Task<BooksOfList?> RemoveBookFromListAsync(int bookListId, int bookId);
        
        /// <summary>
        /// Liste için kitapları getirir
        /// </summary>
        Task<List<Book>> GetBooksInListAsync(int bookListId);
    }
}
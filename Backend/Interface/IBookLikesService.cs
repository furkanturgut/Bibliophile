using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interface
{
    public interface IBookLikesService
    {
        /// <summary>
        /// Bir kitabın beğeni sayısını getirir
        /// </summary>
        Task<int> GetBookLikesCountAsync(int bookId);
        
        /// <summary>
        /// Kullanıcının bir kitabı beğenip beğenmediğini kontrol eder
        /// </summary>
        Task<bool> IsBookLikedByUserAsync(string userId, int bookId);
        
        /// <summary>
        /// Bir kitabı beğenir veya beğeniyi kaldırır (toggle)
        /// </summary>
        Task<bool> ToggleBookLikeAsync(string userId, int bookId);
        
        /// <summary>
        /// Bir kullanıcının beğendiği tüm kitapları getirir
        /// </summary>
        Task<List<int?>> GetUserLikedBookIdsAsync(string userId);
    }
}
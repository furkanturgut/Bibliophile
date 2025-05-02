using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interface
{
    public interface IListLikeRepository
    {
        /// <summary>
        /// Tüm liste beğenilerini getirir
        /// </summary>
        Task<List<ListLike>> GetAllListLikesAsync();
        
        /// <summary>
        /// Bir listenin tüm beğenilerini getirir
        /// </summary>
        Task<List<ListLike>> GetLikesByListIdAsync(int listId);
        
        /// <summary>
        /// Bir kullanıcının beğendiği tüm listeleri getirir
        /// </summary>
        Task<List<ListLike>> GetListLikesByUserIdAsync(string userId);
        
        /// <summary>
        /// Kullanıcının bir listeyi beğenip beğenmediğini kontrol eder
        /// </summary>
        Task<ListLike> GetListLikeByUserAndListAsync(string userId, int listId);
        
        /// <summary>
        /// Yeni bir beğeni ekler
        /// </summary>
        Task<ListLike> AddListLikeAsync(ListLike listLike);
        
        /// <summary>
        /// Bir beğeniyi kaldırır
        /// </summary>
        Task<ListLike> RemoveListLikeAsync(ListLike listLike);
        
        /// <summary>
        /// Bir listenin beğeni sayısını getirir
        /// </summary>
        Task<int> GetListLikesCountAsync(int listId);
        
        /// <summary>
        /// Kullanıcının bir listeyi beğenip beğenmediğini kontrol eder
        /// </summary>
        Task<bool> IsListLikedByUserAsync(string userId, int listId);
    }
}
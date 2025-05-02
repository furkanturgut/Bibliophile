using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interface
{
    public interface IListLikeService
    {
        /// <summary>
        /// Bir listenin beğeni sayısını getirir
        /// </summary>
        Task<int> GetListLikesCountAsync(int listId);
        
        /// <summary>
        /// Kullanıcının bir listeyi beğenip beğenmediğini kontrol eder
        /// </summary>
        Task<bool> IsListLikedByUserAsync(string userId, int listId);
        
        /// <summary>
        /// Bir listeyi beğenir veya beğeniyi kaldırır (toggle)
        /// </summary>
        Task<bool> ToggleListLikeAsync(string userId, int listId);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interface
{
    public interface IListLikeRepository
    {
    
        Task<List<ListLike>> GetAllListLikesAsync();
        Task<List<ListLike>> GetLikesByListIdAsync(int listId);
        Task<List<ListLike>> GetListLikesByUserIdAsync(string userId);
        Task<ListLike> GetListLikeByUserAndListAsync(string userName, int listId);
        Task<ListLike> AddListLikeAsync(ListLike listLike);
        Task<ListLike> RemoveListLikeAsync(ListLike listLike);
        Task<int> GetListLikesCountAsync(int listId);
        Task<bool> IsListLikedByUserAsync(string userName, int listId);
    }
}
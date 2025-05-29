using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interface
{
    public interface IListLikeService
    {
        Task<int> GetListLikesCountAsync(int listId);
        Task<bool> IsListLikedByUserAsync(string userName, int listId);
        Task<bool> ToggleListLikeAsync(string userName, int listId);
    }
}
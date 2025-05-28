using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interface
{
    public interface IBookLikesService
    {
       
        Task<int> GetBookLikesCountAsync(int bookId);
        Task<bool> IsBookLikedByUserAsync(string userName, int bookId); 
        Task<bool> ToggleBookLikeAsync(string userName, int bookId);
        Task<List<int?>> GetUserLikedBookIdsAsync(string userName);
    }
}
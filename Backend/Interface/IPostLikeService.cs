using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interface
{
    public interface IPostLikeService
    {
      
        Task<int> GetPostLikesCountAsync(int postId);
        Task<bool> IsPostLikedByUserAsync(string userName, int postId);
        Task<bool> TogglePostLikeAsync(string userName, int postId);
    }
}
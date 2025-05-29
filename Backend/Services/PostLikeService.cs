using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly UserManager<AppUser> _userManager;

        public PostLikeService(IPostLikeRepository postLikeRepository, IBlogPostRepository blogPostRepository, UserManager<AppUser> userManager)
        {
            _postLikeRepository = postLikeRepository;
            _blogPostRepository = blogPostRepository;
            this._userManager = userManager;
        }

        public async Task<int> GetPostLikesCountAsync(int postId)
        {
            try
            {
                return await _postLikeRepository.GetPostLikesCountAsync(postId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsPostLikedByUserAsync(string userName, int postId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                return await _postLikeRepository.IsPostLikedByUserAsync(user.Id, postId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TogglePostLikeAsync(string userName, int postId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                // Blog yazısının var olup olmadığını kontrol et
                var post = await _blogPostRepository.GetBlogPostByIdAsync(postId) ?? throw new KeyNotFoundException($"ID {postId} ile blog yazısı bulunamadı");

                // Kullanıcının yazıyı beğenip beğenmediğini kontrol et
                var existingLike = await _postLikeRepository.GetPostLikeByUserAndPostAsync(user.Id, postId);
                
                if (existingLike != null)
                {
                    // Beğeni varsa kaldır
                    await _postLikeRepository.RemovePostLikeAsync(existingLike);
                    return false; // Artık beğenilmiyor
                }
                else
                {
                    // Beğeni yoksa ekle
                    var newLike = new PostLike
                    {
                        PostId = postId,
                        UserId = user.Id
                    };
                    
                    await _postLikeRepository.AddPostLikeAsync(newLike);
                    return true; // Şimdi beğeniliyor
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
using Backend.Interface;
using Backend.Models;
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

        public PostLikeService(IPostLikeRepository postLikeRepository, IBlogPostRepository blogPostRepository)
        {
            _postLikeRepository = postLikeRepository;
            _blogPostRepository = blogPostRepository;
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

        public async Task<bool> IsPostLikedByUserAsync(string userId, int postId)
        {
            try
            {
                return await _postLikeRepository.IsPostLikedByUserAsync(userId, postId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TogglePostLikeAsync(string userId, int postId)
        {
            try
            {
                // Blog yazısının var olup olmadığını kontrol et
                var post = await _blogPostRepository.GetBlogPostByIdAsync(postId);
                if (post == null)
                {
                    throw new KeyNotFoundException($"ID {postId} ile blog yazısı bulunamadı");
                }

                // Kullanıcının yazıyı beğenip beğenmediğini kontrol et
                var existingLike = await _postLikeRepository.GetPostLikeByUserAndPostAsync(userId, postId);
                
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
                        UserId = userId
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
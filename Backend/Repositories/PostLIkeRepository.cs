using Backend.Data;
using Backend.Interface;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    public class PostLikeRepository : IPostLikeRepository
    {
        private readonly ApplicationDataContext _dataContext;

        public PostLikeRepository(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<PostLike>> GetAllPostLikesAsync()
        {
            try
            {
                return await _dataContext.postLikes
                    .Include(pl => pl.User)
                    .Include(pl => pl.Post)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PostLike>> GetLikesByPostIdAsync(int postId)
        {
            try
            {
                return await _dataContext.postLikes
                    .Include(pl => pl.User)
                    .Where(pl => pl.PostId == postId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PostLike>> GetPostLikesByUserIdAsync(string userId)
        {
            try
            {
                return await _dataContext.postLikes
                    .Include(pl => pl.Post)
                    .Where(pl => pl.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PostLike> GetPostLikeByUserAndPostAsync(string userId, int postId)
        {
            try
            {
                return await _dataContext.postLikes
                    .FirstOrDefaultAsync(pl => pl.UserId == userId && pl.PostId == postId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PostLike> AddPostLikeAsync(PostLike postLike)
        {
            try
            {
                await _dataContext.postLikes.AddAsync(postLike);
                await _dataContext.SaveChangesAsync();
                return postLike;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PostLike> RemovePostLikeAsync(PostLike postLike)
        {
            try
            {
                _dataContext.postLikes.Remove(postLike);
                await _dataContext.SaveChangesAsync();
                return postLike;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetPostLikesCountAsync(int postId)
        {
            try
            {
                return await _dataContext.postLikes
                    .Where(pl => pl.PostId == postId)
                    .CountAsync();
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
                return await _dataContext.postLikes
                    .AnyAsync(pl => pl.UserId == userId && pl.PostId == postId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
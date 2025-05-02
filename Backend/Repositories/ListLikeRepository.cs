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
    public class ListLikeRepository : IListLikeRepository
    {
        private readonly ApplicationDataContext _dataContext;

        public ListLikeRepository(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<ListLike>> GetAllListLikesAsync()
        {
            try
            {
                return await _dataContext.listLikes
                    .Include(ll => ll.User)
                    .Include(ll => ll.List)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ListLike>> GetLikesByListIdAsync(int listId)
        {
            try
            {
                return await _dataContext.listLikes
                    .Include(ll => ll.User)
                    .Where(ll => ll.ListId == listId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ListLike>> GetListLikesByUserIdAsync(string userId)
        {
            try
            {
                return await _dataContext.listLikes
                    .Include(ll => ll.List)
                    .Where(ll => ll.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ListLike> GetListLikeByUserAndListAsync(string userId, int listId)
        {
            try
            {
                return await _dataContext.listLikes
                    .FirstOrDefaultAsync(ll => ll.UserId == userId && ll.ListId == listId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ListLike> AddListLikeAsync(ListLike listLike)
        {
            try
            {
                await _dataContext.listLikes.AddAsync(listLike);
                await _dataContext.SaveChangesAsync();
                return listLike;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ListLike> RemoveListLikeAsync(ListLike listLike)
        {
            try
            {
                _dataContext.listLikes.Remove(listLike);
                await _dataContext.SaveChangesAsync();
                return listLike;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetListLikesCountAsync(int listId)
        {
            try
            {
                return await _dataContext.listLikes
                    .Where(ll => ll.ListId == listId)
                    .CountAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsListLikedByUserAsync(string userId, int listId)
        {
            try
            {
                return await _dataContext.listLikes
                    .AnyAsync(ll => ll.UserId == userId && ll.ListId == listId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
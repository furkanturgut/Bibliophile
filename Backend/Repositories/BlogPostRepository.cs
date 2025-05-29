using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Interface;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDataContext _dataContext;

        public BlogPostRepository(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<BlogPost?> GetBlogPostByIdAsync(int id)
        {
            try
            {
                return await _dataContext.blogPosts
                    .Include(b => b.User)
                    .Include(b => b.BooksOfPosts)
                    .ThenInclude(bp => bp.Book)
                    .Include(b => b.BookAuthorsOfPost)
                    .ThenInclude(ba => ba.Author)
                    .FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BlogPost>> GetAllBlogPostsAsync()
        {
            try
            {
                return await _dataContext.blogPosts
                    .Include(b => b.User)
                    .Include(b => b.BooksOfPosts)
                    .ThenInclude(bp => bp.Book)
                    .OrderByDescending(b => b.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BlogPost>> GetBlogPostsByAuthorIdAsync(int authorId)
        {
            try
            {
                return await _dataContext.blogPosts
                    .Include(b => b.User)
                    .Include(b => b.BookAuthorsOfPost)
                    .ThenInclude(ba => ba.Author)
                    .Include(b => b.BooksOfPosts)
                    .ThenInclude(bp => bp.Book)
                    .Where(b => b.BookAuthorsOfPost.Any(ba => ba.AuthorId == authorId))
                    .OrderByDescending(b => b.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BlogPost> AddBlogPostAsync(BlogPost blogPost)
        {
            try
            {
                await _dataContext.blogPosts.AddAsync(blogPost);
                await _dataContext.SaveChangesAsync();
                return blogPost;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost)
        {
            try
            {
                _dataContext.blogPosts.Update(blogPost);
                await _dataContext.SaveChangesAsync();
                return blogPost;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BlogPost?> DeleteBlogPostAsync(BlogPost blogPost)
        {
            try
            {
                _dataContext.blogPosts.Remove(blogPost);
                await _dataContext.SaveChangesAsync();
                return blogPost;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> BlogPostExistsAsync(int id)
        {
            try
            {
                return await _dataContext.blogPosts.AnyAsync(b => b.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<BlogPost>> GetRecentBlogPostsAsync(int count)
        {
            try
            {
                return await _dataContext.blogPosts
                    .Include(b => b.User)
                    .OrderByDescending(b => b.CreatedAt)
                    .Take(count)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BlogPost>> GetBlogPostByBook(int bookId)
        {
            try
            {
                return await _dataContext.blogPosts
                    .Include(b => b.User)
                    .Include(b => b.BooksOfPosts)
                    .ThenInclude(bp => bp.Book)
                    .Where(b => b.BooksOfPosts.Any(bp => bp.BookId == bookId))
                    .OrderByDescending(b => b.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
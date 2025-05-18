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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDataContext _dataContext;

        public AuthorRepository(ApplicationDataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            try
            {
              return await _dataContext.authors.FindAsync(id);
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            try
            {
               return await _dataContext.authors.ToListAsync();
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            try
            {
                await _dataContext.authors.AddAsync(author);
                await _dataContext.SaveChangesAsync();
                return author;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Author?> UpdateAuthorAsync(Author author)
        {
            try
            {
                _dataContext.authors.Update(author);
                await _dataContext.SaveChangesAsync();
                return author;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Author?> DeleteAuthorAsync(Author author)
        {
            try
            {
                _dataContext.authors.Remove(author);
                await _dataContext.SaveChangesAsync();
                return author;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public async Task<bool> AuthorExistsAsync(int id)
        {
            try
            {
                return await _dataContext.authors.AnyAsync(a => a.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.AuthorDto;
using Backend.Models;

namespace Backend.Interface.AuthorInterface
{
    public interface IAuthorService
    {
        
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<List<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto?> AddAuthorAsync(CreateAuthorDto authorDto, string userName);
        Task<AuthorDto?> UpdateAuthorAsync(int id, UpdateAuthorDto authorDto, string userName);
        Task<AuthorDto?> DeleteAuthorAsync(int id, string userName);
        Task<bool> AuthorExistsAsync(int id);
    }
}
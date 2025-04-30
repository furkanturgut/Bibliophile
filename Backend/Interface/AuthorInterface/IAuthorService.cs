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
        /// <summary>
        /// Belirtilen ID'ye sahip yazarı getirir
        /// </summary>
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        
        /// <summary>
        /// Tüm yazarları getirir
        /// </summary>
        Task<List<AuthorDto>> GetAllAuthorsAsync();
        
        /// <summary>
        /// Yeni bir yazar ekler
        /// </summary>
        Task<AuthorDto> AddAuthorAsync(CreateAuthorDto authorDto);
        
        /// <summary>
        /// Var olan bir yazarı günceller
        /// </summary>
        Task<AuthorDto?> UpdateAuthorAsync(int id, UpdateAuthorDto authorDto);
        
        /// <summary>
        /// Bir yazarı siler
        /// </summary>
        Task<AuthorDto?> DeleteAuthorAsync(int id);
        
        
        /// <summary>
        /// Yazarın var olup olmadığını kontrol eder
        /// </summary>
        Task<bool> AuthorExistsAsync(int id);
    }
}
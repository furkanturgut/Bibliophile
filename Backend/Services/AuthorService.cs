using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Dtos.AuthorDto;
using Backend.Interface;
using Backend.Interface.AuthorInterface;
using Backend.Models;

namespace Backend.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            try
            {
                var author = await _authorRepository.GetAuthorById(id);
                if (author == null)
                {
                    throw new KeyNotFoundException();
                }
                
                return _mapper.Map<AuthorDto>(author);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AuthorDto>> GetAllAuthorsAsync()
        {
            try
            {
                var authors = await _authorRepository.GetAllAuthorsAsync();
                return _mapper.Map<List<AuthorDto>>(authors);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthorDto> AddAuthorAsync(CreateAuthorDto authorDto)
        {
            try
            {
                var author = _mapper.Map<Author>(authorDto);
                var addedAuthor = await _authorRepository.AddAuthorAsync(author);
                return _mapper.Map<AuthorDto>(addedAuthor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthorDto?> UpdateAuthorAsync(int id, UpdateAuthorDto authorDto)
        {
            try
            {
                var existingAuthor = await _authorRepository.GetAuthorById(id);
                if (existingAuthor == null)
                {
                    throw new KeyNotFoundException();
                }

                // Yazar modeline DTO'dan alınan değerleri uygula
                _mapper.Map(authorDto, existingAuthor);
                
                var updatedAuthor = await _authorRepository.UpdateAuthorAsync(existingAuthor);
                return _mapper.Map<AuthorDto>(updatedAuthor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthorDto?> DeleteAuthorAsync(int id)
        {
            try
            {
                var author = await _authorRepository.GetAuthorById(id);
                if (author == null)
                {
                    throw new KeyNotFoundException();
                }

                var deletedAuthor = await _authorRepository.DeleteAuthorAsync(author);
                return _mapper.Map<AuthorDto>(deletedAuthor);
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
                return await _authorRepository.AuthorExistsAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
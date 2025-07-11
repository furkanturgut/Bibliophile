using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Dtos.BlogPostDtos;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public BlogPostService(IBlogPostRepository blogPostRepository,IBookRepository bookRepository,IAuthorRepository authorRepository,IMapper mapper, UserManager<AppUser> userManager)
        {
            this._blogPostRepository = blogPostRepository;
            this._bookRepository = bookRepository;
            this._authorRepository = authorRepository;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<BlogPostDto?> GetBlogPostByIdAsync(int id)
        {
            try
            {
                var blogPost = await _blogPostRepository.GetBlogPostByIdAsync(id);
                if (blogPost == null)
                    throw new KeyNotFoundException();
                
                return _mapper.Map<BlogPostDto>(blogPost);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BlogPostDto>> GetAllBlogPostsAsync()
        {
            try
            {
                var blogPosts = await _blogPostRepository.GetAllBlogPostsAsync();
                return _mapper.Map<List<BlogPostDto>>(blogPosts);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BlogPostDto>> GetBlogPostsByAuthorIdAsync(int authorId)
        {
            try
            {
                var blogPosts = await _blogPostRepository.GetBlogPostsByAuthorIdAsync(authorId);
                return _mapper.Map<List<BlogPostDto>>(blogPosts);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BlogPostDto> AddBlogPostAsync(CreateBlogPostDto blogPostDto, string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var userRole = await _userManager.GetRolesAsync(user);
                if (userRole.Count == 0 || (userRole[0] != "Admin" && userRole[0] != "Blogger"))
                {
                    throw new UnauthorizedAccessException("Only Admins and Bloggers can add blog posts.");
                }
                // BlogPost nesnesini oluştur
                var blogPost = _mapper.Map<BlogPost>(blogPostDto);
                blogPost.UserId = user.Id;
                blogPost.CreatedAt = DateTime.Now;
                
                // Blog yazısını kaydet
                var createdBlogPost = await _blogPostRepository.AddBlogPostAsync(blogPost);

                // Koleksiyonların initialize edildiğinden emin ol
                createdBlogPost.BooksOfPosts ??= new List<BooksOfPost>();
                createdBlogPost.BookAuthorsOfPost ??= new List<BookAuthorsOfPost>();
                
                // İlişkili kitapları ekle
                if (blogPostDto.BookIds != null && blogPostDto.BookIds.Any())
                {
                    foreach (var bookId in blogPostDto.BookIds)
                    {
                        var book = await _bookRepository.GetBookByIdAsync(bookId);
                        if (book != null)
                        {
                            var bookOfPost = new BooksOfPost
                            {
                                PostId = createdBlogPost.Id,
                                BookId = bookId
                            };
                            createdBlogPost.BooksOfPosts.Add(bookOfPost); // BooksOfPosts null olabilir
                        }
                    }
                }
                
                // İlişkili yazarları ekle
                if (blogPostDto.AuthorIds != null && blogPostDto.AuthorIds.Count != 0)
                {
                    foreach (var authorId in blogPostDto.AuthorIds)
                    {
                        var author = await _authorRepository.GetAuthorById(authorId);
                        if (author != null)
                        {
                            var authorOfPost = new BookAuthorsOfPost
                            {
                                PostId = createdBlogPost.Id,
                                AuthorId = authorId
                            };
                            createdBlogPost.BookAuthorsOfPost.Add(authorOfPost);
                        }
                    }
                }
                
                // İlişkiler eklendikten sonra güncelle
                await _blogPostRepository.UpdateBlogPostAsync(createdBlogPost);
                
                // Güncel hali ile getir
                var updatedBlogPost = await _blogPostRepository.GetBlogPostByIdAsync(createdBlogPost.Id);
                return _mapper.Map<BlogPostDto>(updatedBlogPost);
            }
            catch (Exception )
            {
                throw;
            }
        }

        public async Task<BlogPostDto?> UpdateBlogPostAsync(int blogId, UpdateBlogPostDto blogPostDto, string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var userRole = await _userManager.GetRolesAsync(user);
                if (userRole.Count == 0 || userRole[0] != "Admin" || userRole[0] != "Blogger")
                {
                    throw new UnauthorizedAccessException("Only Admins and Bloggers can add blog posts.");
                }
                var existingBlogPost = await _blogPostRepository.GetBlogPostByIdAsync(blogId) ?? throw new KeyNotFoundException();

                // Temel özellikleri güncelle
                _mapper.Map(blogPostDto, existingBlogPost);
                existingBlogPost.UpdatedAt = DateTime.Now;
                
                // İlişkili kitapları güncelle
                if (blogPostDto.BookIds != null)
                {
                    // Mevcut kitapları temizle
                    if (existingBlogPost.BooksOfPosts != null)
                    {
                        existingBlogPost.BooksOfPosts.Clear();
                    }
                    else
                    {
                        existingBlogPost.BooksOfPosts = new List<BooksOfPost>();
                    }
                    
                    // Yeni kitapları ekle
                    foreach (var bookId in blogPostDto.BookIds)
                    {
                        var book = await _bookRepository.GetBookByIdAsync(bookId);
                        if (book != null)
                        {
                            existingBlogPost.BooksOfPosts.Add(new BooksOfPost
                            {
                                PostId = existingBlogPost.Id,
                                BookId = bookId
                            });
                        }
                    }
                }
                
                // İlişkili yazarları güncelle
                if (blogPostDto.AuthorIds != null)
                {
                    // Mevcut yazarları temizle
                    if (existingBlogPost.BookAuthorsOfPost != null)
                    {
                        existingBlogPost.BookAuthorsOfPost.Clear();
                    }
                    else
                    {
                        existingBlogPost.BookAuthorsOfPost = new List<BookAuthorsOfPost>();
                    }
                    
                    // Yeni yazarları ekle
                    foreach (var authorId in blogPostDto.AuthorIds)
                    {
                        var author = await _authorRepository.GetAuthorById(authorId);
                        if (author != null)
                        {
                            existingBlogPost.BookAuthorsOfPost.Add(new BookAuthorsOfPost
                            {
                                PostId = existingBlogPost.Id,
                                AuthorId = authorId
                            });
                        }
                    }
                }
                
                // Güncelleme işlemini kaydet
                var updatedBlogPost = await _blogPostRepository.UpdateBlogPostAsync(existingBlogPost);
                return _mapper.Map<BlogPostDto>(updatedBlogPost);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BlogPostDto?> DeleteBlogPostAsync(int postId, string userName )
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var userRole = await _userManager.GetRolesAsync(user);
                 if (userRole.Count == 0 || (userRole[0] != "Admin" && userRole[0] != "Blogger"))
                {
                    throw new UnauthorizedAccessException("Only Admins and Bloggers can add blog posts.");
                }
                var blogPost = await _blogPostRepository.GetBlogPostByIdAsync(postId) ?? throw new KeyNotFoundException();
                var deletedBlogPost = await _blogPostRepository.DeleteBlogPostAsync(blogPost);
                return _mapper.Map<BlogPostDto>(deletedBlogPost);
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
                return await _blogPostRepository.BlogPostExistsAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BlogPostDto>?> GetBlogPostByBook(int bookId)
        {

            try
            {
                var bookExist = await _bookRepository.GetBookByIdAsync(bookId) ?? throw new KeyNotFoundException();
                var blogPosts = _mapper.Map<List<BlogPostDto>?>(await _blogPostRepository.GetBlogPostByBook(bookId));
                return blogPosts;
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
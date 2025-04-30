using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Backend.Dtos;
using Backend.Dtos.AccountDtos;
using Backend.Dtos.AuthorDto;
using Backend.Dtos.BlogPostDtos;
using Backend.Dtos.BookDtos;
using Backend.Dtos.GenreDtos;
using Backend.Models;

namespace Backend.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Book -> BookDto mapping
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => 
                    src.Author != null ? src.Author.Name : null))
                .ForMember(dest => dest.GenreNames, opt => opt.MapFrom(src => 
                    src.BookGenres != null ? src.BookGenres.Select(bg => bg.Genre.Name).ToList() : null))
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => 
                    src.BookLikes != null ? src.BookLikes.Count : 0))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AvarageRating));

            // BookDto -> Book mapping
            CreateMap<BookDto, Book>()
                .ForMember(dest => dest.AvarageRating, opt => opt.MapFrom(src => src.AverageRating))
                .ForMember(dest => dest.BookGenres, opt => opt.Ignore())
                .ForMember(dest => dest.BookLikes, opt => opt.Ignore())
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.BooksOfLists, opt => opt.Ignore())
                .ForMember(dest => dest.BooksOfPosts, opt => opt.Ignore())
                .ForMember(dest => dest.Ratings, opt => opt.Ignore());

            // CreateBookDto -> Book mapping
            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AvarageRating, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.BookGenres, opt => opt.Ignore())
                .ForMember(dest => dest.BookLikes, opt => opt.Ignore())
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.BooksOfLists, opt => opt.Ignore())
                .ForMember(dest => dest.BooksOfPosts, opt => opt.Ignore())
                .ForMember(dest => dest.Ratings, opt => opt.Ignore());

            // UpdateBookDto -> Book mapping (partial update)
            CreateMap<UpdateBookDto, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ReleaseDate, opt => opt.Ignore())
                .ForMember(dest => dest.BookCover, opt => opt.Ignore())
                .ForMember(dest => dest.Summary, opt => opt.Ignore())
                .ForMember(dest => dest.AvarageRating, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorId, opt => opt.Ignore())
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.BookGenres, opt => opt.Ignore())
                .ForMember(dest => dest.BookLikes, opt => opt.Ignore())
                .ForMember(dest => dest.BooksOfLists, opt => opt.Ignore())
                .ForMember(dest => dest.BooksOfPosts, opt => opt.Ignore())
                .ForMember(dest => dest.Ratings, opt => opt.Ignore());

            // Book -> Book mapping (for updates)
            CreateMap<Book, Book>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // AppUser -> GetUserDto (for Getting users)
            CreateMap<AppUser ,GetUserDto>().ForMember(dest => dest.role, opt=> opt.Ignore());

            // Author -> AuthorDto mapping
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.BookNames, opt => opt.MapFrom(src => 
                    src.Books != null ? src.Books.Select(b => b.Name).ToList() : null));
                

            // AuthorDto -> Author mapping
            CreateMap<AuthorDto, Author>()
                .ForMember(dest => dest.Books, opt => opt.Ignore())
                .ForMember(dest => dest.BookAuthorsOfPost, opt => opt.Ignore());

            // CreateAuthorDto -> Author mapping
            CreateMap<CreateAuthorDto, Author>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Books, opt => opt.Ignore())
                .ForMember(dest => dest.BookAuthorsOfPost, opt => opt.Ignore());

            // UpdateAuthorDto -> Author mapping
            CreateMap<UpdateAuthorDto, Author>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // BlogPost mappings
            CreateMap<BlogPost, BlogPostDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => 
                    src.User != null ? src.User.UserName : null))
                .ForMember(dest => dest.BookNames, opt => opt.MapFrom(src => 
                    src.BooksOfPosts != null ? src.BooksOfPosts.Select(b => b.Book.Name).ToList() : null))
                .ForMember(dest => dest.AuthorNames, opt => opt.MapFrom(src => 
                    src.BookAuthorsOfPost != null ? src.BookAuthorsOfPost.Select(a => a.Author.Name).ToList() : null));
            
            CreateMap<CreateBlogPostDto, BlogPost>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.BooksOfPosts, opt => opt.Ignore())
                .ForMember(dest => dest.BookAuthorsOfPost, opt => opt.Ignore());
            
            CreateMap<UpdateBlogPostDto, BlogPost>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Genre, GenreDto>().ReverseMap();
        }
    }
}
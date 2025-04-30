using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ApplicationDataContext : IdentityDbContext<AppUser>
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
        }
        public DbSet<Genre> genres { get; set; }
        public DbSet<BookGenre> bookGenres { get; set; }
        public DbSet<BooksOfList> booksOfLists { get; set; }
        public DbSet<BookList> bookLists { get; set; }
        public DbSet<ListLike> listLikes { get; set; }
        public DbSet<Rating> ratings {get;set;}
        public DbSet<BookLikes> bookLikes { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<PostLike> postLikes { get; set; }
        public DbSet<BooksOfPost> booksOfPosts { get; set; }
        public DbSet<Author> authors { get; set; }
        public DbSet<BlogPost> blogPosts { get; set; }
        public DbSet<BookAuthorsOfPost> bookAuthorsOfPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BookGenre>().HasKey(bg=> new {bg.GenreId, bg.BookId});
            builder.Entity<BookGenre>().HasOne(b=> b.Book).WithMany(bg=> bg.BookGenres).HasForeignKey(b=> b.BookId);
            builder.Entity<BookGenre>().HasOne(g=> g.Genre).WithMany(bg=> bg.BookGenres).HasForeignKey(g=> g.GenreId);

            builder.Entity<BooksOfList>().HasKey(bol=> bol.Id);
            builder.Entity<BooksOfList>().HasOne(b=> b.List).WithMany(bol=> bol.BooksOfLists).HasForeignKey(b=> b.ListId);
            builder.Entity<BooksOfList>().HasOne(b=> b.Book).WithMany(bol=> bol.BooksOfLists).HasForeignKey(b=> b.BookId);

            builder.Entity<ListLike>().HasKey(ll=> new {ll.ListId, ll.UserId});
            builder.Entity<ListLike>().HasOne(b=> b.List).WithMany(ll=> ll.ListLikes).HasForeignKey(b=> b.ListId);
            builder.Entity<ListLike>().HasOne(b=> b.User).WithMany(ll=> ll.ListLikes).HasForeignKey(b=> b.UserId);
            
            builder.Entity<BookLikes>().HasKey(bl=> new {bl.BookId, bl.UserId});
            builder.Entity<BookLikes>().HasOne(b=> b.Book).WithMany(bl=> bl.BookLikes).HasForeignKey(b=> b.BookId);
            builder.Entity<BookLikes>().HasOne(b=> b.User).WithMany(bl=> bl.BookLikes).HasForeignKey(b=> b.UserId);

            builder.Entity<PostLike>().HasKey(pl=> new {pl.PostId, pl.UserId});
            builder.Entity<PostLike>().HasOne(b=> b.Post).WithMany(pl=> pl.PostLikes).HasForeignKey(b=> b.PostId);
            builder.Entity<PostLike>().HasOne(b=> b.User).WithMany(pl=> pl.PostLikes).HasForeignKey(b=> b.UserId);

            builder.Entity<BooksOfPost>().HasKey(bop=> new {bop.PostId, bop.BookId});
            builder.Entity<BooksOfPost>().HasOne(b=> b.Post).WithMany(bop=> bop.BooksOfPosts).HasForeignKey(b=> b.PostId);
            builder.Entity<BooksOfPost>().HasOne(b=> b.Book).WithMany(bop=> bop.BooksOfPosts).HasForeignKey(b=> b.BookId);

            builder.Entity<BookAuthorsOfPost>().HasKey(bap=> new {bap.PostId, bap.AuthorId});
            builder.Entity<BookAuthorsOfPost>().HasOne(b=> b.Post).WithMany(bap=> bap.BookAuthorsOfPost).HasForeignKey(b=> b.PostId);
            builder.Entity<BookAuthorsOfPost>().HasOne(b=> b.Author).WithMany(bap=> bap.BookAuthorsOfPost).HasForeignKey(b=> b.AuthorId);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id= "1",
                    Name= "Admin",
                    NormalizedName= "ADMIN"

                },
                new IdentityRole
                {
                    Id= "2",
                    Name= "User",
                    NormalizedName= "USER"
                },
                new IdentityRole
                {
                    Id= "3",
                    Name= "Blogger",
                    NormalizedName= "Blogger"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            var genres = new List<Genre>
            {
                new Genre { Id = 1, Name = "Fiction" },
                new Genre { Id = 2, Name = "Non-fiction" },
                new Genre { Id = 3, Name = "Mystery" },
                new Genre { Id = 4, Name = "Science Fiction" },
                new Genre { Id = 5, Name = "Fantasy" },
                new Genre { Id = 6, Name = "Romance" },
                new Genre { Id = 7, Name = "Thriller" },
                new Genre { Id = 8, Name = "Horror" },
                new Genre { Id = 9, Name = "Biography" },
                new Genre { Id = 10, Name = "Autobiography" },
                new Genre { Id = 11, Name = "History" },
                new Genre { Id = 12, Name = "Self-help" },
                new Genre { Id = 13, Name = "Business" },
                new Genre { Id = 14, Name = "Memoir" },
                new Genre { Id = 15, Name = "Poetry" },
                new Genre { Id = 16, Name = "Children's" },
                new Genre { Id = 17, Name = "Young Adult" },
                new Genre { Id = 18, Name = "Dystopian" },
                new Genre { Id = 19, Name = "Adventure" },
                new Genre { Id = 20, Name = "Historical Fiction" },
                new Genre { Id = 21, Name = "Science" },
                new Genre { Id = 22, Name = "Technology" },
                new Genre { Id = 23, Name = "Philosophy" },
                new Genre { Id = 24, Name = "Religion" },
                new Genre { Id = 25, Name = "Psychology" },
                new Genre { Id = 26, Name = "Cooking" },
                new Genre { Id = 27, Name = "Art" },
                new Genre { Id = 28, Name = "Travel" },
                new Genre { Id = 29, Name = "Sports" },
                new Genre { Id = 30, Name = "Contemporary" },
                new Genre { Id = 31, Name = "Classics" },
                new Genre { Id = 32, Name = "Graphic Novel" },
                new Genre { Id = 33, Name = "Comic" },
                new Genre { Id = 34, Name = "Crime" },
                new Genre { Id = 35, Name = "Literary Fiction" },
                new Genre { Id = 36, Name = "Humor" },
                new Genre { Id = 37, Name = "Drama" },
                new Genre { Id = 38, Name = "Western" },
                new Genre { Id = 39, Name = "Historical" },
                new Genre { Id = 40, Name = "Reference" }
            };
            builder.Entity<Genre>().HasData(genres);
        }
    }
    
       




    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
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
        }
       




    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Models;

namespace Online_Book_Store.Data
{
    public class BookContext : DbContext
    {
        public BookContext (DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BookAuthor>().HasKey(ck => new { ck.BookId, ck.AuthorId });
        }

        public DbSet<Online_Book_Store.Models.Author> Author { get; set; }

        public DbSet<Online_Book_Store.Models.Book_additional_info> Book_additional_info { get; set; }

        public DbSet<Online_Book_Store.Models.Book> Book { get; set; }

        public DbSet<Online_Book_Store.Models.BookAuthor> BookAuthor { get; set; }
    }
}

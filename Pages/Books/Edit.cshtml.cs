using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data;
using Online_Book_Store.Models;

namespace Online_Book_Store.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly Online_Book_Store.Data.BookContext _context;

        public EditModel(Online_Book_Store.Data.BookContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }
        [BindProperty]
        public IList<SelectListItem> AuthorList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book.Include(m => m.BookAuthor)
                .FirstOrDefaultAsync(m => m.ID == id);

            AuthorList = _context.Author.ToList<Author>().Select(a => new SelectListItem
            {
                Text = $"{Convert.ToString(a.ID)} {a.Name}",
                Value = Convert.ToString(a.ID),
                Selected = Book.BookAuthor.Any(ba => ba.AuthorId == a.ID) ? true : false
            }).ToList<SelectListItem>();

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Book BookFromDB = await _context.Book.Include(znj => znj.BookAuthor)
                .FirstOrDefaultAsync(znj => znj.ID == Book.ID);

            /*
            if (!String.IsNullOrEmpty(NewAuthorName))
            {
                Author author = new Author { Name = NewAuthorName, Born = NewAuthorBorn, Colledge = NewAuthorColledge, University = NewAuthorUniversity, Email = NewAuthorEmail };
                await _context.Author.ToListAsync();
                NewAuthorId = author.ID;
            }

            Book BookFromDB = await _context.Book.Include(znj => znj.BookAuthor)
                .FirstOrDefaultAsync(znj => znj.ID == Book.ID);

            IList<BookAuthor> bookAuthors = new List<BookAuthor>();
            IList<BookAuthor> AuthorsToAdd = new List<BookAuthor>();
            IList<BookAuthor> AuthorsToRemove = new List<BookAuthor>();

            if (!String.IsNullOrEmpty(NewAuthorName))
            {
                AuthorsToAdd.Add(new BookAuthor { BookId = Book.ID, AuthorId = NewAuthorId });
            }
            */
            IList<BookAuthor> bookAuthors = new List<BookAuthor>();
            IList<BookAuthor> AuthorsToAdd = new List<BookAuthor>();
            IList<BookAuthor> AuthorsToRemove = new List<BookAuthor>();

            foreach(SelectListItem a in AuthorList)
            {
                if (a.Selected)
                {
                    bookAuthors.Add(new BookAuthor { BookId = Book.ID, AuthorId = Convert.ToInt32(a.Value) });
                    BookAuthor selectedAuthor = BookFromDB.BookAuthor
                        .Where(m => m.AuthorId == Convert.ToInt32(a.Value))
                        .FirstOrDefault();
                    if(selectedAuthor == null)
                    {
                        AuthorsToAdd.Add(new BookAuthor { BookId = Book.ID, AuthorId = Convert.ToInt32(a.Value) });
                    }

                }
            }
            foreach(BookAuthor ba in BookFromDB.BookAuthor)
            {
                if(!bookAuthors.Any(a => a.BookId == ba.BookId && a.AuthorId == ba.AuthorId))
                {
                    AuthorsToRemove.Add(ba);
                }
            }

            BookFromDB.Title = Book.Title;
            BookFromDB.Genre = Book.Genre;
            BookFromDB.Rating = Book.Rating;
            BookFromDB.Publish_date = Book.Publish_date;
            BookFromDB.Price = Book.Price;

            _context.RemoveRange(AuthorsToRemove);

            foreach(var a in AuthorsToAdd)
            {
                BookFromDB.BookAuthor.Add(a);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}

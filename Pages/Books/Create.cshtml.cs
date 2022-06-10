using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Book_Store.Data;
using Online_Book_Store.Models;

namespace Online_Book_Store.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Online_Book_Store.Data.BookContext _context;

        public CreateModel(Online_Book_Store.Data.BookContext context)
        {
            _context = context;
        }


        [BindProperty]
        public Book Book { get; set; }
        [BindProperty]
        public IList<SelectListItem> AuthorList { get; set; }

        //properties for new author if needed
        [BindProperty]
        public string NewAuthorName { get; set; }
        [BindProperty]
        public DateTime NewAuthorBorn { get; set; }
        [BindProperty]
        public string NewAuthorColledge { get; set; }
        [BindProperty]
        public string NewAuthorUniversity { get; set; }
        [BindProperty]
        public string NewAuthorEmail { get; set; }


        public IActionResult OnGet()
        {
            AuthorList = _context.Author.ToList<Author>().Select(al => new SelectListItem { Text = $"{al.ID.ToString()} {al.Name}", Value = al.ID.ToString() }).ToList<SelectListItem>();
            return Page();
        }

       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            IList<BookAuthor> BookAuthors = new List<BookAuthor>();

            foreach(SelectListItem a in AuthorList)
            {
                if (a.Selected)
                {
                    BookAuthors.Add(new BookAuthor { AuthorId = Convert.ToInt32(a.Value) });
                }
                
            }

            if (!String.IsNullOrEmpty(NewAuthorName))
            {
                Author author = new Author { Name = NewAuthorName, Born = NewAuthorBorn, Colledge = NewAuthorColledge, University = NewAuthorUniversity, Email = NewAuthorEmail };
                BookAuthor bookAuthor = new BookAuthor { Author = author };
                BookAuthors.Add(bookAuthor);
            }

            Book.BookAuthor = BookAuthors;
            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

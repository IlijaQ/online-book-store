using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Book_Store.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Born { get; set; }
        public string Colledge { get; set; }
        public string University { get; set; }
        public string Email { get; set; }
        
        //Navigational property to bridge table BookAuthor
        [ForeignKey("AuthorId")]
        public virtual ICollection<BookAuthor> BookAuthor { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Book_Store.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public double Rating { get; set; }
        public DateTime Publish_date { get; set; }
        public double Price { get; set; }
        
        //Navigational property to bridge table BookAuthor
        [ForeignKey("BookId")]
        public virtual ICollection<BookAuthor> BookAuthor { get; set; }
        //navigational property to Book_additional_info
        public virtual Book_additional_info Book_additional_info { get; set; }
    }
}

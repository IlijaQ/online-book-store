using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Book_Store.Models
{
    public class Book_additional_info
    {
        public int ID { get; set; }
        [ForeignKey("Book")]
        public int Book_ID { get; set; }
        public string Description { get; set; }

        //navigational property to Book
        public virtual Book Book { get; set; }
    }
}

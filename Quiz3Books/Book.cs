using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz3Books
{
   public class Book
    {
        [Key]
        public int Id { get; set; } // Primary Key, Identity

        [MinLength(2)][MaxLength(50)]
        public String Title { get; set; } // 2-50 characters

        [MinLength(2)][MaxLength(50)]       
        public String Author { get; set; } // 2-50 characters

        public DateTime Published { get; set; } // valid date 1700-2100 inclusive
        
    }
}

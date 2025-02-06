using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBook_Model
{
    public class Book
    {
        public int Id { get; set; }
        public required Person Person  { get; set; }
        public required string Type { get; set; } 
        public required string Number { get; set; }
    }
}

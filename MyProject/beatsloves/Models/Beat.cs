using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beatsloves.Models
{
    public class Beat
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public decimal Price { get; set; }
        public string Beatimg { get; set; }
        public string Typestring { get; set; }
        public string Description { get; set; }
        public string Beatway { get; set; }
        public int UserId { get; set; }
        public DateTime? PurchaseDate { get; set; }
    }
}

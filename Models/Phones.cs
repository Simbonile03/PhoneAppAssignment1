using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneAppAssignment1.Models
{
    public class Phones
    {
        [PrimaryKey, AutoIncrement]
        public int PhoneId { get; set; } // Primary key for SQL database
        public string PhoneName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }


    }

}
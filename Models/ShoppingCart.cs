using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace PhoneAppAssignment1.Models
{
    public class ShoppingCart
    {
        [PrimaryKey, AutoIncrement]
        public int ShoppingCartId { get; set; } // Primary key for SQL database

        [ForeignKey(typeof(Customer))]
        public int CustomerId { get; set; } // Primary key for SQL database

        [OneToOne]
        public Customer Customer { get; set; }


        // Foreign key for SQL database
        [ForeignKey(typeof(Phones))]
        public int PhonesId { get; set; }

        [OneToMany]
        public Phones Phones { get; set; }

        [ManyToMany(typeof(ShoppingCartPage))]
        public List<Phone> SelectedPhones { get; set; }
        public string PhoneName { get; internal set; }
        public string Brand { get; internal set; }
        public string Price { get; internal set; }
        public string Model { get; internal set; }
    }
}
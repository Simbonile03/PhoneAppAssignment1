using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace PhoneAppAssignment1.Models
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int CustomerId { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public string Bio { get; set; }

        [ForeignKey(typeof(ShoppingCartPage))]
        public int ShoppingCartId { get; set; }

        [OneToOne]
        public ShoppingCartPage ShoppingCartPage { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.IO; // Added for Path
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;
using PhoneAppAssignment1;
using PhoneAppAssignment1.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using SQLitePCL;

namespace PhoneAppAssignment1.Services
{
    public class LocalDataBase
    {
        private SQLiteConnection _dbConnection;
        public List<Phones> SelectedPhones { get; set; }
        public LocalDataBase(string dbPath)
        {
            _dbConnection = new SQLiteConnection(dbPath);
            _dbConnection.CreateTable<Phones>();
        }

        public List<Phones> GetAllPhones()
        {
            return _dbConnection.Table<Phones>().ToList();
        }

        public string GetDatabasePath()
        {
            string filename = "phonesdata.db";
            string pathToDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(pathToDb, filename);
        }

        public LocalDataBase()
        {
            string dbPath = GetDatabasePath(); // Get the database path
            _dbConnection = new SQLiteConnection(dbPath);

            _dbConnection.CreateTable<Phones>();
            _dbConnection.CreateTable<ShoppingCart>();
            _dbConnection.CreateTable<ShoppingCartPage>();
            _dbConnection.CreateTable<Customer>();
            SelectedPhones = new List<Phones>();

            SeedDatabase();
        }

        public void SeedDatabase()
        {
            if (_dbConnection.Table<Phones>().Count() == 0)
            {
                List<Phones> phones = new List<Phones>
                {
                    new Phones()
                    {
                        Name = "IPhone",
                        Model = "IPhone12",
                        Price = 999.99m,

                    },
                    new Phones()
                    {
                        Name = "Samsung Galaxy",
                        Model = "Galaxy S21",
                        Price = 899.99m,

                    },
                    new Phones()
                    {
                        Name = "Nike Air Force 1",
                        Model = "Nike",
                        Price = 90.00m,

                    },
                    new Phones()
                    {
                        Name = "Converse Chuck Taylor All Star",
                        Model = "Converse",
                        Price = 55.00m,

                    },
                    new Phones()
                    {
                        Name = "Vans Old Skool",
                        Model = "Vans",
                        Price = 60.00m,

                    }
                };

                _dbConnection.InsertAll(phones);

            }

            if (_dbConnection.Table<Customer>().Count() == 0)
            {
                Customer customer = new Customer()
                {
                    FirstName = "John",
                    Surname = "Doe",
                    EmailAddress = "johndoe@example.com",
                    Bio = "im a student"

                };

                _dbConnection.Insert(customer);
            }

        }


        public void SaveShoppingCart(ShoppingCart cart)
        {
            _dbConnection.Insert(cart);
        }

        public List<ShoppingCart> GetShoppingCartByCustomerId(int customerId)
        {
            return _dbConnection.Table<ShoppingCart>().Where(c => c.CustomerId == customerId).ToList();
        }

        public void UpdateShoppingCart(ShoppingCart cart)
        {
            // Check if the item already exists in the shopping cart
            var existingItem = _dbConnection.Table<ShoppingCart>()
                                            .FirstOrDefault(c => c.PhonesId == cart.PhonesId);

            if (existingItem != null)
            {
                // If the item exists, increment its quantity
                existingItem.PhonesId++; // Increment by one click
                _dbConnection.Update(existingItem);
            }
            else
            {
                // If the item doesn't exist, add it to the shopping cart with a quantity of 1
                cart.PhonesId = 1; 
                _dbConnection.Insert(cart);
            }
        }


        public void DeleteShoppingCart(ShoppingCart cart)
        {
            _dbConnection.Delete(cart);
        }

        public void AssignShoppingCartsToCustomers()
        {
            // Get all customers and shopping carts from the database
            List<Customer> customers = _dbConnection.Table<Customer>().ToList();
            List<ShoppingCart> shoppingCarts = _dbConnection.Table<ShoppingCart>().ToList();

            // Assign a shopping cart to each customer
            foreach (Customer customer in customers)
            {
                // Check if the customer already has a shopping cart
                ShoppingCart existingCart = shoppingCarts.FirstOrDefault(c => c.CustomerId == customer.CustomerId);

                if (existingCart == null)
                {
                    // If the customer doesn't have a shopping cart, create a new one and assign it to the customer
                    ShoppingCart newCart = new ShoppingCart() { CustomerId = customer.CustomerId };
                    _dbConnection.Insert(newCart);
                }
            }
        }

        public void SaveCustomer(Customer customer)
        {
            _dbConnection.InsertOrReplace(customer);
        }

        public Customer GetSavedCustomer()
        {
            return _dbConnection.Table<Customer>().FirstOrDefault();
        }


        public Customer GetCustomerByEmail(string email)
        {
            return _dbConnection.Table<Customer>().Where(c => c.EmailAddress == email).FirstOrDefault();
        }


        public List<Phones> GetSelectedPhones()
        {
            // Assuming Selected is a property of the Phones class
            return _dbConnection.Table<Phones>().Where(s => s.Selected).ToList();
        }
    }


    public static class DatabaseFilePath
    {
        public static string GetDatabasePath()
        {
            string filename = "phonesdata.db";
            string pathToDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(pathToDb, filename);
        }
    }
}

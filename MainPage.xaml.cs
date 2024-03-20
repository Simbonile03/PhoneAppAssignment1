using PhoneAppAssignment1.Models;
using SQLite;
using SQLiteNetExtensions;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace PhoneAppAssignment1
{

    public class Phones
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public bool Selected { get; internal set; }
        public bool IsSelected { get; internal set; }
    }
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Phones> Phones { get; set; }

        // Input fields for customer information
        Entry firstNameEntry, lastNameEntry, emailEntry, bioEntry;

        // Button for saving
        Button saveButton;

        // Database connection
        private SQLiteAsyncConnection _connection;
        private object connection;
        private string phoneName;
        private string brand;
        private string model;
        private string price;

        public MainPage()
        {
            // Initialize input fields
            firstNameEntry = new Entry { Placeholder = "First Name" };
            lastNameEntry = new Entry { Placeholder = "Last Name" };
            emailEntry = new Entry { Placeholder = "Email" };
            bioEntry = new Entry { Placeholder = "Bio" };

            // Initialize save button
            saveButton = new Button { Text = "Save" };
            saveButton.Clicked += OnSaveClicked;

            // Add input fields and button to the layout
            Content = new StackLayout
            {
                Margin = new Thickness(20),
                Children =
                {
                    firstNameEntry,
                    lastNameEntry,
                    emailEntry,
                    bioEntry,
                    saveButton
                }
            };

            // Initialize the collection and add phone items
            Phones = new ObservableCollection<Phones>
            {
                new Phones { Name = "iPhone", Model = "iPhone 12", Brand = "Apple", Price = 999.99m },
                new Phones { Name = "Samsung Galaxy", Model = "Galaxy S21", Brand = "Samsung", Price = 899.99m },
                new Phones { Name = "Google Pixel", Model = "Pixel 5", Brand = "Google", Price = 799.99m }
                // Add more phone items as needed
            };
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Now you can access the entered data like this:
            string firstName = firstNameEntry.Text;
            string lastName = lastNameEntry.Text;
            string email = emailEntry.Text;
            string bio = bioEntry.Text;

            // Do something with the entered data, like adding it to the shopping cart
            // For example:
            await Navigation.PushAsync(new MenuPage());
        }

        private async Task AddToShoppingCartPage(string firstName, string lastName, string email, string bio)
        {
            // Create a new shopping cart item
            var shoppingCart = new ShoppingCart
            {
                PhoneName = phoneName,
                Brand = brand,
                Model = model,
                Price = price,

            };

            // Save the shopping cart item to the database
            await _connection.InsertAsync(shoppingCart);
        }
    }
}

using PhoneAppAssignment1.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System;
using System.Collections.Generic;


namespace PhoneAppAssignment1
{
    public partial class MenuPage : ContentPage
    {
        private LocalDataBase _database;

        public ObservableCollection<Phones> Phones { get; set; }

        public MenuPage()
        {
            InitializeComponent();

            // Initialize the database
            _database = new LocalDataBase();

            // Load the list of phones and set it as the ListView's ItemsSource
            Phones = new ObservableCollection<Phones>(_database.GetAllPhones())
            {
                new Phones { Name = "iPhone", Model = "iPhone 12", Brand = "Apple", Price = 999.99m },
                new Phones { Name = "Samsung Galaxy", Model = "Galaxy S21", Brand = "Samsung", Price = 899.99m },
                new Phones { Name = "Google Pixel", Model = "Pixel 5", Brand = "Google", Price = 799.99m }
            // Add more phone items as needed
            };
            itemListView.ItemsSource = Phones;
        }

        private async void AddToCartButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Get the selected items
                List<Phones> selectedPhones = GetSelectedPhones();

                // Prompt the user to enter the selected quantity for each selected item
                foreach (var selectedPhone in selectedPhones)
                {
                    // Show an input dialog for selected quantity
                    string selectedQuantityText = await DisplayPromptAsync("Enter Selected phoneId", $"Enter selected phoneId for {selectedPhone.Name}", "OK", "Cancel", "1", -1, Keyboard.Numeric);

                    // Check if the user entered a selected quantity
                    if (!string.IsNullOrEmpty(selectedQuantityText))
                    {
                        // Parse the selected quantity
                        if (int.TryParse(selectedQuantityText, out int selectedPhoneId))
                        {
                            // Add the item to the shopping cart with the specified selected quantity
                            ShoppingCartPage cartPage = new ShoppingCartPage(selectedPhone)
                            {
                                PhoneId = selectedPhoneId // Use the selected quantity entered by the user
                            };
                            await Navigation.PushAsync(cartPage);
                        }
                        else
                        {
                            // Show an error message if the entered selected quantity is invalid
                            await DisplayAlert("Error", "Invalid selected quantity. Please enter a valid number.", "OK");
                        }
                    }
                }

                // Display a success message
                await DisplayAlert("Success", "Selected items added to shopping cart.", "OK");
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Debug.WriteLine($"Error: {ex.Message}");
                await DisplayAlert("Error", $"Failed to add selected items to shopping cart: {ex.Message}", "OK");
            }
        }

        private List<Phones> GetSelectedPhones()
        {
            var selectedPhones = new List<Phones>();
            foreach (var phone in itemListView.ItemsSource)
            {
                if (phone is Phones selectedPhone && selectedPhone.IsSelected)
                {
                    selectedPhones.Add(selectedPhone);
                }
            }
            return selectedPhones;
        }

        private async void OnItemTappedAsync(object sender, EventArgs e)
        {
            var selectedPhone = (sender as ViewCell)?.BindingContext as Phones;
            if (selectedPhone != null)
            {
                // Navigate to the detail page passing the selected phone as parameter
                await Navigation.PushAsync(new ShoppingCartPage(selectedPhone));
            }
        }
    }
}

using System;
using System.Threading.Tasks;


namespace PhoneAppAssignment1
{
    public partial class ShoppingCartPage : ContentPage
    {
        private Phones selectedPhone;

        public ShoppingCartPage(Phones selectedPhone)
        {
            InitializeComponent();
            this.selectedPhone = selectedPhone;

            // Display selected phone details
            if (selectedPhone != null)
            {
                phoneNameLabel.Text = selectedPhone.Name;
                phoneModelLabel.Text = selectedPhone.Model;
                phonePriceLabel.Text = selectedPhone.Price.ToString("C"); // Assuming Price is a property of the Phones class
            }
        }

        public int PhoneId { get; internal set; }

        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            // Perform payment processing (dummy implementation)
            bool paymentSuccess = await ProcessPayment();

            if (paymentSuccess)
            {
                // Payment successful, navigate to a confirmation page or display a success message
                await DisplayAlert("Success", "Payment successful!", "OK");
            }
            else
            {
                // Payment failed, display an error message
                await DisplayAlert("Error", "Payment failed. Please try again later.", "OK");
            }
        }

        private async Task<bool> ProcessPayment()
        {
            // Simulate payment processing with a delay
            await Task.Delay(2000); // Simulate 2 seconds delay for payment processing

            // For demo purposes, assume payment is successful 80% of the time
            Random random = new Random();
            bool paymentSuccess = random.Next(10) < 8; // 80% success rate

            return paymentSuccess;
        }
    }
}

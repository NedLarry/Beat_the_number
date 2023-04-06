using Beat_The_Number.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Beat_The_Number
{
    public partial class MainPage : ContentPage
    {

        string introText = 
            $"Given a grid on numbers which represent indexes.\r" +
            $"You have to choose the right index which reveals a number\r" +
            $"which if greater than your value shortens your number of guesses left;\r" +
            $" If less or equal than your value, adds to your value.\r" +
            $"You win when your number value is greater than the given Target value.\r\n\n" +
            $"Enjoy :)";

        public MainPage()
        {
            InitializeComponent();
            introLbl.Text = introText;
            
        }


        private async void enterBtnClicked(object sender, EventArgs e) => await Navigation.PushAsync(new GamePage());
    }
}

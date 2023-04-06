using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beat_The_Number.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WinLosePage : ContentPage
    {
        public WinLosePage(string winLoseStatement)
        {
            InitializeComponent();

            winLoseLbl.Text = winLoseStatement;
        }

        private async void startNewBtnClicked (object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new GamePage());
            Navigation.RemovePage(this);
        }
        private async void mainPageBtnClicked (object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
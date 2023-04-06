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

            //var thisPage = this;
            //Navigation.RemovePage(thisPage);
        }
        private async void mainPageBtnClicked (object sender, EventArgs e)
        {

            await Navigation.PopToRootAsync();

            //var thisPage = this;


            //Navigation.RemovePage(thisPage);

        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
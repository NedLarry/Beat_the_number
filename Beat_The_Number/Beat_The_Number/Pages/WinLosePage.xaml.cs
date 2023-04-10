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

        private async void startNewBtnClicked (object sender, EventArgs e) => await MoveToNexPage(this, new GamePage());

        private async void mainPageBtnClicked(object sender, EventArgs e) => await MoveToNexPage(this, null);

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async Task MoveToNexPage(Page currentPage, Page nextPage = null)
        {

            if (nextPage == null)
            {
                await Navigation.PopToRootAsync();
                return;
            }

            await Navigation.PushAsync(nextPage);

            await Task.Run(() => Navigation.RemovePage(currentPage));
        }
    }
}
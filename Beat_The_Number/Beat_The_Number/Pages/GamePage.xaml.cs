using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beat_The_Number.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {

        const int ARRAY_SIZE = 9;

        readonly int TARGET_VALUE;

        int NUMBER_OF_GUESSES = 3;

        int UPPERBOUND = 50;

        int LOWERBOUND = 10;

        List<int> GameNumbers = new List<int>();

        public GamePage()
        {
            InitializeComponent();

            GameNumbers = InitializeAndRandomizeGameNumbers();

            TARGET_VALUE = GameNumbers[new Random().Next(ARRAY_SIZE)];

            targetValueLbl.Text = TARGET_VALUE.ToString();

            GameLayout.RowSpacing = 10;
            GameLayout.ColumnSpacing = 10;

            guessesLbl.Text = NUMBER_OF_GUESSES.ToString();

            playerValueLbl.Text = RandomizePlayerValue();


        }

        //Button methods
        private async void btnR0C0Clicked (object sender, EventArgs e) => await HandleClickedButton(sender);

        private async void btnR0C1Clicked(object sender, EventArgs e)=> await HandleClickedButton(sender);

        private async void btnR0C2Clicked(object sender, EventArgs e) => await HandleClickedButton(sender);

        private async void btnR1C0Clicked(object sender, EventArgs e) => await HandleClickedButton(sender);

        private async void btnR1C1Clicked(object sender, EventArgs e) => await HandleClickedButton(sender);

        private async void btnR1C2Clicked(object sender, EventArgs e) => await HandleClickedButton(sender);

        private async void btnR2C0Clicked(object sender, EventArgs e) => await HandleClickedButton(sender);

        private async void btnR2C1Clicked(object sender, EventArgs e) => await HandleClickedButton(sender);

        private async void btnR2C2Clicked(object sender, EventArgs e) => await HandleClickedButton(sender);


        //Helper methods
        List<int> InitializeAndRandomizeGameNumbers()
        {

            List<int> numbersToReturn = new List<int>();

            while (numbersToReturn.Count < ARRAY_SIZE)
            {
                NUMBER_GENERATOR:
                int numberToAdd = new Random().Next(LOWERBOUND, UPPERBOUND);

                if (numberToAdd == LOWERBOUND) goto NUMBER_GENERATOR;

                if (!numbersToReturn.Contains(numberToAdd))
                {
                    numbersToReturn.Add(numberToAdd);
                }
            }

            return numbersToReturn;
        }


        string RandomizePlayerValue()
        {

            NUMBER_GENERATOR:
            int randomValue = GameNumbers[new Random().Next(ARRAY_SIZE)];


            if (randomValue == TARGET_VALUE) goto NUMBER_GENERATOR;

            else if (randomValue > TARGET_VALUE) goto NUMBER_GENERATOR;

            return randomValue.ToString();


        }

        private async Task UpdateGameStatus(int index, Button clickedButton)
        {
            int playerValue = int.Parse(playerValueLbl.Text);

            clickedButton.Text = index.ToString();

            string OUT_OF_GUESSESS = $"GAMEOVER.\r\n" +
                $"You're out of Guesses.\r\n";

            if (NUMBER_OF_GUESSES != 0)
            {
                if (index == playerValue)
                {

                    clickedButton.BackgroundColor = Color.FromHex("646E78");
                    clickedButton.TextColor = Color.White;
                    NUMBER_OF_GUESSES += 1;
                    guessesLbl.Text = NUMBER_OF_GUESSES.ToString();

                }

                else if (index > playerValue)
                {
                    NUMBER_OF_GUESSES -= 1;

                    clickedButton.BackgroundColor = Color.Red;
                    guessesLbl.Text = NUMBER_OF_GUESSES.ToString();
                }
                else
                {
                    clickedButton.BackgroundColor = Color.Green;

                    int newPlayerValue = (index + playerValue);

                    playerValueLbl.Text = newPlayerValue.ToString();

                    if (newPlayerValue > TARGET_VALUE)
                    {

                        string WIN_STATEMENT = $"YOU WIN.\r\n" +
                            $"Target value: {TARGET_VALUE}\r\n" +
                            $"Your Value: {newPlayerValue}";

                        await MoveToNexPage(this, new WinLosePage(WIN_STATEMENT));

                    }


                }


            }
            else
            {
                await MoveToNexPage(this, new WinLosePage(OUT_OF_GUESSESS));

            }

            clickedButton.IsEnabled = false;
        }

        private async void ReloadBtnClicked (object sender, EventArgs e) => await MoveToNexPage(this, new GamePage());


        private async void MainPageBtnClicked(object sender, EventArgs e) => await MoveToNexPage(this, null);

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

        private async Task HandleClickedButton(object sender)
        {

            Button btnObject = sender as Button;

            int newNumber = GameNumbers[int.Parse(btnObject.Text)];

            await UpdateGameStatus(newNumber, btnObject);
        }
    }
}
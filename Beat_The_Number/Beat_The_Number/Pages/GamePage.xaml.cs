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

        List<int> GameNumbers = new List<int>();

        public GamePage()
        {
            InitializeComponent();

            GameNumbers = InitializeAndRandomizeGameNumbers();

            TARGET_VALUE = (int)GameNumbers.Average();

            GameLayout.RowSpacing = 10;
            GameLayout.ColumnSpacing = 10;

            guessesLbl.Text = NUMBER_OF_GUESSES.ToString();
            //targetValueLbl.Text = GameNumbers.Sum().ToString();
            targetValueLbl.Text = ((int)GameNumbers.Average()).ToString();
            playerValueLbl.Text = RandomizePlayerValue();


        }


        List<int> InitializeAndRandomizeGameNumbers()
        {
            if(GameNumbers.Any())
                GameNumbers.Clear();    

            List<int> numbersToReturn = new List<int>();

            int upperBound = new Random().Next(40);

            while (numbersToReturn.Count != ARRAY_SIZE)
            {
                int numberToAdd = new Random().Next(upperBound);
                //int numberToAdd = new Random().Next(new Random().Next(10), new Random().Next(10));

                if (numberToAdd == 0) continue;

                if (!numbersToReturn.Contains(numberToAdd))
                {
                    numbersToReturn.Add(numberToAdd);
                }
            }

            return numbersToReturn;
        }

        string RandomizePlayerValue()
        {
        

            int randomValue = GameNumbers.Sum();

            while (randomValue > TARGET_VALUE)
            {
                 randomValue = GameNumbers[new Random().Next(ARRAY_SIZE)];

                if (randomValue == 0 || randomValue <= 4) continue;

                if (randomValue == TARGET_VALUE) continue;

                if (randomValue < TARGET_VALUE ) break;

            }

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
                    NUMBER_OF_GUESSES += 1;
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

                        await Navigation.PushAsync(new WinLosePage(WIN_STATEMENT));
                        Navigation.RemovePage(this);

                    }


                }


            }
            else
            {
                await Navigation.PushAsync(new WinLosePage(OUT_OF_GUESSESS));
                Navigation.RemovePage(this);

            }

            clickedButton.IsEnabled = false;
        }


        private async void btnR0C0Clicked (object sender, EventArgs e)
        {

            Button btnObject = (Button)sender;

            int newNumber = GameNumbers[int.Parse(btnObject.Text)];

            await UpdateGameStatus(newNumber, btnR0C0);

        }

        private async void btnR0C1Clicked(object sender, EventArgs e)
        {

            Button btnObject = (Button)sender;

            int newNumber = GameNumbers[int.Parse(btnObject.Text)];

            await UpdateGameStatus(newNumber, btnR0C1);

        }

        private async void btnR0C2Clicked(object sender, EventArgs e)
        {

            Button btnObject = (Button)sender;
            int newNumber = GameNumbers[int.Parse(btnObject.Text)];

            await UpdateGameStatus(newNumber, btnR0C2);

        }

        private async void btnR1C0Clicked(object sender, EventArgs e)
        {

            Button btnObject = (Button)sender;
            int newNumber = GameNumbers[int.Parse(btnObject.Text)];

            await UpdateGameStatus(newNumber, btnR1C0);

        }

        private async void btnR1C1Clicked(object sender, EventArgs e)
        {

            Button btnObject = (Button)sender;
            int newNumber = GameNumbers[int.Parse(btnObject.Text)];

            btnR1C1.Text = newNumber.ToString();

            await UpdateGameStatus(newNumber, btnR1C1);

        }

        private async void btnR1C2Clicked(object sender, EventArgs e)
        {

            Button btnObject = (Button)sender;
            int newNumber = GameNumbers[int.Parse(btnObject.Text)];

            await UpdateGameStatus(newNumber, btnR1C2);

        }

        private async void btnR2C0Clicked(object sender, EventArgs e)
        {


            Button btnObject = (Button)sender;
            int newNumber = GameNumbers[int.Parse(btnObject.Text)];

            await UpdateGameStatus(newNumber, btnR2C0);

        }

        private async void btnR2C1Clicked(object sender, EventArgs e)
        {

            Button btnObject = (Button)sender;
            int newNumber = GameNumbers[int.Parse(btnObject.Text)];

            await UpdateGameStatus(newNumber, btnR2C1);

        }

        private async void btnR2C2Clicked(object sender, EventArgs e)
        {

            Button btnObject = (Button)sender;
            int newNumber = GameNumbers[int.Parse(btnObject.Text)];

            await UpdateGameStatus(newNumber, btnR2C2);


        }

        private async void ReloadBtnClicked (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
            Navigation.RemovePage(this);

        }


        private async void MainPageBtnClicked(object sender, EventArgs e) => await Navigation.PopToRootAsync();

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
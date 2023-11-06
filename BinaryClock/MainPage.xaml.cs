using System.Diagnostics;
using System.Text;

namespace BinaryClock
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        void OnConvertClick(object sender, EventArgs e)
        {
            Console.WriteLine(TextEntry.Text);
        }

        void CheckIfValidInput(object sender, EventArgs e)
        {
            string text = TextEntry.Text;
            //string letters = new string(TextEntry.Text.Where(char.IsLetter).ToArray());
            bool NoBueno = false;
            foreach (char c in text)
                if (!char.IsDigit(c) && c != ':')
                    NoBueno = true;

            if(NoBueno)
                TextEntry.TextColor = new Color(255, 0, 0);
            else
                TextEntry.TextColor = new Color(0, 0, 0);
            

        }


    }
}
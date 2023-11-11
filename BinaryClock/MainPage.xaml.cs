using System.Diagnostics;
using System;
using System.Timers;
using static System.Net.Mime.MediaTypeNames;

namespace BinaryClock
{
    public partial class MainPage : ContentPage
    {

        System.Timers.Timer timer;
        List<Border> Hour1 = new List<Border>();
        List<Border> Hour2 = new List<Border>();

        List<Border> Minute1 = new List<Border>();
        List<Border> Minute2 = new List<Border>();

        List<Border> Second1 = new List<Border>();
        List<Border> Second2 = new List<Border>();

        Color On, Off;
        /// <summary>
        /// Adaug toate elementele in listele specifice
        /// </summary>
        void Init()
        {
            Hour1.Add(Hour0_1);
            Hour1.Add(Hour0_2);
            Hour2.Add(Hour1_1);
            Hour2.Add(Hour1_2);
            Hour2.Add(Hour1_3);
            Hour2.Add(Hour1_4);


            Minute1.Add(Minute0_1);
            Minute1.Add(Minute0_2);
            Minute1.Add(Minute0_3);
            Minute2.Add(Minute1_1);
            Minute2.Add(Minute1_2);
            Minute2.Add(Minute1_3);
            Minute2.Add(Minute1_4);

            Second1.Add(Seconds0_1);
            Second1.Add(Seconds0_2);
            Second1.Add(Seconds0_3);
            Second2.Add(Seconds1_1);
            Second2.Add(Seconds1_2);
            Second2.Add(Seconds1_3);
            Second2.Add(Seconds1_4);


        }


        public MainPage()
        {
            InitializeComponent();
            Init();
            On = new Color(191, 64, 191);
            Off = new Color(108, 108, 108);
        }

        /// <summary>
        /// IsValid stocheaza daca inputul este valid sau nu
        /// h1, h2, m1, m2, s1, s2 stocheaza valorile binare ale timpului
        /// </summary>
        bool IsValid = true;
        string h1, h2, m1, m2, s1, s2;

        void ConvertToBinary()
        {
            if (!IsValid || OnTime)
                return;
            string text = TextEntry.Text;
            //verific daca textul are lungimea necesara pentru a fi un timp valid
            if (text.Length < 8)
                return;
            //impart textul in 3 stringuri pentru a putea fi convertite in binar
            SplitTime(text);

            //setez ceasul
            SetHour(h1, h2);
            SetMinute(m1, m2);
            SetSeconds(s1, s2);
            //Trace.WriteLine(s1 + " " + s2);
        }

        /// <summary>
        /// Imparte stringul "text" in 3 stringuri pentru a putea fi convertite in binar
        /// </summary>
        /// <param name="text"></param>
        void SplitTime(string text)
        {
            string[] split = text.Split(':');  // dau split stringul dupa ':'

            
            int hour1 = int.Parse(split[0][0].ToString());
            int hour2 = int.Parse(split[0][1].ToString());
            

            h1 = ConvertToDecimal(hour1);
            h2 = ConvertToDecimal(hour2);

            int min1 = int.Parse(split[1][0].ToString());
            int min2 = int.Parse(split[1][1].ToString());

            m1 = ConvertToDecimal(min1);
            m2 = ConvertToDecimal(min2);

            int sec1 = int.Parse(split[2][0].ToString());
            int sec2 = int.Parse(split[2][1].ToString());

            s1 = ConvertToDecimal(sec1);
            s2 = ConvertToDecimal(sec2);
        }

        #region UI
        //Functiile seateaza backgroundul elementelor in functie de stringul binar
        void SetHour(string h1, string h2)
        {
            for(int i=0;i<h1.Length;i++)
            {
                if (h1[i] == '1')
                {
                    Hour1[i].Background = On;
                }
            }
            for (int i = 0; i < h2.Length; i++)
            {
                if (h2[i] == '1')
                {
                    Hour2[i].Background = On;
                }
            }
        }
        void SetMinute(string m1, string m2)
        {
            for (int i = 0; i < m1.Length; i++)
            {
                if (m1[i] == '1')
                {
                    Minute1[i].Background = On;
                }
            }
            for (int i = 0; i < m2.Length; i++)
            {
                if (m2[i] == '1')
                {
                    Minute2[i].Background = On;
                }
            }
        }
        void SetSeconds(string s1, string s2)
        {
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] == '1')
                {
                    Second1[i].Background = On;
                }
            }
            for (int i = 0; i < s2.Length; i++)
            {
                if (s2[i] == '1')
                {
                    Second2[i].Background = On;
                }
            }
        }
        //Se reseteaza backgroundul tuturor elementelor
        void ClearScreen()
        {
            foreach(var item in Hour1)
                item.Background = Off;
            foreach (var item in Hour2)
                item.Background = Off;
            foreach (var item in Minute1)
                item.Background = Off;
            foreach (var item in Minute2)
                item.Background = Off;
            foreach (var item in Second1)
                item.Background = Off;
            foreach (var item in Second2)
                item.Background = Off;
        }
        #endregion UI
        //Conversia din decimal in binar
        String ConvertToDecimal(int nr)
        {
            string toRet = "";
            while(nr > 0)
            {
                toRet += (nr%2).ToString();
                nr /= 2;
            }
            return toRet;
        }

        /// <summary>
        /// Verific daca inputul este valid
        /// </summary>
        void CheckIfValidInput(object sender, EventArgs e)
        {
            if (OnTime)
                return;
            
            ClearScreen();
            string text = TextEntry.Text;
            //string letters = new string(TextEntry.Text.Where(char.IsLetter).ToArray());
            IsValid = true;
            foreach (char c in text)
                if (!char.IsDigit(c) && c != ':') // verific daca userul a introdus doar cifre si :
                    IsValid = false;
            if (text.Length >= 1) // daca ora incepe cu 3x:xx:xx sau mai mare
            {
                if (text[0] > '2' && text[0] != '0')
                    IsValid = false;
            }
            if (text.Length >= 2) // daca ora are forma x5:xx:xx sau mai mare
            {
                if (text[0] > '2'&& text[0] != '0')
                    IsValid = false;
                if (text[0] == '2' && (text[1] > '3' && text[1] != '0'))
                    IsValid = false;
            }
            if (text.Length >= 4)
            {
                if (text[3] > '5') // daca minutele incep cu xx:5x:xx sau mai mare
                    IsValid = false;
            }
            if(text.Length >= 7)
            {
                if (text[6] > '5')
                    IsValid = false;
            }
            

            if (!IsValid)
                TextEntry.TextColor = new Color(255, 0, 0);
            else
                TextEntry.TextColor = new Color(0, 0, 0);

            if (text.Length > 7 && IsValid)
                ConvertToBinary();

        }

        //Functiile necesare pentru a afisa ora curenta
        #region CurrentTime
        bool OnTime = false;
        /// <summary>
        /// Functia este apelata de butonul "Current Hour"
        /// </summary>
        void ChangeToCurrentTime(object sender, EventArgs e)
        {
            if(OnTime)
            {
                OnTime = false;
                timer.Stop();
                CurrentTime.Text = "Current Hour";
                ClearScreen();
                return;
            }
            OnTime = true;
            ClearScreen();
            TextEntry.Text = "";
            CurrentTime.Text = "Stop";
            

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += FireMainThread;
            timer.AutoReset = true;
            timer.Enabled = true;
            Trace.WriteLine("Timer started");
        }

        //Timerul functineaza pe un thread separat, asa ca trebuie sa apelez functia pe MainThread (UI se poate schimba doar pe MainThread)
        void FireMainThread(object source, ElapsedEventArgs e)
        {
            Action a = () => GetHour();
            MainThread.BeginInvokeOnMainThread(a);
        }
        /// <summary>
        /// Seteaza ora curenta
        /// </summary>
        void GetHour()
        {
            ClearScreen();
            SplitTime(DateTime.Now.ToString("HH:mm:ss"));
            SetHour(h1, h2);
            SetMinute(m1, m2);
            SetSeconds(s1, s2); 
        }
        #endregion CurrentTime
    }
}
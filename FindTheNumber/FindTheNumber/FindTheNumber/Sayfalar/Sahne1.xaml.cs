using Android.Media;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FindTheNumber;

namespace FindTheNumber.Sayfalar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Sahne1 : ContentPage
	{
     public   int toplam = 0; //basilan sayilarin toplami
        int sayi = 0; //basilan tuslar
        int sonuc = 0; //erişilecek canlar
        bool operatorsecilimi = false; //operator tuslara basildi
        int canlar = 5; //can hakkı
        int skor = 0;
        int kutularinsayisi = 0;

        bool islem_topla = false;
        bool islem_cikarma = false;
        bool islem_carpma = false;
        bool islem_bolme = false;

        //MediaPlayer ses_kaybetti;
        //MediaPlayer ses_kazandi;
        //MediaPlayer ses_operator;

        public Sahne1 ()
		{
			InitializeComponent ();

            //var dd = Resources.Count;

            //ses_kaybetti = MediaPlayer.Create(this,Resources  );
            Temizle();
        }
        public void Temizle()
        {
            toplam = 0;
            sonuc = 0;
            lblislem.Text = "";
            lblsonuc.Text = "";
            

            Random random = new Random();
            sonuc = random.Next(20,500);
            lblsayi.Text =Convert.ToString(sonuc);
        }
        void Cansayisi()
        {
            //SkorHesapla();
            canlar -= 1;
            Temizle();
            if (skor > 100 && skor < 500)
                {skor -= 100;}
            if (skor>500 && skor<1000)
                { skor -= 500;}
            if (skor >1000 && skor < 1500)
                {skor -= 1000; }

            skor -= 1000;
            Android.Widget.Toast.MakeText(Android.App.Application.Context, "Kaybettin !!! Kalan Can Sayısı " + canlar, Android.Widget.ToastLength.Long).Show();
            switch (canlar)
            {
                case 4:
                    imgbes.Source = "Hearts32x3232gray.png";
                    break;
                case 3:
                    imgdort.Source = "Hearts32x3232gray.png";
                    break;
                case 2:
                    imguc.Source = "Hearts32x3232gray.png";
                    break;
                case 1:
                    imgiki.Source = "Hearts32x3232gray.png";
                    break;
                case 0:
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var sor = await DisplayAlert("Oyun Bitti", "Oyunu kaybettin. Yeni oyun oynamak ister misin?", "Evet", "Hayır");
                        if (sor)
                        {
                            Temizle();
                            skor = 0;
                            SkorHesapla();
                            Cansayisi();
                            imgbes.Source = "Hearts32x3232.png";
                            imgdort.Source = "Hearts32x3232.png";
                            imguc.Source = "Hearts32x3232.png";
                            imgiki.Source = "Hearts32x3232.png";
                            imgbir.Source = "Hearts32x3232.png";
                        }
                        else
                            imgbir.Source = "Hearts32x3232gray.png";
                    });

                    break;
            }
        }

        void SkorHesapla()
        {
            int kalankutu=0;
            int kullankutu=0;
            int superskor = 1000;
            int kackutukullandi = 0;

            int tt = butonlarinhepsi.Children.Count;
            for (int t = 0; t < tt; t++)
            {
                Button myButton = (Button)butonlarinhepsi.Children[t];
                if (myButton.BackgroundColor == Color.Gray)
                {
                    kackutukullandi += 1;
                    string dgr = myButton.Text;
                    kullankutu += Convert.ToInt32(dgr);
                }
                if (myButton.BackgroundColor == Color.Black)
                {
                    string dgr = myButton.Text;
                    kalankutu += Convert.ToInt32(dgr);
                }
            }

            {
            if (kalankutu == 0)
                   skor = ((kullankutu * 10)  * superskor);
            else
                {
                    if (canlar == 0)
                    {
                        if (kalankutu<kullankutu)
                            skor = ((kullankutu * 10) + (kackutukullandi * 10)) - (kalankutu * 50);
                        else
                            skor = (kullankutu * 10) - (kalankutu * 5);
                    }
                    else
                    {
                        if (kackutukullandi<4)
                            skor += (kullankutu * 10) + (kackutukullandi * 100);
                        else
                            skor += (kullankutu * 10) + (kackutukullandi * 10);
                    }
                }

            }

            lblskor.Text = Convert.ToString(skor);
        }
        void SayiKutulariniSifirla()
        {
            kutularinsayisi = 0;
            int sayilarikutularadoldur = 0;
            int toplambutonsayisi = butonlarinhepsi.Children.Count;
            for (int t = 0; t < toplambutonsayisi; t++)
            {
                Button sayibutonu = (Button)butonlarinhepsi.Children[t];
                {

                    if (sayilarikutularadoldur < 11)
                        sayilarikutularadoldur += 1;
                    else
                        sayilarikutularadoldur += 3;
                }
                sayibutonu.BackgroundColor = Color.Black;
                sayibutonu.TextColor = Color.White;
                sayibutonu.Text = Convert.ToString(sayilarikutularadoldur);
            }
        }
        
        private void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            
            string basilantus = button.Text;
            if (button.BackgroundColor == Color.Gray)
            {
                Android.Widget.Toast.MakeText(Android.App.Application.Context, "Bu sayıyı kullandın. Aynı sayıyı kullanamazsın.", Android.Widget.ToastLength.Long).Show();
                return;
            }

            if (operatorsecilimi==false && toplam>0)
            {
                return;
            }
            operatorsecilimi = false;
            lblislem.Text += basilantus;

            button.BackgroundColor = Color.Gray;
            button.TextColor = Color.Black;
            sayi = Convert.ToInt32(basilantus);
            kutularinsayisi += 1;
            {
                if (lblislem.Text.Length > 20)
                {
                    if (lblislem.Text.Length > 35)
                        lblislem.FontSize = 12;
                    else
                        lblislem.FontSize = 15;
                }
                else
                    lblislem.FontSize =24;
            }
            if (kutularinsayisi==40)
            {
                bool tekraroyna=false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    var sor = await DisplayAlert("Oyun Bitti", "Oyunu kaybettin. Yeniden oyun oynamak ister misin?", "Evet", "Hayır");
                    if (sor)
                    {
                        tekraroyna = true;
                    }

                    {
                        if (lblislem.Text.Length > 0)
                        {
                            Cansayisi();
                            if (tekraroyna)
                            {
                                SayiKutulariniSifirla();
                            }
                            return;
                        }
                        else
                            Temizle();
                        if (tekraroyna)
                        {
                            SayiKutulariniSifirla();
                        }
                        return;
                    }
                });

            }

            if (toplam > 0 && islem_carpma)
            {
                toplam *= sayi;
                islem_carpma = false;
            }

            if (toplam > 0 && islem_topla)
            {
                toplam += sayi;
                islem_topla = false;
            }

            if (toplam > 0 && islem_cikarma)
            {
                toplam -= sayi;
                islem_cikarma = false;
            }

            if (toplam > 0 && islem_bolme)
            {
                toplam /= sayi;
                islem_bolme = false;
            }

            if (toplam == 0)
            {
                toplam = sayi;
            }
            lblsonuc.Text = toplam.ToString();
        }

        private void Button_Operator_Clicked(object sender, EventArgs e)
        {
            if(lblislem.Text=="")
            { return; }
            operatorsecilimi = true;
            Button button = (Button)sender;
            string basilantus = button.Text;
            if (sayi != 0)
            {
                string strislem = lblislem.Text;
                if((islem_bolme==false && islem_carpma==false && islem_cikarma==false && islem_topla == false) || strislem.Substring(strislem.Length-1,1)!=basilantus)
                {
                    {
                        string strislem2 = strislem.Substring(strislem.Length - 1, 1);

                        if (strislem2=="*" || strislem2=="/" || strislem2 == "-" || strislem2 == "+")
                            lblislem.Text = strislem.Substring(0, strislem.Length - 1) + basilantus;
                        else
                            lblislem.Text += basilantus;
                    }

                }
            }
            switch (basilantus)
            {
                case "+":
                    if (toplam > 0)
                    {
                        islem_topla = true;
                        islem_cikarma = false;
                        islem_carpma = false;
                        islem_bolme = false;
                    }
                    break;
                case "-":
                    if (toplam > 0)
                    {
                        islem_topla = false;
                        islem_cikarma = true;
                        islem_carpma = false;
                        islem_bolme = false;
                    }
                    break;
                case "*":
                    if (toplam > 0)
                    {
                        islem_topla = false;
                        islem_cikarma = false;
                        islem_carpma = true;
                        islem_bolme = false;
                    }
                    break;
                case "/":
                    if (toplam > 0)
                    {
                        islem_topla = false;
                        islem_cikarma = false;
                        islem_carpma = false;
                        islem_bolme = true;
                    }
                    break;
                case "=":
                    if (sonuc == toplam)
                    {
                        Temizle();
                        SkorHesapla();
                        Android.Widget.Toast.MakeText(Android.App.Application.Context, "Tebrikler kazandınız. Puanınız " + skor, Android.Widget.ToastLength.Long).Show();
                        if (kutularinsayisi==40)
                        {
                            SayiKutulariniSifirla();
                        }
                        return;
                    }
                    
                    if (sonuc < toplam)
                    {
                        Cansayisi();
                        return;
                    }
                    if (sonuc > toplam)
                    {
                        if (kutularinsayisi == 40)
                        {
                            Temizle();
                            Cansayisi();
                            SayiKutulariniSifirla();
                        }
                            operatorsecilimi = false;
                        if (lblislem.Text.Length>1 )
                        { 
                        string esittirisil = lblislem.Text;
                        lblislem.Text = esittirisil.Substring(0, esittirisil.Length - 1);
                        Android.Widget.Toast.MakeText(Android.App.Application.Context, "Ulaşılacak sayıya daha " + (sonuc-toplam) +" sayı var.", Android.Widget.ToastLength.Long).Show();
                        return;
                        }
                    }
                    break;
            }

        }

    }
}
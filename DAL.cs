using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GrafikerPortal
{
    public class DAL
    {
        SqlConnection Baglanti = null;


        /// <summary>
        /// Veritabanı bağlantı bilgisini alarak SqlConnection nesnesini yapılandırır.
        /// </summary>
        /// <param name="Veritabani">Bağlanılacak veritabanının kodunu giriniz.</param>
        public DAL(string ConnectionString = "")
        {
            //Eğer bir bağlantı cümlesi gönderilmemişse varsayılan bilgiler ile bağlanılıyor.
            if (ConnectionString == "")
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["grafikerportalConnectionString"].ConnectionString.ToString();
            }
            Baglanti = new SqlConnection(ConnectionString);
        }

        protected enum BaglantiDurumu
        {
            Aç,
            Kapat
        };


        /// <summary>
        /// Veritabanı ile bağlantı açar ve kapatır.
        /// </summary>
        /// <param name="durum">Bağlantı durumunu açma ve kapama işlemlerinden hangisinin yapılacağını seçiniz.</param>
        protected void BaglantiAcKapa(BaglantiDurumu durum)
        {
            switch (durum)
            {
                case BaglantiDurumu.Aç:
                    if (Baglanti.State != ConnectionState.Open)
                    {
                        Baglanti.Open();
                    }
                    break;
                case BaglantiDurumu.Kapat:
                    if (Baglanti.State != ConnectionState.Closed)
                    {
                        Baglanti.Close();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Girilen sorguyu, sağlanan parametrelere göre işler ve DataTable'a yükleyerek döndürür.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <param name="parametreler">Sorgunun alacağı parametreleri, sorgudaki sırayla giriniz.</param>
        /// <returns>Fonksiyon, DataTable tipinde sonuç döndürür.</returns>
        public DataTable Sorgu_DataTable(string sorgu, params string[] parametreler)
        {
            //Sağlanan sorgunun içerdiği parametreler saptanıyor.
            string[] dizi_parametreler = parametreleri_bul(sorgu);
            BaglantiAcKapa(BaglantiDurumu.Aç);
            DataTable Tablo = new DataTable();
            SqlCommand Komut = new SqlCommand(sorgu, Baglanti);
            //Sorgu için sağlanan parametreler, sorguya işleniyor.
            int sayac = 0;
            foreach (string parametre in parametreler)
            {
                Komut.Parameters.AddWithValue(dizi_parametreler[sayac], parametre);
                sayac++;
            }
            SqlDataAdapter Adaptor = new SqlDataAdapter(Komut);
            Adaptor.Fill(Tablo);
            BaglantiAcKapa(BaglantiDurumu.Kapat);
            return Tablo;
        }

        /// <summary>
        /// Girilen sorguyu çalıştırır ve sorgudan etkilenen satır sayısını döndürür. 
        /// INSERT, UPDATE ve DELETE sorguları için kullanışlıdır.
        /// Döndürülen sonuç 0'dan büyükse, sorgu başarıyla işlenmiş demektir.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <returns>Fonksiyon, int tipinde sonuç döndürür.</returns>
        public int Sorgu_Calistir(string sorgu)
        {
            int etkilenen_satir_sayisi;
            try
            {
                BaglantiAcKapa(BaglantiDurumu.Aç);
                SqlCommand Komut = new SqlCommand(sorgu, Baglanti);
                etkilenen_satir_sayisi = Komut.ExecuteNonQuery(); //Sorgu çalıştırılıyor.
                BaglantiAcKapa(BaglantiDurumu.Kapat);
            }
            catch (Exception hata)
            {
                string HataMesaji = hata.Message;
                etkilenen_satir_sayisi = 0;
            }
            return etkilenen_satir_sayisi;
        }

        /// <summary>
        /// Girilen sorguyu, sağlanan parametleri işleyerek çalıştırır ve sorgudan etkilenen satır sayısını döndürür. 
        /// INSERT, UPDATE ve DELETE sorguları için kullanışlıdır.
        /// Döndürülen sonuç 0'dan büyükse, sorgu başarıyla işlenmiş demektir.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <param name="parametreler">Sorgunun alacağı parametreleri, sorgudaki sırayla giriniz.</param>
        /// <returns>Fonksiyon, int tipinde sonuç döndürür.</returns>
        public int Sorgu_Calistir(string sorgu, params string[] parametreler)
        {
            //Sağlanan sorgunun içerdiği parametreler saptanıyor.
            string[] dizi_parametreler = parametreleri_bul(sorgu);
            int etkilenen_satir_sayisi;
            try
            {
                BaglantiAcKapa(BaglantiDurumu.Aç);
                SqlCommand Komut = new SqlCommand(sorgu, Baglanti);
                //Sorgu için sağlanan parametreler, sorguya işleniyor.
                int sayac = 0;
                foreach (string parametre in parametreler)
                {
                    Komut.Parameters.AddWithValue(dizi_parametreler[sayac], parametre);
                    sayac++;
                }
                etkilenen_satir_sayisi = Komut.ExecuteNonQuery(); //Sorgu çalıştırılıyor.
                BaglantiAcKapa(BaglantiDurumu.Kapat);
            }
            catch (Exception hata)
            {
                string HataMesaji = hata.Message;
                etkilenen_satir_sayisi = 0;
            }
            return etkilenen_satir_sayisi;
        }

        /// <summary>
        /// Girilen sorguyu, sağlanan parametleri işleyerek çalıştırır ve eklenen kaydın id numarasını döndürür. 
        /// INSERT, UPDATE ve DELETE sorguları için kullanışlıdır.
        /// Döndürülen sonuç 0'dan büyükse, sorgu başarıyla işlenmiş demektir.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <param name="parametreler">Sorgunun alacağı parametreleri, sorgudaki sırayla giriniz.</param>
        /// <returns>Fonksiyon, int tipinde sonuç döndürür.</returns>
        public int Sorgu_Calistir_Eklenen_Id_Dondur(string sorgu, params string[] parametreler)
        {
            //Sağlanan sorgunun içerdiği parametreler saptanıyor.
            string[] dizi_parametreler = parametreleri_bul(sorgu);
            int eklenen_id = 0;
            string ParametreLog = "";
            try
            {
                BaglantiAcKapa(BaglantiDurumu.Aç);
                SqlCommand Komut = new SqlCommand(sorgu, Baglanti);
                //Sorgu için sağlanan parametreler, sorguya işleniyor.
                int sayac = 0;
                foreach (string parametre in parametreler)
                {
                    Komut.Parameters.AddWithValue(dizi_parametreler[sayac], parametre);
                    sayac++;
                    ParametreLog += "/ " + parametre;
                }
                Komut.ExecuteNonQuery(); //Sorgu çalıştırılıyor.
                //Eklenen kaydın ID'si tespit ediliyor.
                SqlCommand Komut_eklenen = new SqlCommand("SELECT @@IDENTITY", Baglanti);
                SqlDataReader Komut_okuyucu = Komut_eklenen.ExecuteReader();
                while (Komut_okuyucu.Read())
                {
                    eklenen_id = Convert.ToInt32(Komut_okuyucu[0].ToString());
                }
                BaglantiAcKapa(BaglantiDurumu.Kapat);
            }
            catch (Exception Hata)
            {
                string HataString = Hata.ToString() + " --- " + sorgu + " --- " + ParametreLog;
                eklenen_id = 0;
                this.Sorgu_Calistir("INSERT INTO LogHata(Hatalar) VALUES(@Hata)", HataString);
            }
            return eklenen_id;
        }


        /// <summary>
        /// Girilen sorguyu çalıştırır ve tespit edilen tablonun ilk satırının ilk sütununu döndürür.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <returns>Fonksiyon, string tipinde sonuç döndürür.</returns>
        public string Sorgu_Scalar(string sorgu)
        {
            string cikti;
            try
            {
                BaglantiAcKapa(BaglantiDurumu.Aç);
                SqlCommand Komut = new SqlCommand(sorgu, Baglanti);
                cikti = Komut.ExecuteScalar().ToString(); //Sorgu çalıştırılıyor.
                BaglantiAcKapa(BaglantiDurumu.Kapat);
            }
            catch (Exception hata)
            {
                string HataMesaji = hata.Message;
                cikti = "";
            }
            return cikti;
        }

        /// <summary>
        /// Girilen sorguyu çalıştırır ve tespit edilen tablonun ilk satırının ilk sütununu döndürür.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <param name="parametreler">Sorgunun alacağı parametreleri, sorgudaki sırayla giriniz.</param>
        /// <returns>Fonksiyon, string tipinde sonuç döndürür.</returns>
        public string Sorgu_Scalar(string sorgu, params string[] parametreler)
        {
            string cikti;
            try
            {
                //Sağlanan sorgunun içerdiği parametreler saptanıyor.
                string[] dizi_parametreler = parametreleri_bul(sorgu);
                BaglantiAcKapa(BaglantiDurumu.Aç);
                SqlCommand Komut = new SqlCommand(sorgu, Baglanti);
                //Sorgu için sağlanan parametreler, sorguya işleniyor.
                int sayac = 0;
                foreach (string parametre in parametreler)
                {
                    Komut.Parameters.AddWithValue(dizi_parametreler[sayac], parametre);
                    sayac++;
                }
                cikti = Komut.ExecuteScalar().ToString(); //Sorgu çalıştırılıyor.
            }
            catch (Exception hata)
            {
                string HataMesaji = hata.Message;
                cikti = "";
            }
            finally
            {
                BaglantiAcKapa(BaglantiDurumu.Kapat);
            }
            return cikti;
        }



        /// <summary>
        /// Sorgudaki parametreleri tespit eder ve bir dizi olarak geri döndürür.
        /// </summary>
        /// <param name="sorgu">Parametre içeren bir sorgu giriniz.</param>
        /// <returns>Bu fonksiyon, string tipinde bir dizi döndürür.</returns>
        private string[] parametreleri_bul(string sorgu)
        {
            List<string> liste = new List<string>(); //Parametreleri barındıracak olan dizi.
            string gecici_parametre; //Parametre dizisine eklenecek olan parametreyi taşıyacak olan string değişken.
            string[] gecici_dizi = sorgu.Split('@'); //Sorgunun parametrelere göre parçalanmış hali.
            //Sorguyu "@" karakterlerine göre böldükten sonra parametreleri teker teker tespit edip diziye yerleştiriyoruz.
            for (int i = 1; i < gecici_dizi.Count(); i++)
            {
                int alt_limit = 666; //En düşük indeks değerini saptayacak olan değişken.
                gecici_parametre = gecici_dizi[i]; //Dizinin, döngünün faal aşamasında işlenmekte olan elemanı.
                List<int> dizi_kelime_sonu = new List<int>(); //Parametre adının kaçıncı karakterde bittiğine dair tahminleri barındıracak olan dizi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf(' ')); //İlk boşluk karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf(',')); //İlk virgül karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf('=')); //İlk eşittir karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf('!')); //İlk ünlem işareti karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf(')')); //İlk parantez karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf('+')); //İlk artı karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf('>')); //İlk büyüktür karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf('<')); //İlk küçüktür karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf('\'')); //İlk tırnak karakterinin indeksi.
                //Karşılaştırılan karakterlerden en önce karşılaşılanı tespit ediliyor.
                foreach (int indeks in dizi_kelime_sonu)
                {
                    if (indeks > 0 && indeks < alt_limit)
                    {
                        alt_limit = indeks;
                    }
                }
                //Eğer karakterlerden birisi ile karşılaşılmışsa, o karaktere kadar olan kısım parametre olarak alınıyor.
                if (alt_limit > 0 && alt_limit != 666)
                {
                    gecici_parametre = "@" + gecici_parametre.Substring(0, alt_limit);
                }
                liste.Add(gecici_parametre);
            }
            string[] dizi = liste.ToArray();
            return dizi;
        }

    }
}

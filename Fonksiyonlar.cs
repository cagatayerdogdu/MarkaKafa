using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace GrafikerPortal
{
    public class Fonksiyonlar
    {
        DAL Veritabani = new DAL();
        public string ProjeFiyatListesi(string KolonNo)
        {
            string ListeProjeTipleri = @"
    <div class=""category_column"" id=""chapter_" + KolonNo + @""">";

            DataTable TabloListeProjeTipleri; string ProjeTipID; string ProjeTipAd; string ProjeTipFiyat; string ProjeTipAciklama; string ProjeTipLink;
            string KategoriID; string KategoriAd; string KategoriUstMetin; string KategoriAltMetin;

            #region Kolondaki kategori başlıkları listeleniyor
            DataTable TabloListeKategoriler = Veritabani.Sorgu_DataTable("SELECT KategoriID, KategoriAd, UstMetin, AltMetin FROM gp_Kategoriler WHERE Kolon=@Kolon ORDER BY Sira", KolonNo);
            for (int k = 0; k < TabloListeKategoriler.Rows.Count; k++)
            {
                KategoriID = TabloListeKategoriler.Rows[k]["KategoriID"].ToString();
                KategoriAd = TabloListeKategoriler.Rows[k]["KategoriAd"].ToString();
                KategoriUstMetin = TabloListeKategoriler.Rows[k]["UstMetin"].ToString();
                KategoriAltMetin = TabloListeKategoriler.Rows[k]["AltMetin"].ToString();
                if (KategoriUstMetin.Length > 0) KategoriUstMetin = @"<p class=""info"">" + KategoriUstMetin + @"</p>";
                if (KategoriAltMetin.Length > 0) KategoriAltMetin = @"<p class=""info"">" + KategoriAltMetin + @"</p>";

                ListeProjeTipleri += @"
        <div class=""project_category"">
            <div class=""project_header"">
                <p>" + KategoriAd + @"</p>
            </div>
            <div class=""project_type_list"">" + 
                KategoriUstMetin + @"
                <ul class=""reset"">";

                #region Kategoriye bağlı proje tipleri listeleniyor
                TabloListeProjeTipleri = Veritabani.Sorgu_DataTable("SELECT TipID, TipAd, Fiyat, AciklamaKisa FROM gp_ProjeTipleri WHERE KategoriID=@KategoriID ORDER BY Sira", KategoriID);
                for (int p = 0; p < TabloListeProjeTipleri.Rows.Count; p++)
                {
                    ProjeTipID = TabloListeProjeTipleri.Rows[p]["TipID"].ToString();
                    ProjeTipAd = TabloListeProjeTipleri.Rows[p]["TipAd"].ToString();
                    ProjeTipFiyat = TabloListeProjeTipleri.Rows[p]["Fiyat"].ToString();
                    ProjeTipAciklama = TabloListeProjeTipleri.Rows[p]["AciklamaKisa"].ToString();
                    ProjeTipLink = "yeni_proje_1.aspx?t=" + ProjeTipID;

                    ListeProjeTipleri += @"
                    <li>
	                    <a class=""title csstooltip"" href=""" + ProjeTipLink + @""" rel=""nofollow"">
		                    " + ProjeTipAd + @": 
		                    <b>" + ProjeTipFiyat + @" TL</b>
		                    <span>" + ProjeTipAciklama + @"</span>
	                    </a>
	                    <a class=""title play_button"" href=""" + ProjeTipLink + @""" rel=""nofollow""> ►</a>
                    </li>";
                }
                #endregion

                ListeProjeTipleri += @"
                </ul>
                " + KategoriAltMetin + @"
	        </div>
        </div>";
            }
            #endregion

            ListeProjeTipleri += @"
    </div>";
            return ListeProjeTipleri;
        }

        public string AdimSekmeleri(int AktifAsama, string ProjeID = "")
        {
            string Liste = "";
            if (ProjeID != "" && Veritabani.Sorgu_Scalar("SELECT TOP(1) ProjeID FROM gp_Projeler WHERE ProjeID=@ProjeID", ProjeID) == "") return Liste;

            if (ProjeID == "") //Yeni proje kayıt
            {
                Liste = @"
                    <div class=""steps_container"">
                        <div class=""step_1"">
                            <div class=""active_step_1""></div>
                        </div>
                        <div class=""step_2"">
                            <div class=""passive_step_2""></div>
                        </div>
                        <div class=""step_3"">
                            <div class=""passive_step_3""></div>
                        </div>
                    </div>";
            }
            else //Kayıtlı proje düzenleme
            {
                int ProjeAsama = Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT TOP(1) Asama FROM gp_Projeler WHERE ProjeID=@ProjeID", ProjeID));
                Liste = @"
                    <div class=""steps_container"">";
                for (int i = 1; i <= 3; i++)
                {
                    Liste += @"
                        <div class=""step_" + i.ToString() + @""">";
                    if (AktifAsama == i)
                        Liste += @"<div class=""active_step_" + i.ToString() + @"""></div>";
                    else if (ProjeAsama <= i+1)
                        Liste += @"
                            <div class=""go_to_step_" + i.ToString() + @""">
                                <a href=""yeni_proje_" + i.ToString() + @".aspx?p=" + ProjeID + @"""></a>
                            </div>";
                    else
                        Liste += @"<div class=""passive_step_" + i.ToString() + @"""></div>";
                    Liste += "</div>";
                }
                Liste += "</div>";
            }
            return Liste;
        }

        public string FormatTemizle(string DosyaAdi)
        {
            string Sonuc = DosyaAdi;
            if (DosyaAdi.Contains('.'))
            {
                Sonuc = "";
                string[] DiziNoktalar = DosyaAdi.Split('.');
                for (int i = 0; i < DiziNoktalar.Length - 1; i++)
                {
                    Sonuc += DiziNoktalar[i];
                }
            }
            return Sonuc;
        }

        public string HtmlGuvenligi(string TehlikeliVeri)
        {
            string Sonuc = TehlikeliVeri;
            Sonuc = Sonuc.Replace("<", "&lt;");
            Sonuc = Sonuc.Replace(">", "&gt;");
            Sonuc = Sonuc.Replace("\"", "&quot;");
            return Sonuc;
        }

        //Kullanıcı adını QueryString olarak kullanılmaya hazır şekilde formatlar.
        public string KelimeSifrele(string Kelime)
        {
            string Sonuc = Kelime;
            Sonuc = Sonuc.Replace(' ', '-');
            Sonuc = Sonuc.Replace('Ğ', 'g');
            Sonuc = Sonuc.Replace('ğ', 'g');
            Sonuc = Sonuc.Replace('Ü', 'u');
            Sonuc = Sonuc.Replace('ü', 'u');
            Sonuc = Sonuc.Replace('Ş', 's');
            Sonuc = Sonuc.Replace('ş', 's');
            Sonuc = Sonuc.Replace('İ', 'i');
            Sonuc = Sonuc.Replace('ı', 'i');
            Sonuc = Sonuc.Replace('Ö', 'o');
            Sonuc = Sonuc.Replace('ö', 'o');
            Sonuc = Sonuc.Replace('Ç', 'c');
            Sonuc = Sonuc.Replace('ç', 'c');
            Sonuc = Sonuc.Replace(",", "");
            Sonuc = Sonuc.Replace(";", "");
            Sonuc = Sonuc.Replace(".", "");
            Sonuc = Sonuc.Replace("@", "");
            Sonuc = Sonuc.Replace("€", "");
            Sonuc = Sonuc.Replace("/", "");
            Sonuc = Sonuc.Replace("\\", "");
            Sonuc = Sonuc.Replace("|", "");
            Sonuc = Sonuc.Replace("!", "");
            Sonuc = Sonuc.Replace("'", "");
            Sonuc = Sonuc.Replace("+", "");
            Sonuc = Sonuc.Replace("^", "");
            Sonuc = Sonuc.Replace("%", "");
            Sonuc = Sonuc.Replace("&", "");
            Sonuc = Sonuc.Replace("(", "");
            Sonuc = Sonuc.Replace(")", "");
            Sonuc = Sonuc.Replace("=", "");
            Sonuc = Sonuc.Replace("*", "");
            Sonuc = Sonuc.Replace("_", "");
            Sonuc = Sonuc.Replace("$", "");
            Sonuc = Sonuc.Replace("\"", "");
            Sonuc = Sonuc.ToLower();
            return Sonuc;
        }

        public string AyAdiTespitEt(DateTime Tarih)
        {
            string Sonuc = "";
            switch (Tarih.ToString("MM"))
            {
                case "01":
                    Sonuc = "Ocak";
                    break;
                case "02":
                    Sonuc = "Şubat";
                    break;
                case "03":
                    Sonuc = "Mart";
                    break;
                case "04":
                    Sonuc = "Nisan";
                    break;
                case "05":
                    Sonuc = "Mayıs";
                    break;
                case "06":
                    Sonuc = "Haziran";
                    break;
                case "07":
                    Sonuc = "Temmuz";
                    break;
                case "08":
                    Sonuc = "Ağustos";
                    break;
                case "09":
                    Sonuc = "Eylül";
                    break;
                case "10":
                    Sonuc = "Ekim";
                    break;
                case "11":
                    Sonuc = "Kasım";
                    break;
                case "12":
                    Sonuc = "Aralık";
                    break;
            }
            return Sonuc;
        }

    }
}
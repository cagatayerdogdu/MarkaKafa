using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafikerPortal
{
    public partial class projelerim_sahip : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        string KullaniciID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            #region Güvenlik protokolü
            //Üye girişi yapılmamışsa üye giriş sayfasına yönlendiriliyor.
            KullaniciID = (Session["UyeID"] != null) ? Session["UyeID"].ToString() : "";
            if (KullaniciID == "") Response.Redirect("uye_ol.aspx");
            else if (Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyeID=@UyeID", KullaniciID) == "") Response.Redirect("uye_ol.aspx");
            #endregion

            ProjeListele();

        }

        protected void ProjeListele(int SayfaNo = 1, string FiltreKategori = "", string FiltreSektor = "")
        {
            string ListeProjeler = "";
            string ListeSayfaNumaralari = "";

            #region Filtre oluşturuluyor

            string SorguFiltre = "";
            string SorguSiralama = " ORDER BY p.ProjeListeBasi DESC, p.OlusturulmaTarihi DESC";
            List<string> DiziFiltreParametreleri = new List<string>();           
            
            #endregion

            DataTable TabloProjeler = Veritabani.Sorgu_DataTable("SELECT p.ProjeID, p.OlusturulmaTarihi, p.ProjeAdi, p.ProjeAdiSifreli, p.Zamanlama, p.ProjeGizlilik, p.ProjeKisisel, p.ProjeListeBasi, p.ProjeAramaMotoruYok, p.ProjeArkaplan, p.ProjeBold, p.Durum, t.TipAd, p.Odul AS Fiyat, p.Asama FROM gp_Projeler AS p JOIN gp_ProjeTipleri AS t ON p.TipID=t.TipID WHERE UyeID=@uyeid",KullaniciID);

           
            #region Proje listesi oluşturuluyor
            string ProjeID = ""; string ProjeAdi = ""; string ProjeAdiSifreli = ""; string ProjeLink = ""; int Zamanlama = 0; string ProjeGizlilik = ""; string ProjeKisisel = ""; string ProjeListeBasi = ""; string ProjeAramaMotoruYok = ""; string ProjeArkaplan = ""; string ProjeDurum = ""; string ProjeBold = ""; string ProjeTipAd = ""; string ProjeFiyat = ""; DateTime ProjeOlusturulmaTarihi; string KalanZamanBilgisi = ""; int TasarimSayisi = 0; string KazananResim = ""; string KazananAdi = ""; string KazananAvatar = ""; string KazananAdiSifreli = ""; string ProjeGirdileri = ""; string ProjeGirdiResim = ""; string BaslikBold = ""; string SatirHighlightedWrapper = ""; string SatirHighlighted = ""; string GizliProjeBilgisi = ""; string LinkNoFollow = ""; string ProjeAsama = ""; string ProjeOdemeButon;
            string OddEven = ""; DataTable TabloKazanan; DataTable TabloGirdiler;
            #region Liste başı

            ListeProjeler += @"
                        <br /><br /><br />
                        <div class=""kutular"">";
          
            #endregion
            for (int i = 0; i < TabloProjeler.Rows.Count; i++)
            {
                //if (i < SayfalamaAltLimit) continue;
                //else if (i > SayfalamaUstLimit) break;

                ProjeID = TabloProjeler.Rows[i]["ProjeID"].ToString();
                ProjeAdi = TabloProjeler.Rows[i]["ProjeAdi"].ToString();
                if (ProjeAdi.Length>=25)
                {
                    ProjeAdi=ProjeAdi.Substring(0, 25)+"...";
                }
                ProjeAdiSifreli = TabloProjeler.Rows[i]["ProjeAdiSifreli"].ToString();
                Zamanlama = Convert.ToInt32(TabloProjeler.Rows[i]["Zamanlama"].ToString());
                ProjeGizlilik = TabloProjeler.Rows[i]["ProjeGizlilik"].ToString();
                ProjeKisisel = TabloProjeler.Rows[i]["ProjeKisisel"].ToString();
                ProjeListeBasi = TabloProjeler.Rows[i]["ProjeListeBasi"].ToString();
                ProjeAramaMotoruYok = TabloProjeler.Rows[i]["ProjeAramaMotoruYok"].ToString();
                ProjeArkaplan = TabloProjeler.Rows[i]["ProjeArkaplan"].ToString();
                ProjeBold = TabloProjeler.Rows[i]["ProjeBold"].ToString();
                ProjeDurum = TabloProjeler.Rows[i]["Durum"].ToString();
                ProjeTipAd = TabloProjeler.Rows[i]["TipAd"].ToString();
                ProjeFiyat = TabloProjeler.Rows[i]["Fiyat"].ToString();
                ProjeOlusturulmaTarihi = Convert.ToDateTime(TabloProjeler.Rows[i]["OlusturulmaTarihi"].ToString());
                ProjeAsama = TabloProjeler.Rows[i]["Asama"].ToString();
                KalanZamanBilgisi = KalanZamanHesapla(ProjeOlusturulmaTarihi, Zamanlama);
                if (ProjeDurum == "3") KalanZamanBilgisi = "Tamamlandı";
                ProjeOdemeButon = "";
                if (ProjeDurum=="1" && ProjeAsama=="2")
                {
                    ProjeOdemeButon = @"<a class=""ozel_buton2"" href=""yeni_marka_3.aspx?p="+ProjeID+@"""> Ödeme Yap </a>";
                }
                ProjeLink = "proje.aspx?p=" + ProjeAdiSifreli;
                LinkNoFollow = (ProjeAramaMotoruYok == "1") ? @"rel=""nofollow""" : "";
                ProjeGirdileri = "";
                //Proje satırı formatlanıyor
                
                BaslikBold = (ProjeBold == "1") ? @"class=""bold""" : "";
                SatirHighlightedWrapper = (ProjeArkaplan == "1") ? @"class=""highlighted_wrapper""" : "";
                SatirHighlighted = (ProjeArkaplan == "1") ? @"class=""highlighted""" : "";
                GizliProjeBilgisi = (ProjeGizlilik == "1") ? "<strong>Gizli proje</strong> - " : "";

                //Katılan ve kazanan projeler tespit ediliyor
                KazananResim = ""; KazananAvatar = ""; KazananAdi = ""; KazananAdiSifreli = "";
                TabloKazanan = Veritabani.Sorgu_DataTable("SELECT u.KullaniciAdi, u.Avatar, u.KullaniciAdiSifreli, g.Resim FROM gp_ProjeGirdiler AS g JOIN gp_Uyeler AS u ON g.UyeID=u.UyeID WHERE g.ProjeID=@ProjeID AND g.Kazanan=1", ProjeID);
                if (TabloKazanan.Rows.Count > 0)
                {
                    KazananResim = TabloKazanan.Rows[0]["Resim"].ToString();
                    KazananAdi = TabloKazanan.Rows[0]["KullaniciAdi"].ToString();
                    KazananAvatar = TabloKazanan.Rows[0]["Avatar"].ToString();
                    if (KazananAvatar == "") KazananAvatar = "logo.png";
                    KazananAdiSifreli = TabloKazanan.Rows[0]["KullaniciAdiSifreli"].ToString();
                }

                //Resmi olmayan kayıtlar için varsayılan resim ayarlanıyor.
                if (KazananResim == "") KazananResim = "logo.png";
                if (KazananAvatar == "") KazananAvatar = "logo.png";
               
                ListeProjeler += @"
                    <div class=""kutu"">
                        <img style=""width:180px; height:140px; cursor:pointer;"" src=""images/contest_entries/300/" + KazananResim + @""" onclick=""document.location='" + ProjeLink + @"';"" />
                        <a href=""" + ProjeLink + @""" " + LinkNoFollow + @">" + ProjeAdi + @"</a>
                        <div class=""clear""></div>
                        <br />
                        <div class=""tasarimci_mini_img"">
                                <img src=""images/avatars/60/" + KazananAvatar + @""" style=""width:40px; height:40px;"" />
                        </div>
                        <div class=""tasarimci_sag"" style=""float:none;"">
                            <a href=""tasarimci.aspx?a=" + KazananAdiSifreli + @""">" + KazananAdi + @"  </a>
                            <a href="""" style=""float: right;margin-left: 10px;margin-top: -3px;"">" + ProjeFiyat + @" TL</a>
                        </div>
                        <div class=""tasarimci_sag"">
                            Kalan süre: " + KalanZamanBilgisi + @"
                        </div>
                        <div class=""proje_odeme"">
                            " + ProjeOdemeButon + @"
                        </div>
                    </div>";                        
            }
            #region Liste sonu
            ListeProjeler += @"</div>";
            #endregion
            #endregion
            //           
            lblListeProjeler.Text = ListeProjeler;
        }

        public string KalanZamanHesapla(DateTime OlusturulmaTarihi, int ProjeSuresi)
        {
            string Sonuc = "";
            TimeSpan TarihFarki = OlusturulmaTarihi.AddDays(ProjeSuresi) - DateTime.Now;
            double SaatFarki = Math.Round(TarihFarki.TotalHours);
            if (SaatFarki > 24)
            {
                Sonuc = Math.Floor(SaatFarki / 24).ToString() + " gün";
            }
            else
            {
                Sonuc = "yaklaşık " + Math.Round(SaatFarki).ToString() + " saat";
            }
            return Sonuc;
        }
    }
}
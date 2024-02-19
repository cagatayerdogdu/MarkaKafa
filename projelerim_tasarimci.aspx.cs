using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class projelerim_tasarimci : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        string AktifSekme = "";
        string KullaniciID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            #region Güvenlik protokolü
            //Üye girişi yapılmamışsa üye giriş sayfasına yönlendiriliyor.
            KullaniciID = (Session["UyeID"] != null) ? Session["UyeID"].ToString() : "";
            if (KullaniciID == "") Response.Redirect("uye_ol.aspx");
            else if (Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyeID=@UyeID and UyelikTipi=2 ", KullaniciID) == "") Response.Redirect("uye_ol.aspx");
            #endregion

            #region Sekme seçiliyor
            AktifSekme = "t";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                switch (Request.QueryString["s"].ToString())
                {
                    case "de":
                    case "t":
                    case "sd":
                    case "d":
                    case "k":
                        AktifSekme = Request.QueryString["s"].ToString();
                        break;
                }
            }
            string SeciliDevamEden = (AktifSekme == "de") ? "selected" : "";
            string SeciliTamamlanan = (AktifSekme == "t") ? "selected" : "";
            string SeciliDolanlar = (AktifSekme == "sd") ? "selected" : "";
            string SeciliDavet = (AktifSekme == "d") ? "selected" : "";
            string SeciliKazandiklarim = (AktifSekme == "k") ? "selected" : "";

            //if (AktifSekme == "p") lblSectionStart.Text = @"<section id=""projects_index"">";
            //else if (AktifSekme == "c") lblSectionStart.Text = @"<section id=""projects_completed"">";
            //else if (AktifSekme == "s") lblSectionStart.Text = @"<section id=""professional_completed"">";
            //lblSectionEnd.Text = "</section>";
            string SekmeListesi = @"
                <li class=""tab " + SeciliDevamEden + @""">
                    <a href=""projelerim_tasarimci.aspx?s=de"">DEVAM EDENLER</a>
                </li>
                <li class=""tab " + SeciliDolanlar + @""">
                    <a href=""projelerim_tasarimci.aspx?s=sd"">SÜRESİ DOLANLAR</a>
                </li>
                <li class=""tab " + SeciliTamamlanan + @""">
                    <a href=""projelerim_tasarimci.aspx?s=t"">TAMAMLANANLAR</a>
                </li>
            <li class=""tab " + SeciliDavet + @""">
                    <a href=""projelerim_tasarimci.aspx?s=d"">DAVET EDİLDİKLERİM</a>
                </li>
            <li class=""tab " + SeciliKazandiklarim + @""">
                    <a href=""projelerim_tasarimci.aspx?s=k"">KAZANDIKLARIM</a>
                </li>";
            lblSekmeler.Text = SekmeListesi;
            #endregion

            ProjeListele();
        }

        protected void ProjeListele(int SayfaNo = 1)
        {
            string ListeProjeler = "";
            string ListeSayfaNumaralari = "";

            #region Filtre oluşturuluyor
            string SorguFiltre = "";
            string SorguSiralama = " ORDER BY p.ProjeListeBasi DESC, p.OlusturulmaTarihi DESC";
            List<string> DiziFiltreParametreleri = new List<string>();
            DiziFiltreParametreleri.Add(KullaniciID);
     
            //Sekme filtresi
            if (AktifSekme == "de")
            {
                SorguFiltre += " AND p.Durum=2";
            }
            else if (AktifSekme=="t")
            {
                SorguFiltre += " AND p.Durum=3";
            }
            
            #endregion

            DataTable TabloProjeler = Veritabani.Sorgu_DataTable("SELECT p.ProjeID, p.OlusturulmaTarihi, p.ProjeAdi, p.ProjeAdiSifreli, P.Zamanlama, p.ProjeGizlilik, p.ProjeKisisel, p.ProjeListeBasi, p.ProjeAramaMotoruYok, p.ProjeArkaplan, p.ProjeBold, t.TipAd, p.Odul AS Fiyat FROM gp_Projeler AS p JOIN gp_ProjeTipleri AS t ON p.TipID=t.TipID WHERE p.Durum>1 AND Metin=0 AND (SELECT COUNT (*) FROM gp_ProjeGirdiler AS Projelerim WHERE Projelerim.ProjeID=p.ProjeID AND Projelerim.UyeID=@projeUyeID)>0" + SorguFiltre + SorguSiralama,DiziFiltreParametreleri.ToArray());
            #region Sayfalama oluşturuluyor
            double SayfaBasinaKayitSayisi = 10;
            double ToplamKayitSayisi = TabloProjeler.Rows.Count;
            double SayfalamaAltLimit = (SayfaNo - 1) * SayfaBasinaKayitSayisi;
            double SayfalamaUstLimit = SayfalamaAltLimit + SayfaBasinaKayitSayisi - 1;
            double SayfaSayisi = Math.Ceiling(ToplamKayitSayisi / SayfaBasinaKayitSayisi);
            double OncekiSayfa = SayfaNo - 1;
            double SonrakiSayfa = (SayfaNo < SayfaSayisi) ? SayfaNo + 1 : 0;

            SayfalamaAltLimit = 0;
            SayfalamaUstLimit = 999999999;

            #endregion
            #region Üye listesi oluşturuluyor
            string ProjeID = ""; string ProjeAdi = ""; string ProjeAdiSifreli = ""; string ProjeLink = ""; int Zamanlama = 0; string ProjeGizlilik = ""; string ProjeKisisel = ""; string ProjeListeBasi = ""; string ProjeAramaMotoruYok = ""; string ProjeArkaplan = ""; string ProjeBold = ""; string ProjeTipAd = ""; string ProjeFiyat = ""; DateTime ProjeOlusturulmaTarihi; string KalanZamanBilgisi = ""; int TasarimSayisi = 0; string KazananResim = ""; string KazananAdi = ""; string KazananAdiSifreli = ""; string ProjeGirdileri = ""; string ProjeGirdiResim = ""; string BaslikBold = ""; string SatirHighlightedWrapper = ""; string SatirHighlighted = ""; string GizliProjeBilgisi = ""; string LinkNoFollow = "";
            string OddEven = ""; DataTable TabloKazanan; DataTable TabloGirdiler;
            #region Liste başı

                    ListeProjeler += @"
                        <ul class=""reset index_project_list"">";
           
            #endregion
            for (int i = 0; i < TabloProjeler.Rows.Count; i++)
            {
                if (i < SayfalamaAltLimit) continue;
                else if (i > SayfalamaUstLimit) break;

                ProjeID = TabloProjeler.Rows[i]["ProjeID"].ToString();
                ProjeAdi = TabloProjeler.Rows[i]["ProjeAdi"].ToString();
                ProjeAdiSifreli = TabloProjeler.Rows[i]["ProjeAdiSifreli"].ToString();
                Zamanlama = Convert.ToInt32(TabloProjeler.Rows[i]["Zamanlama"].ToString());
                ProjeGizlilik = TabloProjeler.Rows[i]["ProjeGizlilik"].ToString();
                ProjeKisisel = TabloProjeler.Rows[i]["ProjeKisisel"].ToString();
                ProjeListeBasi = TabloProjeler.Rows[i]["ProjeListeBasi"].ToString();
                ProjeAramaMotoruYok = TabloProjeler.Rows[i]["ProjeAramaMotoruYok"].ToString();
                ProjeArkaplan = TabloProjeler.Rows[i]["ProjeArkaplan"].ToString();
                ProjeBold = TabloProjeler.Rows[i]["ProjeBold"].ToString();
                ProjeTipAd = TabloProjeler.Rows[i]["TipAd"].ToString();
                ProjeFiyat = TabloProjeler.Rows[i]["Fiyat"].ToString();
                ProjeOlusturulmaTarihi = Convert.ToDateTime(TabloProjeler.Rows[i]["OlusturulmaTarihi"].ToString());
                KalanZamanBilgisi = KalanZamanHesapla(ProjeOlusturulmaTarihi, Zamanlama);
                ProjeLink = "proje.aspx?p=" + ProjeAdiSifreli;
                LinkNoFollow = (ProjeAramaMotoruYok == "1") ? @"rel=""nofollow""" : "";
                ProjeGirdileri = "";
                //Proje satırı formatlanıyor
                
                    BaslikBold = (ProjeBold == "1") ? @"class=""bold""" : "";
                    SatirHighlightedWrapper = (ProjeArkaplan == "1") ? @"class=""highlighted_wrapper""" : "";
                    SatirHighlighted = (ProjeArkaplan == "1") ? @"class=""highlighted""" : "";
                    GizliProjeBilgisi = (ProjeGizlilik == "1") ? "<strong>Gizli proje</strong> - " : "";

                
                //Katılan ve kazanan projeler tespit ediliyor
//                if (AktifSekme == "c")
//                {
//                    TasarimSayisi = Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT COUNT(GirdiID) FROM gp_ProjeGirdiler WHERE ProjeID=@ProjeID", ProjeID));
//                    TabloKazanan = Veritabani.Sorgu_DataTable("SELECT u.KullaniciAdi, u.KullaniciAdiSifreli, g.Resim FROM gp_ProjeGirdiler AS g JOIN gp_Uyeler AS u ON g.UyeID=u.UyeID WHERE ProjeID=@ProjeID AND Kazanan=1", ProjeID);
//                    KazananResim = TabloKazanan.Rows[0]["Resim"].ToString();
//                    KazananAdi = TabloKazanan.Rows[0]["KullaniciAdi"].ToString();
//                    KazananAdiSifreli = TabloKazanan.Rows[0]["KullaniciAdiSifreli"].ToString();
//                    TabloGirdiler = Veritabani.Sorgu_DataTable("SELECT TOP(5) Resim FROM gp_ProjeGirdiler WHERE ProjeID=@ProjeID AND Kazanan=0 ORDER BY Favori DESC, Tarih DESC", ProjeID);
//                    for (int r = 0; r < TabloGirdiler.Rows.Count; r++)
//                    {
//                        ProjeGirdiResim = TabloGirdiler.Rows[r]["Resim"].ToString();
//                        ProjeGirdileri += @"
//				            <li>
//					            <img alt="""" width=""120"" height=""90"" src=""images/contest_entries/120/" + ProjeGirdiResim + @""" />
//				            </li>";
//                    }
                //}

                 OddEven = (i % 2 == 0) ? "odd" : "even";
                        #region Devam eden projeler
                        ListeProjeler += @"
                            <li class=""" + OddEven + @""">
	                            <div " + SatirHighlightedWrapper + @">
		                            <div " + SatirHighlighted + @">
			                            <div class=""info"">
				                            <ul class=""info-list"">
					                            <li class=""time"">" + KalanZamanBilgisi + @" kaldı</li>
					                            <li>" + TasarimSayisi.ToString() + @" tasarım</li>
					                            <li class=""price"">" + ProjeFiyat + @" TL</li>
				                            </ul>
			                            </div>
			                            <h2 " + BaslikBold + @">
				                            <a href=""" + ProjeLink + @""" " + LinkNoFollow + @">" + ProjeAdi + @"</a>
			                            </h2>
			                            <p>" + GizliProjeBilgisi + ProjeTipAd + @"</p>
		                            </div>
	                            </div>
                            </li>";
                        #endregion
            }
            #region Liste sonu
            ListeProjeler += @"</ul>";
            #endregion
            #endregion
            

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
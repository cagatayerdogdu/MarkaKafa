using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class tasarimcilar : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        int IntDenemeTahtasi;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();
            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

            UyeListele();

        }

        protected void btnFiltrele_Click(object sender, EventArgs e)
        {
            string FiltreAd = filter_title.Value.Trim();
            string FiltreSiralama = sort_by.Items[sort_by.SelectedIndex].Value;
            string FiltreKategori = filter_designer.Items[filter_designer.SelectedIndex].Value;
            string FiltreGecmis = approval_time.Items[approval_time.SelectedIndex].Value;
            hf_filter_title.Value = FiltreAd;
            hf_sort_by.Value = FiltreSiralama;
            hf_filter_designer.Value = FiltreKategori;
            hf_approval_time.Value = FiltreGecmis;
            UyeListele(1, FiltreAd, FiltreSiralama, FiltreKategori, FiltreGecmis);
        }

        protected void btnSayfaDegistir_Click(object sender, EventArgs e)
        {
            string FiltreAd = hf_filter_title.Value.Trim();
            string FiltreSiralama = hf_sort_by.Value;
            string FiltreKategori = hf_filter_designer.Value;
            string FiltreGecmis = hf_approval_time.Value;
            int FiltreSayfa = (int.TryParse(hf_page.Value, out IntDenemeTahtasi)) ? IntDenemeTahtasi : 1;
            UyeListele(FiltreSayfa, FiltreAd, FiltreSiralama, FiltreKategori, FiltreGecmis);
        }

        protected void UyeListele(int SayfaNo = 1, string FiltreAd = "", string FiltreSiralama = "", string FiltreKategori = "", string FiltreGecmis = "")
        {
            string ListeUyeler = "";
            string ListeSayfaNumaralari = "";
            #region Filtre oluşturuluyor
            string SorguFiltre = "";
            string SorguSiralama = " ORDER BY u.Tarih DESC";
            List<string> DiziFiltreParametreleri = new List<string>();
            if (FiltreAd.Length > 0)
            {
                FiltreAd = "%" + FiltreAd + "%";
                SorguFiltre += " AND u.KullaniciAdi LIKE @KullaniciAdi";
                DiziFiltreParametreleri.Add(FiltreAd);
            }
            if (FiltreSiralama.Length > 0)
            {
                if (FiltreSiralama=="most_attendance")
                {
//                    DataTable TabloKatilan= Veritabani.Sorgu_DataTable(@"SELECT pg.UyeID,count(pg.GirdiNo) EnCokKatilan,u.KullaniciAdi,u.KullaniciAdiSifreli,u.Avatar FROM gp_ProjeGirdiler pg join gp_Uyeler u on pg.UyeID=u.UyeID
//GROUP BY pg.UyeID,u.KullaniciAdi,u.KullaniciAdiSifreli,u.Avatar
//HAVING (COUNT(pg.UyeID) > 0) order by EnCokKatilan desc");

                    SorguSiralama = " order by (SELECT COUNT(GirdiID) FROM gp_ProjeGirdiler WHERE UyeID=u.UyeID) DESC";
                }
                else if (FiltreSiralama=="most_winning")
                {
//                   DataTable TabloKazanan= Veritabani.Sorgu_DataTable( @"SELECT pg.UyeID,count(*) EnCokKazanan,u.KullaniciAdi,u.KullaniciAdiSifreli,u.Avatar FROM gp_ProjeGirdiler pg join gp_Uyeler u on pg.UyeID=u.UyeID  
//where Kazanan=1  
//GROUP BY pg.UyeID,u.KullaniciAdi,u.KullaniciAdiSifreli,u.Avatar
                    //HAVING (COUNT(pg.UyeID) > 0) order by EnCokKazanan desc");
                    SorguSiralama = " order by (SELECT COUNT(GirdiID) FROM gp_ProjeGirdiler WHERE Kazanan=1 AND UyeID=u.UyeID) DESC";
                }
            }
            if (FiltreKategori.Length > 0)
            {
                string Uzmanlik = "";
                switch (FiltreKategori)
                {
                    case "web_designer":
                        Uzmanlik = "u.UzmanlikDijitalTasarim";
                        break;
                    case "illustrator":
                        Uzmanlik = "u.UzmanlikIllustrasyon";
                        break;
                    case "graphics_designer":
                        Uzmanlik = "u.UzmanlikGrafikTasarim";
                        break;
                    case "writer":
                        Uzmanlik = "u.UzmanlikReklamYazarligi";
                        break;
                    case "industrial_designer":
                        Uzmanlik = "u.UzmanlikEndustriyelTasarim";
                        break;
                }
                if (Uzmanlik != "")
                {
                    SorguFiltre += " AND " + Uzmanlik + "=1";
                }
            }
            if (FiltreGecmis.Length > 0)
            {
                if (FiltreGecmis == "on_this_week")
                {
                    SorguFiltre += " AND u.Tarih>=getdate()-7";//+ DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss"); CE_getdate()-7_[12.06.2015]
                }
                else if (FiltreGecmis == "on_this_month")
                {
                    SorguFiltre += " AND u.Tarih>=getdate()-30"; //+ DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            #endregion

            DataTable TabloUyeler = Veritabani.Sorgu_DataTable("SELECT u.UyeID, u.KullaniciAdi, u.KullaniciAdiSifreli, u.Avatar FROM gp_Uyeler AS u WHERE u.UyelikTipi=2 AND u.Aktif=1 " + SorguFiltre + SorguSiralama, DiziFiltreParametreleri.ToArray());
            #region Sayfalama oluşturuluyor
            double SayfaBasinaKayitSayisi = 20;
            double ToplamKayitSayisi = TabloUyeler.Rows.Count;
            double SayfalamaAltLimit = (SayfaNo - 1) * SayfaBasinaKayitSayisi;
            double SayfalamaUstLimit = SayfalamaAltLimit + SayfaBasinaKayitSayisi -1;
            double SayfaSayisi = Math.Ceiling(ToplamKayitSayisi / SayfaBasinaKayitSayisi);
            double OncekiSayfa = SayfaNo - 1;
            double SonrakiSayfa = (SayfaNo < SayfaSayisi) ? SayfaNo + 1 : 0;
            lblTasarimciSayisi.Text = ToplamKayitSayisi.ToString();
            #endregion
            #region Üye listesi oluşturuluyor
            string KullaniciID = ""; string KullaniciAd = ""; string KullaniciAdSifreli = ""; string KullaniciAvatar = ""; string KullaniciLink = "";
            for (int i = 0; i < TabloUyeler.Rows.Count; i++)
            {
                if (i < SayfalamaAltLimit) continue;
                else if (i > SayfalamaUstLimit) break;

                KullaniciID = TabloUyeler.Rows[i]["UyeID"].ToString();
                KullaniciAd = TabloUyeler.Rows[i]["KullaniciAdi"].ToString();
                KullaniciAdSifreli = TabloUyeler.Rows[i]["KullaniciAdiSifreli"].ToString();
                KullaniciAvatar = TabloUyeler.Rows[i]["Avatar"].ToString();
                if (KullaniciAvatar == "") KullaniciAvatar = "MarkaKafaAvatar.jpg";
                KullaniciLink = "tasarimci.aspx?a=" + KullaniciAdSifreli;

                ListeUyeler += @"
                        <div class=""divTasarimci"">
                            <a title=""" + KullaniciAd + @""" href=""" + KullaniciLink + @"""> <img alt=""" + KullaniciAd + @""" src=""images/avatars/120/" + KullaniciAvatar + @"""style=""border-radius: 100%; height: 127px; margin-left:-7px; margin-top: 9px; width: 128px;"" title=""" + KullaniciAd + @""" /></a>
                        </div>";
                /*
                //ESKİ 20.11.2014
                ListeUyeler += @"
        <li class=""creative_list"">
            <div class=""info"">
                <a class=""no_hover"" href=""" + KullaniciLink + @""">
                    <img alt=""" + KullaniciAd + @""" height=""120px"" src=""images/avatars/120/" + KullaniciAvatar + @""" width=""120px"" />
                </a>
            </div>
            <div class=""post_shadow""></div>
            <strong><a href=""" + KullaniciLink + @""">" + KullaniciAd + @"</a></strong>
        </li>";
                */
            }
            #endregion
            #region Sayfa numaraları oluşturuluyor
            string OncekiSayfaDisabled = (OncekiSayfa == 0) ? " disabled" : "";
            string SonrakiSayfaDisabled = (SonrakiSayfa == 0) ? " disabled" : "";
            ListeSayfaNumaralari += @"
        <a class=""previous_page " + OncekiSayfaDisabled + @""" onclick=""sayfaDegistir('" + OncekiSayfa.ToString() + @"');"" href=""javascript:void(0);"" rel=""prev"">&laquo; Önceki</a> ";
            string SayfaRel = "";
            for (int s = 1; s <= SayfaSayisi; s++)
            {
                SayfaRel = "";
                if (s == SayfaNo)
                    ListeSayfaNumaralari += @"
        <em class=""current"">" + s.ToString() + @"</em> ";
                else if (s == 3 && SayfaNo > 9) //Sol boşluk
                    ListeSayfaNumaralari += @"
        <span class=""gap"">&hellip;</span> ";
                else if (s == SayfaSayisi - 3 && SayfaNo < SayfaSayisi - 9) //Sağ boşluk
                    ListeSayfaNumaralari += @"
        <span class=""gap"">&hellip;</span> ";
                else if (s <= 2 || s >= SayfaSayisi - 1 || (s > SayfaNo - 6 && s < SayfaNo + 5))
                {
                    if (s == SayfaNo - 1) SayfaRel = @"rel=""prev""";
                    else if (s == SayfaNo + 1) SayfaRel = @"rel=""next""";
                    ListeSayfaNumaralari += @"
        <a " + SayfaRel + @" onclick=""sayfaDegistir('" + s.ToString() + @"');"" href=""javascript:void(0);"">" + s.ToString() + @"</a> ";
                }
            }
            ListeSayfaNumaralari += @"
        <a class=""next_page " + SonrakiSayfaDisabled + @""" rel=""next"" onclick=""sayfaDegistir('" + SonrakiSayfa.ToString() + @"');"" href=""javascript:void(0);"">Sonraki &raquo;</a>";
            #endregion

            lblListeKullanicilar.Text = ListeUyeler;
            lblSayfaNumaralari.Text = ListeSayfaNumaralari;
        }

    }
}
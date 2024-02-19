using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class katildigi_markalar_tasarimci : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            #region Güvenlik protokolü
            //Tasarımcı seçilmemişse kullanıcı tasarımcılar sayfasına yönlendiriliyor.
            string TasarimciAdi = (!string.IsNullOrEmpty(Request.QueryString["a"])) ? Request.QueryString["a"].ToString() : "";
            string Kazandigi = (!string.IsNullOrEmpty(Request.QueryString["k"])) ? Request.QueryString["k"].ToString() : "";
            string filtre_sorgu = "";
            if (Kazandigi!="")
            {
                filtre_sorgu=" AND g.Kazanan=1";
            }
            if (TasarimciAdi == "") Response.Redirect("tasarimcilar.aspx");
            else if (Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyelikTipi=2 AND Aktif=1 AND KullaniciAdiSifreli=@KullaniciAdiSifreli", TasarimciAdi) == "") Response.Redirect("tasarimcilar.aspx");
            else
            {
                Session["TasarimciAdiSifreli"] = TasarimciAdi;
                Session["TasarimciID"] = Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyelikTipi=2 AND Aktif=1 AND KullaniciAdiSifreli=@KullaniciAdiSifreli", TasarimciAdi);
            }

            a_Kazandigi.HRef = "katildigi_markalar_tasarimci.aspx?a="+ TasarimciAdi +"&k=1";
            a_Katildigi.HRef = "katildigi_markalar_tasarimci.aspx?a=" + TasarimciAdi;
            #endregion
            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.            
                     

            #region Tasarımcı bilgileri tespit ediliyor
            DataTable TabloTasarimci = Veritabani.Sorgu_DataTable("SELECT TOP(1) Avatar, KullaniciAdi, WebSitesi, Blog, Hakkinda, UzmanlikGrafikTasarim, UzmanlikDijitalTasarim, UzmanlikEndustriyelTasarim, UzmanlikReklamYazarligi, UzmanlikIllustrasyon,Tarih FROM gp_Uyeler WHERE UyeID=@UyeID", Session["TasarimciID"].ToString());
            string UyeKullaniciAdi = TabloTasarimci.Rows[0]["KullaniciAdi"].ToString();
            string UyeAvatar = TabloTasarimci.Rows[0]["Avatar"].ToString();
            if (UyeAvatar.Length == 0) UyeAvatar = "MarkaKafaAvatar.jpg";
            string UyeWebSitesi = TabloTasarimci.Rows[0]["WebSitesi"].ToString();
            string UyeBlog = TabloTasarimci.Rows[0]["Blog"].ToString();
            string UyeHakkinda = TabloTasarimci.Rows[0]["Hakkinda"].ToString();
            string UyeUzmanlikGrafikTasarim = TabloTasarimci.Rows[0]["UzmanlikGrafikTasarim"].ToString();
            string UyeUzmanlikDijitalTasarim = TabloTasarimci.Rows[0]["UzmanlikDijitalTasarim"].ToString();
            string UyeUzmanlikEndustriyelTasarim = TabloTasarimci.Rows[0]["UzmanlikEndustriyelTasarim"].ToString();
            string UyeUzmanlikReklamYazarligi = TabloTasarimci.Rows[0]["UzmanlikReklamYazarligi"].ToString();
            string UyeUzmanlikIllustrasyon = TabloTasarimci.Rows[0]["UzmanlikIllustrasyon"].ToString();
            DateTime TasarimciKayitTarih = Convert.ToDateTime(TabloTasarimci.Rows[0]["Tarih"].ToString());
            //Uzmanlık alanları formatlanıyor
            List<string> DiziUzmanlikAlanlari = new List<string>();
            if (UyeUzmanlikGrafikTasarim == "1") DiziUzmanlikAlanlari.Add("Grafik Tasarım");
            if (UyeUzmanlikDijitalTasarim == "1") DiziUzmanlikAlanlari.Add("Dijital Tasarım");
            if (UyeUzmanlikEndustriyelTasarim == "1") DiziUzmanlikAlanlari.Add("Endütriyel Tasarım");
            if (UyeUzmanlikReklamYazarligi == "1") DiziUzmanlikAlanlari.Add("Reklam Yazarlığı");
            if (UyeUzmanlikIllustrasyon == "1") DiziUzmanlikAlanlari.Add("İllüstrasyon");
            //Bilgiler ekrana yazdırılıyor
            imgAvatar.Src = "images/avatars/60/" + UyeAvatar;
            lblTasarimciAdi.Text = UyeKullaniciAdi;
            lblTarih.Text = TasarimciKayitTarih.ToString("dd ") + AletKutusu.AyAdiTespitEt(TasarimciKayitTarih) + TasarimciKayitTarih.ToString(" yyyy");
            //lblWebsite.Text = (UyeWebSitesi.Length > 0) ? @"<p><a target=""_blank"" href=""" + UyeWebSitesi + @""">" + UyeWebSitesi + @"</a></p>" : "";
            //lblBlog.Text = (UyeBlog.Length > 0) ? @"<p><a target=""_blank"" href=""" + UyeBlog + @""">" + UyeBlog + @"</a></p>" : "";
            //lblHakkinda.Text = UyeHakkinda;
            //lblUzmanlikAlanlari.Text = string.Join(", ", DiziUzmanlikAlanlari);
            #endregion

            
            #region Proje girdileri listesi oluşturuluyor
            string ListeProjeGirdileri = "";
            DataTable TabloProjeGirdileri = Veritabani.Sorgu_DataTable("SELECT g.GirdiID, g.GirdiTip, g.Baslik, g.Resim, g.Video, p.ProjeAdiSifreli,g.Kazanan FROM gp_ProjeGirdiler AS g JOIN gp_Projeler AS p ON g.ProjeID=p.ProjeID WHERE g.Aktif=1 " + filtre_sorgu + " AND g.UyeID=@UyeID ORDER BY g.GirdiID DESC", Session["TasarimciID"].ToString());
            string ProjeGirdiID; string ProjeGirdiBaslik; string ProjeGirdiResim; ; string ProjeGirdiVideo; string ProjeGirdiTip; string GirdiProjeAdiSifreli; string ProjeGirdiLink;
            for (int i = 0; i < TabloProjeGirdileri.Rows.Count; i++)
            {
                ProjeGirdiID = TabloProjeGirdileri.Rows[i]["GirdiID"].ToString();
                ProjeGirdiTip = TabloProjeGirdileri.Rows[i]["GirdiTip"].ToString();
                ProjeGirdiBaslik = TabloProjeGirdileri.Rows[i]["Baslik"].ToString();
                ProjeGirdiResim = TabloProjeGirdileri.Rows[i]["Resim"].ToString();
                ProjeGirdiVideo = TabloProjeGirdileri.Rows[i]["Video"].ToString();
                bool GirdiKazanan = (TabloProjeGirdileri.Rows[i]["Kazanan"].ToString() == "1");
                GirdiProjeAdiSifreli = TabloProjeGirdileri.Rows[i]["ProjeAdiSifreli"].ToString();
                ProjeGirdiLink = "proje_tasarimlar.aspx?p=" + GirdiProjeAdiSifreli + "&g=" + ProjeGirdiID;
                if (ProjeGirdiBaslik.Length >= 18)
                {
                    ProjeGirdiBaslik = ProjeGirdiBaslik.Substring(0, 18) + "...";
                }
                else if (ProjeGirdiBaslik.Length >= 25)
                {
                    ProjeGirdiBaslik = ProjeGirdiBaslik.Substring(0, 25) + "...";
                }

                if (ProjeGirdiTip == "1") //Resim
                {

                    ListeProjeGirdileri += @"
                        <div style=""float:left; text-align:center;"">
                                <div class=""kutu2 proje_arkaplan"" style=""" + (GirdiKazanan ? "background-color:#AB2B77;" : "") + @""">
                                    <div class=""kutu2_ust"">
                                        <div class=""kutu2_ic"">                           
                                            <img src=""images/contest_entries/300/" + ProjeGirdiResim + @"""/>
                                        </div>
                                    </div>
                                    <div class=""kutu2_alt"">                                        
                                        <a href=""" + ProjeGirdiLink + @""">" + ProjeGirdiBaslik + @"</a>
                                    </div>                                

                                </div>
                       </div>";


                }
                else if (ProjeGirdiTip == "2") //Video
                {

                }
            }
            lblTasarimlar.Text = ListeProjeGirdileri;
            #endregion
            

        }
    }
}
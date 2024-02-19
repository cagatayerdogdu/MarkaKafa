using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class tasarimci : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            #region Güvenlik protokolü
            //Tasarımcı seçilmemişse kullanıcı tasarımcılar sayfasına yönlendiriliyor.
            string TasarimciAdi = (!string.IsNullOrEmpty(Request.QueryString["a"])) ? Request.QueryString["a"].ToString() : "";
            if (TasarimciAdi == "") Response.Redirect("tasarimcilar.aspx");
            else if (Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyelikTipi=2 AND Aktif=1 AND KullaniciAdiSifreli=@KullaniciAdiSifreli", TasarimciAdi) == "") Response.Redirect("tasarimcilar.aspx");
            else
            {
                Session["TasarimciAdiSifreli"] = TasarimciAdi;
                Session["TasarimciID"] = Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyelikTipi=2 AND Aktif=1 AND KullaniciAdiSifreli=@KullaniciAdiSifreli", TasarimciAdi);
            }
            #endregion
            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

            #region Üst menü oluşturuluyor
            string UstMenu = "";
            string AktifMenu = (!string.IsNullOrEmpty(Request.QueryString["t"])) ? Request.QueryString["t"].ToString() : "p";
            //Portfolyo
            UstMenu += (AktifMenu == "p") ? @"<a class=""btn_oval fortfolyo_bosluk"">PORTFOLYO</a>" : @"<a class=""btn_oval fortfolyo_bosluk"" href=""tasarimci.aspx?t=p&a=" + Session["TasarimciAdiSifreli"].ToString() + @""">PORTFOLYO</a>";
            //Katıldığı Projeler
            UstMenu += (AktifMenu == "k") ? @"<a class=""btn_oval katildigi_projeler_bosluk"">KATILDIĞI PROJELER</a>" : @"<a class=""btn_oval katildigi_projeler_bosluk"" href=""tasarimci.aspx?t=k&a=" + Session["TasarimciAdiSifreli"].ToString() + @""">KATILDIĞI PROJELER</a>";
            lblKullaniciMenu.Text = UstMenu;
            #endregion

            #region Tasarımcı bilgileri tespit ediliyor
            DataTable TabloTasarimci = Veritabani.Sorgu_DataTable("SELECT TOP(1) Avatar, KullaniciAdi, WebSitesi, Blog, Hakkinda, UzmanlikGrafikTasarim, UzmanlikDijitalTasarim, UzmanlikEndustriyelTasarim, UzmanlikReklamYazarligi, UzmanlikIllustrasyon FROM gp_Uyeler WHERE UyeID=@UyeID", Session["TasarimciID"].ToString());
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
            //Uzmanlık alanları formatlanıyor
            List<string> DiziUzmanlikAlanlari = new List<string>();
            if (UyeUzmanlikGrafikTasarim == "1") DiziUzmanlikAlanlari.Add("Grafik Tasarım");
            if (UyeUzmanlikDijitalTasarim == "1") DiziUzmanlikAlanlari.Add("Dijital Tasarım");
            if (UyeUzmanlikEndustriyelTasarim == "1") DiziUzmanlikAlanlari.Add("Endütriyel Tasarım");
            if (UyeUzmanlikReklamYazarligi == "1") DiziUzmanlikAlanlari.Add("Reklam Yazarlığı");
            if (UyeUzmanlikIllustrasyon == "1") DiziUzmanlikAlanlari.Add("İllüstrasyon");
            //Bilgiler ekrana yazdırılıyor
            imgAvatar.Src = "images/avatars/60/" + UyeAvatar;
            lblKullaniciAdi.Text = UyeKullaniciAdi;
            lblWebsite.Text = (UyeWebSitesi.Length > 0) ? @"<p><a target=""_blank"" href=""" + UyeWebSitesi + @""">" + UyeWebSitesi + @"</a></p>" : "";
            lblBlog.Text = (UyeBlog.Length > 0) ? @"<p><a target=""_blank"" href=""" + UyeBlog + @""">" + UyeBlog + @"</a></p>" : "";
            lblHakkinda.Text = UyeHakkinda;
            lblUzmanlikAlanlari.Text = string.Join(", ", DiziUzmanlikAlanlari);
            #endregion

            if (AktifMenu == "p")
            {
                #region Portfolyo listesi oluşturuluyor
                string ListePortfolyo = "";
                DataTable TabloPortfolyo = Veritabani.Sorgu_DataTable("SELECT Baslik, Resim, Video, PortfolyoTip FROM gp_Portfolyo WHERE Aktif=1 AND UyeID=@UyeID ORDER BY PortfolyoID DESC", Session["TasarimciID"].ToString());
                string PortfolyoBaslik; string PortfolyoResim; string PortfolyoVideo; string PortfolyoTip;
                for (int i = 0; i < TabloPortfolyo.Rows.Count; i++)
                {
                    PortfolyoBaslik = TabloPortfolyo.Rows[i]["Baslik"].ToString();
                    PortfolyoResim = TabloPortfolyo.Rows[i]["Resim"].ToString();
                    PortfolyoVideo = TabloPortfolyo.Rows[i]["Video"].ToString();
                    PortfolyoTip = TabloPortfolyo.Rows[i]["PortfolyoTip"].ToString();
                    if (PortfolyoTip == "1") //Resim
                    {
                        ListePortfolyo += @"
                        <li>
                            <img alt=""Portfolyo"" class=""frame"" style=""height:300px;"" src=""images/portfolio_items/300/" + PortfolyoResim + @""" />
                            <p>" + PortfolyoBaslik + @"</p>
                        </li>";
                    }
                    else if (PortfolyoTip == "2") //Video
                    {

                    }
                }
                lblListe.Text = ListePortfolyo;
                #endregion
            }
            else if (AktifMenu == "k")
            {
                #region Proje girdileri listesi oluşturuluyor
                string ListeProjeGirdileri = "";
                DataTable TabloProjeGirdileri = Veritabani.Sorgu_DataTable("SELECT g.GirdiID, g.GirdiTip, g.Baslik, g.Resim, g.Video, p.ProjeAdiSifreli FROM gp_ProjeGirdiler AS g JOIN gp_Projeler AS p ON g.ProjeID=p.ProjeID WHERE g.Aktif=1 AND g.UyeID=@UyeID ORDER BY g.GirdiID DESC", Session["TasarimciID"].ToString());
                string ProjeGirdiID; string ProjeGirdiBaslik; string ProjeGirdiResim; ; string ProjeGirdiVideo; string ProjeGirdiTip; string GirdiProjeAdiSifreli; string ProjeGirdiLink;
                for (int i = 0; i < TabloProjeGirdileri.Rows.Count; i++)
                {
                    ProjeGirdiID = TabloProjeGirdileri.Rows[i]["GirdiID"].ToString();
                    ProjeGirdiTip = TabloProjeGirdileri.Rows[i]["GirdiTip"].ToString();
                    ProjeGirdiBaslik = TabloProjeGirdileri.Rows[i]["Baslik"].ToString();
                    ProjeGirdiResim = TabloProjeGirdileri.Rows[i]["Resim"].ToString();
                    ProjeGirdiVideo = TabloProjeGirdileri.Rows[i]["Video"].ToString();
                    GirdiProjeAdiSifreli = TabloProjeGirdileri.Rows[i]["ProjeAdiSifreli"].ToString();
                    ProjeGirdiLink = "proje_tasarimlar.aspx?p=" + GirdiProjeAdiSifreli + "&g=" + ProjeGirdiID;
                    if (ProjeGirdiTip == "1") //Resim
                    {


                        ListeProjeGirdileri += @"
                        <div style=""float:left; text-align:center;"">
                                <div class=""kutu2 proje_arkaplan"">
                                    <div class=""kutu2_ust"">
                                        <div class=""kutu2_ic"">                           
                                            <img src=""images/contest_entries/300/" + ProjeGirdiResim + @"""/>
                                        </div>
                                    </div>
                                    <div class=""kutu2_alt"">                                        
                                        <a target=""_blank"" href=""" + ProjeGirdiLink + @""">" + ProjeGirdiBaslik + @"</a>
                                    </div>                                

                                </div>
                       </div>";
                        /*
                        ListeProjeGirdileri += @" 
                        <div style=""float:left; text-align:center;"">
                                <div class=""kutu2"">
                                    <div class=""kutu2_ust"">                                        
                                         <img alt=""Portfolyo"" class=""frame"" style="""" src=""images/contest_entries/300/" + ProjeGirdiResim + @""" />
                                    </div>
                                    <div class=""kutu2_alt"">                                        
                                        <a target=""_blank"" href=""images/contest_entries/300/" + ProjeGirdiResim + @""">" + ProjeGirdiBaslik + @"</a>
                                    </div>                                

                                </div>
                       </div>";
                        */
//                        ListeProjeGirdileri += @"
//                        <li>
//                            <img alt=""Portfolyo"" class=""frame"" style=""height:300px;"" src=""images/contest_entries/300/" + ProjeGirdiResim + @""" />
//                            <p>" + ProjeGirdiBaslik + @"</p>
//                        </li>";
                    }
                    else if (ProjeGirdiTip == "2") //Video
                    {

                    }
                }
                lblListe.Text = ListeProjeGirdileri;
                #endregion
            }

        }

    }
}
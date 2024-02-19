using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class faturabilgileri : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            #region Güvenlik protokolü
            //Üye girişi yapılmamışsa üye giriş sayfasına yönlendiriliyor.
            string KullaniciID = (Session["UyeID"] != null) ? Session["UyeID"].ToString() : "";
            if (KullaniciID == "") Response.Redirect("uye_ol.aspx");
            else if (Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyeID=@UyeID", KullaniciID) == "") Response.Redirect("uye_ol.aspx");
            #endregion

            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

            #region Üye bilgileri tespit ediliyor
            DataTable TabloUyeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) UyelikTipi, IptalTalebi, Avatar, KullaniciAdi, KullaniciAdiSifreli,Tarih, Eposta, Meslek, NeredenDuydunuz, Telefon, WebSitesi, Blog, Hakkinda, UzmanlikGrafikTasarim, UzmanlikDijitalTasarim, UzmanlikEndustriyelTasarim, UzmanlikReklamYazarligi, UzmanlikIllustrasyon, HatirlatmaPeriyodu, HatirlatProjeler, HatirlatGelismeler FROM gp_Uyeler WHERE UyeID=@UyeID", Session["UyeID"].ToString());
            string UyeTip = TabloUyeBilgileri.Rows[0]["UyelikTipi"].ToString();
            bool UyeIptalTalebi = (TabloUyeBilgileri.Rows[0]["IptalTalebi"].ToString() == "1");
            string UyeAvatar = TabloUyeBilgileri.Rows[0]["Avatar"].ToString();
            string UyeNeredenDuydunuz = TabloUyeBilgileri.Rows[0]["NeredenDuydunuz"].ToString();
            string UyeHatirlatmaPeriyodu = TabloUyeBilgileri.Rows[0]["HatirlatmaPeriyodu"].ToString();
            DateTime TasarimciKayitTarih = Convert.ToDateTime(TabloUyeBilgileri.Rows[0]["Tarih"].ToString());
            string KullaniciAdi = TabloUyeBilgileri.Rows[0]["KullaniciAdiSifreli"].ToString();
            string K_Adi = TabloUyeBilgileri.Rows[0]["KullaniciAdi"].ToString();
            #endregion

            #region Fatura bilgileri listeleniyor
            string ListeFaturalar = "";
            DataTable TabloFaturalar = Veritabani.Sorgu_DataTable("SELECT * FROM gp_FaturaBilgileri WHERE UyeID=@UyeID", KullaniciID);
            string FaturaID = ""; string FaturaBaslik = "";
            for (int i = 0; i < TabloFaturalar.Rows.Count; i++)
            {
                FaturaID = TabloFaturalar.Rows[i]["FaturaID"].ToString();
                FaturaBaslik = TabloFaturalar.Rows[i]["FaturaBaslik"].ToString();
                // href=""faturaduzenle.aspx?id=" + FaturaID + @"""
                ListeFaturalar += @"
                    <div class=""divSatir"">
                        " + FaturaBaslik + @" 
                        
                        <a class=""ozel_buton2"" style=""cursor:pointer; margin-left:80px"" data-toggle=""modal"" data-target=""#myModal"" onclick=""faturaDuzenle('" + FaturaID + @"');""> Düzenle </a>
                    </div>";
            }
            lblFaturalar.Text = ListeFaturalar;
            #endregion
        }
    }
}
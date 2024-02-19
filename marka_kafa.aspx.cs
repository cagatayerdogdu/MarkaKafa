using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class marka_kafa : System.Web.UI.Page
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


               if (UyeAvatar != "") imgAvatar.Src = "images/avatars/120/" + UyeAvatar;
            #endregion

            if (UyeTip == "1")
            {
                a_Kazandigi.Visible = false;
                a_Katildigi.Visible = false;
            }
            else if (UyeTip == "2")
            {
                a_Kazandigi.HRef = "katildigi_markalar_tasarimci.aspx?a=" + KullaniciAdi + "&k=1";
                a_Katildigi.HRef = "katildigi_markalar_tasarimci.aspx?a=" + KullaniciAdi;
            }

            lblTarih.Text = TasarimciKayitTarih.ToString("dd ") + AletKutusu.AyAdiTespitEt(TasarimciKayitTarih) + TasarimciKayitTarih.ToString(" yyyy");
            
            lblTasarimciAdi.Text = K_Adi;
            OncekiMarkalarim.HRef = "projelerim_sahip.aspx?a=" + KullaniciAdi;
            
        }
    }
}
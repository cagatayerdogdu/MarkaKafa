using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class proje_detay : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        int IntDenemeTahtasi;
        string KullaniciUyelikTipi = "";
        string KullaniciOnay = "";
        bool ProjeGizli; bool ProjeKisisel;
        string ProjeID; string KullaniciID; string ProjeAdiSifreli = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();
            #region Proje geçerliliği kontrol ediliyor
            ProjeAdiSifreli = (!string.IsNullOrEmpty(Request.QueryString["p"])) ? Request.QueryString["p"].ToString() : "";
            ProjeID = Veritabani.Sorgu_Scalar("SELECT TOP(1) ProjeID FROM gp_Projeler WHERE ProjeAdiSifreli=@ProjeAdiSifreli", ProjeAdiSifreli);
            if (ProjeID == "") Response.Redirect("default.aspx?h=p26");
            #endregion

            #region Üye bilgileri tespit ediliyor
            KullaniciID = (Session["UyeID"] != null) ? Session["UyeID"].ToString() : "";
            if (KullaniciID != "")
            {
                DataTable TabloUyeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) UyelikTipi, Onay FROM gp_Uyeler WHERE UyeID=@UyeID", KullaniciID);
                KullaniciUyelikTipi = TabloUyeBilgileri.Rows[0]["UyelikTipi"].ToString();
                KullaniciOnay = TabloUyeBilgileri.Rows[0]["Onay"].ToString();
            }
            #endregion

            #region Proje bilgileri tespit ediliyor
            DataTable TabloProjeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) t.TipAd, u.KullaniciAdi, p.OlusturulmaTarihi, p.Zamanlama, p.ProjeAdi, p.Odul, p.ProjeGizlilik, p.ProjeKisisel, p.DetayMarka, p.DetayMusteri, p.DetayRakip, p.DetayOzellik, p.DetayNot, (SELECT COUNT(g.GirdiID) FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=p.ProjeID) AS GirdiSayisi, (SELECT COUNT(g.GirdiID) FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=p.ProjeID AND g.Kazanan=1) AS KazananSayisi FROM gp_Projeler AS p JOIN gp_ProjeTipleri AS t ON p.TipID=t.TipID JOIN gp_Uyeler AS u ON p.UyeID=u.UyeID WHERE p.ProjeID=@ProjeID", ProjeID);
            string ProjeAdi = TabloProjeBilgileri.Rows[0]["ProjeAdi"].ToString();
            string TipAdi = TabloProjeBilgileri.Rows[0]["TipAd"].ToString();
            string ProjeSahibi = TabloProjeBilgileri.Rows[0]["KullaniciAdi"].ToString();
            string ProjeOdul = TabloProjeBilgileri.Rows[0]["Odul"].ToString();
            int ProjeZamanlama = Convert.ToInt32(TabloProjeBilgileri.Rows[0]["Zamanlama"].ToString());
            DateTime ProjeOlusturulmaTarihi = Convert.ToDateTime(TabloProjeBilgileri.Rows[0]["OlusturulmaTarihi"].ToString());
            DateTime ProjeBitisTarihi = ProjeOlusturulmaTarihi.AddDays(ProjeZamanlama);
            ProjeGizli = (TabloProjeBilgileri.Rows[0]["ProjeGizlilik"].ToString() == "1");
            ProjeKisisel = (TabloProjeBilgileri.Rows[0]["ProjeKisisel"].ToString() == "1");
            string ProjeGirdiSayisi = TabloProjeBilgileri.Rows[0]["GirdiSayisi"].ToString();
            bool ProjeKazanildi = (TabloProjeBilgileri.Rows[0]["KazananSayisi"].ToString() == "1");
            string ProjeDetayMarka = TabloProjeBilgileri.Rows[0]["DetayMarka"].ToString();
            string ProjeDetayMusteri = TabloProjeBilgileri.Rows[0]["DetayMusteri"].ToString();
            string ProjeDetayRakip = TabloProjeBilgileri.Rows[0]["DetayRakip"].ToString();
            string ProjeDetayOzellik = TabloProjeBilgileri.Rows[0]["DetayOzellik"].ToString();
            string ProjeDetayNot = TabloProjeBilgileri.Rows[0]["DetayNot"].ToString();

            lblProjeAdi.Text = ProjeAdi;
            lblProjeTip.Text = TipAdi;
            lblProjeSahibi.Text = ProjeSahibi;
            lblTabloHucreOdul.Text = ProjeOdul;
            lblTabloHucreBitisTarihi.Text = ProjeBitisTarihi.ToString("dd ") + AletKutusu.AyAdiTespitEt(ProjeBitisTarihi) + ProjeBitisTarihi.ToString(" yyyy HH:mm");
            lblTabloHucreTasarimSayisi.Text = ProjeGirdiSayisi;
            if (ProjeKazanildi)
            {
                lblTabloBaslikKazananTasarim.Text = "Kazanan Tasarım";
                lblTabloHucreKazananTasarim.Text = "#" + Veritabani.Sorgu_Scalar("SELECT TOP(1) g.GirdiNo FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=@ProjeID AND g.Kazanan=1", ProjeID);
            }

            string DosyaListesi = "";
            //Proje dosyaları tespit ediliyor
            DataTable TabloDosyalar = Veritabani.Sorgu_DataTable("SELECT Baslik, DosyaAdi FROM gp_ProjeDosyalar WHERE ProjeID=@ProjeID", ProjeID);
            if(TabloDosyalar.Rows.Count > 0)
            DosyaListesi = @"
                    <br /><br /><br /><h1 style=""color:#d1d3d4; text-shadow: 7px 5px 10px #d1d3d4;"">Proje Dosyaları</h1>";

            for (int d = 0; d < TabloDosyalar.Rows.Count; d++)
            {
                DosyaListesi += @"
                    <br />
                    <h2>" + TabloDosyalar.Rows[d]["Baslik"].ToString() + @"</h2>
                    <a target=""_blank"" href=""uploads/" + TabloDosyalar.Rows[d]["DosyaAdi"].ToString() + @""">" + TabloDosyalar.Rows[d]["DosyaAdi"].ToString() + @"</a>";
            }

            #endregion

            bool GizlilikAnlasmasiImzali = Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT COUNT(ID) FROM gp_ProjeGizlilikAnlasmalari WHERE ProjeID=@ProjeID AND UyeID=@UyeID", ProjeID, KullaniciID)) > 0;
            #region Anlaşma imzalama butonu görüntüleniyor
            if (ProjeGizli)
            {
                if (!GizlilikAnlasmasiImzali)
                {
                    lblProjeDetay.Visible = false;
                }
            }
            #endregion

            btnProjeGaleri.HRef = "proje.aspx?p=" + ProjeAdiSifreli;

            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

            #region Proje detayları ekrana yazdırılıyor
            string ProjeDetaylari = "";
            if(ProjeDetayMarka != "")
            {
                ProjeDetaylari += @"
                    <h1 style=""color:#d1d3d4; text-shadow: 7px -5px 10px #d1d3d4;"">Marka Hakkında</h1>
                    " + ProjeDetayMarka;
            }
            if (ProjeDetayMusteri != "")
            {
                ProjeDetaylari += @"
                    <h1>Müşteri Profili Hakkında</h1>
                    " + ProjeDetayMusteri;
            }
            if (ProjeDetayOzellik != "")
            {
                ProjeDetaylari += @"
                    <h1>Tercih Edilen Özellikler</h1>
                    " + ProjeDetayOzellik;
            }
            if (ProjeDetayNot != "")
            {
                ProjeDetaylari += @"
                    <h1>Ek Notlar</h1>
                    " + ProjeDetayNot;
            }
            if (DosyaListesi != "")
            {
                ProjeDetaylari += DosyaListesi;
            }

            lblProjeDetay.Text = ProjeDetaylari;
            #endregion
        }

    }
}
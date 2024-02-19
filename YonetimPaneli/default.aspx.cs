using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafikerPortal.YonetimPaneli
{
    public partial class _default : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;

        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();
        }

        protected void btnGiris_Click(object sender, EventArgs e)
        {
            string KullaniciAdi = txtKullaniciAdi2.Value.Trim();
            string Sifre = txtSifre2.Value;

            if (Veritabani.Sorgu_Scalar("SELECT COUNT(YoneticiID) FROM Yoneticiler WHERE KullaniciAdi=@kadi AND Sifre=@sifre",KullaniciAdi,Sifre) != "0" )
            {
                string KullaniciID = Veritabani.Sorgu_Scalar("Select YoneticiID FROM Yoneticiler WHERE KullaniciAdi=@kadi AND Sifre=@sifre",KullaniciAdi,Sifre);
                string Yetki = Veritabani.Sorgu_Scalar("Select Yetki FROM Yoneticiler WHERE KullaniciAdi=@kadi AND Sifre=@sifre", KullaniciAdi, Sifre);
                Session["KullaniciID"] = KullaniciID;
                Session["Yetki"] = Yetki;

                Response.Redirect("yonetimsayfa.aspx");
            }
        }
    }
}
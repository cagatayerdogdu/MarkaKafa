using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafikerPortal
{
    public partial class sifremi_unuttum : System.Web.UI.Page
    {
        DAL Veritabani = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGonder_Click(object sender, EventArgs e)
        {
            if (txtMailGir.Text.Length>0)
            {
                string MailKontrol = Veritabani.Sorgu_Scalar("SELECT UyeID FROM gp_Uyeler WHERE (Eposta=@Eposta)", txtMailGir.Text.ToString());
                if (MailKontrol!="")
                {
                    Random rd = new Random();
                    int sayilar = rd.Next(12345,67890);

                    int SifreDegistir = Veritabani.Sorgu_Calistir("UPDATE gp_Uyeler SET Sifre=@Sifre WHERE UyeID=@UyeID", sayilar.ToString(),MailKontrol);

                    if (SifreDegistir>0)
                    {
                        divSuccessSifreDegisti.Visible = true;
                        SifreGuncel.Text = txtMailGir.Text;

                        //Ödeme geldi e-postası gönderiliyor.
                        string MailKonu = "MarKa Kafa Üyelik Şifre Değişikliği.";
                        string MailIcerik = " Şifreniz "+ sayilar.ToString() + " olarak başarıyla güncellenmiştir.";
                        new Mail().MailGonder(Server, "", txtMailGir.Text.ToString().Trim(), MailKonu, MailIcerik);
                    }                    
                }
                else
                {
                    Response.Redirect("sifremi.unuttum.aspx");
                }


            }
        }
    }
}
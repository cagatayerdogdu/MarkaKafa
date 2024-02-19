using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal.YonetimPaneli
{
    public partial class yonetimportfolyo : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;

        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            string KullaniciAdi; string SignupEposta;

            if (Session["Yetki"] == null || Session["Yetki"].ToString() != "1")
            {
                Response.Redirect("default.aspx");
            }

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                string onayid = Request.QueryString["id"].ToString();
                Veritabani.Sorgu_Calistir("UPDATE gp_Uyeler SET PortfolyoOnayTalebi=0, Onay=1 WHERE UyeID=@id", onayid);
                Veritabani.Sorgu_Calistir("UPDATE gp_Portfolyo SET Aktif=1 WHERE UyeID=@id", onayid);

                SignupEposta = Veritabani.Sorgu_Scalar("SELECT Eposta FROM gp_Uyeler WHERE UyeID=@uye", onayid);
                KullaniciAdi = Veritabani.Sorgu_Scalar("SELECT KullaniciAdi FROM gp_Uyeler WHERE UyeID=@uye", onayid);
                //Üye Aktif e-postası gönderiliyor.
                string MailKonu = "MarKa Kafa Üyelik Portfolyo Onaylanması Hk.";
                string MailIcerik = "' " + KullaniciAdi + " ' isimli üyelik portfolyo aktifleştirilmiştir.\r\n\r\n\r\n MarKa Kafa ' yı tercih ettiğin için teşekkürler. " + DateTime.Now;
                new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                new Mail().MailGonder(Server, "", SignupEposta, MailKonu, MailIcerik);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["red"]))
            {
                string onayid = Request.QueryString["red"].ToString();
                Veritabani.Sorgu_Calistir("UPDATE gp_Uyeler SET PortfolyoOnayTalebi=0 WHERE UyeID=@id", onayid);

                SignupEposta = Veritabani.Sorgu_Scalar("SELECT Eposta FROM gp_Uyeler WHERE UyeID=@uye", onayid);
                KullaniciAdi = Veritabani.Sorgu_Scalar("SELECT KullaniciAdi FROM gp_Uyeler WHERE UyeID=@uye", onayid);
                //Üye Aktif e-postası gönderiliyor.
                string MailKonu = "MarKa Kafa Üyelik Portfolyo Onaylanması Hk.";
                string MailIcerik = "' " + KullaniciAdi + " ' isimli üyelik portfolyo içeriği nedeniyle red edilmiştir.\r\n\r\n\r\n MarKa Kafa ' yı tercih ettiğin için teşekkürler. " + DateTime.Now;
                new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                new Mail().MailGonder(Server, "", SignupEposta, MailKonu, MailIcerik);
            }

            DataTable dt = Veritabani.Sorgu_DataTable("SELECT UyeID, KullaniciAdi, PortfolyoOnayTalebi, PortfolyoOnayTalepTarihi, Ad, Soyad FROM gp_Uyeler WHERE PortfolyoOnayTalebi = 1");

            string TumTablo = @"<table style=""text-align:center"">
                    <tr>
                        <th>
                            Üye ID
                        </th>
                        <th>
                            Kullanıcı Adı
                        </th>
                        <th>
                            Portfolyo Onay Talebi
                        </th>
                        <th>
                            Portfolyo Onay Talebi Tarihi
                        </th>
                        <th style=""padding-left:50px;padding-right:50px"">
                            İsim
                        </th style=""padding-left:50px;padding-right:50px"">
                        <th>
                            Soyisim
                        </th>
                    </tr>";

            string butononay = "";
            string butonuyelistele = "";
            string uyeid;
            string butonreddet = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                uyeid = dt.Rows[i]["UyeID"].ToString();
                butononay = @"<a onclick='return confirm(""Üye Portfolyosu Onaylansın mı?"");' href='yonetimportfolyo.aspx?id=" + uyeid + "'>ONAYLA</a>";
                butonuyelistele = @"<a target='_blank' href='yonetimportfolyolistele.aspx?id=" + uyeid + "'>LİSTELE</a>";
                butonreddet = @"<a onclick='return confirm(""Üye Portfolyosu Reddedilsin mı?"");' href='yonetimportfolyo.aspx?red=" + uyeid + "'>REDDET</a>";

                TumTablo += @"
                    <tr>
                        <td>" + dt.Rows[i]["UyeID"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["KullaniciAdi"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["PortfolyoOnayTalebi"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["PortfolyoOnayTalepTarihi"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["Ad"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["Soyad"].ToString() + @"</td>
                        <td>" + butonuyelistele + " " + butononay + " " + butonreddet + @"</td>
                    </tr>";
            }
            TumTablo += "</table>";

            lblTablo.Text = TumTablo;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal.YonetimPaneli
{
    public partial class yonetimodemeler : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;

        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            if (Session["Yetki"]==null || Session["Yetki"].ToString()!="1")
            {
                Response.Redirect("default.aspx");
            }

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                string onayid = Request.QueryString["id"].ToString();          
                string SignupEposta = Veritabani.Sorgu_Scalar("SELECT  gp_Uyeler.Eposta FROM gp_Odemeler INNER JOIN gp_Projeler ON gp_Odemeler.ProjeID = gp_Projeler.ProjeID INNER JOIN gp_Uyeler ON gp_Projeler.UyeID = gp_Uyeler.UyeID WHERE gp_Odemeler.Onay = 0 and gp_Projeler.ProjeID=@proje", onayid);
                string ProjeAdi = Veritabani.Sorgu_Scalar("SELECT  gp_Projeler.ProjeAdi FROM gp_Odemeler INNER JOIN gp_Projeler ON gp_Odemeler.ProjeID = gp_Projeler.ProjeID INNER JOIN gp_Uyeler ON gp_Projeler.UyeID = gp_Uyeler.UyeID WHERE gp_Odemeler.Onay = 0 and gp_Projeler.ProjeID=@proje", onayid);
                string MailIcerik = "' " + ProjeAdi + " ' isimli projeniz onaylanmıştır.\r\n\r\n\r\n MarKa Kafa ' yı tercih ettiğin için teşekkürler. " + DateTime.Now;

                Veritabani.Sorgu_Calistir("UPDATE gp_Odemeler SET Onay=1 WHERE ProjeID=@id", onayid);
                Veritabani.Sorgu_Calistir("UPDATE gp_Odemeler SET OdemeTarihi=getdate() WHERE ProjeID=@id", onayid);
                Veritabani.Sorgu_Calistir("UPDATE gp_Projeler SET Durum=2 WHERE ProjeID=@id", onayid);

                //Ödeme Onay e-postası gönderiliyor.
                string MailKonu = "MarKa Kafa Proje Onaylanması Hk.";
                new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                new Mail().MailGonder(Server, "", SignupEposta, MailKonu, MailIcerik);
            }

            DataTable dt = Veritabani.Sorgu_DataTable("SELECT gp_Odemeler.OlusturulmaTarihi, gp_Odemeler.ProjeID, gp_Projeler.UyeID, gp_Uyeler.KullaniciAdi, gp_Uyeler.Ad, gp_Uyeler.Soyad FROM gp_Odemeler INNER JOIN gp_Projeler ON gp_Odemeler.ProjeID = gp_Projeler.ProjeID INNER JOIN gp_Uyeler ON gp_Projeler.UyeID = gp_Uyeler.UyeID WHERE gp_Odemeler.Onay = 0 ORDER BY gp_Odemeler.OlusturulmaTarihi DESC");

            string TumTablo = @"<table style=""text-align:center"">
                    <tr>
                        <th>
                            Oluşturulma Tarihi
                        </th>
                        <th>
                            Proje ID
                        </th>
                        <th>
                            Uye ID
                        </th>
                        <th>
                            Kullanıcı Adı
                        </th>
                        <th style=""padding-left:50px;padding-right:50px"">
                            İsim
                        </th>
                        <th style=""padding-left:50px;padding-right:50px"">
                            Soyisim
                        </th>
                        <th style=""padding-left:50px;padding-right:50px"">
                            
                        </th>
                    </tr>";

            string butononay = "";
            string projeid;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                projeid = dt.Rows[i]["ProjeID"].ToString();
                butononay = @"<a onclick='return confirm(""Üye Ödemesi Onaylansın mı?"");' href='yonetimodemeler.aspx?id=" + projeid + "'>ONAYLA</a>";

                TumTablo += @"
                    <tr>
                        <td>" + dt.Rows[i]["OlusturulmaTarihi"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["ProjeID"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["UyeID"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["KullaniciAdi"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["Ad"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["Soyad"].ToString() + @"</td>
                        <td>" + butononay + @"</td>
                    </tr>";
            }
            TumTablo += "</table>";

            lblTablo.Text = TumTablo;
        }
    }
}
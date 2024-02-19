using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafikerPortal.YonetimPaneli
{
    public partial class yonetimyorumonay : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;

        string SignupEposta; string KullaniciAdi; string YapilanYorum;

        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();
            if (Session["Yetki"]==null || Session["Yetki"].ToString()!="1")
            {
                Response.Redirect("default.aspx");
            }

            if (!string.IsNullOrEmpty(Request.QueryString["sil"]))
            {
                string SilYorumid = Request.QueryString["sil"].ToString();

                SignupEposta = Veritabani.Sorgu_Scalar("SELECT  u.Eposta FROM gp_Yorumlar AS y JOIN gp_Uyeler AS u ON y.UyeID=u.UyeID  WHERE y.Aktif='0' and y.YorumID=@yorumid", SilYorumid);
                KullaniciAdi = Veritabani.Sorgu_Scalar("SELECT  u.KullaniciAdi FROM gp_Yorumlar AS y JOIN gp_Uyeler AS u ON y.UyeID=u.UyeID  WHERE y.Aktif='0' and y.YorumID=@yorumid", SilYorumid);
                YapilanYorum = Veritabani.Sorgu_Scalar("SELECT  y.Yorum FROM gp_Yorumlar AS y JOIN gp_Uyeler AS u ON y.UyeID=u.UyeID  WHERE y.Aktif='0' and y.YorumID=@yorumid", SilYorumid); 

                Veritabani.Sorgu_Calistir("DELETE FROM gp_Yorumlar WHERE YorumID=@SilYorumid", SilYorumid);

                //Yorum Silme e-postası gönderiliyor.
                string MailKonu = "MarKa Kafa - Yapılan Yorum Silinmesi Hk.";
                string MailIcerik = "' " + KullaniciAdi + " ' isimli kullanıcıya yapmış olduğunuz ' "+YapilanYorum+" ' yorum içeriğinden dolayı silinmiştir.\r\n\r\n\r\n MarKa Kafa ' yı tercih ettiğin için teşekkürler. " + DateTime.Now;
                new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                new Mail().MailGonder(Server, "", SignupEposta, MailKonu, MailIcerik);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["onay"]))
            {
                string onayla = Request.QueryString["onay"].ToString();
                string onayid = Request.QueryString["id"].ToString();

                SignupEposta = Veritabani.Sorgu_Scalar("SELECT  u.Eposta FROM gp_Yorumlar AS y JOIN gp_Uyeler AS u ON y.UyeID=u.UyeID  WHERE y.Aktif='0' and y.YorumID=@yorumid", onayid);
                KullaniciAdi = Veritabani.Sorgu_Scalar("SELECT  u.KullaniciAdi FROM gp_Yorumlar AS y JOIN gp_Uyeler AS u ON y.UyeID=u.UyeID  WHERE y.Aktif='0' and y.YorumID=@yorumid", onayid);
                YapilanYorum = Veritabani.Sorgu_Scalar("SELECT  y.Yorum FROM gp_Yorumlar AS y JOIN gp_Uyeler AS u ON y.UyeID=u.UyeID  WHERE y.Aktif='0' and y.YorumID=@yorumid", onayid); 

                //Yorum Onay e-postası gönderiliyor.
                string MailKonu = "MarKa Kafa - Yapılan Yorum Onayı Hk.";
                string MailIcerik = "' " + KullaniciAdi + " ' isimli kullanıcıya yapmış olduğunuz ' " + YapilanYorum + " ' yorum onaylanmıştır.\r\n\r\n\r\n MarKa Kafa ' yı tercih ettiğin için teşekkürler. " + DateTime.Now;
                new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                new Mail().MailGonder(Server, "", SignupEposta, MailKonu, MailIcerik);

                Veritabani.Sorgu_Calistir("UPDATE gp_Yorumlar SET Aktif=@aktif WHERE YorumID=@id",onayla,onayid);
            }

            DataTable dt = Veritabani.Sorgu_DataTable("SELECT y.YorumID,y.Tarih,y.Aktif, y.Yorum, u.UyeID, u.KullaniciAdi, u.Avatar, u.UyelikTipi FROM gp_Yorumlar AS y JOIN gp_Uyeler AS u ON y.UyeID=u.UyeID  WHERE y.Aktif='0' order by y.Tarih desc");

            string TumTabloDiv = @"<div style = ""margin-left: 350px; width: 560px;height: 860px;"" >";

            string TumTablo = @"
                <table>
                    <tr>
                        <th>
                            Tarih
                        </th>
                        <th>
                            Aktif
                        </th>
                        <th>
                            Yorum
                        </th>
                        <th>
                            KullaniciAdi
                        </th>
                        <th>
                            Avatar
                        </th>
                        <th>
                            UyelikTipi
                        </th>

                        <th style:""padding:300px"">
                            
                        </th>
                    </tr>";

            string butonsil = "";
            string butononay = "";
            string YorumID = "";
            string onay = "";
                           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                YorumID = dt.Rows[i]["YorumID"].ToString();
                onay = dt.Rows[i]["Aktif"].ToString();

                butonsil = @"<a onclick='return confirm(""Yorum Silinecek Emin misin?"");' href='yonetimyorumonay.aspx?sil=" + YorumID + "'>SİL</a>";

                if (onay=="0")
                {
                    butononay = @"<a onclick='return confirm(""Yorum Onaylansın mı?"");' href='yonetimyorumonay.aspx?onay=1&id=" + YorumID + "'>ONAYLA</a>";
                }


                TumTablo += @"
                    <tr>
                        <td>" + dt.Rows[i]["Tarih"].ToString() +@"</td>
                        <td>" + dt.Rows[i]["Aktif"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["Yorum"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["KullaniciAdi"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["Avatar"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["UyelikTipi"].ToString() + @"</td>
                        <td>"+butonsil+" "+butononay+@"</td>
                    </tr>";
            }
            TumTablo += "</table> </div>";

            lblTablo.Text = TumTabloDiv + TumTablo;

        }
    }
}
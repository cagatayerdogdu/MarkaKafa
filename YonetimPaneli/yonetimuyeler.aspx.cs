using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal.YonetimPaneli
{
    public partial class yonetimuyeler : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;

        string SignupEposta; string KullaniciAdi;

        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            if (Session["Yetki"]== null || Session["Yetki"].ToString()!= "1")
            {
                Response.Redirect("default.aspx");
            }

            if (!string.IsNullOrEmpty(Request.QueryString["sil"]))
            {
                string siluyeid = Request.QueryString["sil"].ToString();
                SignupEposta = Veritabani.Sorgu_Scalar("SELECT Eposta FROM gp_Uyeler WHERE UyeID=@uye", siluyeid);
                KullaniciAdi = Veritabani.Sorgu_Scalar("SELECT KullaniciAdi FROM gp_Uyeler WHERE UyeID=@uye", siluyeid); 
                Veritabani.Sorgu_Calistir("DELETE FROM gp_Uyeler WHERE UyeID=@silid",siluyeid);

                //Üye Silme e-postası gönderiliyor.
                string MailKonu = "MarKa Kafa Üyelik Silinmesi Hk.";
                string MailIcerik = "' " + KullaniciAdi + " ' isimli üyelik silinmiştir.\r\n\r\n\r\n MarKa Kafa ' yı tercih ettiğin için teşekkürler. " + DateTime.Now;
                new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                new Mail().MailGonder(Server, "", SignupEposta, MailKonu, MailIcerik);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["aktif"]))
            {
                string aktifpasif = Request.QueryString["aktif"].ToString();
                string aktifpasifid = Request.QueryString["id"].ToString();
                Veritabani.Sorgu_Calistir("UPDATE gp_Uyeler SET Aktif=@durum WHERE UyeID=@id",aktifpasif,aktifpasifid);

                SignupEposta = Veritabani.Sorgu_Scalar("SELECT Eposta FROM gp_Uyeler WHERE UyeID=@uye", aktifpasifid);
                KullaniciAdi = Veritabani.Sorgu_Scalar("SELECT KullaniciAdi FROM gp_Uyeler WHERE UyeID=@uye", aktifpasifid);  
                //Üye Aktif e-postası gönderiliyor.
                string MailKonu = "MarKa Kafa Üyelik Aktifleştirilmesi Hk.";
                string MailIcerik ="' "+ KullaniciAdi + " ' isimli üyelik aktifleştirilmiştir.\r\n\r\n\r\n MarKa Kafa ' yı tercih ettiğin için teşekkürler. " + DateTime.Now;
                new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                new Mail().MailGonder(Server, "", SignupEposta, MailKonu, MailIcerik);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["onay"]))
            {
                string onayla = Request.QueryString["onay"].ToString();
                string onayid = Request.QueryString["id"].ToString();
                Veritabani.Sorgu_Calistir("UPDATE gp_Uyeler SET Onay=@durum WHERE UyeID=@id",onayla,onayid);

                SignupEposta = Veritabani.Sorgu_Scalar("SELECT Eposta FROM gp_Uyeler WHERE UyeID=@uye", onayid);
                KullaniciAdi = Veritabani.Sorgu_Scalar("SELECT KullaniciAdi FROM gp_Uyeler WHERE UyeID=@uye", onayid); 

                //Üye Onay e-postası gönderiliyor.
                string MailKonu = "MarKa Kafa Üyelik Onaylanması Hk.";
                string MailIcerik = "' " + KullaniciAdi + " ' isimli üyelik onaylanmıştır.\r\n\r\n\r\n MarKa Kafa ' yı tercih ettiğin için teşekkürler. " + DateTime.Now;
                new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                new Mail().MailGonder(Server, "", SignupEposta, MailKonu, MailIcerik);
            }

            DataTable dt = Veritabani.Sorgu_DataTable("SELECT UyeID,KullaniciAdi,Sifre,Eposta,Aktif,Telefon,UyelikTipi,Ad,Soyad,Onay,IptalTalebi FROM gp_Uyeler ORDER BY UyeID DESC");

            string TumTabloDiv = @"<div style = ""margin-left: 200px; width: 640px;"" >";
            string TumTablo = @"<table>
                    <tr>
                        <th>
                            Kullanıcı Adı
                        </th>
                        <th>
                            Şifre
                        </th>
                        <th>
                            E-Posta
                        </th>
                        <th>
                            Aktif
                        </th>
                        <th>
                            Telefon
                        </th>
                        <th>
                            Üyelik Tipi
                        </th>
                        <th>
                            Ad
                        </th>
                        <th>
                            Soyad
                        </th>
                        <th>
                            Onay
                        </th>
                        <th>
                           İptal Talebi
                        </th>
                        <th style:""padding:30px"">
                            
                        </th>
                    </tr>";

            string butonsil = "";
            string butononay = "";
            string butonaktif = "";
            string kullaniciuyeid = "";
            string uyeaktifligi = "";
            string onay = "";
                           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                kullaniciuyeid = dt.Rows[i]["UyeID"].ToString();
                uyeaktifligi = dt.Rows[i]["Aktif"].ToString();
                onay = dt.Rows[i]["Onay"].ToString();
                
                if (uyeaktifligi=="1")
                {
                    butonaktif = @"<a onclick='return confirm(""Üye Pasif Hale Getirilsin mi?"");' href='yonetimuyeler.aspx?aktif=0&id="+kullaniciuyeid+"'>PASİF YAP</a>";
                }
                else
                {
                    butonaktif = @"<a onclick='return confirm(""Üye Aktif Hale Getirilsin mi?"");' href='yonetimuyeler.aspx?aktif=1&id="+kullaniciuyeid+"'>AKTİF YAP</a>";
                }

                butonsil = @"<a onclick='return confirm(""Üye Silinecek Emin misin?"");' href='yonetimuyeler.aspx?sil="+kullaniciuyeid+"'>SİL</a>";

                if (onay=="1")
                {
                    butononay = @"<a onclick='return confirm(""Üye Onayı Kaldırılsın mı?"");' href='yonetimuyeler.aspx?onay=0&id="+kullaniciuyeid+"'>ONAY KALDIR</a>";
                }
                else
                {
                    butononay = @"<a onclick='return confirm(""Üye Onaylansın mı?"");' href='yonetimuyeler.aspx?onay=1&id=" + kullaniciuyeid + "'>ONAYLA</a>";
                }

                TumTablo += @"
                    <tr>
                        <td>"+ dt.Rows[i]["KullaniciAdi"].ToString() +@"</td>
                        <td>" + dt.Rows[i]["Sifre"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["Eposta"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["Aktif"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["Telefon"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["UyelikTipi"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["Ad"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["Soyad"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["Onay"].ToString() + @"</td>
                        <td>" + dt.Rows[i]["IptalTalebi"].ToString() + @"</td>
                        <td>"+butonsil+" "+butonaktif+" "+butononay+@"</td>
                    </tr>";
            }
            TumTablo += "</table> </div>";

            lblTablo.Text = TumTabloDiv + TumTablo;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafikerPortal
{
    public partial class PeriyodikKontroller : System.Web.UI.Page
    {
        DAL Veritabani; Mail Eposta;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); Eposta = new Mail();

            #region Periyodik kontroller gerçekleştiriliyor
            string ProjeID = ""; string ProjeAdi = ""; DateTime BitisTarihi; string BitisTarihiString = ""; string UyeID = ""; string UyeAd = ""; string UyeSoyad = ""; string UyeEposta = "";
            string MailKonu = ""; string MailIcerik = ""; string OdemeProjeID = ""; string OdemeEPosta = ""; string OdemeUyeAd = ""; string OdemeUyeSoyad = ""; string OdemeProjeAdi = ""; string OdemeOlusturmaTarihiString = ""; DateTime OdemeOlusturmaTarihi;
            DataTable TabloKontrol; DataTable OdemeTabloKontrol;
            #region Süresi geçmiş projeler tamamlandı olarak işaretleniyor
            TabloKontrol = Veritabani.Sorgu_DataTable("SELECT p.ProjeID, p.ProjeAdi, DATEADD(day, p.Zamanlama, p.OlusturulmaTarihi) AS BitisTarihi, p.UyeID, u.Ad, u.Soyad, u.Eposta FROM gp_Projeler AS p JOIN gp_Uyeler AS u ON p.UyeID=u.UyeID WHERE p.Durum=2 AND DATEADD(day, p.Zamanlama, p.OlusturulmaTarihi)<= GETDATE()");
            for (int i = 0; i < TabloKontrol.Rows.Count; i++)
            {
                ProjeID = TabloKontrol.Rows[i]["ProjeID"].ToString();
                ProjeAdi = TabloKontrol.Rows[i]["ProjeAdi"].ToString();
                BitisTarihi = Convert.ToDateTime(TabloKontrol.Rows[i]["BitisTarihi"].ToString());
                BitisTarihiString = BitisTarihi.ToString("dd.MM.yyyy HH:mm");
                UyeID = TabloKontrol.Rows[i]["UyeID"].ToString();
                UyeAd = TabloKontrol.Rows[i]["Ad"].ToString();
                UyeSoyad = TabloKontrol.Rows[i]["Soyad"].ToString();
                UyeEposta = TabloKontrol.Rows[i]["Eposta"].ToString();

                //Proje kapatılıyor.
                Veritabani.Sorgu_Calistir("UPDATE gp_Projeler SET Durum=3 WHERE ProjeID=@ProjeID", ProjeID);

                //Üyeye bildirim e-postası gönderiliyor.
                MailKonu = "Projenizin süresi sona erdi!";
                MailIcerik = "Sayın " + UyeAd + " " + UyeSoyad + ",<br />" + ProjeAdi + " isimli projeniz " + BitisTarihiString + " tarihinde sona ermiştir. Projenizin bitim tarihinden önce kazanan tasarımcı seçmediğinizden dolayı projeniz tamamlandı olarak işaretlenmiştir.<br />Eğer projenizin tamamlanmadığını düşünüyorsanız, lütfen yeni bir proje başlatınız.<br /><br />MarkaKafa'yı tercih ettiğiniz için teşekkür ederiz.";
                Eposta.MailGonder(Server, "", UyeEposta, MailKonu, MailIcerik);
            }
            #endregion

            #region Süresi geçmiş projeler tamamlandı olarak işaretleniyor
            TabloKontrol = Veritabani.Sorgu_DataTable("SELECT p.ProjeID, p.ProjeAdi, DATEADD(day, p.Zamanlama, p.OlusturulmaTarihi) AS BitisTarihi, p.UyeID, u.Ad, u.Soyad, u.Eposta FROM gp_Projeler AS p JOIN gp_Uyeler AS u ON p.UyeID=u.UyeID WHERE p.Durum=2 AND DATEADD(day, p.Zamanlama, p.OlusturulmaTarihi)<= GETDATE()");
            for (int i = 0; i < TabloKontrol.Rows.Count; i++)
            {
                ProjeID = TabloKontrol.Rows[i]["ProjeID"].ToString();
                ProjeAdi = TabloKontrol.Rows[i]["ProjeAdi"].ToString();
                BitisTarihi = Convert.ToDateTime(TabloKontrol.Rows[i]["BitisTarihi"].ToString());
                BitisTarihiString = BitisTarihi.ToString("dd.MM.yyyy HH:mm");
                UyeID = TabloKontrol.Rows[i]["UyeID"].ToString();
                UyeAd = TabloKontrol.Rows[i]["Ad"].ToString();
                UyeSoyad = TabloKontrol.Rows[i]["Soyad"].ToString();
                UyeEposta = TabloKontrol.Rows[i]["Eposta"].ToString();

                //Proje kapatılıyor.
                Veritabani.Sorgu_Calistir("UPDATE gp_Projeler SET Durum=3 WHERE ProjeID=@ProjeID", ProjeID);

                //İşlem loglanıyor.
                Veritabani.Sorgu_Calistir("INSERT INTO gp_LogBildirim(ProjeID, BildirimTarihi, BildirimTipi) VALUES(@ProjeID, GETDATE(), @BildirimTipi)", ProjeID, "bitti");

                //Üyeye bildirim e-postası gönderiliyor.
                MailKonu = "Projenizin süresi sona erdi!";
                MailIcerik = "Sayın " + UyeAd + " " + UyeSoyad + ",<br />" + ProjeAdi + " isimli projeniz " + BitisTarihiString + " tarihinde sona ermiştir. Projenizin bitim tarihinden önce kazanan tasarımcı seçmediğinizden dolayı projeniz tamamlandı olarak işaretlenmiştir.<br />Eğer projenizin tamamlanmadığını düşünüyorsanız, lütfen yeni bir proje başlatınız.<br /><br />MarkaKafa'yı tercih ettiğiniz için teşekkür ederiz.";
                Eposta.MailGonder(Server, "", UyeEposta, MailKonu, MailIcerik);
            }
            #endregion

            #region Bitimine 1 gün kalmış projelerin sahiplerine bildirim gönderiliyor
            TabloKontrol = Veritabani.Sorgu_DataTable("SELECT p.ProjeID, p.ProjeAdi, DATEADD(day, p.Zamanlama, p.OlusturulmaTarihi) AS BitisTarihi, p.UyeID, u.Ad, u.Soyad, u.Eposta FROM gp_Projeler AS p JOIN gp_Uyeler AS u ON p.UyeID=u.UyeID WHERE p.Durum=2 AND DATEADD(day, p.Zamanlama, p.OlusturulmaTarihi)>=DATEADD(day, -1, GETDATE()) AND (SELECT COUNT(ID) FROM gp_LogBildirim WHERE ProjeID=p.ProjeID)=0");
            for (int i = 0; i < TabloKontrol.Rows.Count; i++)
            {
                ProjeID = TabloKontrol.Rows[i]["ProjeID"].ToString();
                ProjeAdi = TabloKontrol.Rows[i]["ProjeAdi"].ToString();
                BitisTarihi = Convert.ToDateTime(TabloKontrol.Rows[i]["BitisTarihi"].ToString());
                BitisTarihiString = BitisTarihi.ToString("dd.MM.yyyy HH:mm");
                UyeID = TabloKontrol.Rows[i]["UyeID"].ToString();
                UyeAd = TabloKontrol.Rows[i]["Ad"].ToString();
                UyeSoyad = TabloKontrol.Rows[i]["Soyad"].ToString();
                UyeEposta = TabloKontrol.Rows[i]["Eposta"].ToString();

                //İşlem loglanıyor.
                Veritabani.Sorgu_Calistir("INSERT INTO gp_LogBildirim(ProjeID, BildirimTarihi, BildirimTipi) VALUES(@ProjeID, GETDATE(), @BildirimTipi)", ProjeID, "bitti");

                //Üyeye bildirim e-postası gönderiliyor.
                MailKonu = "Projenizin süresi yarın sona eriyor!";
                MailIcerik = "Sayın " + UyeAd + " " + UyeSoyad + ",<br />" + ProjeAdi + " isimli projeniz " + BitisTarihiString + " tarihinde sona erecektir. Projenizin bitim tarihinden önce kazanan tasarımcı seçmezseniz, projeniz yarın otomatik olarak sonlandırılacaktır. Ek süreye ihtiyacınız olduğunu düşünüyorsanız, lütfen proje sayfanızdan proje süresini uzatınız.<br /><br />MarkaKafa'yı tercih ettiğiniz için teşekkür ederiz.";
                Eposta.MailGonder(Server, "", UyeEposta, MailKonu, MailIcerik);
            }
            #endregion

            #region Proje Açıldıktan 3 gün geçmiş ve Ödeme yapılmamış projeler

            //3 gündür ödeme yapılmayan projeler tespit ediliyor.
            //OdemeTabloKontrol = Veritabani.Sorgu_DataTable(@"SELECT ProjeID FROM gp_Odemeler WHERE Onay=0 AND OlusturulmaTarihi > getdate() -3 AND OdemeTarihi IS NULL AND OdemeYapilmamisMail=0");
            int OdemeKontrol;

            SqlConnection baglanti = new SqlConnection(@"Data Source=localhost; Initial Catalog=DB131220161833; User=sa; Password=prx@2005;");
            baglanti.Open();
            SqlCommand KomutOdeme = new SqlCommand(@"SELECT ProjeID FROM gp_Odemeler WHERE Onay=0 AND OlusturulmaTarihi > getdate() -3 AND OdemeTarihi IS NULL AND OdemeYapilmamisMail=0", baglanti);
            OdemeTabloKontrol = new DataTable();            
            SqlDataAdapter da = new SqlDataAdapter(KomutOdeme);
            da.Fill(OdemeTabloKontrol);
            OdemeKontrol = int.Parse(KomutOdeme.ExecuteScalar().ToString());

            if (OdemeKontrol>0)
            {
                
                for (int i = 0; i <OdemeTabloKontrol.Rows.Count; i++)
                {
                //OdemeOnay = TabloKontrol.Rows[i]["Onay"].ToString();
                OdemeProjeID = OdemeTabloKontrol.Rows[i]["ProjeID"].ToString();

               DataTable OdemeTabloKontrol2 = Veritabani.Sorgu_DataTable(@"SELECT TOP 1 u.Eposta,p.ProjeAdi,u.Ad,u.Soyad,o.OlusturulmaTarihi FROM gp_Odemeler o JOIN gp_Projeler p ON o.ProjeID=p.ProjeID JOIN gp_Uyeler u ON u.UyeID=p.UyeID WHERE o.Onay=0 AND o.OlusturulmaTarihi > GETDATE() -3 AND o.OdemeTarihi IS NULL AND OdemeYapilmamisMail=0");

                OdemeUyeAd = OdemeTabloKontrol2.Rows[i]["Ad"].ToString();
                OdemeUyeSoyad = OdemeTabloKontrol2.Rows[i]["Soyad"].ToString();
                OdemeEPosta = OdemeTabloKontrol2.Rows[i]["Eposta"].ToString();
                OdemeOlusturmaTarihi = Convert.ToDateTime(OdemeTabloKontrol2.Rows[i]["OlusturulmaTarihi"].ToString());
                OdemeOlusturmaTarihiString = OdemeOlusturmaTarihi.ToString();
                OdemeProjeAdi = OdemeTabloKontrol2.Rows[i]["ProjeAdi"].ToString();

                //Üyeye bildirim e-postası gönderiliyor.
                MailKonu = "Projenizin ödemesi yapılmamış!";
                MailIcerik = "Sayın " + OdemeUyeAd + " " + OdemeUyeSoyad + ",<br />" + OdemeOlusturmaTarihiString + " tarihinde ödeme kaydı oluşturulan, " + OdemeProjeAdi + " isimli projenizin ödemesi hala gerçekleştirilmemiştir. Projeniz için gerekli ödemeyi gerçekleştirmezseniz, projeniz otomatik olarak sonlandırılacaktır. Ek süreye ihtiyacınız olduğunu düşünüyorsanız, lütfen bu mail üzerinden bizimle irtibata geçiniz.<br /><br />MarkaKafa'yı tercih ettiğiniz için teşekkür ederiz.";
                Eposta.MailGonder(Server, "", OdemeEPosta, MailKonu, MailIcerik);

                //Ödeme Mail Gönderilince loglanıyor.
                Veritabani.Sorgu_Calistir(@"INSERT INTO gp_LogBildirim(ProjeID,BildirimTarihi,BildirimTipi) VALUES(@ProjeID, GETDATE(), @BildirimTipi)", OdemeProjeID, "OdemeMailGonderildi");

                //Ödeme Mail Gönderilince işaretleniyor.
                Veritabani.Sorgu_Calistir(@"UPDATE gp_Odemeler SET OdemeYapilmamisMail=1 WHERE ProjeID=@projeid", OdemeProjeID);
                }
                baglanti.Close();
            }
                

            #endregion

            #endregion

        }
    }
}
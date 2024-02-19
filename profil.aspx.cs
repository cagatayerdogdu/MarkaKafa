using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using ImageResizer;
using Kaliko.ImageLibrary;

namespace GrafikerPortal
{
    public partial class profil : System.Web.UI.Page
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
            DataTable TabloUyeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) UyelikTipi, IptalTalebi, Avatar, KullaniciAdi, Eposta, Meslek, NeredenDuydunuz, Telefon, WebSitesi, Blog, Hakkinda, UzmanlikGrafikTasarim, UzmanlikDijitalTasarim, UzmanlikEndustriyelTasarim, UzmanlikReklamYazarligi, UzmanlikIllustrasyon, HatirlatmaPeriyodu, HatirlatProjeler, HatirlatGelismeler FROM gp_Uyeler WHERE UyeID=@UyeID", Session["UyeID"].ToString());
            string UyeTip = TabloUyeBilgileri.Rows[0]["UyelikTipi"].ToString();
            bool UyeIptalTalebi = (TabloUyeBilgileri.Rows[0]["IptalTalebi"].ToString() == "1");
            string UyeAvatar = TabloUyeBilgileri.Rows[0]["Avatar"].ToString();
            string UyeNeredenDuydunuz = TabloUyeBilgileri.Rows[0]["NeredenDuydunuz"].ToString();
            string UyeHatirlatmaPeriyodu = TabloUyeBilgileri.Rows[0]["HatirlatmaPeriyodu"].ToString();

            //Text alanları dolduruluyor
            txtSirketAd.Text = TabloUyeBilgileri.Rows[0]["KullaniciAdi"].ToString();
            //profile_profession.Value = TabloUyeBilgileri.Rows[0]["Meslek"].ToString();
            profile_phone.Value = TabloUyeBilgileri.Rows[0]["Telefon"].ToString();
            profile_eposta.Value = TabloUyeBilgileri.Rows[0]["Eposta"].ToString();
            //profile_blog.Value = TabloUyeBilgileri.Rows[0]["Blog"].ToString();
           // profile_biography.Value = TabloUyeBilgileri.Rows[0]["Hakkinda"].ToString().Replace("<br />", Environment.NewLine);
            //profile_graphics_designer.Checked = (TabloUyeBilgileri.Rows[0]["UzmanlikGrafikTasarim"].ToString() == "1");
            //profile_web_designer.Checked = (TabloUyeBilgileri.Rows[0]["UzmanlikDijitalTasarim"].ToString() == "1");
            //profile_industrial_designer.Checked = (TabloUyeBilgileri.Rows[0]["UzmanlikEndustriyelTasarim"].ToString() == "1");
            //profile_writer.Checked = (TabloUyeBilgileri.Rows[0]["UzmanlikReklamYazarligi"].ToString() == "1");
            //profile_illustrator.Checked = (TabloUyeBilgileri.Rows[0]["UzmanlikIllustrasyon"].ToString() == "1");
            //profile_deliver_project_invitations.Checked = (TabloUyeBilgileri.Rows[0]["HatirlatProjeler"].ToString() == "1");
            profile_deliver_news.Checked = (TabloUyeBilgileri.Rows[0]["HatirlatGelismeler"].ToString() == "1");

            //Bizi nereden duydunuz seçeneği seçiliyor
            //for (int i = 0; i < profile_how_did_you_hear.Items.Count; i++)
            //{
            //    if (profile_how_did_you_hear.Items[i].Value == UyeNeredenDuydunuz)
            //        profile_how_did_you_hear.SelectedIndex = i;
            //}
            ////Hatırlatma periyodu seçeneği seçiliyor
            //for (int i = 0; i < profile_deliver_project_news.Items.Count; i++)
            //{
            //    if (profile_deliver_project_news.Items[i].Value == UyeHatirlatmaPeriyodu)
            //        profile_deliver_project_news.SelectedIndex = i;
            //}
            //Avatar resmi yüklenmişse resim görüntüleniyor
            if (UyeAvatar != "") imgAvatar.Src = "images/avatars/120/" + UyeAvatar;
            #endregion
            #region Üye tipine göre sayfa formatlanıyor
            if (UyeTip == "2") //Tasarımcı
            {
                //lblSirketAd.Text = "Kullanıcı adı";
                //lblSektor.Text = "Mesleğiniz";
                //lblSirketTanimi.Text = "Hakkınızda";
                //divBiziNeredenDuydunuz.Visible = true;
                //divUzmanlikAlani.Visible = true;
                //divHatirlatmaPeriyodu.Visible = true;
                //divHatirlatmaProjeler.Visible = true;
            }
            #endregion
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            divErrorFile.Visible = false; divSuccessProfilGuncelle.Visible = false; divSuccessProfilIptal.Visible = false; divErrorProfilGuncelle.Visible = false; divErrorProfilIptal.Visible = false;
            //string Meslek = profile_profession.Value;
            string Telefon = profile_phone.Value;
            string Eposta = profile_eposta.Value;
            //if (WebSitesi.Length > 0 && WebSitesi.Substring(0, 7) != "http://") WebSitesi = "http://" + WebSitesi; (kaldırıldı)
           // string Blog = profile_blog.Value;
            //if (Blog.Length > 0 && Blog.Substring(0, 7) != "http://") Blog = "http://" + Blog;
            //string Hakkinda = profile_biography.Value.Trim();
            //Hakkinda = AletKutusu.HtmlGuvenligi(Hakkinda); //HTML ve JavaScript ataklarına karşı güvenlik sağlanıyor.
            //Hakkinda = Hakkinda.Replace(Environment.NewLine, "<br />"); //Satır ayraçları formatlanıyor
            //string UzmanlikGrafikTasarim = (profile_graphics_designer.Checked) ? "1" : "0";
            //string UzmanlikDijitalTasarim = (profile_web_designer.Checked) ? "1" : "0";
            //string UzmanlikEndustriyelTasarim = (profile_industrial_designer.Checked) ? "1" : "0";
            //string UzmanlikReklamYazarligi = (profile_writer.Checked) ? "1" : "0";
            //string UzmanlikIllustrasyon = (profile_illustrator.Checked) ? "1" : "0";
            //string NeredenDuydunuz = profile_how_did_you_hear.Items[profile_how_did_you_hear.SelectedIndex].Value;
            //string HatirlatmaPeriyodu = profile_deliver_project_news.Items[profile_deliver_project_news.SelectedIndex].Value;
            //string HatirlatProjeler = (profile_deliver_project_invitations.Checked) ? "1" : "0";
            string HatirlatGelismeler = (profile_deliver_news.Checked) ? "1" : "0";

            int KayitSayisi = Veritabani.Sorgu_Calistir("UPDATE gp_Uyeler SET Telefon=@Telefon, Eposta=@Eposta, HatirlatGelismeler=@HatirlatGelismeler WHERE UyeID=@UyeID", Telefon, Eposta, HatirlatGelismeler, Session["UyeID"].ToString());
            
           // int KayitSayisi = Veritabani.Sorgu_Calistir("UPDATE gp_Uyeler SET Meslek=@Meslek, Telefon=@Telefon, Eposta=@Eposta, Blog=@Blog, Hakkinda=@Hakkinda, UzmanlikGrafikTasarim=@UzmanlikGrafikTasarim, UzmanlikDijitalTasarim=@UzmanlikDijitalTasarim, UzmanlikEndustriyelTasarim=@UzmanlikEndustriyelTasarim, UzmanlikReklamYazarligi=@UzmanlikReklamYazarligi, UzmanlikIllustrasyon=@UzmanlikIllustrasyon, NeredenDuydunuz=@NeredenDuydunuz, HatirlatmaPeriyodu=@HatirlatmaPeriyodu, HatirlatProjeler=@HatirlatProjeler, HatirlatGelismeler=@HatirlatGelismeler WHERE UyeID=@UyeID", Meslek, Telefon, Eposta, Blog, Hakkinda, UzmanlikGrafikTasarim, UzmanlikDijitalTasarim, UzmanlikEndustriyelTasarim, UzmanlikReklamYazarligi, UzmanlikIllustrasyon, NeredenDuydunuz, HatirlatmaPeriyodu, HatirlatProjeler, HatirlatGelismeler, Session["UyeID"].ToString());
            if (KayitSayisi > 0)
            {
                divSuccessProfilGuncelle.Visible = true;
            }
            else
            {
                divErrorProfilGuncelle.Visible = true;
            }


            string Password = profile_password.Value;
            string Password_Again = profile_password_again.Value;

            if (Password!="" && Password_Again==Password)
            {
                int SifreDegistir = Veritabani.Sorgu_Calistir("UPDATE gp_uyeler SET Sifre=@sifre WHERE UyeID=@uyeid", Password, Session["UyeID"].ToString());                
            }

        }

        protected void btnUyelikIptal_Click(object sender, EventArgs e)
        {
            divErrorFile.Visible = false; divSuccessProfilGuncelle.Visible = false; divSuccessProfilIptal.Visible = false; divErrorProfilGuncelle.Visible = false; divErrorProfilIptal.Visible = false;

            int KayitSayisi = Veritabani.Sorgu_Calistir("UPDATE gp_Uyeler SET IptalTalebi=1, IptalTalepTarihi=@IptalTalepTarihi WHERE UyeID=@UyeID", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), Session["UyeID"].ToString());
            if (KayitSayisi > 0)
            {
                divSuccessProfilIptal.Visible = true;
            }
            else
            {
                divErrorProfilIptal.Visible = true;
            }
        }

        protected void AvatarResmiYukle(object sender, EventArgs e)
        {
            divErrorFile.Visible = false; divSuccessProfilGuncelle.Visible = false; divSuccessProfilIptal.Visible = false; divErrorProfilGuncelle.Visible = false; divErrorProfilIptal.Visible = false;
            liErrorFile.Visible = false; liErrorFileFormat.Visible = false; liErrorFileSize.Visible = false;
            if (fuAvatar.HasFile)
            {
                try
                {
                    if (fuAvatar.PostedFile.ContentType != "image/jpeg" && fuAvatar.PostedFile.ContentType != "image/gif" && fuAvatar.PostedFile.ContentType != "image/png")
                    {
                        divErrorFile.Visible = true;
                        liErrorFileFormat.Visible = true;
                    }
                    else if (fuAvatar.PostedFile.ContentLength > (1024 * 1024 * 5))
                    {
                        divErrorFile.Visible = true;
                        liErrorFileSize.Visible = true;
                    }
                    else //Dosya geçerli
                    {
                        #region Resim kaydediliyor ve resmin küçültülmüş kopyaları oluşturuluyor
                        string DosyaAdi = Session["UyeID"].ToString() + "_" + DateTime.Now.Ticks.ToString() + Path.GetExtension(fuAvatar.FileName);
                        string YeniDosyaAdi = DosyaAdi; ////string YeniDosyaAdi = "x" + DosyaAdi;
                        ////Orijinal dosya kaydediliyor
                        fuAvatar.SaveAs(Server.MapPath("~/uploads/") + DosyaAdi);

                        //KalikoImage image = new KalikoImage(Server.MapPath("~/uploads/") + DosyaAdi);
                        //image.BlitFill(Server.MapPath("~/img/default_transparan.png"));
                        //image.SaveJpg(Server.MapPath("~/uploads/") + YeniDosyaAdi, 99);

                        //Orijinal resim
                        ResizeSettings resizeCropSettings = new ResizeSettings("format=jpg");
                        string DosyaAdiFinal = ImageBuilder.Current.Build(Server.MapPath("~/uploads/") + YeniDosyaAdi, Server.MapPath("~/images/avatars/original/") + AletKutusu.FormatTemizle(YeniDosyaAdi), resizeCropSettings, false, true);
                        string[] DiziDizin = DosyaAdiFinal.Split('\\');
                        DosyaAdiFinal = DiziDizin[DiziDizin.Length - 1];
                        //120 pixel boyutunda kopya
                        resizeCropSettings = new ResizeSettings("width=120&height=120&format=jpg&crop=auto");
                        ImageBuilder.Current.Build(Server.MapPath("~/uploads/") + YeniDosyaAdi, Server.MapPath("~/images/avatars/120/") + AletKutusu.FormatTemizle(YeniDosyaAdi), resizeCropSettings, false, true);
                        //60 pixel boyutunda kopya
                        resizeCropSettings = new ResizeSettings("width=60&height=60&format=jpg&crop=auto");
                        ImageBuilder.Current.Build(Server.MapPath("~/uploads/") + YeniDosyaAdi, Server.MapPath("~/images/avatars/60/") + AletKutusu.FormatTemizle(YeniDosyaAdi), resizeCropSettings, false, true);
                        #endregion
                        
                        //Üye avatar bilgisi güncelleniyor
                        int KayitSayisi = Veritabani.Sorgu_Calistir("UPDATE gp_Uyeler SET Avatar=@Avatar WHERE UyeID=@UyeID", DosyaAdiFinal, Session["UyeID"].ToString());

                        if (KayitSayisi > 0)
                        {
                            //Avatar başarıyla güncellenmişse avatar resmi görüntüleniyor.
                            imgAvatar.Src = "images/avatars/120/" + DosyaAdiFinal;
                        }
                        else //Avatar güncellenemedi
                        {
                            divErrorFile.Visible = true;
                            liErrorFile.Visible = true;
                        }
                    }
                }
                catch (Exception Hata)
                {
                    string HataMesaji = Hata.ToString();
                    divErrorFile.Visible = true;
                    liErrorFile.Visible = true;
                }
            }
        }

    }
}
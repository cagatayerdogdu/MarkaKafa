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
    public partial class profil_portfolyo_yeni_resim : System.Web.UI.Page
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

        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            divError.Visible = false;
            liError.Visible = false; liErrorFile.Visible = false; liErrorTitle.Visible = false; liErrorFileFormat.Visible = false; liErrorFileSize.Visible = false;
            if (fuResim.HasFile)
            {
                try
                {
                    string PortfolyoBaslik = portfolio_item_title.Value.Trim();
                    string PortfolyoAciklama = portfolio_item_description.Value.Trim();
                    if (PortfolyoBaslik.Length == 0)
                    {
                        divError.Visible = true;
                        liErrorTitle.Visible = true;
                    }
                    if (fuResim.PostedFile.ContentType != "image/jpeg" && fuResim.PostedFile.ContentType != "image/gif" && fuResim.PostedFile.ContentType != "image/png")
                    {
                        divError.Visible = true;
                        liErrorFileFormat.Visible = true;
                    }
                    if (fuResim.PostedFile.ContentLength > (1024 * 1024 * 5))
                    {
                        divError.Visible = true;
                        liErrorFileSize.Visible = true;
                    }

                    if(!divError.Visible) //Hata yoksa dosya yükleniyor
                    {
                        #region Resim kaydediliyor ve resmin küçültülmüş kopyaları oluşturuluyor
                        string DosyaAdi = Session["UyeID"].ToString() + "_" + DateTime.Now.Ticks.ToString() + Path.GetExtension(fuResim.FileName);
                        string YeniDosyaAdi = "x" + DosyaAdi;
                        ////Orijinal dosya kaydediliyor
                        fuResim.SaveAs(Server.MapPath("~/uploads/") + DosyaAdi);

                        KalikoImage image = new KalikoImage(Server.MapPath("~/uploads/") + DosyaAdi);
                        image.BlitFill(Server.MapPath("~/img/watermark.png"));
                        image.SaveJpg(Server.MapPath("~/uploads/") + YeniDosyaAdi, 99);

                        //Orijinal resim
                        ResizeSettings resizeCropSettings = new ResizeSettings("format=jpg");
                        string DosyaAdiFinal = ImageBuilder.Current.Build(Server.MapPath("~/uploads/") + YeniDosyaAdi, Server.MapPath("~/images/portfolio_items/original/") + AletKutusu.FormatTemizle(YeniDosyaAdi), resizeCropSettings, false, true);
                        string[] DiziDizin = DosyaAdiFinal.Split('\\');
                        DosyaAdiFinal = DiziDizin[DiziDizin.Length - 1];
                        //300 pixel boyutunda kopya
                        resizeCropSettings = new ResizeSettings("height=300&format=jpg");
                        ImageBuilder.Current.Build(Server.MapPath("~/uploads/") + YeniDosyaAdi, Server.MapPath("~/images/portfolio_items/300/") + AletKutusu.FormatTemizle(YeniDosyaAdi), resizeCropSettings, false, true);
                        //120 pixel boyutunda kopya
                        resizeCropSettings = new ResizeSettings("height=120&format=jpg");
                        ImageBuilder.Current.Build(Server.MapPath("~/uploads/") + YeniDosyaAdi, Server.MapPath("~/images/portfolio_items/120/") + AletKutusu.FormatTemizle(YeniDosyaAdi), resizeCropSettings, false, true);
                        #endregion

                        //Portfolyo kaydediliyor
                        int KayitSayisi = Veritabani.Sorgu_Calistir("INSERT INTO gp_Portfolyo(UyeID, Aktif, Tarih, PortfolyoTip, Baslik, Aciklama, Resim) VALUES(@UyeID, @Aktif, @Tarih, @PortfolyoTip, @Baslik, @Aciklama, @Resim)", Session["UyeID"].ToString(), "0", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), "1", PortfolyoBaslik, PortfolyoAciklama, DosyaAdiFinal);

                        if (KayitSayisi > 0) //Portfolyo başarıyla kaydedildi
                        {
                            Response.Redirect("profil_portfolyo.aspx"); //Kullanıcı profil sayfasına yönlendiriliyor
                        }
                        else //Portfolyo kaydedilemedi
                        {
                            divError.Visible = true;
                            liError.Visible = true;
                        }
                    }
                }
                catch (Exception Hata)
                {
                    string HataMesaji = Hata.ToString();
                    divError.Visible = true;
                    liError.Visible = true;
                }
            }
            else
            {
                divError.Visible = true;
                liErrorFile.Visible = true;
            }
        }


    }
}
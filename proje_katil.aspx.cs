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
    public partial class proje_katil : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu; Mail Mail;
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
            DataTable TabloProjeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) t.TipAd, t.Metin, t.Video, u.KullaniciAdi, p.OlusturulmaTarihi, p.Zamanlama, p.ProjeAdi, p.Odul, p.ProjeGizlilik, p.ProjeKisisel, p.DetayMarka, p.DetayMusteri, p.DetayRakip, p.DetayOzellik, p.DetayNot, (SELECT COUNT(g.GirdiID) FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=p.ProjeID) AS GirdiSayisi, (SELECT COUNT(g.GirdiID) FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=p.ProjeID AND g.Kazanan=1) AS KazananSayisi FROM gp_Projeler AS p JOIN gp_ProjeTipleri AS t ON p.TipID=t.TipID JOIN gp_Uyeler AS u ON p.UyeID=u.UyeID WHERE p.ProjeID=@ProjeID", ProjeID);
            string ProjeAdi = TabloProjeBilgileri.Rows[0]["ProjeAdi"].ToString();
            string TipAdi = TabloProjeBilgileri.Rows[0]["TipAd"].ToString();
            bool TipVideo = (TabloProjeBilgileri.Rows[0]["Video"].ToString() == "1");
            bool TipMetin = (TabloProjeBilgileri.Rows[0]["Metin"].ToString() == "1");
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

            #endregion

            bool GizlilikAnlasmasiImzali = Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT COUNT(ID) FROM gp_ProjeGizlilikAnlasmalari WHERE ProjeID=@ProjeID AND UyeID=@UyeID", ProjeID, KullaniciID)) > 0;
            #region Anlaşma imzalama butonu görüntüleniyor
            if (ProjeGizli)
            {
                if (!GizlilikAnlasmasiImzali)
                {
                    Response.Redirect("default.aspx?h=pk79");
                }
            }
            #endregion

            btnProjeGaleri.HRef = "proje.aspx?p=" + ProjeAdiSifreli;
            btnProjeDetaylari.HRef = "proje_detay.aspx?p=" + ProjeAdiSifreli;

            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

            divGirdiOneri.Visible = false;
            divGirdiResim.Visible = false;
            divGirdiVideo.Visible = false;
            if (TipVideo)
            {
                divGirdiVideo.Visible = true;
                hfTip.Value = "video";
            }
            else if (TipMetin)
            {
                divGirdiOneri.Visible = true;
                hfTip.Value = "metin";
            }
            else
            {
                divGirdiResim.Visible = true;
                hfTip.Value = "resim";
            }

        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            divErrorResim.Visible = false;
            liErrorResim.Visible = false; liErrorResimFile.Visible = false; liErrorResimTitle.Visible = false; liErrorResimFileFormat.Visible = false; liErrorResimFileSize.Visible = false;
            divErrorVideo.Visible = false;
            liErrorVideo.Visible = false; liErrorVideoFile.Visible = false; liErrorVideoTitle.Visible = false; liErrorVideoFileFormat.Visible = false; liErrorVideoFileSize.Visible = false;
            divErrorMetin.Visible = false;
            liErrorMetin.Visible = false; liErrorMetinTitle.Visible = false;

            string GirdiTipKod = "0";
            string GirdiBaslik = ""; string GirdiBaslikSifreli = ""; string GirdiAciklama = "";
            string DosyaAdi = ""; string DosyaAdiFinal = "";
            int KayitSayisi = 0;
            switch (hfTip.Value)
            {
                case "video":
                    #region Video yükleniyor
                    GirdiTipKod = "2";
                    #endregion
                    break;
                case "resim":
                    #region Resim yükleniyor
                    GirdiTipKod = "1";
                    if (fuDosyaResim.HasFile)
                    {
                        try
                        {
                            GirdiBaslik = txtBaslikResim.Value.Trim();
                            GirdiBaslikSifreli = AletKutusu.KelimeSifrele(GirdiBaslik);
                            if (Veritabani.Sorgu_Scalar("SELECT TOP(1) GirdiID FROM gp_ProjeGirdiler WHERE BaslikSifreli=@BaslikSifreli", GirdiBaslikSifreli) != "")
                                GirdiBaslikSifreli += "-" + DateTime.Now.Ticks.ToString();
                            GirdiAciklama = txtAciklamaResim.Value.Trim();
                            if (GirdiBaslik.Length == 0)
                            {
                                divErrorResim.Visible = true;
                                liErrorResimTitle.Visible = true;
                            }
                            if (fuDosyaResim.PostedFile.ContentType != "image/jpeg" && fuDosyaResim.PostedFile.ContentType != "image/gif" && fuDosyaResim.PostedFile.ContentType != "image/png")
                            {
                                divErrorResim.Visible = true;
                                liErrorResimFileFormat.Visible = true;
                            }
                            if (fuDosyaResim.PostedFile.ContentLength > (1024 * 1024 * 5))
                            {
                                divErrorResim.Visible = true;
                                liErrorResimFileSize.Visible = true;
                            }

                            if (!divErrorResim.Visible) //Hata yoksa dosya yükleniyor
                            {
                                #region Resim kaydediliyor ve resmin küçültülmüş kopyaları oluşturuluyor
                                DosyaAdi = Session["UyeID"].ToString() + "_" + DateTime.Now.Ticks.ToString() + Path.GetExtension(fuDosyaResim.FileName);
                                try
                                {
                                    ////Orijinal dosya kaydediliyor
                                    string YeniDosyaAdi = "x" + DosyaAdi;
                                    fuDosyaResim.SaveAs(Server.MapPath("~/uploads/") + DosyaAdi);
                                    KalikoImage image = new KalikoImage(Server.MapPath("~/uploads/") + DosyaAdi);
                                    image.BlitFill(Server.MapPath("~/img/watermark.png"));
                                    image.SaveJpg(Server.MapPath("~/uploads/") + YeniDosyaAdi, 99);

                                    //Orijinal resim
                                    ResizeSettings resizeCropSettings = new ResizeSettings("format=jpg");
                                    DosyaAdiFinal = ImageBuilder.Current.Build(Server.MapPath("~/uploads/") + YeniDosyaAdi, Server.MapPath("~/images/contest_entries/original/") + AletKutusu.FormatTemizle(YeniDosyaAdi), resizeCropSettings, false, true);
                                    string[] DiziDizin = DosyaAdiFinal.Split('\\');
                                    DosyaAdiFinal = DiziDizin[DiziDizin.Length - 1];
                                    //600 pixel boyutunda kopya
                                    resizeCropSettings = new ResizeSettings("height=600&format=jpg");
                                    ImageBuilder.Current.Build(Server.MapPath("~/uploads/") + YeniDosyaAdi, Server.MapPath("~/images/contest_entries/600/") + AletKutusu.FormatTemizle(YeniDosyaAdi), resizeCropSettings, false, true);
                                    //300 pixel boyutunda kopya
                                    resizeCropSettings = new ResizeSettings("height=300&format=jpg");
                                    ImageBuilder.Current.Build(Server.MapPath("~/uploads/") + YeniDosyaAdi, Server.MapPath("~/images/contest_entries/300/") + AletKutusu.FormatTemizle(YeniDosyaAdi), resizeCropSettings, false, true);
                                    //120 pixel boyutunda kopya
                                    resizeCropSettings = new ResizeSettings("height=120&format=jpg");
                                    ImageBuilder.Current.Build(Server.MapPath("~/uploads/") + YeniDosyaAdi, Server.MapPath("~/images/contest_entries/120/") + AletKutusu.FormatTemizle(YeniDosyaAdi), resizeCropSettings, false, true);
                                }
                                catch (Exception hata)
                                {
                                    string HataString = hata.ToString() + " --- proje_katil.aspx:187";
                                    Veritabani.Sorgu_Calistir("INSERT INTO LogHata(Hatalar) VALUES(@Hata)", HataString);
                                }
                                #endregion

                                //Proje girdisi kaydediliyor
                                KayitSayisi = Veritabani.Sorgu_Calistir("INSERT INTO gp_ProjeGirdiler(ProjeID, UyeID, Aktif, GirdiNo, GirdiTip, Tarih, Baslik, BaslikSifreli, Aciklama, Resim, Kazanan) VALUES(@ProjeID, @UyeID, @Aktif, ISNULL((SELECT MAX(GirdiNo) FROM gp_ProjeGirdiler WHERE ProjeID=@GirdiNoProje),0)+1, @GirdiTip, @Tarih, @Baslik, @BaslikSifreli, @Aciklama, @Resim, 0)", ProjeID, Session["UyeID"].ToString(), "1", ProjeID, GirdiTipKod, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), GirdiBaslik, GirdiBaslikSifreli, GirdiAciklama, DosyaAdiFinal);

                                Mail = new Mail();                                
                                if (KayitSayisi > 0) //Proje girdisi başarıyla kaydedildi
                                {
                                    DataTable TabloUyeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) Eposta FROM gp_Uyeler WHERE UyeID=@UyeID", KullaniciID);
                                    string KullaniciMail = TabloUyeBilgileri.Rows[0]["Eposta"].ToString();
                                    string MailKonu = "Projenize yeni bir tasarım gönderildi.";
                                    string MailIcerik = lblProjeAdi.Text + " isimli projenize yeni bir tasarım eklendi.\n\r<a href=\""+Mail.DomainName+"proje.aspx?p=" + ProjeAdiSifreli+"\"> Tasarımı Görmek İçin Tıklayınız! </a>";
                                    new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                                    new Mail().MailGonder(Server, "", KullaniciMail, MailKonu, MailIcerik);
                                    Response.Redirect("proje.aspx?p=" + ProjeAdiSifreli,false);
                                }
                                else //Proje girdisi kaydedilemedi
                                {
                                    divErrorResim.Visible = true;
                                    liErrorResim.Visible = true;
                                }
                            }
                        }
                        catch (Exception Hata)
                        {
                            string HataMesaji = Hata.ToString();
                            divErrorResim.Visible = true;
                            liErrorResim.Visible = true;

                            Veritabani.Sorgu_Calistir("INSERT INTO LogHata(Hatalar) VALUES(@Hata)", "proje_katil.aspx:201 --- " + HataMesaji);
                        }
                    }
                    else
                    {
                        divErrorResim.Visible = true;
                        liErrorResimFile.Visible = true;
                    }
                    #endregion
                    break;
                case "metin":
                    #region Metin kaydediliyor
                    GirdiTipKod = "0";
                    try
                    {
                        GirdiBaslik = txtBaslikMetin.Value.Trim();
                        GirdiBaslikSifreli = AletKutusu.KelimeSifrele(GirdiBaslik);
                        if (Veritabani.Sorgu_Scalar("SELECT TOP(1) GirdiID FROM gp_ProjeGirdiler WHERE BaslikSifreli=@BaslikSifreli", GirdiBaslikSifreli) != "")
                            GirdiBaslikSifreli += "-" + DateTime.Now.Ticks.ToString();
                        GirdiAciklama = txtAciklamaMetin.Value.Trim();
                        if (GirdiBaslik.Length == 0)
                        {
                            divErrorMetin.Visible = true;
                            liErrorMetinTitle.Visible = true;
                        }

                        if (!divErrorMetin.Visible) //Hata yoksa dosya yükleniyor
                        {
                            //Proje girdisi kaydediliyor
                            KayitSayisi = Veritabani.Sorgu_Calistir("INSERT INTO gp_ProjeGirdiler(ProjeID, UyeID, Aktif, GirdiNo, GirdiTip, Tarih, Baslik, BaslikSifreli, Aciklama, Kazanan) VALUES(@ProjeID, @UyeID, @Aktif, ISNULL((SELECT MAX(GirdiNo) FROM gp_ProjeGirdiler WHERE ProjeID=@GirdiNoProje),1), @GirdiTip, @Tarih, @Baslik, @BaslikSifreli, @Aciklama, 0)", ProjeID, Session["UyeID"].ToString(), "1", ProjeID, GirdiTipKod, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), GirdiBaslik, GirdiBaslikSifreli, GirdiAciklama);

                            if (KayitSayisi > 0) //Proje girdisi başarıyla kaydedildi
                            {
                                Response.Redirect("proje.aspx?p=" + ProjeAdiSifreli);
                            }
                            else //Proje girdisi kaydedilemedi
                            {
                                divErrorMetin.Visible = true;
                                liErrorMetin.Visible = true;
                            }
                        }
                    }
                    catch (Exception Hata)
                    {
                        string HataMesaji = Hata.ToString();
                        divErrorResim.Visible = true;
                        liErrorResim.Visible = true;

                        Veritabani.Sorgu_Calistir("INSERT INTO LogHata(Hatalar) VALUES(@Hata)", "proje_katil.aspx:246 --- " + HataMesaji);
                    }
                    #endregion
                    break;
            }
        }

    }
}
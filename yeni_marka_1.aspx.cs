using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using ImageResizer;

namespace GrafikerPortal
{
    public partial class yeni_marka_1 : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            //Session["geciciuye"] = 999999;  //05.12.2015 Tarihinde normale döndürüldü.
            if (!string.IsNullOrEmpty(Request.QueryString["t"])) 
                Session["ProjeTipID"] = Request.QueryString["t"];
            #region Güvenlik protokolü
            //Üye girişi yapılmamışsa üye giriş sayfasına yönlendiriliyor.
            string KullaniciID = (Session["UyeID"] != null) ? Session["UyeID"].ToString() : "";
            if (KullaniciID == "" && Session["geciciuye"] == null)
            {
                //Session["ProjeTipID"] = Request.QueryString["t"];
                string SessionTest = Session["ProjeTipID"].ToString();
                Response.Redirect("uye_ol.aspx");
            }
            else if (Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyeID=@UyeID", KullaniciID) == "" && Session["geciciuye"] == null) Response.Redirect("uye_ol.aspx");
            string SeciliProjeID = "";

            //Proje düzenleme
            if (!string.IsNullOrEmpty(Request.QueryString["p"]))
            {
                SeciliProjeID = Request.QueryString["p"].ToString();
                //Proje geçerliliği kontrol ediliyor
                if (Veritabani.Sorgu_Scalar("SELECT TOP(1) ProjeID FROM gp_Projeler WHERE ProjeID=@ProjeID AND UyeID=@UyeID", SeciliProjeID, KullaniciID) == "")
                    Response.Redirect("yeni_marka_1.aspx");
                else
                {
                    Session["ProjeTipID"] = Veritabani.Sorgu_Scalar("SELECT TOP(1) TipID FROM gp_Projeler WHERE ProjeID=@ProjeID", SeciliProjeID);
                    Session["ProjeID"] = SeciliProjeID;
                    hfIslem.Value = "p";
                    //pProjeTipDegistir.Visible = false;
                }
            }
            //Seçilen proje tipi session'da saklanıyor
            else if (!string.IsNullOrEmpty(Request.QueryString["t"]))
            {
                string SeciliProjeTipi = Request.QueryString["t"].ToString();
                //Proje tipi geçerliliği kontrol ediliyor
                if (Veritabani.Sorgu_Scalar("SELECT TOP(1) TipID FROM gp_ProjeTipleri WHERE TipID=@TipID", SeciliProjeTipi) == "")
                    Response.Redirect("yeni_marka_1.aspx");
                else
                {
                    Session["ProjeTipID"] = SeciliProjeTipi;
                    hfIslem.Value = "t";
                }
            }
            else Response.Redirect("yeni_marka.aspx");
            #endregion
            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

            //Seçilen proje tipi bilgileri tespit ediliyor
            DataTable TabloProjeTipBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) TipAd, AciklamaUzun, SecimGarantili FROM gp_ProjeTipleri WHERE TipID=@TipID", Session["ProjeTipID"].ToString());
            lblProjeAdi.Text = TabloProjeTipBilgileri.Rows[0]["TipAd"].ToString();
            lblProjeAciklama.Text = TabloProjeTipBilgileri.Rows[0]["AciklamaUzun"].ToString();
            //if (TabloProjeTipBilgileri.Rows[0]["SecimGarantili"].ToString() == "1") divOdemeGarantisi.Visible = false;

            //Adım sekmeleri hazırlanıyor
            //lblAdimlar.Text = AletKutusu.AdimSekmeleri(1, SeciliProjeID);

            //Projeler kategorilerine göre gruplanarak listeleniyor
            //lblProjeListeKolon1.Text = AletKutusu.ProjeFiyatListesi("1");
            //lblProjeListeKolon2.Text = AletKutusu.ProjeFiyatListesi("2");
            //lblProjeListeKolon3.Text = AletKutusu.ProjeFiyatListesi("3");

            //Proje düzenleme modu ise form dolduruluyor
            string SeciliSektor = "";
            if (SeciliProjeID != "")
            {
                DataTable TabloProjeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) SektorID, SirketAdi, ProjeAdi, Zamanlama, DetayMarka, DetayMusteri, DetayRakip, DetayOzellik, DetayNot FROM gp_Projeler WHERE ProjeID=@ProjeID", SeciliProjeID);
                SeciliSektor = TabloProjeBilgileri.Rows[0]["SektorID"].ToString();
                project_brand_name.Value = TabloProjeBilgileri.Rows[0]["SirketAdi"].ToString();
                project_title.Value = TabloProjeBilgileri.Rows[0]["ProjeAdi"].ToString();
                string Zamanlama = TabloProjeBilgileri.Rows[0]["Zamanlama"].ToString();
                for (int i = 0; i < project_duration.Items.Count; i++)
                {
                    if (project_duration.Items[i].Value == Zamanlama)
                        project_duration.SelectedIndex = i;
                }
                project_brand_info.Value = TabloProjeBilgileri.Rows[0]["DetayMarka"].ToString();
                //project_customer_info.Value = TabloProjeBilgileri.Rows[0]["DetayMusteri"].ToString();
                //project_competitor_info.Value = TabloProjeBilgileri.Rows[0]["DetayRakip"].ToString();
                //project_competencies.Value = TabloProjeBilgileri.Rows[0]["DetayOzellik"].ToString();
                //project_notes.Value = TabloProjeBilgileri.Rows[0]["DetayNot"].ToString();
            }

            //Sektör seçiliyor
            for (int i = 0; i < project_sector.Items.Count; i++)
            {
                if (SeciliSektor == project_sector.Items[i].Value)
                    project_sector.SelectedIndex = i;
            }
            

        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            string KullaniciID = (Session["UyeID"] != null) ? Session["UyeID"].ToString() : "";
            string SirketIsmi = project_brand_name.Value.Trim();
            string ProjeIsmi = project_title.Value.Trim();
            string ProjeIsmiSifreli = AletKutusu.KelimeSifrele(ProjeIsmi);
            if (Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT COUNT(ProjeID) FROM gp_Projeler WHERE ProjeAdiSifreli=@ProjeAdiSifreli", ProjeIsmiSifreli)) > 0)
                ProjeIsmiSifreli += "-" + DateTime.Now.Ticks.ToString();
            string Zamanlama = project_duration.Items[project_duration.SelectedIndex].Value;
            string MarkaBilgisi = project_brand_info.Value.Trim();
            string Sektor = project_sector.Items[project_sector.SelectedIndex].Value;
            //string MusteriBilgisi = project_customer_info.Value.Trim();
            //string RakipBilgisi = project_competitor_info.Value.Trim();
            //string MarkaOzelligi = project_competencies.Value.Trim();
            //string ProjeNot = project_notes.Value.Trim();
            string MusteriBilgisi = "";
            string RakipBilgisi = "";
            string MarkaOzelligi = "";
            string ProjeNot = "";
            divErrorProje.Visible = true;
            if (ProjeIsmi.Length == 0)
            {
                liProjeAdi.Visible = true;
            }
            else if (fuDosya1.HasFile && (fuDosya1.PostedFile.ContentType != "image/jpeg" && fuDosya1.PostedFile.ContentType != "image/gif" && fuDosya1.PostedFile.ContentType != "image/png"))
            {
                liErrorFileFormat.Visible = true;
            }
            else if (fuDosya1.HasFile && (fuDosya1.PostedFile.ContentLength > (1024 * 1024 * 5)))
            {
                liErrorFileSize.Visible = true;
            }
            else
            {
                divErrorProje.Visible = false;

                #region Resim kaydediliyor ve resmin küçültülmüş kopyaları oluşturuluyor
                string Dosya1Adi = "";
                if (fuDosya1.HasFile)
                 {
                     Dosya1Adi = KullaniciID + "_" + DateTime.Now.Ticks.ToString() + Path.GetExtension(fuDosya1.FileName);
                    ////Orijinal dosya kaydediliyor
                    fuDosya1.SaveAs(Server.MapPath("~/uploads/") + Dosya1Adi);


                    ////SADECE RESİM YÜKLENSEYDİ ŞUNLARI YAPACAKTIK: 01.02.2015
                    //ResizeSettings resizeCropSettings = new ResizeSettings("format=jpg");
                    //string DosyaAdiFinal = ImageBuilder.Current.Build(Server.MapPath("~/uploads/") + Dosya1Adi, Server.MapPath("~/images/contents/original/") + AletKutusu.FormatTemizle(Dosya1Adi), resizeCropSettings, false, true);
                    //string[] DiziDizin = DosyaAdiFinal.Split('\\');
                    //DosyaAdiFinal = DiziDizin[DiziDizin.Length - 1];
                    ////300 pixel boyutunda kopya
                    //resizeCropSettings = new ResizeSettings("height=300&format=jpg");
                    //ImageBuilder.Current.Build(Server.MapPath("~/uploads/") + Dosya1Adi, Server.MapPath("~/images/contents/300/") + AletKutusu.FormatTemizle(Dosya1Adi), resizeCropSettings, false, true);
                }
                #endregion

                if (hfIslem.Value == "p") //Proje düzenleme
                {
                    int EtkilenenSatirSayisi = Veritabani.Sorgu_Calistir("UPDATE gp_Projeler SET SektorID=@SektorID, SirketAdi=@SirketAdi, ProjeAdi=@ProjeAdi, ProjeAdiSifreli=@ProjeAdiSifreli, Zamanlama=@Zamanlama, DetayMarka=@DetayMarka, DetayMusteri=@DetayMusteri, DetayRakip=@DetayRakip, DetayOzellik=@DetayOzellik, DetayNot=@DetayNot WHERE ProjeID=@ProjeID", Sektor, SirketIsmi, ProjeIsmi, ProjeIsmiSifreli, Zamanlama, MarkaBilgisi, MusteriBilgisi, RakipBilgisi, MarkaOzelligi, ProjeNot, Session["ProjeID"].ToString());
                    if (EtkilenenSatirSayisi == 0)
                    {
                        divErrorProje.Visible = true;
                        liError.Visible = true;
                    }
                    else //Kayıt başarılı
                    {
                        //Yüklenen dosyalar projeye bağlanıyor
                        if (Dosya1Adi != "")
                        {
                            string Dosya1Baslik = txtDosya1Baslik.Value;
                            Veritabani.Sorgu_Calistir("INSERT INTO gp_ProjeDosyalar(ProjeID, Baslik, DosyaAdi) VALUES(@ProjeID, @Baslik, @DosyaAdi)", Session["ProjeID"].ToString(), Dosya1Baslik, Dosya1Adi);
                        }
                        Response.Redirect("yeni_proje_2.aspx");
                    }
                }
                else //Yeni proje kayıt
                {
                    //Proje veritabanına kaydediliyor
                    //Durum: 1->Yeni, 2->Sürüyor, 3->Tamamlandı
                    //string Tarih = "CONVERT(datetime, '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 120)";
                    string Tarih = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                    int EklenenKayit = Veritabani.Sorgu_Calistir_Eklenen_Id_Dondur("INSERT INTO gp_Projeler(TipID, UyeID, SektorID, OlusturulmaTarihi, Durum, Asama, SirketAdi, ProjeAdi, ProjeAdiSifreli, Zamanlama, DetayMarka, DetayMusteri, DetayRakip, DetayOzellik, DetayNot, Dosya1) VALUES(@TipID, @UyeID, @SektorID, @OlusturulmaTarihi, @Durum, @Asama, @SirketAdi, @ProjeAdi, @ProjeAdiSifreli, @Zamanlama, @DetayMarka, @DetayMusteri, @DetayRakip, @DetayOzellik, @DetayNot, @Dosya1)", Session["ProjeTipID"].ToString(), KullaniciID, Sektor, Tarih, "1", "1", SirketIsmi, ProjeIsmi, ProjeIsmiSifreli, Zamanlama, MarkaBilgisi, MusteriBilgisi, RakipBilgisi, MarkaOzelligi, ProjeNot, Dosya1Adi);
                    if (EklenenKayit == 0)
                    {
                        divErrorProje.Visible = true;
                        liError.Visible = true;
                    }
                    else //Kayıt başarılı
                    {
                        //Yüklenen dosyalar projeye bağlanıyor
                        if (Dosya1Adi != "")
                        {
                            string Dosya1Baslik = txtDosya1Baslik.Value;
                            Veritabani.Sorgu_Calistir("INSERT INTO gp_ProjeDosyalar(ProjeID, Baslik, DosyaAdi) VALUES(@ProjeID, @Baslik, @DosyaAdi)", EklenenKayit.ToString(), Dosya1Baslik, Dosya1Adi);
                        }

                        Session["ProjeID"] = EklenenKayit.ToString();
                        Response.Redirect("yeni_proje_2.aspx");
                    }
                }
            }
        }
    }
}
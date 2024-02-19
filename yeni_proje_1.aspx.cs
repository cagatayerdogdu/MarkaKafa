//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Data;

//namespace GrafikerPortal
//{
//    public partial class yeni_proje_1 : System.Web.UI.Page
//    {
//        DAL Veritabani; Fonksiyonlar AletKutusu;
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

//            #region Güvenlik protokolü
//            //Üye girişi yapılmamışsa üye giriş sayfasına yönlendiriliyor.
//            string KullaniciID = (Session["UyeID"] != null) ? Session["UyeID"].ToString() : "";
//            if (KullaniciID == "") Response.Redirect("uye_ol.aspx");
//            else if (Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyeID=@UyeID", KullaniciID) == "") Response.Redirect("uye_ol.aspx");
//            string SeciliProjeID = "";

//            //Proje düzenleme
//            if (!string.IsNullOrEmpty(Request.QueryString["p"]))
//            {
//                SeciliProjeID = Request.QueryString["p"].ToString();
//                //Proje geçerliliği kontrol ediliyor
//                if (Veritabani.Sorgu_Scalar("SELECT TOP(1) ProjeID FROM gp_Projeler WHERE ProjeID=@ProjeID AND UyeID=@UyeID", SeciliProjeID, KullaniciID) == "")
//                    Response.Redirect("yeni_proje.aspx");
//                else
//                {
//                    Session["ProjeTipID"] = Veritabani.Sorgu_Scalar("SELECT TOP(1) TipID FROM gp_Projeler WHERE ProjeID=@ProjeID", SeciliProjeID);
//                    Session["ProjeID"] = SeciliProjeID;
//                    hfIslem.Value = "p";
//                    pProjeTipDegistir.Visible = false;
//                }
//            }
//            //Seçilen proje tipi session'da saklanıyor
//            else if (!string.IsNullOrEmpty(Request.QueryString["t"]))
//            {
//                string SeciliProjeTipi = Request.QueryString["t"].ToString();
//                //Proje tipi geçerliliği kontrol ediliyor
//                if (Veritabani.Sorgu_Scalar("SELECT TOP(1) TipID FROM gp_ProjeTipleri WHERE TipID=@TipID", SeciliProjeTipi) == "")
//                    Response.Redirect("yeni_proje.aspx");
//                else
//                {
//                    Session["ProjeTipID"] = SeciliProjeTipi;
//                    hfIslem.Value = "t";
//                }
//            }
//            else Response.Redirect("yeni_proje.aspx");
//            #endregion
//            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

//            //Seçilen proje tipi bilgileri tespit ediliyor
//            DataTable TabloProjeTipBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) TipAd, AciklamaUzun, SecimGarantili FROM gp_ProjeTipleri WHERE TipID=@TipID", Session["ProjeTipID"].ToString());
//            lblProjeAdi.Text = TabloProjeTipBilgileri.Rows[0]["TipAd"].ToString();
//            lblProjeAciklama.Text = TabloProjeTipBilgileri.Rows[0]["AciklamaUzun"].ToString();
//            if (TabloProjeTipBilgileri.Rows[0]["SecimGarantili"].ToString() == "1") divOdemeGarantisi.Visible = false;

//            //Adım sekmeleri hazırlanıyor
//            lblAdimlar.Text = AletKutusu.AdimSekmeleri(1, SeciliProjeID);

//            //Projeler kategorilerine göre gruplanarak listeleniyor
//            lblProjeListeKolon1.Text = AletKutusu.ProjeFiyatListesi("1");
//            lblProjeListeKolon2.Text = AletKutusu.ProjeFiyatListesi("2");
//            lblProjeListeKolon3.Text = AletKutusu.ProjeFiyatListesi("3");

//            //Proje düzenleme modu ise form dolduruluyor
//            if (SeciliProjeID != "")
//            {
//                DataTable TabloProjeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) SirketAdi, ProjeAdi, Zamanlama, DetayMarka, DetayMusteri, DetayRakip, DetayOzellik, DetayNot FROM gp_Projeler WHERE ProjeID=@ProjeID", SeciliProjeID);
//                project_brand_name.Value = TabloProjeBilgileri.Rows[0]["SirketAdi"].ToString();
//                project_title.Value = TabloProjeBilgileri.Rows[0]["ProjeAdi"].ToString();
//                string Zamanlama = TabloProjeBilgileri.Rows[0]["Zamanlama"].ToString();
//                for (int i = 0; i < project_duration.Items.Count; i++)
//                {
//                    if (project_duration.Items[i].Value == Zamanlama) 
//                        project_duration.SelectedIndex = i;
//                }
//                project_brand_info.Value = TabloProjeBilgileri.Rows[0]["DetayMarka"].ToString();
//                project_customer_info.Value = TabloProjeBilgileri.Rows[0]["DetayMusteri"].ToString();
//                project_competitor_info.Value = TabloProjeBilgileri.Rows[0]["DetayRakip"].ToString();
//                project_competencies.Value = TabloProjeBilgileri.Rows[0]["DetayOzellik"].ToString();
//                project_notes.Value = TabloProjeBilgileri.Rows[0]["DetayNot"].ToString();
//            }

//        }

//        protected void btnKaydet_Click(object sender, EventArgs e)
//        {
//            string SirketIsmi = project_brand_name.Value.Trim();
//            string ProjeIsmi = project_title.Value.Trim();
//            string ProjeIsmiSifreli = AletKutusu.KelimeSifrele(ProjeIsmi);
//            if (Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT COUNT(ProjeID) FROM gp_Projeler WHERE ProjeAdiSifreli=@ProjeAdiSifreli", ProjeIsmiSifreli)) > 0)
//                ProjeIsmiSifreli += "-" + DateTime.Now.Ticks.ToString();
//            string Zamanlama = project_duration.Items[project_duration.SelectedIndex].Value;
//            string MarkaBilgisi = project_brand_info.Value.Trim();
//            string MusteriBilgisi = project_customer_info.Value.Trim();
//            string RakipBilgisi = project_competitor_info.Value.Trim();
//            string MarkaOzelligi = project_competencies.Value.Trim();
//            string ProjeNot = project_notes.Value.Trim();
//            divErrorProje.Visible = true;
//            if (ProjeIsmi.Length == 0)
//            {
//                liProjeAdi.Visible = true;
//            }
//            else
//            {
//                divErrorProje.Visible = false;
//                if (hfIslem.Value == "p") //Proje düzenleme
//                {
//                    int EtkilenenSatirSayisi = Veritabani.Sorgu_Calistir("UPDATE gp_Projeler SET SirketAdi=@SirketAdi, ProjeAdi=@ProjeAdi, ProjeAdiSifreli=@ProjeAdiSifreli, Zamanlama=@Zamanlama, DetayMarka=@DetayMarka, DetayMusteri=@DetayMusteri, DetayRakip=@DetayRakip, DetayOzellik=@DetayOzellik, DetayNot=@DetayNot WHERE ProjeID=@ProjeID", SirketIsmi, ProjeIsmi, ProjeIsmiSifreli, Zamanlama, MarkaBilgisi, MusteriBilgisi, RakipBilgisi, MarkaOzelligi, ProjeNot, Session["ProjeID"].ToString());
//                    if (EtkilenenSatirSayisi == 0)
//                    {
//                        divErrorProje.Visible = true;
//                        liError.Visible = true;
//                    }
//                    else //Kayıt başarılı
//                    {
//                        Response.Redirect("yeni_proje_2.aspx");
//                    }
//                }
//                else //Yeni proje kayıt
//                {
//                    //Proje veritabanına kaydediliyor
//                    //Durum: 1->Yeni, 2->Sürüyor, 3->Tamamlandı
//                    //string Tarih = "CONVERT(datetime, '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 120)";
//                    string Tarih = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
//                    int EklenenKayit = Veritabani.Sorgu_Calistir_Eklenen_Id_Dondur("INSERT INTO gp_Projeler(TipID, UyeID, OlusturulmaTarihi, Durum, Asama, SirketAdi, ProjeAdi, ProjeAdiSifreli, Zamanlama, DetayMarka, DetayMusteri, DetayRakip, DetayOzellik, DetayNot) VALUES(@TipID, @UyeID, @OlusturulmaTarihi, @Durum, @Asama, @SirketAdi, @ProjeAdi, @ProjeAdiSifreli, @Zamanlama, @DetayMarka, @DetayMusteri, @DetayRakip, @DetayOzellik, @DetayNot)", Session["ProjeTipID"].ToString(), Session["UyeID"].ToString(), Tarih , "1", "1", SirketIsmi, ProjeIsmi, ProjeIsmiSifreli, Zamanlama, MarkaBilgisi, MusteriBilgisi, RakipBilgisi, MarkaOzelligi, ProjeNot);
//                    if (EklenenKayit == 0)
//                    {
//                        divErrorProje.Visible = true;
//                        liError.Visible = true;
//                    }
//                    else //Kayıt başarılı
//                    {
//                        Session["ProjeID"] = EklenenKayit.ToString();
//                        Response.Redirect("yeni_proje_2.aspx");
//                    }
//                }
//            }
//        }

//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class faturaduzenle : System.Web.UI.Page
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

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                hfFaturaID.Value = Request.QueryString["id"].ToString();

            #region Üye bilgileri tespit ediliyor
            DataTable TabloUyeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) UyelikTipi, IptalTalebi, Avatar, KullaniciAdi, KullaniciAdiSifreli,Tarih, Eposta, Meslek, NeredenDuydunuz, Telefon, WebSitesi, Blog, Hakkinda, UzmanlikGrafikTasarim, UzmanlikDijitalTasarim, UzmanlikEndustriyelTasarim, UzmanlikReklamYazarligi, UzmanlikIllustrasyon, HatirlatmaPeriyodu, HatirlatProjeler, HatirlatGelismeler FROM gp_Uyeler WHERE UyeID=@UyeID", Session["UyeID"].ToString());
            string UyeTip = TabloUyeBilgileri.Rows[0]["UyelikTipi"].ToString();
            bool UyeIptalTalebi = (TabloUyeBilgileri.Rows[0]["IptalTalebi"].ToString() == "1");
            string UyeAvatar = TabloUyeBilgileri.Rows[0]["Avatar"].ToString();
            string UyeNeredenDuydunuz = TabloUyeBilgileri.Rows[0]["NeredenDuydunuz"].ToString();
            string UyeHatirlatmaPeriyodu = TabloUyeBilgileri.Rows[0]["HatirlatmaPeriyodu"].ToString();
            DateTime TasarimciKayitTarih = Convert.ToDateTime(TabloUyeBilgileri.Rows[0]["Tarih"].ToString());
            string KullaniciAdi = TabloUyeBilgileri.Rows[0]["KullaniciAdiSifreli"].ToString();
            string K_Adi = TabloUyeBilgileri.Rows[0]["KullaniciAdi"].ToString();
            #endregion

            #region Fatura bilgileri listeleniyor
            if (hfFaturaID.Value != "")
            {
                DataTable TabloFatura = Veritabani.Sorgu_DataTable("SELECT * FROM gp_FaturaBilgileri WHERE FaturaID=@FaturaID", hfFaturaID.Value);
                for (int i = 0; i < TabloFatura.Rows.Count; i++)
                {
                    projects_invoice_detail_attributes_title.Value = TabloFatura.Rows[i]["FaturaBaslik"].ToString();
                    projects_invoice_detail_attributes_company_name.Value = TabloFatura.Rows[i]["FaturaAdSoyad"].ToString();
                    projects_invoice_detail_attributes_tax_office.Value = TabloFatura.Rows[i]["FaturaVergiDairesi"].ToString();
                    projects_invoice_detail_attributes_tax_number.Value = TabloFatura.Rows[i]["FaturaVergiNo"].ToString();
                    projects_invoice_detail_attributes_address_line_1.Value = TabloFatura.Rows[i]["FaturaAdres"].ToString();
                    projects_invoice_detail_attributes_city.Value = TabloFatura.Rows[i]["FaturaSehir"].ToString();
                    projects_invoice_detail_attributes_postal_code.Value = TabloFatura.Rows[i]["FaturaPostaKodu"].ToString();
                    projects_invoice_detail_attributes_phone.Value = TabloFatura.Rows[i]["FaturaTelefon"].ToString();
                }
            }
            #endregion
        }
        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            string FaturaBaslik = projects_invoice_detail_attributes_title.Value.Trim();
            string FaturaAdSoyad = projects_invoice_detail_attributes_company_name.Value.Trim();
            string FaturaVergiDairesi = projects_invoice_detail_attributes_tax_office.Value.Trim();
            string FaturaVergiNo = projects_invoice_detail_attributes_tax_number.Value.Trim();
            string FaturaAdres = projects_invoice_detail_attributes_address_line_1.Value.Trim();
            string FaturaSehir = projects_invoice_detail_attributes_city.Value.Trim();
            string FaturaPostaKodu = projects_invoice_detail_attributes_postal_code.Value.Trim();
            string FaturaTelefon = projects_invoice_detail_attributes_phone.Value.Trim();

            divErrorProje.Visible = false;
            if (FaturaAdSoyad.Length == 0)
            {
                divErrorProje.Visible = true;
                liFaturaAdSoyad.Visible = true;
            }
            if (FaturaVergiNo.Length == 0)
            {
                divErrorProje.Visible = true;
                liFaturaVergiNumarasi.Visible = true;
            }
            if (FaturaSehir.Length == 0)
            {
                divErrorProje.Visible = true;
                liFaturaSehir.Visible = true;
            }
            if (FaturaAdres.Length == 0)
            {
                divErrorProje.Visible = true;
                liFaturaAdres.Visible = true;
            }
            if (FaturaTelefon.Length == 0)
            {
                divErrorProje.Visible = true;
                liFaturaTelefon.Visible = true;
            }

            //Hata mesajı verilmemişse kayıt gerçekleştiriliyor
            if (!divErrorProje.Visible)
            {
                if (hfFaturaID.Value != "")
                {
                    #region Güncelleme işlemi
                    int EtkilenenSatirSayisi = Veritabani.Sorgu_Calistir("UPDATE gp_FaturaBilgileri SET FaturaBaslik=@FaturaBaslik, FaturaAdSoyad=@FaturaAdSoyad, FaturaVergiDairesi=@FaturaVergiDairesi, FaturaVergiNo=@FaturaVergiNo, FaturaAdres=@FaturaAdres, FaturaSehir=@FaturaSehir, FaturaPostaKodu=@FaturaPostaKodu, FaturaTelefon=@FaturaTelefon WHERE FaturaID=@FaturaID", FaturaBaslik, FaturaAdSoyad, FaturaVergiDairesi, FaturaVergiNo, FaturaAdres, FaturaSehir, FaturaPostaKodu, FaturaTelefon, hfFaturaID.Value);
                    if (EtkilenenSatirSayisi == 0)
                    {
                        divErrorProje.Visible = true;
                        liError.Visible = true;
                    }
                    else //Kayıt başarılı
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "Alert", "kapat();", true);
                    }
                    #endregion
                }
                else
                {
                    #region Yeni kayıt işlemi
                    int EtkilenenSatirSayisi = Veritabani.Sorgu_Calistir("INSERT INTO gp_FaturaBilgileri(UyeID, FaturaBaslik, FaturaAdSoyad, FaturaVergiDairesi, FaturaVergiNo, FaturaAdres, FaturaSehir, FaturaPostaKodu, FaturaTelefon)  VALUES(@UyeID, @FaturaBaslik, @FaturaAdSoyad, @FaturaVergiDairesi, @FaturaVergiNo, @FaturaAdres, @FaturaSehir, @FaturaPostaKodu, @FaturaTelefon)", Session["UyeID"].ToString(), FaturaBaslik, FaturaAdSoyad, FaturaVergiDairesi, FaturaVergiNo, FaturaAdres, FaturaSehir, FaturaPostaKodu, FaturaTelefon);
                    if (EtkilenenSatirSayisi == 0)
                    {
                        divErrorProje.Visible = true;
                        liError.Visible = true;
                    }
                    else //Kayıt başarılı
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "Alert", "kapat();", true);
                    }
                    #endregion
                }

            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class yeni_proje_2 : System.Web.UI.Page
    {
        int FiyatProjeGizli = 229;
        int FiyatProjeKapali = 169;
        int FiyatProjeAramaMotoruYok = 69;
        int FiyatProjeListeBasi = 69;
        int FiyatProjeArkaplan = 39;
        int FiyatProjeBold = 19;
        int OdulUstLimit = 5000;

        DAL Veritabani; Fonksiyonlar AletKutusu;
        int IntDenemeTahtasi;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            #region Güvenlik protokolü
            //Üye girişi yapılmamışsa üye giriş sayfasına yönlendiriliyor.
            string KullaniciID = (Session["UyeID"] != null) ? Session["UyeID"].ToString() : "";
            if (KullaniciID == "" && Session["geciciuye"] == null) Response.Redirect("uye_ol.aspx");
            else if (Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyeID=@UyeID", KullaniciID) == "" && Session["geciciuye"] == null) Response.Redirect("uye_ol.aspx");
            //Proje düzenleme sayfasından gelinmişse proje ID'si session'a atanıyor.
            if (!string.IsNullOrEmpty(Request.QueryString["p"])) Session["ProjeID"] = Request.QueryString["p"].ToString();
            string SeciliProjeID = (Session["ProjeID"] != null) ? Session["ProjeID"].ToString() : "";
            //Proje geçerliliği kontrol ediliyor
            if (Veritabani.Sorgu_Scalar("SELECT TOP(1) ProjeID FROM gp_Projeler WHERE ProjeID=@ProjeID AND UyeID=@UyeID", SeciliProjeID, KullaniciID) == "")
            {
                Response.Redirect("yeni_marka.aspx");
            }
            #endregion
            #region Bazı komponenetler sıfırlanıyor
            divErrorProje.Visible = false;
            liError.Visible = false;
            liFaturaAdres.Visible = false;
            liFaturaAdSoyad.Visible = false;
            liFaturaSehir.Visible = false;
            liFaturaTelefon.Visible = false;
            liFaturaVergiNumarasi.Visible = false;
            #endregion
            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

            //Proje bilgileri tespit ediliyor.
            DataTable TabloProjeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) TipID, KuponKodu, Odul, HizmetBedeli, Ekstra, ToplamMaliyet, ProjeGizlilik, ProjeKisisel, ProjeAramaMotoruYok, ProjeListeBasi, ProjeArkaplan, ProjeBold, Zamanlama, FaturaBilgileri, FaturaAdSoyad, FaturaVergiDairesi, FaturaVergiNo, FaturaAdres, FaturaSehir, FaturaPostaKodu, FaturaTelefon FROM gp_Projeler WHERE ProjeID=@ProjeID", SeciliProjeID);
            Session["ProjeTipID"] = TabloProjeBilgileri.Rows[0]["TipID"].ToString();
            coupon_no_area.Value = TabloProjeBilgileri.Rows[0]["KuponKodu"].ToString();
            projects_campaign_prize.Value = TabloProjeBilgileri.Rows[0]["Odul"].ToString();
            spanResultPrize.InnerText = TabloProjeBilgileri.Rows[0]["Odul"].ToString();
            spanResultServiceCost.InnerText = TabloProjeBilgileri.Rows[0]["HizmetBedeli"].ToString();
            spanResultExtra.InnerText = TabloProjeBilgileri.Rows[0]["Ekstra"].ToString();
            spanResultTotal.InnerText = TabloProjeBilgileri.Rows[0]["ToplamMaliyet"].ToString();
            projects_hidden_contest.Checked = (TabloProjeBilgileri.Rows[0]["ProjeGizlilik"].ToString() == "1");
            projects_private.Checked = (TabloProjeBilgileri.Rows[0]["ProjeKisisel"].ToString() == "1");
            projects_hide_on_search.Checked = (TabloProjeBilgileri.Rows[0]["ProjeAramaMotoruYok"].ToString() == "1");
            projects_publish_on_top.Checked = (TabloProjeBilgileri.Rows[0]["ProjeListeBasi"].ToString() == "1");
            projects_publish_highlighted.Checked = (TabloProjeBilgileri.Rows[0]["ProjeArkaplan"].ToString() == "1");
            projects_publish_bold.Checked = (TabloProjeBilgileri.Rows[0]["ProjeBold"].ToString() == "1");
            projects_invoice_detail_attributes_remind_me_later.Checked = (TabloProjeBilgileri.Rows[0]["FaturaBilgileri"].ToString() == "1");
            int EkGun = int.Parse(TabloProjeBilgileri.Rows[0]["Zamanlama"].ToString()) - 7;
            if(EkGun > 0){
                hfProjectTime.Value = EkGun.ToString();
                int Odul = int.Parse(spanResultPrize.InnerText);
                spanEkSureBedeli.InnerText = ((Odul * EkGun) / 100).ToString();
            }
            if (projects_invoice_detail_attributes_remind_me_later.Checked)
            {
                projects_invoice_detail_attributes_company_name.Disabled = true;
                projects_invoice_detail_attributes_tax_office.Disabled = true;
                projects_invoice_detail_attributes_tax_number.Disabled = true;
                projects_invoice_detail_attributes_address_line_1.Disabled = true;
                projects_invoice_detail_attributes_city.Disabled = true;
                projects_invoice_detail_attributes_postal_code.Disabled = true;
                projects_invoice_detail_attributes_phone.Disabled = true;
            }
            else
            {
                projects_invoice_detail_attributes_company_name.Value = TabloProjeBilgileri.Rows[0]["FaturaAdSoyad"].ToString();
                projects_invoice_detail_attributes_tax_office.Value = TabloProjeBilgileri.Rows[0]["FaturaVergiDairesi"].ToString();
                projects_invoice_detail_attributes_tax_number.Value = TabloProjeBilgileri.Rows[0]["FaturaVergiNo"].ToString();
                projects_invoice_detail_attributes_address_line_1.Value = TabloProjeBilgileri.Rows[0]["FaturaAdres"].ToString();
                projects_invoice_detail_attributes_city.Value = TabloProjeBilgileri.Rows[0]["FaturaSehir"].ToString();
                projects_invoice_detail_attributes_postal_code.Value = TabloProjeBilgileri.Rows[0]["FaturaPostaKodu"].ToString();
                projects_invoice_detail_attributes_phone.Value = TabloProjeBilgileri.Rows[0]["FaturaTelefon"].ToString();
            }

            //Seçilen proje tipi bilgileri tespit ediliyor
            DataTable TabloProjeTipBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) TipAd, Fiyat, SecimGarantili FROM gp_ProjeTipleri WHERE TipID=@TipID", Session["ProjeTipID"].ToString());
            string ProjeTipAd = TabloProjeTipBilgileri.Rows[0]["TipAd"].ToString();
            string OdulAltLimit = TabloProjeTipBilgileri.Rows[0]["Fiyat"].ToString();
            //if (TabloProjeTipBilgileri.Rows[0]["SecimGarantili"].ToString() == "1") divOdemeGarantisi.Visible = false;
            lblFiyatBilgisi.Text = ProjeTipAd + " türündeki projeler için ödül bedeli " + OdulAltLimit + " TL ile " + OdulUstLimit.ToString() + " TL arasında olmalıdır";

            //Adım sekmeleri hazırlanıyor
            lblAdimlar.Text = AletKutusu.AdimSekmeleri(2, SeciliProjeID);

            //Fiyat bilgileri yazdırılıyor
            hfPrizeMin.Value = OdulAltLimit;
            hfPrizeMax.Value = OdulUstLimit.ToString();
            hfCostProjectHidden.Value = FiyatProjeGizli.ToString();
            lblCostProjectHidden.Text = FiyatProjeGizli.ToString();
            hfCostProjectPrivate.Value = FiyatProjeKapali.ToString();
            lblCostProjectPrivate.Text = FiyatProjeKapali.ToString();
            hfCostProjectHideSearch.Value = FiyatProjeAramaMotoruYok.ToString();
            lblCostProjectHideSearch.Text = FiyatProjeAramaMotoruYok.ToString();
            hfCostProjectTop.Value = FiyatProjeListeBasi.ToString();
            lblCostProjectTop.Text = FiyatProjeListeBasi.ToString();
            hfCostProjectHighlight.Value = FiyatProjeArkaplan.ToString();
            lblCostProjectHighlight.Text = FiyatProjeArkaplan.ToString();
            hfCostProjectBold.Value = FiyatProjeBold.ToString();
            lblCostProjectBold.Text = FiyatProjeBold.ToString();

        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            //Seçilen proje tipi bilgileri tespit ediliyor
            int ProjeTipOdul = Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT TOP(1) Fiyat FROM gp_ProjeTipleri WHERE TipID=@TipID", Session["ProjeTipID"].ToString()));

            string Odul = projects_campaign_prize.Value.Trim();
            string spanEkSureBedeli2 = spanEkSureBedeli.InnerText;
            int OdulMiktari = (int.TryParse(Odul, out IntDenemeTahtasi)) ? IntDenemeTahtasi : 0;
            string KuponKodu = coupon_no_area.Value.Trim();
            string ProjeGizlilik = (projects_hidden_contest.Checked) ? "1" : "0";
            string ProjeKisisel = (projects_private.Checked) ? "1" : "0";
            string ProjeAramaMotoruYok = (projects_hide_on_search.Checked) ? "1" : "0";
            string ProjeListeBasi = (projects_publish_on_top.Checked) ? "1" : "0";
            string ProjeArkaplan = (projects_publish_highlighted.Checked) ? "1" : "0";
            string ProjeBold = (projects_publish_bold.Checked) ? "1" : "0";
            //string FaturaBilgileriCheck = (projects_invoice_detail_attributes_remind_me_later.Checked) ? "1" : "0";
            string FaturaBilgileriCheck = "1";
            bool FaturaBilgileriSecili = (FaturaBilgileriCheck == "1");
            string FaturaAdSoyad = projects_invoice_detail_attributes_company_name.Value.Trim();
            string FaturaVergiDairesi = projects_invoice_detail_attributes_tax_office.Value.Trim();
            string FaturaVergiNo = projects_invoice_detail_attributes_tax_number.Value.Trim();
            string FaturaAdres = projects_invoice_detail_attributes_address_line_1.Value.Trim();
            string FaturaSehir = projects_invoice_detail_attributes_city.Value.Trim();
            string FaturaPostaKodu = projects_invoice_detail_attributes_postal_code.Value.Trim();
            string FaturaTelefon = projects_invoice_detail_attributes_phone.Value.Trim();

            divErrorProje.Visible = false;
            if (Odul.Length == 0)
            {
                divErrorProje.Visible = true;
                liOdul.Visible = true;
            }
            if (OdulMiktari < ProjeTipOdul || OdulMiktari > OdulUstLimit)
            {
                divErrorProje.Visible = true;
                liOdulAralik.Visible = true;
            }
            if (!FaturaBilgileriSecili && FaturaAdSoyad.Length == 0)
            {
                divErrorProje.Visible = true;
                liFaturaAdSoyad.Visible = true;
            }
            if (!FaturaBilgileriSecili && FaturaVergiNo.Length == 0)
            {
                divErrorProje.Visible = true;
                liFaturaVergiNumarasi.Visible = true;
            }
            if (!FaturaBilgileriSecili && FaturaSehir.Length == 0)
            {
                divErrorProje.Visible = true;
                liFaturaSehir.Visible = true;
            }
            if (!FaturaBilgileriSecili && FaturaAdres.Length == 0)
            {
                divErrorProje.Visible = true;
                liFaturaAdres.Visible = true;
            }
            if (!FaturaBilgileriSecili && FaturaTelefon.Length == 0)
            {
                divErrorProje.Visible = true;
                liFaturaTelefon.Visible = true;
            }

            //Hata mesajı verilmemişse kayıt gerçekleştiriliyor
            if (!divErrorProje.Visible)
            {

                #region Toplam fiyat hesaplanıyor
                int HizmetBedeliYuzde = 25;
                int KdvYuzde = 18;
                int EkGun = int.Parse(hfProjectTime.Value);
                //Hizmet bedeli hesaplanıyor
                double HizmetBedeli = OdulMiktari * Bol((double)HizmetBedeliYuzde, 100);
                HizmetBedeli = HizmetBedeli + (HizmetBedeli * Bol((double)KdvYuzde, 100));
                int ToplamHizmetBedeli = (int)Math.Round(HizmetBedeli);
                //Ekstralar hesaplanıyor
                double Ekstra = 0;
                if (projects_hidden_contest.Checked) Ekstra += FiyatProjeGizli;
                if (projects_private.Checked) Ekstra += FiyatProjeKapali;
                if (projects_hide_on_search.Checked) Ekstra += FiyatProjeAramaMotoruYok;
                if (projects_publish_on_top.Checked) Ekstra += FiyatProjeListeBasi;
                if (projects_publish_highlighted.Checked) Ekstra += FiyatProjeArkaplan;
                if (projects_publish_bold.Checked) Ekstra += FiyatProjeBold;
                Ekstra = Ekstra + (Ekstra * Bol((double)KdvYuzde, 100));
                int ToplamEkstra = (int)Math.Round(Ekstra);
                int EkGunBedeli = (int)Math.Round(Bol((OdulMiktari * EkGun), 100));

                //Genel toplam hesaplanıyor
                int GenelToplam = OdulMiktari + ToplamHizmetBedeli + ToplamEkstra + EkGunBedeli;
                #endregion

                int EtkilenenSatirSayisi = Veritabani.Sorgu_Calistir("UPDATE gp_Projeler SET Asama=@Asama, Odul=@Odul, HizmetBedeli=@HizmetBedeli, Ekstra=@Ekstra, ToplamMaliyet=@ToplamMaliyet, KuponKodu=@KuponKodu, ProjeGizlilik=@ProjeGizlilik, ProjeKisisel=@ProjeKisisel, ProjeAramaMotoruYok=@ProjeAramaMotoruYok, ProjeListeBasi=@ProjeListeBasi, ProjeArkaplan=@ProjeArkaplan, ProjeBold=@ProjeBold, EkSureBedeli=@eksure, FaturaBilgileri=@FaturaBilgileri, FaturaAdSoyad=@FaturaAdSoyad, FaturaVergiDairesi=@FaturaVergiDairesi, FaturaVergiNo=@FaturaVergiNo, FaturaAdres=@FaturaAdres, FaturaSehir=@FaturaSehir, FaturaPostaKodu=@FaturaPostaKodu, FaturaTelefon=@FaturaTelefon WHERE ProjeID=@ProjeID", "2", Odul, ToplamHizmetBedeli.ToString(), ToplamEkstra.ToString(), GenelToplam.ToString(), KuponKodu, ProjeGizlilik, ProjeKisisel, ProjeAramaMotoruYok, ProjeListeBasi, ProjeArkaplan, ProjeBold, EkGunBedeli.ToString(), FaturaBilgileriCheck, FaturaAdSoyad, FaturaVergiDairesi, FaturaVergiNo, FaturaAdres, FaturaSehir, FaturaPostaKodu, FaturaTelefon, Session["ProjeID"].ToString());
                if (EtkilenenSatirSayisi == 0)
                {
                    divErrorProje.Visible = true;
                    liError.Visible = true;
                }
                else //Kayıt başarılı
                {
                    Response.Redirect("yeni_marka_3.aspx");
                }
            }
        }

        protected double Bol(double Bolen, double Bolunen)
        {
            return Bolen / Bolunen;
        }

    }
}
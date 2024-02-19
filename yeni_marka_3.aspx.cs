using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class yeni_marka_3 : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        int IntDenemeTahtasi; long DenemeTahtasi;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            #region Güvenlik protokolü
            string GelinenYer = !string.IsNullOrEmpty(Request.ServerVariables["HTTP_REFERER"]) ? Request.ServerVariables["HTTP_REFERER"].ToString() : "";
            string GidilecekYer = "";
            //Üye girişi yapılmamışsa üye giriş sayfasına yönlendiriliyor.
            string KullaniciID = (Session["UyeID"] != null) ? Session["UyeID"].ToString() : "";
            if (KullaniciID == "")
            {
                if (GelinenYer.Contains("yeni_proje_2.aspx"))
                {
                    Response.Redirect("uye_ol.aspx?fm=1");
                }
                else
                {
                    Response.Redirect("uye_ol.aspx");
                }
                //GidilecekYer = GelinenYer.Contains("yeni_proje_2.aspx") ? "uye_ol.aspx?fm=1" : "uye_ol.aspx";
            }
            else if (Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyeID=@UyeID", KullaniciID) == "") Response.Redirect("uye_ol.aspx");
            //Proje düzenleme sayfasından gelinmişse proje ID'si session'a atanıyor.
            if (!string.IsNullOrEmpty(Request.QueryString["p"])) Session["ProjeID"] = Request.QueryString["p"].ToString();
            string SeciliProjeID = (Session["ProjeID"] != null) ? Session["ProjeID"].ToString() : "";
            //Proje geçerliliği kontrol ediliyor
            if (Veritabani.Sorgu_Scalar("SELECT TOP(1) ProjeID FROM gp_Projeler WHERE ProjeID=@ProjeID AND UyeID=@UyeID", SeciliProjeID, KullaniciID) == "")
            {
                Response.Redirect("yeni_marka.aspx");
            }
            #endregion
            #region Bazı komponentler sıfırlanıyor
            divNoteEFT.Visible = false;
            divSuccessPayment.Visible = false;
            //divErrorCreditCard.Visible = false; [31.05.2015] Kredi Kartı Aktif Hale geldiğinde kaldırılacak...
            divErrorProje.Visible = false;
            divSuccessCreditCard.Visible = false;
            divSuccessEFT.Visible = false;
            liCreditCardInstallment.Visible = false;
            liCreditCardName.Visible = false;
            liCreditCardNumber1.Visible = false;
            liCreditCardNumber2.Visible = false;
            liCreditCardNumber3.Visible = false;
            liCreditCardNumber4.Visible = false;
            liCreditCardSecurityCode.Visible = false;
            #endregion
            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

            #region Ödeme kontrolleri yapılıyor
            //Ödeme yapılmışsa ödeme butonları gizleniyor
            if (Veritabani.Sorgu_Scalar("SELECT TOP(1) Onay FROM gp_Odemeler WHERE ProjeID=@ProjeID", Session["ProjeID"].ToString()) == "1")
            {
                divSuccessPayment.Visible = true;
                btnKrediKarti.Visible = false;
                btnEFT.Visible = false;
            }
            //EFT talebi yapılmışsa ödeme butonları gizleniyor
            else if (Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT COUNT(OdemeID) FROM gp_Odemeler WHERE OdemeTipi=2 AND Onay=0 AND ProjeID=@ProjeID", Session["ProjeID"].ToString())) > 0)
            {
                divNoteEFT.Visible = true;
                btnKrediKarti.Visible = false;
                btnEFT.Visible = false;
            }
            #endregion

            //Adım sekmeleri hazırlanıyor
            //lblAdimlar.Text = AletKutusu.AdimSekmeleri(3, SeciliProjeID);
            //Proje ID'si referans bilgisi alanında belirtiliyor.
            lblProjeNo.Text = Session["ProjeID"].ToString();

            //Proje bilgileri tespit ediliyor.
            DataTable TabloProjeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) TipID, KuponKodu, Odul, HizmetBedeli, Ekstra,EkSureBedeli, ToplamMaliyet,FaturaBilgileri FROM gp_Projeler WHERE ProjeID=@ProjeID", SeciliProjeID);
            Session["ProjeTipID"] = TabloProjeBilgileri.Rows[0]["TipID"].ToString();
            spanResultPrize.InnerText = TabloProjeBilgileri.Rows[0]["Odul"].ToString();
            spanResultServiceCost.InnerText = TabloProjeBilgileri.Rows[0]["HizmetBedeli"].ToString();
            spanResultExtra.InnerText = TabloProjeBilgileri.Rows[0]["Ekstra"].ToString();
            spanEkSureBedeli.InnerText = TabloProjeBilgileri.Rows[0]["EkSureBedeli"].ToString();
            spanResultTotal.InnerText = TabloProjeBilgileri.Rows[0]["ToplamMaliyet"].ToString();
            string FaturaBilgileri = TabloProjeBilgileri.Rows[0]["FaturaBilgileri"].ToString();

            if (FaturaBilgileri=="0")
            {
                FaturaBilgiDoldur.Visible = false;
            }
            
        }

        protected void btnKrediKarti_Click(object sender, EventArgs e)
        {
            string KrediKartTipi = payment_detail_card_type.Items[payment_detail_card_type.SelectedIndex].Value;
            string KrediKartAdSoyad = payment_detail_card_holder_name.Value.Trim();
            string KrediKartNumara1 = card_number_1.Value.Trim();
            if (!long.TryParse(KrediKartNumara1, out DenemeTahtasi)) KrediKartNumara1 = "";
            //string KrediKartNumara2 = card_number_2.Value.Trim();
            //if (!int.TryParse(KrediKartNumara2, out IntDenemeTahtasi)) KrediKartNumara2 = "";
            //string KrediKartNumara3 = card_number_3.Value.Trim();
            //if (!int.TryParse(KrediKartNumara3, out IntDenemeTahtasi)) KrediKartNumara3 = "";
            //string KrediKartNumara4 = card_number_4.Value.Trim();
            //if (!int.TryParse(KrediKartNumara4, out IntDenemeTahtasi)) KrediKartNumara4 = "";
            string KrediKartGuvenlikKodu = payment_detail_cvv.Value.Trim();
            if (!int.TryParse(KrediKartGuvenlikKodu, out IntDenemeTahtasi)) KrediKartGuvenlikKodu = "";
            string KrediKartSktAy = payment_detail_expiration_month.Items[payment_detail_expiration_month.SelectedIndex].Value;
            string KrediKartSktYil = payment_detail_expiration_year.Items[payment_detail_expiration_year.SelectedIndex].Value;

            if (KrediKartAdSoyad.Length == 0)
            {
                divErrorProje.Visible = true;
                liCreditCardName.Visible = true;
            }
            if (KrediKartNumara1.Length != 16)
            {
                divErrorProje.Visible = true;
                liCreditCardNumber1.Visible = true;
            }
            //if (KrediKartNumara2.Length != 4)
            //{
            //    divErrorProje.Visible = true;
            //    liCreditCardNumber2.Visible = true;
            //}
            //if (KrediKartNumara3.Length != 4)
            //{
            //    divErrorProje.Visible = true;
            //    liCreditCardNumber3.Visible = true;
            //}
            //if (KrediKartNumara4.Length != 4)
            //{
            //    divErrorProje.Visible = true;
            //    liCreditCardNumber4.Visible = true;
            //}
            if (KrediKartGuvenlikKodu.Length < 3)
            {
                divErrorProje.Visible = true;
                liCreditCardSecurityCode.Visible = true;
            }
            //TAKSİT KONTROLÜ YAPILACAK

            //Hata alınmamışsa ödeme talebi yapılıyor
            if (!divErrorProje.Visible)
            {
                bool OdemeBasarili = false;
                //ÖDEME SİSTEMİ ENTEGRASYONU YAPILACAK

                //Kullanıcı ödeme sonucu hakkında bilgilendiriliyor
                if (OdemeBasarili)
                {
                    divSuccessCreditCard.Visible = true;
                    //Ödeme butonları gizleniyor
                    btnEFT.Visible = false;
                    btnKrediKarti.Visible = false;

                    //Ödeme geldi e-postası gönderiliyor.
                    string MailKonu = "Yeni Bir Proje İçin Açılış İşlemi Başlatıldı.";
                    string MailIcerik = Session["ProjeID"].ToString() + " Projesi için eft / kredi kartı ödeme kaydı oluşturuldu.";
                    new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                }
                else //Ödeme başarısız
                {
                    divErrorCreditCard.Visible = true;
                }
            }
        }

        protected void btnEFT_Click(object sender, EventArgs e)
        {
            //EFT ödeme kaydı oluşturuluyor
            int KayitSayisi = Veritabani.Sorgu_Calistir("INSERT INTO gp_Odemeler(ProjeID, OlusturulmaTarihi, OdemeTipi, Onay) VALUES(@ProjeID, @OlusturulmaTarihi, @OdemeTipi, @Onay)", Session["ProjeID"].ToString(), DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), "2", "0");
            if (KayitSayisi > 0)
            {
                divSuccessEFT.Visible = true;
                //Ödeme butonları gizleniyor
                btnEFT.Visible = false;
                btnKrediKarti.Visible = false;

                //Ödeme geldi e-postası gönderiliyor.
                string MailKonu = "Yeni Bir Proje İçin Ödeme Yapıldı.";
                string MailIcerik = Session["ProjeID"].ToString() + " Projesi için ödeme yapıldı.";
                new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
            }
            else //Kayıt başarısız olmuşsa hata mesajı görüntüleniyor
            {
                divErrorCreditCard.Visible = true;
                liCreditCardError.Visible = true;
            }
        }

    }
}
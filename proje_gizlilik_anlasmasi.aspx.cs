using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class proje_gizlilik_anlasmasi : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
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
            DataTable TabloProjeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) t.TipAd, u.KullaniciAdi, p.OlusturulmaTarihi, p.Zamanlama, p.ProjeAdi, p.Odul, p.ProjeGizlilik, p.ProjeKisisel, (SELECT COUNT(g.GirdiID) FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=p.ProjeID) AS GirdiSayisi, (SELECT COUNT(g.GirdiID) FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=p.ProjeID AND g.Kazanan=1) AS KazananSayisi FROM gp_Projeler AS p JOIN gp_ProjeTipleri AS t ON p.TipID=t.TipID JOIN gp_Uyeler AS u ON p.UyeID=u.UyeID WHERE p.ProjeID=@ProjeID", ProjeID);
            string ProjeAdi = TabloProjeBilgileri.Rows[0]["ProjeAdi"].ToString();
            string TipAdi = TabloProjeBilgileri.Rows[0]["TipAd"].ToString();
            string ProjeSahibi = TabloProjeBilgileri.Rows[0]["KullaniciAdi"].ToString();
            string ProjeOdul = TabloProjeBilgileri.Rows[0]["Odul"].ToString();
            int ProjeZamanlama = Convert.ToInt32(TabloProjeBilgileri.Rows[0]["Zamanlama"].ToString());
            DateTime ProjeOlusturulmaTarihi = Convert.ToDateTime(TabloProjeBilgileri.Rows[0]["OlusturulmaTarihi"].ToString());
            DateTime ProjeBitisTarihi = ProjeOlusturulmaTarihi.AddDays(ProjeZamanlama);
            ProjeGizli = (TabloProjeBilgileri.Rows[0]["ProjeGizlilik"].ToString() == "1");
            ProjeKisisel = (TabloProjeBilgileri.Rows[0]["ProjeKisisel"].ToString() == "1");
            string ProjeGirdiSayisi = TabloProjeBilgileri.Rows[0]["GirdiSayisi"].ToString();
            bool ProjeKazanildi = (TabloProjeBilgileri.Rows[0]["KazananSayisi"].ToString() == "1");
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
            btnImzala.Visible = false;
            if (KullaniciUyelikTipi == "2")
            {
                if (KullaniciOnay != "1")
                {
                    //Hata mesajı görüntülenebilir
                }
                else //Üye portfolyosu onaylı
                {
                    btnImzala.Visible = true;
                }
            }
            #endregion

            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

            #region Gizlilik anlaşması ekrana yazdırılıyor
            lblGizlilikAnlasmasi.Text = @"
1-	TARAFLAR
<br /><br />
	Bir tarafta  “                                                   “     adresinde mukim  “                            “ 
(bu sözleşmede kısaca                            olarak anılacaktır.) 

	Diğer tarafta “                                                 “ adreslerinde mukim “                            “ “                                   “ (bu sözleşmede kısaca             olarak anılacaktır.) aralarında aşağıdaki koşullarda anlaşmışlardır. 


    
<br /><br />
2-	SÖZLEŞMENİN KONUSU

	Sözleşmenin konusu, “                                  “ nun (PROJE ADI) projesi kapsamında                              ‘da yürüttüğü çalışmalar ile ilgili olarak, kendisine              tarafından verilen, açıklanan gizlilik içerdiği açıkça belirtilen bilgi ve belgenin                      ‘in onayı alınmadıkça herhangi bir 3. gerçek ve/veya tüzel kişiye açıklanmamasını temin edecek olan gizliliğin sınırlarının ve koşullarının belirlenmesidir. 

    
<br /><br />
3-	(PROJE ADI) PROJE TANIMI

<br /><br />

4-	GİZLİ BİLGİNİN TANIMI 

<br /><br />
	Sözleşmede tanımlanan (PROJE ADI) projesi esnasında                          tarafından                   ‘ya açıklanan iş geliştirme projesi ile ilgili fikir, proje, buluş, iş, metod, ilerleme ve patent, telif hakkı, marka, ticari sır yada diğer yasal korunmaya konu olan yada olmayan her türlü yenilik ve çalışma esnasında öğrenilecek yazılı veya sözlü tüm ticari, mali, teknik bilgiler ve konuşma bilgileri sır olarak kabul edilir. 


    
<br /><br />
5-	TARAFLARIN YÜKÜMLÜLÜKLERİ

5.1.	, (PROJE ADI) projesi kapsamında gerekli her türlü bilgi ve belgeyi              ‘a vermeyi taahhüt eder. 
5.2.	işbu sözleşmede söz konusu edilen proje ile ilgili bilgi, belge, firma                                                               
ismi, ünvanı ve firmalarla ilgili sair bilgi ve belgelerin gizli olduğunu ve bu nedenle, sadece kendisinin ve çalışanlarının işi gereği bilmesi gerektiği kadarını bileceklerini ve bu bilgi ve belgelerin hiçbir şekilde                 ‘nın izni olmaksızın 3. gerçek ve/veya tüzel kişi ve kuruluşlara çalışma amaçları dışında açıklanmayacağını kabul ve taahhüt eder. 

                  , kendi çalışanlarının veya kendileri adına iş yapanların işbu sözleşmede öngörülen gizliliğe aykırı davranışlarından dolayı müştereken ve müteselsilen sorumlu olup, çalışanlarının veya kendileri adına iş yapanların gizlilik ilkelerine riayet edeceğini kabul ve taahhüt eder             , kendisi adına iş yapanların gizliliğe aykırı tutum ve davranışları halinde                       ‘a karşı öncelikle sorumludur. 

5.3.                           tarafından         ‘a temin edilmiş proje ile ilgili belge ve bilginin,              firmasının rızası haricinde sözleşmeye aykırı olarak ifşa edildiğinin               firması tarafından öğrenilmesi halinde,                       bundan dolayı sorumlu olacaktır.                          Bu bilgi ve belgelerin 3. şahıslara iletilmemesi için gerekli her türlü tedbiri almayı taahhüt ettiği gibi, her türlü tedbiri almasına rağmen, bu bilgi ve belgelerin yayılmasına mani olamadığını ve/veya kusuru olmadığını ileri sürerek sorumluluktan KURTULAMAZ. 

5.4. İş bu sözleşmeye aykırı bir durumun gündeme gelmesi ile                           maruz kaldığı her türlü maddi ve/veya manevi zararını tazmin etmeyi kabul ve taahhüt eder. 

<br /><br />
6-	SÜRE 

<br /><br />
	İş bu sözleşmenin konusunu oluşturan gizliliğe riayet yükümlülüğü,              ‘nin,              firması ile (PROJE ADI) projesinin başlatılması ile başlayıp, bu çalışmanın bitiminden sonra da devam edecektir. 
    
<br /><br />
7-	TEBLİGAT

<br /><br />
	Taraflar noter kanalıyla adres değişikliklerini bildirmedikleri sürece (7 gün içinde), işbu sözleşmede yazılı adreslerinin Tebligat Kanunu hükümlerine göre geçerli tebligat adresleri olduğunu her türlü bildirim ve teslim için yukarıda belirtilen adreslerin kullanılacağını kabul ederler. 
    
<br /><br />
8-	UYUŞMAZLIK

<br /><br />
	İş bu sözleşmeden dolayı ihtilaf vukuunda İstanbul Mahkemeleri ve İcra Daireleri yetkili olacaktır. Sözleşme tarafların karşılıklı rıza ve muvafakatleriyle                 tarihinde iki nüsha olarak imza edilmiştir. 
    
<br /><br />";
            #endregion
        }

        protected void btnImzala_Click(object sender, EventArgs e)
        {
            int KayitSayisi = Veritabani.Sorgu_Calistir("INSERT INTO gp_ProjeGizlilikAnlasmalari(ProjeID, UyeID, Tarih) VALUES(@ProjeID, @UyeID, @Tarih)", ProjeID, KullaniciID, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
            if (KayitSayisi > 0)
            {
                Response.Redirect("proje.aspx?p=" + ProjeAdiSifreli);
            }
        }

    }
}
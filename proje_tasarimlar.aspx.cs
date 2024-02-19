using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class proje_tasarimlar : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu; 
        int IntDenemeTahtasi;
        string GirdiID = "";
        string KullaniciID = "";
        string KullaniciUyelikTipi = "";
        string KullaniciOnay = "";
        bool ProjeGizli; bool ProjeKisisel;
        bool ProjeMetin; bool ProjeVideo;
        string ProjeID;
        string ProjeUyeID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();
            btnKazananSec.Visible = false;
            #region Proje geçerliliği kontrol ediliyor
            string ProjeAdiSifreli = (!string.IsNullOrEmpty(Request.QueryString["p"])) ? Request.QueryString["p"].ToString() : "";
            ProjeID = Veritabani.Sorgu_Scalar("SELECT TOP(1) ProjeID FROM gp_Projeler WHERE ProjeAdiSifreli=@ProjeAdiSifreli", ProjeAdiSifreli);
            if (ProjeID == "") Response.Redirect("default.aspx?h=pt29");
            #endregion
            #region Girdi geçerliliği kontrol ediliyor
            GirdiID = (!string.IsNullOrEmpty(Request.QueryString["g"])) ? Request.QueryString["g"].ToString() : "";
            GirdiID = Veritabani.Sorgu_Scalar("SELECT TOP(1) GirdiID FROM gp_ProjeGirdiler WHERE ProjeID=@ProjeID AND GirdiID=@GirdiID", ProjeID, GirdiID);
            if (GirdiID == "") Response.Redirect("default.aspx?h=pt35");
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
            DataTable TabloProjeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) g.GirdiTip, g.Baslik, g.Aciklama, g.Resim, g.Video, t.TipAd, t.Metin, t.Video, pu.UyeID AS ProjeSahibiID, pu.KullaniciAdi AS ProjeSahibiAdi,pu.Eposta as sposta, gu.Eposta as tPosta, gu.UyeID AS GirdiSahibiID, gu.KullaniciAdi AS GirdiSahibiAdi, gu.KullaniciAdiSifreli AS GirdiSahibiAdiSifreli, p.UyeID, p.OlusturulmaTarihi, p.Zamanlama, p.ProjeAdi, p.Odul, p.ProjeGizlilik, p.ProjeKisisel, p.Durum, (SELECT COUNT(g.GirdiID) FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=p.ProjeID) AS GirdiSayisi, (SELECT COUNT(g.GirdiID) FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=p.ProjeID AND g.Kazanan=1) AS KazananSayisi FROM gp_ProjeGirdiler AS g JOIN gp_Projeler AS p ON g.ProjeID=p.ProjeID JOIN gp_ProjeTipleri AS t ON p.TipID=t.TipID JOIN gp_Uyeler AS pu ON p.UyeID=pu.UyeID JOIN gp_Uyeler AS gu ON g.UyeID=gu.UyeID WHERE g.GirdiID=@GirdiID", GirdiID);
            ProjeUyeID = TabloProjeBilgileri.Rows[0]["UyeID"].ToString();
            string ProjeAdi = TabloProjeBilgileri.Rows[0]["ProjeAdi"].ToString();
            string TipAdi = TabloProjeBilgileri.Rows[0]["TipAd"].ToString();
            ProjeMetin = TabloProjeBilgileri.Rows[0]["Metin"].ToString() == "1";
            ProjeVideo = TabloProjeBilgileri.Rows[0]["Video"].ToString() == "1";
            string ProjeSahibiID = TabloProjeBilgileri.Rows[0]["ProjeSahibiID"].ToString();
            hfProjeSahibi.Value = ProjeSahibiID;
            string ProjeSahibiAdi = TabloProjeBilgileri.Rows[0]["ProjeSahibiAdi"].ToString();
            string GirdiSahibiID = TabloProjeBilgileri.Rows[0]["GirdiSahibiID"].ToString();
            hfTasarimci.Value = GirdiSahibiID;
            string TasarimciMail = TabloProjeBilgileri.Rows[0]["tposta"].ToString();
            hfTasarimciMail.Value = TasarimciMail;
            string SahipMail = TabloProjeBilgileri.Rows[0]["sposta"].ToString();
            hfSahipMail.Value = SahipMail;
            string GirdiSahibiAdi = TabloProjeBilgileri.Rows[0]["GirdiSahibiAdi"].ToString();
            string GirdiSahibiAdiSifreli = TabloProjeBilgileri.Rows[0]["GirdiSahibiAdiSifreli"].ToString();
            string ProjeOdul = TabloProjeBilgileri.Rows[0]["Odul"].ToString();
            string ProjeDurum = TabloProjeBilgileri.Rows[0]["Durum"].ToString();
            int ProjeZamanlama = Convert.ToInt32(TabloProjeBilgileri.Rows[0]["Zamanlama"].ToString());
            DateTime ProjeOlusturulmaTarihi = Convert.ToDateTime(TabloProjeBilgileri.Rows[0]["OlusturulmaTarihi"].ToString());
            DateTime ProjeBitisTarihi = ProjeOlusturulmaTarihi.AddDays(ProjeZamanlama);
            ProjeGizli = (TabloProjeBilgileri.Rows[0]["ProjeGizlilik"].ToString() == "1");
            ProjeKisisel = (TabloProjeBilgileri.Rows[0]["ProjeKisisel"].ToString() == "1");
            string ProjeGirdiSayisi = TabloProjeBilgileri.Rows[0]["GirdiSayisi"].ToString();
            bool ProjeKazanildi = TabloProjeBilgileri.Rows[0]["KazananSayisi"].ToString() == "1";
            lblProjeAdi.Text = ProjeAdi;
            lblProjeTip.Text = TipAdi;
            lblProjeSahibi.Text = ProjeSahibiAdi;
            lblTabloHucreOdul.Text = ProjeOdul;
            lblTabloHucreBitisTarihi.Text = ProjeBitisTarihi.ToString("dd ") + AletKutusu.AyAdiTespitEt(ProjeBitisTarihi) + ProjeBitisTarihi.ToString(" yyyy HH:mm");
            lblTabloHucreTasarimSayisi.Text = ProjeGirdiSayisi;
            if (ProjeKazanildi)
            {
                lblTabloBaslikKazananTasarim.Text = "Kazanan Tasarım";
                lblTabloHucreKazananTasarim.Text = "#" + Veritabani.Sorgu_Scalar("SELECT TOP(1) g.GirdiNo FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=@ProjeID AND g.Kazanan=1", ProjeID);
            }

            string GirdiTip = TabloProjeBilgileri.Rows[0]["GirdiTip"].ToString();
            string GirdiBaslik = TabloProjeBilgileri.Rows[0]["Baslik"].ToString();
            string GirdiAciklama = TabloProjeBilgileri.Rows[0]["Aciklama"].ToString();
            string GirdiResim = TabloProjeBilgileri.Rows[0]["Resim"].ToString();
            string GirdiVideo = TabloProjeBilgileri.Rows[0]["Video"].ToString();

            string GirdiBilgileri = "";
            switch(GirdiTip)
            {
                case "0": //Metin
                    break;
                case "1": //Resim
                GirdiBilgileri = @"
                    <div class=""big_photo"">
                        <img alt=""" + GirdiBaslik + @""" src=""/images/contest_entries/600/" + GirdiResim + @""" />
                    <div>";
                    break;
                case "2": //Video
                    break;
            }

            #region Üyenin diğer girdileri
            DataTable TabloDigerGirdiler = Veritabani.Sorgu_DataTable("SELECT GirdiID, GirdiNo, GirdiTip, Baslik, Aciklama, Resim, Video FROM gp_ProjeGirdiler WHERE ProjeID=@ProjeID AND UyeID=@UyeID AND GirdiID!=@GirdiID ORDER BY GirdiNo", ProjeID, GirdiSahibiID, GirdiID);
            if (TabloDigerGirdiler.Rows.Count == 0)
            {
                GirdiBilgileri += @"
                    <p><a href=""tasarimci.aspx?a=" + GirdiSahibiAdiSifreli + @""">" + GirdiSahibiAdi + @"</a></p>";
            }
            else
            {
                GirdiBilgileri += @"
                    <p><br /><a href=""tasarimci.aspx?a=" + GirdiSahibiAdiSifreli + @""">" + GirdiSahibiAdi + @"</a> isimli kullanıcının bu projeye gönderdiği diğer çalışmalar</p>
                    <div class=""kutular"">";
                string SatirGirdiID;  string SatirGirdiNo; string SatirGirdiTip; string SatirGirdiBaslik; string SatirGirdiAciklama; string SatirGirdiResim; string SatirGirdiVideo;
                for (int i = 0; i < TabloDigerGirdiler.Rows.Count; i++)
                {
                    SatirGirdiID = TabloDigerGirdiler.Rows[i]["GirdiID"].ToString();
                    SatirGirdiNo = TabloDigerGirdiler.Rows[i]["GirdiNo"].ToString();
                    SatirGirdiTip = TabloDigerGirdiler.Rows[i]["GirdiTip"].ToString();
                    SatirGirdiBaslik = TabloDigerGirdiler.Rows[i]["Baslik"].ToString();
                    SatirGirdiAciklama = TabloDigerGirdiler.Rows[i]["Aciklama"].ToString();
                    SatirGirdiResim = TabloDigerGirdiler.Rows[i]["Resim"].ToString();
                    SatirGirdiVideo = TabloDigerGirdiler.Rows[i]["Video"].ToString();



                    GirdiBilgileri += @"
                        <div style=""float:left; text-align:center;"">
                                <div class=""kutu2 proje_arkaplan"">
                                    <div class=""kutu2_ust"">
                                        <div class=""kutu2_ic"">                           
                                             <a href=""proje_tasarimlar.aspx?p=" + ProjeAdiSifreli + "&g=" + SatirGirdiID + @""">
                                    <img alt=""" + SatirGirdiBaslik + @""" src=""/images/contest_entries/120/" + SatirGirdiResim + @"""/>
                                </a>
                                        </div>
                                    </div>
                                    <div class=""kutu2_alt"">                                        
                                        
                                    </div>                                

                                </div>
                       </div>";
                    
                    
                    ////////////switch(SatirGirdiTip)
                    ////////////{
                    ////////////    case "0"://Metin
                    ////////////        break;
                    ////////////    case "1"://Resim 
                    ////////////        break;
                    ////////////    case "2"://Video
                    ////////////        break;
                    ////////////}
                }
                GirdiBilgileri += @"
                    </div>";
            }
            #endregion

            lblProjeDetay.Text = GirdiBilgileri;
            #endregion

            bool GizlilikAnlasmasiImzali = Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT COUNT(ID) FROM gp_ProjeGizlilikAnlasmalari WHERE ProjeID=@ProjeID AND UyeID=@UyeID", ProjeID, KullaniciID)) > 0;
            #region Anlaşma imzalama butonu görüntüleniyor
            if (ProjeGizli)
            {
                if (!GizlilikAnlasmasiImzali)
                {
                    lblProjeDetay.Visible = false;
                }
            }
            #endregion
            btnProjeGaleri.HRef = "proje.aspx?p=" + ProjeAdiSifreli;

            #region Yorumlar listeleniyor
            DataTable TabloYorumlar = Veritabani.Sorgu_DataTable("SELECT y.Tarih, y.Yorum, u.UyeID, u.KullaniciAdi, u.KullaniciAdiSifreli, u.Avatar, u.UyelikTipi FROM gp_Yorumlar AS y JOIN gp_Uyeler AS u ON y.UyeID=u.UyeID WHERE y.Aktif='1' and y.GirdiID=@GirdiID ORDER BY y.Tarih", GirdiID);
            string ListeYorumlar = "";
            for (int y = 0; y < TabloYorumlar.Rows.Count; y++)
            {
                DateTime YorumTarih = Convert.ToDateTime(TabloYorumlar.Rows[y]["Tarih"].ToString());
                string YorumTarihString = YorumTarih.ToString("dd.MM.yyyy HH:mm");
                string YorumDetay = TabloYorumlar.Rows[y]["Yorum"].ToString();
                string YorumKullaniciAdi = TabloYorumlar.Rows[y]["KullaniciAdi"].ToString();
                string YorumKullaniciAdiSifreli = TabloYorumlar.Rows[y]["KullaniciAdiSifreli"].ToString();
                string YorumKullaniciAvatar = TabloYorumlar.Rows[y]["Avatar"].ToString();
                if (YorumKullaniciAvatar == "") YorumKullaniciAvatar = "MarkaKafaAvatar.jpg";
                string YorumKullaniciUyelikTipi = TabloYorumlar.Rows[y]["UyelikTipi"].ToString();
                string LinkKullanici = YorumKullaniciUyelikTipi == "2" ? @" href=""tasarimci.aspx?a=" + YorumKullaniciAdiSifreli + @""" " : "";
                ListeYorumlar += @"
                    <div class=""yorum"">
                        <div class=""divTasarimci"">
                            <a href=""" + LinkKullanici + @"""> <img alt=""" + YorumKullaniciAdi + @""" src=""images/avatars/120/" + YorumKullaniciAvatar + @"""style=""border-radius: 100%; height: 50px; margin-left:-7px; margin-top: 9px; width: 60px;"" title=""" + YorumKullaniciAdi + @""" /></a>
                        </div>
                        <div class=""divYorumDetay"">
                            [" + YorumTarihString + "] - <a " + LinkKullanici + ">" + YorumKullaniciAdi + @"</a> - 
                            " + YorumDetay + @"
                        </div>
                    </div>
                    <div style=""clear:both;""></div>";
            }
            lblProjeYorumlar.Text = ListeYorumlar;
            #endregion

            #region Kazanan seç butonu görüntüleniyor
            if (KullaniciID == ProjeSahibiID && ProjeDurum == "2")
            {
                btnKazananSec.Visible = true;
            }
            #endregion

            #region Proje ve tasarım sahiplerine yorum gönderim alanı görüntüleniyor
            if (KullaniciID == ProjeSahibiID || KullaniciID == GirdiSahibiID)
            {
                divYeniYorum.Visible = true;
            }
            if (ProjeDurum=="3")
            {
                divYeniYorum.Visible = false;
            }
            #endregion

            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.
        }

        protected void btnYorumGonder_Click(object sender, EventArgs e)
        {
            divErrorYorum.Visible = false;
            liErrorYorumIcerik.Visible = false;
            liErrorYorum.Visible = false;
            divSuccessYorum.Visible = false;
            string Yorum = txtYorum.Value.Trim();
            if (Yorum.Length == 0)
            {
                divErrorYorum.Visible = true;
                liErrorYorumIcerik.Visible = true;
            }
            else
            {
                string YorumTarih = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                int EklenenYorum = Veritabani.Sorgu_Calistir_Eklenen_Id_Dondur("INSERT INTO gp_Yorumlar(GirdiID, UyeID, Tarih, Yorum, Aktif) VALUES(@GirdiID, @UyeID, @Tarih, @Yorum, 0)", GirdiID, KullaniciID, YorumTarih, Yorum);
                if (EklenenYorum > 0)
                {
                    divYeniYorum.Visible = false;
                    divSuccessYorum.Visible = true;

                    //Yorum geldi e-postası gönderiliyor.
                   string MailKonu = "Yeni Bir Yorum Girildi.";
                   string MailIcerik = Yorum;
                   if (hfProjeSahibi.Value==KullaniciID)
                   {                       
                        new Mail().MailGonder(Server, "", hfTasarimciMail.Value, MailKonu, MailIcerik);
                   }
                   else if (hfTasarimciMail.Value==KullaniciID)
                   {
                        new Mail().MailGonder(Server, "", hfSahipMail.Value, MailKonu, MailIcerik);
                   }
                   new Mail().MailGonder(Server, "", "admin@markakafa.com", MailKonu, MailIcerik);
                }
                else
                {
                    divErrorYorum.Visible = true;
                    liErrorYorum.Visible = true;
                }
            }
        }

        protected void btnKazananSec_Click(object sender, EventArgs e)
        {
            string MailKonu = "Kazanan Seçildi.";
            string MailIcerik = "Tebrikler, Kazanan tasarım seninki oldu.";

            divSuccessKazanan.Visible = false;
            divErrorKazanan.Visible = false;
            string Tarih = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            int Etki = Veritabani.Sorgu_Calistir("UPDATE gp_Projeler SET Durum=3 WHERE ProjeID=@ProjeID", ProjeID);
            Veritabani.Sorgu_Calistir("UPDATE gp_ProjeGirdiler SET Kazanan=1 WHERE GirdiID=@GirdiID", GirdiID);
            if (Etki > 0)
            {
                new Mail().MailGonder(Server, "", hfSahipMail.Value, MailKonu, MailIcerik);
                new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                divSuccessKazanan.Visible = true;
                btnKazananSec.Text = "Kazanan Seçildi";
            }
            else
            {
                divErrorKazanan.Visible = true;
            }
        }

    }
}
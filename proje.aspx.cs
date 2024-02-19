using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class proje : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        int IntDenemeTahtasi;
        string KullaniciID = "";
        string KullaniciUyelikTipi = "";
        string KullaniciOnay = "";
        bool ProjeGizli; bool ProjeKisisel;
        bool ProjeMetin; bool ProjeVideo;
        string ProjeID; string ProjeAdiSifreli = "";
        string ProjeDurum = ""; string ProjeKazanan = "";
        string ProjeUyeID = "";
        DateTime ProjeBitisTarihi;

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
            DataTable TabloProjeBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) t.TipAd, t.Metin, t.Video, u.KullaniciAdi, p.UyeID, p.OlusturulmaTarihi, p.Zamanlama, p.ProjeAdi, p.Odul, p.ProjeGizlilik, p.ProjeKisisel, p.Durum, (SELECT COUNT(g.GirdiID) FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=p.ProjeID) AS GirdiSayisi, (SELECT COUNT(g.GirdiID) FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=p.ProjeID AND g.Kazanan=1) AS KazananSayisi FROM gp_Projeler AS p JOIN gp_ProjeTipleri AS t ON p.TipID=t.TipID JOIN gp_Uyeler AS u ON p.UyeID=u.UyeID WHERE p.ProjeID=@ProjeID", ProjeID);
            ProjeUyeID = TabloProjeBilgileri.Rows[0]["UyeID"].ToString();
            ProjeDurum = TabloProjeBilgileri.Rows[0]["Durum"].ToString();
            string ProjeAdi = TabloProjeBilgileri.Rows[0]["ProjeAdi"].ToString();
            string TipAdi = TabloProjeBilgileri.Rows[0]["TipAd"].ToString();
            ProjeMetin = TabloProjeBilgileri.Rows[0]["Metin"].ToString() == "1";
            ProjeVideo = TabloProjeBilgileri.Rows[0]["Video"].ToString() == "1";
            string ProjeSahibi = TabloProjeBilgileri.Rows[0]["KullaniciAdi"].ToString();
            string ProjeOdul = TabloProjeBilgileri.Rows[0]["Odul"].ToString();
            int ProjeZamanlama = Convert.ToInt32(TabloProjeBilgileri.Rows[0]["Zamanlama"].ToString());
            DateTime ProjeOlusturulmaTarihi = Convert.ToDateTime(TabloProjeBilgileri.Rows[0]["OlusturulmaTarihi"].ToString());
            ProjeBitisTarihi = ProjeOlusturulmaTarihi.AddDays(ProjeZamanlama);
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
                ProjeKazanan = Veritabani.Sorgu_Scalar("SELECT TOP(1) g.UyeID FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=@ProjeID AND g.Kazanan=1", ProjeID);
            }

            #endregion

            #region Duyuru varsa tespit ediliyor
            string Duyuru = "";
            Duyuru = @"
                <div class=""note_box"">
		            <h3>Duyuru:</h3>
		            <p>Proje ödül bedeli güncellenmiştir.</p>
	            </div>";
            #endregion

            bool GizlilikAnlasmasiImzali = Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT COUNT(ID) FROM gp_ProjeGizlilikAnlasmalari WHERE ProjeID=@ProjeID AND UyeID=@UyeID", ProjeID, KullaniciID)) > 0;
            #region Proje detay, Projeye katılım ve Portfolyo ekleme butonları görüntüleniyor
            //Proje detay butonu
            btnProjeDetaylari.HRef = "proje_detay.aspx?p=" + ProjeAdiSifreli;
            
            if (ProjeGizli)
            {
                if (!GizlilikAnlasmasiImzali)
                {
                    btnProjeDetaylari.Visible = false;
                }
            }
            //Projeye katılım ve portfolyo butonları
            btnPortfolyoEkle.Visible = false;
            btnProjeyeKatil.Visible = false;
            btnGizlilikAnlasmasi.Visible = false;
            if (KullaniciUyelikTipi == "2")
            {
                if (KullaniciOnay != "1")
                {
                    btnPortfolyoEkle.Visible = true;
                }
                else if(ProjeDurum != "3" || (KullaniciID == ProjeKazanan && DateTime.Now.Ticks < ProjeBitisTarihi.AddDays(3).Ticks)) //Üye portfolyosu onaylı ve proje tamamlanmamışsa veya kazanan üye giriş yapmışsa ve proje bitiş tarihinden itibaren 3 gün geçmemişse
                {
                    btnProjeyeKatil.HRef = "proje_katil.aspx?p=" + ProjeAdiSifreli;
                    btnGizlilikAnlasmasi.HRef = "proje_gizlilik_anlasmasi.aspx?p=" + ProjeAdiSifreli;
                    if (ProjeGizli)
                    {
                        if (GizlilikAnlasmasiImzali)
                            btnProjeyeKatil.Visible = true;
                        else
                            btnGizlilikAnlasmasi.Visible = true;
                    }
                    else
                    {
                        btnProjeyeKatil.Visible = true;
                    }
                }
            }
            #endregion

            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

            ProjeGirdiListele();
        }

        protected void btnSayfaDegistir_Click(object sender, EventArgs e)
        {
            int FiltreSayfa = (int.TryParse(hf_page.Value, out IntDenemeTahtasi)) ? IntDenemeTahtasi : 1;
            ProjeGirdiListele(FiltreSayfa);
        }

        protected void ProjeGirdiListele(int SayfaNo = 1)
        {
            if (KullaniciID != ProjeUyeID && (ProjeGizli || ProjeKisisel || ProjeMetin))
            {
                #region Gizli proje alanı görüntüleniyor
                string GizliProjeAlani = @"
                    <h2 class=""hidden_project center_text"" style=""margin-left:150px;"">Gizli Galeri</h2>
                    <div class=""center_text"" style=""margin-left:145px;"">
                        <p>Bu projenin galerisi sadece proje sahibi tarafından görüntülenebilir.</p>
                    </div>";
                lblProjeGirdiListesi.Text = GizliProjeAlani;
                #endregion
            }
            else //Proje listesi görüntüleniyor
            {
                #region Proje girdileri listeniyor
                string ListeProjeGirdileri = "";
                string ListeSayfaNumaralari = "";
                DataTable TabloProjeGirdileri = Veritabani.Sorgu_DataTable("SELECT g.GirdiID, g.GirdiNo, g.GirdiTip, g.Baslik, g.Resim, g.Video, g.Favori, g.Kazanan, p.ProjeAdiSifreli, u.KullaniciAdi, u.KullaniciAdiSifreli, u.Avatar, (SELECT COUNT(y.YorumID) FROM gp_Yorumlar AS y WHERE Sistem=0 AND y.GirdiID=g.GirdiID) AS YorumSayisi FROM gp_ProjeGirdiler AS g JOIN gp_Projeler AS p ON g.ProjeID=p.ProjeID JOIN gp_Uyeler AS u ON g.UyeID=u.UyeID WHERE g.ProjeID=@ProjeID", ProjeID);
                #region Sayfalama oluşturuluyor
                double SayfaBasinaKayitSayisi = 10;
                double ToplamKayitSayisi = TabloProjeGirdileri.Rows.Count;
                double SayfalamaAltLimit = (SayfaNo - 1) * SayfaBasinaKayitSayisi;
                double SayfalamaUstLimit = SayfalamaAltLimit + SayfaBasinaKayitSayisi - 1;
                double SayfaSayisi = Math.Ceiling(ToplamKayitSayisi / SayfaBasinaKayitSayisi);
                double OncekiSayfa = SayfaNo - 1;
                double SonrakiSayfa = (SayfaNo < SayfaSayisi) ? SayfaNo + 1 : 0;
                #endregion
                #region Katılım listesi oluşturuluyor
                string GirdiID = ""; string GirdiNo = ""; string GirdiTip = ""; string GirdiBaslik = ""; string GirdiResim = ""; string GirdiVideo = ""; bool GirdiFavori; bool GirdiKazanan; string GirdiProjeAdiSifreli = ""; string GirdiKatilimciAdi = ""; string GirdiKatilimciAdiSifreli = ""; string GirdiKatilimciAvatar = ""; int GirdiYorumSayisi;
                string IkonFavori = ""; string IkonKazanan = ""; string YorumSayisiBilgisi = "";

                ListeProjeGirdileri = @"
                    <ul id=""entry_list"">";
                for (int i = 0; i < TabloProjeGirdileri.Rows.Count; i++)
                {
                    if (i < SayfalamaAltLimit) continue;
                    else if (i > SayfalamaUstLimit) break;

                    GirdiID = TabloProjeGirdileri.Rows[i]["GirdiID"].ToString();
                    GirdiNo = TabloProjeGirdileri.Rows[i]["GirdiNo"].ToString();
                    GirdiTip = TabloProjeGirdileri.Rows[i]["GirdiTip"].ToString();
                    GirdiBaslik = TabloProjeGirdileri.Rows[i]["Baslik"].ToString();
                    GirdiResim = TabloProjeGirdileri.Rows[i]["Resim"].ToString();
                    GirdiVideo = TabloProjeGirdileri.Rows[i]["Video"].ToString();
                    GirdiFavori = (TabloProjeGirdileri.Rows[i]["Favori"].ToString() == "1");
                    GirdiKazanan = (TabloProjeGirdileri.Rows[i]["Kazanan"].ToString() == "1");
                    GirdiProjeAdiSifreli = TabloProjeGirdileri.Rows[i]["ProjeAdiSifreli"].ToString();
                    GirdiKatilimciAdi = TabloProjeGirdileri.Rows[i]["KullaniciAdi"].ToString();
                    GirdiKatilimciAdiSifreli = TabloProjeGirdileri.Rows[i]["KullaniciAdiSifreli"].ToString();
                    GirdiKatilimciAvatar = TabloProjeGirdileri.Rows[i]["Avatar"].ToString();
                    GirdiYorumSayisi = Convert.ToInt32(TabloProjeGirdileri.Rows[i]["YorumSayisi"].ToString());

                    if (GirdiBaslik.Length >= 18)
                    {
                        GirdiBaslik = GirdiBaslik.Substring(0, 18) + "...";
                    }
                    else if (GirdiBaslik.Length >= 25)
                    {
                        GirdiBaslik = GirdiBaslik.Substring(0, 25) + "...";
                    }

                    if (GirdiKatilimciAvatar == "") GirdiKatilimciAvatar = "logo.png";
                    if (GirdiResim == "") GirdiResim = "logo.png";

                    IkonFavori = (GirdiFavori) ? @"<span class=""smiley""></span>" : "";
                    IkonKazanan = (GirdiKazanan) ? @"<span class=""winner-logo"">winner</span>" : "";
                    YorumSayisiBilgisi = (GirdiYorumSayisi > 0) ? @"<div class=""info_comment"">" + GirdiYorumSayisi.ToString() + " yorum</div>" : "";
                    switch(GirdiTip){
                        case "0": //Metin
                            break;
                        case "1": //Resim
                            ListeProjeGirdileri += @"
                            <div class=""kutu"" style=""" + (GirdiKazanan ? "background-color:#AB2B77;" : "") + @""">
                              <img style=""cursor:pointer; width:180px; height:140px;"" src=""images/contest_entries/300/" + GirdiResim + @""" onclick=""document.location='proje_tasarimlar.aspx?p=" + GirdiProjeAdiSifreli + "&g=" + GirdiID + @"';"" />
                              <a href=""proje_tasarimlar.aspx?p=" + GirdiProjeAdiSifreli + "&g=" + GirdiID + @""">" + GirdiBaslik + @"</a>
                              <div class=""clear""></div>
                                <br />
                                <div class=""tasarimci_mini_img"">
                                      <img src=""images/avatars/60/" + GirdiKatilimciAvatar + @""" style=""width:40px; height:40px;"" />
                                </div>
                                <div class=""tasarimci_sag"" style=""float:none;"">
                                    <a href=""tasarimci.aspx?a=" + GirdiKatilimciAdiSifreli + @""">" + GirdiKatilimciAdi + @"  </a>
                                             " + YorumSayisiBilgisi + @"
                                </div>                                
                            </div>";


//@"
//                            <li id=""entry_" + GirdiID + @""">
//		                        " + IkonKazanan + @"
//		                        <div class=""photo-btns"">
//			                        <a class=""contest-entry-lightbox btn"" data-large-img=""images/contest_entries/600/" + GirdiResim + @""" href=""proje_tasarimlar.aspx?g=" + GirdiID + @""" title=""Büyüt"">Büyüt</a>
//			                        <a class=""entry-url btn"" href=""proje_tasarimlar.aspx?g=" + GirdiID + @""" target=""_blank"" title=""Yeni pencerede görüntüle"">Yeni pencerede görüntüle</a>
//                                    " + IkonFavori + @"
//		                        </div>
//		                        <a class=""no_hover"" href=""proje_tasarimlar.aspx?p=" + ProjeAdiSifreli + "&g=" + GirdiID + @"""><img alt=""" + GirdiBaslik + @""" class=""frame"" height=""180"" src=""images/contest_entries/300/" + GirdiResim + @""" title=""" + GirdiBaslik + @""" width=""240"" /></a>
//		                        <div class=""description"">
//			                        <ul id=""info"">
//				                        <li class=""info_entry_no"">
//					                        #" + GirdiNo + @"
//				                        </li>
//				                        <li class=""info_list"">
//					                        <a href=""tasarimci.aspx?a=" + GirdiKatilimciAdiSifreli + @""">" + GirdiKatilimciAdi + @"</a>
//				                        </li>
//                                        " + YorumSayisiBilgisi + @"
//			                        </ul>
//		                        </div>
//		                        <div class=""lightbox-content"">
//			                        <div class=""contest_entry_fancybox_container center_text"">
//				                        <h2>
//					                        #" + GirdiNo + @"
//					                        &mdash;
//					                        <a href=""tasarimci.aspx?a=" + GirdiKatilimciAdiSifreli + @""">" + GirdiKatilimciAdi + @"</a>
//				                        </h2>
//			                        </div>
//		                        </div>
//	                        </li>";
                            break;
                        case "2": //Video
                            break;
                    }
                }
                ListeProjeGirdileri += @"</ul>";
                #endregion
                #region Sayfa numaraları oluşturuluyor
                string OncekiSayfaDisabled = (OncekiSayfa == 0) ? " disabled" : "";
                string SonrakiSayfaDisabled = (SonrakiSayfa == 0) ? " disabled" : "";
                ListeSayfaNumaralari += @"
      <div class=""DivSayfalama"">

        <a class=""previous_page " + OncekiSayfaDisabled + @""" onclick=""sayfaDegistir('" + OncekiSayfa.ToString() + @"');"" href=""javascript:void(0);"" rel=""prev"">&laquo; Önceki</a> ";
                string SayfaRel = "";
                for (int s = 1; s <= SayfaSayisi; s++)
                {
                    SayfaRel = "";
                    if (s == SayfaNo)
                        ListeSayfaNumaralari += @"
        <em class=""current"">" + s.ToString() + @"</em> ";
                    else if (s == 3 && SayfaNo > 9) //Sol boşluk
                        ListeSayfaNumaralari += @"
        <span class=""gap"">&hellip;</span> ";
                    else if (s == SayfaSayisi - 3 && SayfaNo < SayfaSayisi - 9) //Sağ boşluk
                        ListeSayfaNumaralari += @"
        <span class=""gap"">&hellip;</span> ";
                    else if (s <= 2 || s >= SayfaSayisi - 1 || (s > SayfaNo - 6 && s < SayfaNo + 5))
                    {
                        if (s == SayfaNo - 1) SayfaRel = @"rel=""prev""";
                        else if (s == SayfaNo + 1) SayfaRel = @"rel=""next""";
                        ListeSayfaNumaralari += @"
        <a " + SayfaRel + @" onclick=""sayfaDegistir('" + s.ToString() + @"');"" href=""javascript:void(0);"">" + s.ToString() + @"</a> ";
                    }
                }
                ListeSayfaNumaralari += @"
        <a class=""next_page " + SonrakiSayfaDisabled + @""" rel=""next"" onclick=""sayfaDegistir('" + SonrakiSayfa.ToString() + @"');"" href=""javascript:void(0);"">Sonraki &raquo;</a>
      </div>";

                
                #endregion

                lblProjeGirdiListesi.Text = ListeProjeGirdileri;
                lblDivSayfalama.Text = ListeSayfaNumaralari;
                #endregion
            }
        }

    }
}
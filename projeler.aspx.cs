using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class projeler : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        int IntDenemeTahtasi;
        string AktifSekme = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();
            #region Sekme seçiliyor
            AktifSekme = "c";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                switch (Request.QueryString["s"].ToString())
                {
                    case "p":
                    case "s":
                    case "c":
                        AktifSekme = Request.QueryString["s"].ToString();
                        break;
                }
            }
            string SeciliDevamEden = (AktifSekme == "p") ? "_aktif" : "";
            string SeciliTamamlanan = (AktifSekme == "c") ? "_aktif" : "";
            string SeciliGizli = (AktifSekme == "s") ? "_aktif" : "";
            //if (AktifSekme == "p") lblSectionStart.Text = @"<section id=""projects_index"">";
            //else if (AktifSekme == "c") lblSectionStart.Text = @"<section id=""projects_completed"">";
            //else if (AktifSekme == "s") lblSectionStart.Text = @"<section id=""professional_completed"">";
            //lblSectionEnd.Text = "</section>";
            string SekmeListesi = @"

          <a href=""projeler.aspx?s=p""><div class=""uclu_menu_sol" + SeciliDevamEden + @"""> devam eden projeler </div></a> 
           <a href=""projeler.aspx?s=c""><div class=""uclu_menu_orta" + SeciliTamamlanan + @"""> tamamlanan projeler </div></a> 
           <a href=""projeler.aspx?s=s""><div class=""uclu_menu_sag" + SeciliGizli + @"""> gizli projeler </div></a>";

            lblSekmeler.Text = SekmeListesi;
            #endregion

//            #region Filtre seçenekleri hazırlanıyor
//            string ListeFiltreProjeTurleri = @"
//            <select runat=""server"" class=""uniform"" id=""project_type"" name=""project_type"" onchange=""document.getElementById('hf_filter_category').value=this.options[this.selectedIndex].value; document.getElementById('btnFiltrele').click();"" clientidmode=""Static"">
//                <option value="""">Proje türüne göre</option>";
//            DataTable TabloProjeTurleri = Veritabani.Sorgu_DataTable("SELECT TipID, TipAd FROM gp_ProjeTipleri WHERE Metin=0 ORDER BY TipAd");
//            string ProjeTipID = ""; string ProjeTipAd = ""; string ProjeTipSecili = "";
//            for (int i = 0; i < TabloProjeTurleri.Rows.Count; i++)
//            {
//                ProjeTipID = TabloProjeTurleri.Rows[i]["TipID"].ToString();
//                ProjeTipAd = TabloProjeTurleri.Rows[i]["TipAd"].ToString();
//                ProjeTipSecili = (ProjeTipID == hf_filter_category.Value) ? "selected" : "";
//                ListeFiltreProjeTurleri += @"
//                <option " + ProjeTipSecili + @" value=""" + ProjeTipID + @""">" + ProjeTipAd + @"</option>";
//            }
//            ListeFiltreProjeTurleri += @"
//            </select>";
//            lblDropdownProjeTuru.Text = ListeFiltreProjeTurleri;
//            string ListeFiltreSektorler = @"
//            <select runat=""server"" class=""uniform"" id=""project_tag"" name=""project_tag"" onchange=""document.getElementById('hf_filter_sector').value=this.options[this.selectedIndex].value; document.getElementById('btnFiltrele').click();"" clientidmode=""Static"">
//                <option value=''>Sektöre göre</option>";
//            DataTable TabloSektorler = Veritabani.Sorgu_DataTable("SELECT SektorID, SektorAdi FROM gp_Sektorler ORDER BY SektorAdi");
//            string SektorID = ""; string SektorAd = ""; string SektorSecili = "";
//            for (int i = 0; i < TabloSektorler.Rows.Count; i++)
//            {
//                SektorID = TabloSektorler.Rows[i]["SektorID"].ToString();
//                SektorAd = TabloSektorler.Rows[i]["SektorAdi"].ToString();
//                SektorSecili = (SektorID == hf_filter_sector.Value) ? "selected" : "";
//                ListeFiltreSektorler += @"
//                <option " + SektorSecili + @" value=""" + SektorID + @""">" + SektorAd + @"</option>";
//            }
//            ListeFiltreSektorler += @"
//            </select>";
//            lblDropdownSektor.Text = ListeFiltreSektorler;
//            #endregion

            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

            //Projeler listeleniyor
            ProjeListele();
        }

        protected void btnFiltrele_Click(object sender, EventArgs e)
        {
            string FiltreKategori = hf_filter_category.Value;
            string FiltreSektor = hf_filter_sector.Value;
            ProjeListele(1, FiltreKategori, FiltreSektor);
        }

        protected void btnSayfaDegistir_Click(object sender, EventArgs e)
        {
            string FiltreKategori = hf_filter_category.Value;
            string FiltreSektor = hf_filter_sector.Value;
            int FiltreSayfa = (int.TryParse(hf_page.Value, out IntDenemeTahtasi)) ? IntDenemeTahtasi : 1;
            ProjeListele(FiltreSayfa, FiltreKategori, FiltreSektor);
        }

        protected void ProjeListele(int SayfaNo = 1, string FiltreKategori = "", string FiltreSektor = "")
        {
            string ListeProjeler = "";
            string ListeSayfaNumaralari = "";
            #region Filtre oluşturuluyor
            string SorguFiltre = "";
            string SorguSiralama = " ORDER BY p.ProjeListeBasi DESC, p.OlusturulmaTarihi DESC";
            List<string> DiziFiltreParametreleri = new List<string>();
            //Kategori filtresi
            ////if (FiltreKategori.Length > 0)
            ////{
            ////    SorguFiltre += " AND p.TipID=@KategoriID";
            ////    DiziFiltreParametreleri.Add(FiltreKategori);
            ////}
            //////Sektör filtresi
            ////if (FiltreSektor.Length > 0)
            ////{
            ////    SorguFiltre += " AND p.SektorID=@SektorID";
            ////    DiziFiltreParametreleri.Add(FiltreSektor);
            ////}
            //Sekme filtresi
            if (AktifSekme == "s")
            {
                SorguFiltre += " AND p.Durum=3 AND p.ProjeGizlilik=1 AND (SELECT COUNT(g.GirdiID) FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=p.ProjeID)>0";
            }
            else if (AktifSekme == "c")
            {
                SorguFiltre += " AND p.Durum=3 AND (SELECT COUNT(g.GirdiID) FROM gp_ProjeGirdiler AS g WHERE g.ProjeID=p.ProjeID AND g.Kazanan=1)>0";
            }
            else if (AktifSekme == "p")
            {
                SorguFiltre += " AND p.Durum=2";
            }
            #endregion
            DataTable TabloProjeler = Veritabani.Sorgu_DataTable("SELECT p.ProjeID, p.OlusturulmaTarihi, p.ProjeAdi, p.ProjeAdiSifreli, p.Zamanlama, p.ProjeGizlilik, p.ProjeKisisel, p.ProjeListeBasi, p.ProjeAramaMotoruYok, p.ProjeArkaplan, p.ProjeBold, t.TipAd, p.Odul AS Fiyat FROM gp_Projeler AS p JOIN gp_ProjeTipleri AS t ON p.TipID=t.TipID WHERE p.Durum>1 AND Metin=0 " + SorguFiltre + SorguSiralama, DiziFiltreParametreleri.ToArray());
            //#region Sayfalama oluşturuluyor
            //double SayfaBasinaKayitSayisi = 10;
            //double ToplamKayitSayisi = TabloProjeler.Rows.Count;
            //double SayfalamaAltLimit = (SayfaNo - 1) * SayfaBasinaKayitSayisi;
            //double SayfalamaUstLimit = SayfalamaAltLimit + SayfaBasinaKayitSayisi - 1;
            //double SayfaSayisi = Math.Ceiling(ToplamKayitSayisi / SayfaBasinaKayitSayisi);
            //double OncekiSayfa = SayfaNo - 1;
            //double SonrakiSayfa = (SayfaNo < SayfaSayisi) ? SayfaNo + 1 : 0;
            //if (AktifSekme == "p")
            //{
            //    SayfalamaAltLimit = 0;
            //    SayfalamaUstLimit = 999999999;
            //}
            //#endregion
            #region Proje listesi oluşturuluyor
            string ProjeID = ""; string ProjeAdi = ""; string ProjeAdiSifreli = ""; string ProjeLink = ""; int Zamanlama = 0; string ProjeGizlilik = ""; string ProjeKisisel = ""; string ProjeListeBasi = ""; string ProjeAramaMotoruYok = ""; string ProjeArkaplan = ""; string ProjeBold = ""; string ProjeTipAd = ""; string ProjeFiyat = ""; DateTime ProjeOlusturulmaTarihi; string KalanZamanBilgisi = ""; int TasarimSayisi = 0; string KazananResim = ""; string KazananAdi = ""; string KazananAvatar = ""; string KazananAdiSifreli = ""; string ProjeGirdileri = ""; string ProjeGirdiResim = ""; string BaslikBold = ""; string SatirHighlightedWrapper = ""; string SatirHighlighted = ""; string GizliProjeBilgisi = ""; string LinkNoFollow = "";
            string OddEven = ""; DataTable TabloKazanan; DataTable TabloGirdiler;
            #region Liste başı
            switch (AktifSekme)
            {
                case "p":
                    ListeProjeler += @"
                        <br /><br /><br />
                        <div class=""kutular"">";
                    break;
                case "c":
                    ListeProjeler += @"
                        <br /><br /><br />
                        <div class=""kutular"">";
                    break;
                case "s":
                    ListeProjeler += @"
                        <br /><br /><br />
                        <div class=""kutular"">";
                    break;
            }
            #endregion
            for (int i = 0; i < TabloProjeler.Rows.Count; i++)
            {
                //if (i < SayfalamaAltLimit) continue;
                //else if (i > SayfalamaUstLimit) break;

                ProjeID = TabloProjeler.Rows[i]["ProjeID"].ToString();
                ProjeAdi = TabloProjeler.Rows[i]["ProjeAdi"].ToString();
                ProjeAdiSifreli = TabloProjeler.Rows[i]["ProjeAdiSifreli"].ToString();
                Zamanlama = Convert.ToInt32(TabloProjeler.Rows[i]["Zamanlama"].ToString());
                ProjeGizlilik = TabloProjeler.Rows[i]["ProjeGizlilik"].ToString();
                ProjeKisisel = TabloProjeler.Rows[i]["ProjeKisisel"].ToString();
                ProjeListeBasi = TabloProjeler.Rows[i]["ProjeListeBasi"].ToString();
                ProjeAramaMotoruYok = TabloProjeler.Rows[i]["ProjeAramaMotoruYok"].ToString();
                ProjeArkaplan = TabloProjeler.Rows[i]["ProjeArkaplan"].ToString();
                ProjeBold = TabloProjeler.Rows[i]["ProjeBold"].ToString();
                if (ProjeAdi.Length >= 18 && ProjeBold=="1")
                {
                    ProjeAdi = ProjeAdi.Substring(0, 18) + "...";
                }
                else if (ProjeAdi.Length >= 25)
                {
                    ProjeAdi = ProjeAdi.Substring(0, 25) + "...";
                }
                ProjeTipAd = TabloProjeler.Rows[i]["TipAd"].ToString();
                ProjeFiyat = TabloProjeler.Rows[i]["Fiyat"].ToString();
                ProjeOlusturulmaTarihi = Convert.ToDateTime(TabloProjeler.Rows[i]["OlusturulmaTarihi"].ToString());
                KalanZamanBilgisi = KalanZamanHesapla(ProjeOlusturulmaTarihi, Zamanlama);
                ProjeLink = "proje.aspx?p=" + ProjeAdiSifreli;
                LinkNoFollow = (ProjeAramaMotoruYok == "1") ? @"rel=""nofollow""" : "";
                ProjeGirdileri = "";
                //Proje satırı formatlanıyor
                if (AktifSekme == "p")
                {
                    BaslikBold = (ProjeBold == "1") ? @"projeler_bold" : "";
                    //SatirHighlightedWrapper = (ProjeArkaplan == "1") ? @"class=""highlighted_wrapper""" : "";
                    SatirHighlighted = (ProjeArkaplan == "1") ? @"proje_arkaplan" : "";
                    GizliProjeBilgisi = (ProjeGizlilik == "1") ? "<strong>Gizli proje</strong> - " : "";
                    
                }
                //Katılan ve kazanan projeler tespit ediliyor
                if (AktifSekme == "c")
                {
                    TasarimSayisi = Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT COUNT(GirdiID) FROM gp_ProjeGirdiler WHERE ProjeID=@ProjeID", ProjeID));
                    TabloKazanan = Veritabani.Sorgu_DataTable("SELECT u.KullaniciAdi, u.Avatar, u.KullaniciAdiSifreli, g.Resim FROM gp_ProjeGirdiler AS g JOIN gp_Uyeler AS u ON g.UyeID=u.UyeID WHERE ProjeID=@ProjeID AND Kazanan=1", ProjeID);
                    KazananResim = TabloKazanan.Rows[0]["Resim"].ToString();
                    KazananAdi = TabloKazanan.Rows[0]["KullaniciAdi"].ToString();

                    KazananAvatar = TabloKazanan.Rows[0]["Avatar"].ToString();
                    if (KazananAvatar == "")
                    {
                        KazananAvatar = "logo.png";
                    }
                    KazananAdiSifreli = TabloKazanan.Rows[0]["KullaniciAdiSifreli"].ToString();
                    TabloGirdiler = Veritabani.Sorgu_DataTable("SELECT TOP(5) Resim FROM gp_ProjeGirdiler WHERE ProjeID=@ProjeID AND Kazanan=0 ORDER BY Favori DESC, Tarih DESC", ProjeID);
                    for (int r = 0; r < TabloGirdiler.Rows.Count; r++)
                    {
                        ProjeGirdiResim = TabloGirdiler.Rows[r]["Resim"].ToString();
                        ProjeGirdileri += @"
				            <div class=""kutu2"">
					            <img alt="""" width=""120"" height=""90"" src=""images/contest_entries/120/" + ProjeGirdiResim + @""" />
				            </div>";
                    }
                }

                //İsim kısaysa, fiyat bilgisinin sağ alta denk gelmesi için ismi uzatıyoruz. [2015.12.05]
                if (KazananAdi.Length < 15) KazananAdi += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                //Resmi olmayan kayıtlar için varsatyılan resim ayarlanıyor.
                if (KazananResim == "") KazananResim = "logo.png";
                if (KazananAvatar == "") KazananAvatar = "logo.png";

                switch (AktifSekme)
                {
                    case "p":
                        OddEven = (i % 2 == 0) ? "odd" : "even";
                        #region Devam eden projeler
                        /*
                        ListeProjeler += @"
                            <li class=""" + OddEven + @""">
	                            <div " + SatirHighlightedWrapper + @">
		                            <div " + SatirHighlighted + @">
			                            <div class=""info"">
				                            <ul class=""info-list"">
					                            <li class=""time"">" + KalanZamanBilgisi + @" kaldı</li>
					                            <li>" + TasarimSayisi.ToString() + @" tasarım</li>
					                            <li class=""price"">" + ProjeFiyat + @" TL</li>
				                            </ul>
			                            </div>
			                            <h2 " + BaslikBold + @">
				                            <a href=""" + ProjeLink + @""" " + LinkNoFollow + @">" + ProjeAdi + @"</a>
			                            </h2>
			                            <p>" + GizliProjeBilgisi + ProjeTipAd + @"</p>
		                            </div>
	                            </div>
                            </li>";
                        */
                        DataTable SlaytDt;
                        string SlaytResim; string ResimDiv=""; string ResimDiv2 = "";
                        SlaytDt = Veritabani.Sorgu_DataTable("SELECT TOP 5 g.GirdiID, g.GirdiNo, g.GirdiTip, g.Baslik, g.Resim, g.Video, g.Favori, g.Kazanan, p.ProjeAdiSifreli, u.KullaniciAdi, u.KullaniciAdiSifreli, u.Avatar, (SELECT COUNT(y.YorumID) FROM gp_Yorumlar AS y WHERE Sistem=0 AND y.GirdiID=g.GirdiID) AS YorumSayisi FROM gp_ProjeGirdiler AS g JOIN gp_Projeler AS p ON g.ProjeID=p.ProjeID JOIN gp_Uyeler AS u ON g.UyeID=u.UyeID WHERE g.ProjeID=@ProjeID order by GirdiID desc",ProjeID);
                        int Sinir = SlaytDt.Rows.Count;
                        if (Sinir < 5) Sinir = 5;
                        for (int q = 0; q < Sinir; q++)
                        {
                            if (q < SlaytDt.Rows.Count)
                            {
                                SlaytResim = SlaytDt.Rows[q]["Resim"].ToString();
                                ResimDiv += @"<img style=""width:180px; height:140px; cursor:pointer;"" onclick=""document.location='" + ProjeLink + @"';"" src=""images/contest_entries/300/" + SlaytResim + @""">";
                            }
                            else
                            {
                                ResimDiv += @"<img style=""width:180px; height:140px; cursor:pointer;"" onclick=""document.location='" + ProjeLink + @"';"" src=""images/contest_entries/300/l_logo.jpg"">";
                            }
                            //if (SlaytDt.Rows.Count < 5)
                            //{
                                
                            //    ResimDiv2 += @"<img style=""width:180px; height:140px; cursor:pointer;"" onclick=""document.location='" + ProjeLink + @"';"" src=""images/contest_entries/300/l_logo.jpg"">";
                        
                            //+ResimDiv2}
                        }

                        ListeProjeler += @"
                            <div class=""kutu "+SatirHighlighted+@""">
                                <div style=""width:180px; height:141px;"">                                 
                                    <div class=""slides2"">
                                        "+ResimDiv+@"
                                    </div>
                                </div>

                                <img style="" display:none; width:180px; height:140px; cursor:pointer;"" src=""images/contest_entries/300/" + KazananResim + @""" onclick=""document.location='" + ProjeLink + @"';"" />

                                <a class=""" + BaslikBold + @""" href= """ + ProjeLink + @""" style=""cursor:pointer;"" " + LinkNoFollow + ">" + ProjeAdi + @"</a>
                         
                              <div class=""clear""></div>
                                <br />
                                <div class=""tasarimci_mini_img"">
                                      <img src=""images/avatars/60/" + KazananAvatar + @""" style=""width:40px; height:40px;"" />
                                </div>
                                <div class=""tasarimci_sag"" style=""float:none;"">
                                    <a href=""tasarimci.aspx?a=" + KazananAdiSifreli + @""">" + KazananAdi + @"  </a>
                                    <a href="""" style=""float: right;margin-left: 10px;margin-top: -3px;"">" + ProjeFiyat + @" TL</a>
                                </div>
                                <div class=""tasarimci_sag"">
                                    Kalan süre: " + KalanZamanBilgisi + @"
                                </div>
                            </div>";
                        #endregion
                        break;
                    case "s":
                        #region Gizli projeler
                        /*
                        ListeProjeler += @"
	                        <li>
		                        <div class=""project_info"">
			                        <h2><a href=""" + ProjeLink + @""" " + LinkNoFollow + @">" + ProjeAdi + @"</a></h2>
			                        <p>" + ProjeTipAd + @"</p>
			                        <div class=""detail"">
				                        <span>
					                        <strong>Proje süresi:</strong> " + Zamanlama.ToString() + @" gün
				                        </span>
				                        <span>
					                        <strong>Ödül:</strong> " + ProjeFiyat + @" TL
				                        </span>
				                        <span>
					                        <strong>Toplam " + TasarimSayisi.ToString() + @" tasarım</strong>
				                        </span>
				                        <span class=""last"">
					                        <strong>Kazanan tasarımcı:</strong>
					                        <a href=""tasarimci.aspx?a=" + KazananAdiSifreli + @""">" + KazananAdi + @"</a>
				                        </span>
			                        </div>
		                        </div>
		                        <div class=""clear""></div>
	                        </li>";
                        */
                        ListeProjeler += @"
                        <div class=""kutu"">
                          <img style=""width:180px; height:140px; cursor:pointer;"" src=""images/contest_entries/300/" + KazananResim + @""" onclick=""document.location='" + ProjeLink + @"';"" />
                          <a href=""" + ProjeLink + @""" " + LinkNoFollow + @">" + ProjeAdi + @"</a>
                          <div class=""clear""></div>
                            <br />
                            <div class=""tasarimci_mini_img"">
                                  <img src=""images/avatars/60/" + KazananAvatar + @""" style=""width:40px; height:40px;"" />
                            </div>
                            <div class=""tasarimci_sag"" style=""float:none;"">
                                <a href=""tasarimci.aspx?a=" + KazananAdiSifreli + @""">" + KazananAdi + @"  </a>
                                <a href="""" style=""float: right;margin-left: 10px;margin-top: -3px;"">" + ProjeFiyat + @" TL</a>
                            </div>
                        </div>";
                        #endregion
                        break;
                    case "c":
                        #region Tamamlanan projeler
                        ListeProjeler += @"
                        <div class=""kutu"">
                          <img style=""width:180px; height:140px; cursor:pointer;"" src=""images/contest_entries/300/" + KazananResim + @""" onclick=""document.location='" + ProjeLink + @"';"" />
                          <a href=""" + ProjeLink + @""" " + LinkNoFollow + @">" + ProjeAdi + @"</a>
                          <div class=""clear""></div>
                            <br />
                            <div class=""tasarimci_mini_img"">
                                  <img src=""images/avatars/60/" + KazananAvatar + @""" style=""width:40px; height:40px;"" />
                            </div>
                            <div class=""tasarimci_sag"" style=""float:none;"">
                                <a href=""tasarimci.aspx?a=" + KazananAdiSifreli + @""">" + KazananAdi + @"  </a>
                                <a href="""" style=""float: right;margin-left: 10px;margin-top: -3px;"">" + ProjeFiyat + @" TL</a>
                            </div>
                        </div>";

                        #endregion
                        break;
                }
            }
            #region Liste sonu
            ListeProjeler += @"</div>";
            #endregion
            #endregion
//            #region Sayfa numaraları oluşturuluyor
//            string OncekiSayfaDisabled = (OncekiSayfa == 0) ? " disabled" : "";
//            string SonrakiSayfaDisabled = (SonrakiSayfa == 0) ? " disabled" : "";
//            ListeSayfaNumaralari += @"
//        <a class=""previous_page " + OncekiSayfaDisabled + @""" onclick=""sayfaDegistir('" + OncekiSayfa.ToString() + @"');"" href=""javascript:void(0);"" rel=""prev"">&laquo; Önceki</a> ";
//            string SayfaRel = "";
//            for (int s = 1; s <= SayfaSayisi; s++)
//            {
//                SayfaRel = "";
//                if (s == SayfaNo)
//                    ListeSayfaNumaralari += @"
//        <em class=""current"">" + s.ToString() + @"</em> ";
//                else if (s == 3 && SayfaNo > 9) //Sol boşluk
//                    ListeSayfaNumaralari += @"
//        <span class=""gap"">&hellip;</span> ";
//                else if (s == SayfaSayisi - 3 && SayfaNo < SayfaSayisi - 9) //Sağ boşluk
//                    ListeSayfaNumaralari += @"
//        <span class=""gap"">&hellip;</span> ";
//                else if (s <= 2 || s >= SayfaSayisi - 1 || (s > SayfaNo - 6 && s < SayfaNo + 5))
//                {
//                    if (s == SayfaNo - 1) SayfaRel = @"rel=""prev""";
//                    else if (s == SayfaNo + 1) SayfaRel = @"rel=""next""";
//                    ListeSayfaNumaralari += @"
//        <a " + SayfaRel + @" onclick=""sayfaDegistir('" + s.ToString() + @"');"" href=""javascript:void(0);"">" + s.ToString() + @"</a> ";
//                }
//            }
//            ListeSayfaNumaralari += @"
//        <a class=""next_page " + SonrakiSayfaDisabled + @""" rel=""next"" onclick=""sayfaDegistir('" + SonrakiSayfa.ToString() + @"');"" href=""javascript:void(0);"">Sonraki &raquo;</a>";
//            #endregion

            lblListeProjeler.Text = ListeProjeler;
            lblSayfaNumaralari.Text = (AktifSekme != "p") ? ListeSayfaNumaralari : "";
        }

        public string KalanZamanHesapla(DateTime OlusturulmaTarihi, int ProjeSuresi)
        {
            string Sonuc = "";
            TimeSpan TarihFarki = OlusturulmaTarihi.AddDays(ProjeSuresi) - DateTime.Now;
            double SaatFarki = Math.Round(TarihFarki.TotalHours);
            if (SaatFarki > 24)
            {
                Sonuc = Math.Floor(SaatFarki / 24).ToString() + " gün";
            }
            else
            {
                Sonuc = Math.Round(SaatFarki).ToString() + " saat";  //"yaklaşık "+ vardı kaldırıldı [11.01.2015]
            }
            return Sonuc;
        }

    }
}
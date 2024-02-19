using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace GrafikerPortal
{
    public partial class uye_ol : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;

        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();
            divErrorLogin.Visible = false;
            divErrorSignup.Visible = false;

            //////////////////////////////
            string GeciciTip2 = Session["ProjeTipID"] != null ? Session["ProjeTipID"].ToString() : "";
            if (GeciciTip2 != "") hfUyeOlmadanDevamEt.Value = GeciciTip2;
            //Response.Write("Session:"+GeciciTip2);

            if (hfUyeOlmadanDevamEt.Value == "1")
            {
                btnUyeOlmadanDevam_Click(null, null);
                return;
            }


            string GelinenYer = !string.IsNullOrEmpty(Request.ServerVariables["HTTP_REFERER"]) ? Request.ServerVariables["HTTP_REFERER"].ToString() : "";
            if (GelinenYer != "" && GelinenYer.IndexOf("uye_ol.aspx") < 0)
                hfGelinenYer.Value = GelinenYer;
                //Session["GelinenYer"] = GelinenYer;

            string ZorunluUyelik = !string.IsNullOrEmpty(Request.QueryString["fm"]) ? Request.QueryString["fm"].ToString() : "";
            //if (hfGelinenYer.Value.IndexOf("yeni_proje_2.aspx") >= 0)
            if(ZorunluUyelik == "1")
            {
                btnUyeOlmadanDevam.Style["display"] = "none";
                btnUyeOl.Style["margin-left"] = "0px";
                hfGelinenYer.Value = "";
            }

            string SignupKullaniciAdi = ""; string SignupSifre = ""; string SignupEposta = ""; string SignupTelefon = ""; string SignupUyelikTipi = ""; string SignupSozlesme = "";
            string LoginKullaniciAdi = ""; string LoginSifre = ""; string LoginBeniHatirla = "";
            if (!string.IsNullOrEmpty(Request["accesskeydisabled"]))
            {
                string Aksiyon = (!string.IsNullOrEmpty(Request["action_type"])) ? Request["action_type"].ToString() : "";
                if (Aksiyon == "l")
                {
                    #region Üye girişi
                    LoginKullaniciAdi = (!string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$user_email_login"])) ? Request["ctl00$ContentPlaceHolder1$user_email_login"].ToString().Trim() : "";
                    LoginSifre = (!string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$user_password_login"])) ? Request["ctl00$ContentPlaceHolder1$user_password_login"].ToString().Trim() : "";
                    LoginBeniHatirla = (!string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$user_remember_me"])) ? "1" : "";

                    string LoginUyeID = Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE (KullaniciAdi=@KullaniciAdi OR Eposta=@Eposta) AND Sifre=@Sifre", LoginKullaniciAdi, LoginKullaniciAdi, LoginSifre).ToString();
                    if (LoginUyeID != "")
                    {
                        //Beni hatırla işaretlenmişse cookie ataması yapılıyor
                        if (LoginBeniHatirla == "1")
                        {
                            HttpCookie uyeCerez = new HttpCookie("gpUyelik");
                            uyeCerez.Values.Add("uyeid", LoginUyeID);
                            uyeCerez.Expires = DateTime.Now.AddMonths(1); //1 aylık ömür biçiliyor.
                            Response.Cookies.Add(uyeCerez);
                        }

                        //Üye tipi tespit ediliyor
                        string LoginUyeTipi = Veritabani.Sorgu_Scalar("SELECT TOP(1) UyelikTipi FROM gp_Uyeler WHERE UyeID=@UyeID", LoginUyeID).ToString();
                        //Session kaydı gerçekleştiriliyor.
                        Session["UyeID"] = LoginUyeID;
                        Session["UyeTipi"] = LoginUyeTipi;
                        Session["KullaniciAdi"] = Veritabani.Sorgu_Scalar("SELECT TOP(1) KullaniciAdi FROM gp_Uyeler WHERE UyeID=@UyeID", LoginUyeID).ToString();
                        Session["KullaniciAdiSifreli"] = Veritabani.Sorgu_Scalar("SELECT TOP(1) KullaniciAdiSifreli FROM gp_Uyeler WHERE UyeID=@UyeID", LoginUyeID).ToString();
                        //Kullanıcı üye sayfasına yönlendiriliyor
                        //string SessionGelinenYer = Session["GelinenYer"] != null ? Session["GelinenYer"].ToString() : ""; 
                        string SessionGelinenYer = !string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$hfGelinenYer"]) ? Request["ctl00$ContentPlaceHolder1$hfGelinenYer"].ToString() : "";
                        if (SessionGelinenYer != "")
                        {
                            Response.Redirect(SessionGelinenYer);
                        }
                        else if (LoginUyeTipi == "1") //Müşteri
                        {
                            string GeciciProje = Session["ProjeID"] != null ? "?p=" + Session["ProjeID"].ToString() : "";
                            string GeciciTip = Session["ProjeTipID"] != null ? "?t=" + Session["ProjeTipID"].ToString() : "";
                            if (GeciciProje != "")
                            {
                                Veritabani.Sorgu_Scalar("UPDATE gp_Projeler SET UyeID=@UyeID WHERE ProjeID=@ProjeID", LoginUyeID, Session["ProjeID"].ToString());
                                Response.Redirect("yeni_marka_3.aspx" + GeciciProje);
                            }
                            else if (GeciciTip != "") Response.Redirect("yeni_marka.aspx" + GeciciTip);
                            else Response.Redirect("yeni_marka.aspx");
                        }
                        else //Tasarımcı
                        {
                            Response.Redirect("default.aspx");
                            //Response.Redirect("profil_portfolyo.aspx");
                        }
                    }
                    else
                    {
                        divErrorLogin.Visible = true;
                    }

                    user_email_login.Value = LoginKullaniciAdi;
                    #endregion
                }
                else if(Aksiyon == "uode"){
                    btnUyeOlmadanDevam_Click(null, null);
                }
                else
                {
                    #region Yeni kayıt
                    SignupKullaniciAdi = (!string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$user_name_signup"])) ? Request["ctl00$ContentPlaceHolder1$user_name_signup"].ToString().Trim() : "";
                    SignupSifre = (!string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$user_password_signup"])) ? Request["ctl00$ContentPlaceHolder1$user_password_signup"].ToString().Trim() : "";
                    SignupEposta = (!string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$user_email_signup"])) ? Request["ctl00$ContentPlaceHolder1$user_email_signup"].ToString().Trim() : "";
                    SignupTelefon = (!string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$user_profile_attributes_phone"])) ? Request["ctl00$ContentPlaceHolder1$user_profile_attributes_phone"].ToString().Trim() : "";
                    SignupUyelikTipi = (!string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$user_type"])) ? Request["ctl00$ContentPlaceHolder1$user_type"].ToString() : "";
                    if (SignupUyelikTipi != "customer" && SignupUyelikTipi != "creative") SignupUyelikTipi = "";
                    if (SignupUyelikTipi == "customer") SignupUyelikTipi = "1";
                    else if (SignupUyelikTipi == "creative") SignupUyelikTipi = "2";
                    SignupSozlesme = (!string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$user_terms_of_service"])) ? "1" : "";
                    //Bilgiler kontrol ediliyor
                    divErrorSignup.Visible = false;
                    if (SignupKullaniciAdi.Length == 0)
                    {
                        divErrorSignup.Visible = true;
                        liSignupKullaniciAdi.Visible = true;
                    }
                    else if (!KullaniciAdiGecerli(SignupKullaniciAdi))
                    {
                        divErrorSignup.Visible = true;
                        liSignupKullaniciAdiFormat.Visible = true;
                    }
                    else if (Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT COUNT(UyeID) FROM gp_Uyeler WHERE KullaniciAdi=@KullaniciAdi", SignupKullaniciAdi)) > 0)
                    {
                        divErrorSignup.Visible = true;
                        liSignupKullaniciAdiKullanimda.Visible = true;
                    }
                    if (SignupEposta.Length == 0)
                    {
                        divErrorSignup.Visible = true;
                        liSignupEposta.Visible = true;
                    }
                    else if (!new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Match(SignupEposta).Success)
                    {
                        divErrorSignup.Visible = true;
                        liSignupEpostaFormat.Visible = true;
                    }
                    else if (Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT COUNT(UyeID) FROM gp_Uyeler WHERE Eposta=@Eposta", SignupEposta)) > 0)
                    {
                        divErrorSignup.Visible = true;
                        liSignupEpostaKullanimda.Visible = true;
                    }
                    if (SignupSifre.Length == 0)
                    {
                        divErrorSignup.Visible = true;
                        liSignupSifre.Visible = true;
                    }
                    else if (SignupSifre.Length < 6 || SignupSifre.Length > 32)
                    {
                        divErrorSignup.Visible = true;
                        liSignupSifreFormat.Visible = true;
                    }
                    if (SignupSozlesme != "1")
                    {
                        divErrorSignup.Visible = true;
                        liSignupSozlesme.Visible = true;
                    }
                    if (SignupUyelikTipi.Length == 0)
                    {
                        divErrorSignup.Visible = true;
                        liSignupKullaniciTipi.Visible = true;
                    }

                    //Hata mesajı alınmamışsa kayıt gerçekleştiriliyor
                    if(!divErrorSignup.Visible)
                    {
                        string KullaniciAdiSifreli = AletKutusu.KelimeSifrele(SignupKullaniciAdi);
                        //Kayıt işlemi gerçekleştiriliyor
                        int YeniKayitID = Veritabani.Sorgu_Calistir_Eklenen_Id_Dondur("INSERT INTO gp_Uyeler(Tarih, KullaniciAdi, KullaniciAdiSifreli, Sifre, Eposta, Telefon, UyelikTipi) VALUES(@Tarih, @KullaniciAdi, @KullaniciAdiSifreli, @Sifre, @Eposta, @Telefon, @UyelikTipi)", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), SignupKullaniciAdi, KullaniciAdiSifreli, SignupSifre, SignupEposta, SignupTelefon, SignupUyelikTipi);  //dd.MM.yyyy HH:mm:ss eski tarih formatı buydu....17.02.2014.....
                        if (YeniKayitID > 0)
                        {
                            //Session kaydı gerçekleştiriliyor.
                            Session["UyeID"] = YeniKayitID;
                            Session["UyeTipi"] = SignupUyelikTipi;
                            Session["KullaniciAdi"] = SignupKullaniciAdi;
                            //Kullanıcı üye sayfasına yönlendiriliyor
                            if (SignupUyelikTipi == "1") //Müşteri
                            {
                                //Yeni üye kaydı e-postası gönderiliyor.
                                string MailKonu = "Yeni Üye Kaydı.";
                                string MailIcerik = "' "+Session["KullaniciAdi"].ToString() + " ' isimli üyelik oluşturuldu.\r\n\r\n MarKa Kafa Yöneticilerinin üyelik onayından sonra siteyi tam anlamıyla kullanabileceksin.\r\n\r\n\r\n MarKa Kafa ' yı tercih ettiğin için teşekkürler. " + DateTime.Now;
                                new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                                new Mail().MailGonder(Server,"",SignupEposta,MailKonu,MailIcerik);

                                string GeciciProje = Session["ProjeID"] != null ? Session["ProjeID"].ToString() : "";
                                string GeciciTip = Session["ProjeTipID"] != null ? Session["ProjeTipID"].ToString() : "";
                                if (GeciciProje != "")
                                {
                                    //Geçici proje, üyeye bağlanıyor
                                    Veritabani.Sorgu_Calistir("UPDATE gp_Projeler SET UyeID=@UyeID WHERE ProjeID=@ProjeID", YeniKayitID.ToString(), GeciciProje);
                                    Response.Redirect("yeni_marka_3.aspx?p=" + GeciciProje);
                                }
                                else if (GeciciTip != "") Response.Redirect("yeni_marka.aspx?t=" + GeciciTip);
                                else Response.Redirect("yeni_marka.aspx");
                            }
                            else //Tasarımcı
                            {
                                //Yeni üye kaydı e-postası gönderiliyor.
                                string MailKonu = "Yeni Üye Kaydı.";
                                string MailIcerik = "' "+Session["KullaniciAdi"].ToString() + " ' isimli üyelik oluşturuldu.\n\r\n\rMarKa Kafa Yöneticilerinin üyelik onayından sonra siteyi tam anlamıyla kullanabileceksin.\n\r\n\r\n\rMarKa Kafa ' yı tercih ettiğin için teşekkürler. " + DateTime.Now;
                                new Mail().MailGonder(Server, "", "bildirim@markakafa.com", MailKonu, MailIcerik);
                                new Mail().MailGonder(Server, "", SignupEposta, MailKonu, MailIcerik);

                                Response.Redirect("profil.aspx");
                            }

                            SignupKullaniciAdi = ""; SignupSifre = ""; SignupEposta = ""; SignupTelefon = ""; SignupUyelikTipi = ""; SignupSozlesme = "";
                        }

                        divErrorSignup.Visible = false;

                    }

                    user_name_signup.Value = SignupKullaniciAdi;
                    user_email_signup.Value = SignupEposta;
                    user_profile_attributes_phone.Value = SignupTelefon;
                    if (SignupUyelikTipi == "1") user_type_customer.Checked = true;
                    else if (SignupUyelikTipi == "2") user_type_creative.Checked = true;

                    #endregion
                }
            }
        }

        public bool KullaniciAdiGecerli(string KullaniciAdi)
        {
            bool Sonuc = true;
            if (!IsAlphaNumeric(KullaniciAdi)) Sonuc = false;
            return Sonuc;
        }

        public bool IsAlphaNumeric(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            return (str.ToCharArray().All(c => Char.IsLetter(c) || Char.IsNumber(c) || Char.IsWhiteSpace(c)));
        }

        public void btnUyeOlmadanDevam_Click(object sender, EventArgs e)
        {
            Session["geciciuye"] = 999999;
            //string GeciciTip = Session["ProjeTipID"] != null ? "?t="+Session["ProjeTipID"].ToString() : "";
            string GeciciTip = hfUyeOlmadanDevamEt.Value != "" ? "?t=" + hfUyeOlmadanDevamEt.Value : "";
            //if( hfUyeOlmadanDevamEt.Value != ""

            Response.Redirect("yeni_marka_1.aspx" + GeciciTip);
        }

    }
}
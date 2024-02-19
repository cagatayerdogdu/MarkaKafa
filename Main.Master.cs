using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafikerPortal
{
    public partial class Main : System.Web.UI.MasterPage
    {
        DAL Veritabani;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL();

            string AyarlarSorgu = Veritabani.Sorgu_Scalar(@"SELECT SagClick FROM Ayarlar");
            SagClick.Value = AyarlarSorgu;


            #region Beni hatırla işaretlenmişse bilgiler çerezden okunuyor
            if (Session["UyeID"] == null)
            {
                HttpCookie gpCerez = Request.Cookies["gpUyelik"];
                if (gpCerez != null)
                {
                    if (!string.IsNullOrEmpty(gpCerez.Values["uyeid"]))
                    {
                        string CerezUyeID = gpCerez.Values["uyeid"].ToString();
                        //Çerezde tutulan üye ID'si veritabanında kayıtlı değilse çerez sıfırlanıyor.
                        if (Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyeID=@UyeID", CerezUyeID) == "")
                        {
                            gpCerez.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(gpCerez);
                        }
                        else //Çerezdeki veri geçerli ise kullanıcı bilgileri session'a atanıyor.
                        {
                            Session["UyeID"] = CerezUyeID;
                            Session["UyeTipi"] = Veritabani.Sorgu_Scalar("SELECT TOP(1) UyelikTipi FROM gp_Uyeler WHERE UyeID=@UyeID", CerezUyeID);
                            Session["KullaniciAdi"] = Veritabani.Sorgu_Scalar("SELECT TOP(1) KullaniciAdi FROM gp_Uyeler WHERE UyeID=@UyeID", CerezUyeID);
                        }
                    }
                }
            }
            #endregion

            string SessionUyeID = (Session["UyeID"] != null) ? Session["UyeID"].ToString() : "";
            string SessionUyeTipi = (Session["UyeTipi"] != null) ? Session["UyeTipi"].ToString() : "";
            string SessionKullaniciAdi = (Session["KullaniciAdi"] != null) ? Session["KullaniciAdi"].ToString() : "";
            string SessionKullaniciAdiSifreli = (Session["KullaniciAdiSifreli"] != null) ? Session["KullaniciAdiSifreli"].ToString() : "";
            //Session'da tutulan üye ID'si veritabanında kayıtlı değilse session sıfırlanıyor.
            if (Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyeID=@UyeID", SessionUyeID) == "" && Session["geciciuye"] == null)
            {
                SessionUyeID = "";
                SessionUyeTipi = "";
                Session["UyeID"] = null;
                Session["UyeTipi"] = null;
                Session["KullaniciAdi"] = null;
                Session["KullaniciAdiSifreli"] = null;
                Session.RemoveAll();
                Session.Clear();
            }
            string sayfa = HttpContext.Current.Request.Url.AbsolutePath;
            string kafayorulacakyenimarka_secili = sayfa.Contains("yeni_marka.aspx") ? "active" : "";
            string markabedeli_secili = sayfa.Contains("marka_bedeli.aspx") ? "active" : "";
            string markakafalar_secili = sayfa.Contains("projeler.aspx") ? "active" : "";
            string markakolikler_secili = sayfa.Contains("tasarimcilar.aspx") ? "active" : "";
            string markakafanedir_secili = sayfa.Contains("markakafa_nedir.aspx") ? "active" : "";
            string portfolyo_secili = sayfa.Contains("profil_portfolyo.aspx") ? "active" : "";
            string markalarim = sayfa.Contains("katildigi_markalar_tasarimci.aspx") ? "active" : "";
            string marka_sayfam = sayfa.Contains("marka_kafa.aspx") ? "active" : "";
            string marka_kolikler = sayfa.Contains("tasarimcilar.aspx") ? "active" : "";
            string markalarim_sahip = sayfa.Contains("projelerim_sahip.aspx") ? "active" : "";
            lblMenu.Text = @"
       <ul class=""menu_1"">
         <a href=""yeni_marka.aspx"" style="" height:30px;""> <li class=""" + kafayorulacakyenimarka_secili + @""">   marka projeler   </li></a>
         <li class=""aralik"">  </li>
        <!-- <a href=""marka_bedeli.aspx"" style=""""> <li class=""" + markabedeli_secili + @""">  marka bedeli   </li> 	</a>
          <li class=""aralik"">  </li> -->
          <a href=""projeler.aspx"" style="""">  <li class=""" + markakafalar_secili + @"""> kafa yorulacak yeni marka   </li> </a> 	
         <li class=""aralik"">  </li>
         <a href=""tasarimcilar.aspx"" style=""""> <li class=""" + markakolikler_secili + @""">  marka kafalar   </li> </a>
         <li class=""aralik"">  </li>
         <a href=""markakafa_nedir.aspx"" style=""""> <li class=""" + markakafanedir_secili + @"""> markakafa nedir?   </li></a>
        </ul>";
            #region üye girişi yapılmışsa menü değiştiriliyor
            if (SessionUyeID != "") //Üye girişi yapılmışsa
            {
                if (SessionUyeTipi == "1") //Müşteri
                {
                    lblMenu.Text = @"
    <ul class=""menu_1"" style=""margin-left:410px;"">
        <a href=""projelerim_sahip.aspx"" style="" height:30px;""> <li class=""" + markalarim_sahip + @"""> markalarım     </li></a>        
          <li class=""aralik"">  </li>
          <a href=""marka_kafa.aspx"" style="""">  <li class=""" + marka_sayfam + @"""> marka sayfam   </li> </a>
          <li class=""aralik"">  </li>
          <a href=""projeler.aspx"" style="""">  <li class=""" + markakafalar_secili + @"""> kafa yorulacak yeni marka   </li> </a> 
         <li class=""aralik"">  </li>
         <a href=""tasarimcilar.aspx"" style=""""> <li class=""" + marka_kolikler + @""">  marka kafalar   </li> </a>
</ul>";

                }
                else //Tasarımcı
                {
                    lblMenu.Text = @"
    <ul class=""menu_1"" style=""margin-left:310px;"">
        <a href=""katildigi_markalar_tasarimci.aspx?a=" + SessionKullaniciAdiSifreli+@""" style="" height:30px;""> <li class="""+ markalarim +@"""> markalarım     </li></a>
         <li class=""aralik"">  </li>
         <a href=""profil_portfolyo.aspx"" style=""""> <li class="""+ portfolyo_secili +@""">  portfolyo   </li> 	</a>
          <li class=""aralik"">  </li>
          <a href=""marka_kafa.aspx"" style="""">  <li class=""" + marka_sayfam + @"""> marka sayfam   </li> </a>
          <li class=""aralik"">  </li>
          <a href=""projeler.aspx"" style="""">  <li class=""" + markakafalar_secili + @"""> kafa yorulacak yeni marka   </li> </a> 		
         <li class=""aralik"">  </li>
         <a href=""tasarimcilar.aspx"" style=""""> <li class=""" + marka_kolikler +@""">  marka kafalar   </li> </a>
</ul>";
                }
            }
            #endregion

            string HeaderMenu = "";
            if(SessionUyeID != "") //Üye girişi yapılmışsa
            {
                string avatar = "";

                avatar = Veritabani.Sorgu_Scalar(@"select TOP(1) Avatar from gp_uyeler Where UyeId=@uyeid", SessionUyeID);
                if (avatar=="")
                {
                    avatar = "MarkaKafaAvatar.jpg";
                }

                HeaderMenu = @"
                <table style=""float:right; margin-top:-20px;"">
                    <tr>
                        <td rowspan=""2"" style=""width:80px;"">
                            <div style="" float:left; height:72px; width:77px;"">  
                                <a href=""profil.aspx""> <img src=""images/avatars/60/" + avatar + @""" style="" border-radius:100%; height:60px; margin-left:6px; margin-top:6px; width:61px;"" /></a>
                            </div>
                        </td>
                        <td style=""width:110px; word-break:break-all;"">
                            <a href=""marka_kafa.aspx"" style="" float:left; margin-top:22px; margin-left:10px;""> " + SessionKullaniciAdi + @" </a>
                        </td>
                    </tr>
                    <tr>
                        <td style=""height:20px;"">
                            <a href=""cikis.aspx"" style="" float:right;""> çıkış </a>
                        </td>
                    </tr>
                </table>";
            }
            else
            {
                HeaderMenu = @" 
    <a data-toggle=""modal"" href=""#form-content""> <img src=""img/uye_ol.png"" /> </a>
    <a data-toggle=""modal"" href=""#form-login""> <img src=""img/uye_giris.png"" /></a>

    <input id=""action_type_main"" name=""action_type"" type=""hidden"" value=""l"" />
    <input name=""ctl00$ContentPlaceHolder1$user_terms_of_service"" type=""hidden"" value=""1"" />
    <input name=""accesskeydisabled"" type=""hidden"" value=""1"" />
    <div id=""form-content"" class=""modal hide fade in"" style=""width:336px;display: none; background-color:#67696B; border:0px; -webkit-border-radius: 50px;-moz-border-radius: 50px; border-radius: 50px;"">
		<div class=""modal-header"" style=""border:0px; background-color:#67696B; -webkit-border-radius: 50px;-moz-border-radius: 50px; border-radius: 50px;"">
			<a class=""close"" data-dismiss=""modal"">×</a>
			<img  style=""margin-left:25px;"" src=""img/logo.png"" />
		</div>
		<div>
			<fieldset style=""border:0px;"">
		        <div class=""modal-body"">
		        	<ul class=""nav nav-list1"">
						<li class=""nav-header1"" style=""float:left;"">Kullanıcı adı</li>
						<li><input class=""txt"" value="""" type=""text"" name=""ctl00$ContentPlaceHolder1$user_name_signup"" onkeyup=""loginGonder(event, 'kayit');""></li>
						<div style=""height:10px;"">  </div>
						<li class=""nav-header1"" style=""float:left;"">E-posta</li>
						<li><input class=""txt"" value="""" type=""text"" name=""ctl00$ContentPlaceHolder1$user_email_signup"" onkeyup=""loginGonder(event, 'kayit');""></li>
						<div style=""height:10px;"">  </div>
						<li class=""nav-header1"" style=""float:left;"">Şifre</li>
						<li><input class=""txt"" value="""" type=""password"" name=""ctl00$ContentPlaceHolder1$user_password_signup"" onkeyup=""loginGonder(event, 'kayit');""></li>
						<div style=""height:10px;"">  </div>
						<li class=""nav-header1"" style=""float:left;"">Telefon</li>
						<li><input class=""txt"" value="""" type=""text"" name=""ctl00$ContentPlaceHolder1$user_profile_attributes_phone"" onkeyup=""loginGonder(event, 'kayit');""></li>
					</ul> 
                
					<table style=""margin-top:-20px;"">
						<tr>
							<td style=""padding-left:25px;"">
								<div style=""float:left; padding-top:10px;"">
									<input type=""checkbox"" style=""margin-top:10px"" class=""regular-checkbox"" id=""user_user_type_customer"" name=""ctl00$ContentPlaceHolder1$user_type"" value=""customer"" onclick=""if(this.checked) document.getElementById('user_user_type_creative').checked = false;"">
									<label for=""user_user_type_customer"" style=""margin-top:-10px""></label>
								</div>
							</td>
							<td style=""padding-left:10px;"">
								<p style="" font-family: 'Trebuchet MS', sans-serif;font-style: normal; font-size:12px; font-weight:bold; text-transform: normal;letter-spacing: 0px;line-height: 1.2em;""> markalaşmak istiyorum </p>
							</td>
							<td style=""padding-left:20px;"">
								<div style=""float:left; padding-top:10px;"">
									<input type=""checkbox"" style=""margin-top:10px"" class=""regular-checkbox"" id=""user_user_type_creative"" name=""ctl00$ContentPlaceHolder1$user_type"" value=""creative"" onclick=""if(this.checked) document.getElementById('user_user_type_customer').checked = false;"">
									<label for=""user_user_type_creative"" style=""margin-top:-10px""></label>
								</div>
							</td>
							<td style=""padding-left:10px;"">
								<span style="" font-family: 'Trebuchet MS', sans-serif; font-size:12px;font-style: normal;font-weight:bold; text-transform: normal;letter-spacing: 0px;line-height: 1.2em;"" > markakafa olmak istiyorum </span>
							</td>
						</tr>
					</table>
					
					<p style=""font-size:10px; margin-top:-10px; text-align:center; padding-left:27px;""> 
                            <label>üye olunca markakafa.com <a href=""kullanim_sartlari.aspx"" target=""_blank"">Üyelik sözleşmesini</a> okumuş ve kabul etmiş olursunuz.
                            </label>
                    </p>
					<a style=""cursor:pointer; margin-left:80px"" onclick=""menuUyeOl();"" class=""btn_oval""> üye ol </a>
		        </div>
			</fieldset>
		</div>
	</div>

	<div id=""form-login"" class=""modal hide fade in"" style=""margin-top:185px; display: none; background-color:#67696B; border:0px; width:336px;display: none; background-color:#67696B; border:0px; -webkit-border-radius: 50px;-moz-border-radius: 50px; border-radius: 50px;"">
		<div class=""modal-header"" style=""border:0px; background-color:#67696B; border:0px; width:336px;display: none; background-color:#67696B; border:0px; -webkit-border-radius: 50px;-moz-border-radius: 50px; border-radius: 50px;"">			
			<img  style=""margin-left:83px;"" src=""img/logo.png"" />
		</div>
		<div>
			<fieldset style=""border:0px;"">
				<div class=""modal-body"">
                    <a class=""close"" data-dismiss=""modal"">×</a>
					<ul class=""nav nav-list1"">
						<li class=""nav-header1 fonts"" style=""float:left;"">Kullanıcı adı</li>
						<li><input class=""txt"" value="""" type=""text"" name=""ctl00$ContentPlaceHolder1$user_email_login"" onkeyup=""loginGonder(event, 'giris');""></li>
						<div style=""height:10px;"">  </div>
						<li class=""nav-header1"" style=""float:left;"">Şifre</li>
						<li><input class=""txt"" value="""" type=""password"" name=""ctl00$ContentPlaceHolder1$user_password_login"" onkeyup=""loginGonder(event, 'giris');""></li>
 <div class='forgot_pass'>
                       <br /> 
                            <a href=""sifremi_unuttum.aspx"" rel=""nofollow"">Şifremi unuttum</a>
                      </div>
					</ul> 
					<a style=""cursor:pointer; margin-left:80px"" onclick=""document.getElementById('frmMainForm').submit();"" class=""btn_oval"" style=""margin-left:80px;"" > üye girişi </a>
				</div>
			</fieldset>
		</div>
	</div>";

            }

            lblUyeGiris.Text = HeaderMenu;
        }
    }
}

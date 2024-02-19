<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="uye_ol.aspx.cs" Inherits="GrafikerPortal.uye_ol" EnableViewState="false" ViewStateEncryptionMode="Never" ViewStateMode="Disabled" EnableViewStateMac="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .formSatir
    {
        text-align:left;
        /*width:300px;*/
       padding-left:100px;
        height:60px;
    }
    li
    {
        display:block;
    }
    .uye_olmadan_devam_buton:hover
    {        
        color: #FFF !important;
        background-color:#a7a9ac !important;
    }
</style>

    

    <script type="text/javascript">
        function loginEnterKontrol(e) {
            var olay = document.all ? window.event : e;
            var tus = document.all ? olay.keyCode : olay.which;
            if (tus == 13) {
                document.getElementById('btnUyeGirisi').click();
                return false;
            }
            else return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form runat="server" id="frmForm" action="uye_ol.aspx" method="post">
<input type="hidden" name="accesskeydisabled" value="1" />
<input runat="server" type="hidden" id="hfGelinenYer" name="hfGelinenYer" clientidmode="Static" value="" />
<input runat="server" type="hidden" id="hfUyeOlmadanDevamEt" name="hfUyeOlmadanDevamEt" clientidmode="Static" value="" />

<div id="container">
<section id='sign_up'>
    <div class='container_12'>
        
        <div class='grid_6'>
        <div class='form_container'>
        <div class='form' style="display: inline-block; width:400px;">
            <h1>Üye Ol</h1>
            <div runat="server" id="divErrorSignup" class="error">
                <p>
                    <strong>
                        Lütfen aşağıdaki hataları düzeltiniz:
                    </strong>
                </p>
                <div>
                    <ul style="text-align:center; background-color:#505050;">
                        <li runat="server" id="liSignupKullaniciAdi" class="message" visible="false">
                            Kullanıcı adı doldurulmalı.
                        </li>
                        <li runat="server" id="liSignupKullaniciAdiFormat" class="message" visible="false">
                            Kullanıcı adı sadece harf ve boşluk karakterleri içerebilir.
                        </li>
                        <li runat="server" id="liSignupKullaniciAdiKullanimda" class="message" visible="false">
                            Kullanıcı adı hali hazırda kullanılmakta.
                        </li>
                        <li runat="server" id="liSignupEposta" class="message" visible="false">
                            E-posta doldurulmalı.
                        </li>
                        <li runat="server" id="liSignupEpostaFormat" class="message" visible="false">
                            E-posta formatı hatalı.
                        </li>
                        <li runat="server" id="liSignupEpostaKullanimda" class="message" visible="false">
                            E-posta hali hazırda kullanılmakta.
                        </li>
                        <li runat="server" id="liSignupSifre" class="message" visible="false">
                            Şifre doldurulmalı.
                        </li>
                        <li runat="server" id="liSignupSifreFormat" class="message" visible="false">
                            Şifre 6 karakterden kısa, 32 karakterden uzun olamaz.
                        </li>
                        <li runat="server" id="liSignupSozlesme" class="message" visible="false">
                            Üyelik sözleşmesi kabul edilmeli.
                        </li>
                        <li runat="server" id="liSignupKullaniciTipi" class="message" visible="false">
                            Kullanıcı tipi boş geçilemez.
                        </li>
                        <li runat="server" id="liSignupHata" class="message" visible="false">
                            Kayıt işlemi yapılırken bir sorun oluştu. Lütfen bilgilerinizi kontrol edip tekrar deneyiniz.
                        </li>
                    </ul>
                </div>
            </div>

            <!-- FORM SIGNUP -->
                <div style="margin:0;padding:0;display:inline">
                    <input name="utf8" type="hidden" value="&#x2713;" />
                    <input name="authenticity_token" type="hidden" value="L2vzfVg2DxhSXUVeiJMOLDqatcRw5wmxisMAVUqIFmg=" />
                </div>
                <div class='input formSatir'>
                    <label for="user_user_name">Kullanıcı adı</label>
                    <input runat="server" class="txtKucuk" id="user_name_signup" name="user_name" type="text" value="" />
                </div>
                <div class='input formSatir'>
                    <label for="user_email_signup">E-posta</label>
                    <input runat="server" class="txtKucuk" id="user_email_signup" name="user_email" type="text" value="" />
                </div>
                <div class='input formSatir'>
                    <label for="user_password_signup">Şifre</label>
                    <input runat="server" class="txtKucuk" id="user_password_signup" name="user_password" type="password" />
                </div>
                <div class='input formSatir'>
                    <label for="user_profile_attributes_phone">Telefon*</label>
                    <input runat="server" class="txtKucuk" id="user_profile_attributes_phone" name="user_profile_attributes_phone" type="text" />
                </div>
                <div class='radio_container'>
                    <div class='registrati_type'>
                        Üyelik Tipi
                    </div><br />
                    <div class='radio_buttons'>
                        <div class='grid_2'>
                            <input runat="server" id="user_type_customer" name="user_type" type="radio" value="customer" />
                            <label class="user_type_label" for="user_user_type_customer">TASARIMA İHTİYACIM VAR</label>
                        </div><br />
                        <div class='grid_2'>
                            <input runat="server" id="user_type_creative" name="user_type" type="radio" value="creative" />
                            <label class="user_type_label" for="user_user_type_creative">PROFESYONEL TASARIMCIYIM</label>
                        </div><br />
                    </div>
                </div>
                <div class='clear'></div>
                <div class=''>
                    <div class='grid_3'>
                        <div class='check'>
                            <input runat="server" id="user_terms_of_service" name="user_terms_of_service" type="checkbox" value="1"  class="regular-checkbox" clientidmode="Static"/> <label for="user_terms_of_service"></label>
                            <label>
                                <a href="kullanim_sartlari.aspx" target="_blank">Üyelik sözleşmesini</a> okudum ve kabul ediyorum
                            </label>
                        </div>
                    </div>
                    <div class='grid_1 formSatir'>
                        <div class='submit'>
                            <input runat="server" id="btnUyeOl" class="ozel_buton_kucuk" name="commit" type="submit" value="Üye Ol" onclick="document.getElementById('action_type').value='s';" style="margin-top:20px; cursor:pointer; margin-left:-150px" />
                            <a id="btnUyeOlmadanDevam" runat="server" class="ozel_buton2 uye_olmadan_devam_buton" name="commit" clientidmode="Static" onclick="document.getElementById('btnGizliUode').click();" style="width: 220px;  padding-left: 10px;font-family: 'opificio';font-size: 17px; margin-top: -25px; margin-left: 85px; color:#000; background-color:#d1d3d4; font-weight:bold; " autopostback="true">Üye Olmadan Devam Et</a>
                            <div style="display:none;"><asp:Button ID="btnGizliUode" runat="server" Text="Button" ClientIDMode="Static" OnClientClick="document.getElementById('action_type').value='uode';"></asp:Button></div>
                            
                        </div>
                    </div>
                </div><br />
                <div class='phone_info'>
                * Size destek olabilmemiz için telefonunuzu yazın, ihtiyaç duyduğunuz noktada danışmanlarımız yanınızda olsun.
                </div><br />
                <div class='clear'></div>
            <!-- FORM SIGNUP -->

        </div>
        <div class="form" style=" width: 400px; display: inline-block; vertical-align: top; margin-top: -2px;" >
            <div class="foreign_form">
                <h1>
                Üye misiniz? Giriş yapın
                </h1>
                <div class="sign-in-form">
                    <div runat="server" id="divErrorLogin" class="error">
                        <div>
                            <ul>
                                <li>
                                        <p>  Geçersiz e-posta adresi veya şifre. </p>
                                 </li>
                            </ul>
                            
                        </div>
                    </div>
                <!-- FORM LOGIN -->
                    <div style="margin:0;padding:0;display:inline">
                        <input name="utf8" type="hidden" value="&#x2713;" />
                        <input name="authenticity_token" type="hidden" value="L2vzfVg2DxhSXUVeiJMOLDqatcRw5wmxisMAVUqIFmg=" />
                        <input name="action_type" id="action_type" type="hidden" value="l" />
                    </div>
                    <div class='input formSatir'>
                        <label for="user_email_login">Kullanıcı adı veya e-posta</label>
                        <input runat="server" class="txtKucuk" id="user_email_login" name="user_email" type="text" value="" onkeydown="return loginEnterKontrol(event); return false;" />
                    </div>
                    <div class='input formSatir'>
                        <label for="user_password_login">Şifre</label>
                        <input runat="server" class="txtKucuk" id="user_password_login" name="user_password" type="password" onkeydown="return loginEnterKontrol(event); return false;" />
                    </div>
                    <div class='check sign_in formSatir'>
                        <input runat="server" id="user_remember_me_login" name="user_remember_me" type="checkbox" value="1" class="regular-checkbox" clientidmode="Static"/>
                        <label for="user_remember_me_login"></label>
                        <label for="user_remember_me">Beni hatırla</label>
                    </div>
                    <div class='submit formSatir'>
                        <input class="ozel_buton_kucuk" name="commit" id="btnUyeGirisi" type="submit" value="Giriş yap" style="cursor:pointer;" />
                    </div>
                <!-- FORM LOGIN -->
            </div>
            <div class='forgot_pass'>
                <a href="sifremi_unuttum.aspx" rel="nofollow">Şifremi unuttum</a>
            </div>
            </div>
        </div>
        </div>
        </div>
        <div class='clear'></div>
    </div>
</section>
</div>

</form>
</asp:Content>

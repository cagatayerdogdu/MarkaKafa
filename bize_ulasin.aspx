<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="bize_ulasin.aspx.cs" Inherits="GrafikerPortal.bize_ulasin" EnableViewState="false" ViewStateEncryptionMode="Never" ViewStateMode="Disabled" EnableViewStateMac="false" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form runat="server" id="frmForm" action="geri_bildirim.aspx" method="post">

<div id="container">
<section id='contact_us'>
<h1>Bize Ulaşın</h1>
<p>
    Aşağıdaki formu doldurarak veya <a href='mailto:info@markakafa.com'>info@markakafa.com</a> adresinden bizimle iletişime gecebilirsiniz.
</p>
<input name="accesskeydisabled" type="hidden" value="1" />
<div style="margin:0;padding:0;display:inline">
    <input name="utf8" type="hidden" value="&#x2713;" />
    <input name="authenticity_token" type="hidden" value="Xue3GpbTsbSmNpTYPcTkJKAJYclqVcoGzmhXcfYxLUE=" />
</div>
<input id="return_to" name="ctl00$ContentPlaceHolder1$feedback_return_to" type="hidden" />
<input id="feedback_type" name="ctl00$ContentPlaceHolder1$feedback_type" type="hidden" value="feedback" />
<input id="feedback_project_id" name="ctl00$ContentPlaceHolder1$feedback_project_id" type="hidden" />
<input id="feedback_user_id" name="ctl00$ContentPlaceHolder1$feedback_user_id" type="hidden" />
<div class='input'>
    <label for="feedback_name">İsminiz:</label>
    <input id="feedback_name" name="ctl00$ContentPlaceHolder1$feedback_name" type="text" />
</div>
<div class='input'>
    <label for="feedback_email">E-posta adresiniz:</label>
    <input id="feedback_email" name="ctl00$ContentPlaceHolder1$feedback_email" type="text" />
</div>
<div class='text_area'>
    <label for="feedback_message">Mesajınız:</label>
    <textarea cols="60" id="feedback_message" name="ctl00$ContentPlaceHolder1$feedback_message" rows="5">
    </textarea>
</div>
<div style="display:none;" class='abuse_checker_input'>
    <label for="feedback_r_control">Lütfen bu alanı doldurmayınız</label>
    <input id="feedback_r_control" name="ctl00$ContentPlaceHolder1$feedback_r_control" type="text" />
</div>
<div class='submit main_page'>
    <input class='orange' type='submit' value='Gönder'/>
</div>


<h2 class='heading'>Adres</h2>
<p>
İSTANBUL<br />İstanbul/İstanbul<br />Tel:(000) 123 4567<br />Faks:(000) 123 4567
</p>
<h2 class='heading'>Yer sağlayıcı bilgileri</h2>
<table>
<tr>
<td>Şirket Unvanı</td>
<td>MARKA KAFA</td>
</tr>
<tr>
<td>Ticaret Sicil No</td>
<td>123456789</td>
</tr>
<tr>
<td>Merkez Adresi &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
<td>İstanbul</td>
</tr>
<tr>
<td>E-posta</td>
<td>info@markakafa.com</td>
</tr>
<tr>
<td>Telefon No</td>
<td>(000) 123 4567</td>
</tr>
<tr>
<td>Faks</td>
<td>(000) 123 4567</td>
</tr>
</table>
</section>
</div>

</form>
</asp:Content>

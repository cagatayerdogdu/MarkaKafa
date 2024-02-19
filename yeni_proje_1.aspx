<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="yeni_proje.aspx.cs" Inherits="GrafikerPortal.yeni_proje" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form runat="server" id="frmForm" action="geri_bildirim.aspx" method="post">

<div id="container">
<section class='start_a_project' id='start_a_project_index'>
<div class='start_a_project_header'>
    <h1>Neye ihtiyacınız var?</h1>
    <p>
    Yüzlerce profesyonel tasarımcının sizin için çalışmasını ister misiniz?
    Tek yapmanız gereken aşağıdaki proje türlerinden birini seçmek.
    </p>
</div>

<div id='promoted_projects'>
    <asp:Label runat="server" ID="lblProjeListeKolon1" />
    <asp:Label runat="server" ID="lblProjeListeKolon2" />
    <asp:Label runat="server" ID="lblProjeListeKolon3" />
</div>

<div class='clear'></div>
</section>
<div class='clear'></div>
<section id='guarantee_wrapper'>
<div class='grid_6'>
<div class='guarantee'>
<div class='text'>
<p>
Daha önce markakafa.com'da proje tamamlayanlar ne dediler? Tamamlanan 
<a href="projeler.aspx?s=c">projeleri görün</a>
veya
<a href="referanslar.aspx">referanslarımızı inceleyin</a>
</p>
</div>
</div>
</div>
<div class='grid_6'>
<div class='refund'>
<div class='text'>
<p>
Projenize yüklenen hiçbir tasarımı beğenmezseniz paranızı iade ediyoruz. Detaylı bilgi için
<a href="sistem_nasil_isler.aspx">tıklayın</a>
</p>
</div>
</div>
</div>
</section>
<div class='clear'></div>
<section id='start_project_contact_us'>
<h1>BİZE ULAŞIN</h1>
<p>
Aklınıza takılan sorular mı var? Bize aşağıdaki formu kullanarak ulaşabilirsiniz.
</p>
<!-- FORM BİZE ULAŞIN -->
<input name="accesskeydisabled" type="hidden" value="1" />
<div style="margin:0;padding:0;display:inline">
<input name="utf8" type="hidden" value="&#x2713;" />
<input name="authenticity_token" type="hidden" value="Juig+H9uGz22oTR/iiS6sYAo6X0c9mi8B2kI5x98qPo=" />
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
<textarea cols="80" id="feedback_message" name="ctl00$ContentPlaceHolder1$feedback_message" rows="5">
</textarea>
</div>
<div class='submit main_page'>
<input class='orange' type='submit' value='Gönder' />
</div>
<!-- FORM BİZE ULAŞIN -->


<div class='clear'></div>
</section>
</div>

</form>
</asp:Content>--%>

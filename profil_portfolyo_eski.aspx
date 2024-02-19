<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="profil_portfolyo_eski.aspx.cs" Inherits="GrafikerPortal.profil_portfolyo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">

<div id="container">
<section class='idepeople_show' id='portfolio_items'>
<h1>Portfolyom</h1>

<input runat="server" id="hfSilinecekPortfolyo" type="hidden" clientidmode="Static" value="" />

<div runat="server" id="divSuccessPortfolyoSil" class="success" visible="false">
    <div>
        <p>
            Dosya portfolyonuzdan kaldırıldı.
        </p>
    </div>
</div>
<div runat="server" id="divSuccessPortfolyoOnay" class="success" visible="false">
    <div>
        <p>
            Portfolyonuz incelendikten sonra e-posta yoluyla bilgilendirme yapılacaktır. İlginiz için teşekkür ederiz.
        </p>
    </div>
</div>

<div runat="server" id="divNotePortfolyoPasif" class="note_box" visible="false">
    Sistemin sizi profesyonel tasarımcı olarak tanımlayabilmesi için portfolyonuza geçmişte yapmış olduğunuz çalışmalardan örnekler     yüklemelisiniz. Tasarımcıları uzmanlık alanlarına göre kategorize ediyoruz. Bu nedenle uzmanı olduğunuz tüm tasarım kategorileri ile    ilgili örnek çalışmalar yüklemelisiniz. Ne kadar çok, o kadar iyi... Ne kadar iyi, o kadar daha iyi...
</div>
<div runat="server" id="divNotePortfolyoAktif" class="note_box" visible="false">
    Portfolyonuz için onay incelemesi talebinde bulunulmuştur. Portfolyonuz incelendikten sonra e-posta yoluyla bilgilendirme yapılacaktır.
</div>
<asp:Button runat="server" ID="btnPortfolyoSil" Text="SİL" OnClick="PortfolyoSil" style="display:none;" ClientIDMode="Static" />
<a class="orange button" href="profil_portfolyo_yeni_resim.aspx">Portfolyona yeni resim ekle</a>
<a class="orange button" href="profil_portfolyo_yeni_video.aspx">Portfolyona yeni video ekle</a>
<span class="submit">
    <asp:Button runat="server" ID="btnOnayaGonder" class="orange button" Text="Onaya Gönder" OnClick="PortfolyoOnayaGonder" />
</span>
<ul class='reset center_text' id='images'>
    <asp:Label runat="server" ID="lblListePortfolyo" />
</ul>
</section>
</div>

</form>

</asp:Content>

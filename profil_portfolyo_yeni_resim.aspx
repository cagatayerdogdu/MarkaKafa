<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="profil_portfolyo_yeni_resim.aspx.cs" Inherits="GrafikerPortal.profil_portfolyo_yeni_resim" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">

<div id="container">
<section id='new_portfolio_item'>
<h1 style="color: #E70386;text-shadow: 7px 5px 10px #fc0075;">Portfolyona ekle</h1>

<br />
<div runat="server" id="divError" class="error" visible="false">
    <p>
        <strong>
            Portfolyonuz eklenemedi.
        </strong>
    </p>
    <div>
        <ul>
            <li runat="server" id="liErrorTitle" class="message" visible="false">
                Portfolyo başlığı girmediniz.
            </li>
            <li runat="server" id="liErrorFile" class="message" visible="false">
                Portfolyo resmi seçmediniz.
            </li>
            <li runat="server" id="liErrorFileSize" class="message" visible="false">
                Dosya boyutu 5 MB'ı geçmemelidir.
            </li>
            <li runat="server" id="liErrorFileFormat" class="message" visible="false">
                Dosya formatı JPEG, PNG veya GIF olabilir.
            </li>
            <li runat="server" id="liError" class="message" visible="false">
                Dosya yüklenirken bir sorun oluştu. Problem devam ederse lütfen bizimle iletişime geçiniz.
            </li>
        </ul>
    </div>
</div><br />

<div class='input'>
    <label style="margin-left:-262px;">Resim</label>
    <input class="ozel_buton2" name="commit" onclick="document.getElementById('fuResim').click(); return false;" type="submit" value="DOSYA YÜKLE" style="margin-left:25px; float:none;" />
    <asp:FileUpload ID="fuResim" runat="server" ClientIDMode="Static" style="display:none;"/>
</div>
<div style="margin:0;padding:0;display:none">
    <input name="utf8" type="hidden" value="&#x2713;" />
    <input name="authenticity_token" type="hidden" value="MdhBGZwA7gg7GmlhmauOut/XNB6DKTc5GLIEbDf1w3M=" />
    <input id="portfolio_item_upload_key" name="portfolio_item[upload_key]" type="hidden" value="f1f24ede-9a26-4497-b634-69cb0f4dfa66" />
    <input id="portfolio_item_item_file_name" name="portfolio_item[item_file_name]" type="hidden" />
    <input id="portfolio_item_item_type" name="portfolio_item[item_type]" type="hidden" value="image" />
</div>
<br />
<div class='input'>
    <label for="portfolio_item_title">Başlık</label>
    <input runat="server" id="portfolio_item_title" name="portfolio_item[title]" type="text" clientidmode="Static" class="txt2" style="width: 430px; margin-left:28px;" />
</div>
<br />
<div class='text_area'>
    <label for="portfolio_item_description">Açıklama</label>
    <textarea runat="server" id="portfolio_item_description" name="portfolio_item[description]" clientidmode="Static" class="txt2" rows="4" cols="50" style="height: 61px; width: 430px;"></textarea>
</div>
<br />
<div class='submit'>
    <asp:Button ID="btnKaydet" runat="server" Text="PORTFOLYO YÜKLE" class="ozel_buton2" name="commit" onclick="btnKaydet_Click" ClientIDMode="Static" style="margin-left: 252px;" />
</div>
</section>
</div>

</form>

</asp:Content>

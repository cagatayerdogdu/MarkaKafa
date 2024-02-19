<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="proje_katil.aspx.cs" Inherits="GrafikerPortal.proje_katil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--<link href="/assets/application-3887b67cc1d4f352bca12dafbfa2b620.css" media="screen" rel="stylesheet" />
--%></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm" enctype="multipart/form-data">

<div id="container">
<section id="show_project">
    <h1>
<asp:Label ID="lblProjeAdi" runat="server" Text="" style="color: #E70386;text-shadow: 7px 5px 10px #fc0075;"/>
		<br />
        <br />
        <br />
		<span class="sub">
            <asp:Label ID="lblProjeTip" runat="server" Text="" style="color:#d1d3d4;"/>
			<%--&mdash; ( - şeklinde aralık koymaya yarıyor.  )--%>
            <span style="display:none;">[<asp:Label ID="lblProjeSahibi" runat="server" Text="" style="color:#a7a9ac;" />]</span>
           <%-- <asp:Label ID="lblProjeSahibi" runat="server" Text="" />--%>
		</span>
	</h1>

        <br />
    <div id="project_bar">
	    <asp:Label ID="lblProjeDuyuru" runat="server" Text="" />
	    <table>
		    <thead>
			    <tr style="color: #fc0075;">
				    <th style="width:300px; text-decoration: underline;">Ödül</th>
				    <th style="width:269px; text-decoration: underline;">Bitiş</th>
				    <th style="width:200px; text-decoration: underline;">Tasarım Sayısı</th>
                    <th style="text-decoration: underline; display:none;"><asp:Label ID="lblTabloBaslikKazananTasarim" runat="server" Text="" /></th>
				    <th></th>
			    </tr>
		    </thead>
		    <tbody>
			    <tr>
				    <td class="large_text" style="padding-top: 15px;"><asp:Label ID="lblTabloHucreOdul" runat="server" Text="" /> TL</td>
				    <td class="large_text" style="padding-top: 15px;"><asp:Label ID="lblTabloHucreBitisTarihi" runat="server" Text="" /></td>
				    <td class="large_text" style="padding-top: 15px;"><asp:Label ID="lblTabloHucreTasarimSayisi" runat="server" Text="" /></td>
                    <td class="large_text" style="padding-top: 15px; display:none;"><asp:Label ID="lblTabloHucreKazananTasarim" runat="server" Text="" /></td>
				    <td style="padding-top: 15px;"></td>
			    </tr>
		    </tbody>
	    </table>
    </div>
    <br /><br />
    <a runat="server" id="btnProjeDetaylari" class="btn_oval portfolyo_bosluk" style="cursor:pointer; margin-left:217px;">Proje Detayları</a>   
    <a runat="server" id="btnProjeGaleri" class="btn_oval katildigi_projeler_bosluk" style="cursor:pointer;">Proje Galerisi</a>
    <br /><br /> <br /><br />
    
    <div runat="server" id="divErrorResim" class="error" visible="false">
        <div>
            <ul>
                <li runat="server" id="liErrorResimTitle" class="message" visible="false">
                    Başlık girmediniz.
                </li>
                <li runat="server" id="liErrorResimFile" class="message" visible="false">
                    Görsel dosyası seçmediniz.
                </li>
                <li runat="server" id="liErrorResimFileSize" class="message" visible="false">
                    Dosya boyutu 5 MB'ı geçmemelidir.
                </li>
                <li runat="server" id="liErrorResimFileFormat" class="message" visible="false">
                    Dosya formatı JPEG, PNG veya GIF olabilir.
                </li>
                <li runat="server" id="liErrorResim" class="message" visible="false">
                    Dosya yüklenirken bir sorun oluştu. Problem devam ederse lütfen bizimle iletişime geçiniz.
                </li>
            </ul>
        </div>
    </div>
    <div runat="server" id="divErrorVideo" class="error" visible="false">
        <div>
            <ul>
                <li runat="server" id="liErrorVideoTitle" class="message" visible="false">
                    Başlık girmediniz.
                </li>
                <li runat="server" id="liErrorVideoFile" class="message" visible="false">
                    Video dosyası seçmediniz.
                </li>
                <li runat="server" id="liErrorVideoFileSize" class="message" visible="false">
                    Dosya boyutu 5 MB'ı geçmemelidir.
                </li>
                <li runat="server" id="liErrorVideoFileFormat" class="message" visible="false">
                    Dosya formatı VMW, FLA veya MP4 olabilir.
                </li>
                <li runat="server" id="liErrorVideo" class="message" visible="false">
                    Dosya yüklenirken bir sorun oluştu. Problem devam ederse lütfen bizimle iletişime geçiniz.
                </li>
            </ul>
        </div>
    </div>
    <div runat="server" id="divErrorMetin" class="error" visible="false">
        <div>
            <ul>
                <li runat="server" id="liErrorMetinTitle" class="message" visible="false">
                    Önerinizi girmediniz.
                </li>
                <li runat="server" id="liErrorMetin" class="message" visible="false">
                    Girdiniz kaydedilirken bir sorun oluştu. Problem devam ederse lütfen bizimle iletişime geçiniz.
                </li>
            </ul>
        </div>
    </div>
    
    <div runat="server" id="divGirdiResim">
        <div class="input">
            <label>Görsel Dosyası</label>
        </div>  <br />
        <div class="submit">
            <input class="ozel_buton2" name="commit" onclick="document.getElementById('fuDosyaResim').click(); return false;" type="submit" value="DOSYA YÜKLE" style="margin-left:25px; float:none; "/>
            <asp:FileUpload ID="fuDosyaResim" runat="server" ClientIDMode="Static" style="display:none;"/>
        </div>
        <br />
        <div class="input">
            <label for="txtBaslikResim">Başlık</label>
            <input runat="server" id="txtBaslikResim" name="txtBaslikResim" type="text" clientidmode="Static" class="txt2" style="width: 430px; margin-left:28px;" />
        </div>
        <br />
        <div class="text_area">
            <label for="txtAciklamaResim">Açıklama</label>
            <textarea runat="server" id="txtAciklamaResim" name="txtAciklamaResim" clientidmode="Static" class="txt2" rows="4" cols="50" style="height: 61px; width: 430px;"></textarea>
        </div>
    </div>

    <div runat="server" id="divGirdiVideo">
        <div class="input">
            <label>Video Dosyası</label>
        </div>
        <div class="submit">
            <input class="orange" name="commit" onclick="document.getElementById('fuDosyaVideo').click(); return false;" type="submit" value="DOSYA YÜKLE" />
            <asp:FileUpload ID="fuDosyaVideo" runat="server" ClientIDMode="Static" style="display:none;"/>
        </div>
        <div class="input">
            <label for="txtBaslikVideo">Başlık</label>
            <input runat="server" id="txtBaslikVideo" name="txtBaslikVideo" type="text" clientidmode="Static" />
        </div>
        <br /><br />
        <div class="text_area">
            <label for="txtAciklamaVideo">Açıklama</label>
            <textarea runat="server" id="txtAciklamaVideo" name="txtAciklamaVideo" clientidmode="Static" class="txt2" rows="4" cols="50" style="height: 61px; width: 430px;"></textarea>
        </div>
    </div>

    <div runat="server" id="divGirdiOneri">
        <div class="input">
            <label>Öneriniz</label>
        </div>
        <div class="input">
            <label for="txtBaslikMetin">Başlık</label>
            <input runat="server" id="txtBaslikMetin" name="txtBaslikMetin" type="text" clientidmode="Static" class="txt2" style="width: 430px; margin-left:28px;" />
        </div>
        <div class="text_area">
            <label for="txtAciklamaMetin">Açıklama</label>
            <textarea runat="server" id="txtAciklamaMetin" name="txtAciklamaMetin" clientidmode="Static"  class="txt2" rows="4" cols="50" style="height: 61px; width: 430px;"></textarea>
        </div>
    </div>
    <br />
    <div class="submit">
        <asp:HiddenField ID="hfTip" runat="server" Value="" />
        <asp:Button ID="btnKaydet" runat="server" Text="KATIL"  class="ozel_buton2" name="commit" onclick="btnKaydet_Click" style="margin-left:25px; float:none;"></asp:Button>
    </div>


</section>
</div>

</form>

</asp:Content>
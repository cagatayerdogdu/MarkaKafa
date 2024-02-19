<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="proje_tasarimlar.aspx.cs" Inherits="GrafikerPortal.proje_tasarimlar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function sayfaDegistir(sayfaNo) {
        document.getElementById('hf_page').value = sayfaNo;
        document.getElementById('btnSayfaDegistir').click();
    }
</script>
<%--<link href="/assets/application-3887b67cc1d4f352bca12dafbfa2b620.css" media="screen" rel="stylesheet" />--%>
<style type="text/css">
    .yorum {
        display: block;
    }
    .divTasarimci {
        float: left;
    }
    .divYorumDetay {  
        float: left;
        margin-left: 30px;
        margin-top: 20px;
    }
</style>

    <%--<script type="text/javascript">var switchTo5x = true;</script>
<script type="text/javascript" src="http://w.sharethis.com/button/buttons.js"></script>
<script type="text/javascript">stLight.options({ publisher: "8a4ac016-1139-43e6-925c-6140ba9df2b9", doNotHash: false, doNotCopy: false, hashAddressBar: false });</script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">

<div id="container">
<section id="show_project">
     <asp:HiddenField ID="hfProjeSahibi" runat="server" Value="" />
     <asp:HiddenField ID="hfTasarimci" runat="server" Value="" />
     <asp:HiddenField ID="hfSahipMail" runat="server" Value="" />
     <asp:HiddenField ID="hfTasarimciMail" runat="server" Value="" />
	<h1>
		<asp:Label ID="lblProjeAdi" runat="server" Text="" style="color: #E70386;text-shadow: 7px 5px 10px #fc0075;"/>
		<br /><br /><br />
		<span class="sub">
            <asp:Label ID="lblProjeTip" runat="server" Text="" style="color:#d1d3d4;"/>
			<span style="display:none;">[<asp:Label ID="lblProjeSahibi" runat="server" Text="" style="color:#a7a9ac;" />]</span>
		</span>
	</h1>
    
    <div runat="server" id="divErrorYorum" class="error" visible="false">
        <div>
            <ul>
                <li runat="server" id="liErrorYorumIcerik" class="message" visible="false">
                    Yorumunuzu girmediniz.
                </li>
                <li runat="server" id="liErrorYorum" class="message" visible="false">
                    Yorumunuz gönderilirken bir sorun oluştu. Problem devam ederse lütfen bizimle iletişime geçiniz.
                </li>
            </ul>
        </div>
    </div>
    <div runat="server" id="divSuccessYorum" class="success" visible="false">
        <div>
            <ul>
                <li runat="server" id="liSuccessYorum" class="message">
                    Yorumunuz yöneticiler tarafından onaylandığında yayınlanacaktır.
                </li>
            </ul>
        </div>
    </div>
    
    <div runat="server" id="divErrorKazanan" class="error" visible="false">
        <div>
            <ul>
                <li runat="server" id="ErrorKazanan" class="message" visible="false">
                    Projeniz kazanıldı olarak işaretlenirken bir sorun oluştu. Problem devam ederse lütfen bizimle iletişime geçiniz.
                </li>
            </ul>
        </div>
    </div>
    <div runat="server" id="divSuccessKazanan" class="success" visible="false">
        <div>
            <ul>
                <li runat="server" id="liSuccessKazanan" class="message">
                    Projeniz kazanıldı olarak işaretlenmiştir.
                </li>
            </ul>
        </div>
    </div>

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
    <br />
    <br />
    <a runat="server" id="btnProjeGaleri" class="btn_oval" style="cursor:pointer; margin-left:350px;">Proje Galerisi</a>
    <br />
    <%--    <div style="display:none;">
            <span class='st_facebook' displayText='Facebook'></span>
            <span class='st_googleplus' displayText='Google +'></span>
            <span class='st_twitter' displayText='Tweet'></span>
            <span class='st_linkedin' displayText='LinkedIn'></span>
            <span class='st_pinterest' displayText='Pinterest'></span>
            <span class='st_email' displayText='Email'></span>
        </div>--%>

     <div style="clear:both;"></div>
    <br />
    <br />
    <asp:Button ID="btnKazananSec" runat="server" Text="KAZANAN SEÇ" class="ozel_buton2" name="commit" onclick="btnKazananSec_Click" style="margin-left:25px; float:none;" OnClientClick="return confirm('Bu girdiyi kazanan olarak seçmek istediğinizden emin misiniz?\r\nOnaylarsanız, projeniz tamamlandı olarak işaretlenecektir.\r\nBu işlem geri alınamaz!');"></asp:Button>
     <br />
    <br />

    <div>
        <asp:Label ID="lblProjeDetay" runat="server" Text="" />
    </div>

    <div style="clear:both;"></div>
    <div>
        <asp:Label ID="lblProjeYorumlar" runat="server" Text="" />
    </div>
    
    <br />
    <div runat="server" id="divYeniYorum" Visible="false">
        <div class="text_area">
            <label for="txtYorum">Yorumunuz:</label>
            <br />
            <textarea runat="server" id="txtYorum" name="txtYorum" clientidmode="Static" class="txt2" rows="4" cols="50" style="height: 61px; width: 430px;"></textarea>
        </div>
        <div class="submit">
            <br />
            <asp:Button ID="btnYorumGonder" runat="server" Text="Yorum Yaz" class="ozel_buton2" name="commit" onclick="btnYorumGonder_Click" style="margin-left:25px; float:none; font-family:'opificio'; font-size:16px; font-weight:bold; padding-top:5px; padding-left:40px;"></asp:Button>
        </div>
    </div>

</section>
</div>

</form>

</asp:Content>

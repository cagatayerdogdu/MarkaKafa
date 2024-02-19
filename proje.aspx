<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="proje.aspx.cs" Inherits="GrafikerPortal.proje" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function sayfaDegistir(sayfaNo) {
        document.getElementById('hf_page').value = sayfaNo;
        document.getElementById('btnSayfaDegistir').click();
    }

</script>
    <style type="text/css">
        .DivSayfalama
        {
            display:block;
        }
        .info_comment {
            color: #bbb;
        }
         
       
    </style>
<%--<link href="/assets/application-3887b67cc1d4f352bca12dafbfa2b620.css" media="screen" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">

<input runat="server" id="hf_page" type="hidden" value="0" clientidmode="Static" />
<asp:Button ID="btnSayfaDegistir" runat="server" Text="" ClientIDMode="Static" style="display:none;" onclick="btnSayfaDegistir_Click" />

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
    <br />
    <a runat="server" id="btnProjeDetaylari" class="btn_oval portfolyo_bosluk" style="cursor:pointer; margin-left:217px;">Proje Detayları</a>
    <%--<div style="clear:both;"></div>--%>
    <a runat="server" id="btnGizlilikAnlasmasi" class="btn_oval" style="cursor:pointer; margin-left:340px;">Gizlilik Anlaşması</a>
    <%--<div style="clear:both;"></div>--%>
    <a runat="server" id="btnPortfolyoEkle" class="btn_oval" style="cursor:pointer;" href="profil_portfolyo.aspx">Portfolyo Ekle</a>
    <%--<div style="clear:both;"></div>--%>
    <a runat="server" id="btnProjeyeKatil" class="btn_oval katildigi_projeler_bosluk" style="cursor:pointer;">Projeye Katıl</a>
    <div style="clear:both;"></div>

    <br /><br />
    <asp:Label ID="lblProjeGirdiListesi" runat="server" Text="" style="display:table-row"/>
    <asp:Label ID="lblDivSayfalama" runat="server" Text="" />

</section>
</div>

</form>

</asp:Content>

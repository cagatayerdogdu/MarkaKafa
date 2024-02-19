<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="proje_detay.aspx.cs" Inherits="GrafikerPortal.proje_detay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--<link href="/assets/application-3887b67cc1d4f352bca12dafbfa2b620.css" media="screen" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">

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
    <br />
    <a runat="server" id="btnProjeGaleri" class="btn_oval" style="cursor:pointer; margin-left:345px;">Proje Galerisi</a>
     <div style="clear:both;"></div>
    <br />
    <br />
    <div>
        <asp:Label ID="lblProjeDetay" runat="server" Text="" />
    </div>


</section>
</div>

</form>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="proje_gizlilik_anlasmasi.aspx.cs" Inherits="GrafikerPortal.proje_gizlilik_anlasmasi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="/assets/application-3887b67cc1d4f352bca12dafbfa2b620.css" media="screen" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">

<div id="container" style="">
<section id="show_project">
	<h1>
		<asp:Label ID="lblProjeAdi" runat="server" Text="" />
		<br />
        <br />
		<span class="sub">
            <asp:Label ID="lblProjeTip" runat="server" Text="" />
			&mdash;
            <asp:Label ID="lblProjeSahibi" runat="server" Text="" />
            <br />
		</span>
	</h1>

    <div id="project_bar">
	    <asp:Label ID="lblProjeDuyuru" runat="server" Text="" />
	    <table>
		    <thead>
			    <tr>
				    <th style="width:300px;">Ödül</th>
				    <th style="width:269px">Bitiş</th>
				    <th style="width:200px;">Tasarım Sayısı</th>
                    <th><asp:Label ID="lblTabloBaslikKazananTasarim" runat="server" Text="" /></th>
				    <th></th>
			    </tr>
		    </thead>
		    <tbody>
			    <tr>
				    <td class="large_text"><asp:Label ID="lblTabloHucreOdul" runat="server" Text="" /> TL</td>
				    <td class="large_text"><asp:Label ID="lblTabloHucreBitisTarihi" runat="server" Text="" /></td>
				    <td class="large_text"><asp:Label ID="lblTabloHucreTasarimSayisi" runat="server" Text="" /></td>
                    <td class="large_text"><asp:Label ID="lblTabloHucreKazananTasarim" runat="server" Text="" /></td>
				    <td></td>
			    </tr>
		    </tbody>
	    </table>
    </div>

    <div>
        <asp:Label ID="lblGizlilikAnlasmasi" runat="server" Text="" />
    </div>


    <div class="submit">
        <asp:Button ID="btnImzala" runat="server" Text="İmzala" class="ozel_buton2" style="margin-left:650px;" name="commit" ClientIDMode="Static" onclick="btnImzala_Click"></asp:Button>
    </div>


</section>
</div>

</form>

</asp:Content>

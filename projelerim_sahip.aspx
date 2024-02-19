<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="projelerim_sahip.aspx.cs" Inherits="GrafikerPortal.projelerim_sahip" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tasarimci_sag
        {
            font-size: 15px;
        }
        .proje_odeme a
        {
            width: 165px;
            font-size: 17px;
            color: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">


<%--<input runat="server" id="hf_filter_category" type="hidden" clientidmode="Static" />
<input runat="server" id="hf_filter_sector" type="hidden" clientidmode="Static" />
<input runat="server" id="hf_page" type="hidden" value="0" clientidmode="Static" /> Kendi projeleri olduğu için hepsini bir sayfada görsün. diye kapatıldı.
<%--<asp:Button ID="btnSayfaDegistir" runat="server" Text="" ClientIDMode="Static" style="display:none;" onclick="btnSayfaDegistir_Click" />
<asp:Button ID="btnFiltrele" runat="server" Text="" ClientIDMode="Static" style="display:none;" onclick="btnFiltrele_Click" />--%>

<asp:Label ID="lblSectionStart" runat="server" Text="" />

<asp:Label ID="lblListeProjeler" runat="server" Text="" />
<div class="pagination" style="display:none;">
    <asp:Label ID="lblSayfaNumaralari" runat="server" Text="" />
</div>
<asp:Label ID="lblSectionEnd" runat="server" Text="" />



</form>


</asp:Content>

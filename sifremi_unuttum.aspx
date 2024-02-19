<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="sifremi_unuttum.aspx.cs" Inherits="GrafikerPortal.sifremi_unuttum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        
          .txt_orjinal_kucuk
        {
	        border-bottom-left-radius: 30px;
            border-top-left-radius: 30px;
	        border:0px;
	        height:28px;
	        width:175px;
	
	        border-width: 0px 6px 0px 0px;
            border-color:#fc0075; /* #e7038a; _CE_[08.03.2015]*/
            border-style: solid;
	        font-size:13px;
	        padding-left:10px;
	
        }

    </style>

    <form runat="server">
        <div><br /><br /><br /><br /><br />
    <asp:Label Text="Mail Adresi Giriniz" runat="server" style="margin-left:0px;"/>
    <asp:TextBox runat="server" ID="txtMailGir" class="txt_orjinal_kucuk" />
            <br /><br />
    <asp:Button class="ozel_buton_kucuk" ID="btnGonder" Text="Gönder" runat="server" OnClick="btnGonder_Click" style="margin-left:615px;" />
</div>
    <br /><br /><br />
    <div runat="server" id="divSuccessSifreDegisti" class="success" visible="false">
        <div>
            <ul>
                <li runat="server" id="liSuccessSifreDegisti" class="message">
                    Yeni Şifreniz Güncellenerek, Sistemimizde Kayıtlı olan <asp:Label Text="" runat="server" ID="SifreGuncel"/> adresinize gönderilmiştir.
                </li>
            </ul>
        </div>
    </div>
    <br /><br /><br />

    </form>
</asp:Content>

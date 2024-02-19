<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="tasarimci.aspx.cs" Inherits="GrafikerPortal.tasarimci" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">

<div id="container">
<section class='idepeople_show' id='idepeople_show'>
    <div id='user_name'>
        <img runat="server" id="imgAvatar" style="height:60px;" alt="" src="" /> <br />
        <h1>
            <asp:Label ID="lblKullaniciAdi" runat="server" Text="" style="color: #E70386;text-shadow: 7px 5px 10px #fc0075;"/>
            <br /><br /><br />
            <span class='sub'>
                <asp:Label ID="lblUzmanlikAlanlari" runat="server" Text="" style="color:#d1d3d4;"/>
            </span>
        </h1>
    </div><br />
    <div id='user_profile'>
    <asp:Label ID="lblWebsite" runat="server" Text="" /> <br />
    <asp:Label ID="lblBlog" runat="server" Text="" /> <br />
    <p><asp:Label ID="lblHakkinda" runat="server" Text="" style="color:#d1d3d4;"/></p>

    </div> <br />

    <div class='category_links clearfix'>
        <ul class="menu">
            <asp:Label ID="lblKullaniciMenu" runat="server" Text="" />
        </ul>
    </div>
    <br /><br /><br />
    <ul class='reset center_text' id='images' style="list-style-type: none; padding: 0;">
        <asp:Label ID="lblListe" runat="server" Text="" style="color:#E70386;"/>
    </ul>
</section>
</div>

</form>

</asp:Content>

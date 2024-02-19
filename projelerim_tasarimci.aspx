<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="projelerim_tasarimci.aspx.cs" Inherits="GrafikerPortal.projelerim_tasarimci" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="/assets/application-a81f73d8f373159300c6e9fee51cdc83.css" media="screen" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div id="container">
          <section id='projects_index'>
<h1>Projelerim</h1>
<div class='category_links clearfix'>
<ul class='menu'>
 <asp:Label ID="lblSekmeler" runat="server" Text="" />
</ul>
</div>
<asp:Label ID="lblListeProjeler" runat="server" Text="" />
<div class='note_box'>
Sistemin sizi profesyonel tasarımcı olarak tanımlayabilmesi için portfolyonuza geçmişte yapmış olduğunuz çalışmalardan örnekler yüklemelisiniz. Tasarımcıları uzmanlık alanlarına göre kategorize ediyoruz. Bu nedenle uzmanı olduğunuz tüm tasarım kategorileri ile ilgili örnek çalışmalar yüklemelisiniz. Ne kadar çok, o kadar iyi... Ne kadar iyi, o kadar daha iyi...
</div>
<a class="orange button" href="/account/portfolio_items/new">Portfolyona yeni dosya ekle</a>
</section>

        </div>
</asp:Content>

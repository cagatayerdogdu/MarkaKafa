<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="marka_kafa.aspx.cs" Inherits="GrafikerPortal.marka_kafa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table>
     <tr>
     <td valign="top" style="padding-left:50px; padding-top:20px;">
     
   
     <br />
          
          
          <div style="background-image:url('img/balon_01.png'); width:152px; height:146px; margin-left:17px;">
            <img runat="server" id="imgAvatar" style=" border-radius: 100%;
    height: 127px;
    margin-left: -7px;
    margin-top: 9px;
    width: 128px;" src="img/MarkaKafaAvatar.jpg">
          </div>
          
          
          
<br />

<div style="height:10px;">  </div>
    
    <div style="margin-left:30px; text-align:left;">
      <span> <asp:Label ID="lblTasarimciAdi" runat="server" Text=""></asp:Label> </span>
       <br />
             <span style="font-size:11px;"> <asp:Label ID="lblTarih" runat="server" Text=""></asp:Label> tarihinde marka kafa oldu </span>
             <br />
              <a href="#" runat="server" id="a_Katildigi" style="font-size:11px;"> katıldığı markalar </a>
              <br />
              <a href="#" runat="server" id="a_Kazandigi" style="font-size:11px;"> kazandığı markalar </a>
              
              
              <br />
              <br />
              
              <a class="ozel_buton2" href="yeni_marka.aspx"> Yeni marka </a>
              <br />
              <br />
              <a class="ozel_buton2" runat="server" id="OncekiMarkalarim" href=""> Önceki markalarım </a>
                 <br />
              <br />
              <a class="ozel_buton2" href="profil.aspx"> Profilimi düzenle  </a>
                <br />
              <br />
              <a class="ozel_buton2"  href="faturabilgileri.aspx"> Fatura Bilgilerim </a>
                 <br />
              <br />
    </div>
    
     </td>
     <td valign="top" style="text-align:left;">
     
     <div style="height:500px; width:866px; ">
     
     </div>

             
     </td>
     </tr>
     </table>


</asp:Content>

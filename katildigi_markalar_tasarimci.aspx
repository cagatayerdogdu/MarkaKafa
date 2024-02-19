<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="katildigi_markalar_tasarimci.aspx.cs" Inherits="GrafikerPortal.katildigi_markalar_tasarimci" %>
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
    width: 128px;" src="img/maskot.png">
          </div>
          
          
          
<br />

<div style="height:10px;">  </div>
    
    <div style="margin-left:30px; text-align:left;">
      <span> <asp:Label ID="lblTasarimciAdi" runat="server" Text=""></asp:Label> </span>
       <br />
             <span style="font-size:7px;"> <asp:Label ID="lblTarih" runat="server" Text=""></asp:Label> tarihinde marka kafa oldu </span>
             <br />
              <a href="#" runat="server" id="a_Katildigi" style="font-size:11px;"> katıldığı markalar </a>
              <br />
              <a href="#" runat="server" id="a_Kazandigi" style="font-size:11px;"> kazandığı markalar </a>
              
               
              <br />
              <br />
              
              
    </div>
    
     </td>
        <td valign="top" style="text-align:left;">
     
            <div class="kutular" style=" width:960px; margin-left:-40px; margin-top:-30px;">
            
                    <asp:Label ID="lblTasarimlar" runat="server" Text=""></asp:Label>
                               
  	       </div> 
           
        </td>
     </tr>
   </table>
    


</asp:Content>

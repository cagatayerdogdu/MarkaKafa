<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="faturabilgileri.aspx.cs" Inherits="GrafikerPortal.faturabilgileri" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <style type="text/css">
        .divSatir {
            display: block;
            clear: both;
            height: 50px;
        }
        .divSatir .ozel_buton2{
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">

        <table>          

         <tr>
             <td valign="top" style="padding-left:225px; padding-top:20px;"></td>
             <td valign="top" style="text-align:left;">
     
                 <div style="height:500px; width:650px; ">
                
                      

                    <a class="ozel_buton2" style="cursor:pointer; float: right;" data-toggle="modal" data-target="#myModal" onclick="faturaDuzenle('');"> Yeni Kayıt </a>
                     <br />
                     <br />
                     <br />
                     <br />
                     <asp:Label runat="server" ID="lblFaturalar" />

	                <div id="myModal" class="modal fade" style="margin-top:50px; display: none; background-color:#67696B; border:0px; width:600px;display: none; background-color:#67696B; border:0px; -webkit-border-radius: 50px;-moz-border-radius: 50px; border-radius: 50px; left:50%;"  tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		                <div class="modal-header" style="border:0px; background-color:#67696B; border:0px; width:600px;display: none; background-color:#67696B; border:0px; -webkit-border-radius: 50px;-moz-border-radius: 50px; border-radius: 50px;">	
                            <%--<a class="close" data-dismiss="modal">×</a>		--%>
			                <img  style="margin-left:83px;" src="img/logo.png" />
		                </div>
		                <div style="width:600px; height:430px;">
			                <fieldset style="border:0px;">
		                         <div class="modal-body">
                                    <iframe id="myFrame" width="500px" height="400px" style="display:none; margin-left:50px; overflow:hidden;" frameborder="0"></iframe>
		                        </div>
			                </fieldset>
		                </div>
	                </div>

                 </div>

             </td>
         </tr>
         </table>
    </div>
</asp:Content>

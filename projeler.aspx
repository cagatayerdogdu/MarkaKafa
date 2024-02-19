<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="projeler.aspx.cs" Inherits="GrafikerPortal.projeler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


         <style>
    
            .slides2
             {
              display: none;
              margin-bottom:50px;
            }
    
        </style>


      <style>
    .slides {
      display: none
    }

    .container {
      margin: 0 auto
    }

    /* For tablets & smart phones */
    @media (max-width: 767px) {
      body {
        padding-left: 20px;
        padding-right: 20px;
      }
      .container {
        width: auto
      }
    }

    /* For smartphones */
    @media (max-width: 480px) {
      .container {
        width: auto
      }
    }

    /* For smaller displays like laptops */
    @media (min-width: 768px) and (max-width: 979px) {
      .container {
        width: 724px
      }
    }

    /* For larger displays */
    @media (min-width: 1200px) {
      .container {
        width: 1170px
      }
    }
  </style>


    <style>

        .slidesjs-navigation {
            display: none;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">



<input runat="server" id="hf_filter_category" type="hidden" clientidmode="Static" />
<input runat="server" id="hf_filter_sector" type="hidden" clientidmode="Static" />
<input runat="server" id="hf_page" type="hidden" value="0" clientidmode="Static" />
<asp:Button ID="btnSayfaDegistir" runat="server" Text="" ClientIDMode="Static" style="display:none;" onclick="btnSayfaDegistir_Click" />
<asp:Button ID="btnFiltrele" runat="server" Text="" ClientIDMode="Static" style="display:none;" onclick="btnFiltrele_Click" />

<asp:Label ID="lblSectionStart" runat="server" Text="" />

    <div class="uclu_menu">
        <asp:Label ID="lblSekmeler" runat="server" Text="" />
    </div>

<asp:Label ID="lblListeProjeler" runat="server" Text="" />
<div class="pagination" style="display:none;">
    <asp:Label ID="lblSayfaNumaralari" runat="server" Text="" />
</div>
<asp:Label ID="lblSectionEnd" runat="server" Text="" />


     <script src="js/jquery.slides.min.js"></script>

      <script>
          $(function () {
              
              $('.slides2').slidesjs({
                  width: 100,
                  height: 100,
                  start: 3,
                  play: {
                      auto: true
                  },
                  pagination: false
              });           
          });
  </script>


</form>

</asp:Content>

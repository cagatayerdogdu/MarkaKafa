<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GrafikerPortal._default1" EnableViewState="false" ViewStateEncryptionMode="Never" ViewStateMode="Disabled" EnableViewStateMac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet"  href="lightslider/css/lightslider.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="display: inline; float: left; margin-left: 90px; margin-top: 40px;">
        <a style="display:none;" href="markakafa_nedir.aspx">
            <img src="img/004.png" style="width:150px;" />
        </a>

        <div style="width: 300px; margin-left:-40px;">
            <ul id="markakafa-nedir-slider" class="markakafa-nedir-slider">
                <li>
                    <a href="markakafa_nedir.aspx">
                        <img src="img/slide_mn_1.png" alt="" />
                    </a>
                </li>
                <li>
                    <a href="markakafa_nedir.aspx">
                        <img src="img/slide_mn_2.png" alt="" />
                    </a>
                </li>
                <li>
                    <a href="markakafa_nedir.aspx">
                        <img src="img/slide_mn_3.png" alt="" />
                    </a>
                </li>
                <li>
                    <a href="markakafa_nedir.aspx">
                        <img src="img/slide_mn_4.png" alt="" />
                    </a>
                </li>
                <li>
                    <a href="markakafa_nedir.aspx">
                        <img src="img/slide_mn_5.png" alt="" />
                    </a>
                </li>
                <%--<li>
                    <a href="markakafa_nedir.aspx">
                        <img src="img/slide_mn_6.png" alt="" />
                    </a>
                </li>--%>
            </ul>
        </div>
    </div>

     <div class="buyuk_logo">
     <a href="markakafa_nedir.aspx">
     </a>
     </div>
     
     <div class="ihtiyacini_sec">  
     <a href="yeni_marka.aspx">  </a>
     </div>
<%--    
    <ul class="bxslider" >
        <li><img src="bxslider/images/BANNER1.png" /></li>
        <li><img src="bxslider/images/BANNER2.png" /></li>
        <li><img src="bxslider/images/BANNER3.png" /></li>
    </ul>--%>
    
    <script src="lightslider/js/lightslider.js"></script> 
    <script>
        $(document).ready(function () {
            $("#markakafa-nedir-slider").lightSlider({
                item: 1,
                auto: true,
                loop: true,
                speed: 1000,
                slideMargin: 0,
                keyPress: true,
                pause:4000
            });
        });
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="yeni_marka.aspx.cs" Inherits="GrafikerPortal.yeni_marka" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style>
  label {
    display: inline-block;
    width: 5em;
}

/*
  body {
	font-family: "Trebuchet MS", "Helvetica", "Arial",  "Verdana", "sans-serif";
	font-size: 62.5%;
  }
  */
  </style>
  <script type="text/javascript">
      function balonDegistir() {
          var divBalon1 = document.getElementById('divBalon1');
          var divBalon2 = document.getElementById('divBalon2');
          if (divBalon1.style.display != 'none') {
              divBalon1.style.display = 'none';
              divBalon2.style.display = '';
          }
          else if (divBalon2.style.display != 'none') {
              divBalon1.style.display = '';
              divBalon2.style.display = 'none';
          }
      }
  </script>

    
    <link rel="stylesheet"  href="lightslider/css/lightslider.css"/>
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
                pause: 4000
            });
        });
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


   <table>
   <tr valign="top">
   <td> 
           <%--<img src="img/004.png" style="width:200px; margin-left:100px;" />  --%>

               <div style="width: 300px; margin-left:10px; margin-top:50px;">
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

   </td>
   <td  valign="top">   
       
      
          
   <div style="padding-left:120px;" id="divBalon1">
      
     <div class="balon_dis" style="z-index:2; position:absolute; margin-left:63px; ">
      <div class="balon_100">
       <a runat="server" id="aBalon1" href="yeni_marka_1.aspx?t=24" title=""> davetiye</a>
      </div>
     </div>
     
      <div class="balon_dis" style="margin-left: 234px;
    margin-top: 20px;
    position: absolute;
    z-index: 2;">
      <div class="balon_90">
       <a runat="server" id="aBalon2" href="yeni_marka_1.aspx?t=50" style="font-size:13px;"  title="300 TL - 1200 TL"> kurumsal <br /> obje </a>
      </div>
     </div>
     
      <div class="balon_dis" style="margin-left: 261px;
    margin-top: 107px;
    position: absolute;
    z-index: 2;">
      <div class="balon_110">
       <a runat="server" id="aBalon3" href="yeni_marka_1.aspx?t=46" style="font-size:17px;"  title="300 TL - 1200 TL"> katalog  </a>
      </div>
     </div>
     
       <div class="balon_dis" style="margin-left: 324px;
    margin-top: 10px;
    z-index: 2;">
      <div class="balon_120">
       <a runat="server" id="aBalon4" href="yeni_marka_1.aspx?t=31" style="font-size:17px;"  title="300 TL - 1200 TL"> fuar <br /> standı  </a>
      </div>
     </div>
     
      <div class="balon_dis" style="    margin-left: 3px;
    margin-top: 35px;
    z-index: 2;">
      <div class="balon_120">
       <a runat="server" id="aBalon5" href="yeni_marka_1.aspx?t=10" style="font-size:17px;"  title="300 TL - 1200 TL"> broşür  </a>
      </div>
     </div>
     
      <div class="balon_dis" style="  margin-left: 487px;
    margin-top: -3px;
    z-index: 2;">
      <div class="balon_130">
       <a runat="server" id="aBalon6" href="yeni_marka_1.aspx?t=12" style="font-size:17px;"  title="300 TL - 1200 TL"> afiş <br /> poster  </a>
      </div>
     </div>
     

    <div class="balon_dis" style=" z-index:1; position:absolute;  margin-top:66px; ">
           <div class="balon_120">
              <a runat="server" id="aBalon7" href="yeni_marka_1.aspx?t=33"  title="300 TL - 1200 TL"> web <br />tasarımı </a>
           </div>
    </div>
        
    <div class="balon_dis" style="z-index:3; position:absolute; margin-top:170px; margin-left:-40px; ">
      <div class="balon_100">
       <a runat="server" id="aBalon8" href="yeni_marka_1.aspx?t=51"  title="300 TL - 1200 TL"> obje tasarımı</a>
      </div>
     </div>
     
    <div class="balon_dis" style="margin-left: 106px;  margin-top: 61px;    position: absolute;    z-index: 1; ">
      <div class="balon_160">
       <a runat="server" id="aBalon9" href="yeni_marka_1.aspx?t=17"  title="300 TL - 1200 TL"> marka</a>
      </div>
     </div>
     
     <div class="balon_dis" style="margin-left: 338px;
    margin-top: 145px;
    position: absolute;
    z-index: 1; ">
      <div class="balon_180">
       <a runat="server" id="aBalon10" href="yeni_marka_1.aspx?t=5" style="line-height:1;"  title="300 TL - 1200 TL"> logo <br /> ve <br /> kartvizit</a>
      </div>
     </div>
     
      <div class="balon_dis" style="  margin-left: 56px; margin-top: 174px;
    position: absolute;
    z-index: 0;">
           <div class="balon_140">
              <a runat="server" id="aBalon11" href="yeni_marka_1.aspx?t=22"  title="300 TL - 1200 TL"> maskot </a>
           </div>
    </div>
    
      <div class="balon_dis" style="margin-left: 190px;
    margin-top: 206px;
    position: absolute;
    z-index: 0;">
           <div class="balon_130">
              <a runat="server" id="aBalon12" href="yeni_marka_1.aspx?t=6" style="font-size:19px;" title="300 TL - 1200 TL"> kurumsal <br /> kimlik </a>
           </div>
    </div>
    
    <div class="balon_dis" style="   margin-left: 309px;
    margin-top: 297px;
    position: absolute;
    z-index: 0;">
           <div class="balon_110">
              <a runat="server" id="aBalon13" href="yeni_marka_1.aspx?t=18" style="font-size:19px;" title="300 TL - 1200 TL"> slogan  </a>
           </div>
    </div>
    
    <div class="balon_dis" style="  margin-left: 413px;
    margin-top: 311px;
    position: absolute;
    z-index: 0;">
           <div class="balon_120">
              <a runat="server" id="aBalon14" href="yeni_marka_1.aspx?t=52" style="font-size:13px;" title="300 TL - 1200 TL"> digital baskı <br /> ve uygulama  </a>
           </div>
    </div>
    
      <div class="balon_dis" style=" margin-left: -5px;
    margin-top: 280px;
    position: absolute;
    z-index: 0;">
      <div class="balon_110">
       <a runat="server" id="aBalon15" href="yeni_marka_1.aspx?t=23" title="300 TL - 1200 TL"> menü </a>
      </div>
     </div>     
     
      <div class="balon_dis" style=" margin-left: 128px;
    margin-top: 298px;
    position: absolute;
    z-index: 0;">
      <div class="balon_90">
       <a runat="server" id="aBalon16" href="yeni_marka_1.aspx?t=48" title="300 TL - 1200 TL"> organizasyon </a>
      </div>
     </div>
     
      <div class="balon_dis" style="  margin-left: 243px;
    margin-top: 332px;
    position: absolute;
    z-index: 0;">
      <div class="balon_90">
       <a runat="server" id="aBalon17" href="yeni_marka_1.aspx?t=20" title="300 TL - 1200 TL"> ambalaj <br /> tasarımı </a>
      </div>
     </div>
     
   </div>
   



       
   <div style="padding-left:120px; display:none;" id="divBalon2">
      
     <div class="balon_dis" style="z-index:2; position:absolute; margin-left:63px; ">
      <div class="balon_100">
       <a runat="server" id="aBalon18" href="yeni_marka_1.aspx?t=28"  title="300 TL - 1200 TL"> poşet <br /> tasarımı</a>
      </div>
     </div>
     
      <div class="balon_dis" style="margin-left: 234px;
    margin-top: 20px;
    position: absolute;
    z-index: 2;">
      <div class="balon_90">
       <a runat="server" id="aBalon19" href="yeni_marka_1.aspx?t=15" style="font-size:13px;"  title="300 TL - 1200 TL"> bilboard <br /> reklam </a>
      </div>
     </div>
     
      <div class="balon_dis" style="margin-left: 261px;
    margin-top: 107px;
    position: absolute;
    z-index: 2;">
      <div class="balon_110">
       <a runat="server" id="aBalon20" href="yeni_marka_1.aspx?t=16" style="font-size:17px;"  title="300 TL - 1200 TL"> gazete <br /> ilanı  </a>
      </div>
     </div>
     
       <div class="balon_dis" style="margin-left: 324px;
    margin-top: 10px;
    z-index: 2;">
      <div class="balon_120">
       <a runat="server" id="aBalon21" href="yeni_marka_1.aspx?t=19" style="font-size:17px;"  title="300 TL - 1200 TL"> etiket <br /> tasarımı  </a>
      </div>
     </div>
     
      <div class="balon_dis" style="    margin-left: 3px;
    margin-top: 35px;
    z-index: 2;">
      <div class="balon_120">
       <a runat="server" id="aBalon22" href="yeni_marka_1.aspx?t=21" style="font-size:17px;"  title="300 TL - 1200 TL"> t-shirt <br /> tasarımı  </a>
      </div>
     </div>
     
      <div class="balon_dis" style="  margin-left: 487px;
    margin-top: -3px;
    z-index: 2;">
      <div class="balon_130">
       <a runat="server" id="aBalon23" href="yeni_marka_1.aspx?t=25" style="font-size:17px;"  title="300 TL - 1200 TL"> kitap <br /> kapağı  </a>
      </div>
     </div>
     

    <div class="balon_dis" style=" z-index:1; position:absolute;  margin-top:66px; ">
           <div class="balon_120">
              <a runat="server" id="aBalon24" href="yeni_marka_1.aspx?t=26"  title="300 TL - 1200 TL"> cd kapağı <br />tasarımı </a>
           </div>
    </div>
        
    <div class="balon_dis" style="z-index:3; position:absolute; margin-top:170px; margin-left:-40px; ">
      <div class="balon_100">
       <a runat="server" id="aBalon25" href="yeni_marka_1.aspx?t=27"  title="300 TL - 1200 TL"> araç üstü <br /> tasarımı</a>
      </div>
     </div>
     
    <div class="balon_dis" style="margin-left: 106px;  margin-top: 61px;    position: absolute;    z-index: 1; ">
      <div class="balon_160">
       <a runat="server" id="aBalon26" href="yeni_marka_1.aspx?t=14" title="300 TL - 1200 TL"> el ilanı</a>
      </div>
     </div>
     
     <div class="balon_dis" style="margin-left: 338px;
    margin-top: 145px;
    position: absolute;
    z-index: 1; ">
      <div class="balon_180">
              <a runat="server" id="aBalon27" href="yeni_marka_1.aspx?t=38"  style="line-height:1;" title="300 TL - 1200 TL"> e-posta <br /> şablonu </a>
      </div>
     </div>
     
      <div class="balon_dis" style="  margin-left: 56px; margin-top: 174px;
    position: absolute;
    z-index: 0;">
           <div class="balon_140">
              <a runat="server" id="aBalon28" href="yeni_marka_1.aspx?t=29"  title="300 TL - 1200 TL"> takvim </a>
           </div>
    </div>
    
      <div class="balon_dis" style="margin-left: 190px;
    margin-top: 206px;
    position: absolute;
    z-index: 0;">
           <div class="balon_130">
              <a runat="server" id="aBalon29" href="yeni_marka_1.aspx?t=32" style="font-size:19px;" title="300 TL - 1200 TL"> ana sayfa <br /> tasarımı </a>
           </div>
    </div>
    
    <div class="balon_dis" style="   margin-left: 309px;
    margin-top: 297px;
    position: absolute;
    z-index: 0;">
           <div class="balon_110">
              <a runat="server" id="aBalon30" href="yeni_marka_1.aspx?t=36" style="font-size:19px;" title="300 TL - 1200 TL"> internet <br /> banner  </a>
           </div>
    </div>
    
    <div class="balon_dis" style="  margin-left: 413px;
    margin-top: 311px;
    position: absolute;
    z-index: 0;">
           <div class="balon_120">
       <a runat="server" id="aBalon31" href="yeni_marka_1.aspx?t=37" style="font-size:13px;" title="300 TL - 1200 TL"> sosyal medya <br /> sayfası <br /> tasarımı</a>
           </div>
    </div>
    
      <div class="balon_dis" style=" margin-left: -5px;
    margin-top: 280px;
    position: absolute;
    z-index: 0;">
      <div class="balon_110">
       <a runat="server" id="aBalon32" href="yeni_marka_1.aspx?t=2" title="300 TL - 1200 TL"> magnet </a>
      </div>
     </div>     
     
      <div class="balon_dis" style=" margin-left: 128px;
    margin-top: 298px;
    position: absolute;
    z-index: 0;">
      <div class="balon_90">
       <a runat="server" id="aBalon33" href="yeni_marka_1.aspx?t=3" title="300 TL - 1200 TL"> tabela </a>
      </div>
     </div>
     
      <div class="balon_dis" style="  margin-left: 243px;
    margin-top: 332px;
    position: absolute;
    z-index: 0;">
      <div class="balon_90">
       <a runat="server" id="aBalon34" href="yeni_marka_1.aspx?t=20" title="300 TL - 1200 TL"> radyo spotu <br /> metni </a>
      </div>
     </div>
     
   </div>
   
    <a style="cursor:pointer;" onclick="balonDegistir();" class="btn_oval"> diğer kategorileri gör </a>


   
   </td>
   </tr>
   
   </table>   

    

</asp:Content>

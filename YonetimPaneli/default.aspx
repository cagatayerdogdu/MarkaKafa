<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GrafikerPortal.YonetimPaneli._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />

   <script src="../css/jquery.min.js"></script>
<link href="../css/bootstrap-combined.min.css" rel="stylesheet"> 
<script src="../css/bootstrap.min.js"></script>

<link type="text/css" href="../css/style.css" rel="Stylesheet" />

	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript" charset="utf-8"></script>
	<script src="../font/specimen_files/easytabs.js" type="text/javascript" charset="utf-8"></script>
	<link rel="stylesheet" href="../font/specimen_files/specimen_stylesheet.css" type="text/css" charset="utf-8" />
	<link rel="stylesheet" href="../font/stylesheet.css" type="text/css" charset="utf-8" />
    <link rel="stylesheet" href="../bxslider/jquery.bxslider.css" type="text/css" charset="utf-8" />
    <script src="../bxslider/jquery.bxslider.min.js"></script>
    <link rel="shortcut icon" href="../img/logo.ico" >

	<script type="text/javascript" charset="utf-8">
	    $(document).ready(function () {
	        $('#container').easyTabs({ defaultContent: 1 });
	    });
	</script>

    <script type="text/javascript">
        function menuUyeOl() {
            document.getElementById('action_type_main').value = 's';
            document.getElementById('frmMainForm').submit();
        }
	</script>

    <script type="text/javascript">
        function loginGonder(e, tip) {
            var olay = document.all ? window.event : e;
            var tus = document.all ? olay.keyCode : olay.which;
            //alert(tus);
            if (tus == 13) {
                switch (tip) {
                    case 'kayit':
                        menuUyeOl();
                        break;
                    case 'giris':
                        document.getElementById('frmMainForm').submit();
                        break;
                }
            }
        }
    </script>


    <title>Yönetim Paneli</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="display:none;">
    
        <br />
        
        <asp:Label ID="lblKullaniciAdi" runat="server" Text="Kullanıcı Adı"></asp:Label> 
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtKullaniciAdi" runat="server"></asp:TextBox> <br />
        <asp:Label ID="lblSifre" runat="server" Text="Şifre"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" Width="122px"></asp:TextBox>
        <br />        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Button 
            ID="btnGiris" runat="server" Text="Giriş" onclick="btnGiris_Click" />
    
    </div>

        
        <div id="form-login" style="background-color:#444444; border:0px; width:336px; background-color:#444444; border:0px; -webkit-border-radius: 50px;-moz-border-radius: 50px; border-radius: 50px; margin-left:503px;">
		<div>
			<fieldset style="border:0px;">
		         <div class="modal-body">
		        	 <ul class="nav nav-list1">
				<li class="nav-header1 fonts" style="float:left;">Kullanıcı adı</li>
				<li><input runat="server" id="txtKullaniciAdi2" clientidmode="Static" class="txt" value="" type="text" name="name" /></li>
                
                 <div style="height:10px;">  </div>
                 
                <li class="nav-header1" style="float:left;">Şifre</li>
				<li><input runat="server" id="txtSifre2" clientidmode="Static" class="txt" value="" type="password" name="" /></li>
			
     
				</ul> 

              
                <asp:Button ID="btnGiris3" runat="server" Text="giriş" class="ozel_buton_yuvarlak" name="commit" onclick="btnGiris_Click" ClientIDMode="Static" style="padding-left: 40px; margin-left: 40px; text-align: left; cursor:pointer;"></asp:Button>
		        </div>
			</fieldset>
			
		</div>
	     
	</div>

        
     <div class="buyuk_logo_YP"> <a> </a>
     </div>

    </form>
</body>
</html>

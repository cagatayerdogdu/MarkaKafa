<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="faturaduzenleodeme.aspx.cs" Inherits="GrafikerPortal.faturaduzenleodeme" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />

    <script src="css/jquery.min.js"></script>
    <link href="css/bootstrap-combined.min.css" rel="stylesheet"> 
    <script src="css/bootstrap.min.js"></script>
    
    <link type="text/css" href="css/style.css" rel="Stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
  
	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
	<script src="font/specimen_files/easytabs.js" type="text/javascript" charset="utf-8"></script>
	<link rel="stylesheet" href="font/specimen_files/specimen_stylesheet.css" type="text/css" charset="utf-8" />
	<link rel="stylesheet" href="font/stylesheet.css" type="text/css" charset="utf-8" />
    <link rel="stylesheet" href="bxslider/jquery.bxslider.css" type="text/css" charset="utf-8" />
    <script src="bxslider/jquery.bxslider.min.js"></script>
    <script src="js/alertify.js"></script>
    <link rel="stylesheet" href="css/alertify.core.css" />
    <link rel="stylesheet" href="css/alertify.default.css" />
    <link rel="shortcut icon" href="/img/logo.ico" >
    
    <script>
        function kapat() {
            setTimeout(function () {
                alert('Kayıt işlemi başarıyla gerçekleştirildi.');
                parent.location = 'yeni_marka_3.aspx';
            }, 1000);
        }
    </script>
    <script>
        function faturaYukle() {
            var ddlKayitliFaturalar = document.getElementById('ddl_kayitli_faturalar');
            //var ddlKayitliFaturalar = $('#ddl_kayitli_faturalar');
            var projeId = document.getElementById('hfProjeID').value;
            var faturaId = ddlKayitliFaturalar.options[ddlKayitliFaturalar.selectedIndex].value;
            if (ddlKayitliFaturalar.selectedIndex == 0)
                faturaId = '';

            document.location = 'faturaduzenleodeme.aspx?id=' + faturaId + '&p=' + projeId;
        }
    </script>
    
    <script type="text/javascript">
        function faturaBilgileriKontrol(faturaAktif) {
            document.getElementById('projects_invoice_detail_attributes_title').disabled = faturaAktif;
            document.getElementById('projects_invoice_detail_attributes_company_name').disabled = faturaAktif;
            document.getElementById('projects_invoice_detail_attributes_tax_office').disabled = faturaAktif;
            document.getElementById('projects_invoice_detail_attributes_tax_number').disabled = faturaAktif;
            document.getElementById('projects_invoice_detail_attributes_address_line_1').disabled = faturaAktif;
            document.getElementById('projects_invoice_detail_attributes_city').disabled = faturaAktif;
            document.getElementById('projects_invoice_detail_attributes_postal_code').disabled = faturaAktif;
            document.getElementById('projects_invoice_detail_attributes_phone').disabled = faturaAktif;
        }
    </script>

    <style type="text/css">
        .gridFatura div
        {
            height:40px;
        }
        .gridFatura .txtKucuk
        {
            float:right;
        }
    </style>
</head>
<body style="background-image:none;" onload="faturaBilgileriKontrol(document.getElementById('hfFaturaID').value != '');">
    <form id="form1" runat="server">
        
        <input type="hidden" runat="server" id="hfFaturaID" value=""/>
        <input type="hidden" runat="server" id="hfProjeID" value=""/>

        <div>
            <!-- UYARI BÖLÜMÜ -->
            <div runat="server" id="divErrorProje" visible="false" class="error">
                <p>
                    <strong>
                        Lütfen aşağıdaki hataları düzeltiniz:
                    </strong>
                </p>
                <div>
                    <ul>
                        <li runat="server" id="liFaturaAdSoyad" class="message" visible="false">
                            Şirket ünvanı (veya ad, soyad) doldurulmalı.
                        </li>
                        <li runat="server" id="liFaturaVergiNumarasi" class="message" visible="false">
                            Vergi numarası (yoksa T.C. kimlik no) /T.C. kimlik no bilgisi eksik.
                        </li>
                        <li runat="server" id="liFaturaAdres" class="message" visible="false">
                            Adres doldurulmalı.
                        </li>
                        <li runat="server" id="liFaturaSehir" class="message" visible="false">
                            Şehir doldurulmalı.
                        </li>
                        <li runat="server" id="liFaturaTelefon" class="message" visible="false">
                            Telefon doldurulmalı.
                        </li>
                        <li runat="server" id="liError" class="message" visible="false">
                            Proje oluşturulurken bir hata meydana geldi. Lütfen bilgilerinizi kontrol ederek tekrar deneyin.
                        </li>
                    </ul>
                </div>
            </div>
            <!-- UYARI BÖLÜMÜ BİTTİ -->




            <div class='clear'></div>
            <input id="projects_invoice_detail_attributes_campaign_code" name="project[invoice_detail_attributes][campaign_code]" type="hidden" value="" />
            <input id="projects_invoice_detail_attributes_campaign_info" name="project[invoice_detail_attributes][campaign_info]" type="hidden" value="" />
            <input id="projects_invoice_detail_attributes_coupon_id" name="project[invoice_detail_attributes][coupon_id]" type="hidden" />
            <div class='gridFatura' style="width:500px;">
                <input name="project[invoice_detail_attributes][remind_me_later]" type="hidden" value="asd" />
                
                <div class='input'>
                    <label for="ddl_kayitli_faturalar">Kayıtlı faturalardan seçim:</label>
                    <asp:DropDownList ID="ddl_kayitli_faturalar" runat="server" ClientIDMode="Static" class="txtKucuk" onchange="faturaYukle();" style="width: 181px; height: 22px;"></asp:DropDownList>
                </div>
                <div class='input'>
                    <label for="projects_invoice_detail_attributes_title">Başlık</label>
                    <input runat="server" id="projects_invoice_detail_attributes_title" name="project[invoice_detail_attributes][title]" type="text" value="" clientidmode="Static" class="txtKucuk" style="display:inline;" />
                </div>
                <div class='input'>
                    <label for="project_invoice_detail_attributes_company_name">Şirket ünvanı (veya ad, soyad)</label>
                    <input runat="server" id="projects_invoice_detail_attributes_company_name" name="project[invoice_detail_attributes][company_name]" type="text" value="" clientidmode="Static" class="txtKucuk" style="display:inline;" />
                </div>
                <div class='input float_right'>
                    <label for="project_invoice_detail_attributes_tax_number">Vergi numarası (yoksa T.C. kimlik no)</label>
                    <input runat="server" id="projects_invoice_detail_attributes_tax_number" name="project[invoice_detail_attributes][tax_number]" type="text" value="" clientidmode="Static" class="txtKucuk" style="display:inline;" />
                </div>
                <div class='input'>
                    <label for="project_invoice_detail_attributes_tax_office">Vergi dairesi (firmalar için)</label>
                    <input runat="server" id="projects_invoice_detail_attributes_tax_office" name="project[invoice_detail_attributes][tax_office]" type="text" value="" clientidmode="Static" class="txtKucuk" style="display:inline;" />
                </div>
                <div class='input'>
                    <label for="project_invoice_detail_attributes_address_line_1">Adres</label>
                    <input runat="server" id="projects_invoice_detail_attributes_address_line_1" name="project[invoice_detail_attributes][address_line_1]" type="text" value="" clientidmode="Static"  class="address_line txtKucuk" style="display:inline;"/>
                </div>
                <div class='input float_right inline_input'>
                    <label for="project_invoice_detail_attributes_postal_code">P.K.</label>
                    <input runat="server" id="projects_invoice_detail_attributes_postal_code" name="project[invoice_detail_attributes][postal_code]" type="text" value="" clientidmode="Static" class="txtKucuk" style="display:inline;" />
                </div>
                <div class='input inline_input'>
                    <label for="project_invoice_detail_attributes_city">Şehir</label>
                    <input runat="server" id="projects_invoice_detail_attributes_city" name="project[invoice_detail_attributes][city]" type="text" value="" clientidmode="Static" class="txtKucuk" style="display:inline;" />
                </div>
                <div class='input'>
                    <label for="project_invoice_detail_attributes_phone">Telefon</label>
                    <input runat="server" id="projects_invoice_detail_attributes_phone" name="project[invoice_detail_attributes][phone]" type="text" value="" clientidmode="Static" class="txtKucuk" style="display:inline;" />
                </div>
            </div>
            <div class='clear'></div>
            <div class='submit'>
                <asp:Button ID="btnKaydet" runat="server" Text="Kaydet / Devam" class="ozel_buton_kucuk" name="commit" onclick="btnKaydet_Click" ClientIDMode="Static" style="margin-left: 150px;"></asp:Button>
            </div>

        </div>
    </form>
</body>
</html>

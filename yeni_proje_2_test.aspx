<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="yeni_proje_2.aspx.cs" Inherits="GrafikerPortal.yeni_proje_2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="js/markafiyat.js"></script>
<link rel="stylesheet" href="css/markafiyat.css" />

<script type="text/javascript">
    function fiyatHesapla() {
        try {
            if (tryParseInt(document.getElementById('projects_campaign_prize').value)) {
                var odul = parseInt(document.getElementById('projects_campaign_prize').value);
                var fiyatProjeGizli = parseInt(document.getElementById('hfCostProjectHidden').value);
                var fiyatProjeKapali = parseInt(document.getElementById('hfCostProjectPrivate').value);
                var fiyatProjeAramaMotoru = parseInt(document.getElementById('hfCostProjectHideSearch').value);
                var fiyatProjeListeBasi = parseInt(document.getElementById('hfCostProjectTop').value);
                var fiyatProjeArkaplan = parseInt(document.getElementById('hfCostProjectHighlight').value);
                var fiyatProjeBold = parseInt(document.getElementById('hfCostProjectBold').value);
                var hizmetBedeliYuzde = 15;
                var kdvYuzde = 18;
                //Hizmet bedeli hesaplanıyor
                var toplamHizmetBedeli = odul * (hizmetBedeliYuzde / 100);
                toplamHizmetBedeli = toplamHizmetBedeli + Math.round(toplamHizmetBedeli * (kdvYuzde / 100));
                //Ekstralar hesaplanıyor
                var toplamEkstra = 0;
                if (document.getElementById('projects_hidden_contest').checked) toplamEkstra += fiyatProjeGizli;
                if (document.getElementById('projects_private').checked) toplamEkstra += fiyatProjeKapali;
                if (document.getElementById('projects_hide_on_search').checked) toplamEkstra += fiyatProjeAramaMotoru;
                if (document.getElementById('projects_publish_on_top').checked) toplamEkstra += fiyatProjeListeBasi;
                if (document.getElementById('projects_publish_highlighted').checked) toplamEkstra += fiyatProjeArkaplan;
                if (document.getElementById('projects_publish_bold').checked) toplamEkstra += fiyatProjeBold;
                toplamEkstra = toplamEkstra + Math.round(toplamEkstra * (kdvYuzde / 100));
                //Genel toplam hesaplanıyor
                var genelToplam = odul + toplamHizmetBedeli + toplamEkstra;
                odul = odul.toFixed(2);
                toplamHizmetBedeli = toplamHizmetBedeli.toFixed(2);
                toplamEkstra = toplamEkstra.toFixed(2);
                genelToplam = genelToplam.toFixed(2);
                //Sonuç ekrana yazdırılıyor
                document.getElementById('spanResultPrize').innerHTML = odul.toString();
                document.getElementById('spanResultServiceCost').innerHTML = toplamHizmetBedeli.toString();
                document.getElementById('spanResultExtra').innerHTML = toplamEkstra.toString();
                document.getElementById('spanResultTotal').innerHTML = genelToplam.toString();
            }
        } catch (hata) { alert(hata.Message); }
    }
</script>
<script type="text/javascript">
    function projeGizlilikSecenekKontrol() {
        document.getElementById('projects_hidden_contest').disabled = false;
        document.getElementById('projects_private').disabled = false;
        document.getElementById('projects_hide_on_search').disabled = false;

        if (document.getElementById('projects_hidden_contest').checked) {
            document.getElementById('projects_private').checked = false;
            document.getElementById('projects_private').disabled = true;
            document.getElementById('projects_hide_on_search').checked = false;
            document.getElementById('projects_hide_on_search').disabled = true;
        }
        else if (document.getElementById('projects_private').checked || document.getElementById('projects_hide_on_search').checked) {
            document.getElementById('projects_hidden_contest').checked = false;
            document.getElementById('projects_hidden_contest').disabled = true;
        }
    }
</script>
<script type="text/javascript">
    function faturaBilgileriKontrol(faturaAktif) {
        document.getElementById('projects_invoice_detail_attributes_company_name').disabled = faturaAktif;
        document.getElementById('projects_invoice_detail_attributes_tax_office').disabled = faturaAktif;
        document.getElementById('projects_invoice_detail_attributes_tax_number').disabled = faturaAktif;
        document.getElementById('projects_invoice_detail_attributes_address_line_1').disabled = faturaAktif;
        document.getElementById('projects_invoice_detail_attributes_city').disabled = faturaAktif;
        document.getElementById('projects_invoice_detail_attributes_postal_code').disabled = faturaAktif;
        document.getElementById('projects_invoice_detail_attributes_phone').disabled = faturaAktif;
        if (document.getElementById('projects_invoice_detail_attributes_company_name').disabled)
            document.getElementById('projects_invoice_detail_attributes_company_name').value = '';
        if (document.getElementById('projects_invoice_detail_attributes_tax_office').disabled)
            document.getElementById('projects_invoice_detail_attributes_tax_office').value = '';
        if (document.getElementById('projects_invoice_detail_attributes_tax_number').disabled)
            document.getElementById('projects_invoice_detail_attributes_tax_number').value = '';
        if (document.getElementById('projects_invoice_detail_attributes_address_line_1').disabled)
            document.getElementById('projects_invoice_detail_attributes_address_line_1').value = '';
        if (document.getElementById('projects_invoice_detail_attributes_city').disabled)
            document.getElementById('projects_invoice_detail_attributes_city').value = '';
        if (document.getElementById('projects_invoice_detail_attributes_postal_code').disabled)
            document.getElementById('projects_invoice_detail_attributes_postal_code').value = '';
        if (document.getElementById('projects_invoice_detail_attributes_phone').disabled)
            document.getElementById('projects_invoice_detail_attributes_phone').value = '';
    }
</script>
<script type="text/javascript">
    function tryParseInt(str) {
        var retValue = false;
        if (typeof str != 'undefined' && str != null && str.length > 0) {
            if (!isNaN(str)) {
                retValue = true;
            }
        }
        return retValue;
    }
</script>
<script type="text/javascript">
	function kaydiracGuncelle(){
		var txtKaydiracFiyat = document.getElementById('txtKaydiracFiyat');
		if (tryParseInt(txtKaydiracFiyat.value)) {
			var fiyat = parseInt(txtKaydiracFiyat.value);
			var minFiyat = parseInt(document.getElementById('hfPrizeMin').value);
			var maxFiyat = parseInt(document.getElementById('hfPrizeMax').value);
			if(fiyat >= minFiyat && fiyat <= maxFiyat){
				document.getElementById('txtKaydiracFiyatGercek').value = txtKaydiracFiyat.value;
				var fiyatYuzde = parseFloat((fiyat - minFiyat) / (maxFiyat - minFiyat) * 100);
				var barUzunluk = 455; var balonGenislik = 45;
				var yeniKonum = parseFloat((fiyatYuzde * barUzunluk) / 100);
				var diziBalonlar = document.getElementsByClassName('range-handle');
				var diziBarlar = document.getElementsByClassName('range-quantity');
				for(var i=0; i<diziBalonlar.length; i++)
					diziBalonlar[i].style.left = yeniKonum + 'px';
				for(var i=0; i<diziBarlar.length; i++)
					diziBarlar[i].style.width = (yeniKonum + balonGenislik) + 'px';
			}
		}
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
    .divTutar
    {  
        width: 435px;
        /*float: right;*/
        vertical-align: top;
        margin-top: -600px;
        /*margin-right: -200px;*/
    }
    .divTutar p
    {
        display: block;
        float: none;
        text-align: left;
    }
</style>
<style type="text/css">
.range-min,
.range-max {
  padding-top: 1px !important;
  color: #fff !important;
}

.range-bar.marka-fiyat {
  background-color: #6b6b6b;
}

.marka-fiyat .range-quantity {
  background-color: #e7038a;
}

.marka-fiyat .range-handle {
  background: url('img/marka_fiyat.png') 0 0 no-repeat;
  border-radius: 0;
  /*width: 25px;*/

  -webkitbox-shadow: none;
  box-shadow: none;
}
/*
.marka-fiyat .range-min {
  background: url('../images/heart_broken.png') 0 0 no-repeat;
}

.marka-fiyat .range-max {
  background: url('../images/heart.png') 0 0 no-repeat;
}
*/
.slider-wrapper {
    margin-left:150px;
    width: 500px;
    height: 50px;
    margin-top: 30px;
  }

    .check
    {
        text-align: left;
        padding-left: 100px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">

<div id="container" style="margin-left:390px;">

<section class='start_a_project' id='start_a_project_prize'>
<div class='start_a_project_step'>
    <div class='steps_container_with_refund'>
        <asp:Label ID="lblAdimlar" runat="server" Text=""></asp:Label>
        <div runat="server" id="divOdemeGarantisi" class='refund_container' visible="false">
            <a href="sistem_nasil_isler.aspx" target="_blank">
                <img alt="Refund small" class="step" height="40px" src="assets/refund_small-28a1784ed386676777e3507aa6e9a61b.png" width="170px" />
            </a>
        </div>
    </div>
</div>

<div class='start_a_project_header'>
    <h1>Ödülünüzü belirleyin...</h1>
    <p>
        Projenizin başlamasına çok az kaldı.
        <br />
        Ödeme ve fatura bilgilerinizi tamamladıktan sonra hayalinizdeki tasarıma kavuşabilirsiniz.
    </p>
</div>
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
            <li runat="server" id="liOdul" class="message" visible="false">
                Ödül bedeli belirtilmelidir.
            </li>
            <li runat="server" id="liOdulAralik" class="message" visible="false">
                Ödül bedeli geçerli aralıkta olmalıdır.
            </li>
            <li runat="server" id="liError" class="message" visible="false">
                Proje oluşturulurken bir hata meydana geldi. Lütfen bilgilerinizi kontrol ederek tekrar deneyin.
            </li>
        </ul>
    </div>
</div>


<div style="margin:0;padding:0;display:inline">
    <input name="utf8" type="hidden" value="&#x2713;" />
    <input name="_method" type="hidden" value="patch" /><input name="authenticity_token" type="hidden" value="7K6enhKVvaJGTndIg8LHcvykhGaestXckkINVZfhYps=" />
</div>
<input runat="server" id="hfPrizeMin" type="hidden" value="0" clientidmode="Static" />
<input runat="server" id="hfPrizeMax" type="hidden" value="0" clientidmode="Static" />
<input runat="server" id="hfCostProjectHidden" type="hidden" value="0" clientidmode="Static" />
<input runat="server" id="hfCostProjectPrivate" type="hidden" value="0" clientidmode="Static" />
<input runat="server" id="hfCostProjectHideSearch" type="hidden" value="0" clientidmode="Static" />
<input runat="server" id="hfCostProjectTop" type="hidden" value="0" clientidmode="Static" />
<input runat="server" id="hfCostProjectHighlight" type="hidden" value="0" clientidmode="Static" />
<input runat="server" id="hfCostProjectBold" type="hidden" value="0" clientidmode="Static" />
<input runat="server" id="hfBonusCoupon" type="hidden" value="0" clientidmode="Static" />
<input id='warn-message' type='hidden' value='Proje bilgileriniz kaydedildi!&#x000A;&#x000A;Proje taslağınıza ulaşmak ve kaldığınız yerden devam etmek için sayfanın sağ üst köşesindeki isminize tıklayıp "Projelerim" bağlantısını seçmeniz yeterli.' />
<input id="projects_coupon_id" name="project[coupon_id]" type="hidden" />
<h2 class='heading'>Ödül</h2>
<p>Belirleyeceğiniz ödül miktarı projenize katılan tasarımcı sayısını ve kalitesini doğrudan etkiler. Ne kadar yüksek ödül verirseniz projenize gösterilen ilgi o derece artacaktir.</p>
<p><asp:Label ID="lblFiyatBilgisi" runat="server" Text=""></asp:Label></p>
<div class='grid_6'>
    <div id='left'>
        <div class='input' id='prize'>
            <div style="display:none;">
                <input runat="server" id="projects_campaign_prize" name="project[campaign_prize]" type="text" value="" onkeyup="fiyatHesapla();" clientidmode="Static" class="txtKucuk" style="display:inline;" />
                <span>TL</span>
            </div>
            <br />
            <div class="slider-wrapper">
                <input type="text" id="txtKaydiracFiyatGercek" class="js-customized" />
            </div>
            <a id="test" style="display:none;">TEST</a>
        </div>
		<div class='check professional'>
			<input type="text" id="txtKaydiracFiyat" class="js-customized txtKucuk" onkeyup="kaydiracGuncelle();" style="margin-left:210px;" />
			<br />
		</div>
        <div class='check professional'>
                <input runat="server" id="projects_hidden_contest" name="pro_contest" type="checkbox"  class="regular-checkbox" value="1" onclick="fiyatHesapla(); projeGizlilikSecenekKontrol();" clientidmode="Static" />
                <label style="margin-top:-10px" for="projects_hidden_contest"></label>

            <label for="project_hidden_contest">Projenizin gizlenmesi için (Proje detaylarınızı sadece “gizlilik sözleşmesi” imzalayan tasarımcılar görebililir ve katılan tasarımları sadece siz görebilirsiniz)</label>
            <strong>+<asp:Label ID="lblCostProjectHidden" runat="server" Text="229"/> TL</strong>
        </div>
        <br />
        <div class='check private_gallery'>
            <input runat="server" id="projects_private" name="project[hidden_gallery]" type="checkbox" class="regular-checkbox" value="1" onclick="fiyatHesapla(); projeGizlilikSecenekKontrol();" clientidmode="Static" />
            <label style="margin-top:-10px" for="projects_private"></label>
            <label for="project_private">Projenize katılan tasarımları sadece sizin görebilmeniz için</label>
            <strong>+<asp:Label ID="lblCostProjectPrivate" runat="server" Text="169"/> TL</strong>
        </div>
        <br />
        <div class='check hide_on_search'>
            <input runat="server" id="projects_hide_on_search" name="project[hide_on_search]" type="checkbox"  class="regular-checkbox" value="1" onclick="fiyatHesapla(); projeGizlilikSecenekKontrol();" clientidmode="Static" />
            <label style="margin-top:-10px" for="projects_hide_on_search"></label>
            <label for="project_hide_on_search">Projenizin arama motorlarından gizlenmesi için</label>
            <strong>+<asp:Label ID="lblCostProjectHideSearch" runat="server" Text="69"/> TL</strong>
        </div>
        <br />
        <div class='check'>
            <input runat="server" id="projects_publish_on_top" name="project[publish_on_top]" type="checkbox"  class="regular-checkbox" value="1" onclick="fiyatHesapla();" clientidmode="Static" />
            <label style="margin-top:-10px" for="projects_publish_on_top"></label>
            <label for="project_publish_on_top">Projenizin listenin en üstünde yer alması için</label>
            <strong>+<asp:Label ID="lblCostProjectTop" runat="server" Text="69"/> TL</strong>
        </div>
        <br />
        <div class='check'>
            <input runat="server" id="projects_publish_highlighted" name="project[publish_highlighted]" type="checkbox"  class="regular-checkbox" value="1" onclick="fiyatHesapla();" clientidmode="Static" />
            <label style="margin-top:-10px" for="projects_publish_highlighted"></label>
            <label for="project_publish_highlighted">Projenizin arka planının farklı renkte olması ve dikkat çekmesi için</label>
            <strong>+<asp:Label ID="lblCostProjectHighlight" runat="server" Text="39"/> TL</strong>
        </div>
        <br />
        <div class='check'>
            <input runat="server" id="projects_publish_bold" name="project[publish_bold]" type="checkbox" value="1"  class="regular-checkbox" onclick="fiyatHesapla();" clientidmode="Static" />
            <label style="margin-top:-10px" for="projects_publish_bold"></label>
            <label for="project_publish_bold">Projenizin kalın harflerle (bold) dikkat çekmesi için</label>
            <strong>+<asp:Label ID="lblCostProjectBold" runat="server" Text="19"/> TL</strong>
        </div>
        <br />
    </div>
</div>
<div class='grid_6' style="display:none;">
    <div id='campaigns'></div>
    <div class='promo_code'>
        <h2 class='heading'>Kupon Koduna Sahipseniz Buraya Giriniz:</h2>
        <div id='promo_code_error_area'></div>
        <div class='input inline_input' style="text-align:center;">
            <input runat="server" id="coupon_no_area" name="coupon_no_area" type="text" clientidmode="Static" class="txtKucuk" style="margin-left:325px;" />
        </div>
        <div style="display:none;" class='submit'>
            <input class="orange" name="commit" onclick="$(&#39;input[name=coupon_no]&#39;).val($(&#39;#coupon_no_area&#39;).val()); $(&#39;input[name=commit_discount_coupon]&#39;).click(); return false;" type="submit" value="Kullan" />
        </div>
    </div>
</div>
<div class='clear'></div>
<h2 class='heading'>Fatura Bilgileri</h2>
<input id="projects_invoice_detail_attributes_campaign_code" name="project[invoice_detail_attributes][campaign_code]" type="hidden" value="" />
<input id="projects_invoice_detail_attributes_campaign_info" name="project[invoice_detail_attributes][campaign_info]" type="hidden" value="" />
<input id="projects_invoice_detail_attributes_coupon_id" name="project[invoice_detail_attributes][coupon_id]" type="hidden" />
<div class='gridFatura' style="width:500px; margin-left:175px;">
    <div class='remind_me_later'>
        <input name="project[invoice_detail_attributes][remind_me_later]" type="hidden" value="asd" />
        <input runat="server" id="projects_invoice_detail_attributes_remind_me_later" name="project[invoice_detail_attributes][remind_me_later]" onclick="faturaBilgileriKontrol(this.checked);" type="checkbox" class="regular-checkbox" value="1" clientidmode="Static" />
            <label style="margin-top:-10px" for="projects_invoice_detail_attributes_remind_me_later"></label>
        <label for="project_invoice_detail_attributes_remind_me_later">Fatura bilgilerimi daha sonra doldurmak istiyorum</label>
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
    <asp:Button ID="btnKaydet" runat="server" Text="Kaydet / Devam" class="ozel_buton_kucuk" name="commit" onclick="btnKaydet_Click" ClientIDMode="Static" style="margin-left: 337px; margin-top: 20px; "></asp:Button>
</div>
<!-- FORM PROJE DETAY -->
</section>

</div>

    <div class='sum_information divTutar'>
        <p class='total_without_red_span' id='Xtotal_prize'>
            ödül bedeli: <span><span runat='server' id='spanResultPrize' clientidmode='Static'>0</span> TL</span>
        </p>
        <p class='total_without_red_span' id='Xcommission_with_tax'>
            %15 markakafa hizmet bedeli + KDV: <span><span runat="server" id="spanResultServiceCost" clientidmode="Static">0</span> TL</span>
        </p>
        <p class='total_without_red_span' id='Xlisting_fee_extras_with_tax'>
            ekstralar + KDV: <span><span runat="server" id="spanResultExtra" clientidmode="Static">0</span> TL</span>
        </p>
        <p class='total' id='Xtotal' style="margin-left:30px;">
            Toplam: <span><span runat="server" id="spanResultTotal" clientidmode="Static">0</span> TL</span>
        </p>
    </div>
    
  <script src="js/markafiyat.min.js"></script>
  <script type="text/javascript">
      // Basic customization.
      //var cust = document.querySelector('.js-customized');
      //var initCust = new Powerange(cust, {klass: 'marka-fiyat', min: parseInt($('#hfPrizeMin').val()), max: parseInt($('#hfPrizeMax').val()), start: 1000 });
      var minFiyat = parseInt($('#hfPrizeMin').val());
      var maxFiyat = parseInt($('#hfPrizeMax').val());
      var basFiyat = minFiyat + ((maxFiyat - minFiyat)/2);


      var changeInput = document.querySelector('.js-customized')
        , initChangeInput = new Powerange(changeInput, { klass: 'marka-fiyat', min: minFiyat, max: maxFiyat, start: basFiyat, step: 10 });
      
      changeInput.onchange = function() {
          if(parseInt(changeInput.value) <= minFiyat) {
              changeInput.value = minFiyat.toString();
          }
          //alert('test');
          $('#projects_campaign_prize').val(changeInput.value);
          $('#txtKaydiracFiyat').val(changeInput.value);
          $('.range-handle').html(changeInput.value);
          fiyatHesapla();
          //document.getElementById('range-handle').innerHTML = changeInput.value;
      };
      
      var btntest = document.getElementById('test');
      btntest.onclick = function() {
          console.error('TEST');
          changeInput.value = '600';
          changeInput.degistir('DENEME');
          console.error('TEST2');
      };

      $('#projects_campaign_prize').val(basFiyat.toString());
      $('#txtKaydiracFiyat').val(basFiyat.toString());
      $('.range-handle').html(basFiyat.toString());
      fiyatHesapla();
    </script>
</form>
</asp:Content>




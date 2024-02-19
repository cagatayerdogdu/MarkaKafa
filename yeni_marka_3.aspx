<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="yeni_marka_3.aspx.cs" Inherits="GrafikerPortal.yeni_marka_3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

      .havale_proje_yazi
        {
          color:#acacac;
          font:bold;
        }
      .havale_proje_no
        {
          color:#FFF !important;          
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">

<div style="text-align: left; padding-left: 15px;">
  
<table>
    <tr>
        <td>
            <table>

                <tr>
                    <td>

                        <div style="width: 420px;">
                            <h3 style="font-size: 20px;" class="fonts">Tasarımınızı yayınlayın </h3>
                            <p class="fonts" style="font-size: 12px;">
                                EFT veya havale yoluyla ödemenizi yaptığınız anda tasarımınız yayınlanacak.
                            </p>

                            <h3 style="font-size: 16px; display:none;" class="fonts"> <img src="img/nokta.png" style="width: 16px; margin-top: -4px;" /> kredi kartı </h3>

                        </div>

                        
                        <div runat="server" id="divErrorProje" visible="false" class="error">
                            <p>
                                <strong>
                                    Lütfen aşağıdaki hataları düzeltiniz:
                                </strong>
                            </p>
                            <div>
                                <ul>
                                    <li runat="server" id="liCreditCardName" class="message" visible="false">
                                        Kart üzerindeki isim doldurulmalı.
                                    </li>
                                    <li runat="server" id="liCreditCardNumber1" class="message" visible="false">
                                        1. Kart numarası alanı doldurulmalı.
                                    </li>
                                    <li runat="server" id="liCreditCardNumber2" class="message" visible="false">
                                        2. Kart numarası alanı doldurulmalı.
                                    </li>
                                    <li runat="server" id="liCreditCardNumber3" class="message" visible="false">
                                        3. Kart numarası alanı doldurulmalı.
                                    </li>
                                    <li runat="server" id="liCreditCardNumber4" class="message" visible="false">
                                        4. Kart numarası alanı doldurulmalı.
                                    </li>
                                    <li runat="server" id="liCreditCardSecurityCode" class="message" visible="false">
                                        Güvenlik kodu doldurulmalı.
                                    </li>
                                    <li runat="server" id="liCreditCardInstallment" class="message" visible="false">
                                        Geçerli bir taksit seçeneği seçilmeli.
                                    </li>
                                    <li runat="server" id="liCreditCardError" class="message" visible="false">
                                        İşlem gerçekleştirilirken bir hata oluştu. Lütfen bilgilerinizi kontrol edip tekrar deneyin. Sorun devam ederse bizimle iletişime geçiniz.
                                    </li>
                                </ul>
                            </div>
                        </div>

                        <div runat="server" id="divErrorCreditCard" visible="false" class="error">
                            <p>
                                <strong>
                                    Kredi kartı ile ödeme sistemimiz henüz aktif değildir.
                                </strong>
                            </p>
                        </div>

                        <div runat="server" id="divSuccessCreditCard" visible="false" class="success">
                            <p>
                                <strong>
                                    Ödemeniz başarıyla gerçekleştirilmiş ve projeniz aktifleştirilmiştir.
                                </strong>
                            </p>
                        </div>

                        <div runat="server" id="divNoteEFT" visible="false" class="note_box">
                            <p>
                                Henüz ödeme kaydınıza dair bir ödeme tespit edilmemiştir. Ödemenizi gerçekleştirdikten sonra en geç 2 iş günü içerisinde projeniz başlatılacaktır. 
                            </p>
                            <p>
                                Lütfen ödemeyi yaparken proje numaranızı belirtmeyi unutmayınız.
                            </p>
                        </div>

                        <div runat="server" id="divSuccessEFT" visible="false" class="success">
                            <p>
                                <strong>
                                    Ödeme kaydınız oluşturulmuştur. Projeniz bankadan ödeme onayı alındıktan sonra başlayacaktır. Lütfen açıklama bölümünde proje numaranızı belirtmeyi unutmayınız.
                                </strong>
                            </p>
                        </div>

                        <div runat="server" id="divSuccessPayment" visible="false" class="success">
                            <h2>
                                Projenizin ödemesi gerçekleştirilmiştir.
                            </h2>
                        </div>

                        <br /><br /><br /><br /><br />
                        <img src="img/004.png" style="width:150px;" />

                        <table style="display:none;">
                            <tr>
                                <td>
                                    <b class="fonts" style="font-size: 14px;">kart tipi </b>
                                    <div style="height: 6px;"></div>

                                    <select class="txt3" runat="server" id="payment_detail_card_type" name="payment_detail[card_type]" clientidmode="Static" style="width:100px;">
                                        <option value="visa">Visa</option>
                                        <option value="mastercard">Mastercard</option>
                                    </select>

                                    <div style="height:20px;"></div>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <b class="fonts" style="font-size: 14px;">kart üzerindeki isim</b>
                                    <div style="height: 6px;"></div>

                                    <input runat="server" id="payment_detail_card_holder_name" name="payment_detail[card_holder_name]" clientidmode="Static" class="txt2" value="" type="text" />
                                    <div style="height:20px;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b class="fonts" style="font-size: 14px;">kart numarası</b>
                                    <div style="height: 6px;"></div>

                                    <input runat="server" autocomplete="off" id="card_number_1" clientidmode="Static" class="txt2" value="" type="text" name="payment_detail[card_number_1]" />
                                    <div style="height:20px;">
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <b class="fonts" style="font-size: 14px;">son kulanma tarihi</b>
                                    <div style="height: 6px;"></div>
                                                        
                                    <select runat="server" id="payment_detail_expiration_month" name="payment_detail[expiration_month]" clientidmode="Static" class="txt3" style=" width:50px; float:left;">
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                        <option value="6">6</option>
                                        <option value="7">7</option>
                                        <option value="8">8</option>
                                        <option value="9">9</option>
                                        <option value="10">10</option>
                                        <option value="11">11</option>
                                        <option value="12">12</option>
                                    </select>

                                    <select runat="server" id="payment_detail_expiration_year" name="payment_detail[expiration_year]" clientidmode="Static" class="txt2" style=" width:80px; float:left; margin-left:10px;">
                                        <option value="2015">2015</option>
                                        <option value="2016">2016</option>
                                        <option value="2017">2017</option>
                                        <option value="2018">2018</option>
                                        <option value="2019">2019</option>
                                        <option value="2020">2020</option>
                                        <option value="2021">2021</option>
                                        <option value="2022">2022</option>
                                        <option value="2023">2023</option>
                                        <option value="2024">2024</option>
                                        <option value="2025">2025</option>
                                    </select>
                                    <div style="height:20px;">
                                    </div>
                                </td>
                                <td>
                                    <div style="margin-left:-20px;">
                                        <b class="fonts" style="font-size: 14px; margin-left:-75px;">güvenlik kodu</b>
                                        <div style="height: 6px;"></div>

        <input runat="server" autocomplete="off" id="payment_detail_cvv" maxlength="3" name="payment_detail[cvv]" size="3" type="text" clientidmode="Static" class="txt2" style=" width:35px; float:left; margin-left:-28px; height:24px;" />

                                        <div style="height:20px;"></div>

                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td> <br /><br />
                                    <a class="ozel_buton2" href="javascript:document.getElementById('btnKrediKarti').click();" style=" font-size: 15px; width: 279px; line-height: 1.3; display:none;"> ödemeyi yap tasarımı başlat </a>
                                   <br /><br /> <div style="display:none;"><asp:Button ID="btnKrediKarti" runat="server" Text="Kredi kartı ile öde" class="orange" name="commit" onclick="btnKrediKarti_Click" ClientIDMode="Static"></asp:Button></div>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td>

            <h3 style="font-size: 21px;" class="fonts"> <img src="img/nokta.png" style="width: 16px; margin-top: -4px;" /> havale / eft </h3>
            <div style="clear:both;"></div>
            <img src="img/garanti.png" style="width: 240px;" />
            <div style="clear:both; height:15px;"></div>
            <p>
                Çağatay ERDOĞDU        
            </p>
            <div style="font-size: 16px; height:22px;" class="fonts">   Garanti Bankası </div>
            <div style="font-size: 16px; height: 22px;" class="fonts">  Çağlayan Şubesi </div>
            <div style="font-size: 16px; height: 22px;" class="fonts">  Şube Kodu   : 403 </div>
            <div style="font-size: 16px; height: 22px;" class="fonts">  Hesap No    : 6637457 </div>
            <div style="font-size: 16px; height: 22px;" class="fonts">  Iban No     : Tr23 0006 2000 4030 0006 6374 57 </div>
            <p class="havale_proje_yazi">     
                "Lütfen, açıklama bölümüne ‘<asp:Label ID="lblProjeNo" runat="server" CssClass="havale_proje_no" Text="" ClientIDMode="Static" /> numaralı proje için’ yazın."
            </p> 
            <br />

                <div class="remind_me_later" runat="server" id="FaturaBilgiDoldur">
                <input name="project[invoice_detail_attributes][remind_me_later]" type="hidden" value="asd">
                <input name="chkFaturaDuzenle" type="checkbox" id="chkFaturaDuzenle" data-toggle="modal" data-target="#myModal" onclick="faturaDuzenleOdeme('', document.getElementById('lblProjeNo').innerHTML);" class="regular-checkbox" value="1">
                <label style="margin-top:-10px" for="chkFaturaDuzenle"></label>
                <label for="chkFaturaDuzenle">Fatura bilgilerimi doldurmak istiyorum</label>
                </div>
            <br />
            
            <a class="ozel_buton2" href="javascript:document.getElementById('btnEFT').click();" style=" font-size: 15px; width: 279px; line-height: 1.3;"> EFT yap tasarımı başlat </a>
            <div style="display:none;"><asp:Button ID="btnEFT" runat="server" Text="Kredi kartı ile öde" class="orange" name="commit" onclick="btnEFT_Click" ClientIDMode="Static"></asp:Button></div>

            <br />
            <br />
            <br />
            <br />
            <div class='invoice'>
                <table>
                    <tr>
                        <td class='c1'>Ödül Bedeli</td>
                        <td class='c2'>: <span runat="server" id="spanResultPrize">0</span> TL</td>
                    </tr>
                    <tr>
                        <td class='c1'>Ekstralar</td>
                        <td class='c2'>: <span runat="server" id="spanResultExtra">0</span> TL</td>
                    </tr>
                    <tr>
                        <td class='c1'>%25 MarKaKafa hizmet bedeli + KDV</td>
                        <td class='c2'>: <span runat="server" id="spanResultServiceCost">0</span> TL</td>
                    </tr>
                     <tr>
                        <td class='c1'>Ek süre bedeli</td>
                        <td class='c2'>: <span runat="server" id="spanEkSureBedeli">0</span> TL</td>
                    </tr>
                    <tr>
                        <td class='c1'>Toplam</td>
                        <td class='c2'>: <span runat="server" id="spanResultTotal">0</span> TL</td>
                    </tr>
                </table>
            </div>

        </td>
    </tr>
</table>

    	                <div id="myModal" class="modal fade" style="margin-top:50px; display: none; background-color:#67696B; border:0px; width:600px;display: none; background-color:#67696B; border:0px; -webkit-border-radius: 50px;-moz-border-radius: 50px; border-radius: 50px; left:50%;"  tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		                <div class="modal-header" style="border:0px; background-color:#67696B; border:0px; width:600px;display: none; background-color:#67696B; border:0px; -webkit-border-radius: 50px;-moz-border-radius: 50px; border-radius: 50px;">	
                            <%--<a class="close" data-dismiss="modal">×</a>		--%>
			                <img  style="margin-left:83px;" src="img/logo.png" />
		                </div>
		                <div style="width:600px; height:430px;">
			                <fieldset style="border:0px;">
		                         <div class="modal-body" style="overflow:hidden;">
                                    <iframe id="myFrame" width="500px" height="400px" style="display:none; margin-left:50px; overflow:hidden;" frameborder="0" scrolling="no"></iframe>
		                        </div>
			                </fieldset>
		                </div>
	                </div>

        </div>

</form>

</asp:Content>

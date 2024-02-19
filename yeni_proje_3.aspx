<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="yeni_proje_3.aspx.cs" Inherits="GrafikerPortal.yeni_proje_3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">

<div id="container">

<section class='start_a_project' id='start_a_project_payment'>
<div class='start_a_project_step'>
    <div class='steps_container_with_refund'>
        <asp:Label ID="lblAdimlar" runat="server" Text=""></asp:Label>
        <div runat="server" id="divOdemeGarantisi" class='refund_container'>
            <a href="sistem_nasil_isler.aspx" target="_blank">
                <img alt="Refund small" class="step" height="40px" src="assets/refund_small-28a1784ed386676777e3507aa6e9a61b.png" width="170px" />
            </a>
        </div>
    </div>
</div>

<div class='start_a_project_header'>
    <h1>Ödeme türünüzü seçin...</h1>
    <p>Hayalinizdeki tasarıma kavuşmanıza çok az kaldı. Kredi kartı veya havale yoluyla ödemenizi yapın, projenizi başlatın.</p>
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

<div class='grid_6' id='credit_card'>
    <h2 class='heading'>Kredi kartı ile ödeme</h2>
    <p>Kredi kartı bilgilerinizi girer girmez projeniz başlamış olacak.</p>
    <p style='font-weight:bold;'>Taksit Seçenekleri</p>
    <div class='credit_card_advantage'>
        <div class='bank_logo'>
            <img alt="Axess" src="/assets/axess-50bc747cb1fee870bf6acda2391ac6a0.jpg" />
        </div>
        <div class='advantage_text single_line'>
            Akbank kredi kartlarına özel 3 ve 12 taksit
        </div>
    </div>
    <div class='credit_card_advantage'>
        <div class='bank_logo'>
            <img alt="Bonus" src="/assets/bonus-5060a5c024410991da075d6d5d5e586f.jpg" />
        </div>
        <div class='advantage_text single_line'>
            Bonus kredi kartlarına özel 3 ve 6 taksit
        </div>
    </div>
    <div class='credit_card_advantage'>
        <div class='bank_logo'>
            <img alt="Denizbank" src="/assets/denizbank-6930013ad911ff99824f65758aa25ec9.jpg" />
        </div>
        <div class='advantage_text'>
            Denizbank kredi kartlarına özel 3 ve 6 taksit
            <br />
            (İşletme karta özel 6+6 taksit)
        </div>
    </div>
    <!-- FORM -->
    <div style="margin:0;padding:0;display:inline"><input name="utf8" type="hidden" value="&#x2713;" />
        <input name="authenticity_token" type="hidden" value="PiD51S/aXijCBujfzT1HV5wONpgY0MWrOcFtgIq1QGg=" />
    </div>
    <div class='select grid_3 card_type'>
        <label for="payment_detail_card_type">Kart tipi</label>
        <select runat="server" id="payment_detail_card_type" name="payment_detail[card_type]" clientidmode="Static">
            <option value="visa">Visa</option>
            <option value="mastercard">Mastercard</option>
        </select>
    </div>
    <div class='clear'></div>
    <div class='input' id='holder_name'>
        <label for="payment_detail_card_holder_name">Kart üzerindeki isim</label>
        <input runat="server" id="payment_detail_card_holder_name" name="payment_detail[card_holder_name]" type="text" clientidmode="Static" />
    </div>
    <div class='card_number_area'>
        <div class='input'>
            <label for="payment_detail_card_number">Kart numarası</label>
            <div class='card_number_part_container'>
                <input runat="server" autocomplete="off" id="card_number_1" maxlength="4" name="payment_detail[card_number_1]" size="4" type="text" clientidmode="Static" />
            </div>
            <div class='card_number_keypad_container' id='keypad_1'>
                <div class='keypad'>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        1
                        </div>
                        <div class='keypad_digit'>
                        2
                        </div>
                        <div class='keypad_digit'>
                        3
                        </div>
                    </div>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        4
                        </div>
                        <div class='keypad_digit'>
                        5
                        </div>
                        <div class='keypad_digit'>
                        6
                        </div>
                    </div>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        7
                        </div>
                        <div class='keypad_digit'>
                        8
                        </div>
                        <div class='keypad_digit'>
                        9
                        </div>
                    </div>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        0
                        </div>
                        <div class='keypad_clear_digit'>
                        Temizle
                        </div>
                    </div>
                </div>
            </div>

            <div class='card_number_part_container'>
                <input runat="server" autocomplete="off" id="card_number_2" maxlength="4" name="payment_detail[card_number_2]" size="4" type="text" clientidmode="Static" />
            </div>
            <div class='card_number_keypad_container' id='keypad_2'>
                <div class='keypad'>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        1
                        </div>
                        <div class='keypad_digit'>
                        2
                        </div>
                        <div class='keypad_digit'>
                        3
                        </div>
                    </div>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        4
                        </div>
                        <div class='keypad_digit'>
                        5
                        </div>
                        <div class='keypad_digit'>
                        6
                        </div>
                    </div>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        7
                        </div>
                        <div class='keypad_digit'>
                        8
                        </div>
                        <div class='keypad_digit'>
                        9
                        </div>
                    </div>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        0
                        </div>
                        <div class='keypad_clear_digit'>
                        Temizle
                        </div>
                    </div>
                </div>
            </div>

            <div class='card_number_part_container'>
                <input runat="server" autocomplete="off" id="card_number_3" maxlength="4" name="payment_detail[card_number_3]" size="4" type="text" clientidmode="Static" />
            </div>
            <div class='card_number_keypad_container' id='keypad_3'>
                <div class='keypad'>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        1
                        </div>
                        <div class='keypad_digit'>
                        2
                        </div>
                        <div class='keypad_digit'>
                        3
                        </div>
                    </div>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        4
                        </div>
                        <div class='keypad_digit'>
                        5
                        </div>
                        <div class='keypad_digit'>
                        6
                        </div>
                    </div>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        7
                        </div>
                        <div class='keypad_digit'>
                        8
                        </div>
                        <div class='keypad_digit'>
                        9
                        </div>
                    </div>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        0
                        </div>
                        <div class='keypad_clear_digit'>
                        Temizle
                        </div>
                    </div>
                </div>
            </div>

            <div class='card_number_part_container'>
                <input runat="server" autocomplete="off" id="card_number_4" maxlength="4" name="payment_detail[card_number_4]" size="4" type="text" clientidmode="Static" />
            </div>
            <div class='card_number_keypad_container' id='keypad_4'>
                <div class='keypad'>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        1
                        </div>
                        <div class='keypad_digit'>
                        2
                        </div>
                        <div class='keypad_digit'>
                        3
                        </div>
                    </div>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        4
                        </div>
                        <div class='keypad_digit'>
                        5
                        </div>
                        <div class='keypad_digit'>
                        6
                        </div>
                    </div>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        7
                        </div>
                        <div class='keypad_digit'>
                        8
                        </div>
                        <div class='keypad_digit'>
                        9
                        </div>
                    </div>
                    <div class='keypad_row'>
                        <div class='keypad_digit'>
                        0
                        </div>
                        <div class='keypad_clear_digit'>
                        Temizle
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class='input float_right'>
        <label for="payment_detail_cvv">Güvenlik kodu</label>
        <input runat="server" autocomplete="off" class="short numeric" id="payment_detail_cvv" maxlength="3" name="payment_detail[cvv]" size="3" type="text" clientidmode="Static"  />
    </div><br /><br />
    <div class='select'>
        <label for="payment_detail_expiration">Son kullanma tarihi</label>
        <select runat="server" id="payment_detail_expiration_month" name="payment_detail[expiration_month]" clientidmode="Static">
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
        <select runat="server" runat="server" id="payment_detail_expiration_year" name="payment_detail[expiration_year]" clientidmode="Static">
            <option value="2013">2013</option>
            <option value="2014">2014</option>
            <option value="2015">2015</option>
            <option value="2016">2016</option>
            <option value="2017">2017</option>
            <option value="2018">2018</option>
            <option value="2019">2019</option>
            <option value="2020">2020</option>
            <option value="2021">2021</option>
            <option value="2022">2022</option>
            <option value="2023">2023</option>
        </select>
    </div>
    <div class='input'>
        <label for="payment_detail_payment_type">Taksit seçenekleri</label>
        <div id='payment_types'>
            Lütfen Kart Numaranızı Giriniz...
        </div>
    </div><br /><br />
    <div class='submit'>
        <asp:Button ID="btnKrediKarti" runat="server" Text="Kredi kartı ile öde" class="orange" name="commit" onclick="btnKrediKarti_Click" ClientIDMode="Static"></asp:Button>
    </div>
    <!-- FORM -->
</div>
<div class='grid_6' id='bank_transfer'>
    <h2 class='heading'>Banka havalesi ile ödeme</h2>
    <p>
        Dilerseniz ödemenizi havale veya EFT yolu ile de yapabilirsiniz. Projeniz bankadan ödeme onayı alındıktan sonra başlayacaktır.
    </p>
    <p>
        markakafa Reklam ve Danışmanlık Hizmetleri A.Ş.
        <br />
        Garanti Bankası - Çağlayan Şubesi 403-6637457
        <br />
        IBAN: TR23 0006 2000 4030 0006 6374 57
    </p>
    <p>
        "Lütfen, açıklama bölümüne ‘<asp:Label ID="lblProjeNo" runat="server" Text="" /> numaralı proje için’ yazın."
    </p>

    <!-- FORM -->
    <div style="margin:0;padding:0;display:inline"><input name="utf8" type="hidden" value="&#x2713;" />
        <input name="_method" type="hidden" value="patch" />
        <input name="authenticity_token" type="hidden" value="PiD51S/aXijCBujfzT1HV5wONpgY0MWrOcFtgIq1QGg=" />
    </div>
    <div class='submit'>
        <asp:Button ID="btnEFT" runat="server" Text="Banka havalesi/ EFT  ile öde" class="orange" name="commit" onclick="btnEFT_Click" ClientIDMode="Static"></asp:Button>
    </div>
    <!-- FORM -->

    <div class='invoice'>
        <table>
            <tr>
                <td class='c1'>Ödül Bedeli :</td>
                <td class='c2'><span runat="server" id="spanResultPrize">0</span> TL</td>
            </tr>
            <tr>
                <td class='c1'>Ekstralar</td>
                <td class='c2'><span runat="server" id="spanResultExtra">0</span> TL</td>
            </tr>
            <tr>
                <td class='c1'>%25 markakafa hizmet bedeli + KDV :</td>
                <td class='c2'><span runat="server" id="spanResultServiceCost">0</span> TL</td>
            </tr>
            <tr>
                <td class='c1'>Toplam :</td>
                <td class='c2'><span runat="server" id="spanResultTotal">0</span> TL</td>
            </tr>
        </table>
    </div>
    <div class='clear'></div>
</div>
<div class='clear'></div>
<div class='note_box'>
    <h2>Para iade garantisi nedir?</h2>
    <p>Yüklenen tasarımlardan hiçbirini beğenmezseniz proje sonunda paranızı iade ediyoruz.</p>
    <h2>Ödemeyi neden şimdi yapıyorum?</h2>
    <p>markakafa.com  tasarımcılara proje biter bitmez ödeme garantisi veriyor. Proje başarıyla sonuçlanırsa tasarımcıya sizin adınıza hemen ödeme yapıyoruz. Eğer projenize yüklenen tasarımların hiçbirini beğenmezseniz paranızı iade ediyoruz ama seçtiğimiz bir tasarımcıya sizin adınıza bir teselli ödülü veriyoruz. Bu da daha fazla tasarımcının sizin projeniz için çalışmasına ve sistemimize güvenmesine yarıyor.</p>
    <p>
        Detaylı bilgi için
        <a href="sistem_nasil_isler.aspx" target="_blank">tıklayın</a>
    </p>
</div>
</section>
<input id="payment_type_url_field" name="payment_type_url_field" type="hidden" value="" />
<input id="selected_payment_type" name="selected_payment_type" type="hidden" />
</div>

</form>

</asp:Content>

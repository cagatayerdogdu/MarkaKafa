<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="yeni_marka_1.aspx.cs" Inherits="GrafikerPortal.yeni_marka_1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm" enctype="multipart/form-data">

<asp:HiddenField ID="hfIslem" runat="server" />
<div style="padding-left:15px;">
            <div style="width: 420px; text-align:left; ">
                <h1 style="display:none;"><asp:Label runat="server" ID="lblProjeAdi" /></h1>

                <h5 style="font-size: 13px;" class="fonts"><asp:Label runat="server" ID="lblProjeAciklama" /></h5>

                <p class="fonts" style="font-size: 12px;">Markanız için istediğiniz tasarımın detaylarını, öncelikli ifade etmek istediklerinizi, işinizin tanımını, ihtiyaçlarınızı ve yaptığımız çalışmayla markanız için neyi / neleri ön plana çıkarmak istediğinizi ne kadar net ve ayrıntılı anlatırsanız, tasarımcılarımızın o denli doğru odaklanmasını sağlamış olursunuz.</p>

                <div style="clear: both;"></div>

            </div>


<div runat="server" id="divErrorProje" visible="false" class="error">
    <p>
        <strong>
            Lütfen aşağıdaki hataları düzeltiniz:
        </strong>
    </p>
    <div>
        <ul style="width: 500px;">
            <li runat="server" id="liProjeAdi" class="message" visible="false">
                Proje ismi / İşin açıklaması doldurulmalı.
            </li>
            <li runat="server" id="liErrorFileSize" class="message" visible="false">
                Dosya boyutu 5 MB'ı geçmemelidir.
            </li>
            <li runat="server" id="liErrorFileFormat" class="message" visible="false">
                Dosya formatı JPEG, PNG veya GIF olabilir.
            </li>
            <li runat="server" id="liError" class="message" visible="false">
                Proje oluşturulurken bir hata meydana geldi. Lütfen bilgilerinizi kontrol ederek tekrar deneyin.
            </li>
        </ul>
    </div>
</div>
            
            <input name="accesskeydisabled" type="hidden" value="&#x2713;" />
            <table>
                <tr>
                    <td valign="top" style="padding-left: 10px; padding-top: 10px;">
                        <b class="fonts" style="font-size: 13px;">MARKA / FİRMANIZ HAKKINDA</b>
                    </td>
                    <td style="padding-left: 30px;">
                        <p class="fonts" style="font-size: 12px; float: right;">TASARIMDA KULLANILMASINI İSTEDİĞİNİZ DOSYALARI EKLEYİNİZ</p>
                    </td>
                </tr>

                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <b class="fonts" style="font-size: 13px;">marka / firmanızın adı?</b>
                                    <div style="height: 6px;"></div>

                                    <input runat="server" id="project_brand_name" name="project_brand_name" class="txt3" value="" type="text" />
                                </td>
                                <td style="padding-left: 20px;">
                                    <b class="fonts" style="font-size: 14px;">sektörünüz</b>
                                    <div style="height: 6px;"></div>

                                    <asp:Label runat="server" ID="lblDropdownSektor" />

                                    <select class="txt2" runat="server" id="project_sector" name="project_sector" style="margin-top:-1px; height:33px;">
                                        <option value="0">Seçiniz</option>
                                        <option  value="1">Avukatlık ve Hukuki Danışmanlık</option>
                                        <option  value="2">Aydınlatma</option>
                                        <option  value="3">Basın - Yayın</option>
                                        <option  value="4">Bilişim - Yazılım - Teknoloji</option>
                                        <option  value="5">Danışmanlık</option>
                                        <option  value="6">Dernek - Vakıf</option>
                                        <option  value="7">Diğer</option>
                                        <option  value="8">Eczacılık</option>
                                        <option  value="9">Eğitim</option>
                                        <option  value="10">Elektronik</option>
                                        <option  value="11">E-ticaret - Dijital Platform - Blog</option>
                                        <option  value="12">Ev Tekstili - Dekorasyon</option>
                                        <option  value="13">Finans ve Yatırım Danışmanlığı</option>
                                        <option  value="14">Gıda</option>
                                        <option  value="15">Hizmet</option>
                                        <option  value="16">Holding - Şirketler Grubu</option>
                                        <option  value="18">İnsan Kaynakları</option>
                                        <option  value="17">İnşaat - Yapı - Emlak</option>
                                        <option  value="19">Kozmetik</option>
                                        <option  value="20">Kuyumculuk</option>
                                        <option  value="21">Mağaza</option>
                                        <option  value="22">Mobilya</option>
                                        <option  value="23">Otomotiv - Akaryakıt</option>
                                        <option  value="24">Prodüksiyon</option>
                                        <option  value="25">Reklam - Halkla İlişkiler</option>
                                        <option  value="26">Restaurant - Cafe - Bar</option>
                                        <option  value="27">Sağlık</option>
                                        <option  value="28">Tarım - Ziraat - Hayvancılık</option>
                                        <option  value="29">Tekstil - Aksesuar</option>
                                        <option  value="30">Ticaret</option>
                                        <option  value="31">Turizm - Otelcilik</option>
                                        <option  value="32">Üretim - Endüstri</option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding-left: 30px;">
                        <table>
                            <tr>
                                <td><b class="fonts" style="font-size: 12px; float:right; padding-bottom:6px;">TASARIMIN SÜRESİ</b>
                                    <div style="height: 6px; "></div>
                                    <select runat="server" id="project_duration" name="project_duration" class="txt3" style="margin-top:-1px; height:33px;" title="7 günden daha uzun süreli projelerde, 7 günü aşan her gün için proje bedeline %1 eklenir.">
                                        <option value="5">5 gün</option>
                                        <option value="6">6 gün</option>
                                        <option value="7">7 gün</option>
                                        <option value="8">8 gün</option>
                                        <option value="9">9 gün</option>
                                        <option value="10">10 gün</option>
                                        <option value="11">11 gün</option>
                                        <option value="12">12 gün</option>
                                        <option value="13">13 gün</option>
                                        <option value="14">14 gün</option>
                                        <option value="15">15 gün</option>
                                    </select>
                                </td>
                                <td style="padding-left:30px;"><b class="fonts" style="font-size: 12px;">DOSYA EKLE</b>
                                    <div style="height: 6px;"></div>
                                    
                                    <input runat="server" id="txtDosya1Baslik" name="txtDosya1Baslik" class="txt3" value="" type="text" placeholder="Dosya Başlığı" />
                                    <a style="cursor:pointer;" onclick="document.getElementById('fuDosya1').click();" class="ozel_buton_ovalsizbeyaz" style="width:200px;">YÜKLE</a>
                                    
                                    <div style="display:none;"><asp:FileUpload ID="fuDosya1" runat="server" ClientIDMode="Static" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding-top: 30px;">
                        <b class="fonts" style="font-size: 13px;">TASARIMINIZ HAKKINDA</b>
                        <div style="height: 5px;"></div>

                        <b class="fonts" style="font-size: 14px;">Tasarımınıza isim verin</b>
                        <div style="height: 6px;"></div>
                        <input runat="server" id="project_title" name="project_title" class="txt3" value="" type="text" style="width: 431px;"/>
                    </td>

                    <td valign="top" style="padding-top: 30px; padding-left: 30px;">
                        <b class="fonts" style="font-size: 13px;">TASARIMINIZI ANLATIN</b>
                        <div style="height: 5px;"></div>

                        <b class="fonts" style="font-size: 14px;">Tasarımınız hakkında bilgi verin</b>
                        <div style="height: 6px;"></div>
                        <textarea runat="server" id="project_brand_info" name="project_brand_info" class="txt2" rows="4" cols="50" style="height: 61px; width: 430px;"></textarea>

                       
                    </td>
                </tr>
            </table>

            <div style="padding-top: 20px;">
                <a href="javascript:document.getElementById('btnKaydet').click();" class="ozel_buton_ovalsiz" style="margin-left: 600px; width: 206px; line-height: 1.0; border-top-right-radius: 30px; border-bottom-right-radius: 30px;">KAYDET / DEVAM</a>
                <div style="display:none;"><asp:Button ID="btnKaydet" runat="server" Text="KAYDET / DEVAM" class="" name="commit" onclick="btnKaydet_Click" ClientIDMode="Static"></asp:Button></div>
            </div>
</div>
</form>
</asp:Content>

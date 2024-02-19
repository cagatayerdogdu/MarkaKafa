<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="profil_portfolyo.aspx.cs" Inherits="GrafikerPortal.profil_portfolyo1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.divPortfolyo
{
    background-image:url('img/balon_01.png'); width:152px; height:146px; margin-left:17px; display:inline-block;
    
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">

<input runat="server" id="hfSilinecekPortfolyo" type="hidden" clientidmode="Static" value="" />

<div runat="server" id="divSuccessPortfolyoSil" class="success" visible="false">
    <div>
        <p>
            Dosya portfolyonuzdan kaldırıldı.
        </p>
    </div>
</div>
<div runat="server" id="divSuccessPortfolyoOnay" class="success" visible="false">
    <div>
        <p>
            Portfolyonuz incelendikten sonra e-posta yoluyla bilgilendirme yapılacaktır. İlginiz için teşekkür ederiz.
        </p>
    </div>
</div>

<div runat="server" id="divNotePortfolyoPasif" class="note_box" visible="false">
    Sistemin sizi profesyonel tasarımcı olarak tanımlayabilmesi için portfolyonuza geçmişte yapmış olduğunuz çalışmalardan örnekler     yüklemelisiniz. Tasarımcıları uzmanlık alanlarına göre kategorize ediyoruz. Bu nedenle uzmanı olduğunuz tüm tasarım kategorileri ile    ilgili örnek çalışmalar yüklemelisiniz. Ne kadar çok, o kadar iyi... Ne kadar iyi, o kadar daha iyi...
</div>
<div runat="server" id="divNotePortfolyoAktif" class="note_box" visible="false">
    Portfolyonuz için onay incelemesi talebinde bulunulmuştur. Portfolyonuz incelendikten sonra e-posta yoluyla bilgilendirme yapılacaktır.
</div>


            <table>
                <tr>
                    <td valign="top" style="padding-left:50px; padding-top:20px;">
                    
          <div style="background-image:url('img/balon_01.png'); width:152px; height:146px; margin-left:17px;">
            <img runat="server" id="imgAvatar" style=" border-radius: 100%;
    height: 127px;
    margin-left: -7px;
    margin-top: 9px;
    width: 128px;" src="img/MarkaKafaAvatar.jpg">
          </div>
    <div style="margin-left:30px; text-align:left;">
        <br />
        <span> <asp:Label ID="lblTasarimciAdi" runat="server" Text=""></asp:Label> </span>
        <br />
        <span style="font-size:7px;"> <asp:Label ID="lblTarih" runat="server" Text=""></asp:Label> tarihinde marka kafa oldu </span>
        <br />
        <a href="#" runat="server" id="a_Katildigi" style="font-size:11px;"> katıldığı markalar </a>
        <br />
        <a href="#" runat="server" id="a_Kazandigi" style="font-size:11px;"> kazandığı markalar </a>
        <br />
        <br />
        <a class="ozel_buton2" style="font-size:15px;" href="profil_portfolyo_yeni_resim.aspx"> Tasarım ekle </a>
        <asp:Button runat="server" ID="btnPortfolyoSil" Text="SİL" OnClick="PortfolyoSil" style="display:none;" ClientIDMode="Static" />
        <br />
        <br />
        <a class="ozel_buton2" runat="server" id="portfolyoOnaya" style="font-size:15px; cursor:pointer;" onclick="document.getElementById('btnOnayaGonder').click();"> Onaya gönder </a>
        <div style="display:none;"><asp:Button runat="server" ID="btnOnayaGonder" class="orange button" Text="Onaya Gönder" OnClick="PortfolyoOnayaGonder" ClientIDMode="Static" /></div>
        <br />
        <br />
        <a class="ozel_buton2" href="profil.aspx" style="font-size:15px;"> Profilimi düzenle  </a>
        <br />
    </div>
                    </td>
                    <td valign="top" style="text-align:left;">
                        
                        <div style="margin-left: 70px;">
                            <asp:Label runat="server" ID="lblListePortfolyo" />
                        </div>

                        <div style="clear:both;"></div>

                        <div style=" width:866px; height:40px; display:none;">
                            <a class="ozel_buton2" href="" style="font-size:15px; float:right;"> sonraki </a>
                        </div>

                    </td>
                </tr>
            </table>


</form>
</asp:Content>

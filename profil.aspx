<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="profil.aspx.cs" Inherits="GrafikerPortal.profil" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
</script>
<style type="text/css">
.divAvatar
{
    background-image:url('img/balon_01.png'); width:152px; height:146px; margin-left:17px; display:inline-block;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm" enctype="multipart/form-data">

        <div runat="server" id="divErrorFile" class="error" visible="false">
            <p>
                <strong>
                    Avatar resminiz yüklenemedi.
                </strong>
            </p>
            <div>
                <ul>
                    <li runat="server" id="liErrorFileSize" class="message" visible="false">
                        Dosya boyutu 5 MB'ı geçmemelidir.
                    </li>
                    <li runat="server" id="liErrorFileFormat" class="message" visible="false">
                        Dosya formatı JPEG, PNG veya GIF olabilir.
                    </li>
                    <li runat="server" id="liErrorFile" class="message" visible="false">
                        Dosya yüklenirken bir sorun oluştu. Problem devam ederse lütfen bizimle iletişime geçiniz.
                    </li>
                </ul>
            </div>
        </div>
        <div runat="server" id="divErrorProfilGuncelle" class="error" visible="false">
            <p>
                Profiliniz güncellenirken bir hata meydana geldi. Lütfen bilgilerinizi kontrol ederek tekrar deneyin.
            </p>
        </div>
        <div runat="server" id="divSuccessProfilGuncelle" class="success" visible="false">
            <p>
                Profiliniz başarıyla güncellenmiştir.
            </p>
        </div>
        <div runat="server" id="divErrorProfilIptal" class="error" visible="false">
            <p>
                Profil iptal talebiniz kaydedilemedi. Sorun devam ederse lütfen bizimle iletişime geçiniz.
            </p>
        </div>
        <div runat="server" id="divSuccessProfilIptal" class="success" visible="false">
            <p>
                Profil iptal talebiniz kaydedilmiştir. En kısa sürede sizinle iletişime geçeceğiz.
            </p>
        </div>

        <table>
             <tr>
             <td style="padding-left:50px; padding-top:20px;">
     
             <span style="padding-left: 15px;"> avatar resmi </span>
             <br />
                <div class="divAvatar">
                  <img runat="server" id="imgAvatar" alt="Avatar" src="/images/avatars/120/MarkaKafaAvatar.jpg" style="border-radius: 100%; height: 127px; margin-left: -9px; margin-top: 9px; width: 128px;" />
                </div>
                  <br />                  
                   <br />

                  <div class="submit">
        <input class="btn_uye_iptal" name="commit" onclick="document.getElementById('fuAvatar').click(); return false;" type="submit" value="dosya yükle" />
        <asp:FileUpload ID="fuAvatar" runat="server" onchange="document.getElementById('btnUploadAvatar').click();" ClientIDMode="Static" style="display:none;"/>
        <asp:Button ID="btnUploadAvatar" runat="server" Text="AVATAR" ClientIDMode="Static" style="display:none;" onclick="AvatarResmiYukle"></asp:Button>
    </div>
            <br />

    <input id="user_avatar_file_name" name="user[avatar_file_name]" type="hidden" value="photoshop.jpg" />
        <br />

        <div style="height:10px;">  </div>
                <b> <asp:Label ID="txtSirketAd" runat="server" Text="" /> </b>
                  <br />
                <span style="margin-top:-10px;"> kulanıcı bilgileri  </span>
          
             </td>
             <td style="text-align:left; padding-left:40px;">
     
              <span style=" margin-bottom:12px; font-size:12px;font-family: 'Trebuchet MS', sans-serif;font-style: normal;font-weight:bold; text-transform: normal;letter-spacing: 0px;line-height: 1.2em;"> E-posta </span>
             <br />
             <input runat="server" id="profile_eposta" name="profile[eposta]" type="text" value="" class="txt" style=" margin-top:9px;" />
     
             <div style="clear:both; height:20px;">  </div>
    
             <span style=" margin-bottom:12px; font-size:12px;font-family: 'Trebuchet MS', sans-serif;font-style: normal;font-weight:bold; text-transform: normal;letter-spacing: 0px;line-height: 1.2em;">Şifre </span>
             <br />
             <input runat="server" id="profile_password" name="profile[password]" type="password" value="" class="txt" style=" margin-top:9px;"/>
     
              <div style="clear:both; height:20px;">  </div>
    
               <span style=" margin-bottom:12px; font-size:12px;font-family: 'Trebuchet MS', sans-serif;font-style: normal;font-weight:bold; text-transform: normal;letter-spacing: 0px;line-height: 1.2em;"> Şifre Tekrar </span>
             <br />
             <input runat="server" id="profile_password_again" name="profile[password_again]" type="password" value="" class="txt" style=" margin-top:9px;" />     
     
     
               <div style="clear:both; height:20px;">  </div>
                <span style=" margin-bottom:12px; font-size:12px;font-family: 'Trebuchet MS', sans-serif;font-style: normal;font-weight:bold; text-transform: normal;letter-spacing: 0px;line-height: 1.2em;">  Telefon </span>
             <br />
                 <input runat="server" id="profile_phone" name="profile[phone]" type="text" value="" class="txt" style=" margin-top:9px;"/>
             <br />
             <br />
         
                  <div style="float:left; padding-top:10px;">
                    <input runat="server" id="profile_deliver_news" name="profile[deliver_news]" type="checkbox" value="1" clientidmode="Static" class="regular-checkbox-big"  style="margin-top:10px" />
                    <label style="margin-top:-10px" for="profile_deliver_news"></label>
                  </div>
                 <div style="float:left; padding-left:20px;">
                  <span style=" margin-bottom:-12px; font-size:12px;font-family: 'Trebuchet MS', sans-serif;font-style: normal;font-weight:bold; text-transform: normal;letter-spacing: 0px;line-height: 1.2em; margin-left: -10px;">  markakafa ile gelişmelerden haberdar olmak istiyorum.
</span>
                 </div>

          </div>

            <div style="clear:both; height:20px;">  </div>

     
                <asp:Button ID="btnKaydet" runat="server" Text="kaydet" class="ozel_buton" name="commit" onclick="btnKaydet_Click" ClientIDMode="Static" style="padding-left: 40px; text-align: left; cursor:pointer;"></asp:Button>
     
             <div style="clear:both; height:20px;">  </div>

                 <asp:Button  ID="btnUyelikIptal" runat="server" Text="üyeliğimi iptal et" class="btn_uye_iptal" name="commit" onclick="btnUyelikIptal_Click" ClientIDMode="Static" OnClientClick="return confirm('Üyelik iptali talebinde bulunmak istediğinizden emin misiniz?');"></asp:Button>



     
             </td>
             </tr>
             </table>

</form>

</asp:Content>

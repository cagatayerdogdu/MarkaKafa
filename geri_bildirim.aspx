<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="geri_bildirim.aspx.cs" Inherits="GrafikerPortal.geri_bildirim" EnableViewState="false" ViewStateEncryptionMode="Never" ViewStateMode="Disabled" EnableViewStateMac="false" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form runat="server" id="frmForm" action="geri_bildirim.aspx" method="post">

<div id="container">
<section id='new_feedback'>
    <h1>Geri bildirim</h1>
    <div runat="server" id="divErrorGeriBildirim" class="error">
        <p>
            <strong>
                Lütfen aşağıdaki hataları düzeltiniz:
            </strong>
        </p>
        <div>
            <ul>
                <li runat="server" id="liIsimGeriBildirim" class="message" visible="false">
                    İsminiz doldurulmalı.
                </li>
                <li runat="server" id="liEpostaGeriBildirim" class="message" visible="false">
                    E-posta adresiniz doldurulmalı.
                </li>
                <li runat="server" id="liEpostaFormatGeriBildirim" class="message" visible="false">
                    E-posta adresiniz: geçerli formatta değil.
                </li>
                <li runat="server" id="liMesajGeriBildirim" class="message" visible="false">
                    Mesajınız doldurulmalı.
                </li>
                <li runat="server" id="liMesajHata" class="message" visible="false">
                    Mesajınız gönderilirken bir sorun oluştu. Lütfen bilgilerinizi kontrol edip tekrar deneyiniz.
                </li>
            </ul>
        </div>
    </div>
    
    <div runat="server" id="divSuccessGeriBildirim" class="success">
        <p>
            <strong>
                Fikirlerinizi bizimle paylaştığınız için teşekkür ederiz.
            </strong>
        </p>
    </div>

    <input name="accesskeydisabled" type="hidden" value="1" />
    <div style="margin:0;padding:0;display:inline">
        <input name="utf8" type="hidden" value="&#x2713;" />
        <input name="authenticity_token" type="hidden" value="Xue3GpbTsbSmNpTYPcTkJKAJYclqVcoGzmhXcfYxLUE=" />
    </div>
    <input runat="server" id="return_to" name="feedback_return_to" type="hidden" value="" />
    <input runat="server" id="feedback_type" name="feedback_type" type="hidden" value="feedback" />
    <input runat="server" id="feedback_project_id" name="feedback_project_id" type="hidden" />
    <input runat="server" id="feedback_user_id" name="feedback_user_id" type="hidden" />
    <div class='input'>
        <label for="feedback_name">İsminiz:</label>
        <input runat="server" id="feedback_name" name="feedback_name" type="text" value="" />
    </div>
    <div class='input'>
        <label for="feedback_email">E-posta adresiniz:</label>
        <input runat="server" id="feedback_email" name="feedback_email" type="text" value="" />
    </div>
    <div class='text_area'>
        <label for="feedback_message">Mesajınız:</label>
        <textarea runat="server" cols="60" id="feedback_message" name="feedback_message" rows="5">
        </textarea>
    </div>
    <div style="display:none;" class='abuse_checker_input'>
        <label for="feedback_r_control">Lütfen bu alanı doldurmayınız</label>
        <input id="feedback_r_control" name="feedback_r_control" type="text" value="" />
    </div>
    <div class='submit main_page'>
        <input class='orange' type='submit' value='Gönder'>
    </div>
</section>

</div>

</form>
</asp:Content>

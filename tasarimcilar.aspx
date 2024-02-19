<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="tasarimcilar.aspx.cs" Inherits="GrafikerPortal.tasarimcilar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function sayfaDegistir(sayfaNo) {
        document.getElementById('hf_page').value = sayfaNo;
        document.getElementById('btnSayfaDegistir').click();
    }
</script>
<style type="text/css">
.divTasarimci
{
    background-image:url('img/balon_01.png'); width:152px; height:146px; margin-left:17px; display:inline-block;
}
.divFiltre span,input,select
{
    display: inline;
    float: left;
    margin: 5px;
}
.divFiltre select
{
    height: 23px;
    width: 145px;
}

.ui-tooltip {
    font-size:10px;
    padding:  5px;
    border-color:#ff006e;
  }

</style>

      <script type="text/javascript">
          $(function () {
              $(document).tooltip({
                  track: true
              });
          });
  </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server" id="frmForm">

<div id="container">
<input runat="server" id="hf_filter_title" type="hidden" clientidmode="Static" />
<input runat="server" id="hf_sort_by" type="hidden" clientidmode="Static" />
<input runat="server" id="hf_filter_designer" type="hidden" clientidmode="Static" />
<input runat="server" id="hf_approval_time" type="hidden" clientidmode="Static" />
<input runat="server" id="hf_page" type="hidden" value="0" clientidmode="Static" />
<asp:Button ID="btnSayfaDegistir" runat="server" Text="" ClientIDMode="Static" style="display:none;" onclick="btnSayfaDegistir_Click" />
<section id='idepeople_index'>
<h1>Tasarımcılar</h1>
<div class='search clearfix'>
<!-- FORM FİLTRE -->
<div style="margin:0;padding:0;display:inline">
    <input name="utf8" type="hidden" value="&#x2713;" />
</div>
<div class="divFiltre">
    <span class='search-label'>Tasarımcı Ara :</span>
    <input runat="server" id="filter_title" name="filter_title" placeholder="Tasarımcı Adı" style="width: 120px;" type="text" clientidmode="Static" class="txtKucuk" />

    <select runat="server" class="txt2Kucuk" id="sort_by" name="sort_by" clientidmode="Static">
        <option value="">En Çok</option>
        <option value="most_attendance">En Çok Katılan</option>
        <option value="most_winning">En Çok Kazanan</option>
    </select>

    <select runat="server" class="txtKucuk" id="filter_designer" name="filter_designer" clientidmode="Static">
        <option value="">Kategoriler</option>
        <option value="web_designer">Web Tasarımcı</option>
        <option value="illustrator">İllüstratör</option>
        <option value="graphics_designer">Grafik Tasarımcı</option>
        <option value="writer">Reklam Yazarı</option>
        <option value="industrial_designer">Yazılımcı</option>
    </select>

    <select runat="server" class="txt2Kucuk" id="approval_time" name="approval_time" clientidmode="Static">
        <option value="">Yeni Gelenler</option>
        <option value="on_this_week">Son 1 Hafta</option>
        <option value="on_this_month">Son 1 Ay</option>
    </select>
</div>
<div class='submit'>
    <asp:Button ID="btnFiltrele" runat="server" Text="Listele" class="ozel_buton2" name="commit" ClientIDMode="Static" onclick="btnFiltrele_Click" style="width:100px;height: 23px;padding-top: 3px;padding-left: 10px;font-family: 'opificio';font-size: 17px;" />
</div>
<!-- FORM FİLTRE -->


</div>
<div class='center_text' id='intro'>
    <h2 class='large_text'>MarKa Kafa gururla sunar!</h2>
    <p>
        Grafik tasarımcıdan, web tasarımcısına, illüstratörden reklam yazarına ve endüstriyel tasarımcıya birbirinden yetenekli
        <strong><asp:Label runat="server" ID="lblTasarimciSayisi" value="3420" /></strong> 
        tasarımcı burada.
    </p>
</div>
<div class='content'>
<!-- %div -->
<!-- .float_right -->
<!-- =@list.total_entries -->
<!-- Tasarımcı -->
    <ul class='idepeople'>
        <asp:Label ID="lblListeKullanicilar" runat="server" Text="" />
    </ul>
    <div class='clear'></div>
    <div class="pagination">
        <asp:Label ID="lblSayfaNumaralari" runat="server" Text="" />
    </div>
</div>
</section>

</div>

</form>

</asp:Content>

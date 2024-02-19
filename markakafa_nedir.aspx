<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="markakafa_nedir.aspx.cs" Inherits="GrafikerPortal.markakafa_nedir" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function sekmeDegistir(sekme) {
        document.getElementById('what_is').style.display = 'none';
        document.getElementById('markalara_ozel').style.display = 'none';
        document.getElementById('markakafalara_ozel').style.display = 'none';

        document.getElementById(sekme).style.display = 'block';
    }
</script>
<style type="text/css">
    .btn_sol
    {
        cursor:pointer; 
        float:left;
        height:30px;
        width:220px;
        padding-top:11px;
        margin-left:5px;
        background-color:#e7038a;
        color:#fff;
        border-top-left-radius: 30px 30px;
        border-bottom-left-radius:30px 30px;
    }
    .btn_sag
    {
        cursor:pointer; 
        float:left;
        height:30px;
        width:220px;
        padding-top:11px;
        margin-left:5px;
        background-color:#e7038a;
        color:#fff;
        border-top-right-radius: 30px 30px;
        border-bottom-right-radius:30px 30px;
    }
    .btn_sol:hover
    {
        background-color: #A60E68;/*#9A2C64;*/
        color:#fff;
    }
    .btn_sag:hover
    {
        background-color: #A60E68;/*#9A2C64;*/
        color:#fff;
    }
    .baslik:hover
    {
        cursor:pointer;
        color:#e7038a;
    }
    .div_ozel
    {
        text-align:left;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="container">
    <div style="display:inline-block; width:54%; text-align:center;">
        <a class="btn_sol" onclick="sekmeDegistir('markalara_ozel');">MARKALARA ÖZEL</a>
        <a class="btn_sag" onclick="sekmeDegistir('markakafalara_ozel');">MARKAKAFALARA ÖZEL</a>
    </div>
 <div id="what_is">
        <h1>Marka Kafa Nedir?</h1>
        <p>MarkaKafa yüzlerce profesyonel tasarımcıyla, sizlerin ihtiyaçlarını buluşturan dev bir tasarım platformudur.</p>
<p>Tasarım, sınırsızlık içinde ihtiyaç sınırlarına çok takılmadan, hayal ettiğin şeyi resmedip sunmaksa, neden  üç beş beyine ve hayal gücüne bunu hapsedelim ki?Neden, sadece ajansımız ve onlarla çalışıyoruz diye, üç-beş tasarımcıya, zorla hayal kurduralım ki?</p>
<p>MarkaKafa'daki binlerce ''Deha Kafa'' projelerinizi görür, ilham perisi üzerinde olanlar; evdeyim, modada bir cafedeyim, şu an plajdayımı pek umursamadan, aklından geçen renkleri ve çizgileri tasarlayarak vücuda getirir ve sizlere sunar. Her biri alanında ve mausunda güçlü web tasarımcı, grafiker, illustratör, endüstriyel tasarımcı,reklam ve metin yazarlarıyla sizin markanıza en uygun çözümleri sunmak için bu platformda sizlerle buluşur.</p>
<p>Üstelik tamamen sizin belirlemiş olduğunuz bütçelerle..</p>
<p>Kurumsal kimliğe mi ihtiyacınız var? Ya da yeni bir Web sayfasına..Yeni çıkan bir ürününüze isim ve sunum mu gerekiyor? Yapacağınız bir etkinliğin afiş ve broşürleri mi lazım? Yılbaşı etkinliğiniz için sizi anlatan hediyelik kurumsal bir objeye mi ihtiyacınız var? Ya da yeni ürününüzün yeni bir ambalaja? Fuara katılacaksınız ve dikkat çekici,fonksiyonel bir standa mı ihtiyacınız var? Klasik ve alışılageldik tasarım ve tanıtımlardan bıktınız mı? Bunların hepsi için ajanslarla saatler, hatta günler süren toplantılar yapmanıza gerek yok. Zamanınızı diğer işlerinize hatta hobilerinize ayırabilirsiniz.Size bunca zaman sonrasında sunulacak ve seçmek zorunda kalacağınız üç-beş tasarıma da gerek yok..Çünkü Markakafa’da sizin belirlediğiniz zaman zarfında önünüze gelecek ve en önemlisi tek kaynaktan zevkle seçim yapacağınız onlarca farklı beyin ve farklı yorumdan çıkmış tasarımlarınız olacak.Üstelik bu tasarımlar için çok yüksek bütçeler de gerekmiyor.Tamamen sizin baştan belirlemiş olduğunuz tasarım bedeli dahilinde, sınırsız bir hizmet alcaksınız. Hiç olmadı, hiç zannetmiyoruz ya…Ya tasarımları beğenmezseniz..Vazgeçme ve tasarım bedelinizi geri çekme hakkınız da var tabiî ki..</p>
<p>Tasarım süreci bitti..Markakafalar sayesinde Sizi mutlu, markanızı tatmin eden bir tasarımınız var artık.Pekii bunun basımı; hayata geçirimi nerede ve nasıl olacak diyorsanız, bu anlamda da size destek veriyoruz. MarkaKafa aynı zamanda seçmiş olduğunuz tasarımın;kaliteli,hızlı  ve en ekonomik şekilde baskı, imalat ve üretimini de yaptırarak canlı kanlı adresinize teslim edip A’dan,Z’ye sizlere hizmet vermektedir.</p>
<p><b>Unutmayın, markanız da vaktiniz de kıymetlidir!</b></p>
    </div>

    <div id="markalara_ozel" class="div_ozel" style="display:none; background-image:url('img/markakafalaraozelmaskot.png');">
        <h1 class="baslik" style="cursor:default;">Firmalara ipuçları...</h1>
        <div style="font-size:14px;">
            <p>Markanız için istediğiniz tasarımın detaylarını, öncelikli ifade etmek istediklerinizi,  işinizin tanımını, ihtiyaçlarınızı ve yaptığımız çalışmayla markanız için neyi/neleri ön plana çıkarmak istediğinizi ne kadar net ve ayrıntılı anlatırsanız tasarımcılarımızın o denli doğru odaklanmasını sağlamış olursunuz. Tasarım için doğru zaman ve işin ederiyle ilgili doğru belirlenen iş ödülü,sizin kısa  zamanda doğru tasarımlarla buluşmanızı sağlayacaktır.Gerisi size sunulan alternatiflerden, en beğendiğinizi ve sizi en doğru temsil edenini seçmeniz..</p>
            <p>Markakafa’nın ayrıcalıklı sistemi zaten sizleri adım adım yönlendirmek üzerine kurulu.İş başlangıcında sizin için hazırlanmış bilgi formundaki sorularımız, tasarımcılarımızın vakit kaybetmeden hızlıca sizi tanımalarına ve istediğiniz tasarımı doğru anlamalarını sağlayacak ipuçlarına sahiptir.Bunun yanı sıra tasarımları görüp, değerlendirme ve önermelerde de bulunabilirsiniz.Aynı zamanda destek almak üzere 'Canlı Destek'linkimizden on line destek alabilirsiniz. </p>
            <p>Burada işler böyle ilerliyor/ veya Şimdi başlamak için:</p>
            <p>İşi başlat butonuna basın</p>
            <p>Bilgi formunu kısa da olsa net doldurun ki; sizi iyi tanıyalım,neyi tasarlayacağımızı bilelim.</p>
            <p>İşin ödülünü belirleyip, ödemeyi yapın</p>
            <p>Markakafa'lardan tasarımlar çıksın ve yüklenen tasarımlara göz atın.<br />
            Değişiklik  veya öneride bulunmak istiyorsanız, tasarımcıları bilgilendirin.<br />
            İşin süresi bittiğinde kazanan tasarımı 3 gün içinde belirleyin <br />
            ve tasarımcısıyla 5 gün içinde işi son haline getirin.</p>
            <p>Sorun yoksa; her şey tam hayallerinizdeki gibiyse işi onaylayın.<br />
            Biz de markakafımıza ödülünü ve teşekkürlerimizi sunalım.</p>
            <p>İşin üretimi için destek gerekiyorsa çözüm departmanımızdan teklif isteyin.<br />
            En kısa sürede tasarımımızı kanlı canlı kapınıza teslim edelim.</p>
        </div>
    </div>

    <div id="markakafalara_ozel" class="div_ozel" style="display:none; background-image:url('img/markakafalaraozelmaskot.png');">
        <h1 class="baslik" style="cursor:default;">Tasarımcılara ipuçları...</h1>
        <div style="font-size:12px;">
            Bu bir yetenek işi ve sonradan kazanılmaz,kabul..Ancak yıllardır edindiğimiz tecrübeyle bir iki öneride bulunmamıza izin ver.Kafamda bir şey var,hemen başlamalıyım heyecanını kaybetme ama önce markanın bilgilerini üşenmeden, erinmeden oku,tanı,ne istediğini iyi anla ki, biçtiğin ceket üzerine tam otursun.'Picasso bile yıllar sonra anlaşılabilmiş' bunu  hatırlayarak yaptığın tasarımın altına işi az çok anlatabilecek bir iki not düş..En iyi iş, kendi yaptığın iştir,diğerleri ne yapıyor acaba diye düşünme,kimseden etkilenme.Eninde sonunda sivrileceğin profesyonel bir platformda olduğunu aklından çıkarma.Tamam..Şimdi kafandakini unutmadan başlayabilirsin, hayal dünyan ve yeteneğinle baş başasın..<br />
            Bu arada sizin vaktinizin ve yaptığınız işin de değerini bildiğimizi,her projeyi 20 tasarımcıyla sınırlandırma sebebimizin bu olduğunu bil..<br />
            İyi şanslar:) <br />
            Şimdi başlamak için<br />
            İlhamının geldiği projeyi seç<br />
            Markanın doldurduğu bilgi formunu oku, tasarımınla bütünleştir ve kafa yormaya başla<br />
            Yaptığın tasarımlarını yükle<br />
            Marka senden ekleme istediyse son haline getir ve sun<br />
            Markanın 3 günü var, heyecanla bekle<br />
        </div>
        <br />
        <br />
        <br />
        <div style="font-size:16px; line-height:22px; ">
            <p style="display:none;"><span style="color:#e7038a;">Markakafa;</span> seçilen senin işin tebrikler..</p>
            <p>Markayla 5 gün içerisinde işi son haline getir.</p>
            <p>Revizyonları biten tasarımını da yükle, biz de onay alalım.</p>
            <p>İşin onayını alır almaz, ödülün hesabında..Tebrikler:)</p>
        </div>
    </div>
</div>

</asp:Content>

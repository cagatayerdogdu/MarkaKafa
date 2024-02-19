using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal
{
    public partial class profil_portfolyo : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            #region Güvenlik protokolü
            //Üye girişi yapılmamışsa üye giriş sayfasına yönlendiriliyor.
            string KullaniciID = (Session["UyeID"] != null) ? Session["UyeID"].ToString() : "";
            if (KullaniciID == "") Response.Redirect("uye_ol.aspx");
            else if (Veritabani.Sorgu_Scalar("SELECT TOP(1) UyeID FROM gp_Uyeler WHERE UyeID=@UyeID", KullaniciID) == "") Response.Redirect("uye_ol.aspx");
            #endregion
            #region Portfolyo listesi oluşturuluyor
            string ListePortfolyo = "";
            DataTable TabloPortfolyo = Veritabani.Sorgu_DataTable("SELECT PortfolyoID, Baslik, Resim, Video, PortfolyoTip FROM gp_Portfolyo WHERE UyeID=@UyeID ORDER BY PortfolyoID DESC", Session["UyeID"].ToString());
            string PortfolyoID; string PortfolyoBaslik; string PortfolyoResim; string PortfolyoVideo; string PortfolyoTip;
            for (int i = 0; i < TabloPortfolyo.Rows.Count; i++)
            {
                PortfolyoID = TabloPortfolyo.Rows[i]["PortfolyoID"].ToString();
                PortfolyoBaslik = TabloPortfolyo.Rows[i]["Baslik"].ToString();
                PortfolyoResim = TabloPortfolyo.Rows[i]["Resim"].ToString();
                PortfolyoVideo = TabloPortfolyo.Rows[i]["Video"].ToString();
                PortfolyoTip = TabloPortfolyo.Rows[i]["PortfolyoTip"].ToString();
                if (PortfolyoTip == "1") //Resim
                {
                    ListePortfolyo += @"
                        <li>
                            <img alt="""" class=""frame"" src=""images/portfolio_items/300/" + PortfolyoResim + @""" style=""height:300px;"" title=""" + PortfolyoBaslik + @""" />
                            <p>
                                " + PortfolyoBaslik + @"
                                &mdash;
                                <span class=""submit"">
                                    <input type=""submit"" class=""orange button"" value=""SİL"" onclick=""if(confirm('Bu dosyayı silmek istediğinizden emin misiniz?')){ document.getElementById('hfSilinecekPortfolyo').value='" + PortfolyoID + @"'; document.getElementById('btnPortfolyoSil').click(); } return false;"" />
                                </span>
                            </p>
                        </li>";
                }
                else if (PortfolyoTip == "2") //Video
                {

                }
            }
            lblListePortfolyo.Text = ListePortfolyo;
            #endregion
            if (IsPostBack) return; //Postback yapılmışsa açılış işlemleri yapılmıyor.

            #region Portfolyo talep bilgisi tespit ediliyor
            DataTable TabloPortfolyoAktiflikBilgileri = Veritabani.Sorgu_DataTable("SELECT TOP(1) Aktif, PortfolyoOnayTalebi FROM gp_Uyeler WHERE UyeID=@UyeID", Session["UyeID"].ToString());
            bool PortfolyoAktif = (TabloPortfolyoAktiflikBilgileri.Rows[0]["Aktif"].ToString() == "1");
            bool PortfolyoOnayTalebiAktif = (TabloPortfolyoAktiflikBilgileri.Rows[0]["PortfolyoOnayTalebi"].ToString() == "1");
            if (PortfolyoOnayTalebiAktif) //Portfolyo için onay talebinde bulunulmuşsa
            {
                divNotePortfolyoAktif.Visible = true;
                divNotePortfolyoPasif.Visible = false;
                btnOnayaGonder.Visible = false;
            }
            else if (!PortfolyoAktif) //Portfolyo pasifse ve henüz onay talebinde bulunulmamışsa
            {
                divNotePortfolyoAktif.Visible = false;
                divNotePortfolyoPasif.Visible = true;
            }
            else //Portfolyo onaylanmışsa
            {
                divNotePortfolyoAktif.Visible = false;
                divNotePortfolyoPasif.Visible = false;
            }
            #endregion
        }

        protected void PortfolyoSil(object sender, EventArgs e)
        {
            divSuccessPortfolyoSil.Visible = false; divSuccessPortfolyoOnay.Visible = false;

            string SilinecekPortfolyo = hfSilinecekPortfolyo.Value;
            if (Veritabani.Sorgu_Scalar("SELECT TOP(1) PortfolyoID FROM gp_Portfolyo WHERE PortfolyoID=@PortfolyoID AND UyeID=@UyeID", SilinecekPortfolyo, Session["UyeID"].ToString()) != "")
            {
                //Portfolyo dosyaları da silinebilir. Şimdilik sunucuda yer sorunu olmadığını varsayıyorum.
                int KayitSayisi = Veritabani.Sorgu_Calistir("DELETE FROM gp_Portfolyo WHERE PortfolyoID=@PortfolyoID", SilinecekPortfolyo);
                if (KayitSayisi > 0) divSuccessPortfolyoSil.Visible = true;
            }
        }

        protected void PortfolyoOnayaGonder(object sender, EventArgs e)
        {
            divSuccessPortfolyoSil.Visible = false; divSuccessPortfolyoOnay.Visible = false;

            int KayitSayisi = Veritabani.Sorgu_Calistir("UPDATE gp_Uyeler SET PortfolyoOnayTalebi=1, PortfolyoOnayTalepTarihi=@PortfolyoOnayTalepTarihi WHERE UyeID=@UyeID", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), Session["UyeID"].ToString());
            if (KayitSayisi > 0)
            {
                divSuccessPortfolyoOnay.Visible = true;
                btnOnayaGonder.Visible = false;
            }
        }

    }
}
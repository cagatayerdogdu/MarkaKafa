using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace GrafikerPortal
{
    public partial class geri_bildirim : System.Web.UI.Page
    {
        DAL Veritabani;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL();

            divErrorGeriBildirim.Visible = false;
            divSuccessGeriBildirim.Visible = false;

            string Isim = ""; string Eposta = ""; string Mesaj = "";
            if (!string.IsNullOrEmpty(Request["accesskeydisabled"]))
            {
                #region Yeni kayıt
                Isim = (!string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$feedback_name"])) ? Request["ctl00$ContentPlaceHolder1$feedback_name"].ToString().Trim() : "";
                Eposta = (!string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$feedback_email"])) ? Request["ctl00$ContentPlaceHolder1$feedback_email"].ToString().Trim() : "";
                Mesaj = (!string.IsNullOrEmpty(Request["ctl00$ContentPlaceHolder1$feedback_message"])) ? Request["ctl00$ContentPlaceHolder1$feedback_message"].ToString().Trim() : "";
                //Bilgiler kontrol ediliyor
                divErrorGeriBildirim.Visible = true;
                if (Isim.Length == 0)
                {
                    liIsimGeriBildirim.Visible = true;
                }
                else if (Eposta.Length == 0)
                {
                    liEpostaGeriBildirim.Visible = true;
                }
                else if (!new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Match(Eposta).Success)
                {
                    liEpostaFormatGeriBildirim.Visible = true;
                }
                else if (Mesaj.Length == 0)
                {
                    liMesajGeriBildirim.Visible = true;
                }
                else
                {
                    //Kayıt işlemi gerçekleştiriliyor
                    int YeniMesajID = Veritabani.Sorgu_Calistir_Eklenen_Id_Dondur("INSERT INTO gp_Geribildirim(Tarih, Isim, Eposta, Mesaj) VALUES(@Tarih, @Isim, @Eposta, @Mesaj)", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), Isim, Eposta, Mesaj);
                    if (YeniMesajID > 0)
                    {
                        divErrorGeriBildirim.Visible = false;
                        divSuccessGeriBildirim.Visible = true;

                        Isim = ""; Eposta = ""; Mesaj = "";
                    }
                    else
                    {
                        liMesajHata.Visible = true;
                    }
                }

                feedback_name.Value = Isim;
                feedback_email.Value = Eposta;
                feedback_message.Value = Mesaj;
                #endregion
            }
        }
    }
}
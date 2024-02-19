using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

namespace GrafikerPortal
{
    public partial class yeni_marka : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            //Balonlar formatlanıyor
            string SeciliBalonLink = ""; string SeciliBalonId = ""; int SeciliBalonFiyat = 0; 
            //YAPILACAK: Maksimum fiyat için veritabanında alan açılacak.
            double SeciliBalonFiyatMax = 0;
            for (int balon = 1; balon <= 34; balon++)
            {
                var mainCtrl = Master.FindControl("ContentPlaceHolder1");
                var SeciliBalon = (HtmlAnchor)mainCtrl.FindControl("aBalon" + balon);
                SeciliBalonLink = SeciliBalon.HRef;
                SeciliBalonId = SeciliBalonLink.Replace("yeni_marka_1.aspx?t=", "");
                SeciliBalonFiyat = int.Parse(Veritabani.Sorgu_Scalar("SELECT TOP 1 Fiyat FROM gp_ProjeTipleri WHERE TipID=@TipID", SeciliBalonId));
                double SeciliBalonFiyatDouble = Convert.ToDouble(SeciliBalonFiyat);

                SeciliBalonFiyatMax = SeciliBalonFiyatDouble * 15;
                SeciliBalon.Title = SeciliBalonFiyatDouble.ToString("C2") + " - " + SeciliBalonFiyatMax.ToString("C2") +"";
                //SeciliBalon.InnerHtml = SeciliBalonLink;
            }
        }
    }
}
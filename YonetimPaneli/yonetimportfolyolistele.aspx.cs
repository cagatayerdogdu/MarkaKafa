using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal.YonetimPaneli
{
    public partial class yonetimportfolyolistele : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;

        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            if (Session["Yetki"] == null || Session["Yetki"].ToString() != "1")
            {
                Response.Redirect("default.aspx");
            }

            string onayid = Request.QueryString["id"].ToString();

            DataTable dt = Veritabani.Sorgu_DataTable("SELECT Baslik,Aciklama,Resim FROM gp_Portfolyo WHERE UyeID=@id",onayid);

            string TumResim = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TumResim += @"                    
                        <h1>" + dt.Rows[i]["Baslik"].ToString() + @"</h1> <br>
                         <p>"+ dt.Rows[i]["Aciklama"].ToString() +@" </p> <br>
                          <img src=""../images/portfolio_items/300/"+dt.Rows[i]["Resim"]+@"""/><br>";
                
            }
            lblTablo.Text = TumResim;
        }
    }
}
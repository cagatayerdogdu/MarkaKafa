using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafikerPortal.YonetimPaneli
{
    public partial class Panel : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"]==null)
            {
                Response.Redirect("default.aspx");
            }
            else if (Session["Yetki"].ToString() == "2")
            {
                //odemeler.Visible = false; // kategoriler id adı ilgili linkler olabilir.
                //kategoriler.Style["display"] = "none";
                odemeler.Style["display"] = "none";
            }
        }
    }
}
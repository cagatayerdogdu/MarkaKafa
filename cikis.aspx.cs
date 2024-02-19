using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafikerPortal
{
    public partial class cikis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UyeID"] = null;
            Session["UyeTipi"] = null;
            Session["KullaniciAdi"] = null;
            Session["KullaniciAdiSifreli"] = null;
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("default.aspx");
        }
    }
}
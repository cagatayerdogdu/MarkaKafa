using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafikerPortal
{
    public partial class mailtest : System.Web.UI.Page
    {
        DAL Veritabani; Mail Eposta;
        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); Eposta = new Mail();
            bool basarili = Eposta.MailGonder(Server, "", "erdogdu3434@gmail.com", "TEST", "DENEME" + DateTime.Now.ToString("dd.MM.yyyy"));
            lblSonuc.Text = basarili ? "basarili" : "basarisiz";
        }
    }
}
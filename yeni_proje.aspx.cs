//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Data;

//namespace GrafikerPortal
//{
//    public partial class yeni_proje : System.Web.UI.Page
//    {
//        DAL Veritabani; Fonksiyonlar AletKutusu;
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

//            //Projeler kategorilerine göre gruplanarak listeleniyor
//            lblProjeListeKolon1.Text = AletKutusu.ProjeFiyatListesi("1");
//            lblProjeListeKolon2.Text = AletKutusu.ProjeFiyatListesi("2");
//            lblProjeListeKolon3.Text = AletKutusu.ProjeFiyatListesi("3");
//        }
//    }
//}
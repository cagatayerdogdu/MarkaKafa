using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GrafikerPortal.YonetimPaneli
{
    public partial class yonetimkategoriler : System.Web.UI.Page
    {
        DAL Veritabani; Fonksiyonlar AletKutusu;

        protected void Page_Load(object sender, EventArgs e)
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();

            if (Session["Yetki"] == null || Session["Yetki"].ToString() != "1")
            {
                Response.Redirect("default.aspx");
            }
            
            if (Request.Form["kategoriid"]!=null)
            {
                string gelenkategoriid = Request.Form["kategoriid"].ToString();
                string gelenkategoriadi = Request.Form["kategoriadi"].ToString();

                Veritabani.Sorgu_Calistir("UPDATE gp_Kategoriler SET KategoriAd=@adi WHERE KategoriID=@id",gelenkategoriadi,gelenkategoriid);
            }
            if (Request.Form["projetipid"]!=null)
            {
                string gelenprojetipid = Request.Form["projetipid"].ToString();
                string gelenprojeadi = Request.Form["projeadi"].ToString();
                string gelenprojefiyat = Request.Form["projefiyat"].ToString();
                string gelenaciklamauzun = Request.Form["aciklamauzun"].ToString();
                string gelenaciklamakisa = Request.Form["aciklamakisa"].ToString();

                Veritabani.Sorgu_Calistir("UPDATE gp_ProjeTipleri SET TipAd=@adi,Fiyat=@fiyat,AciklamaKisa=@kisa,AciklamaUzun=@uzun WHERE TipID=@id", gelenprojeadi,gelenprojefiyat,gelenaciklamakisa,gelenaciklamauzun,gelenprojetipid);
            }

            if (Request.Form["KategoriEkle"]!=null)
            {
                string gelenkategoriadi = Request.Form["kategoriadi"].ToString();
                string gelenkategorikolon = Request.Form["KategoriKolon"].ToString();

                int kolonsira =Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT MAX(Sira) FROM gp_Kategoriler WHERE Kolon=@kolon",gelenkategorikolon))+1;

                Veritabani.Sorgu_Calistir("INSERT INTO gp_Kategoriler(KategoriAd,Kolon,Sira) VALUES(@ad,@kolon,@sira)",gelenkategoriadi,gelenkategorikolon,kolonsira.ToString());
            }

            if (Request.Form["TipEkle"] != null)
            {
                string gelentipkategoriid= Request.Form["tipkategoriid"].ToString();
                string gelenprojeadi = Request.Form["projeadi"].ToString();
                string gelenprojefiyat = Request.Form["projefiyat"].ToString();
                string gelenprojeaciklamakisa = Request.Form["aciklamakisa"].ToString();
                string gelenprojeaciklamauzun = Request.Form["aciklamauzun"].ToString();

                int projetipsira = Convert.ToInt32(Veritabani.Sorgu_Scalar("SELECT MAX(Sira) FROM gp_ProjeTipleri WHERE KategoriID=@kid", gelentipkategoriid)) + 1;

                Veritabani.Sorgu_Calistir("INSERT INTO gp_ProjeTipleri(KategoriID,TipAd,Fiyat,AciklamaKisa,AciklamaUzun,Sira) VALUES(@KategoriID,@TipAd,@Fiyat,@AciklamaKisa,@AciklamaUzun,@Sira)", gelentipkategoriid, gelenprojeadi,gelenprojefiyat,gelenprojeaciklamakisa,gelenprojeaciklamauzun,projetipsira.ToString());
            }

            if (!string.IsNullOrEmpty(Request.QueryString["kategorisil"]))
            {
                string silkategoriid = Request.QueryString["kategorisil"].ToString();
                Veritabani.Sorgu_Calistir("DELETE FROM gp_Kategoriler WHERE KategoriID=@silid", silkategoriid);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["projetipisil"]))
            {
                string siltipid = Request.QueryString["projetipisil"].ToString();
                Veritabani.Sorgu_Calistir("DELETE FROM gp_ProjeTipleri WHERE TipID=@silid", siltipid);
            }

            DataTable dtana = Veritabani.Sorgu_DataTable("SELECT KategoriID,KategoriAd,UstMetin,AltMetin FROM gp_Kategoriler ORDER BY Kolon,Sira");

            string TumTablo = "";
            string kategoriid = "";
            string projetipid = "";

            DataTable dtprojetip;

            TumTablo += @" <form action=""yonetimkategoriler.aspx"" method=""post"">
                <table>
                    <tr>
                        <td>
                            <input type=""hidden"" name=""KategoriEkle"" value='1'/>
                            <input type=""text"" name=""kategoriadi"" value=''/>
                        </td>
                        <td>
                            <select name=""KategoriKolon"">
                                <option value='1'> Birinci Kolon </option>
                                <option value='2'> İkinci Kolon </option>
                                <option value='3'> Üçüncü Kolon </option>
                            </select>
                        </td>
                        <td>
                        </td>
                        <td>
                            <input type=""submit"" value=""Yeni Kategori Ekle"" />
                        </td>
                    </tr>
                </table>
              </form>";

            for (int i = 0; i < dtana.Rows.Count; i++)
            {
                kategoriid = dtana.Rows[i]["KategoriID"].ToString();
                
                TumTablo += @" <form action=""yonetimkategoriler.aspx"" method=""post"">
                <table>
                    <tr>
                        <td>
                            <input type=""hidden"" name=""kategoriid"" value='"+kategoriid+@"'/>
                            <input type=""text"" name=""kategoriadi"" value='"+dtana.Rows[i]["KategoriAd"].ToString()+@"'/>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <input type=""submit"" value=""Güncelle"" />
                            <a onclick='return confirm(""Kategori Silinecek Emin misin?"");' href='yonetimkategoriler.aspx?kategorisil=" + kategoriid + @"'>SİL</a>
                        </td>
                    </tr>
                </table>
              </form>";

                dtprojetip = Veritabani.Sorgu_DataTable("SELECT * FROM gp_ProjeTipleri WHERE KategoriID=@id",kategoriid);

                TumTablo += @" <form action=""yonetimkategoriler.aspx"" method=""post"">
                <table>
                    <tr>
                        <td>
                            <input type=""hidden"" name=""TipEkle"" value='1'/>
                            <input type=""hidden"" name=""tipkategoriid"" value='"+kategoriid+@"'/>
                            <input type=""text"" name=""projeadi"" value=''/>
                        </td>
                        <td>
                            <input type=""text"" name=""projefiyat"" value=''/>
                        </td>
                        <td>
                            <input type=""text"" name=""aciklamakisa"" value=''/>
                        </td>
                        <td>
                            <input type=""text"" name=""aciklamauzun"" value=''/>
                        </td>
                        <td>
                            <input type=""submit"" value=""Ekle"" />
                            
                        </td>
                    </tr>
                </table>
              </form>";

                for (int y = 0; y < dtprojetip.Rows.Count; y++)
                {
                    projetipid = dtprojetip.Rows[y]["TipID"].ToString();

                    TumTablo += @" <form action=""yonetimkategoriler.aspx"" method=""post"">
                <table>
                    <tr>
                        <td>
                            <input type=""hidden"" name=""projetipid"" value='" + projetipid + @"'/>
                            <input type=""text"" name=""projeadi"" value='" + dtprojetip.Rows[y]["TipAd"].ToString() + @"'/>
                        </td>
                        <td>
                            <input type=""text"" name=""projefiyat"" value='" + dtprojetip.Rows[y]["Fiyat"].ToString() + @"'/>
                        </td>
                        <td>
                            <input type=""text"" name=""aciklamakisa"" value='" + dtprojetip.Rows[y]["AciklamaKisa"].ToString() + @"'/>
                        </td>
                        <td>
                            <input type=""text"" name=""aciklamauzun"" value='" + dtprojetip.Rows[y]["AciklamaUzun"].ToString() + @"'/>
                        </td>
                        <td>
                            <input type=""submit"" value=""Güncelle"" />
                            <a onclick='return confirm(""Proje Tipi Silinecek Emin misin?"");' href='yonetimkategoriler.aspx?projetipisil=" + projetipid + @"'>SİL</a>
                        </td>
                    </tr>
                </table>
              </form>";
                }
            }
            Label1.Text = TumTablo;
        }
    }
}
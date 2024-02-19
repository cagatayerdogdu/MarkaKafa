using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace GrafikerPortal
{
    public class Mail
    {
        public string HostName = "";
        public string Email = "";
        public string Gonderen = "";
        public string Password = "";
        public string Port = "";
        public string GonderenMail = "";
        public string DomainName = "http://www.markakafa.com/";
        public int PortNumber = 0;

        private int IntParseBag;
        private long LongParseBag;
        private double DoubleParseBag;

        DAL Veritabani; Fonksiyonlar AletKutusu;
        public Mail()
        {
            Veritabani = new DAL(); AletKutusu = new Fonksiyonlar();
            #region E-posta ayarları tespit ediliyor
            DataTable TabloEpostaAyarlari = Veritabani.Sorgu_DataTable("SELECT TOP 1 * FROM gp_EpostaAyarlar WHERE id=1");
            HostName = TabloEpostaAyarlari.Rows[0]["HostName"].ToString();
            Email = TabloEpostaAyarlari.Rows[0]["Email"].ToString();
            Password = TabloEpostaAyarlari.Rows[0]["Password"].ToString();
            Port = TabloEpostaAyarlari.Rows[0]["Port"].ToString();
            GonderenMail = TabloEpostaAyarlari.Rows[0]["GonderenMail"].ToString();
            PortNumber = (int.TryParse(Port, out IntParseBag)) ? IntParseBag : 0;
            #endregion
        }

        public bool MailGonder(HttpServerUtility Server, string GonderenEposta, string AliciEposta, string Konu, string Icerik)
        {
            bool Sonuc = true;

            try
            {
                string Gonderen = GonderenEposta == "" ? Email : GonderenEposta;

                #region Mail gönderiliyor
                SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = HostName;
                smtp.Port = PortNumber;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(Email, Password);
                smtp.Timeout = 20000;
                MailMessage mailObj = new MailMessage(Gonderen, AliciEposta, Konu, Icerik);
                mailObj.IsBodyHtml = true;


                smtp.Send(mailObj);
                //smtp.SendCompleted += (s, e) => {
                //    smtp.Dispose();
                //    mailObj.Dispose();
                //};
                //smtp.SendAsync(mailObj, null);

                ////smtp.EnableSsl = true;
                ////smtp.UseDefaultCredentials = true;
                //smtp.Send(Gonderen, AliciEposta, Konu, Icerik);
                #endregion
            }
            catch (Exception Hata)
            {
                string HataMesaji = Hata.ToString();
                Sonuc = false;
            }
            return Sonuc;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace GrafikerPortal
{
    /// <summary>
    /// Summary description for plupload
    /// </summary>
    public class plupload : IHttpHandler
    {
        //Kaynak kodlarını şuradan yürüttüm:
        //http://social.msdn.microsoft.com/Forums/vstudio/tr-TR/620cca01-34bd-4ae6-9b2c-abcb532929d9/plupload-aspnet-kullanm?forum=aspnettr

        private List<string> _GetFileList;
        public List<string> GetFileList
        {
            get
            {
                if (HttpContext.Current.Session["FileList"] != null)
                    return (List<string>)HttpContext.Current.Session["FileList"];
                else
                {
                    HttpContext.Current.Session["FileList"] = new List<string>();
                    return (List<string>)HttpContext.Current.Session["FileList"];
                }
            }
            set { _GetFileList = value; }
        }

        public void ProcessRequest(HttpContext context)
        {
            int chunk = (context.Request["chunk"] != null) ? int.Parse(context.Request["chunk"]) : 0;
            string fileName = (context.Request["name"] != null) ? context.Request["name"] : string.Empty;
            HttpPostedFile fileUpload = context.Request.Files[0];

            // Dosyalar parça halinde geldiği için aynı dosya ismi tekrar listemizin içine atılmasın diye kontrol ediyoruz eğer aynı dosya ismi listemizde mevcut ise eklemiyoruz..
            if (!GetFileList.Contains(fileName))
            {
                GetFileList.Add(fileName);
            }

            using (FileStream fs = new FileStream(Path.Combine(context.Server.MapPath("~/uploads/"), fileName), (chunk == 0) ? FileMode.Create : FileMode.Append))
            {
                byte[] buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write("Success");


        }




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}
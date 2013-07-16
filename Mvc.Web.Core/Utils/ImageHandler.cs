using System.Drawing;
using System.Web;

namespace Mvc.Web.Core.Utils {
    public class ImageHandler : IHttpHandler {
        public bool IsReusable {
            get { return true; }
        }

        public string Watermark {
            get { return "orangepoet"; }
        }

        public void ProcessRequest(HttpContext context) {
            Image image = null;
            Graphics graphic = null;
            try {
                image = Image.FromFile(context.Request.PhysicalPath);
                graphic = Graphics.FromImage(image);
                Font font = new Font("幼圆", 24.0f, FontStyle.Regular);
                graphic.DrawString("orangepoet", font, new SolidBrush(Color.MediumSeaGreen), 20.0f, image.Height - font.Height - 10);
                image.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            } finally {
                if (graphic != null)
                    graphic.Dispose();
                if (image != null)
                    image.Dispose();
            }
        }
    }
}
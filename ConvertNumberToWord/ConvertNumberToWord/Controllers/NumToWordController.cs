using ConvertNumberToWord.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ConvertNumberToWord.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class NumToWordController : ApiController
    {
        bool centconversion = false;

        public string Get(string number)
        {
            NumToWordLogic numToWordLogic = new NumToWordLogic();

            return numToWordLogic.GetWords(number);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(int orderId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            
            if (!Request.Content.IsMimeMultipartContent())
            {
                return StatusCode(HttpStatusCode.UnsupportedMediaType);
            }

            var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();

            //We will use two content part one is used to store the json another is used to store the image binary.
            var fileBytes = await filesReadToProvider.Contents[0].ReadAsByteArrayAsync();

            string filePath = "~/Files/Prec_Oderid_"+orderId+".jpg";
            File.WriteAllBytes(HttpContext.Current.Server.MapPath(filePath), fileBytes);

            //    var postedFile = httpRequest.Files.Get("file");

            //    if (postedFile != null && postedFile.ContentLength > 0)
            //    {
            //        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".png", ".jpeg" };
            //        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
            //        var extension = ext.ToLower();
            //        if (!AllowedFileExtensions.Contains(extension))
            //        {
            //            var message = string.Format("Please Upload image of type .jpg,.png.,.jpeg");
            //            dict.Add("error", message);
            //            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
            //        }
            //        //else
            //        //{
            //        //    var guid = Guid.NewGuid();
            //        //    var fileName = $"{guid.ToString()}_{DateTime.Now.ToString("ddmmyyyhhss")}.{Path.GetExtension(postedFile.FileName)}";
            //        //    var filePath = HttpContext.Current.Server.MapPath("~/Images/prescription/" + fileName);
            //        //    postedFile.SaveAs(filePath);

            //        //}

            //        var message1 = string.Format("Image Updated Successfully.");
            //        return Request.CreateErrorResponse(HttpStatusCode.Created, message1);
            //    }
            //    var res = string.Format("Please Upload a image.");
            //    dict.Add("error", res);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            //}
            //catch (Exception ex)
            //{
            //    var res = string.Format("some Message");
            //    dict.Add("error", res);
            //    return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            //}

            return Ok();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}

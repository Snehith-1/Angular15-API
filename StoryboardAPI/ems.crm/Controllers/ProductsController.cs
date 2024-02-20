
using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using System.Configuration;
using System.Net.Http.Headers;

namespace ems.crm.Controllers
{
    [RoutePrefix("api/Products")]
    [Authorize]
    public class ProductsController : ApiController
    {
        
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        objDacrm objDacrm = new objDacrm();

        public object imagePath { get; private set; }

        // Module Summary
        [ActionName("GetProductSummarys")]
        [HttpGet]
        public HttpResponseMessage GetProductSummarys()
        {
            MdlProducts values = new MdlProducts();
            objDacrm.DaGetProductSummarys(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getproducttypedropdowns")]
        [HttpGet]
        public HttpResponseMessage Getproducttypedropdowns()
        {
            MdlProducts values = new MdlProducts();
            objDacrm.DaGetproducttypedropdowns(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getproductgroupdropdowns")]
        [HttpGet]
        public HttpResponseMessage Getproductgroupdropdowns()
        {
            MdlProducts values = new MdlProducts();
            objDacrm.DaGetproductgroupdropdowns(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getproductunitclassdropdowns")]
        [HttpGet]
        public HttpResponseMessage Getproductunitclassdropdowns()
        {
            MdlProducts values = new MdlProducts();
            objDacrm.DaGetproductunitclassdropdowns(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getproductunitdropdowns")]
        [HttpGet]
        public HttpResponseMessage Getproductunitdropdowns()
        {
            MdlProducts values = new MdlProducts();
            objDacrm.DaGetproductunitdropdowns(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcurrencydropdowns")]
        [HttpGet]
        public HttpResponseMessage Getcurrencydropdowns()
        {
            MdlProducts values = new MdlProducts();
            objDacrm.DaGetcurrencydropdowns(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Terms and conditions
        [ActionName("PostProducts")]
        [HttpPost]
        public HttpResponseMessage PostProducts(product_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDacrm.DaPostProducts(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("UpdatedProducts")]
        [HttpPost]
        public HttpResponseMessage UpdatedProducts(product_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDacrm.DaUpdatedProducts(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("deleteProductSummarys")]
        [HttpGet]
        public HttpResponseMessage deleteProductSummarys(string product_gid)
        {
            product_list objresult = new product_list();
            objDacrm.DadeleteProductSummarys(product_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("editProductSummarys")]
        [HttpGet]
        public HttpResponseMessage editProductSummarys(string user_gid, string product_gid)
        {
            MdlProducts objresult = new MdlProducts();
            objDacrm.DaeditProductSummarys(user_gid, product_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getproductunitclassdropdownonchanges")]
        [HttpGet]
        public HttpResponseMessage Getproductunitclassdropdownonchanges(string user_gid, string productuomclass_gid)
        {
            MdlProducts objresult = new MdlProducts();
            objDacrm.DaGetproductunitclassdropdownonchanges(user_gid, productuomclass_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getproductunitclassdropdownonchangenames")]
        [HttpGet]
        public HttpResponseMessage Getproductunitclassdropdownonchangenames(string user_gid, string productuomclass_name)
        {
            MdlProducts objresult = new MdlProducts();
            objDacrm.DaGetproductunitclassdropdownonchangenames(user_gid, productuomclass_name, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("UpdatedProductcosts")]
        [HttpPost]
        public HttpResponseMessage UpdatedProductcosts(product_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDacrm.DaUpdatedProductcosts(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("enquiryProductSummarys")]
        [HttpGet]
        public HttpResponseMessage enquiryProductSummarys(string user_gid, string product_gid)
        {
            product_list objresult = new product_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDacrm.DaenquiryProductSummarys(getsessionvalues.user_gid, product_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetProductattributesSummarys")]
        [HttpGet]
        public HttpResponseMessage GetProductattributesSummarys(string user_gid, string product_gid)
        {
            MdlProducts objresult = new MdlProducts();
            objDacrm.DaGetProductattributesSummarys(user_gid, product_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("ProductUploadExcels")]
        [HttpPost]
        public HttpResponseMessage ProductUploadExcels()
        {
            HttpRequest httpRequest;
            product_list values = new product_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDacrm.DaProductUploadExcels(httpRequest, getsessionvalues.user_gid, objResult, values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetProductReportExports")]
        [HttpGet]
        public HttpResponseMessage GetProductReportExports()
        {
            MdlProducts values = new MdlProducts();
            objDacrm.DaGetProductReportExports(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlProducts objresult = new MdlProducts();
            objDacrm.DaGetbreadcrumb (user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    



        [ActionName("ImageUploadFile")]
        [HttpPost]
        public HttpResponseMessage ImageUploadFile()
        {
            HttpRequest httpRequest = HttpContext.Current.Request;
            HttpPostedFile httpPostedFile = httpRequest.Files.Count > 0 ? httpRequest.Files[0] : null;

            if (httpPostedFile == null)
            {
                // No image was uploaded, handle the error or return an error response.
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No image file uploaded.");
            }

            // Get the user GID and other necessary data for image insertion into the database.
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            logintoken getsessionvalues = new logintoken();
            session_values Objgetgid = new session_values();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            string userGid = getsessionvalues.user_gid;

            try
            {
                // Save the image to a directory on the server
                string lscompanyCode = GetCompanyCode();
                string lsfilePath = ConfigurationManager.AppSettings["imgfile_path1"];

                // Check if the directory exists, and create it if not
                if (!Directory.Exists(lsfilePath))
                {
                    Directory.CreateDirectory(lsfilePath);
                }

                string fileExtension = Path.GetExtension(httpPostedFile.FileName).ToLower();
                string lsfileGid = GetMasterGID("UPLF") + fileExtension;
                string lspath = Path.Combine(lsfilePath, lsfileGid);
                string product_gid = httpRequest.Form["product_gid"];

                using (FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write))
                {
                    httpPostedFile.InputStream.CopyTo(file);
                }

                // Save the image path to the database
                string imagePath = "assets/images/product/" + lsfileGid;

                // Create an instance of the objDacrm class
                objDacrm objDacrmInstance = new objDacrm();
                objDacrmInstance.UpdateProductImagePath(imagePath, httpPostedFile.FileName, fileExtension, product_gid);

                // Return a success response if the image was uploaded and path was saved successfully
                return Request.CreateResponse(HttpStatusCode.OK, "Image uploaded and path saved successfully.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the image upload and path saving process
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        private static int sequenceCounter = 1;
        public string GetMasterGID(string prefix)
        {
            // Implement the logic to generate the unique GID based on the provided prefix and a sequential number.
            // You can use the 'sequenceCounter' to generate the sequential number part.
            string sequentialNumber = sequenceCounter.ToString("D6"); // "D6" ensures 6 digits with leading zeros.

            // Increment the sequence counter for the next use.
            sequenceCounter++;

            string masterGID = prefix + sequentialNumber;
            return masterGID;
        }


        private string GenerateUniqueId()
        {
            // Replace this with your logic to generate a unique identifier.
            // You can use DateTime, Guid, or any other method to create a unique value.
            return Guid.NewGuid().ToString("N");
        }

        public string GetCompanyCode()
        {
            try
            {
                // Implement the logic to fetch the company code from the database or any other source
                string companyCode = ""; // Replace this with the actual logic to get the company code

                return companyCode;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the data retrieval process
                throw ex;
            }
        }



     
     
   

        [ActionName("downloadImages")]
        [HttpGet]
        public HttpResponseMessage downloadImages (string product_gid )
        {
            MdlProducts objresult = new MdlProducts();
            objDacrm.DadownloadImages(product_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }















    }
}
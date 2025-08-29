using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Net;

namespace AgMvcAdmin.Models.Common
{
    public class Nics
    {

        public void RunNics()
        {
            var requestXml = new XmlDocument();

            // build XML request 

            var httpRequest = HttpWebRequest.Create("https://services.cjis.gov:8443/NICS/NOE/Sandbox");
            httpRequest.Method = "POST";
            httpRequest.ContentType = "text/xml";

            // set appropriate headers

            using (var requestStream = httpRequest.GetRequestStream())
            {
                requestXml.Save(requestStream);
            }

            using (var response = (HttpWebResponse)httpRequest.GetResponse())
            using (var responseStream = response.GetResponseStream())
            {
                // may want to check response.StatusCode to
                // see if the request was successful

                var responseXml = new XmlDocument();
                responseXml.Load(responseStream);
            }

        }



    }
}
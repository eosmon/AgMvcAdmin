using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class GunsAm
    {

        public void CheckRest()
        {
            var client = new RestClient("https://functions-prod.gunsamerica.com/api/identity/connect/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "scope=InventoryServiceAPI&grant_type=client_credentials&client_id=11536065383442-c&client_secret=eb5bc76b-a942-465d-87a0-5afff696f4b6", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }

    }
}
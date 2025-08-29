//using System;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Text;
//using Newtonsoft.Json.Linq;

//namespace AgMvcAdmin.Models
//{
//    public class ShipUps
//    {
//        private readonly IHttpClientFactory _httpClientFactory;
//        public static async Task Main(string[] args)
//        {
//            var client = _httpClientFactory.CreateClient();
//            client.DefaultRequestHeaders.Add("transId", "string");
//            client.DefaultRequestHeaders.Add("transactionSrc", "testing");
//            client.DefaultRequestHeaders.Add("Authorization", "Bearer <YOUR_TOKEN_HERE>");
//            JObject json = JObject.Parse(@"{
//        RateRequest: {
//          Request: {
//            TransactionReference: {
//              CustomerContext: 'CustomerContext'
//            }
//          },
//          Shipment: {
//            Shipper: {
//              Name: 'ShipperName',
//              ShipperNumber: 'ShipperNumber',
//              Address: {
//                AddressLine: [
//                  'ShipperAddressLine',
//                  'ShipperAddressLine',
//                  'ShipperAddressLine'
//                ],
//                City: 'TIMONIUM',
//                StateProvinceCode: 'MD',
//                PostalCode: '21093',
//                CountryCode: 'US'
//              }
//            },
//            ShipTo: {
//              Name: 'ShipToName',
//              Address: {
//                AddressLine: [
//                  'ShipToAddressLine',
//                  'ShipToAddressLine',
//                  'ShipToAddressLine'
//                ],
//                City: 'Alpharetta',
//                StateProvinceCode: 'GA',
//                PostalCode: '30005',
//                CountryCode: 'US'
//              }
//            },
//            ShipFrom: {
//              Name: 'ShipFromName',
//              Address: {
//                AddressLine: [
//                  'ShipFromAddressLine',
//                  'ShipFromAddressLine',
//                  'ShipFromAddressLine'
//                ],
//                City: 'TIMONIUM',
//                StateProvinceCode: 'MD',
//                PostalCode: '21093',
//                CountryCode: 'US'
//              }
//            },
//            PaymentDetails: {
//              ShipmentCharge: {
//                Type: '01',
//                BillShipper: {
//                  AccountNumber: 'ShipperNumber'
//                }
//              }
//            },
//            Service: {
//              Code: '03',
//              Description: 'Ground'
//            },
//            NumOfPieces: '1',
//            Package: {
//              SimpleRate: {
//                Description: 'SimpleRateDescription',
//                Code: 'XS'
//              },
//              PackagingType: {
//                Code: '02',
//                Description: 'Packaging'
//              },
//              Dimensions: {
//                UnitOfMeasurement: {
//                  Code: 'IN',
//                  Description: 'Inches'
//                },
//                Length: '5',
//                Width: '5',
//                Height: '5'
//              },
//              PackageWeight: {
//                UnitOfMeasurement: {
//                  Code: 'LBS',
//                  Description: 'Pounds'
//                },
//                Weight: '1'
//              }
//            }
//          }
//        }
//      }");
//            var postData = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
//            var Version = "YOUR_version_PARAMETER";
//            var Requestoption = "YOUR_requestoption_PARAMETER";
//            var request = await client.PostAsync("https://wwwcie.ups.com/api/rating/" + Version + "/" + Requestoption + "?additionalinfo=string", postData);
//            var response = await request.Content.ReadAsStringAsync();

//            Console.WriteLine(response);
//        }
 

//    }
//}
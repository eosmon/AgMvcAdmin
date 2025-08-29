using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class CustomerBaseModel
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string StateName { get; set; }
        public int CustAddressID { get; set; }
        public int StateId { get; set; }
        public int ZipCode { get; set; }
        public int ZipExt { get; set; }


        public DateTime AgreeSignedDate { get; set; }

        public CustomerBaseModel() {}

        public CustomerBaseModel(int id, int sid, int zip, int ext, string adr, string ste, string cty)
        {
            CustAddressID = id;
            StateId = sid;
            ZipCode = zip;
            ZipExt = ext;
            Address = adr;
            Suite = ste;
            City = cty;
        }

            //var cid = Int32.TryParse(dr["ID"].ToString(), out i0) ? Convert.ToInt32(dr["ID"]) : i0;
            //var sid = Int32.TryParse(dr["StateID"].ToString(), out i0) ? Convert.ToInt32(dr["StateID"]) : i0;
            //var zip = Int32.TryParse(dr["ZipCode"].ToString(), out i0) ? Convert.ToInt32(dr["ZipCode"]) : i0;
            //var ext = Int32.TryParse(dr["ZipExt"].ToString(), out i0) ? Convert.ToInt32(dr["ZipExt"]) : i0;

            //var fnm = dr["Address"].ToString();
            //var lnm = dr["Suite"].ToString();
            //var eml = dr["City"].ToString();

        public CustomerBaseModel(string company, string address, string city, string state, string phone, int zip)
        {
            Company = company;
            Address = address;
            City = city;
            StateName = state;
            Phone = phone;
            ZipCode = zip;
        }

        public CustomerBaseModel(string companyName, string firstName, string lastName, string address, string city, int stateId, int zipCode, int zipExt)
        {
            Company = companyName;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            StateId = stateId;
            ZipCode = zipCode;
            ZipExt = zipExt;
        }

        public CustomerBaseModel(string firstName, string lastName, string address, string city, int stateId, int zipCode, int zipExt, string state)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            StateId = stateId;
            ZipCode = zipCode;
            ZipExt = zipExt;
            StateName = state;
        }

        public CustomerBaseModel(string firstName, string lastName, string address, string city, int stateId, int zipCode, int zipExt)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            StateId = stateId;
            ZipCode = zipCode;
            ZipExt = zipExt;
        }

        public CustomerBaseModel(string email, string fName, string lName, string addr, string suite, string city, string phone, int state, int zip, int ext)
        {
            EmailAddress = email;
            FirstName = fName;
            LastName = lName;
            Address = addr;
            Suite = suite;
            City = city;
            Phone = phone;
            StateId = state;
            ZipCode = zip;
            ZipExt = ext;
        }
    }


}
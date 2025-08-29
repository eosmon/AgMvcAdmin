using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models
{
    public class SecurityModel
    {
        public Guid UserKey { get; set; }
        public int MaxLoginAttempts { get; set; }
        public int LockMinutesAdded { get; set; }
        public int SecurityQuestion1 { get; set; }
        public int SecurityQuestion2 { get; set; }
        public int SecurityQuestion3 { get; set; }
        public string TextQuestion1 { get; set; }
        public string TextQuestion2 { get; set; }
        public string TextQuestion3 { get; set; }
        public string EncryUk { get; set; }
        public string StrUserKey { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TempPswd { get; set; }
        public string SecurityAnswer1 { get; set; }
        public string SecurityAnswer2 { get; set; }
        public string SecurityAnswer3 { get; set; }
        public bool ResetPassword { get; set; }

        public SecurityModel() { }

        public SecurityModel(string user, string pswd)
        {
            UserName = user;
            Password = pswd;
        }

        public SecurityModel(string un, string tp, string q1, string q2, string q3, string a1, string a2, string a3)
        {
            UserName = un;
            TempPswd = tp;
            TextQuestion1 = q1;
            TextQuestion2 = q2;
            TextQuestion3 = q3;
            SecurityAnswer1 = a1;
            SecurityAnswer2 = a2;
            SecurityAnswer3 = a3;
        }

        public SecurityModel(Guid uk, string un, string pw, bool rp, int q1, int q2, int q3, string a1, string a2, string a3)
        {
            UserKey = uk;
            UserName = un;
            Password = pw;
            ResetPassword = rp;
            SecurityQuestion1 = q1;
            SecurityQuestion2 = q2;
            SecurityQuestion3 = q3;
            SecurityAnswer1 = a1;
            SecurityAnswer2 = a2;
            SecurityAnswer3 = a3;
        }
    }
}
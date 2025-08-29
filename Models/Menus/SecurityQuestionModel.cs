using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgMvcAdmin.Models.Menus
{
    public class SecurityQuestionModel
    {
        public List<SelectListItem> SecurityQuestions { get; set; }
        public int? QuestionId { get; set; }
        public string QuestionName { get; set; }
    }
}
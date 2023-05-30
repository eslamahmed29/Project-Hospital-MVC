using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace el7elm.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    public class relation_model
    {
        public int? pre_id { get; set; }
        public int? patient_id { get; set; }
        public int? doc_id { get; set; }
        public string description { get; set; }

        public string patient_name { get; set; }
        public string patient_phone { get; set; }
        public string patient_address { get; set; }
        public string patient_gender { get; set; }
        public System.DateTime patient_birthdate { get; set; }
    }
}
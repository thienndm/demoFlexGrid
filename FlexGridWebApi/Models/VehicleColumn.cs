using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexGridWebApi.Models
{
    //[Serializable]
    public class VehicleColumn
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsReadOnly { get; set; }

    }
}
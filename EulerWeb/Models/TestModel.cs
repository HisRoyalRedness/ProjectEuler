using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace EulerWeb.Models
{
    [DataContract]
    public class TestModel
    {
        public TestModel(int index = 0)
        {
            Index = index;
        }

        public string SomeField => DateTime.Now.ToString();

        [DataMember]
        public int Index { get; set; }
        
    }
}
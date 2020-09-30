using System;
using System.Runtime.Serialization;

namespace MySqlTestApplication.Models
{
    [DataContract]
    public class DataPoint
    {
		public DataPoint(string label, decimal y)
        {
            this.Label = label;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string Label = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<decimal> Y = null;
    }
}
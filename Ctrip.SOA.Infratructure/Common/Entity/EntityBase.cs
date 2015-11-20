using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;


namespace Ctrip.SOA.Infratructure.Common.Entity
{
    public class EntityBase
    {
        public string ConvertEntityToLogString()
        {
            var returnValue = "";
            var serializer = new JavaScriptSerializer();
            serializer.Serialize(this);
            return returnValue;
        }
    }
}

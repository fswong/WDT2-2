using Assignment2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Assignment2WebAPI.REST
{
    [DataContract]
    public class RESTCollection<IRESTObject> : IHttpResponse
    {
        [DataMember]
        public List<IRESTObject> data { get;  set; }

        public RESTCollection() { }

        public RESTCollection(List<IRESTObject> data) {
            this.data = data;
        }
    }
}

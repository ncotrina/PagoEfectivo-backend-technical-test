using System;
using System.Collections.Generic;

namespace ApplicationServices.Utilities
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public T Data { get; set; }
        public IList<T> DataList { get; set; }
    }
}

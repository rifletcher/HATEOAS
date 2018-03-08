using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HATEOAS.Models
{
    public class LinksWrapperList<T>
    {
        public List<LinksWrapper<T>> Values { get; set; }
        public List<LinkInfo> Links { get; set; }
    }
}

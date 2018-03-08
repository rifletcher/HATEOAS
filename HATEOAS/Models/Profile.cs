using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HATEOAS.Models
{
    public class ProfileBase
    {
        public string Name { get; set; }
    }
    public class Profile: ProfileBase
    {
        public long ProfileId { get; set; }
    }

    public class ProfileRequest: ProfileBase
    {

    }
}

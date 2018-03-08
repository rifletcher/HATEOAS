using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HATEOAS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HATEOAS.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly IUrlHelper _urlHelper;

        public ProfileController(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        [HttpGet]
        //[HttpGet(Name = "GetProfiles")]
        public IEnumerable<Profile> GetAll()
        {
            var result = new List<Profile>
            {
                new Profile() {ProfileId = 1, Name = "Profile 1"},
                new Profile() {ProfileId = 2, Name = "Profile 2"}
            };
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = new Profile
            {
                ProfileId = 1,
                Name = "Profile 1"
            };
            var outputModel = ToOutputModel_Links(result);
            return Ok(outputModel);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private LinksWrapper<Profile> ToOutputModel_Links(Profile model)
        {
            return new LinksWrapper<Profile>
            {
                Value = model,
                Links = GetLinks_Model(model)
            };
        }

        private List<LinkInfo> GetLinks_Model(Profile model)
        {
            var links = new List<LinkInfo>
            {
                new LinkInfo
                {
                    Href = _urlHelper.Link("GetProfile", new {profileId = model.ProfileId}),
                    Rel = "self",
                    Method = "GET"
                },
                new LinkInfo
                {
                    Href = _urlHelper.Link("PutProfile", new {profileId = model.ProfileId}),
                    Rel = "put",
                    Method = "PUT"
                },
                new LinkInfo
                {
                    Href = _urlHelper.Link("DeleteProfile", new {profileId = model.ProfileId}),
                    Rel = "delete",
                    Method = "DELETE"
                }
            };

            return links;
        }

    }
}

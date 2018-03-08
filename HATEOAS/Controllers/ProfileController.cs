using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HATEOAS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HATEOAS.Controllers
{
    [Route("profile")]
    public class ProfileController : Controller
    {
        private readonly IUrlHelper _urlHelper;

        public ProfileController(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetProfiles")]
        public ActionResult GetAll(PagingParams pagingParams)
        {
            var profileList = new List<Profile>();
            profileList.Add(new Profile() { ProfileId = 1, Name = "Profile 1" });
            profileList.Add(new Profile() { ProfileId = 1, Name = "Profile 2" });

            var result = new PagedList<Profile>(profileList.AsQueryable(), pagingParams.PageNumber, pagingParams.PageSize);
            var outputModel = ToOutputModel_Links(result);
            return Ok(outputModel);
        }

        [HttpGet("{id}", Name = "GetProfile")]
        public ActionResult Get(int id)
        {
            var result = new Profile
            {
                ProfileId = 1,
                Name = "Profile 1"
            };
            var outputModel = ToOutputModel_Links(result);
            return Ok(outputModel);
        }


        [HttpPost(Name = "PostProfile")]
        public ActionResult Post([FromBody]ProfileRequest profileRequest)
        {
            if (profileRequest == null)
                return BadRequest();

            var profile = new Profile() { ProfileId = 1, Name = profileRequest.Name };
            var outputModel = ToOutputModel_Links(profile);
            return CreatedAtRoute("GetProfile", new { id = outputModel.Value.ProfileId }, outputModel);
        }

        [HttpPut("{id}", Name = "PutProfile")]
        public ActionResult Put(int id, [FromBody]ProfileRequest profileRequest)
        {
            if (profileRequest == null)
                return BadRequest();
            return NoContent();
        }

        [HttpPatch("{id}", Name = "PatchProfile")]
        public ActionResult UpdatePatch(int id, [FromBody]ProfileRequest profileRequest)
        {
            if (profileRequest == null)
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteProfile")]
        public ActionResult Delete(int id)
        {
            return NoContent();
        }

        private LinksWrapper<Profile> ToOutputModel_Links(Profile model)
        {
            return new LinksWrapper<Profile>
            {
                Value = model,
                Links = GetLinks_Model(model)
            };
        }

        private LinksWrapperList<Profile> ToOutputModel_Links(PagedList<Profile> model)
        {
            return new LinksWrapperList<Profile>
            {
                Values = model.List.Select(m => ToOutputModel_Links(m)).ToList(),
                Links = GetLinks_List(model)
            };
        }

        private List<LinkInfo> GetLinks_Model(Profile model)
        {
            var links = new List<LinkInfo>
            {
                new LinkInfo
                {
                    Href = _urlHelper.Link("GetProfile", new {id = model.ProfileId }),
                    Rel = "self",
                    Method = "GET"
                },
                new LinkInfo
                {
                    Href = _urlHelper.Link("PutProfile", new {id = model.ProfileId }),
                    Rel = "profile.update",
                    Method = "PUT"
                },
                new LinkInfo
                {
                    Href = _urlHelper.Link("PatchProfile", new { id = model.ProfileId }),
                    Rel = "update-partial-movie",
                    Method = "PATCH"
                },
                new LinkInfo
                {
                    Href = _urlHelper.Link("DeleteProfile", new { id = model.ProfileId }),
                    Rel = "profile.delete",
                    Method = "DELETE"
                }
            };

            return links;
        }

        private List<LinkInfo> GetLinks_List(PagedList<Profile> model)
        {
            var links = new List<LinkInfo>();

            links.Add(new LinkInfo
            {
                Href = _urlHelper.Link("GetProfiles",
                            new { PageNumber = model.PageNumber, PageSize = model.PageSize }),
                Rel = "self",
                Method = "GET"
            });

            if (model.HasPreviousPage)
                links.Add(new LinkInfo
                {
                    Href = _urlHelper.Link("GetProfiles",
                            new { PageNumber = model.PreviousPageNumber, PageSize = model.PageSize }),
                    Rel = "profile.previous-page",
                    Method = "GET"
                });

            if (model.HasNextPage)
                links.Add(new LinkInfo
                {
                    Href = _urlHelper.Link("GetProfiles",
                            new { PageNumber = model.NextPageNumber, PageSize = model.PageSize }),
                    Rel = "profile.next-page",
                    Method = "GET"
                });

            links.Add(new LinkInfo
            {
                Href = _urlHelper.Link("PostProfile", new { }),
                Rel = "profile.create",
                Method = "POST"
            });

            return links;
        }


    }
}

using System;
using Domain.Samples;
using Domain.Models.Samples;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Api.Utils;
using Microsoft.AspNetCore.Authorization;
using Domain.Models.Pagination;

namespace Api.Controllers
{

    [EnableCors("ClientApp")]
    [Produces("application/json")]
   
    public class SampleController : Controller
    {
        private ISampleRepository repo;
        private ISampleService service;

        public SampleController(ISampleRepository repo, ISampleService service)
        {
            this.repo = repo;
            this.service = service;
        }

        [HttpGet, ActionName("GetSample")]
        [Route("api/Samples")]
        public IActionResult GetSample(Guid? id)
        {
            var result = new List<Sample>();

            if (id == null)
            {
                result.AddRange(this.repo.Retrieve());
            }
            else
            {
                result.Add(this.repo.Retrieve(id.Value));
            }

            return Ok(result);
        }
        [HttpGet]
        [Route("api/Samples/{pageNumber}/{rowNumber}/")]
        public IActionResult Paginate(int pageNumber, int rowNumber) {
            try
            {
                Pagination<Sample> result = this.repo.Paginate(pageNumber, rowNumber);
                return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpPost]
        //[Authorize]
        [Route("api/Samples")]
        public IActionResult CreateSample([FromBody] Sample sample)
        {
            try
            {
                if (sample == null)
                {
                    return BadRequest();
                }
                var result = service.Save(Guid.Empty, sample);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        //[Authorize]
        [Route("api/Samples")]
        public IActionResult DeleteSample(Guid id)
        {
            var found = this.repo.Retrieve(id);

            if(found == null)
            {
                return NotFound();
            }
            else
            {
                this.repo.Delete(id);
            }

            return NoContent();
        }
        
        [HttpPut]
        //[Authorize]
        [Route("api/Samples")]
        public IActionResult UpdateSample([FromBody] Sample sample, Guid id)
        {
            try
            {
                if(sample == null)
                {
                    return BadRequest();
                }

                var itemToUpdate = this.repo.Retrieve(id);

                if(itemToUpdate == null)
                {
                    return NotFound();
                }

                itemToUpdate.ApplyChanges(sample);

                var result = this.service.Save(id, itemToUpdate);

                return Ok(result);
                
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
       
    }
}
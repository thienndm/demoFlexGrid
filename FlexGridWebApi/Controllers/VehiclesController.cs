using FlexGridWebApi.Models;
using FlexGridWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlexGridWebApi.Controllers
{
    public class VehiclesController : ApiController
    {
        static readonly IVehicleRepository repository = new VehicleRepository();

        //[Route("vehicles")]
        [HttpGet]
        public IEnumerable<VehicleInfo> GetAllVehicles()
        {
            return repository.GetAll();
        }

        /// <summary>
        /// The route attribute with the data type of GUID
        /// </summary>
        /// <param name="Id" />
        /// <returns>
        //[Route(Name = "VehicleInfoById")]
        [HttpGet]
        public VehicleInfo GetVehicleInfo(Guid Id)
        {
            VehicleInfo item = repository.Get(Id);

            if (item == null)
            {
                //NOTE: Uncomment the following line of code to use HttpError object.
                //var message = string.Format("Product with id = {0} not found", id);
                //HttpError err = new HttpError(message);
                //return Request.CreateResponse(HttpStatusCode.NotFound, err);

                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No vehicle with ID = {0} exists", Id)),
                    ReasonPhrase = "The given VehicleID was not found."
                };
                throw new HttpResponseException(resp);
            }

            return item;
        }

        public IEnumerable<VehicleColumn> GetVehicleColumns()
        {
            return repository.GetVehicleColumns();
        }

        //[Route("vehicles/{make:alpha}")]
        public IEnumerable<VehicleInfo> GetVehiclesByMake(string make)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Make, make, StringComparison.OrdinalIgnoreCase));
        }

        //[Route("vehicles")]
        [HttpPost]
        public HttpResponseMessage PostVehicle(VehicleInfo item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<VehicleInfo>(HttpStatusCode.Created, item);

            //string uri = Url.Link("vehicles/GetVehicleInfo", new { id = item.Id });
            //response.Headers.Location = new Uri(uri);
            return response;
        }

        //[Route("vehicles")]
        [HttpPut]
        public void PutVehicle(VehicleInfo item)
        {
            //item.Id = Id;
            if (!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        //[Route("vehicles")]
        [HttpDelete]
        public void DeleteVehicle(Guid Id)
        {
            VehicleInfo item = repository.Get(Id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(Id);
        }
    }
}

using EAP.Booking.Api.DataAccess;
using EAP.Booking.Api.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace EAP.Booking.Api.Controllers
{
    public class ClientsController : ApiController
    {
        ClientRepository _repository = new ClientRepository();
        private readonly EAPBMSContext _db = new EAPBMSContext();

        [Authorize]
        // GET api/Clients
        public IQueryable<ClientEntity> GetClients()
        {
                return _repository.GetAll();
        }


        // GET api/Clients/5
        [Authorize]
        [ResponseType(typeof(ClientEntity))]
        public IHttpActionResult GetClientById(int id)
        {
            ClientEntity client = _repository.GetAll().FirstOrDefault(p => p.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [Authorize]
        [HttpPut]
        // PUT api/Clients/5
        public IHttpActionResult UpdateClientById(int id, ClientEntity client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.Id)
            {
                return BadRequest();
            }
            
            try
            {
                _repository.Edit(client);
                _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.ClientExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize]
        // POST api/Clients
        [ResponseType(typeof(ClientEntity))]
        public IHttpActionResult InsertClient(ClientEntity client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(client);
            _repository.Save();

            return CreatedAtRoute("DefaultApi", new { id = client.Id }, client);
        }

        [Authorize]
        // DELETE api/Clients/5
        [ResponseType(typeof(ClientEntity))]
        public IHttpActionResult DeleteClient(int id)
        {
            ClientEntity client = _repository.GetAll().FirstOrDefault(p => p.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            _repository.Delete(client);
            _repository.Save();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

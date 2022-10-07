using EAP.Booking.Api.DataAccess;
using EAP.Booking.Api.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace EAP.Booking.Api.Controllers
{
    public class SessionsBookedController : ApiController
    {
        SessionsBookedRepository _repository = new SessionsBookedRepository();
        private readonly EAPBMSContext _db = new EAPBMSContext();

        // GET api/SessionsBooked
        [Authorize]
        public IQueryable<SessionsBookedEntity> GetSessionsBooked()
        {
            return _repository.GetAllWithRelation(o => o.ClientData, o => o.StaffSessionData, o => o.StaffSessionData.ConsultationTypeData, o => o.StaffSessionData.StaffData);
        }

        // GET api/SessionsBooked/5
        [Authorize]
        [ResponseType(typeof(SessionsBookedEntity))]
        public IHttpActionResult GetSessionsBookedById(int id)
        {
            SessionsBookedEntity sessionBooked = _repository.GetAll().First(p => p.Id == id);
            if (sessionBooked == null)
            {
                return NotFound();
            }

            return Ok(sessionBooked);
        }

        // PUT api/SessionsBooked/5
        [Authorize]
        [HttpPut]
        public IHttpActionResult UpdateSessionsBookedById(int id, SessionsBookedEntity sessionsBooked)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sessionsBooked.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Edit(sessionsBooked);
                _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.SessionBookedExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/SessionsBooked
        [Authorize]
        [ResponseType(typeof(SessionsBookedEntity))]
        public IHttpActionResult InsertSessionsBooked(SessionsBookedEntity sessionsBooked)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(sessionsBooked);
            _repository.Save();

            return CreatedAtRoute("DefaultApi", new { id = sessionsBooked.Id }, sessionsBooked);
        }

        // DELETE api/SessionsBooked/5
        [Authorize]
        [ResponseType(typeof(SessionsBookedEntity))]
        public IHttpActionResult DeleteSessionsBooked(int id)
        {
            SessionsBookedEntity sessionsBooked = _repository.GetAll().First(p => p.Id == id);
            if (sessionsBooked == null)
            {
                return NotFound();
            }

            _repository.Delete(sessionsBooked);
            _repository.Save();

            return Ok(sessionsBooked);
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

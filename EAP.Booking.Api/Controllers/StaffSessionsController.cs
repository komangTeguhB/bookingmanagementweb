using EAP.Booking.Api.DataAccess;
using EAP.Booking.Api.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace EAP.Booking.Api.Controllers
{
    public class StaffSessionsController : ApiController
    {
        StaffSessionRepository _repository = new StaffSessionRepository();
        private readonly EAPBMSContext _db = new EAPBMSContext();

        // GET api/StaffSessions
        [Authorize]
        public IQueryable<StaffSessionEntity> GetStaffSessions()
        {
            return _repository.GetAllWithRelation(o => o.ConsultationTypeData, o => o.StaffData);
        }

        // GET api/StaffSessions/5
        [Authorize]
        [ResponseType(typeof(StaffSessionEntity))]
        public IHttpActionResult GetStaffSessionById(int id)
        {
            StaffSessionEntity staffSession = _repository.GetAll().First(p => p.Id == id);
            if (staffSession == null)
            {
                return NotFound();
            }

            return Ok(staffSession);
        }

        // PUT api/StaffSessions/5
        [Authorize]
        [HttpPut]
        public IHttpActionResult UpdateStaffSessionById(int id, StaffSessionEntity staffSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != staffSession.Id)
            {
                return BadRequest();
            }
                       
            try
            {
                _repository.Edit(staffSession);
                _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.StaffSessionExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/StaffSessions
        [Authorize]
        [ResponseType(typeof(StaffSessionEntity))]
        public IHttpActionResult InsertStaffSession(StaffSessionEntity staffSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(staffSession);
            _repository.Save();

            return CreatedAtRoute("DefaultApi", new { id = staffSession.Id }, staffSession);
        }

        // DELETE api/StaffSessions/5
        [Authorize]
        [ResponseType(typeof(StaffSessionEntity))]
        public IHttpActionResult DeleteStaffSession(int id)
        {
            StaffSessionEntity staffSession = _repository.GetAll().First(p => p.Id == id);
            if (staffSession == null)
            {
                return NotFound();
            }

            _repository.Delete(staffSession);
            _repository.Save();

            return Ok(staffSession);
        }

        // GET api/GetStaffSessionAvailable
        [Authorize]
        [HttpGet]
        [Route("api/GetStaffSessionAvailable")]
        [ResponseType(typeof(StaffSessionEntity))]
        public IEnumerable GetStaffSessionAvailable()
        {
            IList<StaffSessionEntity> staffSessions = _repository.GetAllWithRelation(o => o.ConsultationTypeData, o => o.StaffData)
                                                        .Where(x => x.IsActive == 1).ToList();
            return staffSessions;
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

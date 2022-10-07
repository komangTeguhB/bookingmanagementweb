using EAP.Booking.Api.DataAccess;
using EAP.Booking.Api.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace EAP.Booking.Api.Controllers
{
    public class StaffsController : ApiController
    {
        StaffRepository _repository = new StaffRepository();
        private readonly EAPBMSContext _db = new EAPBMSContext();

        // GET api/Staffs
        [Authorize]
        public IQueryable<StaffEntity> GetClients()
        {
            return _repository.GetAll();
        }

        // GET api/Staffs/5
        [Authorize]
        [ResponseType(typeof(StaffEntity))]
        public IHttpActionResult GetStaffById(int id)
        {
            StaffEntity staff = _repository.GetAll().First(p => p.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }

        // PUT api/Staffs/5
        [Authorize]
        [HttpPut]
        public IHttpActionResult UpdateStaffById(int id, StaffEntity staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != staff.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Edit(staff);
                _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.StaffExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Staffs
        [Authorize]
        [ResponseType(typeof(StaffEntity))]
        public IHttpActionResult InsertStaff(StaffEntity staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(staff);
            _repository.Save();

            return CreatedAtRoute("DefaultApi", new { id = staff.Id }, staff);
        }

        // DELETE api/Staffs/5
        [Authorize]
        [ResponseType(typeof(StaffEntity))]
        public IHttpActionResult DeleteStaff(int id)
        {
            StaffEntity staff = _repository.GetAll().First(p => p.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            _repository.Delete(staff);
            _repository.Save();

            return Ok(staff);
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

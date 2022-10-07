using EAP.Booking.Api.DataAccess;
using EAP.Booking.Api.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace EAP.Booking.Api.Controllers
{
    public class ConsultationTypesController : ApiController
    {
        ConsultationTypeRepository _repository = new ConsultationTypeRepository();
        private readonly EAPBMSContext _db = new EAPBMSContext();

        // GET api/ConsultationTypes
        [Authorize]
        public IQueryable<ConsultationTypeEntity> GetConsultationTypes()
        {
            return _repository.GetAll();
        }

        // GET api/ConsultationTypes/5
        [Authorize]
        [ResponseType(typeof(ConsultationTypeEntity))]
        public IHttpActionResult GetConsultationTypeById(int id)
        {
            ConsultationTypeEntity consultationType = _repository.GetAll().First(p => p.Id == id);
            if (consultationType == null)
            {
                return NotFound();
            }

            return Ok(consultationType);
        }

        // PUT api/ConsultationTypes/5
        [Authorize]
        [HttpPut]
        public IHttpActionResult UpdateConsultationTypeById(int id, ConsultationTypeEntity consultationType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != consultationType.Id)
            {
                return BadRequest();
            }

           
            try
            {
                _repository.Edit(consultationType);
                _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.ConsultationTypeExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/ConsultationTypes
        [Authorize]
        [ResponseType(typeof(ConsultationTypeEntity))]
        public IHttpActionResult InsertConsultationType(ConsultationTypeEntity consultationType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(consultationType);
            _repository.Save();

            return CreatedAtRoute("DefaultApi", new { id = consultationType.Id }, consultationType);
        }

        // DELETE api/ConsultationTypes/5
        [Authorize]
        [ResponseType(typeof(ConsultationTypeEntity))]
        public IHttpActionResult DeleteConsultationType(int id)
        {
            ConsultationTypeEntity consultationType = _repository.GetAll().First(p => p.Id == id);
            if (consultationType == null)
            {
                return NotFound();
            }

            _repository.Delete(consultationType);
            _repository.Save();

            return Ok(consultationType);
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

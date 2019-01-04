using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ValaisBooking.Models;

namespace ValaisBooking.Controllers
{
    public class LiensController : ApiController
    {
        private Context db = new Context();

        // GET: api/Liens
        public IQueryable<Lien> GetLiens()
        {
            return db.Liens;
        }

        // GET: api/Liens/5
        [ResponseType(typeof(Lien))]
        public async Task<IHttpActionResult> GetLien(int id)
        {
            Lien lien = await db.Liens.FindAsync(id);
            if (lien == null)
            {
                return NotFound();
            }

            return Ok(lien);
        }


        // GET: api/Liens/{IdReservation}
        [HttpGet]
        [Route("api/LiensDelete/{code}")]
        public async Task<IHttpActionResult> GetLienById(int code)
        {

            var query = from h in db.Liens
                        where h.IdReservation.Id == code
                        select h
                        ;

            List<Lien> liens = new List<Lien>();

            foreach (Lien h in query)
            {

                Lien l = await db.Liens.FindAsync(h.Id);
                if (l == null)
                {
                    return NotFound();
                }
                liens.Add(l);
            }

            
            return Ok(liens);
        }

        // GET: api/Liens/{IdReservation}/{IdRoom}
        [HttpGet]
        [Route("api/Liens/{IdReservation}/{IdRoom}")]
        public async Task<IHttpActionResult> GetLienByIds(int IdReservation, int IdRoom)
        {

            var query = from h in db.Liens
                        where h.IdReservation.Id == IdReservation && h.IdRoom.Id == IdRoom
                        select h
                        ;

            List<Lien> liens = new List<Lien>();

            foreach (Lien h in query)
            {

                Lien r = await db.Liens.FindAsync(h.Id);
                if (r == null)
                {
                    return NotFound();
                }
                liens.Add(r);

            }
            return Ok(liens);
        }

       

        // PUT: api/Liens/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLien(int id, Lien lien, Room idRoom, Reservation idReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lien.Id)
            {
                return BadRequest();
            }

            //Modifier 
            var donnee = db.Liens.Find(id);
            donnee.IdReservation = idReservation;
            donnee.IdRoom = idRoom;
            
            //db.Entry(lien).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Liens
        [ResponseType(typeof(Lien))]
        public async Task<IHttpActionResult> PostLien(Lien lien)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Liens.Add(lien);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = lien.Id }, lien);
        }

        // DELETE: api/Liens/5
        [ResponseType(typeof(Lien))]
        public async Task<IHttpActionResult> DeleteLien(int id)
        {
            Lien lien = await db.Liens.FindAsync(id);
            if (lien == null)
            {
                return NotFound();
            }

            db.Liens.Remove(lien);
            await db.SaveChangesAsync();

            return Ok(lien);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LienExists(int id)
        {
            return db.Liens.Count(e => e.Id == id) > 0;
        }
    }
}
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
    public class ReservationsController : ApiController
    {
        private Context db = new Context();

        // GET: api/Reservations
        public IQueryable<Reservation> GetReservations()
        {
            return db.Reservations;
        }

        // GET: api/Reservations/entre/sort/nom/prenom
        [HttpGet]
        [Route("api/Reservations/{entre}/{sort}/{nom}/{prenom}")]
        public async Task<IHttpActionResult> GetReservation(string entre, string sort, string nom, string prenom)
        {
            DateTime entree = DateTime.Parse(entre);
            DateTime sortie = DateTime.Parse(sort);


            //Récupérer l'id d'une réservation
            var query = from h in db.Reservations
                        where h.Debut.Equals(entree) && h.Fin.Equals(sortie) &&  h.NomClient == nom && h.PrenomClient == prenom
                        select h
                        ;



            List<Reservation> res = new List<Reservation>();

            foreach (Reservation h in query)
            {

                Reservation r = await db.Reservations.FindAsync(h.Id);
                if (r == null)
                {
                    return NotFound();
                }
                res.Add(r);

            }
            return Ok(res);
            
        }

        // GET: api/Reservations/nom/prenom/code
        [HttpGet]
        [Route("api/Reservations/{nom}/{prenom}/{code}")]
        public async Task<IHttpActionResult> ExisteReservation (string nom, string prenom, int code)
        {
                       
            var query = from h in db.Reservations
                        where h.NomClient == nom && h.PrenomClient == prenom && h.Id == code
                        select h
                        ;

            List<Reservation> reservations = new List<Reservation>();
           
            foreach (Reservation r in query)
            {
                Reservation re = await db.Reservations.FindAsync(r.Id);
                if (r == null)
                {
                    return NotFound();
                }
                reservations.Add(re);
                       

                
            }
            return Ok(reservations);

        }

        // GET: api/Reservations/nom/prenom
        [HttpGet]
        [Route("api/ReservationsOverview/{nom}/{prenom}")]
        public async Task<IHttpActionResult> OverviewReservations(string nom, string prenom)
        {

            var query = from h in db.Reservations
                        where h.NomClient == nom && h.PrenomClient == prenom 
                        select h
                        ;

            List<Reservation> reservations = new List<Reservation>();

            foreach (Reservation r in query)
            {
                Reservation re = await db.Reservations.FindAsync(r.Id);
                if (r == null)
                {
                    return NotFound();
                }
                reservations.Add(re);



            }
            return Ok(reservations);

        }


        // GET: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public async Task<IHttpActionResult> GetReservation(int id)
        {
            Reservation reservation = await db.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // PUT: api/Reservations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReservation(int id, Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.Id)
            {
                return BadRequest();
            }

            db.Entry(reservation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        public async Task<IHttpActionResult> PostReservation(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reservations.Add(reservation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public async Task<IHttpActionResult> DeleteReservation(int id)
        {
            Reservation reservation = await db.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservation);
            await db.SaveChangesAsync();

            return Ok(reservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationExists(int id)
        {
            return db.Reservations.Count(e => e.Id == id) > 0;
        }
    }
}
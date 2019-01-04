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
    public class HotelsController : ApiController
    {
        private Context db = new Context();

        // GET: api/Hotels
        [HttpGet]
        [Route("api/Hotels/")]
        public IQueryable<Hotel> GetHotels()
        {
            return db.Hotels;
        }

        // GET: api/Hotels/5
        [HttpGet]
        [Route("api/Hotels/{id}")]
        public async Task<IHttpActionResult> GetHotel(int id)
        {
            Hotel hotel = await db.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }



        // GET: api/Hotels/{name}
        [HttpGet]
        [Route("api/Hotel/{nomHotel}")]
        public async Task<IHttpActionResult> GetHotelByName (string nomHotel)
        {

            var query = from h in db.Hotels
                        where h.Name ==nomHotel
                        select h
                        ;

            List<Hotel> hotels = new List<Hotel>();

            foreach (Hotel h in query)
            {
               
                Hotel hotel = await db.Hotels.FindAsync(h.Id);
                if (hotel == null)
                {
                    return NotFound();
                }
                hotels.Add(hotel);
                
            }
            return Ok(hotels);
        }

        /*

        // PUT: api/Hotels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHotel(int id, Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotel.Id)
            {
                return BadRequest();
            }

            db.Entry(hotel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        [ResponseType(typeof(Hotel))]
        public async Task<IHttpActionResult> PostHotel(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hotels.Add(hotel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [ResponseType(typeof(Hotel))]
        public async Task<IHttpActionResult> DeleteHotel(int id)
        {
            Hotel hotel = await db.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            db.Hotels.Remove(hotel);
            await db.SaveChangesAsync();

            return Ok(hotel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HotelExists(int id)
        {
            return db.Hotels.Count(e => e.Id == id) > 0;
        }
        */
    }
}
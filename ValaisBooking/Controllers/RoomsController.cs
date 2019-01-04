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
    public class RoomsController : ApiController
    {
        private Context db = new Context();

        // GET: api/Rooms
        public IQueryable<Room> GetRooms()
        {
            return db.Rooms;
        }

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> GetRoom(int id)
        {
            Room room = await db.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // GET: api/Room/{name}
        [HttpGet]
        [Route("api/Room/{nomHotel}")]
        public async Task<IHttpActionResult> GetRoomByName(string nomHotel)
        {

            var query = from h in db.Rooms.Include("IdHotel")
                        where h.IdHotel.Name == nomHotel
                        select h
                        ;

            List<Room> rooms = new List<Room>();

            foreach (Room h in query)
            {

                Room r = await db.Rooms.FindAsync(h.Id);
                if (r == null)
                {
                    return NotFound();
                }
                rooms.Add(r);

            }
            return Ok(rooms);
        }

        //Search
        [HttpGet]
        [Route("api/RoomSearch/{nomlieu}/{entre}/{sort}")]
        public async Task<IHttpActionResult> GetFreeRooms(string nomlieu, string entre, string sort)
        {

            DateTime entree = DateTime.Parse(entre);
            DateTime sortie = DateTime.Parse(entre);

            List<Room> freeRooms = new List<Room>();

            //Les chambres dans ce lieu
            var query = from h in db.Rooms.Include("IdHotel")
                        where h.IdHotel.Location == nomlieu                         
                        select h                        
                        ;
           
            
            //Liste des chambres occupées
            var queryLiens = from n in db.Liens.Include("IdReservation").Include("IdRoom.IdHotel")
                             where
                             (entree >= n.IdReservation.Debut && entree <= n.IdReservation.Fin)
                             || (sortie >= n.IdReservation.Debut && sortie <= n.IdReservation.Fin)
                             || (n.IdReservation.Debut >= entree && n.IdReservation.Debut <= sortie)
                             || (n.IdReservation.Fin >= entree && n.IdReservation.Fin <= sortie)
                             select n;

            

            foreach (Room room in query)
            {
               
                Boolean trouve = false;
                foreach (Lien lien in queryLiens)
                {
                    
                    if (lien.IdRoom.Id.Equals(room.Id))
                    {
                        trouve = true;
                    }                              
                                   
                }
                if(trouve == false)
                {
                    Room r = await db.Rooms.FindAsync(room.Id);
                    if (r == null)
                    {
                        return NotFound();
                    }
                    freeRooms.Add(r);
                }
            }

           


            return Ok(freeRooms);

            
        }

        //Advanced Search
        [HttpGet]
        [Route("api/RoomAdvancedSearch/{nomlieu}/{entre}/{sort}/{categorie}/{wifi}/{parking}/{type}/{prix}/{tv}/{hairDryer}")]
        public async Task<IHttpActionResult> GetFreeRoomsAdvancedSearch(string nomlieu, string entre, string sort, int categorie, bool wifi, bool parking, int type, decimal prix, bool tv, bool hairDryer) 
        {

            DateTime entree = DateTime.Parse(entre);
            DateTime sortie = DateTime.Parse(entre);

            List<Room> freeRooms = new List<Room>();

            //Les chambres dans ce lieu
            var query = from h in db.Rooms.Include("IdHotel")
                        where h.IdHotel.Location == nomlieu && h.IdHotel.Category==categorie && h.IdHotel.HasWifi == wifi && h.IdHotel.HasParking == parking && h.Type == type && h.Price == prix && h.HasTV == tv && h.HasHairDryer == hairDryer
                        select h
                        ;
            

            //Liste des chambres occupées
            var queryLiens = from n in db.Liens.Include("IdReservation").Include("IdRoom.IdHotel")
                             where
                             (entree >= n.IdReservation.Debut && entree <= n.IdReservation.Fin)
                             || (sortie >= n.IdReservation.Debut && sortie <= n.IdReservation.Fin)
                             || (n.IdReservation.Debut >= entree && n.IdReservation.Debut <= sortie)
                             || (n.IdReservation.Fin >= entree && n.IdReservation.Fin <= sortie)
                             select n;



            foreach (Room room in query)
            {

                Boolean trouve = false;
                foreach (Lien lien in queryLiens)
                {

                    if (lien.IdRoom.Id.Equals(room.Id))
                    {
                        trouve = true;
                    }

                }
                if (trouve == false)
                {
                    Room r = await db.Rooms.FindAsync(room.Id);
                    if (r == null)
                    {
                        return NotFound();
                    }
                    freeRooms.Add(r);
                }
            }




            return Ok(freeRooms);


        }

        //Chambres simples
        [HttpGet]
        [Route("api/SimpleRooms/{nomlieu}/{entre}/{sort}/{simple}")]
        public async Task<IHttpActionResult> SimpleRooms (string nomlieu, string entre, string sort, int simple)
        {

            DateTime entree = DateTime.Parse(entre);
            DateTime sortie = DateTime.Parse(entre);

            List<Room> freeRooms = new List<Room>();

            //Les chambres dans ce lieu
            var query = from h in db.Rooms.Include("IdHotel")
                        where h.IdHotel.Location == nomlieu && h.Type == simple
                        select h
                        ;


            //Liste des chambres occupées
            var queryLiens = from n in db.Liens.Include("IdReservation").Include("IdRoom.IdHotel")
                             where
                             (entree >= n.IdReservation.Debut && entree <= n.IdReservation.Fin)
                             || (sortie >= n.IdReservation.Debut && sortie <= n.IdReservation.Fin)
                             || (n.IdReservation.Debut >= entree && n.IdReservation.Debut <= sortie)
                             || (n.IdReservation.Fin >= entree && n.IdReservation.Fin <= sortie)
                             select n;



            foreach (Room room in query)
            {

                Boolean trouve = false;
                foreach (Lien lien in queryLiens)
                {

                    if (lien.IdRoom.Id.Equals(room.Id))
                    {
                        trouve = true;
                    }

                }
                if (trouve == false)
                {
                    Room r = await db.Rooms.FindAsync(room.Id);
                    if (r == null)
                    {
                        return NotFound();
                    }
                    freeRooms.Add(r);
                }
            }




            return Ok(freeRooms);


        }

        //Chambres doubles
        [HttpGet]
        [Route("api/DoubleRooms/{nomlieu}/{entre}/{sort}/{doubles}")]
        public async Task<IHttpActionResult> DoubleRooms(string nomlieu, string entre, string sort, int doubles)
        {

            DateTime entree = DateTime.Parse(entre);
            DateTime sortie = DateTime.Parse(entre);

            List<Room> freeRooms = new List<Room>();

            //Les chambres dans ce lieu
            var query = from h in db.Rooms.Include("IdHotel")
                        where h.IdHotel.Location == nomlieu && h.Type == doubles
                        select h
                        ;


            //Liste des chambres occupées
            var queryLiens = from n in db.Liens.Include("IdReservation").Include("IdRoom.IdHotel")
                             where
                             (entree >= n.IdReservation.Debut && entree <= n.IdReservation.Fin)
                             || (sortie >= n.IdReservation.Debut && sortie <= n.IdReservation.Fin)
                             || (n.IdReservation.Debut >= entree && n.IdReservation.Debut <= sortie)
                             || (n.IdReservation.Fin >= entree && n.IdReservation.Fin <= sortie)
                             select n;



            foreach (Room room in query)
            {

                Boolean trouve = false;
                foreach (Lien lien in queryLiens)
                {

                    if (lien.IdRoom.Id.Equals(room.Id))
                    {
                        trouve = true;
                    }

                }
                if (trouve == false)
                {
                    Room r = await db.Rooms.FindAsync(room.Id);
                    if (r == null)
                    {
                        return NotFound();
                    }
                    freeRooms.Add(r);
                }
            }




            return Ok(freeRooms);


        }

        //Add-on : changement de prix selon l'occupation
        //Chambres libres selon le nom de l'hôtel
        [HttpGet]
        [Route("api/RoomHotelName/{nomHotel}/{entre}/{sort}")]
        public async Task<IHttpActionResult> GetFreeRoomsHotel (string nomHotel, string entre, string sort)
        {

            DateTime entree = DateTime.Parse(entre);
            DateTime sortie = DateTime.Parse(entre);

            List<Room> freeRooms = new List<Room>();

            //Les chambres dans ce lieu
            var query = from h in db.Rooms.Include("IdHotel")
                        where h.IdHotel.Name == nomHotel
                        select h
                        ;


            //Liste des chambres occupées
            var queryLiens = from n in db.Liens.Include("IdReservation").Include("IdRoom.IdHotel")
                             where
                             (entree >= n.IdReservation.Debut && entree <= n.IdReservation.Fin)
                             || (sortie >= n.IdReservation.Debut && sortie <= n.IdReservation.Fin)
                             || (n.IdReservation.Debut >= entree && n.IdReservation.Debut <= sortie)
                             || (n.IdReservation.Fin >= entree && n.IdReservation.Fin <= sortie)
                             select n;



            foreach (Room room in query)
            {

                Boolean trouve = false;
                foreach (Lien lien in queryLiens)
                {

                    if (lien.IdRoom.Id.Equals(room.Id))
                    {
                        trouve = true;
                    }

                }
                if (trouve == false)
                {
                    Room r = await db.Rooms.FindAsync(room.Id);
                    if (r == null)
                    {
                        return NotFound();
                    }
                    freeRooms.Add(r);
                }
            }




            return Ok(freeRooms);


        }

        //Add-on : changement de prix selon l'occupation
        //Chambres selon le nom de l'hôtel
        [HttpGet]
        [Route("api/RoomHotelName/{nomHotel}")]
        public async Task<IHttpActionResult> GetFreeRoomsHotels(string nomHotel)
        {

            List<Room> freeRooms = new List<Room>();

            //Les chambres dans ce lieu
            var query = from h in db.Rooms.Include("IdHotel")
                        where h.IdHotel.Name == nomHotel
                        select h
                        ;



            foreach (Room room in query)
            {

                    Room r = await db.Rooms.FindAsync(room.Id);
                    if (r == null)
                    {
                        return NotFound();
                    }
                    freeRooms.Add(r);
                }
            




            return Ok(freeRooms);


        }

        /*
        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Id)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> DeleteRoom(int id)
        {
            Room room = await db.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(room);
            await db.SaveChangesAsync();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.Id == id) > 0;
        }
        */
    }
}
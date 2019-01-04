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
    public class PicturesController : ApiController
    {
        private Context db = new Context();

        // GET: api/Pictures
        public IQueryable<Picture> GetPictures()
        {
            return db.Pictures;
        }

        // GET: api/Pictures/5
        [ResponseType(typeof(Picture))]
        public async Task<IHttpActionResult> GetPicture(int id)
        {
            Picture picture = await db.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            return Ok(picture);
        }

        // GET: api/Picture/{name}
        [HttpGet]
        [Route("api/Picture/{nomHotel}")]
        public async Task<IHttpActionResult> GetPictureByName(string nomHotel)
        {

            var query = from h in db.Pictures.Include("IdRoom.IdHotel")
                        where h.IdRoom.IdHotel.Name == nomHotel
                        select h
                        ;

            List<Picture> pictures = new List<Picture>();

            foreach (Picture h in query)
            {

                Picture pic = await db.Pictures.FindAsync(h.Id);
                if (pic == null)
                {
                    return NotFound();
                }
                pictures.Add(pic);

            }
            return Ok(pictures);
        }


        //Liste des pictures des chambres libres
        // GET: api/Rooms/{lieu}/{dateDebut}/{dateFin}
        [HttpGet]
        [Route("api/PictureSearch/{nomlieu}/{entre}/{sort}")]
        public async Task<IHttpActionResult> GetFreePictures(string nomlieu, string entre, string sort)
        {
            List<Picture> freePictures = new List<Picture>();
            DateTime entree = DateTime.Parse(entre);
            DateTime sortie = DateTime.Parse(entre);


            //Les pictures dans ce lieu
            var query = from h in db.Pictures.Include("IdRoom")
                        where h.IdRoom.IdHotel.Location == nomlieu
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



            foreach (Picture pic in query)
            {

                Boolean trouve = false;
                foreach (Lien lien in queryLiens)
                {

                    if (lien.IdRoom.Id.Equals(pic.IdRoom.Id))
                    {
                        trouve = true;
                    }

                }
                if (trouve == false)
                {
                    Picture r = await db.Pictures.FindAsync(pic.Id);
                    if (r == null)
                    {
                        return NotFound();
                    }
                    freePictures.Add(r);
                }
            }




            return Ok(freePictures);


        }

        //Liste des pictures des chambres libres
        // GET: api/Rooms/{lieu}/{dateDebut}/{dateFin}
        [HttpGet]
        [Route("api/PictureAdvancedSearch/{nomlieu}/{entre}/{sort}/{categorie}/{wifi}/{parking}/{type}/{prix}/{tv}/{hairDryer}")]
        public async Task<IHttpActionResult> GetFreePictures(string nomlieu, string entre, string sort,int categorie, bool wifi, bool parking, int type, decimal prix, bool tv, bool hairDryer)
        {
            List<Picture> freePictures = new List<Picture>();
            DateTime entree = DateTime.Parse(entre);
            DateTime sortie = DateTime.Parse(entre);


            //Les pictures dans ce lieu
            var query = from h in db.Pictures.Include("IdRoom.IdHotel")
                        where h.IdRoom.IdHotel.Location == nomlieu && h.IdRoom.IdHotel.Category==categorie && h.IdRoom.IdHotel.HasWifi ==wifi && h.IdRoom.IdHotel.HasParking == parking && h.IdRoom.Type==type && h.IdRoom.Price==prix && h.IdRoom.HasTV == tv && h.IdRoom.HasHairDryer==hairDryer
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



            foreach (Picture pic in query)
            {

                Boolean trouve = false;
                foreach (Lien lien in queryLiens)
                {

                    if (lien.IdRoom.Id.Equals(pic.IdRoom.Id))
                    {
                        trouve = true;
                    }

                }
                if (trouve == false)
                {
                    Picture r = await db.Pictures.FindAsync(pic.Id);
                    if (r == null)
                    {
                        return NotFound();
                    }
                    freePictures.Add(r);
                }
            }




            return Ok(freePictures);


        }

        /*
        // PUT: api/Pictures/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPicture(int id, Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != picture.Id)
            {
                return BadRequest();
            }

            db.Entry(picture).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(id))
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

        // POST: api/Pictures
        [ResponseType(typeof(Picture))]
        public async Task<IHttpActionResult> PostPicture(Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pictures.Add(picture);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = picture.Id }, picture);
        }

        // DELETE: api/Pictures/5
        [ResponseType(typeof(Picture))]
        public async Task<IHttpActionResult> DeletePicture(int id)
        {
            Picture picture = await db.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            db.Pictures.Remove(picture);
            await db.SaveChangesAsync();

            return Ok(picture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PictureExists(int id)
        {
            return db.Pictures.Count(e => e.Id == id) > 0;
        }
        */
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using MoviesCF_Odata.Data;
using MoviesCF_Odata.Models;

namespace MoviesCF_Odata.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using MoviesCF_Odata.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Flicks>("Flicks");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class FlicksController : ODataController
    {
        private FlicksDbContext db = new FlicksDbContext();

        // GET: odata/Flicks
        [EnableQuery]
        public IQueryable<Flicks> GetFlicks()
        {
            return db.Flicks;
        }

        // GET: odata/Flicks(5)
        [EnableQuery]
        public SingleResult<Flicks> GetFlicks([FromODataUri] int key)
        {
            return SingleResult.Create(db.Flicks.Where(flicks => flicks.FId == key));
        }

        // PUT: odata/Flicks(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Flicks> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Flicks flicks = db.Flicks.Find(key);
            if (flicks == null)
            {
                return NotFound();
            }

            patch.Put(flicks);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlicksExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(flicks);
        }

        // POST: odata/Flicks
        public IHttpActionResult Post(Flicks flicks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Flicks.Add(flicks);
            db.SaveChanges();

            return Created(flicks);
        }

        // PATCH: odata/Flicks(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Flicks> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Flicks flicks = db.Flicks.Find(key);
            if (flicks == null)
            {
                return NotFound();
            }

            patch.Patch(flicks);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlicksExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(flicks);
        }

        // DELETE: odata/Flicks(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Flicks flicks = db.Flicks.Find(key);
            if (flicks == null)
            {
                return NotFound();
            }

            db.Flicks.Remove(flicks);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FlicksExists(int key)
        {
            return db.Flicks.Count(e => e.FId == key) > 0;
        }
    }
}

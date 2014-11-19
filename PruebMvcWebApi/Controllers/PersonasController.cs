using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PruebMvcWebApi.Models;

namespace PruebMvcWebApi.Controllers
{
    public class PersonasController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();

        // GET api/Personas
        public IEnumerable<Persona> GetPersonas()
        {
            return db.Personas.AsEnumerable();
        }

        // GET api/Personas/5
        public Persona GetPersona(int id)
        {
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return persona;
        }

        // PUT api/Personas/5
        public HttpResponseMessage PutPersona(int id, Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != persona.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(persona).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Personas
        public HttpResponseMessage PostPersona(Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(persona);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, persona);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = persona.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Personas/5
        public HttpResponseMessage DeletePersona(int id)
        {
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Personas.Remove(persona);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, persona);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
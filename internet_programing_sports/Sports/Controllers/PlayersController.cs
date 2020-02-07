using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sports.Data;
using Sports.Models;

namespace Sports.Controllers
{
    public class PlayersController : Controller
    {
        private readonly SportsContext _context;

        public PlayersController(SportsContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            return View(await _context.Players.ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Players
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (players == null)
            {
                return NotFound();
            }

            return View(players);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerId,FullName,Age,Contry,SportId")] Players players)
        {
            if (ModelState.IsValid)
            {
                _context.Add(players);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(players);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Players.FindAsync(id);
            if (players == null)
            {
                return NotFound();
            }
            return View(players);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,FullName,Age,Contry,SportId")] Players players)
        {
            if (id != players.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(players);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayersExists(players.PlayerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(players);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Players
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (players == null)
            {
                return NotFound();
            }

            return View(players);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var players = await _context.Players.FindAsync(id);
            _context.Players.Remove(players);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool PlayersExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }

        //api....
        public HttpResponseMessage Get()
        {
            HttpResponseMessage httpResponseMessage;
            List<Players> listadoPersonas;

            try
            {
                listadoPersonas = Players.pl();

                if (listadoPersonas.Count > 0)//Si la lista de personas no se encuentra vacía
                {
                    httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, listadoPersonas);
                }
                else
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);//Error 500 Internal Server Error
            }

            return httpResponseMessage;
        }
        /////public HttpResponseMessage Get(int id)
        //{
        //        var entity = _context.Players.FirstOrDefault(e => e.PlayerId == id);
        //        if (entity != null)
        //        {
        //            return new HttpResponseMessage(HttpStatusCode.OK);//Request.(StatusCode(HttpStatusCode.OK, entity))
        //        }
        //        else
        //        {
        //        return new HttpResponseMessage(HttpStatusCode.Unauthorized);//,"Employee with Id " + id.ToString() + " not found");
        //        }
        //}

        // POST: api/Players/Post
        //public HttpResponseMessage Post([FromBody]Players players)
        //{
        //    HttpResponseMessage httpResponseMessage;
        //    try
        //    {
        //        _context.Players.Add(players);
        //        _context.SaveChanges();
        //        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        //    }
        //    catch (Exception e)
        //    {
        //        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);//Error 500 Internal Server Error
        //    }

        //    return httpResponseMessage;
        //}

        //    return new HttpResponseMessage(HttpStatusCode.Unauthorized);

        //public HttpResponseMessage Post([FromBody] Players players)
        //{
        //    try
        //    {

        //            _context.Players.Add(players);
        //            _context.SaveChanges();

        //            var message = new HttpResponseMessage(HttpStatusCode.OK); //HttpResponseMessage(HttpStatusCode.Created, players);

        //        message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());

        //            return message;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.Unauthorized); //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}
      
            static List<string> serviceData = LoadService();

            public static List<string> LoadService()
            {
                return new List<string>() { "Mobile Recharge", "Bill Payment" };
            }
            // GET: api/Service
            public HttpResponseMessage Get()
            {
                return Request.CreateResponse<IEnumerable<string>>(HttpStatusCode.OK, serviceData);
            }

            // GET: api/Service/5
            public HttpResponseMessage Get(int id)
            {
                if (serviceData.Count > id)
                    return Request.CreateResponse<string>(HttpStatusCode.OK, serviceData[id]);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item Not Found");
            }

            // POST: api/Service
            public HttpResponseMessage Post([FromBody]string value)
            {
                serviceData.Add(value);
                return Request.CreateResponse(HttpStatusCode.Created, "Item Added Successfully");
            }

            // PUT: api/Service/5
            public HttpResponseMessage Put(int id, [FromBody]string value)
            {
                serviceData[id] = value;
                return Request.CreateResponse(HttpStatusCode.OK, "Item Updated Successfully");
            }

            // DELETE: api/Service/5
            public HttpResponseMessage Delete(int id)
            {
                serviceData.RemoveAt(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Item Deleted Successfully");
            }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel_Reservations_Manager.Data;
using Hotel_Reservations_Manager.Data.Entities;
using Hotel_Reservations_Manager.Services.Abstraction;
using Hotel_Reservations_Manager.Services;
using Microsoft.AspNetCore.Authorization;
using Hotel_Reservations_Manager.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Hotel_Reservations_Manager.Controllers
{
    public class ReservationsController : Controller
    {
        //private readonly IReservationServices _reservationServices;
        //private readonly IRoomServices _roomServices;
        //private readonly IClientServices _clientServices;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ReservationsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.Reservation.Include(r => r.Room).Include(r => r.User).Include(r => r.Clients);
            return View(await applicationDbContext.ToListAsync());
        }

        [Route("Reservations/Search/{name}")]
        public IActionResult Search(string roomNumber)
        {

            return View(_context.Reservation.Where(r => r.Room.Number == roomNumber));
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reservation
                .Include(r => r.Room)
                .Include(r => r.User)
                .Include(r => r.Clients)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Number");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
            return View();
        }

        // POST: Reservations/Create
        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,UserId,StartDate,EndDate,BreackfastIncluded,AllInclusive,Id")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.UserId = _userManager.GetUserId(User);
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Name", reservation.RoomId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Name", reservation.RoomId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,UserId,StartDate,EndDate,BreackfastIncluded,AllInclusive,Id")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Name", reservation.RoomId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Room)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}

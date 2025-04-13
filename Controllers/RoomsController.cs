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
using Microsoft.AspNetCore.Authorization;
using Hotel_Reservations_Manager.DTOs;

namespace Hotel_Reservations_Manager.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomServices _roomServices;

        public RoomsController(IRoomServices roomServices)
        {
            _roomServices = roomServices;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            return View(await _roomServices.GetAllAsync());
        }

        [Route("Rooms/Search/{number}")]
        public IActionResult Search(string number)
        {
            return View(_roomServices.GetByNumber(number));
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomServices.GetByIdAsync(id.Value);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Capacity,Type,IsOccupied,PriceAdult,PriceChild,Number,Id")] RoomDTO roomDto)
        {
            if (ModelState.IsValid)
            {
                await _roomServices.CreateAsync(roomDto);
                return RedirectToAction(nameof(Index));
            }
            return View(roomDto);
        }

        // GET: Rooms/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomServices.GetByIdAsync(id.Value);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Capacity,Type,IsOccupied,PriceAdult,PriceChild,Number,Id")] RoomDTO roomDto)
        {
            if (id != roomDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roomServices.UpdateAsync(roomDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RoomExists(roomDto.Id))
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
            return View(roomDto);
        }

        // GET: Rooms/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomServices.GetByIdAsync(id.Value);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomServices.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RoomExists(int id)
        {
            var item = await _roomServices.GetByIdAsync(id);
            return item != null;
        }
    }
}

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
using Hotel_Reservations_Manager.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Hotel_Reservations_Manager.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientServices _clientServices;

        public ClientsController(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _clientServices.GetAllAsync());
        }


        [Route("Clients/Search/{name}")]
        public IActionResult Search(string name)
        {
            return View(_clientServices.GetByName(name));
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientServices.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Creat
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,TelephoneNumber,Email,IsOfAge,Id")] ClientDTO clientDto)
        {
            if (ModelState.IsValid)
            {
                await _clientServices.CreateAsync(clientDto);
                return RedirectToAction(nameof(Index));
            }
            return View(clientDto);
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientServices.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,TelephoneNumber,Email,IsOfAge,Id")] ClientDTO clientDto)
        {
            if (id != clientDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _clientServices.UpdateAsync(clientDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ClientExists(clientDto.Id))
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
            return View(clientDto);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientServices.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }


        // POST: Clients/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clientServices.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ClientExists(int id)
        {
            var item = await _clientServices.GetByIdAsync(id);
            return item != null;
        }
    }
}

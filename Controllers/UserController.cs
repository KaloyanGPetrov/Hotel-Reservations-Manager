using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel_Reservations_Manager.Data;
using Hotel_Reservations_Manager.Models;
using Hotel_Reservations_Manager.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Hotel_Reservations_Manager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET: UserViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        // GET: UserViewModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: UserViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserViewModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    SecondName = model.LastName,
                    LastName = model.LastName,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }


        // GET: UserViewModels/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(model);
        }

        // POST: UserViewModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.SecondName = model.SecondName;
                user.LastName = model.LastName;
                await _userManager.UpdateAsync(user);

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    if (IdentityResult.Success == await _userManager.ResetPasswordAsync(user, token, model.Password))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: UserViewModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UserViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

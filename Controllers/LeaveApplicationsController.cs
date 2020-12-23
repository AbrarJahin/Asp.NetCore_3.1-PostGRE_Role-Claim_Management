using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StartupProject_Asp.NetCore_PostGRE.Data;
using StartupProject_Asp.NetCore_PostGRE.Data.Models.AppData;
using StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity;

namespace StartupProject_Asp.NetCore_PostGRE.Controllers
{
    //[Area("Self")]
    //[Route("My/[controller]/[action]")]
    [Route("My/[controller]/[action]", Name = "[controller]_[action]")]
    [Authorize]
    public class LeaveApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public LeaveApplicationsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: LeaveApplications
        [Route("All-My-Applications")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Datatable - https://www.c-sharpcorner.com/article/jquery-datatables-with-asp-net-core-server-side-dynamic-multiple-column-searchin/
            var applicationDbContext = _context.LeaveApplications.Include(user => user.Applicant).Include(xml => xml.PreviousSignedFile);
            List<LeaveApplication> data = await applicationDbContext.ToListAsync();
            return View(data);
        }

        [Route("All-My-Applications-Ajax")]
        [HttpPost]
        public async Task<IActionResult> DatatableAjaxAsync()
        {
            //var data = await _context.LeaveApplications
            //    .Select(application => new {
            //        application.Id,
            //        application.LeaveStart,
            //        application.LeaveEnd,
            //        application.LeaveType,
            //        application.ApplicationStatus
            //    })
            //    .ToListAsync();
            //return Json(JsonConvert.SerializeObject(data));
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var customerData = _context.LeaveApplications;

                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                //}
                //if (!string.IsNullOrEmpty(searchValue))
                //{
                //    customerData = customerData.Where(m => m.FirstName.Contains(searchValue)
                //                                || m.LastName.Contains(searchValue)
                //                                || m.Contact.Contains(searchValue)
                //                                || m.Email.Contains(searchValue));
                //}
                recordsTotal = await customerData.CountAsync();
                var data = await customerData
                                    .Select(application => new {
                                        application.Id,
                                        application.LeaveStart,
                                        application.LeaveEnd,
                                        application.LeaveType,
                                        application.ApplicationStatus
                                    })
                                    .Skip(skip)
                                    .Take(pageSize)
                                    .ToListAsync();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Applicant)
                .Include(l => l.PreviousSignedFile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Create
        [Route("Apply")]
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LastSignedId"] = new SelectList(_context.XmlFiles, "Id", "FileContent");
            return View();
        }

        // POST: LeaveApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Designation,LeaveStart,LeaveEnd,LeaveType,PurposeOfLeave,AddressDuringLeave,PhoneNoDuringLeave,ApplicationStatus,LastSignedId,Id,CreateTime,LastUpdateTime,DeletionTime")] LeaveApplication leaveApplication)
        {
            if (ModelState.IsValid)
            {
                leaveApplication.Applicant = await _userManager.GetUserAsync(User);
                _context.LeaveApplications.Add(leaveApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.Users, "Id", "Id", leaveApplication.ApplicantId);
            ViewData["LastSignedId"] = new SelectList(_context.XmlFiles, "Id", "FileContent", leaveApplication.LastSignedId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.Users, "Id", "Id", leaveApplication.ApplicantId);
            ViewData["LastSignedId"] = new SelectList(_context.XmlFiles, "Id", "FileContent", leaveApplication.LastSignedId);
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Designation,LeaveStart,LeaveEnd,LeaveType,PurposeOfLeave,AddressDuringLeave,PhoneNoDuringLeave,ApplicationStatus,LastSignedId,Id,CreateTime,LastUpdateTime,DeletionTime")] LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.Id.GetValueOrDefault()))
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
            ViewData["ApplicantId"] = new SelectList(_context.Users, "Id", "Id", leaveApplication.ApplicantId);
            ViewData["LastSignedId"] = new SelectList(_context.XmlFiles, "Id", "FileContent", leaveApplication.LastSignedId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Applicant)
                .Include(l => l.PreviousSignedFile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            _context.LeaveApplications.Remove(leaveApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationExists(Guid id)
        {
            return _context.LeaveApplications.Any(e => e.Id == id);
        }
    }
}

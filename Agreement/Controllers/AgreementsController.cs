using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agreement.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Agreement.Helpers;

namespace Agreement.Controllers
{
 //   [Authorize]
    public class AgreementsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AgreementsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Agreements
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var applicationDbContext = _context.tbl_Agreements.Include(a => a.Product).Include(a => a.ProductGroup).AsQueryable();
            ViewData["GroupDescription"] = new SelectList(applicationDbContext.Select(c => c.ProductGroup), "ProductGroupId", "GroupDescription");
            ViewData["ProductDescription"] = applicationDbContext.Select(c => c.Product).Distinct();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["UserName"] = String.IsNullOrEmpty(sortOrder) ? "userName" : "";
            ViewData["GroupCode"] = sortOrder == "gCode" ? "groupCode" : "gCode";
            ViewData["ProductNumber"] = sortOrder == "pNumber" ? "productNumber" : "pNumber";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var userId = await _userManager.FindByEmailAsync(Convert.ToString(ViewData["UserName"]));

            if (!String.IsNullOrEmpty(searchString))
            {
                applicationDbContext = from query in applicationDbContext
                             where query.Product.ProductNumber.Contains(searchString)
                             || query.UserId.Contains(searchString)
                             || query.ProductGroup.GroupCode.Contains(searchString)
                             select query;
            }
            switch (sortOrder)
            {
                case "productNumber":
                    applicationDbContext =  applicationDbContext.OrderByDescending(p => p.ProductId);
                    break;
                case "groupCode":
                    applicationDbContext = applicationDbContext.OrderByDescending(p => p.ProductGroupId);
                    break;
                case "userName":
                    applicationDbContext = applicationDbContext.OrderByDescending(p => p.UserId);
                    break;
                default:
                    applicationDbContext = applicationDbContext.OrderBy(p => p.AgreementId);
                    break;
            }
            int pageSize = 3;

            return View(await PaginatedList<Agreement.Data.Agreement>.CreateAsync(applicationDbContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Agreements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tbl_Agreements == null)
            {
                return NotFound();
            }

            var agreement = await _context.tbl_Agreements
                .Include(a => a.Product)
                .Include(a => a.ProductGroup)
                .FirstOrDefaultAsync(m => m.AgreementId == id);
            if (agreement == null)
            {
                return NotFound();
            }

            return View(agreement);
        }
        
        public IActionResult AgreementCreate(int id)
        {
            ViewData["GroupDescription"] = new SelectList(_context.tbl_ProductGroups, "ProductGroupId", "GroupDescription");
            ViewData["ProductDescription"] = _context.tbl_Products;
            return PartialView("_AgreementCreate");
        }

        // POST: Agreements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgreementId,UserId,ProductId,ProductGroupId,EffectiveDate,ExpirationDate,ProductPrice,NewPrice,Active")] Agreement.Data.Agreement agreement)
        {
            agreement.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _context.Add(agreement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        // GET: Agreements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tbl_Agreements == null)
            {
                return NotFound();
            }

            var agreement = await _context.tbl_Agreements.FindAsync(id);
            if (agreement == null)
            {
                return NotFound();
            }

            ViewData["ProductDescription"] = new SelectList(_context.tbl_Products, "ProductId", "ProductDescription", agreement.ProductId);
            ViewData["GroupDescription"] = new SelectList(_context.tbl_ProductGroups, "ProductGroupId", "GroupDescription", agreement.ProductGroupId);
            return PartialView("_EditAgreement", agreement);
        }

        // POST: Agreements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgreementId,UserId,ProductId,ProductGroupId,EffectiveDate,ExpirationDate,ProductPrice,NewPrice,Active")] Agreement.Data.Agreement agreement)
        {
                try
                {
                    _context.Update(agreement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgreementExists(agreement.AgreementId))
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

        // GET: Agreements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tbl_Agreements == null)
            {
                return NotFound();
            }

            var agreement = await _context.tbl_Agreements
                .Include(a => a.Product)
                .Include(a => a.ProductGroup)
                .FirstOrDefaultAsync(m => m.AgreementId == id);
            if (agreement == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteAgreement",agreement);
        }

        // POST: Agreements/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.tbl_Agreements == null)
            {
                return Problem("Entity set 'ApplicationDbContext.tbl_Agreements'  is null.");
            }
            var agreement = await _context.tbl_Agreements.FindAsync(id);
            if (agreement != null)
            {
                _context.tbl_Agreements.Remove(agreement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgreementExists(int id)
        {
          return _context.tbl_Agreements.Any(e => e.AgreementId == id);
        }
    }
}

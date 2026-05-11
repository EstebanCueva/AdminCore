using AdminCore.Data;
using AdminCore.Models;
using AdminCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminCore.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRucValidator _rucValidator;

        public EmpresasController(AppDbContext context, IRucValidator rucValidator)
        {
            _context = context;
            _rucValidator = rucValidator;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Empresas.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empresa empresa)
        {
            if (!_rucValidator.EsValido(empresa.Ruc, out string errorRuc))
            {
                ModelState.AddModelError(nameof(Empresa.Ruc), errorRuc);
            }

            bool rucExiste = await _context.Empresas
                .AnyAsync(e => e.Ruc == empresa.Ruc);

            if (rucExiste)
            {
                ModelState.AddModelError(nameof(Empresa.Ruc), "Ya existe una empresa registrada con este RUC.");
            }

            if (!ModelState.IsValid)
            {
                return View(empresa);
            }

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            if (!_rucValidator.EsValido(empresa.Ruc, out string errorRuc))
            {
                ModelState.AddModelError(nameof(Empresa.Ruc), errorRuc);
            }

            bool rucExiste = await _context.Empresas
                .AnyAsync(e => e.Ruc == empresa.Ruc && e.Id != empresa.Id);

            if (rucExiste)
            {
                ModelState.AddModelError(nameof(Empresa.Ruc), "Ya existe otra empresa registrada con este RUC.");
            }

            if (!ModelState.IsValid)
            {
                return View(empresa);
            }

            _context.Update(empresa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

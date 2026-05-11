using AdminCore.Data;
using AdminCore.Models;
using AdminCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminCore.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRucValidator _rucValidator;

        public ProveedoresController(AppDbContext context, IRucValidator rucValidator)
        {
            _context = context;
            _rucValidator = rucValidator;
        }

        public async Task<IActionResult> Index()
        {
            var proveedores = _context.Proveedores.Include(p => p.Empresa);
            return View(await proveedores.ToListAsync());
        }

        public IActionResult Create()
        {
            CargarEmpresas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proveedor proveedor)
        {
            if (!_rucValidator.EsValido(proveedor.Ruc, out string errorRuc))
            {
                ModelState.AddModelError(nameof(proveedor.Ruc), errorRuc);
            }

            bool rucExiste = await _context.Proveedores.AnyAsync(p =>
                p.EmpresaId == proveedor.EmpresaId &&
                p.Ruc == proveedor.Ruc);

            if (rucExiste)
            {
                ModelState.AddModelError(nameof(proveedor.Ruc), "Ya existe un proveedor con este RUC en esta empresa.");
            }

            if (!ModelState.IsValid)
            {
                CargarEmpresas(proveedor.EmpresaId);
                return View(proveedor);
            }

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            CargarEmpresas(proveedor.EmpresaId);
            return View(proveedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proveedor proveedor)
        {
            if (id != proveedor.Id)
            {
                return NotFound();
            }

            if (!_rucValidator.EsValido(proveedor.Ruc, out string errorRuc))
            {
                ModelState.AddModelError(nameof(proveedor.Ruc), errorRuc);
            }

            bool rucExiste = await _context.Proveedores.AnyAsync(p =>
                p.EmpresaId == proveedor.EmpresaId &&
                p.Ruc == proveedor.Ruc &&
                p.Id != proveedor.Id);

            if (rucExiste)
            {
                ModelState.AddModelError(nameof(proveedor.Ruc), "Ya existe otro proveedor con este RUC en esta empresa.");
            }

            if (!ModelState.IsValid)
            {
                CargarEmpresas(proveedor.EmpresaId);
                return View(proveedor);
            }

            _context.Update(proveedor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var proveedor = await _context.Proveedores
                .Include(p => p.Empresa)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor != null)
            {
                _context.Proveedores.Remove(proveedor);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void CargarEmpresas(int? empresaId = null)
        {
            ViewBag.EmpresaId = new SelectList(
                _context.Empresas.Where(e => e.Activo),
                "Id",
                "Nombre",
                empresaId
            );
        }
    }
}

using AdminCore.Data;
using AdminCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminCore.Controllers
{
    public class AreasEmpresaController : Controller
    {
        private readonly AppDbContext _context;

        public AreasEmpresaController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var areas = _context.AreasEmpresa.Include(a => a.Empresa);
            return View(await areas.ToListAsync());
        }

        public IActionResult Create()
        {
            CargarEmpresas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AreaEmpresa areaEmpresa)
        {
            bool existe = await _context.AreasEmpresa.AnyAsync(a =>
                a.EmpresaId == areaEmpresa.EmpresaId &&
                a.Nombre == areaEmpresa.Nombre);

            if (existe)
            {
                ModelState.AddModelError(nameof(areaEmpresa.Nombre), "Ya existe un área con ese nombre en esta empresa.");
            }

            if (!ModelState.IsValid)
            {
                CargarEmpresas(areaEmpresa.EmpresaId);
                return View(areaEmpresa);
            }

            _context.AreasEmpresa.Add(areaEmpresa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var area = await _context.AreasEmpresa.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            CargarEmpresas(area.EmpresaId);
            return View(area);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AreaEmpresa areaEmpresa)
        {
            if (id != areaEmpresa.Id)
            {
                return NotFound();
            }

            bool existe = await _context.AreasEmpresa.AnyAsync(a =>
                a.EmpresaId == areaEmpresa.EmpresaId &&
                a.Nombre == areaEmpresa.Nombre &&
                a.Id != areaEmpresa.Id);

            if (existe)
            {
                ModelState.AddModelError(nameof(areaEmpresa.Nombre), "Ya existe un área con ese nombre en esta empresa.");
            }

            if (!ModelState.IsValid)
            {
                CargarEmpresas(areaEmpresa.EmpresaId);
                return View(areaEmpresa);
            }

            _context.Update(areaEmpresa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var area = await _context.AreasEmpresa
                .Include(a => a.Empresa)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var area = await _context.AreasEmpresa.FindAsync(id);

            if (area != null)
            {
                _context.AreasEmpresa.Remove(area);
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

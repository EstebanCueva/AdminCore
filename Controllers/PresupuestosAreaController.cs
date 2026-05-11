using AdminCore.Data;
using AdminCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminCore.Controllers
{
    public class PresupuestosAreaController : Controller
    {
        private readonly AppDbContext _context;

        public PresupuestosAreaController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var presupuestos = _context.PresupuestosArea
                .Include(p => p.Empresa)
                .Include(p => p.AreaEmpresa);

            return View(await presupuestos.ToListAsync());
        }

        public IActionResult Create()
        {
            CargarCombos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PresupuestoArea presupuesto)
        {
            bool areaPerteneceEmpresa = await _context.AreasEmpresa.AnyAsync(a =>
                a.Id == presupuesto.AreaEmpresaId &&
                a.EmpresaId == presupuesto.EmpresaId);

            if (!areaPerteneceEmpresa)
            {
                ModelState.AddModelError(nameof(presupuesto.AreaEmpresaId), "El área seleccionada no pertenece a la empresa.");
            }

            bool duplicado = await _context.PresupuestosArea.AnyAsync(p =>
                p.EmpresaId == presupuesto.EmpresaId &&
                p.AreaEmpresaId == presupuesto.AreaEmpresaId &&
                p.Mes == presupuesto.Mes &&
                p.Anio == presupuesto.Anio);

            if (duplicado)
            {
                ModelState.AddModelError("", "Ya existe un presupuesto para esta área en el mes y año seleccionados.");
            }

            if (!ModelState.IsValid)
            {
                CargarCombos(presupuesto.EmpresaId, presupuesto.AreaEmpresaId);
                return View(presupuesto);
            }

            _context.PresupuestosArea.Add(presupuesto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var presupuesto = await _context.PresupuestosArea.FindAsync(id);

            if (presupuesto == null)
            {
                return NotFound();
            }

            CargarCombos(presupuesto.EmpresaId, presupuesto.AreaEmpresaId);
            return View(presupuesto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PresupuestoArea presupuesto)
        {
            if (id != presupuesto.Id)
            {
                return NotFound();
            }

            bool areaPerteneceEmpresa = await _context.AreasEmpresa.AnyAsync(a =>
                a.Id == presupuesto.AreaEmpresaId &&
                a.EmpresaId == presupuesto.EmpresaId);

            if (!areaPerteneceEmpresa)
            {
                ModelState.AddModelError(nameof(presupuesto.AreaEmpresaId), "El área seleccionada no pertenece a la empresa.");
            }

            bool duplicado = await _context.PresupuestosArea.AnyAsync(p =>
                p.EmpresaId == presupuesto.EmpresaId &&
                p.AreaEmpresaId == presupuesto.AreaEmpresaId &&
                p.Mes == presupuesto.Mes &&
                p.Anio == presupuesto.Anio &&
                p.Id != presupuesto.Id);

            if (duplicado)
            {
                ModelState.AddModelError("", "Ya existe otro presupuesto para esta área en el mes y año seleccionados.");
            }

            if (!ModelState.IsValid)
            {
                CargarCombos(presupuesto.EmpresaId, presupuesto.AreaEmpresaId);
                return View(presupuesto);
            }

            _context.Update(presupuesto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var presupuesto = await _context.PresupuestosArea
                .Include(p => p.Empresa)
                .Include(p => p.AreaEmpresa)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (presupuesto == null)
            {
                return NotFound();
            }

            return View(presupuesto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presupuesto = await _context.PresupuestosArea.FindAsync(id);

            if (presupuesto != null)
            {
                _context.PresupuestosArea.Remove(presupuesto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<JsonResult> GetAreasPorEmpresa(int empresaId)
        {
            var areas = await _context.AreasEmpresa
                .Where(a => a.EmpresaId == empresaId)
                .Select(a => new
                {
                    id = a.Id,
                    nombre = a.Nombre
                })
                .ToListAsync();

            return Json(areas);
        }

        private void CargarCombos(int? empresaId = null, int? areaId = null)
        {
            ViewBag.Empresas = new SelectList(
                _context.Empresas.Where(e => e.Activo),
                "Id",
                "Nombre",
                empresaId
            );

            ViewBag.Areas = new SelectList(
                _context.AreasEmpresa.Where(a => empresaId != null && a.EmpresaId == empresaId),
                "Id",
                "Nombre",
                areaId
            );
        }
    }
}

using AdminCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminCore.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalEmpresas = await _context.Empresas.CountAsync();
            ViewBag.TotalAreas = await _context.AreasEmpresa.CountAsync();
            ViewBag.TotalProveedores = await _context.Proveedores.CountAsync();
            ViewBag.TotalCategorias = await _context.CategoriasGasto.CountAsync();
            ViewBag.TotalSubCategorias = await _context.SubCategoriasGasto.CountAsync();
            ViewBag.TotalPresupuestos = await _context.PresupuestosArea.CountAsync();

            ViewBag.TotalPresupuestado = await _context.PresupuestosArea
                .SumAsync(p => (decimal?)p.MontoAsignado) ?? 0;

            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Oblig2Web.Datos;
using Oblig2Web.Modelos;

namespace Oblig2Web.Pages.Reservas
{
    public class PaginaReservasModel : PageModel
    {
        private readonly AppDbContext _appContext;
        public PaginaReservasModel(AppDbContext contexto)
        {
            _appContext = contexto;
        }
        public IEnumerable<Reserva>? Reservas { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
        public IEnumerable<Pago> Pagos { get; set; }
        [TempData]
        public string Message { get; set; }
        public async Task OnGet()
        {
            Reservas = await _appContext.Reservas.ToListAsync();
            Usuarios = await _appContext.Usuarios.ToListAsync();
            Pagos = await _appContext.Pagos.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var reserva = await _appContext.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            var pagoRes = await _appContext.Pagos.FindAsync(id);

            _appContext.Reservas.Remove(reserva);
            _appContext.Pagos.Remove(pagoRes);
            await _appContext.SaveChangesAsync();
            Message = "Reserva borrada correctamente";

            return RedirectToPage();
        }
    }
}

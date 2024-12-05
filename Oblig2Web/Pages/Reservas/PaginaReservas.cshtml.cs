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
            Usuarios = contexto.Usuarios.ToList();
        }
        public IEnumerable<Reserva> Reservas { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
        public IEnumerable<Pago> Pagos { get; set; }
        [BindProperty]
        public int IdUsuario { get; set; }
        [TempData]
        public string Message { get; set; }
        public async Task OnGet()
        {
            Reservas = await _appContext.Reservas.ToListAsync();
            Pagos = await _appContext.Pagos.ToListAsync();
            foreach (var item in Reservas)
            {
                foreach (var user in Usuarios)
                {
                    if (item.IdUsuario == user.IdUsuario)
                    {
                        item.Usuario = user;
                    }
                }
            }
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var reserva = await _appContext.Reservas.FindAsync(id);

            if (reserva != null)
            {
                var pago = await _appContext.Pagos.FindAsync(id);

                _appContext.Pagos.Remove(pago);
                _appContext.Reservas.Remove(reserva);
                await _appContext.SaveChangesAsync();
                Message = "Reserva borrada correctamente";
            } else
            {
                return NotFound();
            }

            return RedirectToPage();
        }
    }
}

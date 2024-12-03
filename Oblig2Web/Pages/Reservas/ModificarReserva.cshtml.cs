using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Oblig2Web.Datos;
using Oblig2Web.Modelos;

namespace Oblig2Web.Pages.Reservas
{
    public class ModificarReservaModel : PageModel
    {
        private readonly AppDbContext _appContext;
        public ModificarReservaModel(AppDbContext contexto)
        {
            _appContext = contexto;
            HabsForEach = contexto.Habitaciones.ToList();
        }
        [BindProperty]
        public Reserva Reserva { get; set; }
        public IEnumerable<Habitacion> HabsForEach { get; set; }
        [TempData]
        public string Message { get; set; }
        public async Task OnGet(int id)
        {
            Reserva = await _appContext.Reservas.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var ReservaEnBD = await _appContext.Reservas.FindAsync(Reserva.IdReserva);
                if (ReservaEnBD != null)
                {
                    ReservaEnBD.NumHabitacion = Reserva.NumHabitacion;
                    ReservaEnBD.FechaInicio = Reserva.FechaInicio;
                    ReservaEnBD.FechaFinal = Reserva.FechaFinal;
                    await _appContext.SaveChangesAsync();
                    Message = "Reserva modificada correctamente.";
                    return RedirectToPage("PaginaReservas");
                }
                else
                {
                    return NotFound();
                }
            }
            // ModelState no es válido, volver a mostrar la página con los errores
            return Page();
        }
    }
}

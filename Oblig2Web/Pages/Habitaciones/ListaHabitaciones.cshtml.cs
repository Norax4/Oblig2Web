using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Oblig2Web.Datos;
using Oblig2Web.Modelos;

namespace Oblig2Web.Pages.Habitaciones
{
    public class ListaHabitacionesModel : PageModel
    {
        private readonly AppDbContext _appContext;
        public ListaHabitacionesModel(AppDbContext contexto)
        {
            _appContext = contexto;
        }
        public IEnumerable<Habitacion> HabitacionesL { get; set; }
        public async Task OnGet()
        {
            HabitacionesL = await _appContext.Habitaciones.ToListAsync();
        }
    }
}

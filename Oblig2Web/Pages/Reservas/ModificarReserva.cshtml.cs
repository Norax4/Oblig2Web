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
			UsuariosForEach = contexto.Usuarios.ToList();
            ResForEach = contexto.Reservas.ToList();
        }
        [BindProperty]
        public Reserva Reserva { get; set; }
		public int UserId { get; set; }
        public IEnumerable<Habitacion> HabsForEach { get; set; }
		public List<Usuario> UsuariosForEach { get; set; }
		public List<Reserva> ResForEach { get; set; }
        [TempData]
        public string Message { get; set; }
        public async Task OnGet(int id)
        {
            Reserva = await _appContext.Reservas.FindAsync(id);
			UserId = Reserva.IdUsuario;
			Console.WriteLine(Reserva.IdUsuario);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
				Console.WriteLine(Reserva.IdUsuario);
				Console.WriteLine(Reserva.HabitacionId);
				Console.WriteLine(Reserva.NumeroPersonas);
				Console.WriteLine(Reserva.FechaInicio);
				Console.WriteLine(Reserva.FechaFinal);
				return Page();
            }

			var ReservaEnBD = await _appContext.Reservas.FindAsync(Reserva.IdReserva);
			var pagoBD = await _appContext.Pagos.FindAsync(Reserva.IdReserva);
			if (ReservaEnBD != null)
			{
				ReservaEnBD.HabitacionId = Reserva.HabitacionId;

				foreach (var item in HabsForEach)
				{
					if (ReservaEnBD.HabitacionId == item.IdHabitacion)
					{
						ReservaEnBD.HabitacionElegida = item;
						ReservaEnBD.NumHabitacion = item.NumHabitacion;
					}
				}

				foreach (var item in UsuariosForEach)
				{
					if (Reserva.IdUsuario == item.IdUsuario)
					{
						ReservaEnBD.Usuario = item;
					}
				}

				foreach (var hab in HabsForEach)
				{
					foreach (var res in ResForEach)
					{
						if (hab.IdHabitacion == res.HabitacionId && res.FechaInicio == Reserva.FechaInicio)
						{
							return Page();
						}
					}
				}

				foreach (var user in UsuariosForEach)
				{
					foreach (var res in ResForEach)
					{
						if (user.IdUsuario == res.IdUsuario && res.FechaInicio == Reserva.FechaInicio)
						{
							return Page();
						}
					}
				}

				double tEstadia = (Reserva.FechaFinal - Reserva.FechaInicio).TotalDays;

				if (Reserva.FechaFinal <= Reserva.FechaInicio)
				{
					return Page();
				}
				else if (tEstadia > 30)
				{
					return Page();
				}
				else if (Reserva.HabitacionElegida.CantidadPersonas < Reserva.NumeroPersonas)
				{
					return Page();
				}

				ReservaEnBD.NumeroPersonas = Reserva.NumeroPersonas;
				ReservaEnBD.FechaInicio = Reserva.FechaInicio;
				ReservaEnBD.FechaFinal = Reserva.FechaFinal;
				ReservaEnBD.TiempoEstadia = (int)tEstadia;
				pagoBD.FechaPago = ReservaEnBD.FechaInicio;
				pagoBD.Monto = ReservaEnBD.TiempoEstadia * ReservaEnBD.HabitacionElegida.Tarifa;
				_appContext.Update(ReservaEnBD);
				await _appContext.SaveChangesAsync();
				Message = "Reserva modificada correctamente.";
				return RedirectToPage("PaginaReservas");
			}
			else
			{
				return NotFound();
			}

		}
    }
}

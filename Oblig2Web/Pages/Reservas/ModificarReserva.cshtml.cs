using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
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
        [BindProperty]
        public string MetodoPago { get; set; }
        public IEnumerable<Habitacion> HabsForEach { get; set; }
		public List<Usuario> UsuariosForEach { get; set; }
		public List<Reserva> ResForEach { get; set; }
        [TempData]
        public string Message { get; set; }
        public async Task OnGet(int id)
        {
            Reserva = await _appContext.Reservas.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
				TempData["errorMessage"] = "Hubo un error al procesar los datos. Intente nuevamente.";
				return Page();
            }

			var ReservaEnBD = await _appContext.Reservas.FindAsync(Reserva.IdReserva);
			var pagoBD = await _appContext.Pagos.FindAsync(Reserva.IdReserva);
			if (ReservaEnBD != null)
			{

				foreach (var res in ResForEach)
				{
					if (res.IdReserva != Reserva.IdReserva) 
					{
						if (Reserva.HabitacionId == res.HabitacionId && res.FechaInicio <= Reserva.FechaInicio && res.FechaFinal >= Reserva.FechaFinal)
						{
							TempData["errorMessage"] = "La habitación en la que desea hospedarse ya fue reservada para las fechas elegidas.";
							return Page();
						}
					}
				}


				foreach (var res in ResForEach)
				{
					if (res.IdReserva != Reserva.IdReserva)
					{
						if (Reserva.IdUsuario == res.IdUsuario && res.FechaInicio <= Reserva.FechaInicio && res.FechaFinal >= Reserva.FechaFinal)
						{
							TempData["errorMessage"] = "El usuario elegido ya tiene una reserva hecha para las fechas elegidas.";
							return Page();
						}
					}
				}
				

				foreach (var item in HabsForEach)
				{
					if (Reserva.HabitacionId == item.IdHabitacion)
					{
						Reserva.HabitacionElegida = item;
						Reserva.NumHabitacion = item.NumHabitacion;
					}
				}

				foreach (var item in UsuariosForEach)
				{
					if (Reserva.IdUsuario == item.IdUsuario)
					{
						Reserva.Usuario = item;
					}
				}

				double tEstadia = (Reserva.FechaFinal - Reserva.FechaInicio).TotalDays;

				if (Reserva.FechaFinal <= Reserva.FechaInicio)
				{
					TempData["errorMessage"] = "La fecha de llegada al hotel es futura a la fecha de salida. Por favor, corrijalo.";
					return Page();
				}
				else if (tEstadia > 30)
				{
					TempData["errorMessage"] = "El tiempo de su estadia supera los 30 días.";
					return Page();
				}
				else if (Reserva.HabitacionElegida.CantidadPersonas < Reserva.NumeroPersonas)
				{
					TempData["errorMessage"] = "El número de personas elegido supera la cantidad de personas que pueden quedarse en la habitación que eligió.";
					return Page();
				}

				ReservaEnBD.HabitacionId = Reserva.HabitacionId;
				ReservaEnBD.HabitacionElegida = Reserva.HabitacionElegida;
				ReservaEnBD.NumHabitacion = Reserva.NumHabitacion;
				ReservaEnBD.NumeroPersonas = Reserva.NumeroPersonas;
				ReservaEnBD.FechaInicio = Reserva.FechaInicio;
				ReservaEnBD.FechaFinal = Reserva.FechaFinal;
				ReservaEnBD.TiempoEstadia = (int)tEstadia;
				pagoBD.FechaPago = ReservaEnBD.FechaInicio;
				pagoBD.Monto = ReservaEnBD.TiempoEstadia * ReservaEnBD.HabitacionElegida.Tarifa;
				pagoBD.MetodoPago = MetodoPago;
				_appContext.UpdateRange(ReservaEnBD, pagoBD);
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

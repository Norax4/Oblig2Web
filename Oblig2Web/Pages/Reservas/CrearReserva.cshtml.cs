using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Oblig2Web.Datos;
using Oblig2Web.Modelos;

namespace Oblig2Web.Pages.Reservas
{
    public class CrearReservaModel : PageModel
    {
        private readonly AppDbContext _appContext;
        public List<Usuario> UsuariosForEach { get; set; }
        public List<Habitacion> HabsForEach { get; set; }
        public List<Reserva> ResForEach { get; set; }
        public CrearReservaModel(AppDbContext contexto)
        {
            _appContext = contexto;
            UsuariosForEach = contexto.Usuarios.ToList();
            HabsForEach = contexto.Habitaciones.ToList();
            ResForEach = contexto.Reservas.ToList();
        }
        [BindProperty] 
        public Reserva Reserva { get; set; }
        [BindProperty]
        public string MetodoPago { get; set; }
        public Pago Pago { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet() {}

        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                TempData["errorMessage"] = "Hubo un error al procesar los datos. Intente nuevamente.";
                return Page();
            }

            if (Reserva == null)
            {
                Reserva = new Reserva();
            }

            if (Reserva.HabitacionElegida == null)
            {
                foreach (var item in HabsForEach)
                {
                    if (Reserva.HabitacionId == item.IdHabitacion)
                    {
                        Reserva.HabitacionElegida = item;
                        Reserva.NumHabitacion = item.NumHabitacion;
                    }
                }
            }

            if (Reserva.Usuario == null)
            {
                foreach (var item in UsuariosForEach)
                {
                    if (Reserva.IdUsuario == item.IdUsuario)
                    {
                        Reserva.Usuario = item;
                    }
                }
            }

			foreach (var res in ResForEach)
			{
				if (Reserva.IdUsuario == res.IdUsuario && res.FechaInicio <= Reserva.FechaInicio && res.FechaFinal >= Reserva.FechaFinal)
				{
                    TempData["errorMessage"] = "El usuario elegido ya tiene una reserva hecha para las fechas elegidas.";
					return Page();
				}
			}

			foreach (var res in ResForEach)
			{
				if (Reserva.HabitacionId == res.HabitacionId && res.FechaInicio <= Reserva.FechaInicio && res.FechaFinal >= Reserva.FechaFinal)
				{
                    TempData["errorMessage"] = "La habitación en la que desea hospedarse ya fue reservada para las fechas elegidas.";
                    return Page();
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

            Reserva.FechaReserva = DateTime.Now;
            Reserva.TiempoEstadia = (int)tEstadia;

            if (Pago == null)
            {
                Pago = new Pago(Reserva, MetodoPago);
            }

            Reserva.Pago = Pago;
            _appContext.Add(Reserva);
            _appContext.Add(Pago);
            await _appContext.SaveChangesAsync();
            Message = "Reserva realizada correctamente";
            return RedirectToPage("PaginaReservas");
        }
    }
}

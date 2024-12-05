using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        [BindProperty] //Esto vincula la pagina con el modelo >>>
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
                Console.WriteLine("no");
				Console.WriteLine(Reserva.IdUsuario);
				Console.WriteLine(Reserva.HabitacionId);
				Console.WriteLine(Reserva.NumeroPersonas);
				Console.WriteLine(Reserva.FechaInicio);
				Console.WriteLine(Reserva.FechaFinal);
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

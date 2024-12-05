using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Oblig2Web.Datos;
using Oblig2Web.Modelos;

namespace Oblig2Web.Pages.Estadisticas
{
    public class IndexEstadisticasModel : PageModel
    {
        private readonly AppDbContext _appContext;

        public IndexEstadisticasModel(AppDbContext contexto)
        {
            _appContext = contexto;
        }

        public IEnumerable<Usuario> UsersList { get; set; }
        public IEnumerable<Huesped> HuespedesList { get; set; }
        public IEnumerable<Reserva> ReservasList { get; set; }
        public List<Habitacion> HabitacionesList { get; set; }
        public async Task OnGet()
        {
            UsersList = await _appContext.Usuarios.ToListAsync();
            HuespedesList = await _appContext.Huespedes.ToListAsync();
            ReservasList = await _appContext.Reservas.ToListAsync();
            var habsList = await _appContext.Habitaciones.ToListAsync();
            HabitacionesList = new List<Habitacion>();
            foreach (var h in HuespedesList)
            {
                foreach (var user in UsersList)
                {
                    if (user.HuespedId == h.IdHuesped)
                    {
                        h.Usuario = user;
                    }
                }
            }

            foreach (var user in UsersList)
            {
                foreach (var res in ReservasList)
                {
                    if (res.IdUsuario == user.IdUsuario)
                    {
                        user.CountRes += 1;
                    }
                }
            }

            foreach (var hab in habsList)
            {
                foreach (var res in ReservasList)
                {
                    if (res.HabitacionId == hab.IdHabitacion)
                    {
                        hab.CountRes += 1;
                    }
                }
            }

            var habMasRes = habsList.OrderByDescending(h => h.CountRes).FirstOrDefault();
            foreach (var hab in habsList)
            {
                if (hab.CountRes == habMasRes.CountRes)
                {
                    HabitacionesList.Add(hab);
                }
            }



        }
    }
}

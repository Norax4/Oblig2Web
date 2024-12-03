using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Oblig2Web.Modelos;

namespace Oblig2Web.Datos
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Habitacion> Habitaciones { get; set; }
		public DbSet<Huesped> Huespedes { get; set; }
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Reserva> Reservas { get; set; }
		public DbSet<Pago> Pagos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Usuario>()
				.HasOne(u => u.Huesped)
				.WithOne(h => h.Usuario)
				.HasForeignKey<Usuario>(u => u.HuespedId);

			modelBuilder.Entity<Usuario>()
				.HasMany(u => u.Reservas)
				.WithOne(r => r.Usuario)
				.HasForeignKey(r => r.IdUsuario);

			modelBuilder.Entity<Habitacion>()
				.HasMany(h => h.Reservas)
				.WithOne(r => r.HabitacionElegida)
				.HasForeignKey(r => r.HabitacionId);

			modelBuilder.Entity<Pago>()
				.HasOne(p => p.Reserva)
				.WithOne(r => r.Pago)
				.HasForeignKey<Pago>(p => p.ReservaId);
		}
	}
}

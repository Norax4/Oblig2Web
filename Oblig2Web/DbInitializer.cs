using Oblig2Web.Datos;
using Oblig2Web.Modelos;

namespace Oblig2Web
{
	public class DbInitializer
	{
		public static void Initialize(AppDbContext context)
		{
			context.Database.EnsureCreated();

			var habitaciones = new Habitacion[]
				{
					new Habitacion(101, "Simple", 2, 100),
					new Habitacion(102, "Simple", 2, 100),
					new Habitacion(103, "Simple", 2, 100),
					new Habitacion(104, "Simple", 2, 100),
					new Habitacion(105, "Simple", 2, 100),
					new Habitacion(106, "Simple", 2, 100),
					new Habitacion(107, "Simple", 2, 100),
					new Habitacion(108, "Simple", 2, 100),
					new Habitacion(109, "Simple", 2, 100),
					new Habitacion(110, "Simple", 2, 100),
					new Habitacion(201, "Doble", 4, 200),
					new Habitacion(202, "Doble", 4, 200),
					new Habitacion(203, "Doble", 4, 200),
					new Habitacion(204, "Doble", 4, 200),
					new Habitacion(205, "Doble", 4, 200),
					new Habitacion(206, "Doble", 4, 200),
					new Habitacion(207, "Doble", 4, 200),
					new Habitacion(208, "Doble", 4, 200),
					new Habitacion(209, "Doble", 4, 200),
					new Habitacion(210, "Doble", 4, 200),
					new Habitacion(301, "Suite", 4, 300),
					new Habitacion(302, "Suite", 4, 300),
					new Habitacion(303, "Suite", 4, 300),
					new Habitacion(304, "Suite", 4, 300),
					new Habitacion(305, "Suite", 4, 300),
					new Habitacion(306, "Suite", 4, 300),
					new Habitacion(307, "Suite", 4, 300),
					new Habitacion(308, "Suite", 4, 300),
					new Habitacion(309, "Suite", 4, 300),
					new Habitacion(310, "Suite", 4, 300),
				};

			var huespedes = new List<Huesped>()
			{
				new Huesped("Jose", "Garcia", DateTime.Parse("24/10/1998"), "Cédula", 55555555, 098111111, "garciajose@gmail.com"),
				new Huesped("Jose", "Martinez", DateTime.Parse("27/04/1987"), "Cédula", 23489977, 987333222, "josemartinez@gmail.com"),
				new Huesped("Maria", "Jimenez Arboleda", DateTime.Parse("03/11/2001"), "Cedula", 49665726, 093222222, "mariajiar@gmail.com"),
				new Huesped("Valentina", "Valenzuela", DateTime.Parse("09/11/2000"), "Cedula", 48759032, 092400238, "valeVal@gmail.com"),
				new Huesped("Jose", "García Miñon", DateTime.Parse("21/3/1995"), "Cedula", 32675893, 095473829, "garJose33@gmail.com")
			};

			var usuarios = new List<Usuario>()
			{
				new Usuario(huespedes[0], huespedes[0].Nombre, huespedes[0].CorreoElec, "199824Gar"),
				new Usuario(huespedes[1], huespedes[1].Nombre, huespedes[1].CorreoElec, "198727Mar"),
				new Usuario(huespedes[2], huespedes[2].Nombre, huespedes[2].CorreoElec, "200103JiAr"),
				new Usuario(huespedes[3], huespedes[3].Nombre, huespedes[3].CorreoElec, "2000Valita"),
				new Usuario(huespedes[4], huespedes[4].Nombre, huespedes[4].CorreoElec, "1995JoMiñon"),
			};

			huespedes[0].Usuario = usuarios[0];
			huespedes[1].Usuario = usuarios[1];
			huespedes[2].Usuario = usuarios[2];
			huespedes[3].Usuario = usuarios[3];
			huespedes[4].Usuario = usuarios[4];

			var reservas = new List<Reserva>()
			{
				new Reserva(habitaciones[2], 2, DateTime.Parse("12/1/2025"), DateTime.Parse("22/1/2025"), usuarios[0]),
				new Reserva(habitaciones[9], 1, DateTime.Parse("12/1/2025"), DateTime.Parse("22/1/2025"), usuarios[1]),
				new Reserva(habitaciones[2], 2, DateTime.Parse("14/12/2024"), DateTime.Parse("20/12/2024"), usuarios[3]),
				new Reserva(habitaciones[3], 2, DateTime.Parse("12/1/2025"), DateTime.Parse("22/1/2025"), usuarios[4]),
				new Reserva(habitaciones[13], 3, DateTime.Parse("12/1/2025"), DateTime.Parse("22/1/2025"), usuarios[2]),
				new Reserva(habitaciones[18], 4, DateTime.Parse("15/3/2025"), DateTime.Parse("25/3/2025"), usuarios[0]),
				new Reserva(habitaciones[25], 3, DateTime.Parse("12/5/2025"), DateTime.Parse("22/5/2025"), usuarios[2]),
			};

			usuarios[0].Reservas.Add(reservas[0]);
			usuarios[0].Reservas.Add(reservas[5]);
			usuarios[1].Reservas.Add(reservas[1]);
			usuarios[3].Reservas.Add(reservas[2]);
			usuarios[4].Reservas.Add(reservas[3]);
			usuarios[2].Reservas.Add(reservas[4]);
			usuarios[2].Reservas.Add(reservas[6]);

			habitaciones[2].Reservas.Add(reservas[0]);
			habitaciones[2].Reservas.Add(reservas[2]);
			habitaciones[9].Reservas.Add(reservas[1]);
			habitaciones[3].Reservas.Add(reservas[3]);
			habitaciones[13].Reservas.Add(reservas[4]);
			habitaciones[18].Reservas.Add(reservas[5]);
			habitaciones[25].Reservas.Add(reservas[6]);

			var pagos = new List<Pago>()
			{
				new Pago(reservas[0], "Tarjeta"),
				new Pago(reservas[1], "Tarjeta"),
				new Pago(reservas[2], "Cheque"),
				new Pago(reservas[3], "Tarjeta"),
				new Pago(reservas[4], "Tarjeta"),
				new Pago(reservas[5], "Efectivo"),
				new Pago(reservas[6], "Tarjeta"),
			};

			reservas[0].Pago = pagos[0];
			reservas[1].Pago = pagos[1];
			reservas[2].Pago = pagos[2];
			reservas[3].Pago = pagos[3];
			reservas[4].Pago = pagos[4];
			reservas[5].Pago = pagos[5];
			reservas[6].Pago = pagos[6];


			if (!context.Habitaciones.Any())
			{
				context.AddRange(habitaciones);
				context.SaveChanges();
			}

			if (!context.Huespedes.Any())
			{
				context.AddRange(huespedes);
				context.SaveChanges();
			}

			if (!context.Usuarios.Any())
			{
				context.AddRange(usuarios);
				context.SaveChanges();
			}

			if (!context.Reservas.Any())
			{
				context.AddRange(reservas);
				context.SaveChanges();
			}

			if (!context.Pagos.Any())
			{
				context.AddRange(pagos);
				context.SaveChanges();
			}
		}
	}
}

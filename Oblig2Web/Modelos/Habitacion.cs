using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Oblig2Web.Modelos
{
	public class Habitacion
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IdHabitacion { get; set; }
		[Required]
		[Display(Name = "Número de la Habitación:")]
		public int NumHabitacion { get; set; }
		[Required]
		[Display(Name = "Tipo de Habitación:")]
		public string? TipoHabitacion { get; set; }
		[Required]
		[Display(Name = "Cantidad máxima de personas:")]
		public int CantidadPersonas { get; set; }
		[Required]
		[Display(Name = "Tarifa por noche:")]
		public int Tarifa { get; set; }
		public int? CountRes = 0;
		public int TiempoReserva = 0;
		public List<Reserva> Reservas { get; set; }

		public Habitacion()
		{

		}

		public Habitacion(int numHab, string tipoHab, int cantPersonas, int tarifa)
		{
			NumHabitacion = numHab;
			TipoHabitacion = tipoHab;
			CantidadPersonas = cantPersonas;
			Tarifa = tarifa;
			Reservas = new List<Reserva>();
		}
	}
}

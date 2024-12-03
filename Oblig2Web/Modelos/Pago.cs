using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Oblig2Web.Modelos
{
	public class Pago
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IdPago { get; private set; }
		[Required]
		[ForeignKey("Reserva")]
		public int ReservaId { get; set; }
		public Reserva? Reserva { get; set; }
		[Required]
		[Column(TypeName = "date")]
		[Display(Name = "Fecha de Pago:")]
		public DateTime FechaPago { get; set; }
		[Required]
		[Display(Name = "Monto de la Estadia:")]
		public int Monto { get; set; }
		[Required]
		public string? MetodoPago { get; set; }
		public bool RealizacionPago = false;

		public Pago()
		{

		}

		public Pago(Reserva res, string metPago)
		{
			ReservaId = res.IdReserva;
			Reserva = res;
			FechaPago = res.FechaInicio;
			Monto = res.TiempoEstadia * res.HabitacionElegida.Tarifa;
			MetodoPago = metPago;
		}
	}
}

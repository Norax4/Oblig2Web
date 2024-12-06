using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Oblig2Web.Modelos
{
	public class Reserva
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IdReserva { get; set; }

		[Required(ErrorMessage = "Elegir una Habitación es obligatorio")]
        [Display(Name = "Numero de la Habitación:")]
        [ForeignKey("Habitacion")]
		public int HabitacionId { get; set; }
		public Habitacion? HabitacionElegida { get; set; }

		[Display(Name = "Numero de la Habitación:")]
		public int? NumHabitacion { get; set; }

		[Required]
		[Display(Name = "Cantidad de personas:")]
		[Range(1, 4, ErrorMessage = "La cantidad de huespedes debe ser mayor a cero.")]
		public int NumeroPersonas { get; set; }

		[Required(ErrorMessage = "Debe elegir una fecha de llegada.")]
		[Display(Name = "Fecha Inicial:")]
		[Column(TypeName = "date")]
		[DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
		public DateTime FechaInicio { get; set; }
		[Required(ErrorMessage = "Debe elegir una fecha de salida.")]
		[Display(Name = "Fecha Final:")]
		[Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaFinal { get; set; }
		public int TiempoEstadia = 0;
        [Display(Name = "Fecha de Reserva:")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaReserva { get; set; }

		[ForeignKey("Usuario")]
        [Display(Name = "Usuario Correspondiente:")]
        [Required(ErrorMessage = "Elegir un Usuario es obligatorio")]
		public int IdUsuario { get; set; }
		public Usuario? Usuario { get; set; }

		public Pago? Pago { get; set; }

		public Reserva()
		{

		}

		public Reserva(int hab, int numP, DateTime fechaI, DateTime fechaF, int user)
		{
			HabitacionId = hab;
			NumeroPersonas = numP;
			FechaInicio = fechaI;
			FechaFinal = fechaF;
			IdUsuario = user;
		}

        public Reserva(Habitacion hab, int numP, DateTime fechaI, DateTime fechaF, Usuario user)
        {
            HabitacionId = hab.IdHabitacion;
            HabitacionElegida = hab;
            NumHabitacion = hab.NumHabitacion;
            NumeroPersonas = numP;
            FechaInicio = fechaI;
            FechaFinal = fechaF;
            FechaReserva = DateTime.Now;
            TiempoEstadia = (int)(fechaF - fechaI).TotalDays;
            IdUsuario = user.IdUsuario;
            Usuario = user;
        }
    }
}

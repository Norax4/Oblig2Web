using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Oblig2Web.Modelos
{
	public class Usuario
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IdUsuario { get; private set; }
		[ForeignKey("Huesped")]
		public int HuespedId { get; set; }
		public Huesped Huesped { get; set; }
		[Required]
		[Display(Name = "Usuario Elegido:")]
		public string? Nombre { get; set; }
		[Required]
		[EmailAddress]
		[Display(Name = "Correo Electronico:")]
		public string? CorreoElec { get; set; }
		[Required]
		[StringLength(40, MinimumLength = 8)]
		public string? Contrasenia { get; set; }
		public int? CountRes = 0;
		public List<Reserva> Reservas { get; set; }

		public Usuario()
		{

		}

		public Usuario(Huesped huesped, string nombre, string correoElec, string contrasenia)
		{
			Huesped = huesped;
			HuespedId = huesped.IdHuesped;
			Nombre = nombre + " " + huesped.Apellidos;
			CorreoElec = correoElec;
			Contrasenia = contrasenia;
			Reservas = new List<Reserva>();
		}
	}
}

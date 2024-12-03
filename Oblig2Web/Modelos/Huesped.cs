using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Oblig2Web.Modelos
{
	public class Huesped
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IdHuesped { get; private set; }
		[Required]
		[StringLength(50)]
		[Display(Name = "Nombre:")]
		public string? Nombre { get; set; }
		[Required]
		[StringLength(100)]
		[Display(Name = "Apellido(s):")]
		public string? Apellidos { get; set; }
		[Required]
		[Column(TypeName = "date")]
		[Display(Name = "Fecha de nacimiento:")]
		public DateTime FechaNacimiento { get; set; }
		[Required]
		[Display(Name = "Documento:")]
		public string? TipoDocumento { get; set; }
		[Required]
		[Display(Name = "Numero de Documento:")]
		public int NumDocumento { get; set; }
		[Required]
		[Display(Name = "Contacto:")]
		public int Telefono { get; set; }
		[Required]
		[EmailAddress]
		public string? CorreoElec { get; set; }
		public Usuario Usuario { get; set; }

		public Huesped()
		{

		}

		public Huesped(string nombre, string apellidos, DateTime fechaNac, string doc, int numdoc, int telefono, string correoElec)
		{
			Nombre = nombre;
			Apellidos = apellidos;
			FechaNacimiento = fechaNac;
			TipoDocumento = doc;
			NumDocumento = numdoc;
			Telefono = telefono;
			CorreoElec = correoElec;
		}
	}
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendExample.Models
{
	public class Beer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int BeerId { get; set; }

		public string Name { get; set; }

		public int BrandID { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Alcohol { get; set; }

		[ForeignKey("BrandID")]
		public Brand Brand { get; set; }
	}
}

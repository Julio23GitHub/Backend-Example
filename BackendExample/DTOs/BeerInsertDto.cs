namespace BackendExample.DTOs
{
	public class BeerInsertDto
	{
		public string Name { get; set; }
		public int BrandId { get; set; }
		public decimal Alcohol { get; set; } // Assuming you want to include alcohol content in the insert DTO
	}
}

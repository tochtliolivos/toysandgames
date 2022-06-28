namespace DBService.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AgeRestriction { get; set; }
        public string Company { get; set; } //TODO: This is a trap, lets try to create a relationship here.
        public decimal Price { get; set; }

        //Navigation Properties
        //public Company Company { get; set; }
    }
}

namespace GoWorkFactoryDataBase.Models
{
    public class MaterialProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }
        public int MaterialAmount { get; set; }
    }
}

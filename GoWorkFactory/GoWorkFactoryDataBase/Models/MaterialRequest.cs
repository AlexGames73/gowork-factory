namespace GoWorkFactoryDataBase.Models
{
    public class MaterialRequest
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public virtual Request Request { get; set; }
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}

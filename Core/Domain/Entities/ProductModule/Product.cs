namespace Domain.Entities.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        public decimal Price { get; set; }

        //Relation 1-M With Product Type
        public ProductType ProductType { get; set; }
        public int TypeId { get; set; } // as  a FK


        // Relation 1-M With Product Brand
        public ProductBrand ProductBrand { get; set; }
        public int BrandId { get; set; } // as  a FK
    }
}

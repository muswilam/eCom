namespace eCom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateWishlistAndWishlistItemTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishlistItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WishlistId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Wishlists", t => t.WishlistId, cascadeDelete: true)
                .Index(t => t.WishlistId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Wishlists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishlistItems", "WishlistId", "dbo.Wishlists");
            DropForeignKey("dbo.WishlistItems", "ProductId", "dbo.Products");
            DropIndex("dbo.WishlistItems", new[] { "ProductId" });
            DropIndex("dbo.WishlistItems", new[] { "WishlistId" });
            DropTable("dbo.Wishlists");
            DropTable("dbo.WishlistItems");
        }
    }
}

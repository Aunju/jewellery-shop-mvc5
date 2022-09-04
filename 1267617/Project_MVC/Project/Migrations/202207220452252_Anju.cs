namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Anju : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        CartItemId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartItemId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 120),
                        CustomerName = c.String(maxLength: 60),
                        Address = c.String(maxLength: 500),
                        CCNumber = c.String(),
                        CCExpire = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        ShippedDate = c.DateTime(storeType: "date"),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.Id })
                .ForeignKey("dbo.Items", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "Id", "dbo.Items");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.OrderItems", new[] { "Id" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.CartItems");
        }
    }
}

namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        StoreDate = c.DateTime(nullable: false, storeType: "date"),
                        PicturePath = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Available = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.EmployeeAddresses",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        Address = c.String(nullable: false, maxLength: 150),
                        PostCode = c.String(nullable: false, maxLength: 4),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeePhotoes",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeAddresses", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeePhotoes", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropIndex("dbo.EmployeePhotoes", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeAddresses", new[] { "EmployeeId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropTable("dbo.EmployeePhotoes");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeAddresses");
            DropTable("dbo.Items");
            DropTable("dbo.Categories");
        }
    }
}

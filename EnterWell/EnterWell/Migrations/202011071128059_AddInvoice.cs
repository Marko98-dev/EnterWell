namespace EnterWell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        TotalPriceTax = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CustomerName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        ProductPrice = c.Int(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        Invoice_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Invoices", t => t.Invoice_Id)
                .Index(t => t.Invoice_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Invoice_Id", "dbo.Invoices");
            DropIndex("dbo.Items", new[] { "Invoice_Id" });
            DropTable("dbo.Items");
            DropTable("dbo.Invoices");
        }
    }
}

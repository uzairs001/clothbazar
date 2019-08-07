namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageURL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageURL", c => c.String());
            DropColumn("dbo.Products", "ImgaeURL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImgaeURL", c => c.String());
            DropColumn("dbo.Products", "ImageURL");
        }
    }
}

namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageURlColumnAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImgaeURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImgaeURL");
        }
    }
}

namespace ValaisBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "IdRoom_Id", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "IdRoom_Id" });
            AddColumn("dbo.Reservations", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Reservations", "IdRoom_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "IdRoom_Id", c => c.Int());
            DropColumn("dbo.Reservations", "Price");
            CreateIndex("dbo.Reservations", "IdRoom_Id");
            AddForeignKey("dbo.Reservations", "IdRoom_Id", "dbo.Rooms", "Id");
        }
    }
}

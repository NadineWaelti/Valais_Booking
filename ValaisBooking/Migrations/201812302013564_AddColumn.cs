namespace ValaisBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Liens", "IdReservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.Liens", "IdRoom_Id", "dbo.Rooms");
            DropIndex("dbo.Liens", new[] { "IdReservation_Id" });
            DropIndex("dbo.Liens", new[] { "IdRoom_Id" });
            RenameColumn(table: "dbo.Liens", name: "IdReservation_Id", newName: "ReservationId");
            RenameColumn(table: "dbo.Liens", name: "IdRoom_Id", newName: "RoomId");
            AlterColumn("dbo.Liens", "ReservationId", c => c.Int(nullable: false));
            AlterColumn("dbo.Liens", "RoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.Liens", "ReservationId");
            CreateIndex("dbo.Liens", "RoomId");
            AddForeignKey("dbo.Liens", "ReservationId", "dbo.Reservations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Liens", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Liens", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Liens", "ReservationId", "dbo.Reservations");
            DropIndex("dbo.Liens", new[] { "RoomId" });
            DropIndex("dbo.Liens", new[] { "ReservationId" });
            AlterColumn("dbo.Liens", "RoomId", c => c.Int());
            AlterColumn("dbo.Liens", "ReservationId", c => c.Int());
            RenameColumn(table: "dbo.Liens", name: "RoomId", newName: "IdRoom_Id");
            RenameColumn(table: "dbo.Liens", name: "ReservationId", newName: "IdReservation_Id");
            CreateIndex("dbo.Liens", "IdRoom_Id");
            CreateIndex("dbo.Liens", "IdReservation_Id");
            AddForeignKey("dbo.Liens", "IdRoom_Id", "dbo.Rooms", "Id");
            AddForeignKey("dbo.Liens", "IdReservation_Id", "dbo.Reservations", "Id");
        }
    }
}

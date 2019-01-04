namespace ValaisBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Location = c.String(),
                        Category = c.Int(nullable: false),
                        HasWifi = c.Boolean(nullable: false),
                        HasParking = c.Boolean(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Liens",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    IdReservation_Id = c.Int(),
                    IdRoom_Id = c.Int(),
                    ReservationId = c.Int(),
                    RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.IdReservation_Id)
                .ForeignKey("dbo.Rooms", t => t.IdRoom_Id)
                .Index(t => t.IdReservation_Id)
                .Index(t => t.IdRoom_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Debut = c.DateTime(nullable: false),
                        Fin = c.DateTime(nullable: false),
                        NomClient = c.String(),
                        PrenomClient = c.String(),
                        IdRoom_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.IdRoom_Id)
                .Index(t => t.IdRoom_Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Description = c.String(),
                        Type = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HasTV = c.Boolean(nullable: false),
                        HasHairDryer = c.Boolean(nullable: false),
                        IdHotel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.IdHotel_Id)
                .Index(t => t.IdHotel_Id);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        IdRoom_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.IdRoom_Id)
                .Index(t => t.IdRoom_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "IdRoom_Id", "dbo.Rooms");
            DropForeignKey("dbo.Liens", "IdRoom_Id", "dbo.Rooms");
            DropForeignKey("dbo.Liens", "IdReservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.Reservations", "IdRoom_Id", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "IdHotel_Id", "dbo.Hotels");
            DropIndex("dbo.Pictures", new[] { "IdRoom_Id" });
            DropIndex("dbo.Rooms", new[] { "IdHotel_Id" });
            DropIndex("dbo.Reservations", new[] { "IdRoom_Id" });
            DropIndex("dbo.Liens", new[] { "IdRoom_Id" });
            DropIndex("dbo.Liens", new[] { "IdReservation_Id" });
            DropTable("dbo.Pictures");
            DropTable("dbo.Rooms");
            DropTable("dbo.Reservations");
            DropTable("dbo.Liens");
            DropTable("dbo.Hotels");
        }
    }
}

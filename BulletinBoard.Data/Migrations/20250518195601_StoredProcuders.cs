using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.Data.Migrations
{
    /// <inheritdoc />
    public partial class StoredProcuders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create spGetAllAnnouncements
            migrationBuilder.Sql(@"
                EXEC('
                    CREATE PROCEDURE spGetAllAnnouncements
                    AS
                    BEGIN
                        SELECT * FROM Announcements ORDER BY CreatedDate DESC;
                    END
                ')
            ");

            // Create spCreateAnnouncement
            migrationBuilder.Sql(@"
                EXEC('
                    CREATE PROCEDURE spCreateAnnouncement
                        @Title NVARCHAR(100),
                        @Description NVARCHAR(MAX),
                        @Status NVARCHAR(10),
                        @CreatedDate DATETIME2(7),
                        @Category NVARCHAR(50),
                        @SubCategory NVARCHAR(50)
                    AS
                    BEGIN
                        INSERT INTO Announcements (Title, Description, CreatedDate, Status, Category, SubCategory)
                        VALUES (@Title, @Description, @CreatedDate, @Status, @Category, @SubCategory);
                    END
                ')
            ");

            // Create spGetAnnouncementById
            migrationBuilder.Sql(@"
                EXEC('
                    CREATE PROCEDURE spGetAnnouncementById
                        @Id INT
                    AS
                    BEGIN
                        SELECT * FROM Announcements WHERE Id = @Id;
                    END
                ')
            ");

            // Create spUpdateAnnouncement
            migrationBuilder.Sql(@"
                EXEC('
                    CREATE PROCEDURE spUpdateAnnouncement
                        @Id INT,
                        @Title NVARCHAR(100),
                        @Description NVARCHAR(MAX),
                        @Status NVARCHAR(10),
                        @Category NVARCHAR(50),
                        @SubCategory NVARCHAR(50)
                    AS
                    BEGIN
                        UPDATE Announcements
                        SET Title = @Title,
                            Description = @Description,
                            Status = @Status,
                            Category = @Category,
                            SubCategory = @SubCategory
                        WHERE Id = @Id;
                    END
                ')
            ");

            // Create spDeleteAnnouncement
            migrationBuilder.Sql(@"
                EXEC('
                    CREATE PROCEDURE spDeleteAnnouncement
                        @Id INT
                    AS
                    BEGIN
                        DELETE FROM Announcements WHERE Id = @Id;
                    END
                ')
            ");
        }



        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spGetAllAnnoucements");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spCreateAnnouncement");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spGetAnnouncementById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spUpdateAnnouncement");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spDeleteAnnouncement");
        }
    }
}

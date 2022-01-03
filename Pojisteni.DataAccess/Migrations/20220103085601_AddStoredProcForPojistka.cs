using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pojisteni.DataAccess.Migrations
{
    public partial class AddStoredProcForPojistka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { //příště názvy v jednotném čísle až na první PROC.
            migrationBuilder.Sql(@"CREATE PROC usp_GetPojistky 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Pojistky 
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_GetPojistku 
                                    @PojisteniId int 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Pojistky  WHERE  (PojisteniId = @PojisteniId) 
                                    END ");

            migrationBuilder.Sql(@"CREATE PROC usp_UpdatePojistky
	                                @PojisteniId int,
	                                @Podminky varchar(1000)
                                    AS 
                                    BEGIN 
                                     UPDATE dbo.Pojistky
                                     SET  Podminky = @Podminky
                                     WHERE  PojisteniId = @PojisteniId
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_DeletePojistky
	                                @PojisteniId int
                                    AS 
                                    BEGIN 
                                     DELETE FROM dbo.Pojistky
                                     WHERE  PojisteniId = @PojisteniId
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_CreatePojistky
                                   @Podminky varchar(1000)
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.Pojistky(Podminky)
                                    VALUES (@Podminky)
                                   END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetPojistky");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetPojistky");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdatePojistky");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_DeletePojistky");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CreatePojistky");
        }
    }
}

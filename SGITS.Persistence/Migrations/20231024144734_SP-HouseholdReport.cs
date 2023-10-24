using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGITS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SPHouseholdReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var storedProcedure = @"
                CREATE PROCEDURE [dbo].[HouseholdReport] @OwnerId uniqueidentifier
                AS
                BEGIN
                	SELECT 
                		H.Address AS HouseAddress,
                		CONCAT(FM.FirstName, FM.LastName) AS OwnerName,
                		FM.ContactNumber,
                		(SELECT
                			COUNT(F.MemberAssignment)
                			FROM [dbo].[FamilyMembers] AS F
                			WHERE F.HouseId = FM.HouseId
                			AND F.MemberAssignment = 2
                		) AS NumberOfDependents
                	FROM [dbo].[FamilyMembers] FM 
                	INNER JOIN [dbo].[Houses] H ON H.Id = FM.HouseId
                	WHERE FM.Id = @OwnerId
                END";

            migrationBuilder.Sql(storedProcedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE IF EXISTS [dbo].[HouseholdReport];
                GO");
        }
    }
}

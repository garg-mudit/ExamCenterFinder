using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamCenterFinder.API.Migrations
{
    /// <inheritdoc />
    public partial class Added_CalculateDistanceInMiles_DbFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"
CREATE OR ALTER FUNCTION dbo.CalculateDistanceMiles(
    @lat1 DOUBLE PRECISION,
    @lon1 DOUBLE PRECISION,
    @lat2 DOUBLE PRECISION,
    @lon2 DOUBLE PRECISION
)
RETURNS DOUBLE PRECISION
AS
BEGIN
    DECLARE @earthRadiusMiles DOUBLE PRECISION = 3959.0;
    DECLARE @dLat DOUBLE PRECISION = RADIANS(@lat2 - @lat1);
    DECLARE @dLon DOUBLE PRECISION = RADIANS(@lon2 - @lon1);
    DECLARE @a DOUBLE PRECISION = SIN(@dLat / 2) * SIN(@dLat / 2) +
                      COS(RADIANS(@lat1)) * COS(RADIANS(@lat2)) *
                      SIN(@dLon / 2) * SIN(@dLon / 2);
    DECLARE @c DOUBLE PRECISION = 2 * ATN2(SQRT(@a), SQRT(1 - @a));
    DECLARE @distanceMiles DOUBLE PRECISION = @earthRadiusMiles * @c;
    RETURN @distanceMiles;
END
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION dbo.CalculateDistanceMiles");
        }
    }
}

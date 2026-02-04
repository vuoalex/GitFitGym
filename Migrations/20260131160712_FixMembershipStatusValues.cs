using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitFitGym.Migrations
{
    /// <inheritdoc />
    public partial class FixMembershipStatusValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Status would show up as an int before in the db = not very readable
            migrationBuilder.Sql(@"
                UPDATE memberships SET status = 'Active' WHERE status = '0';
                UPDATE memberships SET status = 'Expired' WHERE status = '1';
                UPDATE memberships SET status = 'Cancelled' WHERE status = '2';
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE memberships SET status = '0' WHERE status = 'Active';
                UPDATE memberships SET status = '1' WHERE status = 'Expired';
                UPDATE memberships SET status = '2' WHERE status = 'Cancelled';
            ");
        }
    }
}

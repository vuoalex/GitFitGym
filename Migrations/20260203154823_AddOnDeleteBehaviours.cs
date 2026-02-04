using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitFitGym.Migrations
{
    /// <inheritdoc />
    public partial class AddOnDeleteBehaviours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_members_trainers_trainer_id",
                table: "members");

            migrationBuilder.DropForeignKey(
                name: "fk_memberships_membership_plans_membership_plan_id",
                table: "memberships");

            migrationBuilder.AddForeignKey(
                name: "fk_members_trainers_trainer_id",
                table: "members",
                column: "trainer_id",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_memberships_membership_plans_membership_plan_id",
                table: "memberships",
                column: "membership_plan_id",
                principalTable: "membership_plans",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_members_trainers_trainer_id",
                table: "members");

            migrationBuilder.DropForeignKey(
                name: "fk_memberships_membership_plans_membership_plan_id",
                table: "memberships");

            migrationBuilder.AddForeignKey(
                name: "fk_members_trainers_trainer_id",
                table: "members",
                column: "trainer_id",
                principalTable: "trainers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_memberships_membership_plans_membership_plan_id",
                table: "memberships",
                column: "membership_plan_id",
                principalTable: "membership_plans",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

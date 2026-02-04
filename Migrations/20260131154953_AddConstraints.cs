using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitFitGym.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_workout_exercises_workout_id",
                table: "workout_exercises");

            migrationBuilder.AlterColumn<decimal>(
                name: "salary",
                table: "trainers",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<DateTime>(
                name: "joined_at",
                table: "trainers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "timezone('utc', now())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "memberships",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                table: "memberships",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "membership_plans",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<DateTime>(
                name: "joined_at",
                table: "members",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "timezone('utc', now())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "ix_workouts_name",
                table: "workouts",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_workout_exercises_workout_id_exercise_id",
                table: "workout_exercises",
                columns: new[] { "workout_id", "exercise_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trainers_email",
                table: "trainers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_membership_plans_name",
                table: "membership_plans",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_members_email",
                table: "members",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_exercises_name",
                table: "exercises",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_workouts_name",
                table: "workouts");

            migrationBuilder.DropIndex(
                name: "ix_workout_exercises_workout_id_exercise_id",
                table: "workout_exercises");

            migrationBuilder.DropIndex(
                name: "ix_trainers_email",
                table: "trainers");

            migrationBuilder.DropIndex(
                name: "ix_membership_plans_name",
                table: "membership_plans");

            migrationBuilder.DropIndex(
                name: "ix_members_email",
                table: "members");

            migrationBuilder.DropIndex(
                name: "ix_exercises_name",
                table: "exercises");

            migrationBuilder.AlterColumn<decimal>(
                name: "salary",
                table: "trainers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "joined_at",
                table: "trainers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "timezone('utc', now())");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "memberships",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                table: "memberships",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "membership_plans",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "joined_at",
                table: "members",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "timezone('utc', now())");

            migrationBuilder.CreateIndex(
                name: "ix_workout_exercises_workout_id",
                table: "workout_exercises",
                column: "workout_id");
        }
    }
}

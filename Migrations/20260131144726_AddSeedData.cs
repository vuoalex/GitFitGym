using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitFitGym.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO membership_plans (name, duration_days, price) VALUES
                ('Monthly', 30, 299),
                ('Quarterly', 90, 799),
                ('Yearly', 365, 2499);

                INSERT INTO trainers (first_name, last_name, email, salary, joined_at) VALUES
                ('Erik', 'Svensson', 'erik@gitfitgym.se', 35000, '2024-01-15 08:00:00+00'),
                ('Anna', 'Lindberg', 'anna@gitfitgym.se', 38000, '2023-06-01 08:00:00+00');

                INSERT INTO exercises (name) VALUES
                ('Bench Press'),
                ('Squat'),
                ('Deadlift'),
                ('Shoulder Press'),
                ('Bicep Curl'),
                ('Tricep Pushdown'),
                ('Leg Press'),
                ('Lat Pulldown'),
                ('Calf Raise');

                INSERT INTO workouts (name) VALUES
                ('Push Day'),
                ('Pull Day'),
                ('Leg Day');

                INSERT INTO members (first_name, last_name, email, joined_at, trainer_id) VALUES
                ('Johan', 'Karlsson', 'johan@email.com', '2024-06-01 10:00:00+00', 1),
                ('Maria', 'Andersson', 'maria@email.com', '2024-08-15 10:00:00+00', 1),
                ('Oscar', 'Berg', 'oscar@email.com', '2025-01-10 10:00:00+00', NULL);

                INSERT INTO memberships (start_date, end_date, status, member_id, membership_plan_id) VALUES
                ('2024-06-01 00:00:00+00', '2025-06-01 00:00:00+00', 0, 1, 3),
                ('2024-08-15 00:00:00+00', '2024-11-15 00:00:00+00', 1, 2, 2),
                ('2025-01-10 00:00:00+00', '2025-02-10 00:00:00+00', 0, 3, 1);

                INSERT INTO workout_exercises (sets, reps, workout_id, exercise_id) VALUES
                (4, 8, 1, 1),
                (3, 10, 1, 4),
                (3, 12, 1, 6),
                (4, 8, 2, 3),
                (3, 10, 2, 8),
                (3, 12, 2, 5),
                (4, 8, 3, 2),
                (3, 10, 3, 7),
                (4, 15, 3, 9);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                TRUNCATE TABLE workout_exercises, memberships, members, workouts, exercises, trainers, membership_plans RESTART IDENTITY CASCADE;
            ");
        }
    }
}

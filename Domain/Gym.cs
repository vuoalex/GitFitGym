using GitFitGym.Data.Repositories;
using GitFitGym.Domain.Models;

namespace GitFitGym.Domain;

public class Gym
{
    private readonly MemberRepository _memberRepository = new();
    private readonly TrainerRepository _trainerRepository = new();
    private readonly MembershipPlanRepository _membershipPlanRepository = new();
    private readonly MembershipRepository _membershipRepository = new();
    private readonly WorkoutRepository _workoutRepository = new();
    private readonly ExerciseRepository _exerciseRepository = new();
    private readonly WorkoutExerciseRepository _workoutExerciseRepository = new();

    #region Members

    public async Task<List<Member>> GetAllMembersAsync() =>
        await _memberRepository.GetAllAsync();

    public async Task<Member?> GetMemberByIdAsync(int id) =>
        await _memberRepository.GetByIdAsync(id);

    public async Task<Member>
        CreateMemberAsync(string firstName, string lastName, string email, int? trainerId = null) =>
        await _memberRepository.CreateAsync(firstName, lastName, email, trainerId);

    public async Task<Member> UpdateMemberAsync(Member member) =>
        await _memberRepository.UpdateAsync(member);

    public async Task DeleteMemberAsync(int id) =>
        await _memberRepository.DeleteAsync(id);

    #endregion

    #region Trainers

    public async Task<List<Trainer>> GetAllTrainersAsync() =>
        await _trainerRepository.GetAllAsync();

    public async Task<Trainer?> GetTrainerByIdAsync(int id) =>
        await _trainerRepository.GetByIdAsync(id);

    public async Task<Trainer> CreateTrainerAsync(string firstName, string lastName, string email, decimal salary) =>
        await _trainerRepository.CreateAsync(firstName, lastName, email, salary);

    public async Task<Trainer> UpdateTrainerAsync(Trainer trainer) =>
        await _trainerRepository.UpdateAsync(trainer);

    public async Task DeleteTrainerAsync(int id) =>
        await _trainerRepository.DeleteAsync(id);

    #endregion

    #region Membership Plans

    public async Task<List<MembershipPlan>> GetAllMembershipPlansAsync() =>
        await _membershipPlanRepository.GetAllAsync();

    public async Task<MembershipPlan?> GetMembershipPlanByIdAsync(int id) =>
        await _membershipPlanRepository.GetByIdAsync(id);

    public async Task<MembershipPlan> CreateMembershipPlanAsync(string name, int durationDays, decimal price) =>
        await _membershipPlanRepository.CreateAsync(name, durationDays, price);

    public async Task DeleteMembershipPlanAsync(int id) =>
        await _membershipPlanRepository.DeleteAsync(id);

    #endregion

    #region Memberships

    public async Task<List<Membership>> GetAllMembershipsAsync() =>
        await _membershipRepository.GetAllAsync();

    public async Task<Membership?> GetMembershipByIdAsync(int id) =>
        await _membershipRepository.GetByIdAsync(id);

    public async Task<Membership> CreateMembershipAsync(int memberId, int membershipPlanId) =>
        await _membershipRepository.CreateAsync(memberId, membershipPlanId);

    public async Task<Membership> UpdateMembershipAsync(Membership membership) =>
        await _membershipRepository.UpdateAsync(membership);

    #endregion

    #region Workouts

    public async Task<List<Workout>> GetAllWorkoutsAsync() =>
        await _workoutRepository.GetAllAsync();

    public async Task<Workout?> GetWorkoutByIdAsync(int id) =>
        await _workoutRepository.GetByIdAsync(id);

    public async Task<Workout> CreateWorkoutAsync(string name) =>
        await _workoutRepository.CreateAsync(name);

    public async Task DeleteWorkoutAsync(int id) =>
        await _workoutRepository.DeleteAsync(id);

    #endregion

    #region Exercises

    public async Task<List<Exercise>> GetAllExercisesAsync() =>
        await _exerciseRepository.GetAllAsync();

    public async Task<Exercise?> GetExerciseByIdAsync(int id) =>
        await _exerciseRepository.GetByIdAsync(id);

    public async Task<Exercise> CreateExerciseAsync(string name) =>
        await _exerciseRepository.CreateAsync(name);

    public async Task DeleteExerciseAsync(int id) =>
        await _exerciseRepository.DeleteAsync(id);

    #endregion

    #region Workout Exercises

    public async Task<List<WorkoutExercise>> GetAllWorkoutExercisesAsync() =>
        await _workoutExerciseRepository.GetAllAsync();

    public async Task<WorkoutExercise?> GetWorkoutExerciseByIdAsync(int id) =>
        await _workoutExerciseRepository.GetByIdAsync(id);

    public async Task<WorkoutExercise> AddExerciseToWorkoutAsync(int workoutId, int exerciseId, int sets, int reps) =>
        await _workoutExerciseRepository.CreateAsync(workoutId, exerciseId, sets, reps);

    public async Task RemoveExerciseFromWorkoutAsync(int id) =>
        await _workoutExerciseRepository.DeleteAsync(id);

    #endregion
}
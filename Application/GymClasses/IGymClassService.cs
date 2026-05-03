using Application.GymClasses.Responses;

namespace Application.GymClasses;

public interface IGymClassService
{
    Task<IEnumerable<GymClassResponse>> GetAllAsync();
    Task DeleteAsync(int id);
    Task CreateAsync(GymClassResponse model);
}
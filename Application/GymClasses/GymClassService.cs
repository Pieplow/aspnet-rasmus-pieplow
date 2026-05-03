using Application.GymClasses.Responses;
using Domain.Abstractions.Repositories; 

namespace Application.GymClasses;

public class GymClassService(IGymClassRepository gymClassRepository) : IGymClassService
{
    public async Task<IEnumerable<GymClassResponse>> GetAllAsync()
    {
        var classes = await gymClassRepository.GetAllAsync();

        return classes.Select(c => new GymClassResponse(
            c.Id,
            c.Name,
            c.Trainer,
            c.StartTime,
            c.MaxCapacity,
            c.CurrentBookings));
    }

    public async Task DeleteAsync(int id)
    {
       
        var gymClass = await gymClassRepository.GetByIdAsync(id);

        if (gymClass != null)
        {
            
            await gymClassRepository.RemoveAsync(gymClass);
        }
    }
    }
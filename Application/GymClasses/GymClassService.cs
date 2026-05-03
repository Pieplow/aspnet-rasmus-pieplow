using Application.GymClasses.Responses;
using Domain.Abstractions.Repositories;
using Domain.Aggregates.GymClasses;
using Domain.Abstractions;

namespace Application.GymClasses;

public class GymClassService(
    IGymClassRepository gymClassRepository,
    IUnitOfWork unitOfWork) : IGymClassService
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

    public async Task CreateAsync(GymClassResponse model)
    {
       
        var gymClass = new Domain.Aggregates.GymClasses.GymClass(
            model.Name,
            model.Trainer,
            model.StartTime,
            model.MaxCapacity
        );

        await gymClassRepository.AddAsync(gymClass);

        await unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var gymClass = await gymClassRepository.GetByIdAsync(id);

        if (gymClass != null)
        {
            await gymClassRepository.RemoveAsync(gymClass);

            
            await unitOfWork.CompleteAsync();
        }
    }
}
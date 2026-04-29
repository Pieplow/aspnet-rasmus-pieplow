using Application.Memberships.Commands;
using Application.Memberships.Responses;
using Domain.Abstractions.Repositories;
using Domain.Aggregates.Memberships;

namespace Application.Memberships;

public sealed class MembershipService(IMembershipRepository repo) : IMembershipService
{
    // READ
    public async Task<IReadOnlyList<MembershipResponse>> GetMembershipsAsync(CancellationToken ct = default)
    {
        var memberships = await repo.GetAllAsync(ct);

        return memberships.Select(m => new MembershipResponse(
            m.Id, 
            m.Title,
            m.Description,
            m.Benefits,
            m.Price,
            m.MonthlyClasses)).ToList();
    }

    public async Task<MembershipResponse?> GetByUserIdAsync(int userId, CancellationToken ct = default)
    {
        var memberships = await repo.GetAllAsync(ct);

        var membership = memberships.FirstOrDefault(m => m.UserId == userId);

        if (membership == null)
            return null;

        return new MembershipResponse(
            membership.Id,
            membership.Title,
            membership.Description,
            membership.Benefits,
            membership.Price,
            membership.MonthlyClasses
        );
    }



    // CREATE
    public async Task CreateMembershipAsync(CreateMembershipCommand command, CancellationToken ct = default)
    {
        // VIKTIGT: Vi skickar nu med command.UserId (som ska vara int)
        var membership = Membership.Create(
            command.UserId,
            command.Title,
            command.Description,
            command.Benefits,
            command.Price,
            command.MonthlyClasses
        );

        await repo.AddAsync(membership, ct);
    }

    // UPDATE
    public async Task UpdateMembershipAsync(UpdateMembershipCommand command, CancellationToken ct = default)
    {
        // Eftersom command.Id nu bör vara en int i DTO:n, slipper vi parse om vi ändrar i Command-klassen
        var membership = await repo.GetByIdAsync(command.Id, ct);

        if (membership is null)
            throw new KeyNotFoundException($"Membership with ID {command.Id} was not found.");

        membership.Update(
            command.Title,
            command.Description,
            command.Benefits,
            command.Price,
            command.MonthlyClasses
        );

        await repo.UpdateAsync(membership, ct);
    }

    // DELETE
    public async Task DeleteMembershipAsync(DeleteMembershipCommand command, CancellationToken ct = default)
    {
        var membership = await repo.GetByIdAsync(command.Id, ct);

        if (membership is not null)
        {
            await repo.RemoveAsync(membership, ct);
        }
    }
}
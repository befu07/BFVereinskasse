using BFVereinskasse.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFVereinskasse.Services;

public class MemberService
{
    private readonly BfvereinskasseContext _ctx;
    public MemberService(BfvereinskasseContext ctx)
    {
        _ctx = ctx;
    }
    public async Task<List<Mitglied>> GetMembers()
    {
        return await _ctx.Mitglieds.ToListAsync();
    }
    internal async Task<bool> IsUserActiveAsync(int mitgliedId)
    {
        var user = await _ctx.Mitglieds.FindAsync(mitgliedId);
        return user.IsActive;
    }

    internal async Task<int> UpdateUserStatusAsync(int id, bool v)
    {
        var user = await _ctx.Mitglieds.FindAsync(id);
        user.IsActive = v;
        return await _ctx.SaveChangesAsync();
    }
}

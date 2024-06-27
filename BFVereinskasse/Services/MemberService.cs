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

    internal async Task<int> CreateMember(Mitglied mitglied)
    {
        if (_ctx.Mitglieds.Any(o => o.Nachname == mitglied.Nachname & o.Vorname == mitglied.Vorname))
            return -1;
        _ctx.Mitglieds.Add(mitglied);
        return await _ctx.SaveChangesAsync();
    }

    internal async Task<Mitglied> GetMember(int mitgliedId)
    {
        var user = await _ctx.Mitglieds.FindAsync(mitgliedId);
        return user;
    }

    internal async Task<bool> IsUserActiveAsync(int mitgliedId)
    {
        var user = await _ctx.Mitglieds.FindAsync(mitgliedId);
        return user.IsActive;
    }

    internal async Task<int> SetImagePath(int memberId)
    {
        var user = await _ctx.Mitglieds.FindAsync(memberId);
        user.Bild = "userImage" + memberId +".png";
        return await _ctx.SaveChangesAsync();
    }

    internal async Task<int> UpdateUserStatusAsync(int id, bool v)
    {
        var user = await _ctx.Mitglieds.FindAsync(id);
        user.IsActive = v;
        return await _ctx.SaveChangesAsync();
    }
}

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
}

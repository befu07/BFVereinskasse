using BFVereinskasse.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace BFVereinskasse.Services;

public class PaymentService
{
    private readonly BfvereinskasseContext _ctx;
    public PaymentService(BfvereinskasseContext ctx)
    {
        _ctx = ctx;
    }
    public async Task<List<Zahlung>> GetZahlungen()
    {
        return await _ctx.Zahlungs.Include(o=>o.Mitglied).OrderByDescending(o=>o.Datum).ToListAsync();
    }

}

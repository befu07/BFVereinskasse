﻿using BFVereinskasse.Data;
using BFVereinskasse.Models;
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

    internal async Task<int> CreatePayment(Zahlung payment)
    {
        _ctx.Zahlungs.Add(payment);
        return await _ctx.SaveChangesAsync();
    }

    internal async Task<List<Zahlung>> GetFilteredPayments(IndexVM form)
    {
        var payments = await GetZahlungen();
        if (form.MemberId.HasValue)
        {
            //payments = payments.Where(payments, form.MemberId)
            payments = payments.Where(FilterMember(form.MemberId.Value)).ToList();
        }
        if (form.InOutFilterType.HasValue)
        {
            if(form.InOutFilterType.Value == 1)
            payments = payments.Where(FilterPositive).ToList();
            if(form.InOutFilterType.Value == 2)
            payments = payments.Where(FilterNegative).ToList();
        }
        if (!String.IsNullOrEmpty(form.Query))
        {
            payments = payments.Where(FilterHasDescription).Where(FilterQuery(form.Query)).ToList();
        }
        if (form.Limit.HasValue)
        {
            payments = payments.Take(form.Limit.Value).ToList();
        }
        return payments;
    }

    private static Func<Zahlung, bool> FilterPositive => (t) => t.Betrag >= 0;
    private static Func<Zahlung, bool> FilterNegative => (t) => t.Betrag < 0;
    private static Func<Zahlung, bool> FilterMember(int memberId) => (t) => t.MitgliedId == memberId;
    private static Func<Zahlung, bool> FilterQuery(string query) => (t) => t.Beschreibung.Contains(query);
    private static Func<Zahlung, bool> FilterHasDescription => (t) => t.Beschreibung != null;
}

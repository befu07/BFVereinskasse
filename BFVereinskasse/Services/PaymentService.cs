using BFVereinskasse.Data;
using BFVereinskasse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Text.Json.Serialization;
using System.Text.Json;

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
        return await _ctx.Zahlungs.Include(o => o.Mitglied).OrderByDescending(o => o.Datum).ToListAsync();
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
        if (form.InOutFilter.HasValue)
        {
            if (form.InOutFilter.Value == IndexVM.InOutFilterType.Eingänge)
                payments = payments.Where(FilterPositive).ToList();
            if (form.InOutFilter.Value == IndexVM.InOutFilterType.Ausgänge)
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

    internal async Task<int> DeletePaymentAsync(int id)
    {
        var payment = await _ctx.Zahlungs.FindAsync(id);
        if (payment is null)
        {
            return -1;
        }
        _ctx.Zahlungs.Remove(payment);
        return await _ctx.SaveChangesAsync();
    }

    internal async Task<Zahlung> GetPaymentAsync(int id)
    {
        return await _ctx.Zahlungs.FindAsync(id);
    }

    internal async Task<int> UpdatePaymentAsync(Zahlung updatedPayment)
    {
        var payment = await _ctx.Zahlungs.FindAsync(updatedPayment.Id);
        if (payment is null)
        {
            return -1;
        }
        payment.Datum = updatedPayment.Datum;
        payment.Beschreibung = updatedPayment.Beschreibung;
        payment.Betrag = updatedPayment.Betrag;
        payment.MitgliedId = updatedPayment.MitgliedId;
        return await _ctx.SaveChangesAsync();
    }

    internal async Task<List<Zahlung>> GetFilteredZahlungenAsync(int memberId, DateOnly startDate, DateOnly endDate)
    {
        var payments = _ctx.Zahlungs.AsQueryable();
        if (memberId != default(int))
        {
            payments = payments.Where(o => o.MitgliedId == memberId);
        }
        if (startDate != default(DateOnly))
        {
            var startDateDT = startDate.ToDateTime(new TimeOnly());
            payments = payments.Where(o => o.Datum >= startDateDT);
        }
        if (endDate != default(DateOnly))
        {
            var endDateDT = endDate.ToDateTime(new TimeOnly());
            payments = payments.Where(o => o.Datum <= endDateDT);
        }
        //payments = payments.OrderByDescending(o => o.Betrag); // get ned mit SQLite 
        return await payments.ToListAsync();
    }

    //internal async Task<string> ExportJsonAsync()
    //{
    //    var members = await _ctx.Mitglieds.ToListAsync();
    //    var payments = await _ctx.Zahlungs.ToListAsync();
    //    var database = new Database { members = members, payments = payments };
    //    return 
    //    System.Text.Json.JsonSerializer.Serialize(database, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles });
    //}
    internal async Task<int> ImportJsonAsync()
    {
        var json = System.IO.File.ReadAllText(@"database.json");
        var jsonobject = System.Text.Json.JsonSerializer.Deserialize<Database>(json);
        //_ctx.Mitglieds.AddRange(jsonobject.members);
        //await _ctx.SaveChangesAsync();
        _ctx.Zahlungs.AddRange(jsonobject.payments);
        await _ctx.SaveChangesAsync();
        return 1;
    }
    public struct Database
    {
        public List<Mitglied> members { get; set; }
        public List<Zahlung> payments { get; set; }
    }
}

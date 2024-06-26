using BFVereinskasse.Data;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace BFVereinskasse.Models;

public class IndexVM
{
    public List<Mitglied> Members { get; set; }
    public List<Zahlung> Payments { get; set; }
    public string? Query { get; set; }
    public int? MemberId { get; set; }
    public int? Limit { get; set; }
    public InOutFilterType? InOutFilter { get; set; }
    public enum InOutFilterType
    {
        Eingänge = 1, 
        Ausgänge = 2
    }
    public int[] LimitFilters = { 15, 30 };

}

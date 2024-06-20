using BFVereinskasse.Data;
using Microsoft.AspNetCore.Mvc;

namespace BFVereinskasse.Models;

public class IndexVM
{
    public List<Mitglied> Members { get; set; }
    public List<Zahlung> Payments { get; set; }
    public string? Query { get; set; }
    public int? Limit { get; set; }
    public int? MemberId { get; set; }
    public int? InOutFilterType { get; set; }

}

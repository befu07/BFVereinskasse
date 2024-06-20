using BFVereinskasse.Data;
using Microsoft.AspNetCore.Mvc;

namespace BFVereinskasse.Models;

public class IndexVM
{
    public List<Mitglied> Members { get; set; }
}

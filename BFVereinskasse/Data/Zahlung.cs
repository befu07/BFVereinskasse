using System;
using System.Collections.Generic;

namespace BFVereinskasse.Data;

public partial class Zahlung
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public decimal Betrag { get; set; }

    public DateTime Datum { get; set; }

    public string? Beschreibung { get; set; }

    public virtual Mitglied Member { get; set; } = null!;
}

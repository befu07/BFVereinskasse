using System;
using System.Collections.Generic;

namespace BFVereinskasse.Data;

public partial class Zahlung
{
    public int Id { get; set; }

    public int MitgliedId { get; set; }

    public decimal Betrag { get; set; }

    public DateTime Datum { get; set; }

    public string? Beschreibung { get; set; }

    public virtual Mitglied Mitglied { get; set; } = null!;
}

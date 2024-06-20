using System;
using System.Collections.Generic;

namespace BFVereinskasse.Data;

public partial class Zahlungen
{
    public int Id { get; set; }

    public int VereinsmitgliedId { get; set; }

    public decimal Betrag { get; set; }

    public DateTime Datum { get; set; }

    public string? Beschreibung { get; set; }
}

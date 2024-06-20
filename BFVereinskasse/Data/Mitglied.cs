using System;
using System.Collections.Generic;

namespace BFVereinskasse.Data;

public partial class Mitglied
{
    public int Id { get; set; }

    public string Vorname { get; set; } = null!;

    public string Nachname { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? Bild { get; set; }

    public virtual ICollection<Zahlung> Zahlungs { get; set; } = new List<Zahlung>();
}

using System;
using System.Collections.Generic;

namespace BFVereinskasse.Data;

public partial class Vereinsmitglied
{
    public int Id { get; set; }

    public string? Vorname { get; set; }

    public string? Nachname { get; set; }

    public bool? IsActive { get; set; }

    public byte[]? Bild { get; set; }
}

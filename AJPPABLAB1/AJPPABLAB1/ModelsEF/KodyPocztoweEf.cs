using System;
using System.Collections.Generic;

namespace AJPPABLAB1.ModelsEF;

public partial class KodyPocztoweEf
{
    public int Id { get; set; }

    public string KodPocztowy { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public string Miejscowosc { get; set; } = null!;

    public string Wojewodztwo { get; set; } = null!;

    public string Powiat { get; set; } = null!;
}

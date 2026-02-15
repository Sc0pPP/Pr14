using System;
using System.Collections.Generic;

namespace pr14Avalonia.Models;

public partial class SessionSeat
{
    public int Id { get; set; }

    public int SessionsId { get; set; }

    public int SeatsId { get; set; }

    public bool Status { get; set; }

    public virtual Seat Seats { get; set; } = null!;

    public virtual Session Sessions { get; set; } = null!;
}

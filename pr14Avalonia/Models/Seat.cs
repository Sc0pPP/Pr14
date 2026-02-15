using System;
using System.Collections.Generic;

namespace pr14Avalonia.Models;

public partial class Seat
{
    public int Id { get; set; }

    public int SessionId { get; set; }

    public int? Place { get; set; }

    public virtual ICollection<SessionSeat> SessionSeats { get; set; } = new List<SessionSeat>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

using System;
using System.Collections.Generic;

namespace pr14Avalonia.Models;

public partial class Session
{
    public int Id { get; set; }

    public int MoviesId { get; set; }

    public int HallId { get; set; }

    public DateTime StartDateTime { get; set; }

    public decimal BaseTicketPrice { get; set; }

    public virtual Hall Hall { get; set; } = null!;

    public virtual Movie Movies { get; set; } = null!;

    public virtual ICollection<SessionSeat> SessionSeats { get; set; } = new List<SessionSeat>();
}

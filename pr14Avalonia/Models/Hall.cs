using System;
using System.Collections.Generic;

namespace pr14Avalonia.Models;

public partial class Hall
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double HallRating { get; set; }

    public int RowsCount { get; set; }

    public int SeatPerRow { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}

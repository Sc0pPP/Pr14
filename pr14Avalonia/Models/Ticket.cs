using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace pr14Avalonia.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SeatId { get; set; }

    public DateTime PerchaseDateTime { get; set; }

    public decimal FinalPrice { get; set; }

    public string Status { get; set; } = null!;

    public int? Place { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual Seat Seat { get; set; } = null!;
    [NotMapped]
    public string MoviesName{get;set;}
}

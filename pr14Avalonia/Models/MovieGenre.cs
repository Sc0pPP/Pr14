using System;
using System.Collections.Generic;

namespace pr14Avalonia.Models;

public partial class MovieGenre
{
    public int MovieId { get; set; }

    public int Genrel { get; set; }

    public virtual Genre GenrelNavigation { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}

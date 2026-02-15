using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Avalonia.Media.Imaging;

namespace pr14Avalonia.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string MovieName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Rating { get; set; } = null!;

    public int AgeRating { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public string Url { get; set; } = null!;
    [NotMapped]
    public Bitmap Image { get; set; }
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
 
}

using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateWebApplication.Models;

public partial class Type
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Назва")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Realty> Realties { get; set; } = new List<Realty>();
}

using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateWebApplication.Models;

public partial class Owner
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Ім'я")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Прізвище")]
    public string Surname { get; set; } = null!;

    public virtual ICollection<RealtyOwner> RealtyOwners { get; set; } = new List<RealtyOwner>();
}

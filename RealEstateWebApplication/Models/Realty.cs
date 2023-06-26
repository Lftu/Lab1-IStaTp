using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateWebApplication.Models;

public partial class Realty
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    public int TypeId { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Площа")]
    public int Area { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Поверх")]
    public int FloorNumber { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Кількість кімнат")]
    public int RoomsNumber { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Ціна")]
    public long Price { get; set; }

    public virtual ICollection<RealtyOwner> RealtyOwners { get; set; } = new List<RealtyOwner>();

    [Display(Name = "Тип")]
    public virtual Type Type { get; set; } = null!;
}

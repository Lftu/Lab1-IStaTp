using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace RealEstateWebApplication.Models;

public partial class RealtyOwner
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    public int RealtyId { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    public int OwnerId { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Початок власності")]
    [BindProperty, DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Кінець власності")]
    [BindProperty, DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [Display(Name = "Власник")]
    public virtual Owner Owner { get; set; } = null!;

    [Display(Name = "Нерухомість")]
    public virtual Realty Realty { get; set; } = null!;
}

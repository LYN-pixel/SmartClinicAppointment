using System;
using System.Collections.Generic;

namespace SmartClinicAppointment.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? IconOrImagePath { get; set; }

    public int? DisplayOrder { get; set; }
}

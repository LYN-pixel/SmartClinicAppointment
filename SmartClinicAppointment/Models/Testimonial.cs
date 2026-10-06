using System;
using System.Collections.Generic;

namespace SmartClinicAppointment.Models;

public partial class Testimonial
{
    public int Id { get; set; }

    public string? PatientName { get; set; }

    public string? Message { get; set; }

    public int? Rating { get; set; }

    public string? ImagePath { get; set; }

    public bool IsVisible { get; set; }

    public DateTime CreatedAt { get; set; }
}

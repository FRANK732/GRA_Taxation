using System.ComponentModel.DataAnnotations;

namespace GRA_Taxation.Models;

public class TaxInput
{
    [Required]
    public double DesiredNet { get; set; }
    [Required]
    public double Allowances { get; set; }
}
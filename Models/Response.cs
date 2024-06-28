namespace GRA_Taxation.Infrastructure;

public class Response
{
    public double GrossSalary { get; set; }
    public double BasicSalary { get; set; }
    public double TotalPayeTax  { get; set; }
    public double EmployeePensionContribution { get; set; }
    public double EmployerPensionContribution { get; set; }
    public double NETSalary { get; set; }
}

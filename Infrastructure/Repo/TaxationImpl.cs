using GRA_Taxation.Models;

namespace GRA_Taxation.Infrastructure;

public class TaxationImpl : Taxation
{
    
    public  Response Calculate(TaxInput input)
        {
            // Pension rates
            const double tier1EmployerRate = 0.13;
            const double tier2EmployeeRate = 0.055;
            const double tier3EmployeeRate = 0.05;
            const double tier3EmployerRate = 0.05;

            // Total pension contributions
            var employeePensionContribution = (tier2EmployeeRate + tier3EmployeeRate) * input.DesiredNet;
            var employerPensionContribution = (tier1EmployerRate + tier3EmployerRate) * input.DesiredNet;

            // Taxable income
            var taxableIncome = input.DesiredNet - employeePensionContribution + input.Allowances;

            // PAYE tax
            var payeTax = CalculatePayeTax(taxableIncome);

            // Net salary
            var netSalary = input.DesiredNet - payeTax - employeePensionContribution;

            return new Response
            {
                GrossSalary = Math.Round(input.DesiredNet,2),
                BasicSalary = Math.Round(input.DesiredNet - input.Allowances,2),
                TotalPayeTax = Math.Round(payeTax,2),
                EmployeePensionContribution = Math.Round(employeePensionContribution,2),
                EmployerPensionContribution = Math.Round(employerPensionContribution, 2),
                NETSalary =Math.Round(netSalary)
            };
        }

        private static double CalculatePayeTax(double taxableIncome)
        {
            var tax = 0.0;
            foreach (var (limit, rate) in TaxBrackets)
            {
                if (taxableIncome > limit)
                {
                    tax += limit * rate;
                    taxableIncome -= limit;
                }
                else
                {
                    tax += taxableIncome * rate;
                    break;
                }
            }
            return tax;
        }
    
    private static readonly List<(double Limit, double Rate)> TaxBrackets = new List<(double, double)>
    {
        (490, 0.0),
        (600, 0.05), 
        (730, 0.10), 
        (3896.67, 0.175), 
        (19896.67, 0.25),
        (50416.67, 0.30),  
        (double.MaxValue, 0.35) 
    };

}
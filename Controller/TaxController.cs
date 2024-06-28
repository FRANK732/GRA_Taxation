using GRA_Taxation.Infrastructure;
using GRA_Taxation.Models;
using Microsoft.AspNetCore.Mvc;

namespace GRA_Taxation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TaxController
{
    private readonly Taxation _taxation;

    public TaxController( Taxation taxation)
    {
        _taxation = taxation;
    }
    /// <summary>
    /// Calculates the net salary details based on the desired net salary and allowances.
    /// </summary>
    /// <param name="input">The desired net salary and allowances.</param>
    /// <returns>The gross salary, basic salary, total PAYE tax, employee pension contribution, and employer pension contribution.</returns>
    [HttpPost(Name = "CalculateNetSalary")]
    public IResult Calculation([FromBody] TaxInput input)
    {
        var response = _taxation.Calculate(input);

        return response != null ? Results.Ok(response) : Results.BadRequest("Unable to calculate");
    }
    
    
    /// <summary>
    /// Calculates the net salary details for multiple desired net salaries and allowances.
    /// </summary>
    /// <param name="inputs">The list of desired net salaries and allowances.</param>
    /// <returns>A list of calculations with gross salary, basic salary, total PAYE tax, employee pension contribution, and employer pension contribution for each input.</returns>
    [HttpPost(Name = "CalculateMultipleNetSalaries")]
    public IResult CalculateMultiple([FromBody] List<TaxInput> inputs)
    {

        var responses = new List<Response>();

        foreach (var input in inputs)
        {
            var response = _taxation.Calculate(input);
            if (response != null)
            {
                responses.Add(response);
            }
            else
            {
                return Results.BadRequest($"Unable to calculate for input: {input}");
            }
        }

        return Results.Ok(responses);
    }
}
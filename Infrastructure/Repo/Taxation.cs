using GRA_Taxation.Models;

namespace GRA_Taxation.Infrastructure;

public interface Taxation
{
    Response Calculate(TaxInput input); 
}
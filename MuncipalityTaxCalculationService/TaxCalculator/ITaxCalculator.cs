using System;

namespace MuncipalityTaxCalculationService.TaxCalculator
{
  public interface ITaxCalculator
  {
    /// <summary>
    /// Calculates municipality taxes for the specified date.
    /// </summary>
    /// <param name="municipality">Municipality name.</param>
    /// <param name="date">A date for which to calculate taxes.</param>
    /// <returns>Calculated taxes or zero if no data for the given municipality exists.</returns>
    decimal CalculateTax(string municipality, DateTime date);
  }
}

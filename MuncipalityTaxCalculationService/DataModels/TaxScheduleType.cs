namespace MuncipalityTaxCalculationService.DataModels
{
  /// <summary>
  /// These entities defines how municipality tax is defined in time.
  /// <para>
  /// The precedence of the taxes should be daily > weekly > monthly > yearly in order to return
  /// the tax scheduled on a specific date.
  /// </para>
  /// </summary>
  public enum TaxScheduleType
  {
    /// <summary>
    /// Yearly municipality tax.
    /// </summary>
    Yearly = 1,

    /// <summary>
    /// Monthly municipality tax.
    /// </summary>
    Monthly = 2,

    /// <summary>
    /// Weekly municipality tax.
    /// </summary>
    Weekly = 3,

    /// <summary>
    /// Daily municipality tax.
    /// </summary>
    Daily = 4
  }
}

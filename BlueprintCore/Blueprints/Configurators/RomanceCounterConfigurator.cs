using BlueprintCore.Utils;
using Kingmaker.Blueprints;

namespace BlueprintCore.Blueprints.Configurators
{
  /// <summary>
  /// Configurator for <see cref="BlueprintRomanceCounter"/>.
  /// </summary>
  /// <inheritdoc/>
  [Configures(typeof(BlueprintRomanceCounter))]
  public class RomanceCounterConfigurator : BaseBlueprintConfigurator<BlueprintRomanceCounter, RomanceCounterConfigurator>
  {
    private RomanceCounterConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static RomanceCounterConfigurator For(string name)
    {
      return new RomanceCounterConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static RomanceCounterConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintRomanceCounter>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static RomanceCounterConfigurator For(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintRomanceCounter>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="BlueprintRomanceCounter.m_CounterFlag"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="counterFlag"><see cref="BlueprintUnlockableFlag"/></param>
    [Generated]
    public RomanceCounterConfigurator SetCounterFlag(string counterFlag)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.m_CounterFlag = BlueprintTool.GetRef<BlueprintUnlockableFlagReference>(counterFlag);
          });
    }

    /// <summary>
    /// Sets <see cref="BlueprintRomanceCounter.m_MinValueFlag"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="minValueFlag"><see cref="BlueprintUnlockableFlag"/></param>
    [Generated]
    public RomanceCounterConfigurator SetMinValueFlag(string minValueFlag)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.m_MinValueFlag = BlueprintTool.GetRef<BlueprintUnlockableFlagReference>(minValueFlag);
          });
    }

    /// <summary>
    /// Sets <see cref="BlueprintRomanceCounter.m_MaxValueFlag"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="maxValueFlag"><see cref="BlueprintUnlockableFlag"/></param>
    [Generated]
    public RomanceCounterConfigurator SetMaxValueFlag(string maxValueFlag)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.m_MaxValueFlag = BlueprintTool.GetRef<BlueprintUnlockableFlagReference>(maxValueFlag);
          });
    }
  }
}

using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Area;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Settlements;

namespace BlueprintCore.Blueprints.Configurators.Area
{
  /// <summary>
  /// Configurator for <see cref="BlueprintSettlementAreaPreset"/>.
  /// </summary>
  /// <inheritdoc/>
  [Configures(typeof(BlueprintSettlementAreaPreset))]
  public class SettlementAreaPresetConfigurator : BaseAreaPresetConfigurator<BlueprintSettlementAreaPreset, SettlementAreaPresetConfigurator>
  {
    private SettlementAreaPresetConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static SettlementAreaPresetConfigurator For(string name)
    {
      return new SettlementAreaPresetConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static SettlementAreaPresetConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintSettlementAreaPreset>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static SettlementAreaPresetConfigurator For(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintSettlementAreaPreset>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="BlueprintSettlementAreaPreset.m_StartSettlement"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="startSettlement"><see cref="BlueprintSettlement"/></param>
    [Generated]
    public SettlementAreaPresetConfigurator SetStartSettlement(string startSettlement)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.m_StartSettlement = BlueprintTool.GetRef<BlueprintSettlement.Reference>(startSettlement);
          });
    }

    /// <summary>
    /// Sets <see cref="BlueprintSettlementAreaPreset.m_StartSettlementPoint"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="startSettlementPoint"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    public SettlementAreaPresetConfigurator SetStartSettlementPoint(string startSettlementPoint)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.m_StartSettlementPoint = BlueprintTool.GetRef<BlueprintGlobalMapPointReference>(startSettlementPoint);
          });
    }

    /// <summary>
    /// Sets <see cref="BlueprintSettlementAreaPreset.m_StartSettlementLevel"/> (Auto Generated)
    /// </summary>
    [Generated]
    public SettlementAreaPresetConfigurator SetStartSettlementLevel(SettlementState.LevelType startSettlementLevel)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.m_StartSettlementLevel = startSettlementLevel;
          });
    }
  }
}

using BlueprintCore.Utils;
using Kingmaker.Blueprints.Items;

namespace BlueprintCore.Blueprints.Configurators.Items
{
  /// <summary>
  /// Configurator for <see cref="BlueprintItemThiefTool"/>.
  /// </summary>
  /// <inheritdoc/>
  [Configures(typeof(BlueprintItemThiefTool))]
  public class ItemThiefToolConfigurator : BaseItemConfigurator<BlueprintItemThiefTool, ItemThiefToolConfigurator>
  {
    private ItemThiefToolConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static ItemThiefToolConfigurator For(string name)
    {
      return new ItemThiefToolConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static ItemThiefToolConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintItemThiefTool>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static ItemThiefToolConfigurator For(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintItemThiefTool>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="BlueprintItemThiefTool.m_Consumable"/> (Auto Generated)
    /// </summary>
    [Generated]
    public ItemThiefToolConfigurator SetConsumable(bool consumable)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.m_Consumable = consumable;
          });
    }

    /// <summary>
    /// Sets <see cref="BlueprintItemThiefTool.m_SkillCheckBonus"/> (Auto Generated)
    /// </summary>
    [Generated]
    public ItemThiefToolConfigurator SetSkillCheckBonus(int skillCheckBonus)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.m_SkillCheckBonus = skillCheckBonus;
          });
    }
  }
}

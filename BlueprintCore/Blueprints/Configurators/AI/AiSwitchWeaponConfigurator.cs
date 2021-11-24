using BlueprintCore.Utils;
using Kingmaker.AI.Blueprints;

namespace BlueprintCore.Blueprints.Configurators.AI
{
  /// <summary>
  /// Configurator for <see cref="BlueprintAiSwitchWeapon"/>.
  /// </summary>
  /// <inheritdoc/>
  [Configures(typeof(BlueprintAiSwitchWeapon))]
  public class AiSwitchWeaponConfigurator : BaseAiActionConfigurator<BlueprintAiSwitchWeapon, AiSwitchWeaponConfigurator>
  {
    private AiSwitchWeaponConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static AiSwitchWeaponConfigurator For(string name)
    {
      return new AiSwitchWeaponConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static AiSwitchWeaponConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintAiSwitchWeapon>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static AiSwitchWeaponConfigurator For(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintAiSwitchWeapon>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAiSwitchWeapon.SwitchTo"/> (Auto Generated)
    /// </summary>
    [Generated]
    public AiSwitchWeaponConfigurator SetSwitchTo(SwitchMode switchTo)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.SwitchTo = switchTo;
          });
    }
  }
}

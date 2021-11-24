using BlueprintCore.Utils;
using Kingmaker.AI.Blueprints;

namespace BlueprintCore.Blueprints.Configurators.AI
{
  /// <summary>
  /// Configurator for <see cref="BlueprintAiAttack"/>.
  /// </summary>
  /// <inheritdoc/>
  [Configures(typeof(BlueprintAiAttack))]
  public class AiAttackConfigurator : BaseAiActionConfigurator<BlueprintAiAttack, AiAttackConfigurator>
  {
    private AiAttackConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static AiAttackConfigurator For(string name)
    {
      return new AiAttackConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static AiAttackConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintAiAttack>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static AiAttackConfigurator For(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintAiAttack>(name, assetId);
      return For(name);
    }
  }
}

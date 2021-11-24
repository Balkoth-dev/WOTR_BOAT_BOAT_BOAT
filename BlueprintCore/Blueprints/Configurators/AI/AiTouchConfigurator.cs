using BlueprintCore.Utils;
using Kingmaker.AI.Blueprints;

namespace BlueprintCore.Blueprints.Configurators.AI
{
  /// <summary>
  /// Configurator for <see cref="BlueprintAiTouch"/>.
  /// </summary>
  /// <inheritdoc/>
  [Configures(typeof(BlueprintAiTouch))]
  public class AiTouchConfigurator : BaseAiActionConfigurator<BlueprintAiTouch, AiTouchConfigurator>
  {
    private AiTouchConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static AiTouchConfigurator For(string name)
    {
      return new AiTouchConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static AiTouchConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintAiTouch>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static AiTouchConfigurator For(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintAiTouch>(name, assetId);
      return For(name);
    }
  }
}

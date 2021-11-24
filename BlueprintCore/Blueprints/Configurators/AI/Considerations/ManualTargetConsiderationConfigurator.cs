using BlueprintCore.Utils;
using Kingmaker.AI.Blueprints.Considerations;

namespace BlueprintCore.Blueprints.Configurators.AI.Considerations
{
  /// <summary>
  /// Configurator for <see cref="ManualTargetConsideration"/>.
  /// </summary>
  /// <inheritdoc/>
  [Configures(typeof(ManualTargetConsideration))]
  public class ManualTargetConsiderationConfigurator : BaseConsiderationConfigurator<ManualTargetConsideration, ManualTargetConsiderationConfigurator>
  {
    private ManualTargetConsiderationConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static ManualTargetConsiderationConfigurator For(string name)
    {
      return new ManualTargetConsiderationConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static ManualTargetConsiderationConfigurator New(string name)
    {
      BlueprintTool.Create<ManualTargetConsideration>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static ManualTargetConsiderationConfigurator For(string name, string assetId)
    {
      BlueprintTool.Create<ManualTargetConsideration>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="ManualTargetConsideration.IsManualTargetScore"/> (Auto Generated)
    /// </summary>
    [Generated]
    public ManualTargetConsiderationConfigurator SetIsManualTargetScore(float isManualTargetScore)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.IsManualTargetScore = isManualTargetScore;
          });
    }

    /// <summary>
    /// Sets <see cref="ManualTargetConsideration.NotManualTargetScore"/> (Auto Generated)
    /// </summary>
    [Generated]
    public ManualTargetConsiderationConfigurator SetNotManualTargetScore(float notManualTargetScore)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.NotManualTargetScore = notManualTargetScore;
          });
    }

    /// <summary>
    /// Sets <see cref="ManualTargetConsideration.NoManualTargetScore"/> (Auto Generated)
    /// </summary>
    [Generated]
    public ManualTargetConsiderationConfigurator SetNoManualTargetScore(float noManualTargetScore)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.NoManualTargetScore = noManualTargetScore;
          });
    }
  }
}

using BlueprintCore.Utils;
using Kingmaker.AI.Blueprints.Considerations;

namespace BlueprintCore.Blueprints.Configurators.AI.Considerations
{
  /// <summary>
  /// Configurator for <see cref="DistanceConsideration"/>.
  /// </summary>
  /// <inheritdoc/>
  [Configures(typeof(DistanceConsideration))]
  public class DistanceConsiderationConfigurator : BaseConsiderationConfigurator<DistanceConsideration, DistanceConsiderationConfigurator>
  {
    private DistanceConsiderationConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static DistanceConsiderationConfigurator For(string name)
    {
      return new DistanceConsiderationConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static DistanceConsiderationConfigurator New(string name)
    {
      BlueprintTool.Create<DistanceConsideration>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static DistanceConsiderationConfigurator For(string name, string assetId)
    {
      BlueprintTool.Create<DistanceConsideration>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="DistanceConsideration.MinDistance"/> (Auto Generated)
    /// </summary>
    [Generated]
    public DistanceConsiderationConfigurator SetMinDistance(float minDistance)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.MinDistance = minDistance;
          });
    }

    /// <summary>
    /// Sets <see cref="DistanceConsideration.MaxDistance"/> (Auto Generated)
    /// </summary>
    [Generated]
    public DistanceConsiderationConfigurator SetMaxDistance(float maxDistance)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.MaxDistance = maxDistance;
          });
    }

    /// <summary>
    /// Sets <see cref="DistanceConsideration.MaxDistanceScore"/> (Auto Generated)
    /// </summary>
    [Generated]
    public DistanceConsiderationConfigurator SetMaxDistanceScore(float maxDistanceScore)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.MaxDistanceScore = maxDistanceScore;
          });
    }

    /// <summary>
    /// Sets <see cref="DistanceConsideration.MinDistanceScore"/> (Auto Generated)
    /// </summary>
    [Generated]
    public DistanceConsiderationConfigurator SetMinDistanceScore(float minDistanceScore)
    {
      return OnConfigureInternal(
          bp =>
          {
            bp.MinDistanceScore = minDistanceScore;
          });
    }
  }
}

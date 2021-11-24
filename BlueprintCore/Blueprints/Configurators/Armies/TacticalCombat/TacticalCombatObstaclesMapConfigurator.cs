using BlueprintCore.Utils;
using Kingmaker.Armies.TacticalCombat.Blueprints;
using System.Linq;

namespace BlueprintCore.Blueprints.Configurators.Armies.TacticalCombat
{
  /// <summary>
  /// Configurator for <see cref="BlueprintTacticalCombatObstaclesMap"/>.
  /// </summary>
  /// <inheritdoc/>
  [Configures(typeof(BlueprintTacticalCombatObstaclesMap))]
  public class TacticalCombatObstaclesMapConfigurator : BaseBlueprintConfigurator<BlueprintTacticalCombatObstaclesMap, TacticalCombatObstaclesMapConfigurator>
  {
    private TacticalCombatObstaclesMapConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static TacticalCombatObstaclesMapConfigurator For(string name)
    {
      return new TacticalCombatObstaclesMapConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static TacticalCombatObstaclesMapConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintTacticalCombatObstaclesMap>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static TacticalCombatObstaclesMapConfigurator For(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintTacticalCombatObstaclesMap>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="BlueprintTacticalCombatObstaclesMap.Obstacles"/> (Auto Generated)
    /// </summary>
    [Generated]
    public TacticalCombatObstaclesMapConfigurator SetObstacles(BlueprintTacticalCombatObstaclesMap.MapObstacle[] obstacles)
    {
      ValidateParam(obstacles);
    
      return OnConfigureInternal(
          bp =>
          {
            bp.Obstacles = obstacles;
          });
    }

    /// <summary>
    /// Adds to <see cref="BlueprintTacticalCombatObstaclesMap.Obstacles"/> (Auto Generated)
    /// </summary>
    [Generated]
    public TacticalCombatObstaclesMapConfigurator AddToObstacles(params BlueprintTacticalCombatObstaclesMap.MapObstacle[] obstacles)
    {
      ValidateParam(obstacles);
      return OnConfigureInternal(
          bp =>
          {
            bp.Obstacles = CommonTool.Append(bp.Obstacles, obstacles ?? new BlueprintTacticalCombatObstaclesMap.MapObstacle[0]);
          });
    }

    /// <summary>
    /// Removes from <see cref="BlueprintTacticalCombatObstaclesMap.Obstacles"/> (Auto Generated)
    /// </summary>
    [Generated]
    public TacticalCombatObstaclesMapConfigurator RemoveFromObstacles(params BlueprintTacticalCombatObstaclesMap.MapObstacle[] obstacles)
    {
      ValidateParam(obstacles);
      return OnConfigureInternal(
          bp =>
          {
            bp.Obstacles = bp.Obstacles.Where(item => !obstacles.Contains(item)).ToArray();
          });
    }
  }
}

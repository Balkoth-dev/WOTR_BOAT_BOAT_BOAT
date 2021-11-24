using BlueprintCore.Utils;
using Kingmaker.UnitLogic.Customization;

namespace BlueprintCore.Blueprints.Configurators.UnitLogic.Customization
{
  /// <summary>
  /// Configurator for <see cref="RandomParameters"/>.
  /// </summary>
  /// <inheritdoc/>
  [Configures(typeof(RandomParameters))]
  public class RandomParametersConfigurator : BaseBlueprintConfigurator<RandomParameters, RandomParametersConfigurator>
  {
    private RandomParametersConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static RandomParametersConfigurator For(string name)
    {
      return new RandomParametersConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static RandomParametersConfigurator New(string name)
    {
      BlueprintTool.Create<RandomParameters>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static RandomParametersConfigurator For(string name, string assetId)
    {
      BlueprintTool.Create<RandomParameters>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="RandomParameters.randomParametersInfo"/> (Auto Generated)
    /// </summary>
    [Generated]
    public RandomParametersConfigurator SetRandomParametersInfo(RandomParametersInfo randomParametersInfo)
    {
      ValidateParam(randomParametersInfo);
    
      return OnConfigureInternal(
          bp =>
          {
            bp.randomParametersInfo = randomParametersInfo;
          });
    }
  }
}

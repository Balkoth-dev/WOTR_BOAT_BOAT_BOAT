using BlueprintCore.Utils;
using Kingmaker.AreaLogic.Etudes;

namespace BlueprintCore.Blueprints.Configurators.AreaLogic.Etudes
{
  /// <summary>
  /// Configurator for <see cref="BlueprintEtudeConflictingGroup"/>.
  /// </summary>
  /// <inheritdoc/>
  [Configures(typeof(BlueprintEtudeConflictingGroup))]
  public class EtudeConflictingGroupConfigurator : BaseBlueprintConfigurator<BlueprintEtudeConflictingGroup, EtudeConflictingGroupConfigurator>
  {
    private EtudeConflictingGroupConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static EtudeConflictingGroupConfigurator For(string name)
    {
      return new EtudeConflictingGroupConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static EtudeConflictingGroupConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintEtudeConflictingGroup>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static EtudeConflictingGroupConfigurator For(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintEtudeConflictingGroup>(name, assetId);
      return For(name);
    }
  }
}

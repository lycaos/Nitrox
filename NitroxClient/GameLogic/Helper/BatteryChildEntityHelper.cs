using NitroxModel.DataStructures.GameLogic.Entities;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures;
using NitroxModel_Subnautica.DataStructures;
using System.Collections.Generic;
using UWE;
using UnityEngine;
using static WorldForces;

namespace NitroxClient.GameLogic.Helper;

/// <summary>
/// Vehicles and items are created without a battery loaded into them.Subnautica usually spawns these in async; however, this
/// is disabled in nitrox so we can properly tag the id.  Here we create the installed battery (with a new NitroxId) and have the 
/// entity spawner take care of loading it in.
/// </summary>
public class BatteryChildEntityHelper
{
    public static void PopulateInstalledBattery(GameObject gameObject, List<Entity> toPopulate, NitroxId parentId, Entities entities)
    {
        if (gameObject.TryGetComponent(out EnergyMixin energyMixin))
        {
            PopulateInstalledBattery(energyMixin, toPopulate, parentId, entities);
        }
    }

    public static void PopulateInstalledBattery(EnergyMixin energyMixin, List<Entity> toPopulate, NitroxId parentId, Entities entities)
    {
        InstalledBatteryEntity installedBattery = new(new NitroxId(), energyMixin.defaultBattery.ToDto(), null, parentId, new List<Entity>());
        toPopulate.Add(installedBattery);

        CoroutineHost.StartCoroutine(entities.SpawnAsync(installedBattery));
    }
}

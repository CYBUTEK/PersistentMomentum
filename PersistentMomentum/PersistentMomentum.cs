// 
//     Copyright (C) 2014 CYBUTEK
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

#region Using Directives

using System;

using UnityEngine;

#endregion

namespace PersistentMomentum
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class PersistentMomentum : MonoBehaviour
    {
        #region Fields

        private static bool hasLoadedEvents;

        #endregion

        #region Constructors

        private void Start()
        {
            try
            {
                if (!hasLoadedEvents)
                {
                    GameEvents.onVesselGoOnRails.Add(this.OnRails);
                    GameEvents.onVesselGoOffRails.Add(this.OffRails);
                    hasLoadedEvents = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        #endregion

        #region Private Methods

        private void OnRails(Vessel vessel)
        {
            try
            {
                Logger.Blank();
                Logger.Log(vessel.vesselName + " Going On Rails");
                foreach (var part in vessel.Parts)
                {
                    if (part.physicalSignificance == Part.PhysicalSignificance.FULL && part.Modules.Contains("PersistentMomentumModule"))
                    {
                        (part.Modules["PersistentMomentumModule"] as PersistentMomentumModule).SaveMomentum();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void OffRails(Vessel vessel)
        {
            try
            {
                Logger.Blank();
                Logger.Log(vessel.vesselName + " Going Off Rails");
                foreach (var part in vessel.Parts)
                {
                    if (part.physicalSignificance == Part.PhysicalSignificance.FULL && part.Modules.Contains("PersistentMomentumModule"))
                    {
                        (part.Modules["PersistentMomentumModule"] as PersistentMomentumModule).LoadMomentum();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        #endregion
    }
}
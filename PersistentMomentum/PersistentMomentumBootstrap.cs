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
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class PersistentMomentumBootstrap : MonoBehaviour
    {
        private void Start()
        {
            try
            {
                foreach (var node in GameDatabase.Instance.GetConfigNodes("PART"))
                {
                    node.AddNode("MODULE").AddValue("name", "PersistentMomentumModule");
                    Logger.Log("Added Module to " + node.GetValue("name"));
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }
    }
}
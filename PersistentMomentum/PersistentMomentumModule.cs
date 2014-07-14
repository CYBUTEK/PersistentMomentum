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
    public class PersistentMomentumModule : PartModule
    {
        #region Fields

        [KSPField(isPersistant = true)] public Vector3 AngularVelocity;
        [KSPField(isPersistant = true)] public Vector3 Velocity;

        private bool onRails = true;

        #endregion

        #region Save & Load

        public void SaveMomentum()
        {
            Logger.Log("Saving Momentum on " + this.part.partName);
            this.onRails = true;
        }

        public void LoadMomentum()
        {
            try
            {
                Logger.Log("Loading Momentum on " + this.part.name);
                this.rigidbody.angularVelocity = this.AngularVelocity;
                this.rigidbody.velocity = this.Velocity + this.vessel.rigidbody.velocity;
                this.onRails = false;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        #endregion

        #region Update

        public override void OnUpdate()
        {
            try
            {


                if (!this.onRails && this.part.physicalSignificance == Part.PhysicalSignificance.FULL)
                {
                    this.AngularVelocity = this.rigidbody.angularVelocity;
                    this.Velocity = this.rigidbody.velocity;
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
﻿using DellFanManagement.DellSmbiozBzhLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DellFanManagement.App.FanControllers
{
    /// <summary>
    /// Allows fan speed control using the BZH SMM I/O driver.
    /// </summary>
    class BzhFanController : FanController
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public BzhFanController()
        {
            IsIndividualFanControlSupported = false;
        }

        /// <summary>
        /// Disable automatic fan control.
        /// </summary>
        /// <returns>True on success, false on failure.</returns>
        public override bool DisableAutomaticFanControl()
        {
            return DellSmbiosBzh.DisableAutomaticFanControl();
        }

        /// <summary>
        /// Enable automatic fan control.
        /// </summary>
        /// <returns>True on success, false on failure.</returns>
        public override bool EnableAutomaticFanControl()
        {
            return DellSmbiosBzh.EnableAutomaticFanControl();
        }

        /// <summary>
        /// Set the fan speed.
        /// </summary>
        /// <param name="level">Speed level to set.</param>
        /// <param name="fanIndex">Which fan to set.</param>
        /// <returns>True on succes, false on failure.</returns>
        public override bool SetFanLevel(FanLevel level, FanIndex fanIndex)
        {
            bool result1 = true;
            bool result2 = true;

            BzhFanLevel bzhLevel;
            switch (level)
            {
                case FanLevel.Off:
                    bzhLevel = BzhFanLevel.Level0;
                    break;
                case FanLevel.Medium:
                    bzhLevel = BzhFanLevel.Level1;
                    break;
                case FanLevel.High:
                    bzhLevel = BzhFanLevel.Level2;
                    break;
                default:
                    return false;
            }

            if (fanIndex == FanIndex.Fan1 || fanIndex == FanIndex.AllFans)
            {
                result1 = DellSmbiosBzh.SetFanLevel(BzhFanIndex.Fan1, bzhLevel);
            }

            if (fanIndex == FanIndex.Fan2 || fanIndex == FanIndex.AllFans)
            {
                result2 = DellSmbiosBzh.SetFanLevel(BzhFanIndex.Fan2, bzhLevel);
            }

            return result1 && result2;
        }
    }
}

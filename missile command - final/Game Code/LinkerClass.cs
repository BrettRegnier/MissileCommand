using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile_Command___Final
{
    /// <summary>
    /// Links all the thing I need on several different forms together.
    /// </summary>
    class LinkerClass
    {
        public bool isPlaying = true;
        public double currentMoney = 0;
        public int explosionDamage = 35;
        public int currentHealth = 100;

        // Building Status
        public bool incomeDestroyed = false;
        public bool researchDestroyed = false;
        public bool engineerDestroyed = false;

        // Income Building
        public double bountryReward = 10;

        // Research Building
        public int reloadAmmoInterval = 3000;
        public int explosionVar = 200;
        public double bombSpeedVar = 15;

        // Engineer Building
        public int maxHealth = 100;
        public int repairAmt = 1;
        public int repairSpeed = 10000; // Mess with values.
    }
}

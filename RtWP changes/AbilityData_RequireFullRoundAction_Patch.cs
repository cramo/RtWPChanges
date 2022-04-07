using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker;
using HarmonyLib;
using Kingmaker.UnitLogic.Abilities;
using UnityModManagerNet;

namespace RtWPChanges
{
    //HarmonyPatch attribute allows PatchAll to find the patch
    [HarmonyPatch(typeof(AbilityData), "RequireFullRoundAction")]
    static class AbilityData_RequireFullRoundAction_Patch
    {
        static bool loaded = false;
        //Postfix must be spelt correctly to be applied
        static void Postfix(ref bool __result)
        {
            if (loaded) return;
            loaded = true;
            if (!Main.Enabled) return;
            // Harmony parameters are determined by name, __result 
            // is the current cost of the mercenary. Because it is a 
            // ref parameter, we can modify it's value
            try
            {
                __result = false;
            }
            catch(Exception e)
            {
                e.Message.ToString();
            }
        }
    }
}

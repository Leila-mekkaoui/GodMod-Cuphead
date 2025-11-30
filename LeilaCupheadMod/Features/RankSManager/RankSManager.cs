using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace LeilaCupheadMod.Features.RankS
{
    public class RankSManager
    {
        public bool alwaysRankS = false;

        public RankSManager()
        {
            try
            {
                var harmony = new Harmony("com.leila.ranks");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                Debug.Log("[RANK S] Harmony patches applied successfully");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[RANK S] Harmony patching failed: {ex.Message}");
            }
        }

        public void ToggleRankS()
        {
            alwaysRankS = !alwaysRankS;
            Debug.Log($"[RANK S] {(alwaysRankS ? "ACTIVÉ" : "DÉSACTIVÉ")}");
        }

        // Patch pour forcer le rang S
        [HarmonyPatch(typeof(LevelScoringData), "CalculateGrade")]
        public static class RankSPatch
        {
            public static void Postfix(ref LevelScoringData.Grade __result)
            {
                try
                {
                    CorePlugin[] corePlugins = Object.FindObjectsOfType<CorePlugin>();
                    foreach (CorePlugin corePlugin in corePlugins)
                    {
                        if (corePlugin.rankS != null && corePlugin.rankS.alwaysRankS)
                        {
                            __result = LevelScoringData.Grade.S; // Grade S
                            Debug.Log($"[RANK S] Forcé: S");
                            break;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"[RANK S] Error in RankSPatch: {ex.Message}");
                }
            }
        }

        public void OnGUI()
        {
            if (alwaysRankS)
            {
                GUI.color = Color.magenta;
                GUI.skin.label.fontSize = 20;
                GUI.skin.label.fontStyle = FontStyle.Bold;

                GUI.Label(new Rect(10, 120, 300, 30), "⭐ RANK S FORCÉ ⭐");
            }
        }
    }
}
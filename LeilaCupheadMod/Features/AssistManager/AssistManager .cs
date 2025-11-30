using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace LeilaCupheadMod.Features.Assist
{
    public class AssistManager
    {
        public bool assistEnabled = false;
        private int frameCount = 0;
        private Harmony harmony;

        public AssistManager()
        {
            try
            {
                harmony = new Harmony("com.leila.assist");

                // Patch manuel pour tester
                var originalMethod = typeof(AbstractProjectile).GetProperty("DamageMultiplier").GetGetMethod();
                var postfix = new HarmonyMethod(typeof(AssistManager).GetMethod("DamageMultiplierPostfix", BindingFlags.Static | BindingFlags.NonPublic));
                harmony.Patch(originalMethod, postfix: postfix);

                Debug.Log("[ASSIST] Manual patch applied successfully");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[ASSIST] Manual patching failed: {ex.Message}");
            }
        }

        private static void DamageMultiplierPostfix(ref float __result)
        {
            Debug.Log($"[ASSIST] DamageMultiplier called: {__result}");

            CorePlugin[] corePlugins = Object.FindObjectsOfType<CorePlugin>();
            foreach (CorePlugin corePlugin in corePlugins)
            {
                if (corePlugin.assist != null && corePlugin.assist.assistEnabled)
                {
                    if (__result > 0f)
                    {
                        float baseDamage = 1f;
                        __result = baseDamage * 3f;
                        Debug.Log($"[ASSIST] Dégâts set à: {__result}");
                    }
                    break;
                }
            }
        }

        public void ToggleAssist()
        {
            assistEnabled = !assistEnabled;
            Debug.Log($"[ASSIST] {(assistEnabled ? "ACTIVÉ" : "DÉSACTIVÉ")}");
        }

        public void Update()
        {
            frameCount++;
            if (frameCount % 120 == 0 && assistEnabled)
            {
                Debug.Log($"[ASSIST] Actif - Frame: {frameCount}");
            }
        }

        public void OnGUI()
        {
            if (assistEnabled)
            {
                GUI.color = Color.cyan;
                GUI.skin.label.fontSize = 22;
                GUI.skin.label.fontStyle = FontStyle.Bold;
                GUI.skin.label.alignment = TextAnchor.UpperLeft;

                GUI.Label(new Rect(10, 160, 300, 40), "🛡️ ASSIST ACTIVÉ 🛡️");
                GUI.Label(new Rect(10, 190, 300, 30), "Vies: 6");
                GUI.Label(new Rect(10, 220, 350, 30), "Dégâts: x3");

                GUI.color = new Color(0f, 1f, 1f, 0.3f);
                GUI.DrawTexture(new Rect(5, 155, 310, 100), Texture2D.whiteTexture);
            }
        }
    }
}
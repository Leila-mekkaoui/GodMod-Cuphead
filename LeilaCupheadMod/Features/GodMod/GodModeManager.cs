using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace LeilaCupheadMod.Features.GodMode
{
    public class GodModeManager
    {
        public bool godModeEnabled = false;
        private int frameCount = 0;

        public GodModeManager()
        {
            try
            {
                var harmony = new Harmony("com.leila.godmode");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                Debug.Log("[GODMODE] Harmony patches applied successfully");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[GODMODE] Harmony patching failed: {ex.Message}");
            }
        }

        public void ToggleGodMode()
        {
            godModeEnabled = !godModeEnabled;
            Debug.Log($"[GODMODE] {(godModeEnabled ? "ACTIVÉ" : "DÉSACTIVÉ")}");
        }

        public void Update()
        {
            frameCount++;

            // Log toutes les 2 secondes pour debug
            if (frameCount % 120 == 0 && godModeEnabled)
            {
                Debug.Log($"[GODMODE] Actif - Frame: {frameCount}");
            }
        }

        // Patch pour l'invincibilité
        [HarmonyPatch(typeof(LevelPlayerController), "CanTakeDamage", MethodType.Getter)]
        public static class InvincibilityPatch
        {
            public static void Postfix(ref bool __result)
            {
                try
                {
                    CorePlugin[] corePlugins = Object.FindObjectsOfType<CorePlugin>();
                    foreach (CorePlugin corePlugin in corePlugins)
                    {
                        if (corePlugin.godMode != null && corePlugin.godMode.godModeEnabled)
                        {
                            __result = false; // Invincible
                            Debug.Log($"[GODMODE] CanTakeDamage = false");
                            break;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"[GODMODE] Error in InvincibilityPatch: {ex.Message}");
                }
            }
        }

        [HarmonyPatch(typeof(AbstractProjectile), "DamageMultiplier", MethodType.Getter)]
        public static class DamagePatch
        {
            public static void Postfix(ref float __result)
            {
                try
                {
                    CorePlugin[] corePlugins = Object.FindObjectsOfType<CorePlugin>();
                    foreach (CorePlugin corePlugin in corePlugins)
                    {
                        if (corePlugin.godMode != null && corePlugin.godMode.godModeEnabled)
                        {
                            // Vérifier si c'est un projectile joueur
                            if (__result > 0f) // Si c'est un projectile joueur
                            {
                                __result *= 10f; // x10 dégâts
                                Debug.Log($"[GODMODE] Dégâts multipliés: {__result}");
                            }
                            break;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"[GODMODE] Error in DamagePatch: {ex.Message}");
                }
            }
        }

        public void OnGUI()
        {
            if (godModeEnabled)
            {
                // UI très visible
                GUI.color = Color.yellow;
                GUI.skin.label.fontSize = 24;
                GUI.skin.label.fontStyle = FontStyle.Bold;
                GUI.skin.label.alignment = TextAnchor.UpperLeft;

                GUI.Label(new Rect(10, 10, 300, 40), "🔥 GOD MODE ACTIVÉ 🔥");
                GUI.Label(new Rect(10, 50, 300, 30), "Invincibilité: OUI");
                GUI.Label(new Rect(10, 80, 350, 30), "Dégâts: x10");

                // Fond semi-transparent
                GUI.color = new Color(1f, 1f, 0f, 0.3f);
                GUI.DrawTexture(new Rect(5, 5, 310, 110), Texture2D.whiteTexture);
            }
            else
            {
                // Message quand désactivé
                GUI.color = Color.white;
                GUI.skin.label.fontSize = 16;
                GUI.Label(new Rect(10, 10, 200, 30), "F5: God Mode");
            }
        }
    }
}
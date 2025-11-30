using BepInEx;
using UnityEngine;

[BepInPlugin("Leila.LeilaCupheadMod", "LeilaCupheadMod", "1.0.0")]
public class CorePlugin : BaseUnityPlugin
{
    private LeilaCupheadMod.Features.HealthBar.HealthBarManager healthBar;
    public LeilaCupheadMod.Features.GodMode.GodModeManager godMode;
    public LeilaCupheadMod.Features.RankS.RankSManager rankS;
    public LeilaCupheadMod.Features.Assist.AssistManager assist;

    private void Awake()
    {
        Debug.Log("[LEILA MOD] Awake called");

        try
        {
            healthBar = new LeilaCupheadMod.Features.HealthBar.HealthBarManager();
            Debug.Log("[LEILA MOD] HealthBar initialized");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[LEILA MOD] HealthBar failed: {ex.Message}");
        }

        try
        {
            godMode = new LeilaCupheadMod.Features.GodMode.GodModeManager();
            Debug.Log("[LEILA MOD] GodMode initialized");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[LEILA MOD] GodMode failed: {ex.Message}");
        }

        try
        {
            rankS = new LeilaCupheadMod.Features.RankS.RankSManager();
            Debug.Log("[LEILA MOD] RankS initialized");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[LEILA MOD] RankS failed: {ex.Message}");
        }

        try
        {
            assist = new LeilaCupheadMod.Features.Assist.AssistManager();
            Debug.Log("[LEILA MOD] Assist initialized");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[LEILA MOD] Assist failed: {ex.Message}");
        }

        Logger.LogInfo("Leila Mod loaded!");
        Logger.LogInfo("HealthBar: Active | GodMode: F5 | RankS: F6 | Assist: F7");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5) && godMode != null)
        {
            godMode.ToggleGodMode();
        }

        if (Input.GetKeyDown(KeyCode.F6) && rankS != null)
        {
            rankS.ToggleRankS();
        }

        if (Input.GetKeyDown(KeyCode.F7) && assist != null)
        {
            assist.ToggleAssist();
        }

        if (godMode != null)
        {
            godMode.Update();
        }

        if (assist != null)
        {
            assist.Update();
        }
    }

    private void OnGUI()
    {
        healthBar?.OnGUI();
        godMode?.OnGUI();
        rankS?.OnGUI();
        assist?.OnGUI();
    }
}
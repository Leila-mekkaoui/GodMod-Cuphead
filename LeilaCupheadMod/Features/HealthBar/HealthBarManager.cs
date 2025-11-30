using UnityEngine;


namespace LeilaCupheadMod.Features.HealthBar
{
    public class HealthBarManager
    {
        private Texture2D healthTex;
        private Texture2D bgTex;
        private GUIStyle textStyle;

        public HealthBarManager()
        {
            healthTex = CreateTexture(Color.red);
            bgTex = CreateTexture(new Color(0, 0, 0, 0.7f));

            textStyle = new GUIStyle();
            textStyle.normal.textColor = Color.white;
            textStyle.fontSize = 16;
            textStyle.alignment = TextAnchor.UpperCenter;
        }

        private Texture2D CreateTexture(Color color)
        {
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color);
            tex.Apply();
            return tex;
        }

        public void OnGUI()
        {
            if (Level.Current == null || Level.Current.timeline == null)
                return;

            var timeline = Level.Current.timeline;
            float damage = timeline.damage;
            float totalHealth = timeline.health;

            if (totalHealth <= 0) return;

            float healthPercent = Mathf.Max(0, 1f - (damage / totalHealth));

            float width = 400f;
            float height = 25f;
            float x = (Screen.width - width) / 2f;
            float y = 50f;

            GUI.DrawTexture(new Rect(x - 5, y - 5, width + 10, height + 30), bgTex);

            GUI.color = Color.gray;
            GUI.DrawTexture(new Rect(x, y + 20, width, height), healthTex);

            GUI.color = Color.red;
            GUI.DrawTexture(new Rect(x, y + 20, width * healthPercent, height), healthTex);

            GUI.color = Color.white;
            string displayText = $"BOSS - {Mathf.RoundToInt(healthPercent * 100f)}%\n{totalHealth - damage:F0}/{totalHealth:F0} HP";
            GUI.Label(new Rect(x, y, width, 50f), displayText, textStyle);
        }
    }
}
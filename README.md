# 💡 Leila Cuphead Mod

Ce mod pour **Cuphead** (PC) ajoute des fonctionnalités de triche et d'assistance pour faciliter le gameplay ou tester des niveaux rapidement.

## 🛠 Installation

Ce mod nécessite **BepInEx 5** pour fonctionner.

1.  Assurez-vous que [BepInEx](https://github.com/BepInEx/BepInEx/releases) est installé dans le dossier racine de votre jeu Cuphead.
2.  Récupérez le fichier `LeilaCupheadMod.dll` (généralement dans le dossier `bin/Debug/`).
3.  Placez la DLL dans le dossier suivant :
    `SteamLibrary\steamapps\common\Cuphead\BepInEx\plugins`
4.  Lancez le jeu.

---

## 🎮 Commandes en jeu (Hotkeys)

| Touche | Action | Description |
| :--- | :--- | :--- |
| **F5** | **God Mod** | Active l'invincibilité totale et multiplie les dégâts par 10. |
| **F6** | **Rank S Force** | Force l'obtention du rang S à la fin du niveau. |
| **F7** | **Assist Mode** | Définit la santé à 6 vies et multiplie les dégâts par 3. |

---

## ✨ Fonctionnalités passives
* **HUD HP :** La barre de points de vie reste affichée en permanence à l'écran pour un meilleur suivi.

---

## 🛠 Développement & Compilation

Si vous souhaitez modifier le code source :
1.  Ouvrez le fichier `LeilaCupheadMod.csproj` avec **Visual Studio**.
2.  **Références :** Si des erreurs apparaissent, liez manuellement les fichiers `UnityEngine.dll` et `Assembly-CSharp.dll` situés dans le dossier `Cuphead_Data/Managed/` de votre installation de jeu.
3.  Compilez (`Build`) pour générer une nouvelle version de la DLL.

---
> *Mod inspirée de je sais plus je dois retrouver le git.*

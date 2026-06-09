using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    public override void Initialize(MenuController context)
    {
        base.Initialize(context);
        state = MenuStates.SettingsMenu;

        if (allButtons.Length == 0)
        {
            Debug.LogWarning("No buttons found in SettingsMenu. Please ensure there are Button components in the children of this GameObject.");
            return;
        }

        foreach (Button button in allButtons)
        {
            if (button == null) continue;
            if (button.name.Contains("Credits")) button.onClick.AddListener(() => context.JumpTo(MenuStates.CreditsMenu));
            if (button.name.Contains("Back")) button.onClick.AddListener(() => context.JumpBack());
        }
    }
}

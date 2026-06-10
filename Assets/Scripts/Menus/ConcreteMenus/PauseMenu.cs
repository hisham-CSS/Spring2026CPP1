using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    public override void Initialize(MenuController context)
    {
        base.Initialize(context);

        state = MenuStates.PauseMenu;

        if (allButtons.Length == 0)
        {
            Debug.LogWarning("No buttons found in SettingsMenu. Please ensure there are Button components in the children of this GameObject.");
            return;
        }

        foreach (Button button in allButtons)
        {
            if (button == null) continue;
            if (button.name.Contains("Resume")) button.onClick.AddListener(() => context.JumpBack());
            if (button.name.Contains("Menu")) button.onClick.AddListener(() => SceneManager.LoadScene("Title"));
            if (button.name.Contains("Quit")) button.onClick.AddListener(QuitGame);
        }
    }

    public override void Enter()
    {
        base.Enter();

        Time.timeScale = 0;
    }

    public override void Exit()
    {
        base.Exit();

        Time.timeScale = 1;
    }
}

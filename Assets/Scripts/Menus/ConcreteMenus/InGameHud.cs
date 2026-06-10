using UnityEngine;
using TMPro;

public class InGameHud : BaseMenu
{
    [SerializeField] private TMP_Text livesText;

    public override void Initialize(MenuController context)
    {
        base.Initialize(context);
        state = MenuStates.InGameHud;  //STATE was added to the MenuStates enum

        //lives text is implemented via reference in the inspector
        if (livesText)
        {
            livesText.text = $"Lives: {GameManager.Instance.lives}";

            //An action that is fired when the life value is changed was added to the game manager.  Now the UI is only updated when the life values change.
            GameManager.Instance.OnLifeValueChanged += (value) => livesText.text = $"Lives: {value}";
        }
        else
        {
            Debug.Log("No lives text available on the HUD");
        }
    }
}

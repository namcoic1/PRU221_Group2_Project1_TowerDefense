using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    //The UI text for the health count
    public Text txt_healthCount;
    //The default value of the health count (used for init)
    public int defaultHealthCount;
    //Current health count
    public int healthCount;
    [SerializeField] GameObject pauseMenu;

    //Init the health system (reset the health count)
    public void Init()
    {
        healthCount = defaultHealthCount;
        txt_healthCount.text = healthCount.ToString();
    }

    //Lose health count
    public void LoseHealth()
    {
        if (healthCount < 1)
            return;

        healthCount--;
        txt_healthCount.text = healthCount.ToString();

        CheckHealthCount();
    }

    //Check health count for losing
    void CheckHealthCount()
    {
        if (healthCount < 1)
        {
            //Call some reset values and stop the game from the manager
            SSTools.ShowMessage("You lost.", SSTools.Position.bottom, SSTools.Time.halfsecond);
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            SSTools.ShowMessage("Health decreases.", SSTools.Position.bottom, SSTools.Time.halfsecond);
        }
    }
}

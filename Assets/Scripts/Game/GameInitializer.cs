using UnityEngine;
namespace Assets.Scripts.Game
{
    public class GameInitializer : MonoBehaviour
    {
        void Awake()
        {
            ConfigurationUtils.Initialize();
        }
    }
}

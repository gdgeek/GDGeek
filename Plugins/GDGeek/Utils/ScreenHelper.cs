using UnityEngine;
namespace GDGeek
{
    public class ScreenHelper : MonoBehaviour
    {
        
        [SerializeField] private ScreenOrientation _orientation = ScreenOrientation.LandscapeRight;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Screen.orientation = _orientation;
        }

    }
}
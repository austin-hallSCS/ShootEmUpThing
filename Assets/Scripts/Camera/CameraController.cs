using UnityEngine;
using WizardGame.Player;

namespace WizardGame.Camera
{
    public class CameraController : MonoBehaviour
    {
        [field: SerializeField] public GameObject Player { get; private set; }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        void LateUpdate()
        {
            Vector3 targetPosition = Player.transform.position;
            targetPosition.z -= 1.5f;
            transform.position = targetPosition;
        }
    }
}

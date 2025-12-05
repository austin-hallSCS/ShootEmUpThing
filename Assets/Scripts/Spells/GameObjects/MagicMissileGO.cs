using UnityEngine;
using WizardGame.Spells;

namespace WizardGame
{
    public class MagicMissileGO : SpellGO
    {
        float moveSpeed = 5f;

        protected override void Start()
        {

        }

        protected override void Update()
        {
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = new Vector2(currentPosition.x + moveSpeed, currentPosition.y);

            transform.position = newPosition;
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class StartGameItem : SelectableItem
    {
        [SerializeField] private SceneChanger changer;

        public override void Select()
        {
            changer.OnClick("GameScene");
        }
    }
}

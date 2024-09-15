using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class StartGameItem : SelectableItem
    {
        public override void Select()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Assets._Project.Scripts.Runtime.Input
{
    public class InputHandler : MonoBehaviour
    {
        private PlayerActions _input;
        public PlayerActions Input => _input ??= new PlayerActions();
    }
}

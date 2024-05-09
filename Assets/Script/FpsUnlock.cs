using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class FpsUnlock : MonoBehaviour
    {
        [Range(30, 144)]
        public int targetFPS = 60;
        private void Awake()
        {
            Application.targetFrameRate = targetFPS;
        }
    }
}

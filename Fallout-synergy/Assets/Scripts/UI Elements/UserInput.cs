using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace UI_Elements
{
    public class UserInput
    {
        public static Vector3 GetMouseWorldPosition(Camera camera)
        {
            Vector3 vec = GetMousePositionWithZ(Input.mousePosition, camera);
            vec.z = 0f;
            return vec;
        }

        public static Vector3 GetMousePositionWithZ(Vector3 screenPos, Camera camera)
        {
            Vector3 worldPos = camera.ScreenToWorldPoint(screenPos);
            return worldPos;
        }
    }
}

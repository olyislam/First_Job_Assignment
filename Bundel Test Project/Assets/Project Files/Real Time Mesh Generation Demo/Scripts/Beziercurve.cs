using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assignment
{
    public class Beziercurve : MonoBehaviour
    {
        [SerializeField] protected Transform[] points = new Transform[4];

        protected Vector3 GetBezier_Point(float time)
        {
            Vector3 A = Vector3.Lerp(points[0].position, points[1].position, time);
            Vector3 B = Vector3.Lerp(points[1].position, points[2].position, time);
            Vector3 C = Vector3.Lerp(points[3].position, points[3].position, time);

            Vector3 D = Vector3.Lerp(A, B, time);
            Vector3 E = Vector3.Lerp(B, C, time);

            return Vector3.Lerp(D, E, time);
        }

        protected Vector3 GetBezier_Tangent(float time)
        {
            Vector3 A = Vector3.Lerp(points[0].position, points[1].position, time);
            Vector3 B = Vector3.Lerp(points[1].position, points[2].position, time);
            Vector3 C = Vector3.Lerp(points[3].position, points[3].position, time);

            Vector3 D = Vector3.Lerp(A, B, time);
            Vector3 E = Vector3.Lerp(B, C, time);

            return (E - D).normalized;
        }

        public Quaternion GetBezierOrientation(float time)
        {
            return Quaternion.LookRotation(GetBezier_Tangent(time));
        }
    }
}

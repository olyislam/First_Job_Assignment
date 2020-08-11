using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assignment
{

    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]

    public class RealTimeMesh : Beziercurve
    {

        [SerializeField, Range(5f, 100f)] protected float PolyCount;


        private void Update()
        {
            List<Vector3> vertices = new List<Vector3>();
            vertices = GenerateVertex(); //create vertex W.T.To Beziercurve path

            MeshShape MainShape = new MeshShape(); //create shape object 
            MainShape.meshData.SetVertices = vertices;//set vertices into the MainShape object that will generate visible mesh

            Mesh mesh = MainShape.meshData.Mesh;
            GetComponent<MeshFilter>().sharedMesh = mesh;
        }


        private List<Vector3> GenerateVertex()
        {
            //store all vertices point
            List<Vector3> vertices = new List<Vector3>();

            for (int i = 0; i <= PolyCount; i++)
            {
                float time = i / (float)PolyCount;
                float next_time = (i + 1) / (float)PolyCount > (float)PolyCount ? time : (i + 1) / (float)PolyCount;

                Vector3 point = GetBezier_Point(time);
                Quaternion rot = GetBezierOrientation(time);

                Vector3 nextPointb = GetBezier_Point(next_time);
                Quaternion nextRot = GetBezierOrientation(next_time);

                MeshShape A = new MeshShape(point, rot, 5); //first root point vertex points
                MeshShape B = new MeshShape(nextPointb, nextRot, 5); //next root point vertex points


                List<Vector3> TileShape = MeshShape.AddShape(A, B);
                vertices.AddRange(TileShape);
            }
            return vertices;

        }

        
    }
}
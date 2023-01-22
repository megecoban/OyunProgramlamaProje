using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class tempBezier : MonoBehaviour
{
    public List<Transform> positionsTransform;
    private List<Vector3> positions;

    float timer = 0;

    [SerializeField] private GameObject GO;


    private Vector3[] p0, p1, p2, p3;


    // Bezier eðrisinin sonucunu tutacak deðiþken
    private Vector3 bezierPoint;

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.O))
        {
            if(positions!=null)
                positions.Clear();

            if (positions == null)
                positions = new List<Vector3>();

            for (int a = 0; a < positionsTransform.Count; a++)
            {
                positions.Add(positionsTransform[a].position);
            }

            setControlPoints();
        }

        if(positions!=null && positions.Count >= 2)
        {
            GO.transform.position = (GetBezierPoint(timer));
        }

    }

    private void setControlPoints()
    {
        // Control noktalarýný otomatik olarak belirleyelim
        p0 = new Vector3[positions.Count];
        p1 = new Vector3[positions.Count];
        p2 = new Vector3[positions.Count];
        p3 = new Vector3[positions.Count];

        for (int i = 0; i < positions.Count; i++)
        {
            p0[i] = positions[i];
            p3[i] = positions[(i + 1) % positions.Count];
            p1[i] = p0[i] + (p3[i] - p0[i]) / 3;
            p2[i] = p3[i] + (p0[i] - p3[i]) / 3;
        }
    }


    // Bezier eðrisi oluþturan fonksiyon
    Vector3 GetBezierPoint(float t)
    {
        t = t % 1;
        // Bezier eðrisinin formülünü kullanarak eðrinin sonucunu hesaplýyoruz
        Vector3 point = Vector3.zero;
        for (int i = 0; i < positions.Count; i++)
        {
            int index = (i + (int)t) % positions.Count;
            point += (Mathf.Pow(1 - t, 3) * p0[index]) + (3 * Mathf.Pow(1 - t, 2) * t * p1[index]) + (3 * (1 - t) * Mathf.Pow(t, 2) * p2[index]) + (Mathf.Pow(t, 3) * p3[index]) * BinomialCoefficient(positions.Count - 1, i);
        }
        return point;
    }

    float BinomialCoefficient(int n, int k)
    {
        int result = 1;
        for (int i = 1; i <= k; i++)
        {
            result *= (n - i + 1);
            result /= i;
        }
        return result;
    }
}

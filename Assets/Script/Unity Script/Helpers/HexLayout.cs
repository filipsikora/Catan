using UnityEngine;

namespace Unity.Helpers
{
    public static class HexLayout
    {
        public static Vector3 AxialToPixel(int q, int r, float size)
        {
            float x = Mathf.Sqrt(3f) * (q + r * 0.5f) * size;
            float y = 1.5f * r * size;

            return new Vector3(x, 0, y);
        }

        public static Vector3 GetCorner(int q, int r, int cornerIndex, float size)
        {
            var center = AxialToPixel(q, r, size);
            float angle = Mathf.Deg2Rad * (60 * cornerIndex - 30);

            return center + new Vector3(Mathf.Cos(angle) * size, 0, Mathf.Sin(angle) * size);
        }
    }
}
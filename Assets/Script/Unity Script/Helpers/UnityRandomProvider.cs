using Catan.Shared.Interfaces;

namespace Catan.Unity.Helpers
{
    public class UnityRandomProvider : IRandomProvider
    {
        public float NextFloat() => UnityEngine.Random.value;

        public int NextInt(int min, int max) => UnityEngine.Random.Range(min, max);
    }
}   
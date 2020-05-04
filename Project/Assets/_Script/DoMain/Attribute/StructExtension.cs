namespace OurGameName.DoMain.Attribute
{
    using UnityEngine;

    public struct Float2
    {
        public float a, b;

        public static Float2 Create()
        {
            Float2 float2;
            float2.a = Random.value;
            float2.b = Random.value;
            return float2;
        }
    }
}
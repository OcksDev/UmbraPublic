using UnityEngine;

public class I_Constructmyballs : MonoBehaviour
{
    public GameObject[] particles;
    public Card melol;
    private void Awake()
    {
        foreach (var particle in particles)
        {
            particle.SetActive(false);
        }
    }

    public void SetParticle(int index, bool yes)
    {
        particles[index].SetActive(yes);
        if (yes)
        {
            var p = particles[index].GetComponent<ParticleSystem>();
            var g = Gamer.Instance;
            bool scalewithsize = true;
            bool emissionratescalewithvolume = true;
            float defaultemrate = 5;
            bool overrideemrate = true;
            switch (index)
            {
                default:
                    break;
            }
            if (scalewithsize)
            {
                var e = p.shape;
                if (melol.IndividualParts)
                {
                    e.scale = new Vector3(1, 1, 0);
                }
                else
                {
                    e.scale = new Vector3(g.ConstructSizes[melol.CardType][0], g.ConstructSizes[melol.CardType][1], 0);
                }
            }
            if (overrideemrate)
            {
                var e = p.emission;
                e.rateOverTime = defaultemrate;
            }
            if (emissionratescalewithvolume)
            {
                var e = p.emission;
                var s = p.shape;
                e.rateOverTime = e.rateOverTime.constant * ( s.scale.x * s.scale.y);
            }
            p.Play();
        }
    }
}

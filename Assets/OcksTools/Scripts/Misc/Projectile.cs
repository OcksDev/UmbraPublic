using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    public SpawnData spawnData;
    public Card target_card;
    public float Speed = 5f;
    public float Damage = 15f;
    private Vector3 targetpos;
    public int TargetType = 0;
    public SpriteRenderer sr;
    public Sprite[] sprites;
    public bool IsPlayerOwned;
    public List<Card> targets = new List<Card>();
    public List<Card> hits = new List<Card>();

    public Gamer g;
    public void Start()
    {
        GetTarget();
        Sprite chosen = sprites[0];
        switch (spawnData.card.CardType)
        {
            case 21:
            case 6:
            case 3:
                if (IsPlayerOwned)
                {
                    chosen = sprites[1];
                }
                else
                {
                    chosen = sprites[2];
                }
                break;
            case 13:
                if (IsPlayerOwned)
                {
                    chosen = sprites[3];
                }
                else
                {
                    chosen = sprites[4];
                }
                break;
            case 26:
                if (IsPlayerOwned)
                {
                    chosen = sprites[5];
                }
                else
                {
                    chosen = sprites[6];
                }
                break;
        }

        sr.sprite = chosen;
    }
    public void GetData()
    {
        Speed = 15f;
        switch (spawnData.card.CardType)
        {
            default:
                Damage = 15f;
                break;
            case 6:
                Damage = 10f;
                break;
            case 26:
                Damage = 20f;
                TargetType = 1;
                break;
            case 13:
                Damage = 5f;
                TargetType = 1;
                break;
        }
    }



    public void GetTarget(bool changedam = true)
    {
        float dam = Damage;
        IsPlayerOwned = spawnData.card.IsPlayerControlled;
        g = Gamer.Instance;
        GetData();
        switch (TargetType)
        {
            case 1:
                int y2 = (int)spawnData.card.GridPos.y;
                var a2 = new Vector2(IsPlayerOwned ? 1000 : -1000, y2);
                targets.Clear();
                for (int i = 0; i < g.MapX; i++)
                {
                    var hh = IsPlayerOwned ? g.enemyturf[i, y2] : g.turf[i, y2];
                    if (hh != null && !targets.Contains(hh) && !hh.IsFakeCard) targets.Add(hh);
                }
                break;
            case 0:
                int y = (int)spawnData.card.GridPos.y;
                Card c;
                var a = new Vector2(IsPlayerOwned ? 1000 : -1000, y);
                //Debug.Log("Y:" + y);
                for (int i = 0; i < g.MapX; i++)
                {
                    int j = IsPlayerOwned ? i : (g.MapX - 1) - i;
                    c = (IsPlayerOwned ? g.enemyturf : g.turf)[j, y];
                    if (c != null && !Gamer.Instance.ValidLocation(c) && !c.IsFakeCard)
                    {
                        target_card = (IsPlayerOwned ? g.enemyturf : g.turf)[j, y];
                        a = new Vector2(target_card.Construct.transform.position.x, y);
                        break;
                    }
                }
                targetpos = a;

                break;

        }
        if (!changedam)
        {
            Damage = dam;
        }
    }
    public void Update()
    {
        GetTarget(false);
        int hasreached = 0;
        switch (TargetType)
        {
            case 0:
                if (IsPlayerOwned)
                {
                    if (transform.position.x >= targetpos.x) hasreached = 1;
                    if (transform.position.x >= g.MapX + 3) hasreached = 2;
                }
                else
                {
                    if (transform.position.x <= targetpos.x) hasreached = 1;
                    if (transform.position.x <= -g.MapX - 3) hasreached = 2;
                }
                break;
            case 1:
                foreach (var c in targets)
                {
                    bool e = IsPlayerOwned ? c.Construct.transform.position.x <= transform.position.x : c.Construct.transform.position.x >= transform.position.x;
                    if (!hits.Contains(c) && e && !c.IsFakeCard)
                    {
                        hits.Add(c);
                        Gamer.Instance.DamageFunc(c, Damage);
                        if(spawnData.card.CardType == 26)
                        {
                            Damage /= 2;
                        }
                    }
                }

                if (IsPlayerOwned)
                {
                    if (transform.position.x >= g.MapX + 3) hasreached = 2;
                }
                else
                {
                    if (transform.position.x <= -g.MapX - 3) hasreached = 2;
                }
                break;

        }
        if (hasreached == 0)
        {
            transform.position += new Vector3(IsPlayerOwned ? Speed : -Speed, 0, 0) * Time.deltaTime;
        }
        else
        {
            DieDie(hasreached);
        }
    }

    public void DieDie(int i)
    {
        if (i == 2)
        {
            if (TargetType != 1 || spawnData.card.CardType == 26)
            {
                g.CoreDamageFunc(!IsPlayerOwned, Damage);
            }
        }
        if (i == 1)
        {
            Gamer.Instance.DamageFunc(target_card, Damage);
        }
        Destroy(gameObject);
    }

}

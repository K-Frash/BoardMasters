using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Customization : MonoBehaviour {
    //SpriteMaster_Int
    /* Tengu: 0
        * Gnoll: 1
        * Warlock:2
        * Warrior: 4
        * Rogue: 5
        * Mage: 6   */

    Sprite[] spritesheet;
    public int currentColourIDT1, currentClassIDT1, currentColourIDT2, currentClassIDT2,currentColourIDT3,currentClassIDT3;
    public int T2currentColourIDT1, T2currentClassIDT1, T2currentColourIDT2, T2currentClassIDT2, T2currentColourIDT3, T2currentClassIDT3;
    Image representation1;
    Image representation2;
    Image representation3;

    public string T1;
    public string T2;
    public string T3;
    public string T4;
    public string T5;
    public string T6;


    void Start()
    {
        T1 = "SpriteMaster_40";
        T2 = "SpriteMaster_50";
        T3 = "SpriteMaster_60";
        T4 = "SpriteMaster_10";
        T5 = "SpriteMaster_00";
        T6 = "SpriteMaster_20";
        representation1 = GameObject.Find("Image").GetComponent<Image>();
        representation2 = GameObject.Find("Image (1)").GetComponent<Image>();
        representation3 = GameObject.Find("Image (2)").GetComponent<Image>();
        currentColourIDT1 = 0;
        currentClassIDT1 = 4;
        currentColourIDT2 = 0;
        currentClassIDT2 = 5;
        currentColourIDT3 = 0;
        currentClassIDT3 = 6;

        T2currentColourIDT1 = 0;
        T2currentClassIDT1 = 1;
        T2currentColourIDT2 = 0;
        T2currentClassIDT2 = 0;
        T2currentColourIDT3 = 0;
        T2currentClassIDT3 = 2;
        spritesheet = Resources.LoadAll<Sprite>("SpriteMaster");
    }
    
    public void ChangeColourU1()
    {
        if(currentColourIDT1 < 2)
        {
            currentColourIDT1++;
        }else if(currentColourIDT1 == 2)
        {
            currentColourIDT1 = 0;
        }

        foreach(Sprite S in spritesheet)
        {
            if (S.name.Equals("SpriteMaster_" + currentClassIDT1 + currentColourIDT1))
            {
                representation1.sprite = S;
                T1 = "SpriteMaster_" + currentClassIDT1 + currentColourIDT1;
            }
        }

    }

    public void ChangeClassU1()
    {
        if (currentClassIDT1 < 6)
        {
            currentClassIDT1++;
        }
        else if (currentClassIDT1 == 6)
        {
            currentClassIDT1 = 4;
        }

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals("SpriteMaster_" + currentClassIDT1 + currentColourIDT1))
            {
                representation1.sprite = S;
                T1 = "SpriteMaster_" + currentClassIDT1 + currentColourIDT1;
            }
        }
    }

    public void ChangeColourU2()
    {
        if (currentColourIDT2 < 2)
        {
            currentColourIDT2++;
        }
        else if (currentColourIDT2 == 2)
        {
            currentColourIDT2 = 0;
        }

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals("SpriteMaster_" + currentClassIDT2 + currentColourIDT2))
            {
                representation2.sprite = S;
                T2 = "SpriteMaster_" + currentClassIDT2 + currentColourIDT2;
            }
        }

    }

    public void ChangeClassU2()
    {
        if (currentClassIDT2 < 6)
        {
            currentClassIDT2++;
        }
        else if (currentClassIDT2 == 6)
        {
            currentClassIDT2 = 4;
        }

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals("SpriteMaster_" + currentClassIDT2 + currentColourIDT2))
            {
                representation2.sprite = S;
                T2 = "SpriteMaster_" + currentClassIDT2 + currentColourIDT2;
            }
        }
    }

    public void ChangeColourU3()
    {
        if (currentColourIDT3 < 2)
        {
            currentColourIDT3++;
        }
        else if (currentColourIDT3 == 2)
        {
            currentColourIDT3 = 0;
        }

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals("SpriteMaster_" + currentClassIDT3 + currentColourIDT3))
            {
                representation3.sprite = S;
                T3 = "SpriteMaster_" + currentClassIDT3 + currentColourIDT3;
            }
        }

    }

    public void ChangeClassU3()
    {
        if (currentClassIDT3 < 6)
        {
            currentClassIDT3++;
        }
        else if (currentClassIDT3 == 6)
        {
            currentClassIDT3 = 4;
        }

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals("SpriteMaster_" + currentClassIDT3 + currentColourIDT3))
            {
                representation3.sprite = S;
                T3 = "SpriteMaster_" + currentClassIDT3 + currentColourIDT3;
            }
        }
    }

    /////////////// Team Two's Units

    public void T2ChangeColourU1()
    {

        if (T2currentColourIDT1 < 2)
        {
            T2currentColourIDT1++;
        }
        else if (T2currentColourIDT1 == 2)
        {
            T2currentColourIDT1 = 0;
        }

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals("SpriteMaster_" + T2currentClassIDT1 + T2currentColourIDT1))
            {
                representation1.sprite = S;
                T4 = "SpriteMaster_" + T2currentClassIDT1 + T2currentColourIDT1;
            }
        }

    }

    public void T2ChangeClassU1()
    {
        if (T2currentClassIDT1 < 2)
        {
            T2currentClassIDT1++;
        }
        else if (T2currentClassIDT1 == 2)
        {
            T2currentClassIDT1 = 0;
        }

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals("SpriteMaster_" + T2currentClassIDT1 + T2currentColourIDT1))
            {
                representation1.sprite = S;
                T4 = "SpriteMaster_" + T2currentClassIDT1 + T2currentColourIDT1;
            }
        }
    }

    public void T2ChangeColourU2()
    {
        if (currentColourIDT2 < 2)
        {
            T2currentColourIDT2++;
        }
        else if (T2currentColourIDT2 == 2)
        {
            T2currentColourIDT2 = 0;
        }

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals("SpriteMaster_" + T2currentClassIDT2 + T2currentColourIDT2))
            {
                representation2.sprite = S;
                T5 = "SpriteMaster_" + T2currentClassIDT2 + T2currentColourIDT2;
            }
        }

    }

    public void T2ChangeClassU2()
    {
        if (T2currentClassIDT2 < 2)
        {
            T2currentClassIDT2++;
        }
        else if (T2currentClassIDT2 == 2)
        {
            T2currentClassIDT2 = 0;
        }

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals("SpriteMaster_" + T2currentClassIDT2 + T2currentColourIDT2))
            {
                representation2.sprite = S;
                T5 = "SpriteMaster_" + T2currentClassIDT2 + T2currentColourIDT2;
            }
        }
    }

    public void T2ChangeColourU3()
    {
        if (T2currentColourIDT3 < 2)
        {
            T2currentColourIDT3++;
        }
        else if (T2currentColourIDT3 == 2)
        {
            T2currentColourIDT3 = 0;
        }

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals("SpriteMaster_" + T2currentClassIDT3 + T2currentColourIDT3))
            {
                representation3.sprite = S;
                T6 = "SpriteMaster_" + T2currentClassIDT3 + T2currentColourIDT3;
            }
        }

    }

    public void T2ChangeClassU3()
    {
        if (T2currentClassIDT3 < 2)
        {
           T2currentClassIDT3++;
        }
        else if (T2currentClassIDT3 == 2)
        {
            T2currentClassIDT3 = 0;
        }

        foreach (Sprite S in spritesheet)
        {
            if (S.name.Equals("SpriteMaster_" + T2currentClassIDT3 + T2currentColourIDT3))
            {
                representation3.sprite = S;
                T6 = "SpriteMaster_" + T2currentClassIDT3 + T2currentColourIDT3;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSystem : MonoBehaviour
{
    public enum Brick { TwoHit, Single, Multi, Solid, Power, Special, Invisible, Explosive }
    public enum BColor { White, Blue, Grey, Red, Green, Yellow, Orange, Black, Cyan, Clear, Pink, Purple, Teal }
    MeshRenderer meshRend;
    public Brick brickType;
    public BColor brickColor;
    public int brickHealth = 1;
    bool broken = false;
    IEnumerator flashRoutine = null;
    IEnumerator deathRoutine = null;
    Transform thisBrick;
    void Start()
    {
        thisBrick = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SelectBrickType(Brick type)
    {
        switch (type)
        {
            case Brick.Single: 
                {
                    brickHealth = 1;
                    break; 
                }
        }
    }
    //void DamageBrick()
    //{
    //    brickHealth -= 1;
    //    SetRoutine(flashRoutine, Flash());
    //    if(brickHealth <= 0 && !broken)
    //    {
    //        broken = true;
    //        brickHealth = 0;

    //    }

    //}
    public void SetRoutine(IEnumerator routine, IEnumerator enumerator)
    {
        if (routine != null)
            StopCoroutine(routine);
        routine = enumerator;
        StartCoroutine(routine);
    }
    IEnumerator Death()
    {
        Quaternion brick = thisBrick.rotation;
        float val = 0.04f;
        float index = 1;
        Vector3 rotDest = OptimizeSystem.ChangeVector3(45, 0, 45);
        Quaternion rot = Quaternion.Euler(rotDest);
        while (true)
        {
            brick = Quaternion.Lerp(brick, rot, index);
            meshRend.material.color = new Color(meshRend.material.color.r, meshRend.material.color.g, meshRend.material.color.b, Mathf.Lerp(1, 0, index));
            index -= val;
            if (index < 0)
                break;
            yield return OptimizeSystem.EndOfFrame;
        }
        thisBrick.gameObject.SetActive(false);
        yield break;
    }
    IEnumerator Flash()
    {
        yield break;
    }
    void SelectBrickColor(BColor color)
    {
        Color c = Color.white;
        meshRend = thisBrick.GetComponent<MeshRenderer>();
        switch (color)
        {
            case BColor.White: { c = OptimizeSystem.ChangeColor(1, 1, 1, 1); break; }
            case BColor.Black: { c = OptimizeSystem.ChangeColor(0, 0, 0, 1); break; }
            case BColor.Grey: { c = OptimizeSystem.ChangeColor(0.5f, 0.5f, 0.5f, 1); break; }
            case BColor.Clear: { c = OptimizeSystem.ChangeColor(c.r, c.g, c.b, 0); break; }
            case BColor.Red: { c = OptimizeSystem.ChangeColor(1, 0, 0, 1); break; }
            case BColor.Blue: { c = OptimizeSystem.ChangeColor(0, 0, 1, 1); break; }
            case BColor.Cyan: { c = OptimizeSystem.ChangeColor(0, 1, 1, 1); break; }
            case BColor.Green: { c = OptimizeSystem.ChangeColor(0, 1, 0, 1); break; }
            case BColor.Orange: { c = OptimizeSystem.ChangeColor(1, 0.5f, 0, 1); break; }
            case BColor.Yellow: { c = OptimizeSystem.ChangeColor(1, 0.92f, 0.016f, 1); break; }
            case BColor.Pink: { c = OptimizeSystem.ChangeColor(1, 0, 1, 1); break; }
            case BColor.Purple: { c = OptimizeSystem.ChangeColor(0.7f, 0, 1, 1); break; }
        }
        meshRend.material.color = c;
    }

    public void SetupBrick()
    {
        SelectBrickColor(brickColor);
        SelectBrickType(brickType);
        thisBrick.transform.rotation = Quaternion.identity;
    }
}

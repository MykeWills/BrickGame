using System.Collections;
using UnityEngine;

public class BrickSystem : MonoBehaviour
{
    public enum Brick { TwoHit, Single, Multi, Solid, Power, Special, Invisible, Explosive }
    public enum BColor { White, Blue, Grey, Red, Green, Yellow, Orange, Black, Cyan, Clear, Pink, Purple, Teal }

    [SerializeField]
    private Brick brickType;
    [SerializeField]
    private BColor brickColor;
    IEnumerator deathRoutine = null;
    IEnumerator flashRoutine = null;
    private OptSystem optSystem = new OptSystem();
    private MeshRenderer meshRend;
    private Transform thisBrick;
    private int brickHealth = 1;
    private bool broken = false;


   

    private void Start()
    { 
        thisBrick = transform.GetChild(0);
        meshRend = thisBrick.GetComponent<MeshRenderer>();
        SelectBrickType(brickType);
       
    }
  
    private void SelectBrickType(Brick type)
    {
        switch (type)
        {
            case Brick.Single: 
                {
                    brickHealth = 1;
                    break; 
                }
            case Brick.Multi:
                {
                    brickHealth = 5;
                    break;
                }
            case Brick.Invisible:
                {
                    brickColor = BColor.Clear;
                    brickHealth = 2;
                    break;
                }
            case Brick.Power:
                {
                    brickHealth = 1;
                    break;
                }
            case Brick.Special:
                {
                    brickHealth = 5;
                    break;
                }
            case Brick.TwoHit:
                {
                    brickHealth = 2;
                    break;
                }
            case Brick.Solid:
                {
                    brickColor = BColor.Grey;
                    brickHealth = 999;
                    break;
                }
        }
        SelectBrickColor(brickColor);
    }
    void SetRoutine(IEnumerator routine, IEnumerator enumerator)
    {
        if (routine != null)
            StopCoroutine(routine);
        routine = enumerator;
        StartCoroutine(routine);
    }
    public void DamageBrick()
    {
        brickHealth -= 1;

        if(!broken)
            SetRoutine(flashRoutine, Flash());
        if (brickHealth < 1 && !broken)
        {
            PlayerController p = PlayerController.player.GetComponent<PlayerController>();
            p.AddScore(235);
            BoxCollider col = thisBrick.GetComponent<BoxCollider>();
            col.enabled = false;
            broken = true;
            brickHealth = 0;
            SetRoutine(deathRoutine, Death());
        }
    }
    private IEnumerator Death()
    {
        float val = 0.01f;
        float index = 0;
        Vector3 rotDest = optSystem.Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));
        Quaternion rot = Quaternion.Euler(rotDest);
        Vector3 sizeDest = Vector3.zero;
        Vector3 zoomDest = optSystem.Vector3(thisBrick.localPosition.x, thisBrick.localPosition.y, -5);
        while (true)
        {
            thisBrick.localRotation = Quaternion.Lerp(thisBrick.localRotation, rot, index);
            meshRend.material.color = new Color(meshRend.material.color.r, meshRend.material.color.g, meshRend.material.color.b, Mathf.Lerp(1, 0, index));
            thisBrick.localScale = Vector3.Lerp(thisBrick.localScale, sizeDest, index);
            thisBrick.localPosition = Vector3.Lerp(thisBrick.localPosition, zoomDest, index);
            index += val;
            if (index > 1 && thisBrick.localRotation == rot)
                break;
            yield return optSystem.EndOfFrame;
        }
        thisBrick.gameObject.SetActive(false);
        yield break;
    }
    IEnumerator Flash()
    {
        Color flashColor = Color.white;
        Color orgBrickColor = meshRend.material.color;
        for(float c = 0; c < 1; c+= 0.01f)
        {
            meshRend.material.color = Color.Lerp(flashColor, meshRend.material.color, c);
            if (c > 0.99)
            {
                meshRend.material.color = orgBrickColor;
            }
        }
        yield break;
    }
    private void SelectBrickColor(BColor color)
    {
        Color c = Color.white;

        ParticleSystemRenderer ps = GetComponent<ParticleSystemRenderer>();


        switch (color)
        {
            case BColor.White: { c = optSystem.Color(1, 1, 1, 1); break; }
            case BColor.Black: { c = optSystem.Color(0, 0, 0, 1); break; }
            case BColor.Grey: { c = optSystem.Color(0.5f, 0.5f, 0.5f, 1); break; }
            case BColor.Clear: { c = optSystem.Color(c.r, c.g, c.b, 0); break; }
            case BColor.Red: { c = optSystem.Color(1, 0, 0, 1); break; }
            case BColor.Blue: { c = optSystem.Color(0, 0, 1, 1); break; }
            case BColor.Cyan: { c = optSystem.Color(0, 1, 1, 1); break; }
            case BColor.Green: { c = optSystem.Color(0, 1, 0, 1); break; }
            case BColor.Orange: { c = optSystem.Color(1, 0.5f, 0, 1); break; }
            case BColor.Yellow: { c = optSystem.Color(1, 0.92f, 0.016f, 1); break; }
            case BColor.Pink: { c = optSystem.Color(1, 0, 1, 1); break; }
            case BColor.Purple: { c = optSystem.Color(0.7f, 0, 1, 1); break; }
        }
        meshRend.material.SetColor("_Color", c);
        meshRend.material.EnableKeyword("Emission");
        meshRend.material.SetColor("_EmissionColor", c * 1);
        ps.material.SetColor("_Color", c);
        ps.material.EnableKeyword("Emission");
        ps.material.SetColor("_EmissionColor", c * 4);
    }
    public void SetupBrick()
    {
        SelectBrickColor(brickColor);
        SelectBrickType(brickType);
        thisBrick.transform.rotation = Quaternion.identity;
    }
   
}

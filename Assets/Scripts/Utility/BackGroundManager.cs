using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] private GameObject BackGround;
    [SerializeField] private Transform RespawnPoint, Target;
    [SerializeField] private float fSpeed = 0.5f;

    private List<GameObject> BackGroundPanels = new List<GameObject>();

    private float _fDistance;
    // Start is called before the first frame update
    void Awake()
    {


        //cache the size of a background panel
        _fDistance = BackGround.gameObject.transform.lossyScale.y;


        //set the two panels 
        for (int i = 0; i < 2; i++)
        {
            BackGroundPanels.Add(Instantiate(BackGround));
        }

        //set the first panel to the respawn point
        BackGroundPanels[0].transform.position = RespawnPoint.position;


    }

    // Update is called once per frame
    void Update()
    {
        //lerp both BackGround down
        foreach (GameObject s in BackGroundPanels)
        {
            s.transform.position = Vector2.Lerp(
                s.transform.position,
                new Vector2(0, s.transform.position.y -_fDistance), 
                Time.deltaTime * fSpeed);

            if((int)s.transform.position.y == (int)Target.position.y)
            {
                
                s.transform.position = RespawnPoint.position;
            }
        }

        
        //when a background panel reaches a object called target, set posision to respawnpoint
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float fSpeed = 1.5f;
    [SerializeField] float fRotAdd = 0f;
    [SerializeField] float fRotSpeed = 0.5f;

    [SerializeField] bool bShouldLookAtMouse = false;
    [SerializeField] bool bShouldmove = true;
    [SerializeField] bool bShouldClamp = true;
    Vector2 TranslatedMouseVector;
    private Rigidbody2D _rb;
    [SerializeField] Vector2 _Dir = new Vector2();
    [SerializeField] Transform body;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (bShouldmove)
            MovePlayer();
        //turn the player
        if (bShouldLookAtMouse)
        {

            //-.5f because center of veiwport is(.5,.5)
            float fRot = MathF.Atan2(
            (TranslatedMouseVector.y - .5f),
            (TranslatedMouseVector.x - .5f));



            body.rotation = Quaternion.Lerp(body.rotation, Quaternion.Euler(0, 0, (fRot * Mathf.Rad2Deg) + fRotAdd), Time.time * fRotSpeed);

        }

    }

    private void MovePlayer()
    {
        //get input
        float fHorizontal = Input.GetAxis("Horizontal");
        float fVertical = Input.GetAxis("Vertical");


        if (GameManager.state == GameState.TopDownShooter && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += new Vector3(fHorizontal * fSpeed * 2 * Time.deltaTime, fVertical * 2 * fSpeed * Time.deltaTime);

        }
        else
        {
            transform.position += new Vector3(fHorizontal * fSpeed * Time.deltaTime, fVertical * fSpeed * Time.deltaTime);

        }

    }

    private void FixedUpdate()
    {
        //calculate the direction
        Vector3 worldPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        TranslatedMouseVector = new Vector2(worldPosition.x, worldPosition.y);
        _Dir = (TranslatedMouseVector - new Vector2(transform.position.x, transform.position.y));
        _Dir.Normalize();

        //handle input
        _Dir *= Input.GetAxis("Vertical");

        //make framerate independant
        _Dir *= Time.deltaTime;


        //clamp the players position to the bounds of the screen
        if (bShouldClamp)
        {
            Vector2 pos = transform.position;

            pos.x = Mathf.Clamp(transform.position.x, -2, 2);
            pos.y = Mathf.Clamp(transform.position.y, -4, 4);

            transform.position = pos;

        }


    }
}

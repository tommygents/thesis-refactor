using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; private set;}

//The events that the input manager will raise
public event Action<InputAction.CallbackContext> OnPushStart;
public event Action<InputAction.CallbackContext> OnPushEnd;
public event Action<InputAction.CallbackContext> OnPull;
public event Action<InputAction.CallbackContext> OnPullStart;
public event Action<InputAction.CallbackContext> OnPullEnd;
public event Action<InputAction.CallbackContext> OnSquat;
public event Action<InputAction.CallbackContext> OnSquatStart;
public event Action<InputAction.CallbackContext> OnSquatEnd;
public event Action<InputAction.CallbackContext> OnRunStart;
public event Action<InputAction.CallbackContext> OnRunEnd;
public event Action<InputAction.CallbackContext> OnSprintStart;
public event Action<InputAction.CallbackContext> OnSprintEnd;
public event Action<InputAction.CallbackContext> OnMove;


public Vector2 moveVector = Vector2.zero;
private bool invertYaxis = false;
private ControlScheme controls;

    private void SubscribeToMovementEvents()
    {
        controls.gameplay.Move.performed += ctx => OnMove?.Invoke(ctx);
        controls.gameplay.Move.canceled += ctx => OnMove?.Invoke(ctx);
    }



    void OnAwake()    

    { //first, initialize the instance as a singleton
                if (Instance == null)
                {
                    Instance = this;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }


 // then, initialize the control scheme
 controls = new ControlScheme();
 controls.gameplay.Enable();

 //subscribe to the events
 SubscribeToEvents();




    }

                                                                                            
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetMoveVector()
    {
     moveVector = controls.gameplay.move.ReadValue<Vector2>();
     if (invertYaxis)
     {
        moveVector.y = -moveVector.y;
    }
    return moveVector;
    }

    public void SetInvertYAxis(bool value)
    {
        invertYaxis = value;
    }

    private void SubscribeToEvents()
{
    controls.gameplay.lightpush.started += ctx => OnPushStart?.Invoke(ctx);
    controls.gameplay.lightpush.canceled += ctx => OnPushEnd?.Invoke(ctx);
    controls.gameplay.lightpush.performed += ctx => OnPush?.Invoke(ctx);

    controls.gameplay.heavypush.started += ctx => OnPushStart?.Invoke(ctx);
    controls.gameplay.heavypush.canceled += ctx => OnPushEnd?.Invoke(ctx);
    controls.gameplay.heavypush.performed += ctx => OnPush?.Invoke(ctx);

    controls.gameplay.lightpull.started += ctx => OnPullStart?.Invoke(ctx);
    controls.gameplay.lightpull.canceled += ctx => OnPullEnd?.Invoke(ctx);
    controls.gameplay.lightpull.performed += ctx => OnPull?.Invoke(ctx);

    controls.gameplay.heavypull.started += ctx => OnPullStart?.Invoke(ctx);
    controls.gameplay.heavypull.canceled += ctx => OnPullEnd?.Invoke(ctx);
    controls.gameplay.heavypull.performed += ctx => OnPull?.Invoke(ctx);

    controls.gameplay.Squat.started += ctx => OnSquatStart?.Invoke(ctx);
    controls.gameplay.Squat.canceled += ctx => OnSquatEnd?.Invoke(ctx);
    controls.gameplay.Squat.performed += ctx => OnSquat?.Invoke(ctx);

    controls.gameplay.Run.started += ctx => OnRunStart?.Invoke(ctx);
    controls.gameplay.Run.canceled += ctx => OnRunEnd?.Invoke(ctx);

    controls.gameplay.Sprint.started += ctx => OnSprintStart?.Invoke(ctx);
    controls.gameplay.Sprint.canceled += ctx => OnSprintEnd?.Invoke(ctx);

    controls.gameplay.move.performed += ctx => OnMove?.Invoke(ctx);
    controls.gameplay.move.canceled += ctx => OnMove?.Invoke(ctx);
}


}

using UnityEngine;
using UnityEngine.InputSystem;

public class Flipper : MonoBehaviour
{
    [Header("Flipper Physics")]
    [SerializeField] private float hitStrength = 80000f;
    [SerializeField] private float dampening = 250f;

    [Header("Hinge Joints")]
    [SerializeField] private HingeJoint hingeJointLeft;
    [SerializeField] private HingeJoint hingeJointRight;

    private InputAction leftFlipperAction;
    private InputAction rightFlipperAction;

    private JointSpring springLeftPressed;
    private JointSpring springLeftReleased;
    private JointSpring springRightPressed;
    private JointSpring springRightReleased;

    private bool leftFlipperPressed;
    private bool rightFlipperPressed;

    // ---------------------------------------------------------------
    // Inicialización: springs + bindings en código (sin asset ni PlayerInput)
    // ---------------------------------------------------------------
    private void Awake()
    {
        // --- Springs ---
        springLeftPressed = new JointSpring
        {
            spring = hitStrength,
            damper = dampening,
            targetPosition = hingeJointLeft.limits.max
        };
        springLeftReleased = new JointSpring
        {
            spring = hitStrength,
            damper = dampening,
            targetPosition = hingeJointLeft.limits.min
        };
        springRightPressed = new JointSpring
        {
            spring = hitStrength,
            damper = dampening,
            targetPosition = hingeJointRight.limits.max
        };
        springRightReleased = new JointSpring
        {
            spring = hitStrength,
            damper = dampening,
            targetPosition = hingeJointRight.limits.min
        };

        // --- Flipper izquierdo: Z / Left Shift  |  Mando: LB / LT ---
        leftFlipperAction = new InputAction("LeftFlipper", InputActionType.Button);
        leftFlipperAction.AddBinding("<Keyboard>/z");
        leftFlipperAction.AddBinding("<Keyboard>/leftShift");
        leftFlipperAction.AddBinding("<Gamepad>/leftShoulder");
        leftFlipperAction.AddBinding("<Gamepad>/leftTrigger");

        // --- Flipper derecho: X / Right Shift  |  Mando: RB / RT ---
        rightFlipperAction = new InputAction("RightFlipper", InputActionType.Button);
        rightFlipperAction.AddBinding("<Keyboard>/x");
        rightFlipperAction.AddBinding("<Keyboard>/rightShift");
        rightFlipperAction.AddBinding("<Gamepad>/rightShoulder");
        rightFlipperAction.AddBinding("<Gamepad>/rightTrigger");
    }

    // ---------------------------------------------------------------
    // Suscripción / desuscripción (evita fugas de memoria)
    // ---------------------------------------------------------------
    private void OnEnable()
    {
        leftFlipperAction.performed += OnLeftPressed;
        leftFlipperAction.canceled += OnLeftReleased;
        leftFlipperAction.Enable();

        rightFlipperAction.performed += OnRightPressed;
        rightFlipperAction.canceled += OnRightReleased;
        rightFlipperAction.Enable();
    }

    private void OnDisable()
    {
        leftFlipperAction.performed -= OnLeftPressed;
        leftFlipperAction.canceled -= OnLeftReleased;
        leftFlipperAction.Disable();

        rightFlipperAction.performed -= OnRightPressed;
        rightFlipperAction.canceled -= OnRightReleased;
        rightFlipperAction.Disable();
    }

    private void OnDestroy()
    {
        leftFlipperAction.Dispose();
        rightFlipperAction.Dispose();
    }

    // ---------------------------------------------------------------
    // Callbacks de input
    // ---------------------------------------------------------------
    private void OnLeftPressed(InputAction.CallbackContext _) => leftFlipperPressed = true;
    private void OnLeftReleased(InputAction.CallbackContext _) => leftFlipperPressed = false;
    private void OnRightPressed(InputAction.CallbackContext _) => rightFlipperPressed = true;
    private void OnRightReleased(InputAction.CallbackContext _) => rightFlipperPressed = false;

    // ---------------------------------------------------------------
    // Aplicar springs cada frame
    // ---------------------------------------------------------------
    private void Update()
    {
        hingeJointLeft.spring = leftFlipperPressed ? springLeftPressed : springLeftReleased;
        hingeJointRight.spring = rightFlipperPressed ? springRightPressed : springRightReleased;
    }
}

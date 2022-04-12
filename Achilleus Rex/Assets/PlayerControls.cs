// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""38c4c389-e57d-4aa9-b070-0d30035cc592"",
            ""actions"": [
                {
                    ""name"": ""LockOn"",
                    ""type"": ""Button"",
                    ""id"": ""c2df27e6-ed6f-46d5-955d-3ed81569a9f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lock On Target Left"",
                    ""type"": ""Button"",
                    ""id"": ""a8a1a49a-eec7-4ff6-9551-0fd415e75a44"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lock On Target Right"",
                    ""type"": ""Button"",
                    ""id"": ""af013397-62c2-4149-b585-f409727a4154"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""85e9b56e-8b4c-4856-9d99-7c98ee9d79e8"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9653b500-42bf-44e1-9e98-2f69ef59521d"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4896ba64-3c60-401d-a772-e9d75d0ef4c7"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": ""Tap(duration=0.2,pressPoint=1)"",
                    ""processors"": ""AxisDeadzone(min=0.1,max=1)"",
                    ""groups"": """",
                    ""action"": ""Lock On Target Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6096161-5d6b-4a23-918b-ea2c6eb92a43"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Target Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aacded95-8d5b-4fe8-8ef7-012c84b8c02c"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": ""Tap(duration=0.2,pressPoint=1)"",
                    ""processors"": ""AxisDeadzone(min=0.1,max=1)"",
                    ""groups"": """",
                    ""action"": ""Lock On Target Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b904862-f238-49bc-b891-357ed7e32222"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""Lock On Target Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""New control scheme"",
            ""bindingGroup"": ""New control scheme"",
            ""devices"": []
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_LockOn = m_Gameplay.FindAction("LockOn", throwIfNotFound: true);
        m_Gameplay_LockOnTargetLeft = m_Gameplay.FindAction("Lock On Target Left", throwIfNotFound: true);
        m_Gameplay_LockOnTargetRight = m_Gameplay.FindAction("Lock On Target Right", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_LockOn;
    private readonly InputAction m_Gameplay_LockOnTargetLeft;
    private readonly InputAction m_Gameplay_LockOnTargetRight;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LockOn => m_Wrapper.m_Gameplay_LockOn;
        public InputAction @LockOnTargetLeft => m_Wrapper.m_Gameplay_LockOnTargetLeft;
        public InputAction @LockOnTargetRight => m_Wrapper.m_Gameplay_LockOnTargetRight;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @LockOn.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLockOn;
                @LockOn.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLockOn;
                @LockOn.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLockOn;
                @LockOnTargetLeft.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLockOnTargetLeft;
                @LockOnTargetLeft.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLockOnTargetLeft;
                @LockOnTargetLeft.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLockOnTargetLeft;
                @LockOnTargetRight.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLockOnTargetRight;
                @LockOnTargetRight.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLockOnTargetRight;
                @LockOnTargetRight.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLockOnTargetRight;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LockOn.started += instance.OnLockOn;
                @LockOn.performed += instance.OnLockOn;
                @LockOn.canceled += instance.OnLockOn;
                @LockOnTargetLeft.started += instance.OnLockOnTargetLeft;
                @LockOnTargetLeft.performed += instance.OnLockOnTargetLeft;
                @LockOnTargetLeft.canceled += instance.OnLockOnTargetLeft;
                @LockOnTargetRight.started += instance.OnLockOnTargetRight;
                @LockOnTargetRight.performed += instance.OnLockOnTargetRight;
                @LockOnTargetRight.canceled += instance.OnLockOnTargetRight;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_NewcontrolschemeSchemeIndex = -1;
    public InputControlScheme NewcontrolschemeScheme
    {
        get
        {
            if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.FindControlSchemeIndex("New control scheme");
            return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnLockOn(InputAction.CallbackContext context);
        void OnLockOnTargetLeft(InputAction.CallbackContext context);
        void OnLockOnTargetRight(InputAction.CallbackContext context);
    }
}

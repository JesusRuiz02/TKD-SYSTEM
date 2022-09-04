//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputActions/TableManager.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @TableManager : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @TableManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TableManager"",
    ""maps"": [
        {
            ""name"": ""Table"",
            ""id"": ""ba0aeadf-0206-41cc-b8e7-b80f3a537489"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""dd8caab6-d1f6-45ab-9a24-977dd7ee90c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AddRedGamJeom"",
                    ""type"": ""Button"",
                    ""id"": ""0a993950-032b-4a1b-ad34-c3a410c4e7b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AddBlueGamJeom"",
                    ""type"": ""Button"",
                    ""id"": ""0322a939-bee0-49c4-8ac0-7d306e7dbe05"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d09c6eb7-6a30-4f42-beac-9e9a606b642b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4e1ccbc-e0db-4f3b-ad1b-5558ab26fe03"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""AddRedGamJeom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65469194-46d1-4011-8b4d-27e4370d2601"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""AddBlueGamJeom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Table
        m_Table = asset.FindActionMap("Table", throwIfNotFound: true);
        m_Table_Pause = m_Table.FindAction("Pause", throwIfNotFound: true);
        m_Table_AddRedGamJeom = m_Table.FindAction("AddRedGamJeom", throwIfNotFound: true);
        m_Table_AddBlueGamJeom = m_Table.FindAction("AddBlueGamJeom", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Table
    private readonly InputActionMap m_Table;
    private ITableActions m_TableActionsCallbackInterface;
    private readonly InputAction m_Table_Pause;
    private readonly InputAction m_Table_AddRedGamJeom;
    private readonly InputAction m_Table_AddBlueGamJeom;
    public struct TableActions
    {
        private @TableManager m_Wrapper;
        public TableActions(@TableManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Table_Pause;
        public InputAction @AddRedGamJeom => m_Wrapper.m_Table_AddRedGamJeom;
        public InputAction @AddBlueGamJeom => m_Wrapper.m_Table_AddBlueGamJeom;
        public InputActionMap Get() { return m_Wrapper.m_Table; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TableActions set) { return set.Get(); }
        public void SetCallbacks(ITableActions instance)
        {
            if (m_Wrapper.m_TableActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_TableActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_TableActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_TableActionsCallbackInterface.OnPause;
                @AddRedGamJeom.started -= m_Wrapper.m_TableActionsCallbackInterface.OnAddRedGamJeom;
                @AddRedGamJeom.performed -= m_Wrapper.m_TableActionsCallbackInterface.OnAddRedGamJeom;
                @AddRedGamJeom.canceled -= m_Wrapper.m_TableActionsCallbackInterface.OnAddRedGamJeom;
                @AddBlueGamJeom.started -= m_Wrapper.m_TableActionsCallbackInterface.OnAddBlueGamJeom;
                @AddBlueGamJeom.performed -= m_Wrapper.m_TableActionsCallbackInterface.OnAddBlueGamJeom;
                @AddBlueGamJeom.canceled -= m_Wrapper.m_TableActionsCallbackInterface.OnAddBlueGamJeom;
            }
            m_Wrapper.m_TableActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @AddRedGamJeom.started += instance.OnAddRedGamJeom;
                @AddRedGamJeom.performed += instance.OnAddRedGamJeom;
                @AddRedGamJeom.canceled += instance.OnAddRedGamJeom;
                @AddBlueGamJeom.started += instance.OnAddBlueGamJeom;
                @AddBlueGamJeom.performed += instance.OnAddBlueGamJeom;
                @AddBlueGamJeom.canceled += instance.OnAddBlueGamJeom;
            }
        }
    }
    public TableActions @Table => new TableActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface ITableActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnAddRedGamJeom(InputAction.CallbackContext context);
        void OnAddBlueGamJeom(InputAction.CallbackContext context);
    }
}
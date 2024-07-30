using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
        private static StarterAssetsInputs _instance;

        public static StarterAssetsInputs Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<StarterAssetsInputs>();

                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject(typeof(StarterAssetsInputs).Name);
                        _instance = singletonObject.AddComponent<StarterAssetsInputs>();
                    }
                }
                return _instance;
            }
        }

        [Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
        public bool primary;
        public bool secondary;
        public bool zoom;
        public bool inventory;
        public bool interact;

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

        public bool canMove = true;
        public bool canLook = true;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void SetCanMove(bool value)
        {
            canMove = value;
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

        public void OnPrimary(InputValue value)
        {
            PrimaryInput(value.isPressed);
        }

        public void OnSecondary(InputValue value)
        {
            SecondaryInput(value.isPressed);
        }

        public void OnZoom(InputValue value)
        {
            ZoomInput(value.isPressed);
        }

        public void OnInventory(InputValue value)
        {
            InventoryInput(value.isPressed);
        }

        public void OnInteract(InputValue value)
        {
            InteractInput(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		public void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

        private void PrimaryInput(bool newPrimaryState)
        {
            primary = newPrimaryState;
        }

        private void SecondaryInput(bool newSecondaryState)
        {
            secondary = newSecondaryState;
        }

        private void ZoomInput(bool newZoomState)
        {
            zoom = newZoomState;
        }

        private void InventoryInput(bool newInventoryState)
        {
            inventory = newInventoryState;
        }

        private void InteractInput(bool newState)
        {
            interact = newState;
        }
    }
	
}
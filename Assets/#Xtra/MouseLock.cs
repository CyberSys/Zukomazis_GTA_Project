using UnityEngine;


    public class MouseLock : MonoBehaviour
    {
        public GameObject Inventory;

        private bool _isLocked = true;

        private void LateUpdate()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Inventory.SetActive(true);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().enabled = false;
                gameObject.GetComponent<OrbitCamera>().enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
                _isLocked = false;
            


            if (Input.GetMouseButtonDown(1))
                _isLocked = true;

            if (_isLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
 }

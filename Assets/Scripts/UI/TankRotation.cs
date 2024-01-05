using UnityEngine;

namespace UI
{
    public class TankRotation : MonoBehaviour
    {
        private float angleOld;
        private RaycastHit hit;
        private Vector3 mousePos;
        private Vector3 getRotation;

        private void Update()
        {
            mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit) && hit.transform.name == "Tank_Model")
                {
                    getRotation = transform.eulerAngles;
                    angleOld = Angle();
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit) && hit.transform.name == "Tank_Model")
                {
                    float changeRotation = getRotation.y + (Angle() - angleOld);
                    transform.rotation =
                        Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, changeRotation, 0), 1);
                }
            }
        }

        private float Angle()
        {
            Vector3 pos = new Vector3(mousePos.x, mousePos.y, Camera.main.WorldToScreenPoint(hit.transform.position).z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
            float angle = Vector3.SignedAngle(worldPos, Vector3.left, Vector3.up);
            return angle;
        }
    }
}
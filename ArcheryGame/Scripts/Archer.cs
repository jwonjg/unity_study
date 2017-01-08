using UnityEngine;
using System.Collections;

public class Archer : MonoBehaviour {

    public int rotateAngle;
    public float arrowForce;

    GameObject arrowObj = null;
	GameObject bowObj = null;
	bool action = false;

    private Vector3 pivotVector;
    private Vector3 bowAngle;
    private bool isBowUp = true;


    // Use this for initialization
    void Start () {
		
		bowObj = GameObject.Find("Bow").gameObject;

        pivotVector.x = bowObj.transform.position.x - bowObj.transform.localScale.x;
        pivotVector.y = bowObj.transform.position.y;

        // 화살 게임 오브젝트를 가져오고, 화면에 표시되지 않도록 함
        arrowObj = GameObject.Find("Arrow").gameObject;
		arrowObj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
			if (collition2d) {
				if (collition2d.gameObject == gameObject) {
					// 액션 유효화
					action = true;
				}
			}
			// 버튼 누른 상태인지 확인
			if (action) {
                // TODO: 활 각도 회전
                bowAngle = bowObj.transform.rotation.eulerAngles;
                if (bowAngle.z >= 45 && bowAngle.z <= 90)
                {
                    isBowUp = false;
                } else if(bowAngle.z <= 315 && bowAngle.z >= 300)
                {
                    isBowUp = true;
                }
                if(isBowUp)
                {
                    bowObj.transform.RotateAround(pivotVector, Vector3.forward, rotateAngle * Time.deltaTime);
                } else
                {
                    bowObj.transform.RotateAround(pivotVector, Vector3.back, rotateAngle * Time.deltaTime);
                }
			}
		} else if(Input.GetMouseButtonUp(0) && action) {
			// 화살 발사
			if(arrowObj) {
				GameObject arrowClone = Instantiate(arrowObj) as GameObject;
				arrowClone.SetActive(true);
                arrowClone.GetComponent<Rigidbody2D>().MoveRotation(bowAngle.z);
                arrowClone.GetComponent<Rigidbody2D>().AddForce((Vector2)(Quaternion.Euler(0, 0, bowAngle.z) * Vector2.right) * arrowForce);
                Destroy(arrowClone.gameObject, 3.0f);
			}
			action = false;
		}
	}

}

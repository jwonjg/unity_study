using UnityEngine;
using System.Collections;

public class Archer : MonoBehaviour {

	GameObject arrowObj = null;
	GameObject bowObj = null;
	bool action = false;

	// Use this for initialization
	void Start () {
		
		bowObj = GameObject.Find("Bow").gameObject;
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
				bowObj.transform.Rotate(Vector3.forward);

			}
		} else if(Input.GetMouseButtonUp(0) && action) {
			// 화살 발사
			if(arrowObj) {
				GameObject arrowClone = Instantiate(arrowObj) as GameObject;
				arrowClone.SetActive(true);
				arrowClone.rigidbody2D.AddForce(new Vector2(+300.0f, 200.0f));
				Destroy(arrowClone.gameObject, 3.0f);
			}
			action = false;
		}
	}

}

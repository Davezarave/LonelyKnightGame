using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColourMatch : MonoBehaviour {

	public SpriteRenderer playersr;
	public SpriteRenderer doorsr;
	[SerializeField] private GameObject _this;


	void Update()
	{

		if (playersr.color == Color.red)
			_this.SetActive(false);
            //Destroy(col.gameObject);
		return;
	}
}

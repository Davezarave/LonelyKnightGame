using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Playercolourforwheel : MonoBehaviour {

	public Rigidbody2D rb;
	private Rigidbody2D body;
	public SpriteRenderer sr;

	public string currentColor;
	[SerializeField] private Transform player;

	/*public Color colorCyan;
    public Color colorYellow;
    public Color colorMagenta;
    public Color colorPink;
    public Color colorWhite;*/

	void Start ()
	{
		currentColor = "White";
		sr.color = Color.white;
	}
	

	void OnTriggerEnter2D (Collider2D col)
	{

		/*if (col.tag != currentColor & currentColor != "White")
			player.position = new Vector3(player.position.x - 2, player.position.y, player.position.z);*/

		SetColor(col.tag);

		//Destroy(col.gameObject);
		return;
	}

	void SetColor (string colour)
	{

		if (colour == "Cyan")
		{
			currentColor = "Cyan";
			sr.color = new Color(0, 1, 1, 1);
		}
		else if (colour == "Yellow")
		{
			currentColor = "Yellow";
			sr.color = new Color(1, 1, 0, 1);
		}
		else if (colour == "Magenta")
		{
			currentColor = "Magenta";
			sr.color = new Color(1, 0, 1, 1);
		}
		else if (colour == "Pink")
		{
			currentColor = "Pink";
			sr.color = new Color(1, 0, 0, 1);
		}
		return;
	}


}

using CoinFlipGames.FSM;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExampleGameManager : MonoBehaviour
{
	public StateMachine stateMachine;
	public Image currentStateMarker;
	public Image previousStateMarker;

	public Image startRunningIndicator;
	public Image p1RunningIndicator;
	public Image p2RunningIndicator;
	public Image winRunningIndicator;
	public Image loseRunningIndicator;

	private enum States
	{
		Start,
		P1Turn,
		P2Turn,
		Win,
		Lose
	};

	void Start ()
	{
		stateMachine.AddState (
			States.Start, 
			(previous) => { StartCoroutine (MoveCurrentStateMarker (-400)); },
			() => { startRunningIndicator.color = Color.green; },
			(next) => { startRunningIndicator.color = Color.gray; StartCoroutine (MovePreviousStateMarker (-400)); }
		);

		stateMachine.AddState (
			States.P1Turn, 
			(previous) => { StartCoroutine (MoveCurrentStateMarker (-200)); },
			() => { p1RunningIndicator.color = Color.green; },
			(next) => { p1RunningIndicator.color = Color.gray; StartCoroutine (MovePreviousStateMarker (-200)); }
		);

		stateMachine.AddState (
			States.P2Turn, 
			(previous) => { StartCoroutine (MoveCurrentStateMarker (0)); },
			() => { p2RunningIndicator.color = Color.green; },
			(next) => { p2RunningIndicator.color = Color.gray; StartCoroutine (MovePreviousStateMarker (0)); }
		);

		stateMachine.AddState (
			States.Win, 
			(previous) => { StartCoroutine (MoveCurrentStateMarker (200)); },
			() => { winRunningIndicator.color = Color.green; },
			(next) => { winRunningIndicator.color = Color.gray; StartCoroutine (MovePreviousStateMarker (200)); }
		);

		stateMachine.AddState (
			States.Lose, 
			(previous) => { StartCoroutine (MoveCurrentStateMarker (400)); },
			() => { loseRunningIndicator.color = Color.green; },
			(next) => { loseRunningIndicator.color = Color.gray; StartCoroutine (MovePreviousStateMarker (400)); }
		);
	}

	public void SwitchState (string state)
	{
		switch (state) {
			case "Start":
				stateMachine.Switch (States.Start);
				break;
			case "Player 1 Turn":
				stateMachine.Switch (States.P1Turn);
				break;
			case "Player 2 Turn":
				stateMachine.Switch (States.P2Turn);
				break;
			case "Win":
				stateMachine.Switch (States.Win);
				break;
			case "Lose":
				stateMachine.Switch (States.Lose);
				break;
		}
	}

	IEnumerator MoveCurrentStateMarker (float destination)
	{
		var animSpeed = 5f;
		float progress = 0.0f;

		Vector2 position = currentStateMarker.GetComponent<RectTransform> ().localPosition;

		while (progress < 1.0f)
		{
			currentStateMarker.GetComponent<RectTransform> ().localPosition = Vector2.Lerp(position, new Vector2 (destination, 100), progress);
			yield return new WaitForEndOfFrame();
			progress += Time.deltaTime * animSpeed;
		}

		currentStateMarker.GetComponent<RectTransform> ().localPosition = new Vector2 (destination, 100);
	}

	IEnumerator MovePreviousStateMarker (float destination)
	{
		var animSpeed = 5f;
		float progress = 0.0f;
		
		Vector2 position = previousStateMarker.GetComponent<RectTransform> ().localPosition;
		
		while (progress < 1.0f)
		{
			previousStateMarker.GetComponent<RectTransform> ().localPosition = Vector2.Lerp(position, new Vector2 (destination, -100), progress);
			yield return new WaitForEndOfFrame();
			progress += Time.deltaTime * animSpeed;
		}

		previousStateMarker.GetComponent<RectTransform> ().localPosition = new Vector2 (destination, -100);
	}
}

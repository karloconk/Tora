using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectNode : MonoBehaviour {

	// private static GameManager GameManager.Instance;

	static int currentPlayer = 0;
	public static int spacesLeft;
	int typeOfSpace;

	public bool isMoving = false;

	public static string[] innDialogue, hospitalDialogue;

	// Use this for initialization
	// void Start () {
	// 	GameManager.Instance = GameManager.Instance;
	// }

	void Awake () {
		// GameManager.Instance = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		if (Dice.finishedBouncing) {
			if (spacesLeft == 0) {
				HideSpacesLeftBtn();
				ShowConfirmSpaceBtn();
				if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Return)) {
					LoadSpecialEventProxy();
				} else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Backspace)) {
					MoveBackCurrentPlayer();
				}
			} else {
				MoveForwardCurrentPlayer();
			}
		} else if (SpecialGameEvents.innInteraction){
			if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Return)) {
				RestAtTheInn();
			} else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Backspace)) {
				NoVacancy();
			}
		} else if (SpecialGameEvents.hospitalInteraction){
			if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Return)) {
				UseHospital();
			} else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Backspace)) {
				RefuseHospital();
			}
		}
	}

	void ShowConfirmSpaceBtn(){
		GameObject confirmSpaceBtn = GameObject.FindGameObjectWithTag("confirmSpace");
		if(confirmSpaceBtn){
			for (int i = 0; i < confirmSpaceBtn.transform.childCount; i++){
				confirmSpaceBtn.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
	}

	void ShowConfirmStayNightBtn(){
		GameObject spendNightBtn = GameObject.FindGameObjectWithTag("spendNight");
		if(spendNightBtn){
			for (int i = 0; i < spendNightBtn.transform.childCount; i++){
				spendNightBtn.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
	}

	void ShowConfirmHealthCareBtn(){
		GameObject healthCareBtn = GameObject.FindGameObjectWithTag("healthCare");
		if(healthCareBtn){
			for (int i = 0; i < healthCareBtn.transform.childCount; i++){
				healthCareBtn.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
	}

	void HideConfirmSpaceBtn(){
		GameObject confirmSpaceBtn = GameObject.FindGameObjectWithTag("confirmSpace");
		GameObject spendNightBtn = GameObject.FindGameObjectWithTag("spendNight");
		GameObject healthCareBtn = GameObject.FindGameObjectWithTag("healthCare");
		if(confirmSpaceBtn){
			for (int i = 0; i < confirmSpaceBtn.transform.childCount; i++){
				confirmSpaceBtn.transform.GetChild(i).gameObject.SetActive(false);
				spendNightBtn.transform.GetChild(i).gameObject.SetActive(false);
				healthCareBtn.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	public static void ShowSpacesLeftBtn(){
		GameObject dialogText = GameObject.FindGameObjectWithTag("spacesLeft");
		if (dialogText){
			for (int i = 0; i < dialogText.transform.childCount; i++){
				dialogText.transform.GetChild(i).gameObject.SetActive(true);
			}
			UpdateSpacesLeft();
		}
	}
	void HideSpacesLeftBtn(){
		GameObject dialogText = GameObject.FindGameObjectWithTag("spacesLeft");
		if (dialogText){
			for (int i = 0; i < dialogText.transform.childCount; i++){
				dialogText.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	public static void UpdateSpacesLeft(){
        GameObject dialogText = GameObject.FindGameObjectWithTag("spacesLeft");
		if (dialogText){
			string spacesLeft = dialogText.transform.GetChild(1).gameObject.GetComponent<Text>().text;
			dialogText.transform.GetChild(1).gameObject.GetComponent<Text>().text = spacesLeft.Remove(spacesLeft.Length - 1) + SelectNode.spacesLeft;
		}
    }

	public void LoadSpecialEventProxy(){
		// LookAt.CoupleCameras();
		StartCoroutine(LoadSpecialEvent());
        //LoadFightScene();
		//LoadEspecialEvent();
	}

	public IEnumerator LoadSpecialEvent(){
		
		//se obtiene el jugador para saber cuál mover
		currentPlayer = GameManager.Instance.GetCurrentPlayer();
		GameObject characters = GameObject.FindGameObjectWithTag("Characters");
		if (characters){
			Saviour script = characters.transform.GetChild(currentPlayer-1).GetComponent<Saviour>();
			if (script.gameObject != this.gameObject){
				yield break;
			}
			yield return null;

			// En este punto ya se sabe que hay solo una instancia de SelectNode activa
			// Las demás instancias ya se "salieron" debido al yield break;

			HideConfirmSpaceBtn();

			//Pelea:0,Tienda:1,Cofre:2,Hospital:3,Inn:4,Fábrica:5
			string character = GameManager.Instance.idCharacter[currentPlayer];
			GameObject currentCharacter = GameObject.Find(character);
			if (currentCharacter){
				Saviour saviour = currentCharacter.GetComponent<Saviour>();
				
				GameManager.Instance.currentSaviour = saviour;
				if (saviour) {
					typeOfSpace = NodesMap.nodesArray[saviour.currentNode, 4];
					
					// This is ok, and tested. Do not move or change.
					PlayerUber player = GameManager.Instance.GetPlayerUber();

					switch (typeOfSpace) {
						case 0:
							// StartCoroutine(LoadNextPlayer());

							ResetPila(saviour);
							// ActivateCharacters(false);
							LoadFightScene(saviour, player);
							break;
						case 1:
							ResetPila(saviour);
							ActivateCharacters(false);
							LoadStoreScene(saviour, player);
							break;
						case 2:
							ResetPila(saviour);
							GiveMeSomeMoney(saviour, player);
							break;
						case 3:
							GiveMeHealthCare(saviour, player);
							break;
						case 4:
							GiveMeSomeRest(saviour, player);
							break;
						case 5:
							ResetPila(saviour);
							ActivateCharacters(false);
							LoadFactoryScene(saviour, player);
							break;
						default:
							break;
					}
				}
			}
			
		}		
	}

	public void LoadFightScene(Saviour saviour, PlayerUber player){
		int cp = PlayerUber.normalizeCurrentPlayer(currentPlayer);

		// Guarda el número de jugador
		StatusMaker sm = new StatusMaker(cp, 1, 0);
		sm.MakeAndPostJSONFight();
		
		sm.SetPlayer(cp, player);
		GameManager.Instance.currentSaviour = saviour;

		Dice.finishedBouncing = false;
		GameManager.Instance.BanderaYaSeDecidioCurrentPlayer = false;
		GameManager.Instance.changedScene = true;
		bool firstTimeTutorial = GameManager.Instance.firstTimeTutorial[GameManager.Instance.GetCurrentPlayer()-1];
		if (firstTimeTutorial){
			// Este jugador ya vio por primera vez el tutorial de la pelea
			GameManager.Instance.firstTimeTutorial[GameManager.Instance.GetCurrentPlayer()-1] = false;
			GameManager.Instance.NombreNivelQueSeVaCargar = "Tutorial";
        	SceneManager.LoadScene("PantallaCargandoLoadingScreen");
		} else{
			GameManager.Instance.NombreNivelQueSeVaCargar = "FightScene";
        	SceneManager.LoadScene("PantallaCargandoLoadingScreen");
		}
    }

	public static IEnumerator LoadNextPlayer(){
		// En el caso de que regrese de la pelea o de la tienda,
		// vuelve a prender a los objetos 
		SelectNode.ActivateCharacters(true);

		if (!GameManager.Instance.samePlayer){
            // No acabamos de mostrar la escena del cambio de fase de la luna
            // Cargar al siguiente personaje
			GameManager.Instance.nextPlayer();
		} else {
			// Cargar al mismo personaje, pues mostramos la escena de la luna azul
			// resetear la variable para que al siguiente turno cargue al otro jugador
			GameManager.Instance.samePlayer = false;
		}
		currentPlayer = GameManager.Instance.GetCurrentPlayer();

		string character = GameManager.Instance.idCharacter[currentPlayer];
		string characterArm = character.ToLower()+"Arm";

		LookAt.LookAtNextCharacter(character, characterArm);

		SceneManager.LoadScene ( "Dice", LoadSceneMode.Additive);
		yield return null;
		yield return null;

		GameObject currentCharacter = GameObject.Find(character);
		if (currentCharacter){
			Saviour saviour = currentCharacter.GetComponent<Saviour>();
			if (saviour) {
				// int area = NodesMap.nodesArea["Node "+saviour.currentNode];
				// Debug.Log(NodesMap.areaName[area]);
				GameObject node = GameObject.Find("Node "+saviour.currentNode);
				if (node){
					GameObject dice = GameObject.FindGameObjectWithTag("Dice");
					GameObject box = GameObject.FindGameObjectWithTag("Box");
					GameObject sparks = GameObject.FindGameObjectWithTag("Sparks");
					if (dice && box && sparks){
						dice.transform.position = node.transform.position;
						dice.transform.Translate(0, 3.23f, 0, Space.World);
						sparks.transform.position = node.transform.position;
						sparks.transform.Translate(0, 3.23f, 0, Space.World);
						box.transform.position = node.transform.position;
						box.transform.Translate(0, 2.4f, 0, Space.World);
					}
				}
			}
		}
	}

	public void MoveForwardCurrentPlayer(){
		PlayerUber player = GameManager.Instance.GetPlayerUber();
		Saviour saviour = GameManager.Instance.GetCurrentSaviour();
		if (player.onDuty){
			// este jugador debe seguir peleando
			ResetPila(saviour);
			ActivateCharacters(false);
			LoadFightScene(saviour, player);
		}
		if (player.turnsToSkip == 0){
			//se obtiene el jugador para saber cuál mover
			currentPlayer = GameManager.Instance.GetCurrentPlayer();
			GameObject characters = GameObject.FindGameObjectWithTag("Characters");
			if (characters){
				Saviour script = characters.transform.GetChild(currentPlayer-1).GetComponent<Saviour>();
				if (script.gameObject == this.gameObject){
					GetMovement(script);
				}
			}
		}
		// Quiere decir que este jugador debe pasar un turno, pues perdió en batalla
		else{
			if (isMoving){
				return;
			}
			isMoving = true;
			StartCoroutine(NotYetYourTurn(player));
		}
	}

	public IEnumerator NotYetYourTurn(PlayerUber player){
		Saviour saviour = GameManager.Instance.GetCurrentSaviour();
		if (saviour.gameObject != this.gameObject){
			yield break;
		}
		yield return null;

		// En este punto ya se sabe que hay solo una instancia de SelectNode activa
		// Las demás instancias ya se "salieron" debido al yield break;	

		GameObject stayTurn = GameObject.FindGameObjectWithTag("stayTurn");
		if (stayTurn){
			// Para evitar que regrese a este mismo punto y se cicle al mismo personaje
			Dice.finishedBouncing = false;

			GameObject timeline = stayTurn.transform.GetChild(2).gameObject;
			GameObject dialogText = stayTurn.transform.GetChild(1).gameObject;
			dialogText.GetComponent<Text>().text = 
				GameManager.Instance.orderedPlayers[0] + " is still recovering from that horrid fight.";
			timeline.SetActive(true);

			player.turnsToSkip --;
			saviour.turnsToSkip --;
			StatusMaker sm = new StatusMaker();
			sm.SetPlayer(PlayerUber.normalizeCurrentPlayer(currentPlayer), player);
			
			HideSpacesLeftBtn();
			isMoving = false;
			StartCoroutine(LoadNextPlayer());
		}
	}

	public void LoadStoreScene(Saviour saviour, PlayerUber player){

		// currentplayer, area, typeOfFight = 3 (STORE)
		StatusMaker sm = new StatusMaker(currentPlayer, 1, 3);
		sm.MakeAndPostJSONFight();
		sm.SetPlayer(PlayerUber.normalizeCurrentPlayer(currentPlayer), player);
		GameManager.Instance.currentSaviour = saviour;

		Dice.finishedBouncing = false;
		GameManager.Instance.changedScene = true;
		GameManager.Instance.BanderaYaSeDecidioCurrentPlayer = false;
        GameManager.Instance.NombreNivelQueSeVaCargar = "Store";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
	}

	public void GiveMeSomeMoney(Saviour saviour, PlayerUber player){
		GameObject node = GameObject.Find("Node "+saviour.currentNode);
		if (node){
			GameObject coins = GameObject.FindGameObjectWithTag("Coin");
			if (coins){
				coins.transform.position = node.transform.position;
				coins.transform.Translate(0, 1.34f, 0, Space.World);
				SpecialGameEvents specialGameEvents = new SpecialGameEvents();
				string[] text = specialGameEvents.ChestInteraction(1, player, saviour, currentPlayer);
				GameObject coinsGO = coins.transform.GetChild(0).gameObject;
				StartCoroutine(PlayCoinAnimation(coinsGO, text));
			}
		}
		Dice.finishedBouncing = false;
	}

	private IEnumerator PlayCoinAnimation(GameObject coins, string[] text){
		coins.SetActive(true);
		Animator anim = coins.GetComponent<Animator>();
		float counter = 0;
		float waitTime = 1.50f;

		GameObject foundGold = GameObject.FindGameObjectWithTag("foundGold");
		if (foundGold){
			GameObject goldBox = foundGold.transform.GetChild(0).gameObject;
			GameObject textGold = foundGold.transform.GetChild(1).gameObject;
			goldBox.SetActive(true);
			textGold.SetActive(true);
			textGold.GetComponent<Text>().text = text[0] + text[1] + "\n" + text[2];

			while (counter < (waitTime)){
				counter += Time.deltaTime;
				yield return null;
			}

			coins.SetActive(false);
			goldBox.SetActive(false);
			textGold.SetActive(false);
			StartCoroutine(LoadNextPlayer());
		}
		
	}

	public void GiveMeHealthCare(Saviour saviour, PlayerUber player){
		Dice.finishedBouncing = false;
		SpecialGameEvents specialGameEvents = new SpecialGameEvents();
		string[] text = specialGameEvents.HospitalInteraction(false);
		hospitalDialogue = text;
		
		GameObject healthCare = GameObject.FindGameObjectWithTag("healthCare");
		if (healthCare){
			for (int i=0; i<healthCare.transform.childCount; i++){
				healthCare.transform.GetChild(i).gameObject.SetActive(true);
			}
			GameObject textInn = healthCare.transform.GetChild(1).gameObject;
			textInn.GetComponent<Text>().text = hospitalDialogue[0];
		}	
	}

	public void GiveMeSomeRest(Saviour saviour, PlayerUber player){
		Dice.finishedBouncing = false;
		int area = NodesMap.nodesArea["Node " + saviour.currentNode];

		SpecialGameEvents specialGameEvents = new SpecialGameEvents();
		string[] text = specialGameEvents.InnInteraction(area);
		innDialogue = text;

		GameObject spendNight = GameObject.FindGameObjectWithTag("spendNight");
		if (spendNight){
			for (int i=0; i<spendNight.transform.childCount; i++){
				spendNight.transform.GetChild(i).gameObject.SetActive(true);
			}
			GameObject textInn = spendNight.transform.GetChild(1).gameObject;
			textInn.GetComponent<Text>().text = innDialogue[0] + "\n" + innDialogue[1] + innDialogue[2];
		}
	}

	public void LoadFactoryScene(Saviour saviour, PlayerUber player){
		if (FactoryAlreadyDistroyed(saviour)){
			ActivateCharacters(true);
			StartCoroutine(LoadNextPlayer());
		}
		else{
			int cp = PlayerUber.normalizeCurrentPlayer(currentPlayer);
			// Guarda el número de jugador
			StatusMaker sm = new StatusMaker(cp, 1, 1);
			sm.MakeAndPostJSONFight();
			
			sm.SetPlayer(cp, player);
			GameManager.Instance.currentSaviour = saviour;

			Dice.finishedBouncing = false;
			GameManager.Instance.changedScene = true;
			GameManager.Instance.BanderaYaSeDecidioCurrentPlayer = false;

			GameManager.Instance.NombreNivelQueSeVaCargar = "FightScene";
			SceneManager.LoadScene("PantallaCargandoLoadingScreen");
		}
    }

	private bool FactoryAlreadyDistroyed(Saviour saviour){
		try{
			if (GameManager.Instance.bossWinners[saviour.currentNode] != 0){
				return true;
			}
			return false;
		}
		catch (KeyNotFoundException){
			return false;
		}
	}

	public static IEnumerator ChangeNodeMaterial(){
		foreach (KeyValuePair<int, int> entry in GameManager.Instance.bossWinners) {
			if (GameManager.Instance.bossWinners[entry.Key] != 0){
				Debug.Log(entry.Key + " " + entry.Value);
				GameObject node = GameObject.Find("Node "+entry.Key);
				if (node){
					GameObject cilinder = node.transform.GetChild(0).gameObject;
					if (cilinder){
						GameObject material = GameObject.Find("GoldenPlayers");
						if (material){
							GoldenNodes materials = material.GetComponent<GoldenNodes>();
							Material[] mats = cilinder.GetComponent<Renderer>().materials;
							mats[1] = materials.goldenPlayers[entry.Value-1];
							cilinder.GetComponent<Renderer>().materials = mats;
						}
					}
				}
			}
		}
		yield return null;
	}

	public static IEnumerator ChangeFactories(){
		foreach (KeyValuePair<int, int> entry in GameManager.Instance.bossWinners) {
			if (GameManager.Instance.bossWinners[entry.Key] != 0){
				Debug.Log(entry.Key + " " + entry.Value);
				GameObject factory = GameObject.Find("Factory"+entry.Key);
				if (factory){
					factory.transform.GetChild(0).gameObject.SetActive(false);
					factory.transform.GetChild(1).gameObject.SetActive(true);
				}
			}
		}
		yield return null;
	}

	public static IEnumerator ShowBridgesLinks(){
		foreach (KeyValuePair<int, bool> entry in GameManager.Instance.areaCleared) {
			if (GameManager.Instance.areaCleared[entry.Key] == true){
				Debug.Log(entry.Key + " " + entry.Value);
				GameObject bridges = GameObject.Find("Bridges");
				GameObject links = GameObject.Find("BridgeLinks");
				switch (entry.Key){
					case 1:
						bridges.transform.GetChild(0).gameObject.SetActive(true);
						links.transform.GetChild(0).gameObject.SetActive(true);
						break;
					case 2:
						bridges.transform.GetChild(1).gameObject.SetActive(true);
						links.transform.GetChild(0).gameObject.SetActive(true);
						break;
					case 3:
						bridges.transform.GetChild(2).gameObject.SetActive(true);
						links.transform.GetChild(0).gameObject.SetActive(true);
						break;
					case 4:
						break;
					default:
						break;
				}
			}
		}
		yield return null;
	}

	public static IEnumerator SendToHospital(){
		Saviour saviour = GameManager.Instance.GetCurrentSaviour();
		PlayerUber player = GameManager.Instance.GetPlayerUber();
		int hospitalNode = NodesMap.hospitalAreaNode[GameManager.Instance.GetCurrentPlayerArea()];
		saviour.transform.position = NodesMap.nodesPosition[hospitalNode];
		saviour.currentNode = hospitalNode;
		saviour.oldNode = hospitalNode;
		saviour.selectedNode = hospitalNode;
		SpecialGameEvents.RecoverHealth(player, saviour, currentPlayer);
		player.turnsToSkip = 2;
		saviour.turnsToSkip = 2;
		// Se guardan los cambios en el JSON para que puedan ser cargados
		// Siempre que se utilice el GameManager.Instance.GetPlayerUber();
		StatusMaker sm = new StatusMaker();
		sm.SetPlayer(PlayerUber.normalizeCurrentPlayer(currentPlayer), player);
		saviour.AvoidCollision();
        GameManager.Instance.loseFight = false;
		yield return null;
	}
	

	private static void ResetPila(Saviour saviour){
		saviour.pila.Clear();
		saviour.oldNode = saviour.currentNode;
		// Hacer el setup inicial para que no dé la condición de oldNode != currentNode
	}

	/// <summary>
	/// Method to hide or spawn the characters when another scene is Loaded
	/// such as Store or Fight.
	/// </summary>  
	public static void ActivateCharacters(bool show){
		Debug.Log("currentPLay: "+ GameManager.Instance.currentPlayer);
		GameObject characters = GameObject.FindGameObjectWithTag("Characters");
		if (characters){
			// - 1 porque tenemos al ShopKeeper.
			for (int i = 0; i < characters.transform.childCount - 1; i++){
				GameObject spawnedCharacter = characters.transform.GetChild(i).gameObject;
				if(GameManager.Instance.playerDictionary.ContainsValue(i+1)){
					spawnedCharacter.SetActive(show);
				}
			}
		}
	}

	

	public void MoveBackCurrentPlayer(){

		HideConfirmSpaceBtn();
		ShowSpacesLeftBtn();

		//se obtiene el jugador para saber cuál mover
		currentPlayer = GameManager.Instance.GetCurrentPlayer();

		GameObject characters = GameObject.FindGameObjectWithTag("Characters");
		if (characters){
			Saviour script = characters.transform.GetChild(currentPlayer-1).GetComponent<Saviour>();
			if (script.gameObject == this.gameObject){
				script.MoveBack();
			}
		}
	}

	public void UseHospital(){
		HideConfirmHospitalBtn();
		StartCoroutine(DisplayHospitalMsg(hospitalDialogue[1], true));
	}

	public void RefuseHospital(){
		HideConfirmHospitalBtn();
		StartCoroutine(DisplayHospitalMsg(hospitalDialogue[2], false));
	}

	/// <summary>
	/// Displays the corresponding message depending on the player choice
	/// of spending the night in the Inn or going one space back.
	/// /<sumamary> 
	public IEnumerator DisplayHospitalMsg(string message, bool stay){
		Saviour saviour = GameManager.Instance.GetCurrentSaviour();
		PlayerUber player = GameManager.Instance.GetPlayerUber();

		if (saviour.gameObject != this.gameObject){
			yield break;
		}
		yield return null;

		// En este punto ya se sabe que hay solo una instancia de SelectNode activa
		// Las demás instancias ya se "salieron" debido al yield break;

		GameObject useHospital = GameObject.FindGameObjectWithTag("healthCare");
		if (useHospital){
			GameObject textInn = useHospital.transform.GetChild(1).gameObject;
			textInn.GetComponent<Text>().text = message;
		}

		float counter = 0;
		float waitTime = 1.50f;

		while (counter < (waitTime)){
			counter += Time.deltaTime;
			yield return null;
		}

		if (useHospital){
			useHospital.transform.GetChild(0).gameObject.SetActive(false);
			useHospital.transform.GetChild(1).gameObject.SetActive(false);
		}

		SpecialGameEvents.hospitalInteraction = false;

		if (stay){
	        SpecialGameEvents.RecoverHealth(player, saviour, currentPlayer);
		}
		ResetPila(GameManager.Instance.currentSaviour);
		StartCoroutine(LoadNextPlayer());
	}

	public void RestAtTheInn(){
		HideConfirmNightBtn();
		StartCoroutine(DisplayInnMsg(innDialogue[3], true));
	}

	public void NoVacancy(){
		HideConfirmNightBtn();
		StartCoroutine(DisplayInnMsg(innDialogue[4], false));
	}

	/// <summary>
	/// Checks if the current saviour has enough money for staying in the Inn
	/// If he does, then his wealth is decreased by _nightCost_
	/// If he doesn't, then he shall return one space.
	/// /<sumamary> 
	public bool HasEnoughMoney(int nightCost){
		return GameManager.Instance.GetCurrentSaviour().money>=nightCost;
	}

	/// <summary>
	/// Displays the corresponding message depending on the player choice
	/// of spending the night in the Inn or going one space back.
	/// /<sumamary> 
	public IEnumerator DisplayInnMsg(string message, bool stay){
		Saviour saviour = GameManager.Instance.GetCurrentSaviour();
		PlayerUber player = GameManager.Instance.GetPlayerUber();

		if (saviour.gameObject != this.gameObject){
			yield break;
		}
		yield return null;

		// En este punto ya se sabe que hay solo una instancia de SelectNode activa
		// Las demás instancias ya se "salieron" debido al yield break;

		if (stay && !HasEnoughMoney(SpecialGameEvents.nightCost)){
			message = innDialogue[5];
			stay = false;
		}

		GameObject spendNight = GameObject.FindGameObjectWithTag("spendNight");
		if (spendNight){
			GameObject textInn = spendNight.transform.GetChild(1).gameObject;
			textInn.GetComponent<Text>().text = message;
		}

		float counter = 0;
		float waitTime = 1.50f;

		while (counter < (waitTime)){
			counter += Time.deltaTime;
			yield return null;
		}

		if (spendNight){
			spendNight.transform.GetChild(0).gameObject.SetActive(false);
			spendNight.transform.GetChild(1).gameObject.SetActive(false);
		}

		SpecialGameEvents.innInteraction = false;

		if (stay){
			saviour.money -= SpecialGameEvents.nightCost;
			player.money -= SpecialGameEvents.nightCost;
	        SpecialGameEvents.RecoverHealth(player, saviour, currentPlayer);
			ResetPila(GameManager.Instance.currentSaviour);
			StartCoroutine(LoadNextPlayer());
		}
		else{
			Dice.finishedBouncing = true;
			MoveBackCurrentPlayer();
		}
	}

	public void HideConfirmNightBtn(){
		GameObject spendNight = GameObject.FindGameObjectWithTag("spendNight");
		if (spendNight){
			spendNight.transform.GetChild(2).gameObject.SetActive(false);
			spendNight.transform.GetChild(3).gameObject.SetActive(false);
		}
	}

	public void HideConfirmHospitalBtn(){
		GameObject healthCare = GameObject.FindGameObjectWithTag("healthCare");
		if (healthCare){
			healthCare.transform.GetChild(2).gameObject.SetActive(false);
			healthCare.transform.GetChild(3).gameObject.SetActive(false);
		}
	}

	void OnEnable(){
		ShowConfirmSpaceBtn();
		ShowConfirmStayNightBtn();
		ShowConfirmHealthCareBtn();
		
		GameObject moveBackBtn = GameObject.FindGameObjectWithTag("moveBack");
		if(moveBackBtn){
			Button btn = moveBackBtn.GetComponent<Button>();
			btn.onClick.AddListener(MoveBackCurrentPlayer);
		}

		GameObject stayHereBtn = GameObject.FindGameObjectWithTag("stayHere");
		if(stayHereBtn){
			Button btn = stayHereBtn.GetComponent<Button>();
			btn.onClick.AddListener(LoadSpecialEventProxy);
		}

		GameObject stayNightkBtn = GameObject.FindGameObjectWithTag("stayNight");
		if(stayNightkBtn){
			Button btn = stayNightkBtn.GetComponent<Button>();
			btn.onClick.AddListener(RestAtTheInn);
		}

		GameObject refuseNightBtn = GameObject.FindGameObjectWithTag("refuseNight");
		if(refuseNightBtn){
			Button btn = refuseNightBtn.GetComponent<Button>();
			btn.onClick.AddListener(NoVacancy);
		}

		GameObject useHospitalBtn = GameObject.FindGameObjectWithTag("useHospital");
		if(useHospitalBtn){
			Button btn = useHospitalBtn.GetComponent<Button>();
			btn.onClick.AddListener(UseHospital);
		}

		GameObject refuseHospitalBtn = GameObject.FindGameObjectWithTag("refuseHospital");
		if(refuseHospitalBtn){
			Button btn = refuseHospitalBtn.GetComponent<Button>();
			btn.onClick.AddListener(RefuseHospital);
		}

		HideConfirmSpaceBtn();
	}

	void OnDisable(){
		//ShowConfirmSpaceBtn();

		// GameObject moveBackBtn = GameObject.FindGameObjectWithTag("moveBack");
		// if(moveBackBtn){
		// 	Button btn = moveBackBtn.GetComponent<Button>();
		// 	btn.onClick.RemoveListener(MoveBackCurrentPlayer);
		// }

		// GameObject stayHereBtn = GameObject.FindGameObjectWithTag("stayHere");
		// if(stayHereBtn){
		// 	Button btn = stayHereBtn.GetComponent<Button>();
		// 	btn.onClick.RemoveListener(LoadSpecialEventProxy);
		// }

		// HideConfirmSpaceBtn();
	}

	private void GetMovement(Saviour saviour) {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			if (MoveSucceded(saviour, 0)) {
				saviour.Walk(saviour.currentNode, 0);
			}
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			if (MoveSucceded(saviour, 1)) {
				saviour.Walk(saviour.currentNode, 1);
			}
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			if (MoveSucceded(saviour, 2)) {
				saviour.Walk(saviour.currentNode, 2);
			}
		} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			if (MoveSucceded(saviour, 3)) {
				saviour.Walk(saviour.currentNode, 3);
			}
		}
	}

	private bool MoveSucceded(Saviour saviour, int i) {
		int current_node = saviour.currentNode;
		return NodesMap.nodesArray[current_node, i] != 0;
	}
}

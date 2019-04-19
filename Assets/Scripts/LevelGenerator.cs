using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour {
	/*
	* Variables públicas de nuestro generador de niveles
	*/
	public static LevelGenerator sharedInstance; // Instancia compartida para solo tener un generador de niveles

	public List<LevelBlock>
		allLevelBlocks = new List<LevelBlock>(); // Lista que contiene todos los niveles que se han creado

	public List<LevelBlock>
		currentLevelBlocks = new List<LevelBlock>(); // Lista de los bloques que tenemos ahora mismo en pantalla

	public Transform levelInitialPosition; // Punto inicializa donde empezará a crearse el primer nivel de todos
	private Boolean isGeneratingInitialBlocks = false;

	private void Awake() {
		sharedInstance = this;
	}

	// Start is called before the first frame update
	void Start() {
		GenerateInitialBlocks(3);
	}

	// Update is called once per frame
	void Update() { }

	public void AddNewBlock() {
		// Seleccionar un bloque aleatorio entre los que tenemos disponibles
		int randomIndex = Random.Range(0, allLevelBlocks.Count);

		if (isGeneratingInitialBlocks) {
			randomIndex = 0;
		}
		// Se crea una copia de uno de los niveles que ya existen y se asigna a la variable 'block'. 
		LevelBlock block = (LevelBlock) Instantiate(allLevelBlocks[randomIndex]);
		block.transform.SetParent(this.transform, false); // El padre del bloque siempre es él mismo

		// Posicion del bloque
		Vector3 blockPosition = Vector3.zero;

		if (currentLevelBlocks.Count == 0) {
			// Vamos a colocar el primer bloque en pantalla
			blockPosition = levelInitialPosition.position;
		}
		else {
			// Ya tengo bloques en pantalla y lo empalmo al último disponible.
			blockPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;
		}

		block.transform.position = blockPosition;
		currentLevelBlocks.Add(block); // Se añade a la lista de bloques 
	}

	public void RemoveOldBlock() {
		LevelBlock block = currentLevelBlocks[0]; // El bloque más antiguo es siempre el primero de la lista
		currentLevelBlocks.Remove(block);
		Destroy(block.gameObject);
	}

	public void GenerateInitialBlocks(int blocksNumber) {
		isGeneratingInitialBlocks = true;
		for (int i = 0; i < blocksNumber; i++) {
			AddNewBlock();
		}

		isGeneratingInitialBlocks = false;
	}

	public void RemoveAllBlocks() {
		while (currentLevelBlocks.Count > 0) {
			RemoveOldBlock();
		}
	}
}
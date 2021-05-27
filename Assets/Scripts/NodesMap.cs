﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NodesMap : MonoBehaviour {

    //(Valores de Nodos en el quinto Valor)
    //(0:Pelea,1:Tienda,2:Cofre,3:Hospital,4:Inn,5:Fabrica)
	public static int[,] nodesArray = new int[125, 5] {
		{1, 0, 1, 1, 0 },
		{2, 0, 0, 0, 0 },
		{3, 1, 0, 0, 0 },
		{4, 2, 0, 0, 0 },
		{5, 3, 0, 0, 0 },
		{10, 4, 6, 7, 0 },
		{0, 0, 0, 5, 2 },
		{8, 0, 5, 0, 1 },
		{9, 7, 0, 9, 0 },
		{0, 8, 8, 0, 2 },
		{0, 5, 11, 13, 0 },
		{12, 10, 0, 13, 4 },
		{17, 11, 0, 14, 0 },
		{14, 10, 11, 0, 0 },
		{0, 13, 12, 15, 0 },
		{19, 0, 14, 21, 0 },
		{18, 0, 0, 0, 2 },
		{18, 12, 0, 18, 0 },
		{0, 16, 17, 19, 0 },
		{0, 15, 18, 20, 0 },
		{0, 0, 19, 22, 0 },
		{22, 0, 15, 0, 0 },
		{23, 21, 20, 25, 0 },
		{0, 22, 24, 0, 3 },
		{0, 0, 0, 23, 0 },
		{0, 0, 22, 26, 5 },
		{27, 124, 25, 0, 0 },
		{29, 26, 28, 0, 0 },
		{0, 27, 0, 29, 2 },
		{0, 27, 28, 30, 0 },
		{0, 0, 29, 31, 0 },
		{0, 32, 30, 49, 0 },
		{31, 0, 33, 48, 0 },
		{0, 34, 0, 32, 0 },
		{33, 42, 35, 0, 0 },
		{124, 36, 36, 34, 0 },
		{0, 37, 0, 35, 0 },
		{36, 39, 38, 0, 0 },
		{37, 0, 0, 39, 2 },
		{37, 0, 38, 40, 0 },
		{0, 0, 39, 41, 4 },
		{42, 0, 40, 46, 0 },
		{34, 41, 0, 43, 0 },
		{44, 46, 42, 45, 0 },
		{0, 43, 0, 0, 5 },
		{47, 0, 43, 0, 2 },
		{43, 0, 41, 57, 0 },
		{48, 45, 0, 55, 0 },
		{0, 47, 32, 0, 0 },
		{0, 0, 31, 50, 0 },
		{51, 0, 49, 53, 0 },
		{0, 50, 0, 52, 1 },
		{0, 53, 51, 0, 0 },
		{52, 54, 50, 54, 0 },
		{53, 0, 55, 0, 2 },
		{0, 56, 47, 54, 0 },
		{55, 58, 57, 0, 0 },
		{56, 0, 46, 59, 0 },
		{56, 59, 0, 0, 0 },
		{58, 60, 57, 0, 3 },
		{59, 61, 0, 0, 0 },
		{60, 62, 0, 62, 0 },
		{61, 63, 61, 63, 0 },
		{84, 64, 62, 0, 0 },
		{63, 66, 0, 65, 0 },
		{64, 0, 66, 0, 3 },
		{64, 0, 67, 65, 0 },
		{70, 0, 68, 66, 2 },
		{69, 0, 0, 67, 0 },
		{0, 68, 0, 0, 1 },
		{72, 67, 73, 0, 0 },
		{0, 0, 72, 0, 2 },
		{74, 70, 74, 71, 0 },
		{74, 0, 76, 70, 0 },
		{75, 73, 75, 72, 0 },
		{0, 76, 77, 74, 4 },
		{75, 0, 78, 73, 0 },
		{0, 78, 79, 75, 0 },
		{77, 0, 83, 76, 2 },
		{0, 0, 80, 77, 0 },
		{0, 0, 81, 79, 0 },
		{80, 82, 82, 0, 5 },
		{0, 0, 81, 83, 0 },
		{0, 0, 82, 78, 0 },
		{0, 63, 85, 111, 0 },
		{86, 84, 0, 84, 4 },
		{87, 85, 0, 0, 0 },
		{112, 86, 88, 109, 0 },
		{113, 89, 93, 87, 0 },
		{88, 90, 92, 0, 0 },
		{89, 0, 91, 0, 0 },
		{92, 0, 0, 90, 1 },
		{93, 91, 0, 89, 3 },
		{116, 92, 94, 88, 0 },
		{0, 0, 0, 93, 2 },
		{96, 0, 0, 117, 0 },
		{97, 95, 0, 118, 2 },
		{98, 96, 0, 121, 0 },
		{99, 97, 0, 122, 0 },
		{0, 98, 0, 100, 0 },
		{0, 122, 99, 101, 0 },
		{0, 123, 100, 102, 0 },
		{0, 103, 101, 0, 0 },
		{102, 104, 123, 0, 0 },
		{103, 114, 120, 105, 0 },
		{0, 106, 104, 0, 2 },
		{105, 109, 112, 107, 0 },
		{0, 108, 106, 0, 0 },
		{107, 0, 109, 0, 2 },
		{106, 110, 87, 108, 0 },
		{109, 111, 0, 0, 0 },
		{110, 84, 84, 0, 0 },
		{115, 87, 113, 106, 0 },
		{114, 88, 116, 112, 0 },
		{104, 113, 119, 115, 0 },
		{0, 112, 114, 0, 5 },
		{119, 93, 117, 113, 0 },
		{118, 0, 95, 116, 0 },
		{121, 117, 96, 119, 0 },
		{120, 116, 118, 114, 5 },
		{123, 119, 121, 104, 0 },
		{122, 118, 97, 120, 0 },
		{100, 121, 98, 123, 5 },
		{101, 120, 122, 103, 0 },
		{26, 35, 0, 0, 0 },
	};
	
	public static Vector3[] nodesPosition = new Vector3[] {
        new Vector3(0, 0, 0),
        new Vector3(44.9f, -3.3f, 7.0f),
        new Vector3(45.3f, -3.9f, 9.6f),
        new Vector3(48.7f, -5.4f, 13.0f),
        new Vector3(51.5f, -6.3f, 13.4f),
        new Vector3(55.9f, -6.3f, 17.0f),
        new Vector3(60.7f, -6.5f, 12.0f),
        new Vector3(52.5f, -6.4f, 20.8f),
        new Vector3(55.2f, -6.4f, 26.8f),
        new Vector3(54.2f, -6.4f, 33.9f),
        new Vector3(58.7f, -6.3f, 19.7f),
        new Vector3(63.2f, -6.3f, 19.9f),
        new Vector3(66.5f, -6.3f, 23.6f),
        new Vector3(58.7f, -6.3f, 24.2f),
        new Vector3(62.0f, -6.3f, 27.8f),
        new Vector3(58.7f, -6.3f, 34.2f),
        new Vector3(63.2f, -6.3f, 31.0f),
        new Vector3(68.0f, -6.3f, 29.2f),
        new Vector3(66.0f, -6.3f, 34.2f),
        new Vector3(64.0f, -6.3f, 38.9f),
        new Vector3(59.9f, -6.3f, 42.3f),
        new Vector3(53.0f, -6.3f, 39.4f),
        new Vector3(55.9f, -6.3f, 45.5f),
        new Vector3(58.4f, -6.3f, 51.6f),
        new Vector3(63.9f, -6.3f, 44.9f),
        new Vector3(52.6f, -6.3f, 47.1f),
        new Vector3(47.3f, -6.3f, 49.8f),
        new Vector3(50.6f, -6.3f, 55.0f),
        new Vector3(58.6f, -6.3f, 59.2f),
        new Vector3(52.3f, -6.3f, 63.9f),
        new Vector3(44.5f, -6.3f, 65.5f),
        new Vector3(30.8f, -6.3f, 66.4f),
        new Vector3(26.6f, -6.3f, 62.0f),
        new Vector3(31.6f, -6.3f, 54.8f),
        new Vector3(26.7f, -6.3f, 45.8f),
        new Vector3(33.7f, -6.3f, 42.2f),
        new Vector3(35.8f, -6.3f, 32.7f),
        new Vector3(32.0f, -6.3f, 25.3f),
        new Vector3(36.3f, -6.3f, 15.1f),
        new Vector3(28.6f, -6.3f, 19.8f),
        new Vector3(20.1f, -6.3f, 25.1f),
        new Vector3(12.8f, -6.3f, 29.7f),
        new Vector3(19.2f, -6.3f, 36.9f),
        new Vector3(10.0f, -6.3f, 43.1f),
        new Vector3(13.1f, -5.8f, 46.7f),
        new Vector3(5.1f, -6.3f, 46.6f),
        new Vector3(3.2f, -6.3f, 35.0f),
        new Vector3(7.8f, -5.9f, 55.9f),
        new Vector3(16.6f, -6.3f, 63.0f),
        new Vector3(25.2f, -6.3f, 74.9f),
        new Vector3(14.6f, -6.3f, 73.7f),
        new Vector3(14.3f, -6.3f, 82.7f),
        new Vector3(2.4f, -5.9f, 83.9f),
        new Vector3(3.1f, -6.3f, 75.0f),
        new Vector3(-5.0f, -6.3f, 69.8f),
        new Vector3(-0.5f, -6.3f, 60.0f),
        new Vector3(-10.9f, -6.3f, 56.6f),
        new Vector3(-5.0f, -6.3f, 39.9f),
        new Vector3(-14.0f, -6.3f, 51.6f),
        new Vector3(-16.3f, -6.3f, 46.6f),
        new Vector3(-19.1f, -6.3f, 37.6f),
        new Vector3(-30.2f, -6.3f, 32.0f),
        new Vector3(-46.2f, -6.3f, 28.6f),
        new Vector3(-69.0f, -6.3f, 28.9f),
        new Vector3(-68.0f, -6.3f, 22.3f),
        new Vector3(-68.4f, -6.3f, 11.3f),
        new Vector3(-61.3f, -6.3f, 7.7f),
        new Vector3(-48.0f, -6.3f, -1.7f),
        new Vector3(-37.6f, -6.3f, -5.4f),
        new Vector3(-29.3f, -6.3f, -1.2f),
        new Vector3(-42.2f, -6.3f, 5.1f),
        new Vector3(-51.4f, -6.3f, 15.9f),
        new Vector3(-41.0f, -6.3f, 15.9f),
        new Vector3(-28.6f, -6.3f, 11.9f),
        new Vector3(-26.3f, -6.3f, 20.8f),
        new Vector3(-15.3f, -6.3f, 24.5f),
        new Vector3(-15.3f, -6.3f, 15.9f),
        new Vector3(-2.2f, -6.3f, 23.8f),
        new Vector3(-2.9f, -6.3f, 14.5f),
        new Vector3(9.7f, -6.3f, 18.2f),
        new Vector3(16.5f, -6.3f, 14.8f),
        new Vector3(21.4f, -6.3f, 7.3f),
        new Vector3(12.0f, -6.3f, 7.5f),
        new Vector3(4.8f, -6.3f, 11.1f),
        new Vector3(-67.8f, -6.3f, 42.1f),
        new Vector3(-63.7f, -6.2f, 43.8f),
        new Vector3(-59.6f, -6.2f, 52.0f),
        new Vector3(-55.5f, -6.2f, 61.6f),
        new Vector3(-43.6f, -6.2f, 55.9f),
        new Vector3(-45.7f, -6.2f, 51.0f),
        new Vector3(-48.3f, -6.2f, 45.5f),
        new Vector3(-42.5f, -6.2f, 43.2f),
        new Vector3(-39.8f, -6.2f, 48.9f),
        new Vector3(-38.0f, -6.2f, 53.3f),
        new Vector3(-26.0f, -6.2f, 48.7f),
        new Vector3(-20.1f, -6.2f, 52.2f),
        new Vector3(-14.8f, -6.2f, 64.1f),
        new Vector3(-12.4f, -6.2f, 69.7f),
        new Vector3(-7.0f, -6.2f, 81.8f),
        new Vector3(-4.5f, -6.2f, 87.1f),
        new Vector3(-9.8f, -6.2f, 89.8f),
        new Vector3(-19.6f, -6.2f, 94.4f),
        new Vector3(-25.0f, -6.2f, 96.5f),
        new Vector3(-27.7f, -6.2f, 91.7f),
        new Vector3(-33.2f, -6.2f, 79.4f),
        new Vector3(-51.0f, -6.2f, 87.1f),
        new Vector3(-58.7f, -6.2f, 69.6f),
        new Vector3(-65.3f, -6.2f, 72.5f),
        new Vector3(-68.1f, -6.2f, 67.2f),
        new Vector3(-61.4f, -6.2f, 63.8f),
        new Vector3(-65.3f, -6.2f, 54.6f),
        new Vector3(-69.0f, -6.2f, 46.2f),
        new Vector3(-53.0f, -6.2f, 67.0f),
        new Vector3(-41.1f, -6.2f, 61.7f),
        new Vector3(-35.9f, -6.2f, 73.6f),
        new Vector3(-47.7f, -6.2f, 79.2f),
        new Vector3(-35.4f, -6.2f, 59.4f),
        new Vector3(-25.3f, -6.2f, 54.8f),
        new Vector3(-20.2f, -6.2f, 66.5f),
        new Vector3(-30.0f, -6.2f, 71.1f),
        new Vector3(-27.7f, -6.2f, 76.8f),
        new Vector3(-17.7f, -6.2f, 72.5f),
        new Vector3(-12.6f, -6.2f, 84.4f),
        new Vector3(-22.4f, -6.2f, 89.1f),
        new Vector3(43.8f, -6.3f, 45.3f)
    };

	public static Dictionary<string, int> nodesArea = new Dictionary<string, int>
		{
			{ "Node 0", 1 },
			{ "Node 1", 1 },
			{ "Node 2", 1 },
			{ "Node 3", 1 },
			{ "Node 4", 1 },
			{ "Node 5", 1 },
			{ "Node 6", 1 },
			{ "Node 7", 1 },
			{ "Node 8", 1 },
			{ "Node 9", 1 },
			{ "Node 10", 1 },
			{ "Node 11", 1 },
			{ "Node 12", 1 },
			{ "Node 13", 1 },
			{ "Node 14", 1 },
			{ "Node 15", 1 },
			{ "Node 16", 1 },
			{ "Node 17", 1 },
			{ "Node 18", 1 },
			{ "Node 19", 1 },
			{ "Node 20", 1 },
			{ "Node 21", 1 },
			{ "Node 22", 1 },
			{ "Node 23", 1 },
			{ "Node 24", 1 },
			{ "Node 25", 1 },
			{ "Node 26", 2 },
			{ "Node 27", 2 },
			{ "Node 28", 2 },
			{ "Node 29", 2 },
			{ "Node 30", 2 },
			{ "Node 31", 2 },
			{ "Node 32", 2 },
			{ "Node 33", 2 },
			{ "Node 34", 2 },
			{ "Node 35", 2 },
			{ "Node 36", 2 },
			{ "Node 37", 2 },
			{ "Node 38", 2 },
			{ "Node 39", 2 },
			{ "Node 40", 2 },
			{ "Node 41", 2 },
			{ "Node 42", 2 },
			{ "Node 43", 2 },
			{ "Node 44", 2 },
			{ "Node 45", 2 },
			{ "Node 46", 2 },
			{ "Node 47", 2 },
			{ "Node 48", 2 },
			{ "Node 49", 2 },
			{ "Node 50", 2 },
			{ "Node 51", 2 },
			{ "Node 52", 2 },
			{ "Node 53", 2 },
			{ "Node 54", 2 },
			{ "Node 55", 2 },
			{ "Node 56", 2 },
			{ "Node 57", 2 },
			{ "Node 58", 2 },
			{ "Node 59", 2 },
			{ "Node 60", 3 },
			{ "Node 61", 3 },
			{ "Node 62", 3 },
			{ "Node 63", 3 },
			{ "Node 64", 3 },
			{ "Node 65", 3 },
			{ "Node 66", 3 },
			{ "Node 67", 3 },
			{ "Node 68", 3 },
			{ "Node 69", 3 },
			{ "Node 70", 3 },
			{ "Node 71", 3 },
			{ "Node 72", 3 },
			{ "Node 73", 3 },
			{ "Node 74", 3 },
			{ "Node 75", 3 },
			{ "Node 76", 3 },
			{ "Node 77", 3 },
			{ "Node 78", 3 },
			{ "Node 79", 3 },
			{ "Node 80", 3 },
			{ "Node 81", 3 },
			{ "Node 82", 3 },
			{ "Node 83", 3 },
			{ "Node 84", 4 },
			{ "Node 85", 4 },
			{ "Node 86", 4 },
			{ "Node 87", 4 },
			{ "Node 88", 4 },
			{ "Node 89", 4 },
			{ "Node 90", 4 },
			{ "Node 91", 4 },
			{ "Node 92", 4 },
			{ "Node 93", 4 },
			{ "Node 94", 4 },
			{ "Node 95", 4 },
			{ "Node 96", 4 },
			{ "Node 97", 4 },
			{ "Node 98", 4 },
			{ "Node 99", 4 },
			{ "Node 100", 4 },
			{ "Node 101", 4 },
			{ "Node 102", 4 },
			{ "Node 103", 4 },
			{ "Node 104", 4 },
			{ "Node 105", 4 },
			{ "Node 106", 4 },
			{ "Node 107", 4 },
			{ "Node 108", 4 },
			{ "Node 109", 4 },
			{ "Node 110", 4 },
			{ "Node 111", 4 },
			{ "Node 112", 4 },
			{ "Node 113", 4 },
			{ "Node 114", 4 },
			{ "Node 115", 4 },
			{ "Node 116", 4 },
			{ "Node 117", 4 },
			{ "Node 118", 4 },
			{ "Node 119", 4 },
			{ "Node 120", 4 },
			{ "Node 121", 4 },
			{ "Node 122", 4 },
			{ "Node 123", 4 },
			{ "Node 124", 2 }
		};
	public static Dictionary<int, string> areaName = new Dictionary<int, string>
		{
			{ 1, "Dragon Land" },
			{ 2, "Troll Mountains" },
			{ 3, "Underlains" },
			{ 4, "LiftHelm Statal Union" }
		};

	public static Dictionary<int, int> hospitalAreaNode = new Dictionary<int, int>
		{
			{ 1, 23 },
			{ 2, 59 },
			{ 3, 65 },
			{ 4, 92 }
		};

	public static void DisplayCurrentNode(int node){
		int typeOfSpace = nodesArray[node, 4];
		GameObject nodesType = GameObject.FindGameObjectWithTag("nodesType");
		if (nodesType){
			GameObject nodes = nodesType.transform.GetChild(0).gameObject;
			GameObject nodeGO;
			Image img;
			Color tempColor;
			for (int i=1; i<=6; i++){
				nodeGO = nodes.transform.GetChild(i).gameObject;
				img = nodeGO.GetComponent<Image>();
				tempColor = img.color;
				if (i==typeOfSpace+1){
					tempColor.a = 1f;
					img.color = tempColor;
				}else{
					tempColor.a = 0.4901961f;
					img.color = tempColor;
				}
			}
		}
	}

	private static string[] buildingNames1 = new string[] {
		"wall_H10m_L50m (3)",
		"wall_H10m_L50m (4)",
		"sentinel_tower_a (2)",
		"wall_H10m_L50m (6)",
		"sentinel_tower_a (3)",
		"tower_gates",
		"wall_H10m_L50m (7)",
		"wall_H10m_L50m (1)"
	};

	private static string[] buildingNames2 = new string[] {
		"Building_I_1_prefab (3)",
		"Building_I_1_prefab (7)",
		"Building_A1_prefab (4)",
		"Building_A1_prefab (7)",
		"Building_A1_prefab (3)",
		"Building_A1_prefab (8)",
		"Building_I_1_prefab (14)"
	};

	public static void ChangeMaterialAlpha(int selectedNode){
		GameObject conflictBuilding;
		Shader shaderTransparent = Shader.Find("Transparent/VertexLit");
		Shader shaderStandard = Shader.Find("Standard");
		switch (selectedNode){
			case 38: case 39: case 40:
				conflictBuilding = GameObject.Find("wall_H10m_L50m (3)");
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);
				break;
			case 41: case 46:
				conflictBuilding = GameObject.Find("wall_H10m_L50m (4)");
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);
				break;
			case 57:
				conflictBuilding = GameObject.Find("sentinel_tower_a (2)");
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);
				break;
			case 62:
				conflictBuilding = GameObject.Find("wall_H10m_L50m (6)");
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);
				break;
			case 63:
				conflictBuilding = GameObject.Find("sentinel_tower_a (3)");
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);
				break;
			case 70:
				conflictBuilding = GameObject.Find("tower_gates");
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);
				break;
			case 71:
				conflictBuilding = GameObject.Find("wall_H10m_L50m (7)");
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);
				break;
			case 82:
				conflictBuilding = GameObject.Find("wall_H10m_L50m (1)");
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);
				break;
			case 103: case 102:
				conflictBuilding = GameObject.Find("Building_I_1_prefab (3)");
				conflictBuilding = conflictBuilding.transform.GetChild(0).gameObject;
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);

				conflictBuilding = GameObject.Find("Building_I_1_prefab (7)");
				conflictBuilding = conflictBuilding.transform.GetChild(0).gameObject;
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);

				conflictBuilding = GameObject.Find("Building_A1_prefab (4)");
				conflictBuilding = conflictBuilding.transform.GetChild(0).gameObject;
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);

				conflictBuilding = GameObject.Find("Building_A1_prefab (7)");
				conflictBuilding = conflictBuilding.transform.GetChild(0).gameObject;
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);
				break;
			case 105:
				conflictBuilding = GameObject.Find("Building_A1_prefab (3)");
				conflictBuilding = conflictBuilding.transform.GetChild(0).gameObject;
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);

				conflictBuilding = GameObject.Find("Building_A1_prefab (8)");
				conflictBuilding = conflictBuilding.transform.GetChild(0).gameObject;
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);
				break;
			case 107: case 108:
				conflictBuilding = GameObject.Find("Building_I_1_prefab (14)");
				conflictBuilding = conflictBuilding.transform.GetChild(0).gameObject;
				ChangeMaterialAlphaUtil(conflictBuilding, shaderTransparent);
				break;
			default:
				foreach (string buildingName in buildingNames1){
					conflictBuilding = GameObject.Find(buildingName);
					ChangeMaterialAlphaUtil(conflictBuilding, shaderStandard);
				}
				foreach (string buildingName in buildingNames2){
					conflictBuilding = GameObject.Find(buildingName);
					conflictBuilding = conflictBuilding.transform.GetChild(0).gameObject;
					ChangeMaterialAlphaUtil(conflictBuilding, shaderStandard);
				}
				break;
		}
	}

	static void ChangeMaterialAlphaUtil(GameObject conflictBuilding, Shader shaderTransparent){
		Material[] mats = conflictBuilding.GetComponent<Renderer>().materials;
		foreach (Material mat in mats){
			mat.shader = shaderTransparent;
		}
		conflictBuilding.GetComponent<Renderer>().materials = mats;
	}
}

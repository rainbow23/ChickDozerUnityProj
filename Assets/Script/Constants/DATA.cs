using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class DATA
{
	//プレハブのフォルダへのパス
	public const string PREFABS_PATH = "Prefabs/";

	//固定データのパス
	public const string CONSTANTS_PATH = "Assets/Scripts/Constants/";

	//オーディオファイルのパス
	public const string BGM_PATH = "Audio/BGM";
	public const string SE_PATH  = "Audio/SE";


	public const string OBTAINEDCHARKEY =  "obtainedCharArray";

	public const int ResourcesChickNum = 23;
	public static Dictionary<int,int> NextLevelScore = new Dictionary<int,int>()
	{
		{1,10},
		{2, 21},
		{3, 42},
		{4, 87},
		{5, 167},
		{6, 318},
		{7, 448},
		{8, 599},
		{9, 771},
		{10, 964},

		{11,1254},
		{12, 1623},
		{13, 2099},
		{14, 2704},
		{15, 3455},
		{16, 4564},
		{17, 5907},
		{18, 7500},
		{19, 9356},
		{20, 13241},

		{21,17972},
		{22, 23818},
		{23, 31017},
		{24, 39781},
		{25, 50299},
		{26, 62736},
		{27, 77241},
		{28, 98417},
		{29, 155630},
		{30, 224735},

		{31, 308648},
		{32, 410263}
	};
}
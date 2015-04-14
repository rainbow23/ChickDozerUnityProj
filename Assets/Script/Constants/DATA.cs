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
	public const string SCOREKEY =  "Score";
	public const string LEVELKEY =  "Level";
	public const string POINTKEY =  "Point";
	public const string SAVEDFIRSTRUNKEY =  "savedFirstRun";
	public const string NEXTLEVELPERCENTAGEKEY =  "nextLevelPercentage";
	public const int ResourcesChickNum = 32;

	private static int _level = 1;
	public static int Level{
		get{ return _level;}
		set{ _level = value;}
	}

	private static int _score;
	public static int  Score{
		get{return _score;}
		set{ _score = value;}
	}

	private static int _point = 2000;
	public static int  Point{
				get{ return _point;}
				set{ _point = value;}
	}
	public static int  NextLevelPercentage{set; get;}
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
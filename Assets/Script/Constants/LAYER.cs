/// <summary>
/// レイヤー名を定数で管理するクラス
/// </summary>
public static class LAYER
{
	  public const int DEFAULT = 0;
	  public const int TRANSPARENTFX = 1;
	  public const int IGNORERAYCAST = 2;
	  public const int WATER = 4;
	  public const int UI = 5;
	  public const int BOMBS = 8;
	  public const int PLAYER = 9;
	  public const int ENEMIES = 10;
	  public const int PICKUPS = 11;
	  public const int GROUND = 12;
	  public const int BALL = 13;
	  public const int NOHITBALL = 14;
	  public const int POINTCHECK = 15;
	  public const int OBSTACLE = 16;
	  public const int DEFAULT_MASK = 1;
	  public const int TRANSPARENTFX_MASK = 2;
	  public const int IGNORERAYCAST_MASK = 4;
	  public const int WATER_MASK = 16;
	  public const int UI_MASK = 32;
	  public const int BOMBS_MASK = 256;
	  public const int PLAYER_MASK = 512;
	  public const int ENEMIES_MASK = 1024;
	  public const int PICKUPS_MASK = 2048;
	  public const int GROUND_MASK = 4096;
	  public const int BALL_MASK = 8192;
	  public const int NOHITBALL_MASK = 16384;
	  public const int POINTCHECK_MASK = 32768;
	  public const int OBSTACLE_MASK = 65536;
}

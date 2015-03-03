using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// BGMとSEの管理をするマネージャ。シングルトン。
/// </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
	//ボリューム保存用のkeyとデフォルト値
	private const float  BGM_VOLUME_DEFULT = 0.5f;
	private const float  SE_VOLUME_DEFULT  = 1.0f;

	//ミュート中か
	private const string MUTE_KEY    = "BGM_VOLUME_KEY";
	private const int MUTE_OFF = 0;
	private const int MUTE_ON  = 1;
	private const int MUTE_DEFULT = MUTE_OFF;

	private int _isMute = MUTE_DEFULT;
	public  bool IsMute{
		get{return _isMute == MUTE_ON;}
	}

	//BGMがフェードするのにかかる時間
	public const float BGM_FADE_SPEED_RATE_IMMEDIATELY = 10.0f;
	public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
	public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
	private float _bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

	//次流すBGM名、SE名
	private string _nextBGMName;
	private string _nextSEName;

	//BGMをフェードアウト中か
	private bool _isFadeOut = false;

	//BGM用、SE用に分けてオーディオソースを持つ
	public AudioSource AttachBGMSource, AttachSESource;

	//全Audioを保持
	private Dictionary<string, AudioClip> _bgmDic, _seDic;

	//=================================================================================
	//初期化
	//=================================================================================

	private void Awake ()
	{
		_isMute = PlayerPrefs.GetInt(MUTE_KEY);
		//リソースフォルダから全SE&BGMのファイルを読み込みセット
		_bgmDic = new Dictionary<string, AudioClip> ();
		_seDic  = new Dictionary<string, AudioClip> ();

		object[] bgmList = Resources.LoadAll (DATA.BGM_PATH);
		object[] seList  = Resources.LoadAll (DATA.SE_PATH);

		foreach (AudioClip bgm in bgmList) {
			_bgmDic [bgm.name] = bgm;
		}
		foreach (AudioClip se in seList) {
			_seDic [se.name] = se;
		}

		_isMute = PlayerPrefs.GetInt (MUTE_KEY, MUTE_DEFULT);

		AttachBGMSource.volume = BGM_VOLUME_DEFULT;
		AttachSESource.volume  = SE_VOLUME_DEFULT;
	}

	private void Start()
	{
		GameController.createCharNum.AddListener(PlayTouchSE);
	}

	//=================================================================================
	//SE
	//=================================================================================

	/// <summary>
	/// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
	/// </summary>
	public void PlaySE (string seName, float delay = 0.0f)
	{
		if (!_seDic.ContainsKey (seName)) {
			Debug.Log (seName + "という名前のSEがありません");
			return;
		}

		_nextSEName = seName;
		Invoke ("DelayPlaySE", delay);
	}

	public void PlayTouchSE (int touchNum)
	{
		if (!_seDic.ContainsKey (AUDIO.SE_PIYP)) {
			Debug.Log (AUDIO.SE_PIYP + "という名前のSEがありません");
			return;
		}
		_nextSEName = AUDIO.SE_PIYP;
		Invoke ("DelayPlaySE", 0f);
	}

	private void DelayPlaySE ()
	{
		if(_isMute == MUTE_ON){
			return;
		}
		print ("_seDic [_nextSEName]: " + _seDic [_nextSEName]);
		AttachSESource.PlayOneShot (_seDic [_nextSEName] as AudioClip);
	}

	//=================================================================================
	//BGM
	//=================================================================================

	/// <summary>
	/// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
	/// 第二引数のfadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
	/// </summary>
	public void PlayBGM (string bgmName, float fadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH)
	{
		if (!_bgmDic.ContainsKey (bgmName)) {
			Debug.Log (bgmName + "という名前のBGMがありません");
			return;
		}

		if(_isMute == MUTE_ON){
			return;
		}

		//現在BGMが流れていない時はそのまま流す or 即切り替え
		if (!AttachBGMSource.isPlaying || fadeSpeedRate == BGM_FADE_SPEED_RATE_IMMEDIATELY) {
			_nextBGMName = "";
			AttachBGMSource.clip = _bgmDic [bgmName] as AudioClip;
			AttachBGMSource.Play ();
		}
		//違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
		else if (AttachBGMSource.clip.name != bgmName) {
			_nextBGMName = bgmName;
			FadeOutBGM (fadeSpeedRate);
		}

	}


	/// <summary>
	/// BGMを止める
	/// </summary>
	public void StopBGM (){
		AttachBGMSource.Stop ();
	}

	/// <summary>
	/// 現在流れている曲をフェードアウトさせる
	/// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
	/// </summary>
	public void FadeOutBGM (float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
	{
		_bgmFadeSpeedRate = fadeSpeedRate;
		_isFadeOut = true;
	}

	private void Update ()
	{
		if (!_isFadeOut) {
			return;
		}

		//徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
		AttachBGMSource.volume -= Time.deltaTime * _bgmFadeSpeedRate;
		if (AttachBGMSource.volume <= 0) {
			AttachBGMSource.Stop ();
			AttachBGMSource.volume = BGM_VOLUME_DEFULT;
			_isFadeOut = false;

			if (!string.IsNullOrEmpty (_nextBGMName)) {
				PlayBGM (_nextBGMName);
			}
		}

	}

	//=================================================================================
	//音量変更
	//=================================================================================

	/// <summary>
	/// BGMとSEのボリュームを0に
	/// </summary>
	public void SetMute(bool isMute){
		_isMute = isMute ? MUTE_ON : MUTE_OFF;
		PlayerPrefs.SetInt (MUTE_KEY, _isMute);

		if(_isMute == MUTE_ON){
			StopBGM ();
		}
		else{
			if(Application.loadedLevelName == SCENE.TITLE){
				PlayBGM (AUDIO.BGM_TITLE);
			}
			else if(Application.loadedLevelName == SCENE.GAME){
				PlayBGM (AUDIO.BGM_GAME);
			}
			else if(Application.loadedLevelName == SCENE.ENDING){
				PlayBGM (AUDIO.BGM_RESULT);
			}
		}

	}
}
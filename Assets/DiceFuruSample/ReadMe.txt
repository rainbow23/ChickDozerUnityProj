
このサンプルの動作には、NGUIのエクスポートが必須です。
動作確認（NGUI2.6.3）

＊＊＊＊＊＊＊sampleAspect＊＊＊＊＊＊＊
　縦長でもアスペクト比の変化に対応できるようにするサンプル。
　主にiPhone5対応のため。
　
注目するオブジェクト
●CameraMangaer
　使用するカメラのセットが必須。
　以下の三つのパラメーターの範囲でアスペクト比の変化に対応する。
　・defaultAspect    //基本のアスペクト比
　・wideAspect       //最も横長になった場合のアスペクト比
　・nallowAspect     //最も縦長になった場合のアスペクト比 
　ゲーム画面のアスペクト比を固定にしたい場合は全て同じ値にすれば良い。
　アスペクト比の設定値を超える場合は、レターボックス処理（余白を黒で埋める）をする。
●ClearCamera
　バックバッファを黒色でクリアするためだけのカメラ
   depthをちゃんと設定して、他のカメラよりも描画順が先になるように注意
●2D Camera
　ClearCameraがあるので、ClearはNotingでOK
●UIRoot
　　UtageUIRootという、NGUI標準UIRootを継承した独自コンポーネントを使用。
　　通常のUIRootの変わりに使う。
　　CameraMangaerのセットだけ必要。
　　実行時に内部で上書きされるため、ScalingStyleなど他の値は設定は不要。



＊＊＊＊＊＊＊sampleOffsetZ＊＊＊＊＊＊＊
　Z値を一元管理するためのサンプル。
　NGUIの描画順が制御しづらいので、主にそのため。
　NGUIは同じパネルの中では同一マテリアル内でしか描画順を制御できない。
　そして、各マテリアルの描画順はZ値によって制御される。（厳密にいうと、さらにシェーダによっても影響する場合があるが）
　特に日本語環境では、フォントとUI用のマテリアルが分かれがちなので、Z値で描画順を制御する必要がある。
　
注目するオブジェクト
●UIPanel - UIPanel - Front、UIPanel - Back、UILabel
　UtageNguiOffsetZ というコンポネーントをアタッチしてる。
　これは、描画順用のZ値を一元管理して描画順をゲーム全体で管理しやすくしている。
　オブジェクトのZ値を、文字列をキーに設定されたZ値で上書きするという処理。

　各パネルのZ値を「60」,「40」などと設定することで、パネルごとの描画順を制御し、
　パネル内のUIスプライトのZ値は「0」。ラベルのZ値は「-0.1」などとして、同一パネル内の異なるマテリアルの描画順を制御している。
　
　注）このコードでは、キーになる文字と対応するZ値はソースコード内で定義している
　各自の要望に応じて変更・拡張して使うことを推奨。
　（外部データを参照できるほうがスマートだけどやってない）
 
 ＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊
 
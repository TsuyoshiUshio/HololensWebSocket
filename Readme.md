# Hololens で Socket.io に接続する

Hololens で WebSocket を使いたい場合に工夫が必要だったので、うまくいった手順をまとめておきます。

## 1. SocketIoClientDotNet

[SocketIoClientDotNet](https://github.com/Quobject/SocketIoClientDotNet)を使います。このライブラリ
はあくまでUnity用で、ここについているDLLをアセットインポートで試しましたが動作しませんでした。

ちなみにUWPの公式のライブラリ(MessageWebSocket)がありますが、これは動作しませんでした。これは、RFCを読むと、Sec-Websocket-Extensions を有効化する必要が Socket.ioや、ほかの Websocket実装で必要っぽいですが、Sec-Webscket-Protocolヘッダにプロトコルをクライアントでセットして送信する必要がありますが、標準ライブラリはサポートしていません。サポートされると多くの人が救われると思います。

* [MessageWebSocketControl](https://docs.microsoft.com/en-us/uwp/api/windows.networking.sockets.messagewebsocketcontrol.supportedprotocols#Windows_Networking_Sockets_MessageWebSocketControl_SupportedProtocols )

* [How to use Sec-Websocket-Extensions in MessageWebsocket upgrade request](https://stackoverflow.com/questions/41131470/how-to-use-sec-websocket-extensions-in-messagewebsocket-upgrade-request )

## 2. ライブラリの組み込みとデプロイ

SocketIoClient をNuget で読み込むと使えました。ただし、バージョンは、`0.9.13` にしました。最新では動きません。ビルドの方法は、

```
Unity: (build) -> Solution: Nuget インストール, Solution: コメントアウト -> Hololens にビルド/デプロイ
```


このパターンがこのサンプルリポジトリのやり方です。ただし、UWPのアプリのライブラリに
なるので、Unityのビルド時は、このコードは無視する必要があります。最初は `if UNITY_EDITOR`などプリプロセッサを設定したり、[グローバルで`Assets/mcs.rsp` を設定する](https://docs.unity3d.com/ja/current/Manual/PlatformDependentCompilation.html)などの方法も試しましたが、ビルドにはなぜか有効にならず、、、結局、なんと、該当箇所をビルド時にはコメント
アウトするしかないという２日間のハックでは結論です。もっといい方法があれば教えてください。

ちなみにこのサンプルでは、下記の部分の部分をUnityビルド後にコメントアウトしてください。

```
//#if !UNITY_BUILD
//#endif
```

こちらと接続できるサーバーは、こちらのリポジトリのサーバーが使えます。

[floatinghotpot/socket.io-unity](https://github.com/floatinghotpot/socket.io-unity/tree/master/Demo/test-server)

## 3. たまに出るエラー

このエラーに出会って、相当はまりました。Web を検索しても対策はまちまちで、論理的な解決策はありませんでした。[こんなブログ](http://blog.roy29fuku.com/virtual-reality/hololens/error-list/)を書いている人もいました。

```
CS0006
```

ホロラボの人に教えてもらうと、CS0006が出たらUnityでビルドするときのディレクトリを丸ごとけして、再作成するとうまくいくことが多い様子です。


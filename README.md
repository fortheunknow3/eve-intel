# EVE 自动预警

一个 Windows 桌面工具。它定时截取选定的屏幕区域，检测符合阈值的红色像素，并用声音和日志提醒。把监控范围框在游戏的概览列表或需要观察的红色标记上。

## 下载与启动

从 [GitHub Releases](https://github.com/fortheunknow3/eve-intel/releases) 下载 `eve-intel-v0.2.0-windows.zip`，解压整个压缩包，再运行其中的 `EVEAntiCollision.exe`。运行需要 Windows 和 .NET Framework 4.7.2 或更新的 4.x 版本。

让 `EVEAntiCollision.exe.config` 和 `sound.wav` 与 EXE 放在同一目录。想更换提示音，可以用自己的 WAV 文件替换 `sound.wav`。

## 使用步骤

1. 将 EVE 切换到能在桌面上正常显示概览的位置。点击主窗口的 **预警设置 → 鼠标框选区域**。
2. 按住鼠标左键拖出监控范围。松开后，起始坐标及宽高会自动填入。按 Esc 可以取消框选，也可以直接输入四项数值。
3. 点击 **查看效果** 核对截图。将区域限制在你要观察的列表，尽量避开无关的红色界面元素。
4. 选择提醒模式，点击 **保存并应用**。
5. 返回主窗口，点击 **开启自动预警**。再次点击同一个按钮可暂停。**清除预警信息** 会清空界面日志。

| 提醒模式 | 行为 |
| --- | --- |
| 单次提醒（默认） | 每次检测到一段新的红色状态时提醒一次。红色消失后再次出现会重新提醒。 |
| 持续提醒 | 首次检测到红色时立即提醒，红色持续存在时约每 3 秒再提醒一次。 |

预警运行中，点击 **保存并应用** 就会更新区域和提醒模式。主窗口的预览能帮助你确认新的监控范围。区域与模式按 Windows 用户保存。

程序读取实际显示的屏幕内容。预览为空、被其他窗口挡住或框到错误位置时，调整窗口和选区，再用 **查看效果** 核对。遇到截图或提示音错误时，可查看主窗口日志。

## 从源码构建

项目采用 C# WinForms，目标为 .NET Framework 4.7.2，Any CPU（首选 32 位）。使用安装了「.NET 桌面开发」工作负载的 Visual Studio，打开 `EVEAntiCollision.csproj`，或在 Visual Studio Developer PowerShell 中执行以下命令。项目通过 NuGet 还原参考程序集。

```powershell
msbuild EVEAntiCollision.csproj -restore -p:Configuration=Release
```

输出位于 `bin\Release\`。主窗口的监控循环和颜色检测在 [Form1.cs](Form1.cs)，设置及鼠标选区在 [Form3.cs](Form3.cs) 和 [RegionSelectionForm.cs](RegionSelectionForm.cs)，截图与配置保存共用 [ScreenCapture.cs](ScreenCapture.cs)。

## 项目来源

本工程基于 DragoNar Studio 的 EVE 自动预警 V0.1.3 发布版重建并扩展。原版界面注明软件免费，未经授权不得销售。本仓库的许可证文件见 [LICENSE](LICENSE)。原版署名和版权信息保留在项目说明与程序集元数据中。

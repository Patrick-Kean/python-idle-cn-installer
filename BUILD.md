# 构建说明

本项目可以使用 Windows 自带的 .NET Framework C# 编译器进行构建。

## 编译命令

在项目目录中执行：

```bat
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /target:winexe /platform:anycpu /win32icon:gear.ico /out:"Python IDLE 汉化包安装器.exe" /reference:System.dll /reference:System.Drawing.dll /reference:System.Windows.Forms.dll IdleCnInstaller.cs
```

如果系统没有 64 位编译器，也可以尝试：

```bat
C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /target:winexe /platform:anycpu /win32icon:gear.ico /out:"Python IDLE 汉化包安装器.exe" /reference:System.dll /reference:System.Drawing.dll /reference:System.Windows.Forms.dll IdleCnInstaller.cs
```

## 说明

- `/target:winexe` 表示生成 Windows 图形界面程序，不显示命令行窗口
- `/win32icon:gear.ico` 表示给 exe 嵌入齿轮图标
- `IdleCnInstaller.cs` 是主程序源码


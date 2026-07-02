# Python IDLE 汉化包安装器

这是一个用于安装 Python IDLE 汉化包的可视化安装器。

它将原本需要在命令行中手动执行的 `pip install idcn` 命令封装成了一个 Windows 图形界面程序，适合不熟悉命令行的用户使用。

## 软件截图

![安装器截图](assets/screenshot.jpg)

## 功能特点

- 提供简单的图形化安装窗口
- 一键执行 IDLE 汉化包安装
- 提供单独的汉化包卸载器
- 自动显示安装日志
- 自动兼容 `python`、`py`、`pip` 三种命令方式
- 安装成功或失败后给出明确提示
- 使用齿轮图标，便于识别

## 使用方法

### 安装汉化包

1. 前往本项目的 Releases 页面
2. 下载最新版本中的 `Python IDLE 汉化包安装器.exe`
3. 双击运行安装器
4. 点击窗口中的“开始安装”
5. 等待安装完成提示

如果安装器提示已经安装，例如：

```text
Requirement already satisfied: idcn
```

说明汉化包已经存在，无需重复安装。

### 卸载汉化包

1. 前往本项目的 Releases 页面
2. 下载最新版本中的 `Python IDLE 汉化包卸载器.exe`
3. 双击运行卸载器
4. 点击窗口中的“开始卸载”
5. 确认卸载后等待完成提示

## Python / IDLE 安装

本安装器需要电脑上已经安装 Python。

Python 官方下载地址：

[https://www.python.org/downloads/](https://www.python.org/downloads/)

IDLE 通常会随 Python 官方安装包一起安装。安装 Python 时，建议勾选：

```text
Add python.exe to PATH
```

如果安装器提示找不到 Python 或 pip，请先安装 Python，或重新安装 Python 并勾选 PATH 选项。

## 实际执行的安装命令

安装器会优先执行：

```bat
python -m pip install idcn -i https://mirrors.aliyun.com/pypi/simple
```

如果当前电脑无法识别 `python`，程序会继续尝试：

```bat
py -m pip install idcn -i https://mirrors.aliyun.com/pypi/simple
```

如果仍然不可用，会继续尝试：

```bat
pip install idcn -i https://mirrors.aliyun.com/pypi/simple
```

## 实际执行的卸载命令

卸载器会优先执行：

```bat
python -m pip uninstall -y idcn
```

如果当前电脑无法识别 `python`，程序会继续尝试：

```bat
py -m pip uninstall -y idcn
```

如果仍然不可用，会继续尝试：

```bat
pip uninstall -y idcn
```

## 环境要求

- Windows 系统
- 已安装 Python
- `python`、`py` 或 `pip` 至少有一个可以在命令行中使用
- 电脑需要能访问 Python 包安装源

## 技术实现

本项目使用 C# 和 Windows Forms 实现。

实现流程如下：

1. 使用 C# 编写 WinForms 图形界面
2. 在窗口中提供标题、说明、进度条、日志框和按钮
3. 用户点击“开始安装”后，程序启动后台任务
4. 后台任务调用系统命令执行 `pip install idcn`
5. 程序捕获命令行输出，并显示到日志框中
6. 根据命令返回码判断安装成功或失败
7. 使用 .NET Framework C# 编译器编译为 Windows `.exe`
8. 编译时嵌入齿轮图标

卸载器使用相同技术实现，只是将安装命令替换为 `pip uninstall -y idcn`。

## 文件说明

建议仓库中保留以下文件：

```text
README.md
NOTICE.md
BUILD.md
IdleCnInstaller.cs
IdleCnUninstaller.cs
gear.ico
assets/screenshot.png
```

最终给用户下载的 `.exe` 建议放在 GitHub Releases 中。

## 作者

海绵制作

联系方式：patrickhbq@gmail.com

## 声明

内容来源于互联网，仅供学习交流使用，请勿用于商业用途。

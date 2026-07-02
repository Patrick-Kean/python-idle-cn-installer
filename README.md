# Python IDLE 汉化包安装器

这是一个用于安装 Python IDLE 汉化包的可视化安装器。

它将原本需要在命令行中手动执行的 `pip install idcn` 命令封装成了一个 Windows 图形界面程序，适合不熟悉命令行的用户使用。

## 功能特点

- 提供简单的图形化安装窗口
- 一键执行 IDLE 汉化包安装
- 自动显示安装日志
- 自动兼容 `python`、`py`、`pip` 三种命令方式
- 安装成功或失败后给出明确提示
- 使用齿轮图标，便于识别

## 使用方法

1. 下载 `Python IDLE 汉化包安装器.exe`
2. 双击运行安装器
3. 点击窗口中的“开始安装”
4. 等待安装完成提示

如果安装器提示已经安装，例如：

```text
Requirement already satisfied: idcn
```

说明汉化包已经存在，无需重复安装。

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

## 文件说明

建议仓库中保留以下文件：

```text
README.md
NOTICE.md
IdleCnInstaller.cs
gear.ico
Python IDLE 汉化包安装器.exe
```

其中：

- `Python IDLE 汉化包安装器.exe` 是最终给用户双击运行的安装器
- `IdleCnInstaller.cs` 是安装器源码
- `gear.ico` 是安装器图标
- `README.md` 是项目说明
- `NOTICE.md` 是声明文件

## 作者

海绵制作

联系方式：patrickhbq@gmail.com

## 声明

内容来源于互联网，仅供学习交流使用，请勿用于商业用途。


using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IdleCnUninstaller
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UninstallerForm());
        }
    }

    internal sealed class UninstallerForm : Form
    {
        private readonly ProgressBar progressBar;
        private readonly Label statusLabel;
        private readonly TextBox logBox;
        private readonly Button uninstallButton;
        private readonly Button closeButton;

        public UninstallerForm()
        {
            Text = "Python IDLE 汉化包卸载器";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(520, 430);
            Size = new Size(520, 430);
            MaximizeBox = false;
            Font = new Font("Microsoft YaHei UI", 9F);
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            var titleLabel = new Label
            {
                Text = "Python IDLE 汉化包卸载器",
                Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold),
                Location = new Point(22, 20),
                Size = new Size(460, 34)
            };

            var descLabel = new Label
            {
                Text = "点击“开始卸载”后，将通过 pip 卸载 idcn。",
                Location = new Point(24, 62),
                Size = new Size(460, 24)
            };

            progressBar = new ProgressBar
            {
                Location = new Point(26, 100),
                Size = new Size(450, 20),
                Style = ProgressBarStyle.Blocks
            };

            statusLabel = new Label
            {
                Text = "准备就绪",
                Location = new Point(26, 130),
                Size = new Size(450, 22)
            };

            logBox = new TextBox
            {
                Location = new Point(26, 162),
                Size = new Size(450, 112),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.White
            };

            var makerLabel = new Label
            {
                Text = "海绵制作    联系方式: patrickhbq@gmail.com",
                Location = new Point(26, 286),
                Size = new Size(450, 22)
            };

            var copyrightLabel = new Label
            {
                Text = "版权说明: 内容来源于互联网，仅供学习交流使用，请勿用于商业用途。",
                Location = new Point(26, 310),
                Size = new Size(450, 22)
            };

            uninstallButton = new Button
            {
                Text = "开始卸载",
                Location = new Point(264, 344),
                Size = new Size(100, 32)
            };
            uninstallButton.Click += UninstallButton_Click;

            closeButton = new Button
            {
                Text = "关闭",
                Location = new Point(376, 344),
                Size = new Size(100, 32)
            };
            closeButton.Click += delegate { Close(); };

            Controls.AddRange(new Control[]
            {
                titleLabel,
                descLabel,
                progressBar,
                statusLabel,
                logBox,
                makerLabel,
                copyrightLabel,
                uninstallButton,
                closeButton
            });
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                this,
                "确定要卸载 Python IDLE 汉化包 idcn 吗？",
                "确认卸载",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            uninstallButton.Enabled = false;
            closeButton.Enabled = false;
            progressBar.Style = ProgressBarStyle.Marquee;
            statusLabel.Text = "正在卸载，请稍候...";
            logBox.Text = "正在运行 python -m pip uninstall -y idcn";

            var worker = new System.ComponentModel.BackgroundWorker();
            worker.DoWork += delegate(object s, System.ComponentModel.DoWorkEventArgs args)
            {
                args.Result = RunUninstall();
            };
            worker.RunWorkerCompleted += delegate(object s, System.ComponentModel.RunWorkerCompletedEventArgs args)
            {
                var result = (UninstallResult)args.Result;
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Value = 100;
                uninstallButton.Enabled = true;
                closeButton.Enabled = true;

                logBox.Text = result.Output.Trim();

                if (result.ExitCode == 0)
                {
                    statusLabel.Text = "卸载完成。";
                    MessageBox.Show(this, "Python IDLE 汉化包卸载完成。", "卸载完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    statusLabel.Text = "卸载失败，请查看日志。";
                    MessageBox.Show(this, "卸载失败，请查看窗口中的日志。", "卸载失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            worker.RunWorkerAsync();
        }

        private static UninstallResult RunUninstall()
        {
            string[] candidates = {
                "python -m pip uninstall -y idcn",
                "py -m pip uninstall -y idcn",
                "pip uninstall -y idcn"
            };

            var output = new StringBuilder();

            foreach (string command in candidates)
            {
                UninstallResult result = RunCommand(command);
                output.AppendLine("> " + command);
                output.AppendLine(result.Output);

                if (result.ExitCode == 0)
                {
                    result.Output = output.ToString();
                    return result;
                }

                if (result.ExitCode != 9009)
                {
                    result.Output = output.ToString();
                    return result;
                }
            }

            return new UninstallResult
            {
                ExitCode = 9009,
                Output = output + Environment.NewLine + "未找到 Python 或 pip。请先安装 Python，并勾选 Add Python to PATH。"
            };
        }

        private static UninstallResult RunCommand(string command)
        {
            try
            {
                var startInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                };

                using (Process process = Process.Start(startInfo))
                {
                    string stdout = process.StandardOutput.ReadToEnd();
                    string stderr = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    return new UninstallResult
                    {
                        ExitCode = process.ExitCode,
                        Output = stdout + stderr
                    };
                }
            }
            catch (Exception ex)
            {
                return new UninstallResult
                {
                    ExitCode = 1,
                    Output = ex.Message
                };
            }
        }

        private sealed class UninstallResult
        {
            public int ExitCode { get; set; }
            public string Output { get; set; }
        }
    }
}

using Microsoft.NodejsTools.SharedProject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace RA3Diagnosis
{
    public partial class MainForm : Form
    {
        private string? _selectedPath;
        public MainForm()
        {
            InitializeComponent();
            TryUpdatePath();
            UpdateView();
        }

        public bool TryUpdatePath()
        {
            if (Registry.IsGamePathValid(Path.GetFullPath(Directory.GetCurrentDirectory())))
            {
                _selectedPath = Path.GetFullPath(Directory.GetCurrentDirectory());
                return true;
            }
            else if (Registry.IsGameRegistryPathValid())
            {
                _selectedPath = Registry.GetGamePath();
                return true;
            }
            return false;
        }

        public bool TryUpdatePath(string path)
        {
            if (Registry.IsGamePathValid(path))
            {
                _selectedPath = path;
                return true;
            }
            return false;
        }

        public void UpdateView()
        {
            if (_selectedPath != null)
            {
                gamePathText.Text = _selectedPath;
                SetButtonState(true);
            }
            else
            {
                gamePathText.Text = "�޷��ҵ���ĺ�ɫ����3����ѱ����߷ŵ���ɫ����3��Ŀ¼��������İ�ť�ֶ�ѡ���ɫ����3��";
                SetButtonState(false);
            }
        }

        public void SetButtonState(bool hasValidPath)
        {
            selectGamePathButton.Enabled = true;
            launchGameButton.Enabled = hasValidPath;
            launchGameCenterButton.Enabled = hasValidPath;
            diagnosisGameButton.Enabled = hasValidPath;
            memoryExtensionButton.Enabled = hasValidPath;
            fixRegistryButton.Enabled = hasValidPath;
            clearRegistryButton.Enabled = hasValidPath;
            openGameRootFolderButton.Enabled = hasValidPath;
            openMapsFolderButton.Enabled = hasValidPath;
            openModsFolderButton.Enabled = hasValidPath;
            openProfileFolderButton.Enabled = hasValidPath;
            openReplaysFolderButton.Enabled = hasValidPath;
        }

        private void SelectGamePathButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("��ѡ����Ϸ��װĿ¼�µ�RA3.exe�ļ���ʹ�ñ����ߡ�");
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "ѡ����Ϸ��װĿ¼�µ�RA3.exe";
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "RA3 Game |RA3.exe";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!TryUpdatePath(openFileDialog.FileName.Substring(0, openFileDialog.FileName.Length - 8)))
                    {
                        MessageBox.Show("�ⲻ��һ����ȷ��·����");
                    }
                }
            }
            UpdateView();
        }

        private void CopyPathButton_Click(object sender, EventArgs e)
        {
            if (_selectedPath != null)
            {
                MessageBox.Show("�Ѹ���·���������壡");
                Clipboard.SetText(gamePathText.Text);
            }
        }

        private void CopyReportButton_Click(object sender, EventArgs e)
        {
            if (_selectedPath != null)
            {
                MessageBox.Show("�Ѹ�����ϵ������壡");
                Clipboard.SetText(diagnosisResultText.Text);
            }
        }

        private void OpenGameRootFolderButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(_selectedPath))
                {
                    Process.Start("explorer.exe", _selectedPath);
                }
                else
                {
                    MessageBox.Show("�ļ��в����ڣ���ѡ���ɫ����3·����ִ����ϡ�");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"���Դ��ļ��е�ʱ��������\r\n{ex}");
            }
        }

        private void OpenReplaysFolderButton_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Red Alert 3"), "Replays");
            try
            {
                if (Directory.Exists(path))
                {
                    Process.Start("explorer.exe", path);
                }
                else
                {
                    MessageBox.Show("�ļ��в����ڣ���ѡ���ɫ����3·����ִ����ϡ�");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"���Դ��ļ��е�ʱ��������\r\n{ex}");
            }
        }

        private void OpenProfileFolderButton_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Red Alert 3"), "Profiles");
            try
            {
                if (Directory.Exists(path))
                {
                    Process.Start("explorer.exe", path);
                }
                else
                {
                    MessageBox.Show("�ļ��в����ڣ���ѡ���ɫ����3·����ִ����ϡ�");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"���Դ��ļ��е�ʱ��������\r\n{ex}");
            }
        }

        private void OpenMapsFolderButton_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Red Alert 3"), "Maps");
            try
            {
                if (Directory.Exists(path))
                {
                    Process.Start("explorer.exe", path);
                }
                else
                {
                    MessageBox.Show("�ļ��в����ڣ���ѡ���ɫ����3·����ִ����ϡ�");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"���Դ��ļ��е�ʱ��������\r\n{ex}");
            }
        }

        private void OpenModsFolderButton_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Red Alert 3"), "Mods");
            try
            {
                if (Directory.Exists(path))
                {
                    Process.Start("explorer.exe", path);
                }
                else
                {
                    MessageBox.Show("�ļ��в����ڣ���ѡ���ɫ����3·����ִ����ϡ�");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"���Դ��ļ��е�ʱ��������\r\n{ex}");
            }
        }

        private bool TryFixRegistry()
        {
            try
            {
                if (_selectedPath != null && Directory.Exists(_selectedPath))
                {
                    var key = Registry.GetKey();
                    if (ShowKeyDialog(ref key) == DialogResult.OK)
                    {
                        if (string.IsNullOrEmpty(key))
                        {
                            key = Guid.NewGuid().ToString();
                        }
                        Registry.FixGameRegistry(_selectedPath, key);
                        MessageBox.Show("ע����޸��ɹ���");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("��ȡ��ע����޸���");
                    }
                }
                else
                {
                    MessageBox.Show("�޷��ҵ���ĺ�ɫ����3����ѱ����߷ŵ���ɫ����3��Ŀ¼��������İ�ť�ֶ�ѡ���ɫ����3��");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�����޸�ע����ʱ��������\r\n{ex}");
            }
            return false;
        }

        private void FixRegistryButton_Click(object sender, EventArgs e)
        {
            TryFixRegistry();
        }

        private void ClearRegistryButton_Click(object sender, EventArgs e)
        {
            try
            {
                Registry.ClearGameRegistry();
                MessageBox.Show("ע���ж�سɹ���");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"����ж��ע����ʱ��������\r\n{ex}");
            }
        }

        private static DialogResult ShowKeyDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(500, 100);
            var inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "���������CDKEY";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            var noteLabel1 = new Label();
            noteLabel1.Name = "noteLabel1";
            noteLabel1.Size = new System.Drawing.Size(320, 20);
            noteLabel1.Font = new Font("Arial", 8);
            noteLabel1.Text = "����д����д�����CDKEY����Ӱ���޸�";
            noteLabel1.Location = new System.Drawing.Point(0, 40);
            inputBox.Controls.Add(noteLabel1);
            var noteLabel2 = new Label();
            noteLabel2.Name = "noteLabel2";
            noteLabel2.Size = new System.Drawing.Size(320, 20);
            noteLabel2.Font = new Font("Arial", 8);
            noteLabel2.Text = "���ǿ���Υ����EA���û�Э�飡";
            noteLabel2.Location = new System.Drawing.Point(0, 60);
            inputBox.Controls.Add(noteLabel2);

            var okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 30);
            okButton.Text = "&ȷ��";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 50);
            inputBox.Controls.Add(okButton);

            var cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 30);
            cancelButton.Text = "&ȡ��";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 50);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            var result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void CheckFolder(string folderName, string path)
        {
            if (Directory.Exists(path))
            {
                diagnosisResultText.Text += $"- ��� - {folderName}���ɹ���\r\n";
            }
            else
            {
                diagnosisResultText.Text += $"- ���� - �Ҳ���{folderName}��\r\n";
                if (MessageBox.Show($"�Ƿ����̴���{folderName}��", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                        diagnosisResultText.Text += $"- �ѽ�� - �Ѿ��ɹ�����{folderName}��\r\n";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"���Դ���{folderName}��ʱ��������\r\n{ex}");
                        diagnosisResultText.Text += $"- ������� - ���ֶ�����{folderName}��\r\n";
                    }
                }
                else
                {
                    diagnosisResultText.Text += $"- ������� - ���ֶ�����{folderName}��\r\n";
                }
            }
        }

        private void DiagnosisGameButton_Click(object sender, EventArgs e)
        {
            diagnosisResultText.Text = "��ʼ���...\r\n";
            if (_selectedPath == null)
            {
                diagnosisResultText.Text += "�޷��ҵ���ĺ�ɫ����3����ѱ����߷ŵ���ɫ����3��Ŀ¼��������İ�ť�ֶ�ѡ���ɫ����3��\r\n";
                return;
            }

            var possibleUnofficialGamePath = Path.Combine(Path.Combine(_selectedPath, "Data"), "ra3_1.13.game");
            if (File.Exists(possibleUnofficialGamePath))
            {
                diagnosisResultText.Text += "- ���� - ��Ϸ�汾�ƺ���1.13������1.12������һ���ǹٷ��汾���޷����ж�����Ϸ��\r\n";
            }

            if (_selectedPath.All(x => x < 128))
            {
                diagnosisResultText.Text += "- ��� - ��Ŀ¼·�����ɹ���\r\n";
            }
            else
            {
                diagnosisResultText.Text += "- ���� - ��ɫ����3��װĿ¼���з�ASCII�ַ����������ģ���\r\n";
                diagnosisResultText.Text += "- ������� - ����İ�װĿ¼��Ȼ���޸�ע���\r\n";
            }
            var hasInvalidSkuDef = false;
            foreach (var file in Directory.GetFiles(_selectedPath))
            {
                if (Path.GetExtension(file) == ".SkuDef")
                {
                    if (!file.All(x => x < 128))
                    {
                        diagnosisResultText.Text += $"- ���� - SkuDef�ļ��Ϸ�ASCII�ַ�: {Path.GetFileNameWithoutExtension(file)}��\r\n";
                        hasInvalidSkuDef = true;
                    }
                }
            }
            if (hasInvalidSkuDef)
            {
                diagnosisResultText.Text += "- ������� - ��������SkuDef�ļ�����֤·����ֻ��ASCII�ַ���\r\n";
            }
            else
            {
                diagnosisResultText.Text += "- ��� - SkuDef�ļ�·�����ɹ���\r\n";
            }

            var gameFilePath = Path.Combine(Path.Combine(_selectedPath, "Data"), "ra3_1.12.game");
            if (File.Exists(gameFilePath))
            {
                if (LargeAddress.IsLargeAddressEnabled(gameFilePath))
                {
                    diagnosisResultText.Text += "- ��� - �������ڴ���չ��\r\n";
                }
                else
                {
                    diagnosisResultText.Text += "- ���� - δ�����ڴ���չ�����ܻᵼ�²���ģ���޷���ȷʹ�á�\r\n";
                    if (MessageBox.Show("�Ƿ����������ڴ���չ��", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        LargeAddress.EnableLargeAddress(gameFilePath);
                        diagnosisResultText.Text += "- �ѽ�� - �ڴ���չ�����á�\r\n";
                    }
                    else
                    {
                        diagnosisResultText.Text += "- ������� - ���������ڴ���չ��\r\n";
                    }
                }
            }
            else
            {
                MessageBox.Show("�޷��ҵ���ĺ�ɫ����3����ѱ����߷ŵ���ɫ����3��Ŀ¼��������İ�ť�ֶ�ѡ���ɫ����3��");
            }

            if (Registry.IsRegistryValid())
            {
                diagnosisResultText.Text += "- ��� - ע�����ɹ���\r\n";
            }
            else
            {
                diagnosisResultText.Text += "- ���� - ע�������������\r\n";
                if (MessageBox.Show("�Ƿ������޸�ע���", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (TryFixRegistry())
                    {
                        diagnosisResultText.Text += "- �ѽ�� - �Ѿ��ɹ����ע������⡣\r\n";
                    }
                    else
                    {
                        diagnosisResultText.Text += "- ������� - ���޸�ע���\r\n";
                    }
                }
                else
                {
                    diagnosisResultText.Text += "- ������� - ���޸�ע���\r\n";
                }
            }

            // ��ʼ����ļ���
            var replaysPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Red Alert 3"), "Replays");
            CheckFolder("¼���ļ���", replaysPath);
            var modsPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Red Alert 3"), "Mods");
            CheckFolder("ģ���ļ���", modsPath);
            var mapsPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Red Alert 3"), "Maps");
            CheckFolder("��ͼ�ļ���", mapsPath);
            var profilesPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Red Alert 3"), "Profiles");
            CheckFolder("�û��ļ���", profilesPath);

            // ��ʼ����ͼ�ļ���
            if (Directory.Exists(mapsPath))
            {
                var mapsSize = Directory.GetDirectories(mapsPath).Length;
                if (mapsSize > 200)
                {
                    diagnosisResultText.Text += $"- ���� - �ѷ���{mapsSize}�ŵ�ͼ��������������϶��ᵼ�´��������򿪵�ͼ�ļ��У�Ȼ�󽫵�ͼ�������͵�50���¡�\r\n";
                }
                else if (mapsSize > 100)
                {
                    diagnosisResultText.Text += $"- ���� - �ѷ���{mapsSize}�ŵ�ͼ�����������һ�����ܵ��´��������򿪵�ͼ�ļ��У�Ȼ�󽫵�ͼ�������͵�50���¡�\r\n";
                }
                else if (mapsSize > 50)
                {
                    diagnosisResultText.Text += $"- ���� - �ѷ���{mapsSize}�ŵ�ͼ����������������ᵼ�´��󣬵��Խ���򿪵�ͼ�ļ��У�Ȼ�󽫵�ͼ�������͵�50���¡�\r\n";
                }
                else
                {
                    diagnosisResultText.Text += $"- ��� - ��ͼ�ļ��м����ɣ��ѷ���{mapsSize}�ŵ�ͼ���������һ�����ᵼ�´���\r\n";
                }
            }
            else
            {
                diagnosisResultText.Text += "- ���� - ��ͼ�ļ��в����ڡ�\r\n";
            }

            // ��ʼ���options.ini
            var hasInvalidOption = false;
            foreach (var profile in Directory.GetDirectories(profilesPath))
            {
                var options = Path.Combine(profile, "Options.ini");
                if (File.Exists(options))
                {
                    var allLines = File.ReadAllLines(options);
                    var result = new List<string>();
                    foreach (var line in allLines)
                    {
                        if (!line.StartsWith("FirewallPortOverride"))
                        {
                            result.Add(line);
                        }
                        else
                        {
                            hasInvalidOption = true;
                        }
                    }
                    File.WriteAllLines(options, result.ToArray());
                }
            }
            if (hasInvalidOption)
            {
                diagnosisResultText.Text += "- �ѽ�� - ���ܻᵼ�¶�����Ϸ�޷����ӵĴ���������ɾ����\r\n";
            }
            else
            {
                diagnosisResultText.Text += "- ��� - û�з��ֿ��ܻᵼ�´�������á�\r\n";
            }

            // ��ʼ���skirmish.ini �ð� û����� ���ǿ���ɾ
            if (MessageBox.Show("�Ƿ�����������ս�޷���ȷѡ����Ӫ���߶�����Ϸ�����������⣿", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var profile in Directory.GetDirectories(profilesPath))
                {
                    var skirmish = Path.Combine(profile, "Skirmish.ini");
                    if (File.Exists(skirmish))
                    {
                        File.Delete(skirmish);
                    }
                }
                diagnosisResultText.Text += "- �ѽ�� - ����ս�������Ϸ׼�����������޸���ɡ�\r\n";
            }
            else
            {
                diagnosisResultText.Text += "- ���� - û������ս�������Ϸ׼���������⡣\r\n";
            }

            diagnosisResultText.Text += "�����о���һЩ���ܵģ�������������޷�����������\r\n";
            diagnosisResultText.Text += "�����Ϸ������ս���߶�����Ϸ������ս����˲���������¼�񲻱�����" +
                "���п�����Windows Defender����������ȫ�����ֹ�˺�ɫ����3����¼���ļ��С�" +
                "��ر���Щ��ȫ����ԡ��ĵ������Ŀ¼�ı�����\r\n";
            diagnosisResultText.Text += "��������˶�����Ϸ����ʧ�ܣ���رշ���ǽ����һ�Ρ�\r\n";

            diagnosisResultText.Text += "������\r\n";
        }

        private void MemoryExtensionButton_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Path.Combine(_selectedPath, "Data"), "ra3_1.12.game");
            if (File.Exists(path))
            {
                if (LargeAddress.IsLargeAddressEnabled(path))
                {
                    MessageBox.Show("�Ѿ���������ظ����");
                }
                else
                {
                    LargeAddress.EnableLargeAddress(path);
                    MessageBox.Show("�����ļ��Ѵ��棬����ɹ���");
                }

            }
            else
            {
                MessageBox.Show("�޷��ҵ���ĺ�ɫ����3����ѱ����߷ŵ���ɫ����3��Ŀ¼��������İ�ť�ֶ�ѡ���ɫ����3��");
            }
        }

        private void LaunchGameButton_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(_selectedPath, "RA3.exe");
            if (File.Exists(path))
            {
                SystemUtility.ExecuteProcessUnElevated(path, "");
            }
            else
            {
                MessageBox.Show("�޷��ҵ���ĺ�ɫ����3����ѱ����߷ŵ���ɫ����3��Ŀ¼��������İ�ť�ֶ�ѡ���ɫ����3��");
            }
        }

        private void LaunchGameCenterButton_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(_selectedPath, "RA3.exe");
            if (File.Exists(path))
            {
                SystemUtility.ExecuteProcessUnElevated(path, "-ui");
            }
            else
            {
                MessageBox.Show("�޷��ҵ���ĺ�ɫ����3����ѱ����߷ŵ���ɫ����3��Ŀ¼��������İ�ť�ֶ�ѡ���ɫ����3��");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Try to show program version on title.
                this.Text += $" {Assembly.GetExecutingAssembly().GetName().Version}";
            }
            catch { }
        }
    }
}
using System.Diagnostics;
using GreenField.BLL.Services.Interfaces;

namespace GreenField.BLL.Services
{
    public class BackupService : IBackupService
    {
        private const string CdToMongoToolsCommand = "cd C:\\Program Files\\MongoDB\\Tools\\100\\\\bin";
        private const string MongoDumpCommand = "mongodump --db green-field-database --out C:\\Users\\Arseniy\\Desktop\\Backup";
        private const string MongoRestoreCommand = "mongorestore --db green-field-database C:\\Users\\Arseniy\\Desktop\\Backup\\green-field-database";
        public void MakeBackup()
        {
            var cmd = CreateCmdProcess();
            cmd.StandardInput.WriteLine(CdToMongoToolsCommand);
            cmd.StandardInput.Flush();
            cmd.StandardInput.WriteLine(MongoDumpCommand);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }

        public void Restore()
        {
            var cmd = CreateCmdProcess();
            cmd.StandardInput.WriteLine(CdToMongoToolsCommand);
            cmd.StandardInput.Flush();
            cmd.StandardInput.WriteLine(MongoRestoreCommand);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }

        private Process CreateCmdProcess()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            return cmd;
        }
    }
}
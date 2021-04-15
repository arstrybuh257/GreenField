namespace GreenField.BLL.Services.Interfaces
{
    public interface IBackupService
    {
        void MakeBackup();
        void Restore();
    }
}
using RED.Models.ElectronicDiary;

namespace RED.Repositories.Abstract
{
    public interface IMiscRepository
    {
        AcreditationMetaW GetAcreditationRegistry();

        void SaveAcreditationRegistry(AcreditationMetaW model);
    }
}
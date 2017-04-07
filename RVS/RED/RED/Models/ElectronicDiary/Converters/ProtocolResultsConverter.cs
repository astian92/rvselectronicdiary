using System;
using RED.Models.DataContext;

namespace RED.Models.ElectronicDiary.Converters
{
    public class ProtocolResultsConverter
    {
        /// <summary>
        /// Shallow copy.
        /// </summary>
        public ProtocolResult ConvertFromArchived(ArchivedProtocolResult aProtocolResult)
        {
            var protocolResult = new ProtocolResult();

            protocolResult.ResultNumber = aProtocolResult.ResultNumber;
            protocolResult.Results = aProtocolResult.Results;

            var converter = new ProductTestsConverter();
            protocolResult.ProductTest = converter.ConvertFromArchived(aProtocolResult.ArchivedProductTest);

            return protocolResult;
        }

        /// <summary>
        /// Shallow copy.
        /// </summary>
        public ArchivedProtocolResult ConvertToArchived(ProtocolResult productTest)
        {
            throw new NotImplementedException();
        }
    }
}
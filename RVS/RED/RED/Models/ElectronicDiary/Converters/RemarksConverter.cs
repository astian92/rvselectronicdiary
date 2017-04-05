using RED.Models.DataContext;
using System;

namespace RED.Models.ElectronicDiary.Converters
{
    public class RemarksConverter
    {
        /// <summary>
        /// Shallow copy.
        /// </summary>
        public Remark ConvertFromArchived(ArchivedProtocolRemark aRemark)
        {
            var remark = new Remark();

            remark.Text = aRemark.Remark;

            return remark;
        }

        /// <summary>
        /// Shallow copy.
        /// </summary>
        public ArchivedProtocolRemark ConvertToArchived(Remark productTest)
        {
            throw new NotImplementedException();
        }
    }
}
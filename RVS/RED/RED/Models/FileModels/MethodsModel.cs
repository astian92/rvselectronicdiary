using System;

namespace RED.Models.FileModels
{
    public class MethodsModel : IEquatable<MethodsModel>
    {
        public string TestName { get; set; }

        public string TestMethod { get; set; }

        //So the Distinct works
        public bool Equals(MethodsModel other)
        {
            if (this.TestName == other.TestName && this.TestMethod == other.TestMethod)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return
                this.TestName != null ? this.TestName.GetHashCode() : 0
                +
                this.TestMethod != null ? this.TestMethod.GetHashCode() : 0;
        }
    }
}
